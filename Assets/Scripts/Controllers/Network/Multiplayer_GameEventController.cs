using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_GameEventController : MonoBehaviour
{
    public delegate void OnEventReturn<T>(T returnData);
    public OnEventReturn<RollResult_MSG> OnRollResultReturn;
    public OnEventReturn<LuckyDraw_MSG> OnLuckyDrawReturn;
    public OnEventReturn<Chance_MSG> OnChanceReturn;
    public OnEventReturn<Battle_MSG> OnBattleReturn;
    public OnEventReturn <TokenBalance> OnBalanceReturn;

    private UserDataManager UserDataManager;
    private NetworkManager NetworkManager;

    // Start is called before the first frame update
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;

        
        BountyColyseusManager.Instance.onGameReceiveMsg = null;
        BountyColyseusManager.Instance.onGameReceiveMsg += HandleGameMessage;
    }

    #region Handle Game Event
    void HandleGameMessage(GAMEROOM_RECEIVE_EVENTS messageType, object message)
    {
        try
        {
            switch (messageType)
            {
                case GAMEROOM_RECEIVE_EVENTS.ROLL_RESULT:
                    RollResult_MSG rollMessage = JsonUtility.FromJson<RollResult_MSG>(message.ToString());
                    Debug.Log("[GameEventController] Roll Success...");
                    OnRollSuccess(rollMessage);
                    break;
                case GAMEROOM_RECEIVE_EVENTS.FIGHT_RESULT:
                    Debug.Log("[GameEventController] FIGHT_RESULT");
                    Battle_MSG battle_msg = JsonUtility.FromJson<Battle_MSG>(message.ToString());
                    OnBattleEnd(battle_msg);
                    break;
                case GAMEROOM_RECEIVE_EVENTS.LUCKY_DRAW_RESULT:
                    Debug.Log("[GameEventController] LUCKY_DRAW_RESULT");
                    LuckyDraw_MSG ld_msg = JsonUtility.FromJson<LuckyDraw_MSG>(message.ToString());
                    OnLuckyDrawSuccess(ld_msg);
                    break;
                case GAMEROOM_RECEIVE_EVENTS.BALANCE_RESULT:
                    Debug.Log("[GameEventController] BALANCE_RESULT");
                    break;
                case GAMEROOM_RECEIVE_EVENTS.CHANCE_RESULT:
                    Debug.Log("[GameEventController] CHANCE_RESULT");
                    Chance_MSG chance_msg = JsonUtility.FromJson<Chance_MSG>(message.ToString());
                    OnChanceEnd(chance_msg);
                    break;
                default:
                    OnDefaultEnd();
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[HandleGameMessage] ERROR:" + ex.Message);
        }        
    }


    /// <summary>
    /// Handle the Roll Dice Event with the Server.
    /// </summary>
    public void HandleRoll()
    {
        if (UserDataManager.GetEnergy() > 0)
        {
            Debug.Log("[Multiplayer_GameEventController] Send Roll event...");
            NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.ROLL_DICE.ToString());
        }
        else
        {
            //Call external Refill feature.
            NetworkManager.Refill_External();
        }
    }

    void OnRollSuccess(RollResult_MSG rollResult)
    {
        Debug.Log("[Multiplayer_GameEventController] OnRollSuccess");
        UserDataManager.UserGameStatus.UpdateGameStatus(rollResult);
        OnRollResultReturn?.Invoke(rollResult);
    }

    /// <summary>
    /// Send Lucky Draw to Server
    /// </summary>
    public void HandleLuckyDraw()
    {
        Debug.Log("[Multiplayer_GameEventController] Send Lucky Draw...");
        NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.LUCKY_DRAW.ToString());
    }

    /// <summary>
    /// On Lucky Draw return from Server.
    /// </summary>
    /// <param name="luckyDraw"></param>
    private void OnLuckyDrawSuccess(LuckyDraw_MSG luckyDraw)
    {
        Debug.Log("[Multiplayer_GameEventController] OnLuckyDrawSuccess");
        OnLuckyDrawReturn?.Invoke(luckyDraw);
    }

    /// <summary>
    /// Handle Combat data.
    /// </summary>
    public void Handle_Combat()
    {
        Debug.Log("[Multiplayer_GameEventController] Send Combat...");
        //Only for test: deny the battle
        Battle_MSG msg = new Battle_MSG()
        {
            skip = false,
            status = 1,
            battleProgress = new AttackResult_MSG[0],
        };
        string jsonMsg = JsonUtility.ToJson(msg);
        NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.FIGHT.ToString(), jsonMsg);
    }

    /// <summary>
    /// Send Battle Message from Server
    /// </summary>
    /// <param name="battle_MSG"></param>
    public void OnBattleEnd(Battle_MSG battle_MSG)
    {
        //Debug.Log("[Multiplayer_GameEventController] OnBattleEnd");
        string jsonMsg = JsonUtility.ToJson(battle_MSG);
        Debug.Log("[Multiplayer_GameEventController] OnBattleEnd: " + jsonMsg);
        OnBattleReturn?.Invoke(battle_MSG);
    }

    /// <summary>
    /// Send Chance Message from Server
    /// </summary>
    public void Handle_Chance()
    {
        Debug.Log("[Multiplayer_GameEventController] Send Chance...");
        NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.CHANCE.ToString());
    }

    public void OnChanceEnd(Chance_MSG chance_MSG)
    {
        Debug.Log("[Multiplayer_GameEventController] OnChanceEnd");
        OnChanceReturn?.Invoke(chance_MSG);
    }

    public void Handle_Other_Default()
    {
        Debug.Log("[Multiplayer_GameEventController] Other action.");
        NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.DEFAULT.ToString());
    }

    public void OnDefaultEnd()
    {

    }

    public void OnBalanceUpdate(TokenBalance tokenBalance)
    {
        string jsonMsg = JsonUtility.ToJson(tokenBalance);
        Debug.Log("[Multiplayer_GameEventController] On Balance Update: " + jsonMsg);
        OnBalanceReturn?.Invoke(tokenBalance);
    }
    void HandleExitGame()
    {
        NetworkManager.Disconnect();
    }
    #endregion
}

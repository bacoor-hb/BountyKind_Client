using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicesController : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public OnEventTriggered<int> OnAllDiceFinishAnim;

    [SerializeField]
    Transform[] SpawnPos;
    [SerializeField]
    DiceWithValue[] Dices;

    /// <summary>
    /// Setup the Module, and then set the position of the dice to the Spawn Pos
    /// </summary>
    /// <param name="_spawnPos">The spawn pos will take the default position of the dices if the Input is null</param>
    public void Init(Transform[] _spawnPos = null)
    {
        if (_spawnPos != null && _spawnPos.Length >= Dices.Length)
        {
            SpawnPos = _spawnPos;
        }
        
        if(SpawnPos.Length < Dices.Length)
        {
            Debug.LogError("[DiceWithValueController] Invalid data length.");
            return;
        }
        else
        {
            for(int i = 0; i < Dices.Length; i++)
            {
                Dices[i].Init();
                Dices[i].OnFinishAnim = null;
                Dices[i].OnFinishAnim += SetDiceReady;
            }

            ResetDicesPosition();
        }       
    }

    void SetDiceReady(int index)
    {
        Debug.Log("Dice " + index + " is ready.");
        diceReady++;

        if(diceReady == Dices.Length)
        {
            OnAllDiceFinishAnim?.Invoke(diceReady);
        }
    }

    /// <summary>
    /// The Dice need to be reset if we want to roll more.
    /// </summary>
    public void ResetDicesPosition()
    {
        for (int i = 0; i < Dices.Length; i++)
        {
            Dices[i].transform.SetPositionAndRotation(SpawnPos[i].position, SpawnPos[i].rotation);
            Dices[i].ResetDice();
        }
        diceReady = Dices.Length;
    }

    int diceReady = 0;
    /// <summary>
    /// Command to roll all the dices
    /// </summary>
    /// <param name="diceValues">Wish list for each dice</param>
    public void RollDice(int[] diceValues)
    {
        if(diceReady != Dices.Length)
        {
            Debug.LogError("[DiceWithValueController][RollDice] Dices are not ready: " + diceReady);
            return;
        }

        if(diceValues.Length != Dices.Length)
        {
            Debug.LogError("[DiceWithValueController][RollDice] Invalid Input data length.");
            return;
        }

        for (int i = 0; i < Dices.Length; i++)
        {
            //Debug.Log("[RollDice] Value = " + diceValues[i]);
            Dices[i].RollDice(diceValues[i]);
        }
    }

    public int GetDicesCount()
    {
        return Dices.Length;
    }
} 

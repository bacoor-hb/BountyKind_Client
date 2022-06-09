using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_LoadMap : LoadingAction
{
    private MapNodeDataManager MapNodeDataManager;
    private NetworkManager NetworkManager;

    private string selectedMapKey;
    public void Init(string _actionDetail, float _progressPercent, string _selectedMapKey)
    {
        base.Init(_actionDetail, _progressPercent);
        NetworkManager = GlobalManager.Instance.NetworkManager;
        MapNodeDataManager = GlobalManager.Instance.MapNodeDataManager;
        selectedMapKey = _selectedMapKey;
    }

    public override void StartAction()
    {
        base.StartAction();
        NetworkManager.Send(SEND_TYPE.LOBBY_SEND, LOBBY_SENT_EVENTS.MAP_NODE.ToString(), selectedMapKey);
        StartCoroutine(CheckMapReturn());
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    IEnumerator CheckMapReturn()
    {
        if (MapNodeDataManager == null 
            || MapNodeDataManager.cachedMap == null
            || !MapNodeDataManager.cachedMap.ContainsKey(selectedMapKey)
            || MapNodeDataManager.cachedMap[selectedMapKey].nodes == null
            || MapNodeDataManager.cachedMap[selectedMapKey].nodes.Count <= 0)
        {
            Debug.Log("[CheckMap] Waiting for the Map Response...");
            yield return new WaitForSeconds(0.5f);
        }

        EndAction();
    }
}

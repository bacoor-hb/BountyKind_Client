using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_LoadMap : LoadingAction
{
    private MapNodeDataManager MapNodeDataManager;

    public override void Init(string _actionDetail, float _progressPercent)
    {
        base.Init(_actionDetail, _progressPercent);
        MapNodeDataManager = GlobalManager.Instance.MapNodeDataManager;
    }

    public override void StartAction()
    {
        base.StartAction();
        StartCoroutine(CheckMap());
    }

    public override void EndAction()
    {
        base.EndAction();
    }

    IEnumerator CheckMap()
    {
        if (MapNodeDataManager == null 
            || MapNodeDataManager.currentMap == null
            || MapNodeDataManager.currentMap.nodes == null
            || MapNodeDataManager.currentMap.nodes.Count <= 0)
        {
            Debug.Log("[CheckMap] Waiting for the Map Response...");
            yield return new WaitForSeconds(0.5f);
        }

        EndAction();
    }
}

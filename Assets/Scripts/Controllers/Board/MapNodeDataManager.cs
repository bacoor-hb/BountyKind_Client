using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeDataManager : MonoBehaviour
{
    public List<MapShort_MSG> mapList;
    public Dictionary<string,BountyMap> cachedMap;

    UserDataManager UserDataManager;
    // Start is called before the first frame update
    public void Init()
    {
        mapList = new List<MapShort_MSG>();
        cachedMap = new Dictionary<string, BountyMap>();

        UserDataManager = GlobalManager.Instance.UserDataManager;
    }

    /// <summary>
    /// Update Map list from the server Schema
    /// </summary>
    /// <param name="mapShortList"></param>
    public void UpdateMapList(MapShortList_MSG mapShortList)
    {
        mapList = new List<MapShort_MSG>();
        for (int i = 0; i < mapShortList.maps.Length; i++)
        {
            mapList.Add(new MapShort_MSG(mapShortList.maps[i].key, mapShortList.maps[i].name, mapShortList.maps[i].totalNode));
        }
    }

    /// <summary>
    /// Update Map detail to the client cache
    /// </summary>
    /// <param name="mapSchema"></param>
    public void UpdateMap(Map_MSG mapSchema)
    {
        if(mapSchema == null|| mapSchema.nodes == null)
        {
            Debug.LogError("[UpdateMap] Map Data Invalid...");
            return;
        }

        if(this.cachedMap.ContainsKey(mapSchema.key))
        {
            Debug.Log("[UpdateMap] Map existed...");
            return;
        }

        var cachedMap = new BountyMap
        {
            key = mapSchema.key,
            name = mapSchema.name,
            totalNode = Mathf.RoundToInt(mapSchema.totalNode),
            nodes = new List<MapNode>()
        };

        for (int i = 0; i < cachedMap.totalNode; i++)
        {
            MapNode node = new MapNode(mapSchema.nodes[i]);
            cachedMap.nodes.Add(node);
        }

        this.cachedMap.Add(cachedMap.key, cachedMap);
        UserDataManager.UserGameStatus.UpdateMapData();
    }
}

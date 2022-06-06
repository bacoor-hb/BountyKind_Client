using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeDataManager : MonoBehaviour
{
    public List<MapShort_MSG> mapList;
    public BountyMap currentMap;
    // Start is called before the first frame update
    public void Init()
    {
        mapList = new List<MapShort_MSG>();
        currentMap = new BountyMap();
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
        currentMap = new BountyMap();

        currentMap.key = mapSchema.key;
        currentMap.name = mapSchema.name;
        currentMap.totalNode = Mathf.RoundToInt(mapSchema.totalNode);
        currentMap.nodes = new List<MapNode>();

        for(int i = 0; i < currentMap.totalNode; i++)
        {
            MapNode node = new MapNode(mapSchema.nodes[i]);
            currentMap.nodes.Add(node);
        }
    }
}

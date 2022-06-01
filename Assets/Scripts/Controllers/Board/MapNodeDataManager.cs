using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNodeDataManager : MonoBehaviour
{
    public List<BountyMap_Short> mapList;
    public BountyMap currentMap;
    // Start is called before the first frame update
    public void Init()
    {
        mapList = new List<BountyMap_Short>();
        currentMap = new BountyMap();
    }

    /// <summary>
    /// Update Map list from the server Schema
    /// </summary>
    /// <param name="mapShortList"></param>
    public void UpdateMapList(MapShortListSchema mapShortList)
    {
        mapList = new List<BountyMap_Short>();
        for (int i = 0; i < mapShortList.maps.Count; i++)
        {
            mapList.Add(new BountyMap_Short(mapShortList.maps[i]));
        }
    }

    /// <summary>
    /// Update Map detail to the client cache
    /// </summary>
    /// <param name="mapSchema"></param>
    public void UpdateMap(MapSchema mapSchema)
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

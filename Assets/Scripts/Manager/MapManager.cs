using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    private JsonMapData _mapData;
    public List<JsonMapData> JsonMap = new List<JsonMapData>();

    public void Init()
    {
        JsonMap.Add(Define.MapData.Mountain);
        JsonMap.Add(Define.MapData.RaceTrack);
        JsonMap.Add(Define.MapData.Near_Geometry);
        MapDecision(0);
    }
    public void MapDecision(int count)
    {
        _mapData = JsonMap[count];
    }

    public JsonMapData RetunrMap()
    {
        return _mapData;
    }
}


using UnityEngine;

public class MapController : MonoBehaviour
{
    Transform _map;
    Transform _startPoint;
    private void Awake()
    {
        string path = string.Concat(Define.Path.Map, Manager.Map.RetunrMap().MapName);
        GameObject go = Instantiate(Resources.Load<GameObject>(path));
        _map = go.transform;
        Init();

    }
    void Init()
    {
        _startPoint = GameObject.FindWithTag("StartPoint").transform;
        Vector3 vector3 = _map.Find("StartPoint").TransformPoint(Vector3.zero);
        _startPoint.position = Manager.Map.RetunrMap().Position;
    }


}

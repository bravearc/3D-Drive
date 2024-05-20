using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject _car;
    List<JsonCarData> jsonDatas = new List<JsonCarData>();
    public JsonCarData json;
    public Transform Model;

    public int Speed;
    public string CarName;

    private int _carCount = 3;

    private void Awake()
    {
        JsonSetting();
    }
    public void Init()
    {
        JsonSetting();
        CarSetting();
    }

    IEnumerator StartInit()
    {
        while (Model.name == "BackGround")
        {
            Model = GameObject.Find("BackGround").transform.Find("Model");
            yield return null; 
        }
        CarSetting();
    }

    private void JsonSetting()
    {
        jsonDatas.Add(Define.CarData.Ambulance);
        jsonDatas.Add(Define.CarData.SchoolBus);
        jsonDatas.Add(Define.CarData.Truck);
        jsonDatas.Add(Define.CarData.TouristBus);
        jsonDatas.Add(Define.CarData.G7_1200);
    }

    public void CarChange(int i)
    {

        _carCount += i;

        if(_carCount < 0)
        {
            _carCount = jsonDatas.Count - 1;
        }

        else if(_carCount > jsonDatas.Count - 1) 
        {
            _carCount = 0;
        }
        CarSetting();
    }
    public void CarSetting()
    {
        Model = GameObject.Find("BackGround").transform.Find("Model");
        Destroy(_car);
        json = jsonDatas[_carCount];
        CarName = json.CarName;
        GameObject newCar = Resources.Load<GameObject>(string.Concat(Define.Path.Car, json.CarName));
        _car = Instantiate(newCar, Model);
    }
}


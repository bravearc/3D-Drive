using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    GameObject _car;
    private void Start()
    {
        CarPosition(Manager.Car.CarName);
    }
    public void CarPosition(string carname)
    {
        if(_car == null)
        {
            string path = string.Concat(Define.Path.RaceCar, carname);
            _car = Resources.Load<GameObject>(path);

            Instantiate(_car, transform);
        }

        else
        {
            _car.transform.position = transform.localPosition;
        }
    }
}

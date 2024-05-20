using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    public static MainMenu Menu;
    public static SoundManager Sound;
    public static CarManager Car;
    public static MapManager Map;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        PlayMode();


        GameObject go;        
        
        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Sound = go.AddComponent<SoundManager>();

        go = new GameObject(nameof(CarManager));
        go.transform.parent = transform;
        Car = go.AddComponent<CarManager>();

        go = new GameObject(nameof(MapManager));
        go.transform.parent = transform;
        Map = go.AddComponent<MapManager>();


        //Menu.Init();
        Sound.Init();
        Map.Init();
        Car.Init();
    }

    public void PlayMode()
    {
        Menu = GameObject.FindWithTag("UI").GetComponent<MainMenu>();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class MainMenu : MonoBehaviour
{
    private Transform MenuBttons;

    private Button _gameStart;
    private Button _carButton;
    private Button _mapButton;
    private Button _settingButton;
    private Button _exitButton;

    private GameObject _car;
    private GameObject _map;
    private GameObject _setting;    

    bool _isSetting;

    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        MenuBttons = transform.Find("MenuButtons");
        _setting = transform.Find("Setting").gameObject;
        _car = transform.Find("CarChoice").gameObject;
        _map = transform.Find("MapChoice").gameObject;



        _gameStart = MenuBttons.transform.Find("GameStart").GetComponent<Button>();
        _carButton = MenuBttons.transform.Find("CarChoice").GetComponent<Button>();
        _mapButton = MenuBttons.transform.Find("MapChoice").GetComponent<Button>();
        _settingButton = MenuBttons.transform.Find("Settings").GetComponent<Button>();
        _exitButton = MenuBttons.transform.Find("Exit").GetComponent<Button>();

        var clickButton = Observable.Merge(
            _gameStart.OnClickAsObservable().Select(_ => _gameStart),
            _carButton.OnClickAsObservable().Select(_ => _carButton),
            _mapButton.OnClickAsObservable().Select(_ => _mapButton),
            _settingButton.OnClickAsObservable().Select(_ => _settingButton),
            _exitButton.OnClickAsObservable().Select(_ => _exitButton));

        clickButton.Subscribe(button => ClickChack(button)).AddTo(this);

        _setting.SetActive(false);
        _car.SetActive(false);
        _map.SetActive(false);

    }

    private void Start()
    {
        Manager.Car.CarSetting();
    }

    void ClickChack(Button bt)
    {
        MenuClose();
        switch (bt.name)
        {
            case "GameStart":
                SceneManager.LoadScene("PlayScene");
                break;
            case "CarChoice":
                _car.SetActive(true);
                break;
            case "MapChoice":
                _map.SetActive(true);
                break;
            case "Settings":
                _setting.SetActive(true);
                break;
            case "Exit":
                Exit();
                break;
            default: 
                break;
        }
    }

    void MenuClose()
    {
        for (int i = 1; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);

        }
    }
    void Exit()
    {
#if UNITY_EDITOR
        Time.timeScale = 0f;
#else
        Application.Quit();
#endif        
    }
}

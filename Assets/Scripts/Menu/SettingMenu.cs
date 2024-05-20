using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingMenu : MenuAbstract
{
    Transform _buttons;

    Slider _BGMSlider;
    Slider _engineSlider;

    Button _BGMButton;
    Button _engineButton;
    Button _miniMapButton;
    Button _gearButton;
    Button _exitButton;

    bool isBGM = true;
    bool isEngine = true;
    private void Awake()
    {
        _buttons = transform.Find("Buttons");
        _BGMSlider = transform.Find("Sound_BGM_Size").GetComponent<Slider>();
        _engineSlider = transform.Find("Sound_Engine_Size").GetComponent<Slider>();

        _BGMButton = _buttons.transform.Find("Sound_BGM").GetComponent<Button>();
        _engineButton = _buttons.transform.Find("Sound_Engine").GetComponent<Button>();
        _miniMapButton = _buttons.transform.Find("MiniMap").GetComponent<Button>();
        _gearButton = _buttons.transform.Find("Gear").GetComponent<Button>();
        _exitButton = _buttons.transform.Find("Exit").GetComponent<Button>();

        Init();
    }

    void Init()
    {
        var clickButton = Observable.Merge(
            _BGMButton.OnClickAsObservable().Select(_ => _BGMButton),
            _engineButton.OnClickAsObservable().Select(_ => _engineButton),
            _miniMapButton.OnClickAsObservable().Select(_ => _miniMapButton),
            _gearButton.OnClickAsObservable().Select(_ => _gearButton),
            _exitButton.OnClickAsObservable().Select(_ => _exitButton)
            );

        clickButton.Subscribe(button => ClickButton(button)).AddTo(this);
    }

    protected override void ClickButton(Button bt)
    {
        switch (bt.name)
        {
            case "Sound_BGM":
                Manager.Sound.BGMVolume(isBGM);
                isBGM = !isBGM;
                break;
            case "Sound_Engine":
                Manager.Sound.EngineVolume(isEngine);
                isEngine = !isEngine;
                break;
            case "MiniMap":
                
                break;
            case "Gear":
                
                break;
            case "Exit":
                gameObject.SetActive(false);
                break;



        }
    }
}

using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CarChoice : MenuAbstract
{
    Button _leftButton;
    Button _rightButton;
    Button _choiceButton;

    private void Awake()
    {
        _leftButton = transform.Find("LeftButton").GetComponent<Button>();
        _rightButton = transform.Find("RightButton").GetComponent<Button>();
        _choiceButton = transform.Find("Choice").GetComponent<Button>();

        Init();
    }

    void Init()
    {
        var buttonClick = Observable.Merge(
            _leftButton.OnClickAsObservable().Select(_ => _leftButton),
            _rightButton.OnClickAsObservable().Select(_ => _rightButton),
            _choiceButton.OnClickAsObservable().Select(_ => _choiceButton)
            );

        buttonClick.Subscribe(_ => ClickButton(_));
    }


    protected override void ClickButton(Button bt)
    {
        switch (bt.name) 
        {
            case "LeftButton":
                Manager.Car.CarChange(-1);
                break;
            case "RightButton":
                Manager.Car.CarChange(1);
                break;
            case "Choice":
                Manager.Car.CarName = Manager.Car.json.CarName;
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}

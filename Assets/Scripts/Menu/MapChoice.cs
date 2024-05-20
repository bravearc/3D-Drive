using UnityEngine.UI;
using UnityEngine;
using UniRx;
using System.Collections.Generic;

public class MapChoice : MenuAbstract
{
    Button _leftButton;
    Button _rightButton;
    Button _choiceButton;

    private int _mapCount = 0;
    public Image _mapImage;

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

        buttonClick.Subscribe(_ => ClickButton(_)).AddTo(this);
        _mapImage = transform.Find("MapImage").GetComponent<Image>();
    }

    private void Start()
    {
        MapSetting(_mapCount);
        
    }

    public void MapSetting(int i)
    {
        _mapCount = i;
        Manager.Map.MapDecision(_mapCount);
        string path = Define.Path.Sprite + Manager.Map.RetunrMap().MapName;
        Sprite newSprite = Resources.Load<Sprite>(path);
        _mapImage.sprite = newSprite;
    }
    protected override void ClickButton(Button bt)
    {
        switch (bt.name)
        {
            case "LeftButton":
                MapSetting(MapChange(-1));
                break;
            case "RightButton":
                MapSetting(MapChange(1));
                break;
            case "Choice":
                Manager.Map.MapDecision(_mapCount);
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    private int MapChange(int i)
    {
        int _count = Manager.Map.JsonMap.Count - 1;
        _mapCount += i;
        if(_mapCount < 0)
        {
            _mapCount = _count;
        }
        else if (_mapCount > _count)
        {
            _mapCount = 0;
        }
        return _mapCount;
    }
}

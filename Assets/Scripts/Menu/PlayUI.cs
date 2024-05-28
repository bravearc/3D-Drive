using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayUI : MonoBehaviour
{
    GameObject[] _gears = new GameObject[7];
    GameObject _startPoint;
    CarController _carController;

    Button _setting;
    Button _reset;
    Button _engine;
    Button _belt;
    Button _camera;
    Button _sun;

    Button _leftturnBT;
    Button _rightturnBT;
    Button _dangerBT;
    Button _foglightBT;
    Button _highBeamBT;
    Button _sideBreakBT;
    Button _wiferBT;

    public Animator _animator;

    Light _light;

    Transform _lights;
    GameObject _leftturn;
    GameObject _rightturn;
    GameObject _foglight;
    GameObject _highBeam;

    public bool isEngine;
    public bool isBelt;
    bool isSideBreak;
    bool isSun;
    bool isLeftturn;
    bool isRightturn;
    bool isDanger;
    bool isHighBeam;
    bool isFogLight;
    bool isWifer;

    private void Awake()
    {
        _startPoint = GameObject.FindWithTag("StartPoint");
        StartCoroutine(StartInit());
        _carController = _startPoint.GetComponent<CarController>();

    }
    private void Update()
    {
        LightKey();
    }
    IEnumerator StartInit()
    {
        while (_startPoint.transform.childCount < 1)
        {
            yield return null;
        }
        SetObject();
        SetButton();
        SetObservable();
    }
    void SetObject()
    {
        for (int i = 0; i < _gears.Length; ++i)
        {
            _gears[i] = transform.Find("Gear").GetChild(i).gameObject;
        }

        _light = GameObject.FindWithTag("Light").GetComponent<Light>();

        _lights = _startPoint.transform.GetChild(0).Find("Lights");
        _leftturn = _lights.Find("Left").gameObject;
        _rightturn = _lights.Find("Right").gameObject;
        _foglight = _lights.Find("FogLight").gameObject;
        _highBeam = _lights.Find("HighBeam").gameObject;
        _carController = _startPoint.transform.GetChild(0).GetComponent<CarController>();
        if(_startPoint.transform.GetChild(0).Find("Wifer") != null)
        {
            _animator = _startPoint.transform.GetChild(0).Find("Wifer").GetComponent<Animator>();   
        }

    }
    void SetButton()
    {
        Transform tr = transform.Find("Buttons");
        _setting = tr.Find("Setting").GetComponent<Button>();
        _reset = tr.Find("Reset").GetComponent<Button>();
        _engine = tr.Find("Engine").GetComponent<Button>();
        _belt = tr.Find("Belt").GetComponent<Button>();
        _camera = tr.Find("Camera").GetComponent<Button>();
        _sun = tr.Find("Sun").GetComponent<Button>();
        _wiferBT = tr.Find("Wifer").GetComponent<Button>();

        tr = transform.Find("Lights");
        _leftturnBT = tr.Find("LeftTurn").GetComponent<Button>();
        _rightturnBT = tr.Find("RightTurn").GetComponent<Button>();
        _dangerBT = tr.Find("Danger").GetComponent<Button>();
        _foglightBT = tr.Find("FogLight").GetComponent<Button>();
        _highBeamBT = tr.Find("HighBeam").GetComponent<Button>();
        _sideBreakBT = tr.Find("SideBreak").GetComponent<Button>();
    }

    void SetObservable()
    {
        var bt = Observable.Merge(
            _setting.OnClickAsObservable().Select(_ => _setting),
            _reset.OnClickAsObservable().Select(_ => _reset),
            _camera.OnClickAsObservable().Select(_ => _camera),
            _sun.OnClickAsObservable().Select(_ => _sun),
            _engine.OnClickAsObservable().Select(_ => _engine),
            _belt.OnClickAsObservable().Select(_ => _belt),
            _leftturnBT.OnClickAsObservable().Select(_ => _leftturnBT),
            _rightturnBT.OnClickAsObservable().Select(_ => _rightturnBT),
            _dangerBT.OnClickAsObservable().Select(_ => _dangerBT),
            _foglightBT.OnClickAsObservable().Select(_ => _foglightBT),
            _highBeamBT.OnClickAsObservable().Select(_ => _highBeamBT),
            _sideBreakBT.OnClickAsObservable().Select(_ => _sideBreakBT),
            _wiferBT.OnClickAsObservable().Select(_ => _wiferBT));

        bt.Subscribe(button => LightButton(button)).AddTo(this);

    }
    private void CarReset()
    {
        _startPoint.transform.GetChild(0).position =
            _startPoint.transform.localPosition;
        _startPoint.transform.GetChild(0).rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    void LightKey()
    {
        if(Input.GetKeyDown(KeyCode.A)) { ButtonCheak(ref isLeftturn, _leftturnBT); LightCheak(isLeftturn, _leftturn); }
        if(Input.GetKeyDown(KeyCode.S)) { ButtonCheak(ref isDanger, _dangerBT); LightCheak(isDanger); }
        if(Input.GetKeyDown(KeyCode.D)) { ButtonCheak(ref isRightturn, _rightturnBT); LightCheak(isRightturn, _rightturn); }
        if(Input.GetKeyDown(KeyCode.Z)) { ButtonCheak(ref isFogLight, _foglightBT); LightCheak(isFogLight, _foglight); }
        if(Input.GetKeyDown(KeyCode.X)) { ButtonCheak(ref isHighBeam, _highBeamBT); LightCheak(isHighBeam, _highBeam); }
        if(Input.GetKeyDown(KeyCode.C)) { ButtonCheak(ref isWifer, _wiferBT); WiferStart(isWifer); }
        if(Input.GetKeyDown(KeyCode.Q)) { ButtonCheak(ref isEngine, _engine); Manager.Sound.StartTheEngine(); }
        if(Input.GetKeyDown(KeyCode.W)) { ButtonCheak(ref isBelt, _belt); }
        if(Input.GetKeyDown(KeyCode.E)) { }
        if(Input.GetKeyDown(KeyCode.Escape)) { }
        if(Input.GetKeyDown(KeyCode.Escape)) { }
        if(Input.GetKeyDown(KeyCode.Escape)) { }
        if(Input.GetKeyDown(KeyCode.Escape)) { }
    }
    void LightButton(Button bt)
    {
        GameObject go = gameObject;
        switch (bt.name)
        {
            case "LeftTurn":
                ButtonCheak(ref isLeftturn, _leftturnBT);
                LightCheak(isLeftturn, _leftturn);
                break;
            case "RightTurn":
                ButtonCheak(ref isRightturn, _rightturnBT);
                LightCheak(isRightturn, _rightturn);
                break;
            case "Danger":
                ButtonCheak(ref isDanger, _dangerBT);
                LightCheak(isDanger);
                break;
            case "FogLight":
                ButtonCheak(ref isFogLight, _foglightBT);
                LightCheak(isFogLight, _foglight);
                break;
            case "HighBeam":
                ButtonCheak(ref isHighBeam, _highBeamBT);
                LightCheak(isHighBeam,_highBeam);
                break;
            case "Setting":
                Manager.Sound.StopSound();
                SceneManager.LoadScene(0);
                Manager.Instance.PlayMode();
                break;
            case "Reset":
                CarReset();
                break;
            case "Engine":
                ButtonCheak(ref isEngine, _engine);
                Manager.Sound.StartTheEngine();
                break;
            case "Belt":
                ButtonCheak(ref isBelt, _belt);
                break;
            case "Camera":
                //carController에 메서드 만들기 카메라 포지션 변경;
                break;
            case "Sun":
                ButtonCheak(ref isSun, _sun);
                SunSwitch();
                break;
            case "SideBreak":
                ButtonCheak(ref isSideBreak, _sideBreakBT);
                SideBreakChack();
                break;
            case "Wifer":
                ButtonCheak(ref isWifer, _wiferBT); 
                _animator.SetBool("Switch", isWifer);
                break;
            default:
                throw new ArgumentException("Invalid button name");
        }
    }

    void WiferStart(bool boo)
    {
        if(_animator != null)
        {
        _animator.SetBool("Switch", boo);
        }
    }

    void LightCheak(bool boo, GameObject go = null)
    {
        if (go != null)
        {
            go.SetActive(boo ? true : false);
            return;
        }

        if(isDanger)
        {
            _rightturn.SetActive(true);
            _leftturn.SetActive(true);
        }
        else
        {
            _rightturn.SetActive(isRightturn);
            _leftturn.SetActive(isLeftturn);
        }

    }

    void ButtonCheak(ref bool boo, Button bt = null)
    {
        boo = !boo;
        bt.GetComponent<Image>().color = boo ? Color.green : Color.white;
    }
    void SunSwitch()
    {
        _light.color = isSun ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1);
    }

    void SideBreakChack()
    {
        _carController._sideBreak = isSideBreak ? 0 : 1;
    }

    public void MoveGaer(int i)
    {
        for (int j = 0; j < _gears.Length; ++j)
        {
            _gears[j].SetActive(false);
        }
        _gears[i + 1].SetActive(true);
    }
    public bool RetuneEngine()
    {
        return isEngine;
    }

    public bool ReturnBelt()
    {
        return isBelt;
    }
}

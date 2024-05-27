using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleinfos;
    Rigidbody _carRigid;
    public float maxMotorTorque; 
    float maxBrakeTorque; 
    float maxSteeringAngle;
    public float steering;
    public float _carSpeed;

    float _power;
    public float MAX_SPEED;
    public float _rpm;
    int _gear;

    public float _sideBreak = 1;

    private PlayUI _playUI;

    private void Start()
    {
        _playUI = GameObject.FindWithTag("UI").GetComponent<PlayUI>();
        StartCoroutine(StartInit());
    }

    IEnumerator StartInit()
    {
        while (transform.childCount < 1)
        {
            yield return null; 
        }

        AxleInfo axleinfo = new AxleInfo();
        axleinfo.steering = true;
        axleinfo.motor = true;
        axleinfo.leftWheel = transform.GetChild(0).Find("Wheel").Find("LeftWheel").GetComponent<WheelCollider>();
        axleinfo.rightWheel = transform.GetChild(0).Find("Wheel").Find("RightWheel").GetComponent<WheelCollider>();
        _carRigid = transform.GetChild(0).GetComponent<Rigidbody>();
        axleinfos.Add(axleinfo);
        Init();
    }

    void Init()
    {
        maxBrakeTorque = 2000f;
        maxSteeringAngle = 40f;
        _power = 50;
    }

    public void Update()
    {
        if (_playUI.isEngine && _playUI.isBelt)
        {
            GearCount();
        }
    }

    public void FixedUpdate()
    {
        _carSpeed = _carRigid.velocity.magnitude;
        CarMove();
    }

    private void CarMove()
    {
        float motor = 0f;
        if (_playUI.isEngine && _playUI.isBelt)
        {
            motor = maxMotorTorque * Input.GetAxisRaw("Accelerator") * _power * _sideBreak;
            if(_rpm > MAX_SPEED)
            {
                motor = 0f;
            }
        }
        float brake = maxBrakeTorque * Input.GetAxisRaw("Brake") * _power;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            steering = maxSteeringAngle * Input.GetAxisRaw("Horizontal") / 3f;
        }
        else
        {
            steering = Mathf.Lerp(steering, 0f, Time.fixedDeltaTime * 100f) ;
        }
        foreach (AxleInfo axle in axleinfos)
        {
            if (axle.steering)
            {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
                _rpm = axle.leftWheel.rpm;
            }

            if (axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
                axle.leftWheel.brakeTorque = brake;
                axle.rightWheel.brakeTorque = brake;
            }
            ApplyLocalPositionToVisuals(axle.leftWheel);
            ApplyLocalPositionToVisuals(axle.rightWheel);
        }
    }
    void GearCount()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) )
        {
            if(_rpm > MAX_SPEED * 0.8f || _gear < 1)
            {
                TorqueChack(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TorqueChack(-1);
        }

    }

    int _minGaer = -1;
    int _maxGaer = 5;
    private void TorqueChack(int i)
    {
        _gear += i;
        _gear = _gear < _minGaer ? _minGaer : _gear;
        _gear = _gear > _maxGaer ? _maxGaer : _gear;

        maxMotorTorque = _gear switch {
            5 => 10,
            4 => 8,
            3 => 6,
            2 => 4,
            1 => 2,
            0 => 0,
            -1 => -2,
            _ => throw new System.NotImplementedException()};

        MAX_SPEED = _gear switch
        {
            5 => 250,
            4 => 180,
            3 => 100,
            2 => 60,
            1 => 30,
            0 => 0,
            -1 => -50,
            _ => throw new System.NotImplementedException()};

        _playUI.MoveGaer(_gear);
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        
        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion retation;
        collider.GetWorldPose(out position, out retation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = retation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}
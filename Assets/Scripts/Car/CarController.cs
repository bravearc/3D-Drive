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
            if(_carSpeed > MAX_SPEED)
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
            if(_carSpeed > MAX_SPEED * 0.7f || _gear < 1)
            {
                ReturnTorque(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ReturnTorque(-1);
        }

    }

    private void ReturnTorque(int i)
    {
        _gear += i;
        _gear = _gear < -1 ? -1 : _gear;
        _gear = _gear > 5 ? 5 : _gear;

        maxMotorTorque = _gear switch {
            5 => 5,
            4 => 5,
            3 => 4,
            2 => 3,
            1 => 2,
            0 => 0,
            -1 => -1,
            _ => throw new System.NotImplementedException()};

        MAX_SPEED = maxMotorTorque switch
        {
            5 => 11,
            4 => 9,
            3 => 7,
            2 => 5,
            1 => 3,
            0 => 3,
            -1 => 5,
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
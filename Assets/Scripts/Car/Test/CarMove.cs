using UnityEngine;

public class CarMove : MonoBehaviour
{
    Rigidbody _carRigid;
    public float _carSpeed;

    float _accelSpeed;
    float MAX_SPEED;
    float _handleangle;
    public int _gear;
    public bool _isbreak;

    private float _handleSpeed = 5f;
    private float _handReturnSpeed = 50f;
    private void Awake()
    {
        _carRigid = GetComponent<Rigidbody>();  
    }
    private void Update()
    {
        GearController();
        BreakControll();

    }

    private void FixedUpdate()
    {
        _carSpeed = _carRigid.velocity.magnitude;
        Debug.Log(_carSpeed);
        MoveControll();
        HandleControll();
    }

    private void HandleControll()
    {
        float _handle = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _handleangle -= Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.RightArrow)) 
        {
            _handleangle += Time.deltaTime;
        }
        else
        {
            _handleangle = Mathf.Lerp(_handleangle, 0, Time.deltaTime * _handReturnSpeed);
        }
    }
    private void BreakControll()
    { 
        if(Input.GetKey(KeyCode.LeftControl)) 
        {
            _accelSpeed -= Time.deltaTime * _handleSpeed;
            _isbreak = true; 
        }
        else
        {
            _isbreak= false;
        }

        if (_isbreak && _carSpeed <= 1f)
        {
            _carRigid.MovePosition(_carRigid.position);
        }
    }

    private void MoveControll()
    { 

        float _accel = Input.GetAxis("Accelerator");
        if (Input.GetKey(KeyCode.Space))
        {
            _accelSpeed += _accel * Time.fixedDeltaTime;
        }
        else
        {
            _accelSpeed = Mathf.Lerp(_carSpeed, 0, Time.fixedDeltaTime * 0.1f);
        }


        if(_accelSpeed >= MAX_SPEED) 
        {
            _accelSpeed = MAX_SPEED;
        }

        Vector3 movement = transform.forward * _accelSpeed;
        _carRigid.MovePosition(_carRigid.position + movement);

        if (_carSpeed != 0f)
        {
            float rotation = _handleangle * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
            _carRigid.MoveRotation(_carRigid.rotation * deltaRotation);
        }
    }

    private void GearController()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            ++_gear;
            if(_gear > 5)
            {
                _gear = 5;
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            --_gear;
            if (_gear < -1) 
            {
                _gear = -1;
                return; 
            }
        }

        MAX_SPEED = _gear switch
        {
            -1 => -5f,
            0 => 0f,
            1 => 5f,
            2 => 10f,
            3 => 15f,
            4 => 20f,
            5 => 25f
        };
    }
}

public enum GearCount
{
    None,
    One,
    Two,
    Three,
    Fourth,
    Five
}

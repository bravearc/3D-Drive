using Unity.VisualScripting;
using UnityEngine;

public class Car_Truck : MonoBehaviour
{
    public GameObject _trailer;
    public HingeJoint _joint;
    Rigidbody _body;


    void Start()
    {
        _body = GetComponent<Rigidbody>();
        GameObject go = Resources.Load<GameObject>(Define.Path.Car + "Trailer");
        _trailer = Instantiate(go, transform.parent);
        _trailer.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        _trailer.transform.localPosition = Vector3.zero;
        _joint = _trailer.AddComponent<HingeJoint>();
        _joint.connectedBody = _body;
        _joint.anchor = new Vector3(0, 0, 0);
        _joint.axis = new Vector3(0, 1, 0);
        _joint.useLimits = true;
        JointLimits _limits = _joint.limits;
        _limits.min = -60;
        _limits.max = 60;
        _joint.limits = _limits;
    }


}

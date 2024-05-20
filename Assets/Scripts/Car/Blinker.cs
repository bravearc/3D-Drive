using UnityEngine;

public class Blinker : MonoBehaviour
{
    public Light[] _light;
    float timer;

    private void OnEnable()
    {
        _light = new Light[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i) 
        {
            _light[i] = transform.GetChild(i).GetComponent<Light>();
        }
    }
    void Update()
    {
        timer += Time.deltaTime * 2;
        if(timer > 1f) 
        { 
            foreach(Light light in _light)
            {
                light.enabled = !light.enabled;
            }
            timer = 0;
        }

    }
}

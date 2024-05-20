using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private AudioSource _bgm;
    private AudioSource _drive;
    private AudioSource _engine;
    AudioListener listener;

    private float _BGMSize;
    private float _EngineSize;

    public void Init()
    {
        listener = gameObject.AddComponent<AudioListener>();
        //_bgm = gameObject.AddComponent<AudioSource>();
        //_drive = gameObject.AddComponent<AudioSource>();
        _engine = gameObject.AddComponent<AudioSource>();
    }

    public void StartTheEngine()
    {
        _engine.clip = ChangeSound("CarStart");
        _engine.Play();
        StartCoroutine(EngineChange());
    }
    public void StopSound()
    {
        _engine.Stop();
    }
    IEnumerator EngineChange()
    {
        while(_engine.isPlaying) 
        { 
            yield return null;
        }
        _engine.clip = ChangeSound(Manager.Car.json.EngineSound);
        _engine.loop = true;
        _engine.Play();

    }
    private AudioClip ChangeSound(string sound)
    {
        string path = string.Concat(Define.Path.Sound, sound);
        AudioClip newAudio = Resources.Load<AudioClip>(path);
        return newAudio;
    }

    public void BGMVolume(bool boo)
    {
        if(boo)
        {
            _bgm.volume = _BGMSize;
        }
        else
        {
            _bgm.volume = 0;
        }
    }

    public void EngineVolume(bool boo)
    {
        if(boo) 
        { 
            _engine.volume = _EngineSize;
        }
        else
        {
            _engine.volume = 0;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 1f;
    [Range(0.6f, 1.2f)]
    public float pitch = 1f;

    [Range(0f,0.4f)]
    public float volRand = 0.1f;
    [Range(0f, 0.4f)]
    public float pitRand = 0.1f;

    private AudioSource source;
    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volRand / 2f, volRand / 2f));
        source.pitch = pitch * (1 + Random.Range(-pitRand / 2f, pitRand / 2f));
        source.Play();
    }

}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("sound_" + 1 + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());

        }

        
    }

    public void PlaySound (string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("sound not found :(");
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _ambianceSource;
    
    private void Awake()
    {
        InitSingleton();
       
        DontDestroyOnLoad(this);
    }
    
    #region Singleton
    private void InitSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        } 
    }
    #endregion

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if(clip == null)
        {
            Debug.LogWarning("Tried to play null clip");
            return;
        }
        
        GameObject audioGO = new GameObject();
        audioGO.transform.parent = this.transform;
        audioGO.transform.position = position;
        
        AudioSource source = audioGO.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 1;
        
        source.Play();
        Destroy(audioGO, clip.length);
    }

    public void PlaySound2D(AudioClip clip)
    {
        if(clip == null)
        {
            Debug.LogWarning("Tried to play null clip");
            return;
        }
        
        GameObject audioGO = new GameObject();
        audioGO.transform.parent = this.transform;
        
        AudioSource source = audioGO.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 0;
        
        source.Play();
        Destroy(audioGO, clip.length);
    }

    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if(clip == null)
        {
            Debug.LogWarning("Tried to play null clip");
            return;
        }
        if(_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.loop = loop;
        
        _musicSource.Play();
    }
    
    public void PlayAmbiance(AudioClip clip, bool loop = true)
    {
        if(clip == null)
        {
            Debug.LogWarning("Tried to play null clip");
            return;
        }
        if(_ambianceSource.clip == clip) return;

        _ambianceSource.clip = clip;
        _ambianceSource.loop = loop;
        
        _ambianceSource.Play();
    }
}

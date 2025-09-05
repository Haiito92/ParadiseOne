using UnityEngine;

public class AudioOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _musicOnStart;
    [SerializeField] private AudioClip _ambianceOnStart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_musicOnStart != null)
        {
            AudioManager.Instance?.PlayMusic(_musicOnStart);
        }
        
        if (_ambianceOnStart != null)
        {
            AudioManager.Instance?.PlayAmbiance(_ambianceOnStart);
        }
    }

    
}

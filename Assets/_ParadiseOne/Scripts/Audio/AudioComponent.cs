using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    public void PlaySound(AudioClip clip)
    {
        AudioManager.Instance?.PlaySound(clip, transform.position);
    }

    public void PlaySound2D(AudioClip clip)
    {
        AudioManager.Instance?.PlaySound2D(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioManager.Instance?.PlayMusic(clip);
    }
    
    public void PlayAmbiance(AudioClip clip)
    {
        AudioManager.Instance?.PlayAmbiance(clip);
    }
}

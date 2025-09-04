using NaughtyAttributes;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;
    
    [Button]
    public void PlaySound2D()
    {
        AudioManager.Instance?.PlaySound2D(_audioClip);
    }

    [Button]
    public void PlaySound()
    {
        AudioManager.Instance?.PlaySound(_audioClip, transform.position);

    }

    [Button]
    public void PlayMusic()
    {
        AudioManager.Instance?.PlayMusic(_audioClip);

    }
    
    [Button]
    public void PlayAmbiance()
    {
        AudioManager.Instance?.PlayAmbiance(_audioClip);
    }
}

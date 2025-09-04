using UnityEngine;

[CreateAssetMenu(fileName = "SeaAudiosSO", menuName = "Scriptable Objects/SeaAudiosSO")]
public class SeaAudiosSO : ScriptableObject
{
    [field: Header("EVENT RANDOMIZER")]
    [field: SerializeField] public AudioClip NewEventSound { get; private set; }
    
    [field: Header("BOAT")]
    [field: SerializeField] public AudioClip BoatCollisionWithBoatSound { get; private set; }
    [field: SerializeField] public AudioClip BoatCollisionWithObstacleSound { get; private set; }
    
    [field: SerializeField] public AudioClip BoostSound { get; private set; }
}

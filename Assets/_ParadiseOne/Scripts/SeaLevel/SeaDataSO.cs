using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaDataSO", menuName = "Scriptable Objects/SeaDataSO")]
public class SeaDataSO : ScriptableObject
{
    [field: Header("GAME")]
    [field:SerializeField, Label("Sea Game Length (in seconds)"), Tooltip("in seconds")] public float SeaGameLength { get;
        private set;
    } = 30;

    [field: Header("FISH SPAWNER")]
    [field: SerializeField]
    public int MaxNumberOfFishes { get; private set; } = 10;

    [field: SerializeField, Tooltip("In seconds")] public float SpawningInterval { get; private set; } = 1.5f;

    [field: Header("FISHES")]
    [field: Header("Oursin")]
    [field: SerializeField, Tooltip("In seconds")]
    public float OursinLifeTime { get; private set; } = 5;
    [field: SerializeField] public int OursinScore { get; private set; } = 10;

    [field: Header("Vivano")]
    [field: SerializeField, Tooltip("In seconds")]
    public float VivanoLifeTime { get; private set; } = 5;

    [field: SerializeField] public int VivanoScore { get; private set; } = 10;
    [field: SerializeField] public float VivanoSpeed { get; private set; } = 50;
    [field: SerializeField, Tooltip("In seconds")] public float VivanoChangeDirectionInterval { get; private set; } = 1;


    [field: Header("BOAT")]
    [field: Header("Normal Boat Stats")]
    [field: SerializeField]
    public float BoatMaxSpeed { get; private set; } = 3;
    [field: SerializeField] public float BoatAcceleration { get; private set; } = 8;
    [field: SerializeField] public float BoatTurnSpeed { get; private set; } = 120;

    [field: Header("Boosted Boat Stats")]
    [field: SerializeField]
    public float BoostBoatMaxSpeed { get; private set; } = 6;

    [field: SerializeField] public float BoostBoatAcceleration { get; private set; } = 16;
    [field: SerializeField] public float BoostBoatTurnSpeed { get; private set; } = 240;

    [field: Header("Boost")]
    [field: SerializeField, Tooltip("In seconds")]
    public float MaxDurationBoost { get; private set; } = 2;

    [field: SerializeField, Tooltip("Unit/seconds")] public float RechargeRate { get; private set; } = 0.2f;
    [field: SerializeField, Tooltip("Units")] public float MinQuantityToBoost { get; private set; } = 0.5f;

}

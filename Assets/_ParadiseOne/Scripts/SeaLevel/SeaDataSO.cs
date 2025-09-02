using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaDataSO", menuName = "Scriptable Objects/SeaDataSO")]
public class SeaDataSO : ScriptableObject
{
    [SerializeField, Label("Sea Game Length (in seconds)"), Tooltip("in seconds")] public float SeaGameLength = 30;
}

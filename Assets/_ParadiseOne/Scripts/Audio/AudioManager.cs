using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
    
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
}

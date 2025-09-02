using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    
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

    #region SceneLoading

    public void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    #endregion
}

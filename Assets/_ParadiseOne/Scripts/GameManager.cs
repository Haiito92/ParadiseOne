using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _ParadiseOne.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private GameManager _instance;
        public GameManager Instance => _instance;

        
        
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
}

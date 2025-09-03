using System;
using UnityEngine;

public class SeaCanvas : MonoBehaviour
{
    [SerializeField] private SeaManager _seaManager;

    [SerializeField] private GameObject _seaStartGameMenu;
    [SerializeField] private GameObject _seaEndGameMenu;

    private void Start()
    {
        _seaManager.SeaGameStarted += OnSeaGameStarted;
        _seaManager.SeaGameEnded += OnSeaGameEnded;
    }

    private void OnSeaGameStarted()
    {
        _seaStartGameMenu.SetActive(false);
    }

    #region React To SeaManager Events

    private void OnSeaGameEnded()
    {
        _seaEndGameMenu.SetActive(true);
    }

    #endregion
    
}

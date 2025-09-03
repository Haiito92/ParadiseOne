using System;
using UnityEngine;

public class SeaCanvas : MonoBehaviour
{
    [SerializeField] private SeaManager _seaManager;

    [SerializeField] private GameObject _seaEndGameMenu;

    private void Start()
    {
        _seaManager.SeaGameEnded += OnSeaGameEnded;
    }

    #region React To SeaManager Events

    private void OnSeaGameEnded()
    {
        _seaEndGameMenu.SetActive(true);
    }

    #endregion
    
}

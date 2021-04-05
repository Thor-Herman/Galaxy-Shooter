﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isGameOver = false;
    [SerializeField]
    private bool _isCoopMode;
    private UIManager _uiManager;

    void Start() {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.LogError("UIManager is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver) RestartLevel();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void GameOver()
    {
        _isGameOver = true;
        _uiManager.OnGameOver();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool GetIsCoopMode() {return _isCoopMode;}
}

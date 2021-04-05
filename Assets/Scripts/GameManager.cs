using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool _isGameOver = false;
    [SerializeField]
    private bool _isCoopMode;
    private int _timesGameOverHasBeenCalled = 0;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_uiManager == null) Debug.LogError("UIManager is null");
        if (_spawnManager == null) Debug.LogError("Spawn manager is null");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver) RestartLevel();
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    public void GameOver()
    {
        if ((_isCoopMode && _timesGameOverHasBeenCalled == 1) || (!_isCoopMode))
        {
            _isGameOver = true;
            _uiManager.OnGameOver();
            _spawnManager.OnPlayerDeath();
        }
        _timesGameOverHasBeenCalled++;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool GetIsCoopMode() { return _isCoopMode; }
}

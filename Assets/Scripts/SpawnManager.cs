using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private float _minWaitTimePowerUp, _maxWaitTimePowerUp, _spawnRoutinesDelay = 3.0f;
    private float _enemyRespawnTime = 5.0f;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(IncreaseDifficultyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(_spawnRoutinesDelay);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = GetPosToSpawn();
            GameObject spawnedEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
            spawnedEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemyRespawnTime);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(_spawnRoutinesDelay);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = GetPosToSpawn();
            int powerUpId = Random.Range(0, _powerUps.Length);
            GameObject powerUpToSpawn = _powerUps[powerUpId];
            Instantiate(powerUpToSpawn, posToSpawn, Quaternion.identity);
            float waitTime = Random.Range(_minWaitTimePowerUp, _maxWaitTimePowerUp);
            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator IncreaseDifficultyRoutine() {
        yield return new WaitForSeconds(_spawnRoutinesDelay);
        while (_enemyRespawnTime == 0.5f && !_stopSpawning) {
            yield return new WaitForSeconds(7);
            _enemyRespawnTime -= 0.5f;
            if (_enemyRespawnTime < 0.5f) _enemyRespawnTime = 0.5f;
        }   
    }

    Vector3 GetPosToSpawn()
    {
        return new Vector3(Random.Range(-GameConstants.ENEMY_X_LIMIT, GameConstants.ENEMY_X_LIMIT), GameConstants.WINDOW_TOP_POS, 0);
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

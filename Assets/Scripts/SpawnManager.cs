using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private float _minWaitTimePowerUp;
    [SerializeField]
    private float _maxWaitTimePowerUp;
    private bool _stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = GetPosToSpawn();
            GameObject spawnedEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
            spawnedEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
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

    Vector3 GetPosToSpawn()
    {
        return new Vector3(Random.Range(-8.5f, 8.5f), 7, 0);
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

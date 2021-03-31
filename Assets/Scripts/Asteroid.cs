using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private const float _rotationSpeed = 15f;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    private void Start() {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("SpawnManager is null");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        if (_explosionPrefab == null) Debug.LogError("Explosion prefab is null");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Laser")) {
            Instantiate(_explosionPrefab, this.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}

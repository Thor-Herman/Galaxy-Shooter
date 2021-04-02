using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed = -8.0f;
    private Player _player;

    void Start() {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("Player is null");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);
        if (transform.position.y < -5f)
        {
            if (transform.parent) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_player != null) _player.DamagePlayer(1);
            Debug.Log("Damaged player");
            Destroy(gameObject);
        }
        Debug.Log(other.gameObject.tag);
    }
}

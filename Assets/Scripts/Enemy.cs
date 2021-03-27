using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        if (_player == null) Debug.LogError("Player is null");
        if (_animator == null) Debug.LogError("Player is null");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5f)
        {
            float newXPos = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(newXPos, 7, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_player != null) _player.DamagePlayer(1);
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            if (_player != null) _player.IncrementScore(10);
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Player _player;
    private Animator _animator;
    private bool _deathAnimPlaying = false;
    private const float _animationDuration = 2.633f;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        if (_player == null) Debug.LogError("Player is null");
        if (_animator == null) Debug.LogError("Animator is null");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        bool hasLeftGameFrame = transform.position.y < -5f;
        if (hasLeftGameFrame)
        {
            if (_deathAnimPlaying) Destroy(this.gameObject); // Finish animation early. 
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
            OnDeath();
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            if (_player != null) _player.IncrementScore(10);
            OnDeath();
            Destroy(other.gameObject);
        }
    }

    void OnDeath()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _animator.SetTrigger("OnEnemyDeath");
        _deathAnimPlaying = true;
        Destroy(this.gameObject, _animationDuration); 
    }
}

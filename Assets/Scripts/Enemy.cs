using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _laserOffset = -0.8f;
    private Player _player;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private bool _deathAnimPlaying = false;
    private const float _animationDuration = 2.633f;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null) Debug.LogError("Player is null");
        if (_animator == null) Debug.LogError("Animator is null");
        if (_boxCollider == null) Debug.LogError("BoxCollider is null");
        if (_audioClip == null) Debug.LogError("AudioSource is null");
        else _audioSource.clip = _audioClip;
        StartCoroutine(FireLaser());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        bool hasLeftGameFrame = transform.position.y < -5f;
        if (hasLeftGameFrame)
        {
            if (_deathAnimPlaying) return; // Don't respawn 
            float newXPos = Random.Range(-8.5f, 8.5f);
            transform.position = new Vector3(newXPos, 7, 0);
        }
    }

    IEnumerator FireLaser() {
        while (!_deathAnimPlaying) {
            float waitTime = Random.Range(0.5f, 7.0f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
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
        _animator.SetTrigger("OnEnemyDeath");
        _audioSource.Play();
        _boxCollider.enabled = false;
        _deathAnimPlaying = true;
        Destroy(this.gameObject, _animationDuration);
    }
}

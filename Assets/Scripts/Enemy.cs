using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _laserOffset = -0.8f;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _laserPrefab;
    private bool _deathAnimPlaying = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_animator == null) Debug.LogError("Animator is null");
        if (_boxCollider == null) Debug.LogError("BoxCollider is null");
        if (_audioClip == null) Debug.LogError("AudioSource is null");
        else _audioSource.clip = _audioClip;
        if (_uiManager == null) Debug.LogError("UIManager is null");
        StartCoroutine(FireLaser());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        bool hasLeftGameFrame = transform.position.y < GameConstants.WINDOW_BOTTOM_POS;
        if (hasLeftGameFrame)
        {
            if (_deathAnimPlaying) return; // Don't respawn 
            float newXPos = Random.Range(-GameConstants.ENEMY_X_LIMIT, GameConstants.ENEMY_X_LIMIT);
            transform.position = new Vector3(newXPos, GameConstants.WINDOW_TOP_POS, 0);
        }
    }

    IEnumerator FireLaser() {
        while (!_deathAnimPlaying) {
            float waitTime = Random.Range(0.5f, 7.0f);
            yield return new WaitForSeconds(waitTime);
            GameObject laser = Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
            laser.transform.parent = transform.parent;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.DamagePlayer(1);
            _animator.SetTrigger("OnEnemyDeath");
            OnDeath();
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            _uiManager.AddScore(10);
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
        Destroy(this.gameObject, GameConstants.ANIMATION_DURATION);
    }
}

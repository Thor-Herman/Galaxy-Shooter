using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    private float _laserOffset = 0.8f;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _nextFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private bool _rightEngineDamaged = false;
    private GameObject _rightEngineFire;
    private GameObject _leftEngineFire;
    [SerializeField]
    private GameObject _laserPrefab;
    private SpawnManager _spawnManager;
    private bool _tripleShotActive = false;
    private bool _shieldActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score;
    private UIManager _uiManager;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Current pos = Start pos
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _shieldVisualizer = transform.Find("PlayerShield").gameObject;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rightEngineFire = transform.Find("FireRight").gameObject;
        _leftEngineFire = transform.Find("FireLeft").gameObject;
        if (_spawnManager == null) Debug.LogError("Spawn manager is null");
        if (_shieldVisualizer == null) Debug.LogError("Shields are null");
        if (_uiManager == null) Debug.LogError("UIManager is null");
        if (_gameManager == null) Debug.LogError("GameManager is null");
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire) FireLaser();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        // transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);

        float xPos = transform.position.x;
        float yPos = transform.position.y;
        float zPos = transform.position.z;
        float newYPos = yPos;
        // ALTERNATIVT: newYPos = Mathf.Clamp(yPos, -3.8f, 0);

        if (yPos >= 0) newYPos = 0;
        else if (yPos <= -3.8f) newYPos = -3.8f;
        transform.position = new Vector3(xPos, newYPos, zPos);

        if (xPos > 11) transform.position = new Vector3(-11, newYPos, zPos);
        else if (xPos < -11) transform.position = new Vector3(11, newYPos, zPos);
    }

    void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        if (_tripleShotActive) Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        else Instantiate(_laserPrefab, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
    }

    public void DamagePlayer(int noOfLives)
    {
        if (!_shieldActive)
        {
            _lives -= noOfLives;
            _uiManager.UpdateLives(_lives);
            switch (_lives)
            {
                case 2:
                        int engine = Random.Range(0, 2);
                        _rightEngineDamaged = engine == 1;
                        if (_rightEngineDamaged) _rightEngineFire.SetActive(true);
                        else _leftEngineFire.SetActive(true);
                        break;
                case 1:
                        if (_rightEngineDamaged) _leftEngineFire.SetActive(true);
                        else _rightEngineFire.SetActive(true);
                        break;
                case 0:
                        Destroy(this.gameObject);
                        _spawnManager.OnPlayerDeath();
                        _gameManager.GameOver();
                        break;
                default: break;
            }
        }
        else
        {
            DeactivateShield();
        }
    }

    public void ActivateTripleShot()
    {
        this._tripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(5);
        this._tripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        StartCoroutine(DeactivateSpeedBoost(_speed));
        this._speed *= 2;
    }

    IEnumerator DeactivateSpeedBoost(float originalSpeed)
    {
        yield return new WaitForSeconds(5);
        this._speed = originalSpeed;
    }

    public void ActivateShield()
    {
        this._shieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    void DeactivateShield()
    {
        this._shieldActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void IncrementScore(int value)
    {
        this._score += value;
        _uiManager.UpdateScore(this._score);
    }
}

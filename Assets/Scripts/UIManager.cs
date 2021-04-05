using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score;
    private Text _scoreText;
    private Text _gameOverText;
    private Text _restartText;
    private Image _livesDisplayP1, _livesDisplayP2;
    [SerializeField]
    private Sprite[] _livesSprites = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<Text>();
        _livesDisplayP1 = transform.Find("LivesDisplayP1").GetComponent<Image>();
        CheckForCoop();
        _gameOverText = transform.Find("GameOverText").GetComponent<Text>();
        _restartText = transform.Find("RestartText").GetComponent<Text>();
        if (_scoreText == null) Debug.LogError("ScoreText is null");
        if (_livesDisplayP1 == null) Debug.LogError("LivesDisplay is null");
        if (_gameOverText == null) Debug.LogError("GameOverText is null");
        if (_restartText == null) Debug.LogError("RestartText is null");
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    void CheckForCoop() {
        GameManager _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null) Debug.LogError("GameManager is null");
        if (_gameManager.GetIsCoopMode())
        {
            _livesDisplayP2 = transform.Find("LivesDisplayP2").GetComponent<Image>();
            if (_livesDisplayP2 == null) Debug.LogError("Lives Display P2 is null");
        }
    }

    public void AddScore(int additionalScore)
    {
        _score += additionalScore;
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateLives(int lives, bool isPlayerOne = true)
    {
        if (!(0 <= lives && lives <= 3))
        {
            Debug.LogError("Lives must be between 0 and 3");
        }
        if (isPlayerOne) _livesDisplayP1.sprite = _livesSprites[lives];
        else _livesDisplayP2.sprite = _livesSprites[lives];
    }

    public void OnGameOver()
    {
        _restartText.gameObject.SetActive(true);
        StartCoroutine(FlickerGameOverText(0.5f));
    }

    IEnumerator FlickerGameOverText(float delay)
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(delay);
        }
    }
}

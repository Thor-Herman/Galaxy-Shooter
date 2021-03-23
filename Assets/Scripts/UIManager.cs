using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Text _scoreText;
    private Text _gameOverText;
    private Text _restartText;
    private Image _livesDisplay;
    [SerializeField]
    private Sprite[] _livesSprites = new Sprite[4];

    // Start is called before the first frame update
    void Start()
    {
        _scoreText = transform.Find("ScoreText").GetComponent<Text>();
        _livesDisplay = transform.Find("LivesDisplay").GetComponent<Image>();
        _gameOverText = transform.Find("GameOverText").GetComponent<Text>();
        _restartText = transform.Find("RestartText").GetComponent<Text>();
        if (_scoreText == null) Debug.LogError("ScoreText is null");
        if (_livesDisplay == null) Debug.LogError("LivesDisplay is null");
        if (_gameOverText == null) Debug.LogError("GameOverText is null");
        if (_restartText == null) Debug.LogError("RestartText is null");
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        if (!(0 <= lives && lives <= 3))
        {
            Debug.LogError("Lives must be between 0 and 3");
        }
        this._livesDisplay.sprite = _livesSprites[lives];
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

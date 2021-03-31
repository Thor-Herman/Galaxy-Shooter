using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpId; // 0 = TripleShot, 1 = Speed, 2 = Shields
    [SerializeField]
    private AudioClip _audioClip;

    void Start() {
        if (_audioClip == null) Debug.LogError("Audio Clip is null");
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            // HUSK NULL CHECK 
            if (player == null) Debug.LogError("Player is null");
            else
            {
                switch (_powerUpId)
                {
                    case 0: player.ActivateTripleShot(); break;
                    case 1: player.ActivateSpeedBoost(); break;
                    case 2: player.ActivateShield(); break;
                    default:
                        Debug.Log(_powerUpId);
                        break;
                }
            }
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
        }
    }
}

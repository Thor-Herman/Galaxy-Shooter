using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class EnemyLaser : Laser
{
    private Player _player;

    void Start() {
        _speed = -8.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player != null) player.DamagePlayer(1);
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    override protected bool ShouldDestroy() {
        return transform.position.y < GameConstants.WINDOW_BOTTOM_POS;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class Laser : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 8.0f;
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);
        if (ShouldDestroy())
        {
            if (transform.parent) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }

    virtual protected bool ShouldDestroy() {
        return transform.position.y > GameConstants.WINDOW_TOP_POS;
    }
}

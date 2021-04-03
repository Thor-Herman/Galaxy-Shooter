using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    protected float _speed = 8.0f;
    protected float _windowBorder = 8.0f;

    // Update is called once per frame
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
        return transform.position.y > _windowBorder;
    }
}

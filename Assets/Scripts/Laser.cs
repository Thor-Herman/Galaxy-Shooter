using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime, 0);
        if (transform.position.y > 8)
        {
            if (transform.parent) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}

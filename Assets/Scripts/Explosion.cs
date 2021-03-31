using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private const float _animationDuration = 2.633f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _animationDuration);
    }
}

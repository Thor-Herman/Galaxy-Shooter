using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameConstants;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, GameConstants.ANIMATION_DURATION);
    }
}

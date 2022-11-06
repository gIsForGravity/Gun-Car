using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var collided = other.gameObject;

        if (collided.layer == 9)
        {
            Laps laps;
            if (laps = collided.GetComponentInParent<Laps>())
                laps.checkIncr();
        }
    }
}

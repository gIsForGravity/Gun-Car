using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeroer : MonoBehaviour
{
    [SerializeField] private Vector3 displacement;
    [SerializeField] private Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = displacement;
        transform.localRotation = Quaternion.Euler(rotation);
    }
}

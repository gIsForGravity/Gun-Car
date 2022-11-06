using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCamera : NetworkBehaviour
{

    [SerializeField] float sensivity = 4.0f;    

    private const float YMin = -60.0f;
    private const float YMax = 70.0f;

    [Networked] private float currentX {get; set;} = 0.0f;
    [Networked] private float currentY {get; set;} = 0.0f;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (GetInput<PlayerInput>(out var input)) {
            currentX += input.xChange * sensivity;
            currentY -= input.yChange * sensivity;

            if (_camera) {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    _camera.fieldOfView = 40;
                    var pos = _camera.transform.localPosition;
                    pos.x = 0;
                    _camera.transform.localPosition = pos;
                }
                else
                {
                    _camera.fieldOfView = 70;
                    var pos = _camera.transform.localPosition;
                    pos.x = 1.5f;
                    _camera.transform.localPosition = pos;
                }
            }
        }

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.rotation = rotation;

    }
}

using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
        GetComponent<NetworkEvents>().OnInput.AddListener(OnInput);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        //Debug.Log("OnInput");
        PlayerInput data = new PlayerInput {
            Forward = Input.GetKey(key: KeyCode.W),
            Backward = Input.GetKey(key: KeyCode.S),
            Left = Input.GetKey(key: KeyCode.A),
            Right = Input.GetKey(key: KeyCode.D),
            Fire = Input.GetKey(key: KeyCode.Mouse0),
            ADS = Input.GetKey(key: KeyCode.Mouse1),
            xChange = Input.GetAxis("Mouse X"),
            yChange = Input.GetAxis("Mouse Y"),
        };

        input.Set(data);
    }
}

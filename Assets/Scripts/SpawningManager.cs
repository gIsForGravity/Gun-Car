using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 spawnLocation;
    [SerializeField] private Vector3 spawnRotation;
    private void Awake() {
        GetComponent<NetworkEvents>().PlayerJoined.AddListener(PlayerJoined);
    }

    private void PlayerJoined(NetworkRunner runner, PlayerRef player) {
        Debug.Log("PlayerJoined");
        NetworkObject gun = runner.Spawn(playerPrefab, inputAuthority: player);
        if (!runner.IsPlayer || player != runner.LocalPlayer) {
            foreach (var cam in gun.GetComponentsInChildren<Camera>()) {
                Debug.Log("cam");
                DestroyImmediate(cam.gameObject);
            }
            //Destroy(gun.GetComponent<GunCamera>());
        }
    }
}

using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[OrderBefore(typeof(NetworkTransformAnchor))]
public class Gun : NetworkBehaviour
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private float bulletVel;
    public int cooldown;
    public float sprayRange;

    [Networked] private int Cooldown { get; set; } = 0;
    [Networked] public NetworkObject Child { get; set; }

    public override void Spawned()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void FixedUpdateNetwork()
    {
        if ((object) Child != null && Child.transform.parent != transform)
            Child.transform.parent = transform;
        
        if (!Runner.IsServer)
            return;
        PlayerInput input;
        if (!GetInput<PlayerInput>(out input))
            return;
        
        if (input.ADS)
        {
            Shoot(Mathf.Clamp(sprayRange - 0.5f, 0, 100), input);
        }
        else
        {
            Shoot(sprayRange, input);
        }

        //GetComponent<NetworkTransformAnchor>().BeforeAllTicks
        /*var position = transform.position;

        if (input.Left)
            position.x -= 0.3f;
        if (input.Right)
            position.x += 0.3f;
        if (input.Forward)
            position.z += 0.3f;
        if (input.Backward)
            position.z -= 0.3f;

        transform.position = position;*/

        Cooldown--;
    }

    private void Shoot(float sprayRange, PlayerInput input)
    {
        if (Cooldown <= 0 && input.Fire)
        {
            Cooldown = cooldown;
            var bullet = Runner.Spawn(bulletObject, transform.position, transform.rotation * Quaternion.Euler(new Vector3(Random.Range(-sprayRange, sprayRange), Random.Range(-sprayRange, sprayRange), 0))).GetComponent<Bullet>();
            bullet.Velocity = bulletVel;
        }

        if (Input.GetKey(KeyCode.L))
        {
            GetComponent<SwitchGun>().Switch(Guns.Pistol);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            GetComponent<SwitchGun>().Switch(Guns.AK);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SwitchGun : NetworkBehaviour
{

    [SerializeField] private GameObject piss;
    [SerializeField] private GameObject akay;

    private Gun gun;
    private ChildSync childSync;

    private NetworkObject currentObject;


    private void Awake() {
        gun = GetComponent<Gun>();
        childSync = GetComponent<ChildSync>();
    }
    public override void Spawned()
    {
        var jawn = GetComponent<Gun>();

        jawn.cooldown = 30;
        jawn.sprayRange = 1;
        currentObject = Runner.Spawn(piss);

        if (currentObject) {
            currentObject.transform.parent = transform;
            currentObject.transform.localPosition = new Vector3();
            currentObject.transform.localEulerAngles = new Vector3();
        }
    }

    public void Switch(Guns gunType)
    {
        Runner.Despawn(currentObject);

        switch (gunType)
        {
            case Guns.Pistol:
                gun.cooldown = 30;
                gun.sprayRange = 1;
                currentObject = Runner.Spawn(piss);
                GetComponent<Gun>().Child = currentObject;
                currentObject.transform.parent = transform;
                currentObject.transform.localEulerAngles = new Vector3();
                currentObject.transform.localPosition = new Vector3();
                break;

            case Guns.AK:
                gun.cooldown = 10;
                gun.sprayRange = 3;
                currentObject = Runner.Spawn(akay);
                GetComponent<Gun>().Child = currentObject;
                currentObject.transform.parent = transform;
                currentObject.transform.localEulerAngles = new Vector3(-90, 180);
                currentObject.transform.localPosition = new Vector3(0, -0.5f, -0.6f);
                break;
        }
        
    }
}

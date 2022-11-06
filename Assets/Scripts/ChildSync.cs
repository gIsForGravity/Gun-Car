using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ChildSync : NetworkBehaviour
{
    private Transform _transform;
    [Networked] private NetworkObject Child {get; set;}


    public void SetChild(NetworkObject child) {
        Child = child;
        child.transform.parent = _transform;
    }

    private void Awake() {
        _transform = transform;
    }

    public override void Spawned()
    {
        NetworkObject[] networkObjects = GetComponentsInChildren<NetworkObject>();
        var thisObject = GetComponent<NetworkObject>();

        NetworkObject networkedChild = null;

        foreach (NetworkObject obj in networkObjects) {
            if (thisObject != obj) {
                networkedChild = obj;
                break;
            }
        }

        if (networkedChild)
            SetChild(networkedChild);
    }

    public override void FixedUpdateNetwork()
    {
        if (Child == null)
            return;
        
        var childTransform = Child.transform;

        if (childTransform.parent != _transform)
            childTransform.parent = _transform;
    }
}

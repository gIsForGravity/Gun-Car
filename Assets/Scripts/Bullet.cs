using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float Velocity { get; set; }
    public float Damage;

    [SerializeField] private float despawnTime;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        var vel = transform.forward * Velocity;
        _rigidbody.velocity = vel;
        StartCoroutine(DeleteBullet(despawnTime));
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

//    private void OnTri(Collision collision)
//    {
//        
//    }

    private void OnTriggerEnter(Collider other) {
        var collided = other.gameObject;
        
        if (collided.layer == 6)
        {
            var attackable = collided.GetComponentInParent<IAttackable>();
            attackable.Damage(10);
            //Destroy(gameObject);
            Runner.Despawn(GetComponent<NetworkObject>());
        }
    }

    private IEnumerator DeleteBullet(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}

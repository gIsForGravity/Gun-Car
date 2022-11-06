using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : SimulationBehaviour, IAttackable
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float deathWait;
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private GameObject damageParticle;

    private CarController _controller;

    public float Health { get; private set; }

    private void Start()
    {
        Reset();
    }

    // Returns remaining health
    public float Damage(float damage) {
        Debug.Log("damaged!");

        Health -= damage;

        var ouch = Instantiate(damageParticle);
        ouch.transform.parent = transform;
        ouch.transform.localPosition = new Vector3(0, -1, 2);
        ouch.transform.localEulerAngles = new Vector3();

        if (Health < 0)
            Health = 0;
        
        if (Health <= 0) {
            Reset();
            _controller.Dead = TickTimer.CreateFromSeconds(Runner, deathWait);
            return 0f;
        }
        
        return Health;
    }

    private void Reset() {
        Health = startingHealth;
        transform.position = spawnPosition;
    }

    private void Awake() {
        _controller = GetComponent<CarController>();
    }
}

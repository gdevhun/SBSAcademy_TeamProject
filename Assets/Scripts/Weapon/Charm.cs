using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charm : WeaponBase
{
    [SerializeField] private int damage;
    
    public override int Damage
    {
        get => damage;
        set => damage = value; 
    }

    protected override void Awake()
    {
        base.Awake();
        Damage = damage;
    }

    protected override void Update()
    {
        
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    protected override void AttackFeature()
    {
        
    }
}

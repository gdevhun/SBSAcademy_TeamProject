using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected int damage;
    protected Transform PlayerTrans;
    public abstract int Damage
    {
        get;
        set;
    }
    protected virtual void Awake()
    {
        PlayerTrans = GameManager.Instance.player.transform;
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.EnemyDamaged(Damage);
            }
        }
    }

    protected abstract void AttackFeature();
}

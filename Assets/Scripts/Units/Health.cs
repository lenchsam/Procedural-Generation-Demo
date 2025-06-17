using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] int _maxHealth;
    int _health;
    void Awake(){
        _health = _maxHealth;
    }
    public void TakeDamage(int damageToTake)
    {
        _health -= damageToTake;

        if(_health <= 0) Die();
    }
    void Die(){
        Destroy(gameObject);
    }
}

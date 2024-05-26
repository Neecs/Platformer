using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit;

    [SerializeField]
    private float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, _isAlive);
            Debug.Log("IsAlive set" + value);
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;


            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);

            if (CharacterEvents.characterDamaged != null)
            {
                CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            }
            else
            {
                Debug.LogWarning("El evento characterDamaged no ha sido inicializado.");
            }
            // Verificar si la salud es menor o igual a cero para llamar a OnDeath
            if (Health <= 0)
            {
                Knight knight = GetComponent<Knight>();
                if (knight != null)
                {
                    knight.OnDeath();
                }
            }

            return true;
        }

        return false;
    }
    public bool Heal(int healthRestored)
    {
        if (IsAlive && Health<MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHealt = Mathf.Min(maxHeal, healthRestored);

            Health += actualHealt;

            CharacterEvents.characterHealed(gameObject, actualHealt);
            return true;
        }
        return false;
    }
}

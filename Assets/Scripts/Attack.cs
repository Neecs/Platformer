using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 15;
    public Vector2 knockback = Vector2.zero;
    private bool isAttackBoosted = false;
    Animator animator;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool goHit = damageable.Hit(attackDamage, deliveredKnockback);

            if (goHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }
    public void UpAttack(float durationPower)
    {
        Debug.Log("ENTRO AL METODO UPATTACK ");

        if (!isAttackBoosted)
        {
            Debug.Log("ENTRO AL IF UP ATTACK ");

            StartCoroutine(AttackBoostCoroutine(durationPower));
            Debug.Log("SALIO DEL METODO");

        }
    }
    public IEnumerator AttackBoostCoroutine(float durationPower)
    {
        isAttackBoosted = true;
        attackDamage *= 2; // Aumentar el ataque
        Debug.Log("DEBERIA AUMENTAR EL DMG");
       

        yield return new WaitForSeconds(durationPower); // Esperar la duración del boost

        attackDamage /= 2; // Restaurar el ataque original
        isAttackBoosted = false;
    }
}

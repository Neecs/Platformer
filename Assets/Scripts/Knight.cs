using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    private LinearCongruential rng;
    public List<LootItem> lootTable; // Lista de power-ups que pueden ser dropeados


    public GameObject AppleHealth; // Asignar en el Inspector
    public GameObject BoarAttckDMG; // Asignar en el Inspector
    public GameObject BeerVelocity; // Asignar en el Inspector

    private void Start()
    {
        // Inicializar la tabla de loot con power-ups y sus probabilidades
        lootTable = new List<LootItem>
        {
            new LootItem { itemName = "HealthPowerUp", dropProbability = 0.5f, itemPrefab = AppleHealth },
            new LootItem { itemName = "DamageBoostPowerUp", dropProbability = 0.3f, itemPrefab = BoarAttckDMG },
            new LootItem { itemName = "SpeedBoostPowerUp", dropProbability = 0.2f, itemPrefab = BeerVelocity }
        };
    }


    public void DropLoot()
    {
        float totalProbability = 0f;

        // Calcular la suma total de las probabilidades
        foreach (LootItem item in lootTable)
        {
            totalProbability += item.dropProbability;
        }

        // Generar un número aleatorio propsio entre 0 y la suma total de las probabilidades

        rng = new LinearCongruential();
        float randomValue = rng.RandomNumber() * totalProbability;
       
        float cumulativeProbability = 0f;

        // Determinar qué power-up se dropea basado en la probabilidad acumulada
        foreach (LootItem item in lootTable)
        {
            cumulativeProbability += item.dropProbability;

            if (randomValue <= cumulativeProbability)
            {
                DropItem(item);
                break;
            }
        }
    }
    void DropItem(LootItem lootItem)
    {
        Debug.Log("Dropped item: " + lootItem.itemName);
        if (lootItem.itemPrefab != null)
        {
            Instantiate(lootItem.itemPrefab, transform.position, Quaternion.identity);
        }
    }
    public void OnDeath()
    {
        DropLoot();
        Destroy(gameObject); // Elimina al "Knight" del juego
    }

    public float walkSpeed = 3f;
    public float walkStopRate = 0.2f;
    public DetectionZone attackZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);


    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}

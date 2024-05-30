using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class FlyingEyeScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f;
    public DetectionZone biteDetectionZone;
    Animator animator;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDiretionVector = Vector2.left;
    private Vector2 flyDiretionVector = Vector2.up;


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
                    walkDiretionVector = Vector2.right;
                    flyDiretionVector = Vector2.down;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDiretionVector = Vector2.left;
                    flyDiretionVector = Vector2.up;
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
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall) 
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkDiretionVector.x * moveSpeed, rb.velocity.y);
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

    private Coroutine _randomWalkCoroutine;

    private void Start()
    {
        _randomWalkCoroutine = StartCoroutine(RandomWalk());
    }

    private IEnumerator RandomWalk()
    {

      MultiplicativeCongruence linearCongruential = new MultiplicativeCongruence();


        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            float randomValue = linearCongruential.RandomNumber();
            Vector2 newDirection = Vector2.zero;

            Debug.Log("Random walk direction: " + randomValue);
            if (randomValue < 0.5f)
            {
                newDirection = Vector2.left;
                WalkDirection = WalkableDirection.Left;
            }
            else if (randomValue >= 0.5f)
            {
                newDirection = Vector2.right;
                WalkDirection = WalkableDirection.Right;
            }

            walkDiretionVector = newDirection;
            rb.velocity = new Vector2(walkDiretionVector.x * moveSpeed, rb.velocity.y);
        }
    }

    private void OnDestroy()
    {
        if (_randomWalkCoroutine!= null)
        {
            StopCoroutine(_randomWalkCoroutine);
        }
    }
}
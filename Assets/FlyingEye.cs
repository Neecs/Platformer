using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class FlyingEyeScript : MonoBehaviour
{
    public DetectionZone biteDetectionZone;
    Animator animator;
    Rigidbody2D rb;

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
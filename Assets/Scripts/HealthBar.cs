using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;
    Damageable playerDmageable;
    // Start is called before the first frame update

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("No player found. Make sure tag id 'Player'");  
        }
        playerDmageable = player.GetComponent<Damageable>();
    }
    void Start()
    {   
        healthSlider.value = CalculateSliderPercentage(playerDmageable.Health,playerDmageable.MaxHealth);
        healthBarText.text="HP " + playerDmageable.Health +" / " + playerDmageable.MaxHealth;
    }
    private void OnEnable()
    {
        playerDmageable.healthChange.AddListener(OnHealthChange);
    }
    private void OnDisable()
    {
        playerDmageable.healthChange.RemoveListener(OnHealthChange);
    }
    

    private float CalculateSliderPercentage(float CurrentHealth,float maxHealth)
    {
        return CurrentHealth / maxHealth;
    }
    private void OnHealthChange(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

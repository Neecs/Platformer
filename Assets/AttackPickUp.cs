using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickUp : MonoBehaviour
{
    public float damageBoostDuration = 15f;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        Debug.Log("PASOOO Atack attack ");
        if (playerController)
        {
            Debug.Log("ENTRO AL IF ATTACK PICK UP ");
            playerController.UpAttack(damageBoostDuration);
            Debug.Log("SALIO METODO AL IF ATTACK PICK UP ");

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}

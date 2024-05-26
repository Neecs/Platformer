using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityPickUp : MonoBehaviour
{
    public float velocityDuration = 15f;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();
        if (playerController)
        {
            Debug.Log("ENTRO AL IF VELOCITY PICK UP ");
            playerController.UpSpeed(velocityDuration);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}

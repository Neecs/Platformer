using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceEnemy : MonoBehaviour
{

    public GameObject enemy;
    public GameObject enemy1;

    // Start is called before the first frame update
    void Start()
    {
        enemy.SetActive(true);
        enemy1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

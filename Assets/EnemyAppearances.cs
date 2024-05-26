using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppearances : MonoBehaviour
{

    public List<GameObject> enemies;
    private Coroutine enemyAppearanceCoroutine; // Referencia a la corrutina de aparición de enemigos
    MultiplicativeCongruence linearCongruential;

    // Start is called before the first frame update
    void Start()
    {
        linearCongruential = new MultiplicativeCongruence();
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        enemyAppearanceCoroutine = StartCoroutine(ManageEnemyAppearances());
    }

    
    IEnumerator ManageEnemyAppearances()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies.Count > 0)
            {

                float interArrivalTime = linearCongruential.RandomNumber() * 10;
                yield return new WaitForSeconds(interArrivalTime);
                Debug.Log(interArrivalTime);

                GameObject currentEnemy = enemies[(int)linearCongruential.RandomNumberRange(0, enemies.Count)];

                if (currentEnemy != null)
                {
                    currentEnemy.SetActive(true);

                    yield return new WaitForSeconds(linearCongruential.RandomNumber() * 10);
                }

            }
        }
    }

    // Método para detener la corrutina de aparición de enemigos
    public void StopEnemyAppearances()
    {
        if (enemies.Count == 0)
        {
            StopCoroutine(enemyAppearanceCoroutine);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

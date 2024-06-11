using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPickUpPrefab, healthPickUpPrefab;

    public void DropPickUp()
    {
        int random = Random.Range(1, 5);

        if (random == 1)
        {
            Instantiate(healthPickUpPrefab, transform.position, Quaternion.identity);
        }
        Instantiate(coinPickUpPrefab, transform.position, Quaternion.identity);
    }

    public void DropPickUp(bool isBoss)
    {
        if (isBoss)
        {
            int randomAmount = Random.Range(5, 10);
            for (int i = 0; i < randomAmount; i++)
            {
                Instantiate(coinPickUpPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

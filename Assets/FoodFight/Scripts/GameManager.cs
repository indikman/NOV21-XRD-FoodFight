using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Target targetPrefab;

    public GameObject[] FoodItems;


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnRandomFood(Vector3 position, Quaternion rotation)
    {
        Instantiate(FoodItems[Random.Range(0, FoodItems.Length)], position, rotation);
    }

    public void CreateTarget()
    {
        var target = Instantiate(targetPrefab);
        target.RandomMoveAmount(Random.Range(0, 10));
    }
}

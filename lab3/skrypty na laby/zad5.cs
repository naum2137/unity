using UnityEngine;
using System.Collections.Generic;

public class zad5 : MonoBehaviour
{
    public GameObject cubePrefab; // Prefab Cube'a
    public int cubeCount = 10; // Iloœæ generowanych Cube'ów
    public float planeSize = 10.0f; // Wielkoœæ p³aszczyzny (10x10)
    private List<Vector3> usedPositions = new List<Vector3>(); // Lista zajêtych pozycji

    void Start()
    {
        GenerateCubes();
    }

    void GenerateCubes()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(cubePrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        bool positionAvailable = false;

        do
        {
            // Generowanie losowej pozycji w zakresie p³aszczyzny
            float randomX = Random.Range(-planeSize / 2, planeSize / 2);
            float randomZ = Random.Range(-planeSize / 2, planeSize / 2);
            randomPosition = new Vector3(randomX, 0.5f, randomZ); // 0.5f, ¿eby Cube znajdowa³ siê nad p³aszczyzn¹

            // Sprawdzenie, czy pozycja nie jest ju¿ zajêta
            if (!usedPositions.Contains(randomPosition))
            {
                positionAvailable = true;
                usedPositions.Add(randomPosition); // Zapisanie pozycji jako zajêtej
            }

        } while (!positionAvailable); // Powtarzaj, dopóki nie znajdzie siê wolna pozycja

        return randomPosition;
    }
}

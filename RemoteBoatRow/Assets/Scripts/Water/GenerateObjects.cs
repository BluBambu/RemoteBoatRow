using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
	public GameObject islandPrefab;
    public GameObject boatPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(islandPrefab, new Vector3(Random.Range(-10.0f, 30.0f), 0, Random.Range(-10.0f, 30.0f)), Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)
        {
            Instantiate(boatPrefab, new Vector3(Random.Range(-10.0f, 30.0f), 0, Random.Range(-10.0f, 30.0f)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

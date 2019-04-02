using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public List<GameObject> boatParts;

    private int defaulty = 0;
    public GameObject excludeArea1;
    public GameObject excludeArea2;
    public GameObject spawnArea1;
    public GameObject spawnArea2;

    // Start is called before the first frame update
    private void Start()
    {
        PlaceBoatParts();
    }

    public void PlaceBoatParts()
    {
        Debug.Log("start spawning boat parts");
        var xRand = 0;
        var zRand = 0;

        var randomPos = new Vector3(0, 0, 0);
        for (var i = 0; i < boatParts.Count; i++)
        {
            rerandomize:
            xRand = Random.Range(165, 458);
            zRand = Random.Range(81, 379);
            if (xRand > 326 && xRand < 381 || zRand > 241 && zRand < 311) goto rerandomize;
            randomPos = new Vector3(xRand, 0, zRand);
            Instantiate(boatParts[i], randomPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
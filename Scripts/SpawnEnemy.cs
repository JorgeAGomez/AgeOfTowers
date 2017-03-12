using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject[] waypoints;      //allow you to specify how many waypoints on map
    public GameObject Enemies;          //allow you to assign enemy
    void Start()
    {
        //Instantiate enemies and assign it waypoints to follow
        Instantiate(Enemies).GetComponent<MoveEnemy>().waypoints = waypoints;
    }
}

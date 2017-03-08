using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{

    
    public int Target = 0;
    public Transform exitPoint;
    public Transform[] wayPoints;
    public float navigationUpdate;
    public float navigationMoveUpdate;

    private Transform enemy;
    private float navigationTime = 0;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoints != null)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (Target < wayPoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[Target].position, navigationTime);
                }
                else
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "checkpoint")
            Target += 1;
        else if (other.tag == "Finish")
        {
            GameManager.instance.removeEnemyFromScreen();
            Destroy(gameObject);
        }
    }
}
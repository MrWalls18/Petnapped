using System.Collections;
using System.Drawing;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float speed = 10f;
    private int patrol = 0;


    public float startWaitTime;
    private float waitTime;



    bool enemyTurned = false;

    private void Awake()
    {
        waitTime = startWaitTime;


    }

    private bool isKnocked = false;

    void Update()
    {
        if (!isKnocked && patrolPoints.Length > 0)
        {
            Movement();
        }
    }

    void Movement()
    {
        //Resets the patrol point when it reaches the length of the array
        if (patrol >= patrolPoints.Length)
        {
            patrol = 0;
        }

        //Turns to face the position it is walking towards
        this.transform.LookAt(patrolPoints[patrol].position);
        if (enemyTurned)
        {
            enemyTurned = false;
        }



        //Move Enemy towards next position
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[patrol].position, speed * Time.deltaTime);

        //Checks to see if the enemy has reached the patrol point
        if (Vector3.Distance(transform.position, patrolPoints[patrol].position) < 0.02f)
        {
            //Makes the enemy wait a few seconds before moving towards the next position
            if (waitTime <= 0)
            {
                patrol += 1;
                waitTime = startWaitTime;

                enemyTurned = true;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }



}
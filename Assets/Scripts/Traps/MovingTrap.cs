using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : Trap
{

    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float moveSpeed;
    private int movePointIdx;
    

    protected override void Start()
    {
        base.Start();
        transform.position = movePoints[0].position;
    }
    


    private void Update()
    {
        MoveController();
    }


    private void MoveController()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoints[movePointIdx].position, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePoints[movePointIdx].position) < 0.1f)
        {
            movePointIdx++;
            if (movePointIdx == movePoints.Length)
                movePointIdx = 0;
        }
    }

}

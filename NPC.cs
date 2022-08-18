using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform[] movePoints;
    private Transform target,pointA;
    [SerializeField] private Animator animator;
    private bool isMoving;
    [SerializeField] private float speed = 1;
    private int movePointIndex = 0;

    private bool isInteractable;
    
    void Start()
    {

        isMoving = true;
        animator.SetBool("IsRunning",isMoving);
        WalkToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        //WalkToTarget();
    }

    public void WalkToTarget()
    {
        if (movePointIndex == movePoints.Length)
        {
            animator.SetBool("IsRunning",false);
            isInteractable = true;
            return;
        }

        pointA = transform;
        target = movePoints[movePointIndex];
        movePointIndex += 1;
        float xDist = target.position.x - pointA.position.x;
        float yDist = target.position.y - pointA.position.y;
        animator.SetFloat("x",xDist);
        animator.SetFloat("y",yDist);
        float dist = Vector2.Distance(pointA.position, target.position);
        Debug.Log(dist/speed);
        transform.DOMove(target.position, dist/speed).OnComplete(WalkToTarget).SetEase(Ease.Linear);

    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawWireSphere(target.position,1);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(pointA.position,1);
    // }
}

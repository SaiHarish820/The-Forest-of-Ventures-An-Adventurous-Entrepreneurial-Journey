using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform pathHolder;

    private void Start()
    {
        Vector3[] wayPoints = new Vector3[pathHolder.childCount];

        for(int i = 0; i < wayPoints.Length; i++) {
            wayPoints[i] = pathHolder.GetChild(i).position;
        }
    }



    private void OnDrawGizmos()
    {

        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach(Transform wayPoints in pathHolder)
        {
            Gizmos.DrawSphere(wayPoints.position, .3f);
            Gizmos.DrawLine(previousPosition, wayPoints.position);
            previousPosition = wayPoints.position;
        }

        Gizmos.DrawLine(previousPosition, startPosition);
    }
}

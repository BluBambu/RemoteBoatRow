using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPointer : MonoBehaviour
{
    public float ArrowDistanceFromPlayer = 2;
    public float ArrowDistanceFromPlayerBobDelta = 0.5f;
    public float ArrowDistanceBobPeriod = 1;

    public Transform Root;
    public Transform Target;

    private void Update()
    {
        var posDeltaNormalized = (Target.position - Root.position).normalized;

        var arrowDistance = ArrowDistanceFromPlayer + 
            Mathf.Sin((Time.time) * ArrowDistanceBobPeriod) * ArrowDistanceFromPlayerBobDelta;
        transform.position = Root.position + posDeltaNormalized * arrowDistance;

        transform.LookAt(Target.position, Vector3.up);
    }
}

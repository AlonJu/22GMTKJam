using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceProjection : MonoBehaviour
{
    PlayerMovement playerMovement;
    public Transform winch;
    public LineRenderer lineRenderer;

    // Number of points on the line
    public int numPoints = 50;

    // distance between those points on the line
    public float timeBetweenPoints = 0.1f;
    public float gravFactor = 8f;

    // The physics layers that will cause the line to stop being drawn
    public LayerMask CollidableLayers;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();
        winch = GameObject.Find("Aiming Winch").GetComponent<Transform>();
    }

    public float angleOffset = -45.0f;
    void Update()
    {
        if (timeBetweenPoints < 0.05f){
            timeBetweenPoints = 0.05f;
        }
        if (playerMovement.currentDice !=null){
        lineRenderer.forceRenderingOff = false;
        lineRenderer.positionCount = (int)numPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = playerMovement.currentDice.transform.position;
        Vector3 startingVelocity = (Quaternion.AngleAxis(angleOffset, playerMovement.currentDice.transform.right) * playerMovement.currentDice.transform.forward) * playerMovement.throwSpeed;
        
            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y/gravFactor * t * t;
                points.Add(newPoint);

                if(Physics.OverlapSphere(newPoint, 2, CollidableLayers).Length > 0)
                {
                    lineRenderer.positionCount = points.Count;
                    break;
                }
            }
            lineRenderer.SetPositions(points.ToArray());
        } else{
            lineRenderer.forceRenderingOff = true;
        }
    }
}
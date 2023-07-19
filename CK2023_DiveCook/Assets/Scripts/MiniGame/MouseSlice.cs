using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MouseSlice : MonoBehaviour
{
    public float minimumCuttingSpeed = 1f;
    LineRenderer lineRenderer;
    List<Vector2> points;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(points == null)
            {
                points= new List<Vector2> { mousePosition, mousePosition };
                lineRenderer.SetPosition(0, points[0]);
                lineRenderer.SetPosition(1, points[1]);
            }
            else
            {
                points[1] = mousePosition;
                lineRenderer.SetPosition(1, points[1]);
            }
        }
        else if (points != null)
        {
            if(IsMouseMovingFastEnough())
            {
                Cut();
            }

            points = null;
        }
    }
    private bool IsMouseMovingFastEnough()
    {
        float mouseSpeed = (points[1] - points[0]).magnitude / Time.deltaTime;
        return mouseSpeed >= minimumCuttingSpeed;
    }

    private void Cut()
    {
        foreach (var cuttable in FindObjectsOfType<Cuttable>())
        {
            Vector2 objectPosition = cuttable.transform.position;
            if(IsUnderneathCut(objectPosition))
            {
                cuttable.Split();
            }
        }
    }

    bool IsUnderneathCut(Vector2 objectPosition)
    {
        float distanceFromStart = Vector2.Distance(objectPosition, points[0]);
        float distanceFromEnd = Vector2.Distance(objectPosition, points[1]);

        if (distanceFromStart + distanceFromEnd > (points[1] - points[0]).magnitude + 0.01f)
        {
            return false;
        }

        float dotProduct = Vector3.Dot(points[1] - points[0], objectPosition - points[0]);
        return dotProduct > 0;
    }

}

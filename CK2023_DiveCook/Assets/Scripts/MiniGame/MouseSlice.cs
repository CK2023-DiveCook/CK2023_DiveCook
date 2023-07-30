using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class MouseSlice : MonoBehaviour
{
    public float minimumCuttingSpeed = 1f;
    public GameObject specificObject;
    LineRenderer lineRenderer;
    List<Vector2> points;
    [SerializeField] Manager.MiniGameManager miniGameManager;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            if (points == null)
            {
                points = new List<Vector2> { mousePosition, mousePosition };
                lineRenderer.positionCount = 2;
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
            Collider2D[] colliders = Physics2D.OverlapAreaAll(points[0], points[1], LayerMask.GetMask("Default"));

            foreach (var collider in colliders)
            {
                if (collider.gameObject == specificObject)
                {
                    if (IsColliderFullyWithinLine(collider))
                    {
                        Cut();
                        specificObject.SetActive(false);
                    }
                }
            }
            lineRenderer.positionCount = 0;
            points = null;
        }
    }
    private void Cut()
    {
        foreach (var cuttable in FindObjectsOfType<Cuttable>())
        {
            Vector2 objectPosition = cuttable.transform.position;

            cuttable.Split();
            miniGameManager.Success();
        }
    }

    private bool IsColliderFullyWithinLine(Collider2D collider)
    {
        Bounds bounds = collider.bounds;
        bool isColliderFullyWithinLine = true;

        foreach (var point in new Vector3[] { bounds.min, bounds.max })
        {
            if (!lineRenderer.bounds.Contains(point))
            {
                isColliderFullyWithinLine = false;
                break;
            }
        }

        return isColliderFullyWithinLine;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineTest : MonoBehaviour
{
    RaycastHit hit;

    public LineRenderer lineRender;

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                lineRender.enabled = true;

                lineRender.SetPosition(0, transform.position);
//                lineRender.SetPosition(1, Vector3.down * hit.distance);
lineRender.SetPosition(1, ray.GetPoint(hit.distance));

            }
        }
    }
}

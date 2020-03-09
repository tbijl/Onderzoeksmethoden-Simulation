using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineDrawer : MonoBehaviour
{
    public float width;

    // Update is called once per frame
    void Update()
    {
        Vector2 pointA = new Vector2(-7.8f, -2.5f);
        Vector2 pointB = new Vector2(width, -2.5f);
        DottedLine.DottedLine.Instance.DrawDottedLine(pointA, pointB);

        Vector2 pointC = new Vector2(-7.8f, 0f);
        Vector2 pointD = new Vector2(width, 0f);
        DottedLine.DottedLine.Instance.DrawDottedLine(pointC, pointD);

        Vector2 pointE = new Vector2(-7.8f, 2.5f);
        Vector2 pointF = new Vector2(width, 2.5f);
        DottedLine.DottedLine.Instance.DrawDottedLine(pointE, pointF);
    }
}

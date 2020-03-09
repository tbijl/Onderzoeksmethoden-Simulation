using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DottedLine;

public class DataVisualizer : MonoBehaviour
{
    public DataParser parser;
    public Text messageText;
    public Text infoText;


    public LineRenderer lineRenderer;
    public LineRenderer axis;

    [SerializeField] DataTypes mode;
    [SerializeField] float SimplifyTolerance;

    public DottedLineDrawer dotDrawer;

    DataTypes previousMode;

    float minVal;
    float maxVal;
    float avg;

    double interval;
    float middle;
    float maxDiff;
    float heightPerVal;

    float xLen;

    List<float> listToVisualize;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mode != previousMode)
            Visualize();

        previousMode = mode;


        if (listToVisualize == null)
            messageText.text = "PARSING DATA...";
        else
            messageText.text = "";

    }

    public void Visualize()
    {
        listToVisualize = parser.ExtractValues(mode);
        interval = 16 / listToVisualize.Count;

        if (interval <= 0.03f)
            interval = 0.03f;

        xLen = listToVisualize.Count * (float)interval - 250 * (float)interval;

        dotDrawer.width = xLen;

        axis.SetPosition(2, new Vector3(xLen, -3, 0));


        minVal = listToVisualize.Min();
        maxVal = listToVisualize.Max();
        avg = listToVisualize.Average();

        infoText.text = string.Format("Min: {0} | Max: {1} | Average: {2}", minVal, maxVal, avg);

        middle = (maxVal + minVal) / 2;
        maxDiff = maxVal - middle;
        heightPerVal = 2.5f / maxDiff;

        Debug.Log("MId " + middle);
        Debug.Log("maxDiff " + maxDiff);
        Debug.Log("Max " + maxVal + " Min " + minVal);

        List<Vector3> pointsList = GraphPoints();
        lineRenderer.positionCount = pointsList.Count;
        lineRenderer.SetPositions(pointsList.ToArray());
        lineRenderer.Simplify(SimplifyTolerance);
    }

    public List<Vector3> GraphPoints()
    {
        List<Vector3> result = new List<Vector3>();

        lineRenderer.positionCount = listToVisualize.Count;

        for(int i = 0; i<listToVisualize.Count; i++)
        {
            float x = (float)(i * interval) - 8f;
            float y = (listToVisualize[i] - middle) * heightPerVal;
            result.Add(new Vector3(x, y, 0));
        }

        return result;
    }

}

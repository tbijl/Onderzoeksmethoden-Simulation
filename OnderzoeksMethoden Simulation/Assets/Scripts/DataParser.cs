using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public struct StockInfo
{
    public float open;
    public float close;
    public float high;
    public float low;

    public int volume;
}

public enum DataTypes
{
    Open,
    Close,
    High,
    Low,
    Volume
}

public class DataParser : MonoBehaviour
{
    [SerializeField] string filename;
    string path;

    StreamReader reader;

    public List<StockInfo> stockList;

    // Start is called before the first frame update
    void Start()
    {
        path = $"Assets/Resources/{filename}.txt";
        reader = new StreamReader(path);

        stockList = ParseFile();
        stockList.Reverse();

        DataVisualizer visualizer = FindObjectOfType<DataVisualizer>();
        visualizer.Visualize();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<StockInfo> ParseFile()
    {
        List<StockInfo> result = new List<StockInfo>();

        bool stop = false;
        while (!stop)
        {
            string[] lines = new string[7];

            for (int i = 0; i < 7; i++)
            {
                lines[i] = reader.ReadLine();
                if (lines[i] == null)
                    stop = true;
            }
            if (!stop)
            {
                result.Add(CreateStockInfo(lines));
            }
        }

        return result;
    }

    StockInfo CreateStockInfo(string[] input)
    {
        StockInfo result = new StockInfo();

        result.open     = float.Parse(input[1].Split('\"')[3], CultureInfo.InvariantCulture);
        result.high     = float.Parse(input[2].Split('\"')[3], CultureInfo.InvariantCulture);
        result.low      = float.Parse(input[3].Split('\"')[3], CultureInfo.InvariantCulture);
        result.close    = float.Parse(input[4].Split('\"')[3], CultureInfo.InvariantCulture);
        result.volume   = int.Parse(input[5].Split('\"')[3], CultureInfo.InvariantCulture);

        return result;
    }

    public List<float> ExtractValues(DataTypes type)
    {
        List<float> result = new List<float>();

        switch (type)
        {
            case DataTypes.Open:
                for (int i = 0; i < stockList.Count; i++)
                    result.Add(stockList[i].open);
                break;
            case DataTypes.Close:
                for (int i = 0; i < stockList.Count; i++)
                    result.Add(stockList[i].close);
                break;
            case DataTypes.High:
                for (int i = 0; i < stockList.Count; i++)
                    result.Add(stockList[i].high);
                break;
            case DataTypes.Low:
                for (int i = 0; i < stockList.Count; i++)
                    result.Add(stockList[i].low);
                break;
        }

        

        return result;
    }
}

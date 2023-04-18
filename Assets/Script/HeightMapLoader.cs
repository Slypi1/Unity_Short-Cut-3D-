using UnityEngine;
using System.IO;
using System;

public class HeightMapLoader : MonoBehaviour
{
    [SerializeField] private Path _path;
    private string _fileName = "heightmap.txt";

    void Awake() => LoadHightMap(_fileName);

    private void LoadHightMap(string fileName)
    {
        string filePath = Application.dataPath + "/" + fileName;

        if (File.Exists(filePath))
        {
            string[] data = File.ReadAllLines(filePath);
            BuildHeightMap(data);
        }
        else 
            Debug.Log("File not found.");
    }
    
    private void BuildHeightMap(string [] data)
    {
        string[] size = data[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int widthMap= int.Parse(size[0]);
        int lenghtMap= int.Parse(size[1]);

        for (int l = 0; l < lenghtMap; l++)
        {
            for (int w = 0; w < widthMap; w++)
            {
                string[] values = data[widthMap - w].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int heightSlot = int.Parse(values[l]);
                CreatingSlot(l,heightSlot,w);           
            }
        }      
    }

   private void CreatingSlot(int x, int y, int z)
   {
        float slotSize = 1f;
        GameObject slotPref = Resources.Load<GameObject>("Slot");

        Vector3 position = new Vector3(x * slotSize, y * 0.5f, z * slotSize);
        GameObject newSlot = Instantiate(slotPref, position, Quaternion.identity, transform);
        newSlot.transform.localScale = new Vector3(slotSize, y, slotSize);

        _path.AddSlot(newSlot.transform);
   }
}

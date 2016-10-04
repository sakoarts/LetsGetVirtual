using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int arrayNumber;
    private float yOffset = 5;
    private Color textColor = Color.black;
    private int fontSize = 20;

    private int xSize, ySize;
    private float minX, minY, minZ;
    private Vector3 position;
    private char[] charArray;
    private Vector3[] vertices;
    private Vector3 transformation;
    private String objectName;


    private void Start()
    {
        Generate();
    }
    private void Generate()
    {
        objectName = gameObject.name;
        position = gameObject.transform.position;
        Vector3 bounds = GetComponent<Collider>().bounds.size;
        minX = (float)(position.x - 0.5 * bounds.x);
        minY = (float)(position.y - 0.5 * bounds.y);       
        minZ = (float)(position.z - 0.5 * bounds.z);
        
        charArray = Main.ps.getFilledArrays()[arrayNumber];
        double size = Convert.ToInt64(Math.Sqrt(charArray.Length));
        this.xSize = (int)size;
        this.ySize = xSize;

        vertices = new Vector3[(xSize) * (ySize)];
    
        float stepX = bounds.x / xSize;
        float stepY = bounds.y / ySize;
        float stepZ = bounds.z / xSize;


        Vector3 startlocation;
        switch (objectName)
        {
            case "WallWest":
            case "WallEast":

                //YZ - stelsel
                startlocation = new Vector3((float)(minX), (float)(minY + 0.5 * stepY), (float)(0.5 * stepZ + minZ));
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < xSize; z++, i++)
                    {
                        Vector3 location = new Vector3(0, y * stepY, z * stepZ) + startlocation;
                        vertices[i] = location;
                    }
                }
                break;
            case "WallNorth":
            case "WallSouth":
                // XY - stelsel            
                startlocation = new Vector3((float)(minX + 0.5 * stepX), (float)(minY + 0.5 * stepY), (float)minZ);
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++, i++)
                    {
                        Vector3 location = new Vector3(x * stepX, y * stepY) + startlocation;
                        vertices[i] = location;
                    }
                }
                break;
            case "Ceiling":
            case "Floor":
                // XZ-stelsel
                startlocation = position;
                float stepAngle = 360 / charArray.Length;
                float angle = 0;
                for (int i = 0; i < xSize*ySize; i++)
                {
                    Vector3 location = PointOnCircle((float)(0.7* bounds.x / 2), angle, startlocation);
                    vertices[i] = location;
                    angle += stepAngle;
                }
                break;
        }
    }

  

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            GizmosUtils.DrawText(GUI.skin, charArray[i].ToString(), vertices[i], textColor, fontSize, yOffset);
        }
    }

    private  Vector3 PointOnCircle(float radius, float angleInDegrees, Vector3 origin)
    {
        // Convert from degrees to radians via multiplication by PI/180        
        float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180F)) + origin.x;   
        float z = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180F)) + origin.z;



        return new Vector3(x, origin.y,z);
    }
}


public static class GizmosUtils
{
    public static void DrawText(GUISkin guiSkin, string text, Vector3 position, Color? color = null, int fontSize = 0, float yOffset = 0)
    {
#if UNITY_EDITOR
        var prevSkin = GUI.skin;
        if (guiSkin == null)
            Debug.LogWarning("editor warning: guiSkin parameter is null");
        else
            GUI.skin = guiSkin;

        GUIContent textContent = new GUIContent(text);

        GUIStyle style = (guiSkin != null) ? new GUIStyle(guiSkin.GetStyle("Label")) : new GUIStyle();
        if (color != null)
            style.normal.textColor = (Color)color;
        if (fontSize > 0)
            style.fontSize = fontSize;

        Vector2 textSize = style.CalcSize(textContent);
        Vector3 screenPoint = Camera.current.WorldToScreenPoint(position);

        if (screenPoint.z > 0) // checks necessary to the text is not visible when the camera is pointed in the opposite direction relative to the object
        {
            var worldPosition = Camera.current.ScreenToWorldPoint(new Vector3(screenPoint.x - textSize.x * 0.5f, screenPoint.y + textSize.y * 0.5f + yOffset, screenPoint.z));
            UnityEditor.Handles.Label(worldPosition, textContent, style);
        }
        GUI.skin = prevSkin;
#endif
    }
}
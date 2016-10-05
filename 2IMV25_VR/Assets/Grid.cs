using System;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public int arrayNumber;
    private float yOffset = 5;
    private Color textColor = Color.black;
    private int fontSize = 26;

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

        vertices = new Vector3[charArray.Length];
    
        float stepX = bounds.x / xSize;
        float stepY = bounds.y / ySize;
        float stepZ = bounds.z / xSize;


        Vector3 startlocation;
        float stepAngle;
        float angle;
        switch (objectName)
        {
            case "WallWest":
                //YZ - stelsel
                startlocation = new Vector3((float)(minX), (float)(minY + 0.8 * stepY), (float)(0.5 * stepZ + minZ));
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < xSize; z++, i++)
                    {
                        Vector3 location = new Vector3(0, y * stepY, z * stepZ) + startlocation;
                        vertices[i] = location;
                    }
                }
                drawLetter(vertices, new Vector3(0, 90, 0), false, 0);
                break;
            case "WallEast":

                //YZ - stelsel
                startlocation = new Vector3((float)(minX), (float)(minY + 0.8 * stepY), (float)(0.5 * stepZ + minZ));
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int z = 0; z < xSize; z++, i++)
                    {
                        Vector3 location = new Vector3(0, y * stepY, z * stepZ) + startlocation;
                        vertices[i] = location;
                    }
                }
                drawLetter(vertices, new Vector3(0, -90, 0), false, 0);
                break;
            case "WallNorth":
                // XY - stelsel            
                startlocation = new Vector3((float)(minX + 0.5 * stepX), (float)(minY + 0.8 * stepY), (float)minZ);
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++, i++)
                    {
                        Vector3 location = new Vector3(x * stepX, y * stepY) + startlocation;
                        vertices[i] = location;
                    }
                }
                drawLetter(vertices, new Vector3(0, 180, 0), false, 0);
                break;
            case "WallSouth":
                // XY - stelsel            
                startlocation = new Vector3((float)(minX + 0.5 * stepX), (float)(minY + 0.8 * stepY), (float)minZ);
                for (int i = 0, y = 0; y < ySize; y++)
                {
                    for (int x = 0; x < xSize; x++, i++)
                    {
                        Vector3 location = new Vector3(x * stepX, y * stepY) + startlocation;
                        vertices[i] = location;
                    }
                }
                drawLetter(vertices,new Vector3(0,0,0), false, 0);
                break;
            case "Ceiling":
                // XZ-stelsel
                startlocation = position;
                stepAngle = 360 / charArray.Length;
                angle = 0;
                for (int i = 0; i < charArray.Length; i++)
                {
                    Vector3 location = PointOnCircle((float)(0.65 * bounds.x / 2), angle, startlocation);

                    vertices[i] = location;
                    angle += stepAngle;

                }
                drawLetter(vertices, new Vector3(-90, 0, 0), true, stepAngle);
                break;
            case "Floor":
                // XZ-stelsel
                startlocation = position;
                 stepAngle = 360 / charArray.Length;
                 angle = 0;
                for (int i = 0; i < charArray.Length; i++)
                {
                    Vector3 location = PointOnCircle((float)(0.65* bounds.x / 2), angle, startlocation);
                  
                    vertices[i] = location;
                    angle += stepAngle;
                 
                }
                drawLetter(vertices, new Vector3(90, 0,0),false,stepAngle);
                break;
        }
    }

   
    public void drawLetter(Vector3[] vertices,Vector3 rotation, Boolean roof, float stepAngle)
     {
        for (int i = 0; i < vertices.Length; i++)
             {

            GameObject letter = Resources.Load<GameObject>("letter");
            GameObject clone = Instantiate(letter,vertices[i],Quaternion.identity) as GameObject;
            clone.transform.Rotate(rotation.x, rotation.y, rotation.z);
            if (stepAngle != 0)
            {
                if (roof)
                {
                    clone.transform.Rotate(0, 0, -stepAngle * i - 90);
                } else
                {
                    clone.transform.Rotate(0, 0, stepAngle * i - 90);
                }
              
            }
            TextMesh ms = clone.GetComponent<TextMesh>();
            ms.text = charArray[i].ToString();
        }
    }

    //private void OnDrawGizmos()
    //{

    //    if (vertices == null)
    //    {
    //        return;
    //    }
    //    Gizmos.color = Color.black;
    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        GizmosUtils.DrawText(GUI.skin, charArray[i].ToString(), vertices[i], textColor, fontSize, yOffset);
    //    }
    //}

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
           
            UnityEditor.Handles.Label(position, textContent, style);
        }
        GUI.skin = prevSkin;
#endif
    }
}
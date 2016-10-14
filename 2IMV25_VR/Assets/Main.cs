using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

public class Main : MonoBehaviour
{

    public static ProblemSet ps;
    private Boolean pressed = false;
    private float timeLeft = 2;
    public static float fieldOfView;

    void Awake()
    {
        changeFOV();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!pressed)   
            {
                pressed = true;
                generatePS();
                Vector3 position = GameObject.Find("WallSouth").transform.position + new Vector3((float)1.3, (float)0.1, 0);
                drawTarget(position, ps.getTarget());
            } else  
                SceneManager.LoadScene("LetsGetVirtual");           
        }
       
    }

    private void drawTarget(Vector3 pos, char target)
    {
        GameObject letter = Resources.Load<GameObject>("letter");
        GameObject clone = Instantiate(letter, pos, Quaternion.identity) as GameObject;
        TextMesh ms = clone.GetComponent<TextMesh>();
        ms.text = target.ToString();
    }

    public void generatePS()
    {
        System.Random rnd = new System.Random();
        int chance = rnd.Next(0, 100);
        int chance1 = rnd.Next(0, 100);
        Boolean targetPresent = true;
        if (chance < 50)
        {
            targetPresent = false;
        }
        if (chance1 < 50)
        {
            ps = new ProblemSet(ProblemSet.CharSet.A, targetPresent);
        }
        else
        {
            ps = new ProblemSet(ProblemSet.CharSet.B, targetPresent);
        }
    }

    private void changeFOV()
    {
        System.Random rnd = new System.Random();
        int fovChance = rnd.Next(0, 100);
        if (fovChance < 33)
        {
            fieldOfView =  70f;
        }
        else if (fovChance < 66)
        {
            fieldOfView = 100f;
        }
        else
        {
            fieldOfView = 130f;
        }
       Camera.main.fieldOfView = fieldOfView;
    }

   
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class TestHandler : MonoBehaviour
{

    private float timer = 0;
    private float endTime = 0;

    // Use this for initialization
    void Awake()
    {
        Camera.main.fieldOfView =  Main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        { 
            endTime = timer;
            String output = Main.ps.returnExercise() + ", " + Main.fieldOfView + ", " + endTime;
            TxtWriter tw = new TxtWriter();
            tw.addLine(output);
        
            SceneManager.LoadScene("Introduction");
        }
    }
}

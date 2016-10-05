using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class TestHandler : MonoBehaviour
{

    private float timer = 0;
    private float startTime = 0;
    private float endTime = 0;
    private char target = Main.ps.getTarget();
    private string output = "";
    private Boolean tPresent = Main.ps.targetPresent;

    // Use this for initialization
    void Awake()
    {
        startTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        { 
            endTime = timer;
            output = "" + tPresent + ", "+target + ", "+startTime + ", "+endTime;
            Debug.Log(output);
            SceneManager.LoadScene("Introduction");
        }
    }
}

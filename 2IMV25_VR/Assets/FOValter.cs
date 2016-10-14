using UnityEngine;

public class FOValter : MonoBehaviour
{

    public float startFOV = 90f;
    public float deltaFOV = 28f;
    public float speed = 10f;
    private float timer = 0f;

    void Update()
    {
        //if (ticker == 30)
        {
            Camera.main.fieldOfView = startFOV + Mathf.Sin(timer * speed) * deltaFOV;
            timer += Time.deltaTime;
        }
    }
}
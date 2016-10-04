using UnityEngine;
using System.Collections;

public class movement : MonoBehaviour {

	float speedX = 0;
	float speedY = -5;
	float speedZ = 0;
	bool pressed = false;
	public float depth = -10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.T)){
			pressed = true;
		}
		if (pressed) {
			float pos = this.transform.position.y;
			if (pos > depth) {
				transform.Translate (new Vector3 (speedX, speedY, speedZ) * Time.deltaTime);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class InitialForce : MonoBehaviour {

	public Rigidbody rb;
	public float thrust;
	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		//rb.AddForce (new Vector3(thrust, 0, 0));
	}
	
	// Update is called once per frame
	void Update () 
	{
		rb.AddForce (new Vector3( Random.Range(-100.0F, 100.0F),  0,  Random.Range(-100.0F, 100.0F)));
	}
}

using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float _speed;

	// Use this for initialization
	void Start () {
       GetComponent<Rigidbody>().velocity = transform.forward * _speed;        
	}
}

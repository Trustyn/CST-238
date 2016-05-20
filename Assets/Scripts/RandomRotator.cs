using UnityEngine;
using System.Collections;




public class RandomRotator : MonoBehaviour {

    public float _multMin, _multMax;
    public int _points;
    private Rigidbody rb;
   
    void Start()
    {
        float _multiplier = Random.Range(_multMin, _multMax);
        DamageController _dc = gameObject.GetComponent<DamageController>();
        _dc._health = _dc._experience = 1;
        _dc._points = _multiplier * _points;
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * _multiplier;
    }
}

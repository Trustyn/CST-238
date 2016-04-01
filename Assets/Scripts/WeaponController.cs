using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public GameObject _shot;
    public Transform _shotSpawn;
    public float _fireRate;
    public float _delay;

    void Start()
    {
        InvokeRepeating("Fire", _delay, _fireRate);
    }

    void Fire()
    {
        Instantiate(_shot, _shotSpawn.position, _shotSpawn.rotation);
    }
    
}

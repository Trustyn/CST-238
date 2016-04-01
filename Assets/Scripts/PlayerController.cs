using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundry
{
    public float xmin, xmax, zmin, zmax;
}

public class PlayerController : MonoBehaviour {

    
    public Boundry _boundry;
    public float _speed, _tilt;    

    private Rigidbody _rb;
    private float _experience;
    private int _score;
    private Gun _gun;

    public int Score
    {
        get { return _score; }
    }

    public float Experience
    {
        get { return _experience; }
    }


    void Start()
    {
        _score = 0;
        _experience = 0.0f;
        _rb = GetComponent<Rigidbody>();
        _gun = gameObject.GetComponent<DamageController>()._gun;        
    }

	void FixedUpdate ()
    {
        float mvHor = Input.GetAxis("Horizontal");
        float mvVert = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(mvHor, 0.0f, mvVert);

        
        _rb.velocity = move * _speed;

        _rb.position = new Vector3(
             Mathf.Clamp(_rb.position.x, _boundry.xmin, _boundry.xmax),
             0.0f,
             Mathf.Clamp(_rb.position.z, _boundry.zmin, _boundry.zmax)
        );

        _rb.rotation = Quaternion.Euler(0.0f, 0.0f, _rb.velocity.x * (-_tilt));
	}

    void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space)) && Time.time > _gun.NextFire)
        {
            _gun.NextFire = Time.time + _gun.fireRate;
            foreach(Transform spawnPoint in _gun.FiringLocations) 
                Instantiate(_gun.shot, spawnPoint.position, spawnPoint.rotation); 
        }
    }
}

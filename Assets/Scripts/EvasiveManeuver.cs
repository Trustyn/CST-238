using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

    public float _dodge, _smoothing, tilt;
    public Vector2 _startWait, _manueverTime, _maneuverWait;
    public Boundry _boundry;

    private float _targetManuever, _currentSpeed;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(_startWait.x, _startWait.y));
        while (true)
        {
            _targetManuever = Random.Range(1,_dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(_manueverTime.x, _manueverTime.y));
            _targetManuever = 0;
            yield return new WaitForSeconds(Random.Range(_maneuverWait.x, _maneuverWait.y));
        }
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        float newManuever = Mathf.MoveTowards(rb.velocity.x, _targetManuever, Time.deltaTime * _smoothing);
        rb.velocity = new Vector3(newManuever, 0.0f, _currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, _boundry.xmin, _boundry.xmax),
            rb.position.y,
            Mathf.Clamp(rb.position.z, _boundry.zmin, _boundry.zmax)
            );

        rb.rotation = Quaternion.Euler(0, 180.0f, rb.position.x * -tilt);
	}
}

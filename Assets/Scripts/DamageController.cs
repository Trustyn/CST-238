using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Gun
{
    private float nextFire;
    public float fireRate;
    public float dmg;
    public GameObject shot;
    public Transform[] _spawnPoints;

    private List<Transform> _activePoints; 

    public Gun()
    {
        _activePoints = new List<Transform>();
        if (_spawnPoints != null && _spawnPoints.Length == 1)
            _activePoints.Add(_spawnPoints[0]);
    }

    public float NextFire
    {
        get; set;
    }

    public int UpgradeLevel { get; set; }

    public Transform[] FiringLocations
    {
        get { return _activePoints.ToArray(); }
    }

    public void ClearFiringLocations()
    {
        _activePoints.Clear();
    }

    public void AddFiringLocation(Transform location)
    {
        _activePoints.Add(location);
    }

    public void Setup(int level)
    {
        if (level > 12)
            return;

        _activePoints.Clear();

        switch (level)
        {
            case 3:
                _activePoints.Add(_spawnPoints[1]);
                _activePoints.Add(_spawnPoints[2]);
                break;
            case 6:
                _activePoints.Add(_spawnPoints[0]);
                _activePoints.Add(_spawnPoints[3]);
                _activePoints.Add(_spawnPoints[4]);
                break;
            case 9:
                _activePoints.Add(_spawnPoints[0]);
                _activePoints.Add(_spawnPoints[1]);
                _activePoints.Add(_spawnPoints[2]);
                break;
            case 12:
                _activePoints.Add(_spawnPoints[0]);
                _activePoints.Add(_spawnPoints[1]);
                _activePoints.Add(_spawnPoints[2]);
                _activePoints.Add(_spawnPoints[3]);
                _activePoints.Add(_spawnPoints[4]);
                break;
            default:
                _activePoints.Add(_spawnPoints[0]);
                break;
        }
    }
}

public class DamageController : MonoBehaviour {

    public float _health, _experience, _points;
    public Gun _gun;

    private int _level;

    void Start()
    {
        _level = 1;
        if (gameObject.tag == "Player")
            _gun.Setup(_level);
    }

    
    
    public float Experience
    {
        get { return _experience; }
    }

    public int Points
    {
        get { return (int)_points; }
    }
        
    public float Damage
    {
        get { return (_gun != null)? _gun.dmg : 1; }
    }

    public bool Hit(float dmg)
    {
        return (_health -= dmg) <= 0;
    }

    public void UpdateScore(int add_points)
    {
        _experience += (add_points*2);
        _points += add_points;
        if (CheckForLevel())
        {
            _level++;
            _experience /= 100;
            if ((_level % 3) == 0)
            {
                _gun.Setup(_level);
            }
            else if ((_level % 2) == 0)
            {
                //Every other level increase gun damage or firing speed up to a maximum of 5 for damage
                //or down to a 0.1 firing speed.
                if (Random.Range(0, 100) <= 50)
                {
                    if(_gun.dmg < 5)
                        _gun.dmg++;
                }
                else
                {
                    if(_gun.fireRate > 0.1)
                        _gun.fireRate -= 0.05f;
                }
            }                
        }
        
    }

    bool CheckForLevel()
    {
        return _experience >= 100;
    }
}

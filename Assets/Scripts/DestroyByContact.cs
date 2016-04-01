using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    
    private GameController   _game;
    public GameObject        _explosion;
    public GameObject        _playerExplosion;
    private DamageController _player, _target;
    

    void Start()
    {
        _target = gameObject.GetComponent<DamageController>();
        GameObject _pObject = GameObject.FindGameObjectWithTag("Player");

        if (_pObject)        
            _player = _pObject.GetComponent<DamageController>();
            
        

        GameObject game = GameObject.FindGameObjectWithTag("GameController");
     
        if (game != null)
            _game = game.GetComponent<GameController>();
        else
            Debug.Log("Cannot find 'GameController' script");
    }

    void OnTriggerEnter(Collider other)
    {
        if(InvalidCollision(other))
            return;        
        
        Destroy(other.gameObject);

        if (other.tag == "Player")
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
            _game.GameOver();
            return;
        }        
        else if (_target.Hit(_player.Damage))
        {
            _player.UpdateScore(_target.Points);
            _game.UpdateScore();
            Destroy(gameObject);
            if(_explosion)
                Instantiate(_explosion, transform.position, transform.rotation);
        }
    }

    bool InvalidCollision(Collider other)
    {
        bool retVal = false;
        if (Boundry(other) || AnotherAsteroid(other) || EnemyShip(other))
            retVal = true;

        return retVal;
    }

    bool Boundry(Collider other)
    {
        return other.tag == "Boundry";
    }

    bool AnotherAsteroid(Collider other)
    {
        return other.tag == this.tag;
    }

    bool EnemyShip(Collider other)
    {
        bool retVal = false;

        if (gameObject.tag == "Hazard" && other.tag == "Enemy" ||
            gameObject.tag == "Enemy" && other.tag == "Hazard")
            retVal = true;

        return retVal;
    }
}

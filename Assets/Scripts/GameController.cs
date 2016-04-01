using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] _hazards;
    public Vector3 _spawnValues;
    public Text _scoreText, _experienceText, _restartText, _overText;
    public GameObject _playerObject;

    public float _startWait, _spawnWait, _waveWait;

    public int _hazardCount;

    private bool _restart, _gameOver, _alive;
    private DamageController _player;

    void Start()
    {
        _restartText.text = _overText.text = "";
        _restart = _gameOver = false;
        StartCoroutine(SpawnWaves());
        _playerObject = Instantiate(_playerObject) as GameObject;
        _player = _playerObject.GetComponent<DamageController>();
        UpdateScore();
    }

    void Update()
    {
        if (_restart)
        {
            if (Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(_startWait);
        while (!_gameOver)
        {
            for (int i = 0; i < _hazardCount; ++i)
            {
                GameObject _hazard = _hazards[Random.Range(0, _hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-_spawnValues.x, _spawnValues.x), _spawnValues.y, _spawnValues.z);
                Instantiate(_hazard, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(_spawnWait);
            }
            yield return new WaitForSeconds(_waveWait);
        }                
    }

    public void GameOver()
    {
        _overText.text = "GAME OVER";
        _restartText.text = "Press 'R' to restart game";
        _gameOver = _restart = true;
    }

    public void UpdateScore()
    {
        //Debug.Log(_player.Points);
        _scoreText.text = "Score: " + _player.Points;
        _experienceText.text = "Experience: " + _player.Experience;
    }
}

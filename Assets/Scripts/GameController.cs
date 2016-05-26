using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject[] _hazards;
    public Vector3 _spawnValues;
    public Text _scoreText, _experienceText, _restartText, _overText;
    public GameObject _playerObject;

    public float _startWait, _spawnWait, _waveWait;

    public int _hazardCount;
    public GameObject _settingsScreen, _splashScreen;
    private bool _restart, _gameOver, _alive;
    private DamageController _player;
    private GameObject splash;
    private Animator anim;
    private AudioSource audio;
    public Slider _spawns;

    void Start()
    {
        splash = GameObject.FindGameObjectWithTag("SplashScreen");
        anim = gameObject.GetComponent<Animator>();
        splash.SetActive(true);
        _restartText.text = _overText.text = _scoreText.text = _experienceText.text = "";
        _restart = _gameOver = false;
        audio = gameObject.GetComponent <AudioSource>();
        _spawns.onValueChanged.AddListener(delegate { UpdateSpawnCount(); });
    }

    void Update()
    {
        if (_restart)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void StartGame() {
        anim.SetTrigger("Play");
        splash.SetActive(false);
        StartCoroutine(SpawnWaves());
        _playerObject = Instantiate(_playerObject) as GameObject;
        _player = _playerObject.GetComponent<DamageController>();
        UpdateScore();
    }

    public void SpawnAmountSettings(int number)
    {
        _hazardCount = number;
    }

    private void UpdateSpawnCount()
    {
        _hazardCount = (int)_spawns.value;
    }

    public void ShowSettings()
    {
        _splashScreen.SetActive(false);
        _settingsScreen.SetActive(true);
    }

    public void CloseSettings()
    {
        _settingsScreen.SetActive(false);
        _splashScreen.SetActive(true);
    }



}

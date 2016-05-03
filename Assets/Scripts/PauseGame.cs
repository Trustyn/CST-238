using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
    private bool _paused;
    private Animator _anim;
    private AudioSource _audio;

    void Start() {
        _paused = false;
        _anim = gameObject.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (_paused) {
                Time.timeScale = 1;
                _anim.SetBool("_paused", false);
                _audio.mute = false;
                _paused = false;
            } else {
                Time.timeScale = 0;
                _anim.SetBool("_paused", true);
                _audio.mute = true;
                _paused = true;               
            }
        }
	}
}

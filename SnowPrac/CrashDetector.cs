using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delay = 0.7f;
    [SerializeField] ParticleSystem effect;
    [SerializeField] AudioClip crashSFX;
    bool hasCrashed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ground" && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControl();
            effect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", delay);
        }   
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

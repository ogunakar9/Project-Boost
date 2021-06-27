using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip defeatClip;
    
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem defeatParticle;
    
    private Movement movement;
    private AudioSource audioSource;
    private ParticleSystem _particleSystem;

    private bool isTransitioning = false; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly Ground");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        //TODO: add particle effect upon crash
        
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successClip);
        successParticle.Play();
        movement = GetComponent<Movement>();
        movement.enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    void StartCrashSequence()
    {
        //TODO: add particle effect upon crash

        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(defeatClip);
        defeatParticle.Play();
        movement = GetComponent<Movement>();
        movement.enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

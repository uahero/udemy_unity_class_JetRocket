using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem successParticle;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool crashCheatEnabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheatNextLevel();
        CheatNoCrash();
    }

    void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || crashCheatEnabled) return;
        //if (isTransitioning || crashCheatEnabled == false)
    
            switch (other.gameObject.tag)
            {
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "Friendly":
                    Debug.Log("Start");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
 
    }

    void CheatNextLevel()
    {
        if (Input.GetKey(KeyCode.L)) NextLevel();
    }

    void CheatNoCrash()
    {
        if (Input.GetKey(KeyCode.C)) crashCheatEnabled = !crashCheatEnabled; //this will toggle the collision
    }

    void StartCrashSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        isTransitioning = true;
        Invoke("ReloadLevel", loadDelay);
    }

    void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        isTransitioning = true;
        Invoke("NextLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}

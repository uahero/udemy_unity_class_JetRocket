using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                Debug.Log ("Start");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        //Add soundeffect on crash
        //Add particle effect on crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void StartSuccessSequence()
    {
        //GetComponent<Movement>().enabled = false;
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

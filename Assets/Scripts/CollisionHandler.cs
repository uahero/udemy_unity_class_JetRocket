using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Finish":
                Debug.Log ("Finished");
                break;
            case "Friendly":
                Debug.Log ("Start");
                break;
            case "Fuel":
                Debug.Log ("Fuel up");
                break;
            default:
                ReloadLevel();
                break;
        }
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

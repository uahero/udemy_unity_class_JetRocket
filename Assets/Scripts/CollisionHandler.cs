using UnityEngine;

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
                Debug.Log ("you are dead");
                break;
        }
    }
}

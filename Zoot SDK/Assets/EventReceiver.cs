using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    void Start()
    {

        // Check if running in a WebGL context
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("Running in a WebGL context");

            // Directly call the JavaScript function
            Application.ExternalEval("SetupMessageEventListeners();");

        } else
        {
            Debug.Log("Error: not running in a WebGL context");
        }
    }
}

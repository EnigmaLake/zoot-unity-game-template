using UnityEngine;

public class EventReceiver : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("Running in a WebGL context");
            Application.ExternalEval("if (typeof SetupMessageEventListeners === 'function') { SetupMessageEventListeners(); } else { console.error('SetupMessageEventListeners function is not defined.'); }");
        }
        else
        {
            Debug.Log("Error: not running in a WebGL context");
        }
    }
}

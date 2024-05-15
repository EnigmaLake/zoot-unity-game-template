using UnityEngine;
using System.Runtime.InteropServices;

public class EventSender : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void sendEventResponse(string message);

    void Start()
    {
        sendEventResponse("EL_GET_USER_CURRENCY");
    }
}

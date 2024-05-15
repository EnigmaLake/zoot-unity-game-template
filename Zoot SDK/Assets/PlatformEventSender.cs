using UnityEngine;
using System.Runtime.InteropServices;

public class PlatformEventSender : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void sendEventResponse(string message);

    void Start()
    {
        sendEventResponse("EL_USER_BALANCE");
        sendEventResponse("EL_GET_USER_CURRENCY");
        sendEventResponse("EL_USER_INFORMATION");
        sendEventResponse("EL_GET_EXPANDED_GAME_VIEW");
    }
}

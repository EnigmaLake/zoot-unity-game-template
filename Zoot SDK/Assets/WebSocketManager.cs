using UnityEngine;
using System.Runtime.InteropServices;

public class WebSocketManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WebSocketConnect(string url, string UserId, string UserAccessToken);

    [DllImport("__Internal")]
    private static extern void WebSocketSend(string message);

    [DllImport("__Internal")]
    private static extern void WebSocketClose();

    public string UserAccessToken = "your_user_access_token";
    public string UserId = "your_user_id";

    void Start()
    {
        WebSocketConnect("http://localhost:8080/crash", UserId, UserAccessToken);
    }

    public void SendSocketMessage(string message)
    {
        WebSocketSend(message);
    }

    public void CloseConnection()
    {
        WebSocketClose();
    }
}

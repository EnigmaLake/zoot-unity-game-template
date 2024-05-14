using UnityEngine;
using SocketIOClient;
using System.Collections.Generic;

public class SocketManager : MonoBehaviour
{
    private SocketIOClient.SocketIO client;

    public string DefaultSocketUrl = "non-empty";
    public string GuestAccessToken = "your_guest_access_token";
    public string GuestUserId = "your_guest_user_id";
    public string Path = "/crash";

    void Start()
    {
        //SetSocket();
    }

    public void SetSocket()
    {
        // Initialize the client with the socket URL and options
        client = new SocketIOClient.SocketIO(DefaultSocketUrl, new SocketIOOptions
        {
            Path = Path,
            Auth = new Dictionary<string, object>
            {
                { "authorization", $"Bearer {GuestAccessToken}" },
                { "userId", GuestUserId }
            },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        }) ;

        client.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected to server");
        };

        client.ConnectAsync();
    }

    void OnDestroy()
    {
        if (client != null)
        {
            client.DisconnectAsync();
        }
    }
}
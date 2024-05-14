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

    const string BET_REGISTER = "BET_REGISTER";
    const string BET_DEREGISTER = "BET_DEREGISTER";
    const string BET_CASHOUT = "BET_CASHOUT";
    const string BET_LIST = "BET_LIST";

    void Start()
    {
        //SetSocket();
    }

    public void ConnectToSocket()
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

    public async void BetRegister()
    {
        Debug.Log("Clicked BetRegister");

        // Define the payload to send
        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", "test-123" },
            { "userId", 15 },
            { "userNickname", "Richard" },
            { "playAmountInCents", 1000 },
            { "coinType", 0 },
            { "pictureUrl", "https://lh3.googleusercontent.com/a/ACg8ocLyp0TCe7yq2ydJJm3d32XgcP3yh8T2wEXBHL4zW2dk=s96-c" },
            { "userAccessToken", "test-123" },
        };

        // Send the event to the server with the payload
        await client.EmitAsync(BET_REGISTER, payload);
    }

    void OnDestroy()
    {
        if (client != null)
        {
            client.DisconnectAsync();
        }
    }
}
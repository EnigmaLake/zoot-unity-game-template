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

    // Server Actions (from server towards client)
    const string WELCOME_FROM_SERVER = "WELCOME_FROM_SERVER";

    // Game round status
    const string GAME_ROUND_PREPARED = "GAME_ROUND_PREPARED";
    const string GAME_ROUND_LIVE = "GAME_ROUND_LIVE";
    const string GAME_ROUND_COMPLETED = "GAME_ROUND_COMPLETED";
    const string GAME_ROUND_CANCELED = "GAME_ROUND_CANCELED";

    const string USER_BET_LIST_SUCCEEDED = "USER_BET_LIST_SUCCEEDED";
    const string USER_BET_LIST_FAILED = "USER_BET_LIST_FAILED";

    // Register events (confirming register action)
    const string BET_REGISTER_SUCCEEDED = "BET_REGISTER_SUCCEEDED";
    const string BET_REGISTER_FAILED = "BET_REGISTER_FAILED";

    // Deregister events (confirming deregister action)
    const string BET_DEREGISTER_SUCCEEDED = "BET_DEREGISTER_SUCCEEDED";
    const string BET_DEREGISTER_FAILED = "BET_DEREGISTER_FAILED";

    // Cashout events (confirming cashout action)
    const string BET_CASHOUT_SUCCEEDED = "BET_CASHOUT_SUCCEEDED";
    const string BET_CASHOUT_FAILED = "BET_CASHOUT_FAILED";


    // Client Actions (from client towards server)
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

            SetupListeners();
        };

        client.ConnectAsync();
    }

    private void SetupListeners()
    {
        Debug.Log("Listeners mounted");

        client.On(BET_REGISTER_SUCCEEDED, response =>
        {
            Debug.Log("Bet register succeeded: " + response.ToString());
        });

        client.On(BET_REGISTER_FAILED, response =>
        {
            Debug.Log("Bet register failed: " + response.ToString());
        });
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
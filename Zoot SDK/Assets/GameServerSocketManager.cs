using UnityEngine;
using SocketIOClient;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameServerSocketManager: MonoBehaviour
{
    private SocketIOClient.SocketIO client;

    public string DefaultSocketUrl = "http://localhost:8080/";
    public string UserAccessToken = "user_access_token_123";
    public string UserId = "15";
    public string Path = "/crash";

    public string GameRoundUuid = "test123";
    public int playAmount = 1;
    public int coinTypeId = 0;

    public TMPro.TextMeshProUGUI CurrentGameRoundStatus;


    /**
     * Server Actions (from server towards client)
     */
    const string WELCOME_FROM_SERVER = "WELCOME_FROM_SERVER";

    // Game round status
    const string GAME_ROUND_PREPARED = "GAME_ROUND_PREPARED";
    const string GAME_ROUND_LIVE = "GAME_ROUND_LIVE";
    const string GAME_ROUND_COMPLETED = "GAME_ROUND_COMPLETED";

    const string USER_BET_LIST_SUCCEEDED = "USER_BET_LIST_SUCCEEDED";
    const string USER_BET_LIST_FAILED = "USER_BET_LIST_FAILED";

    // Register events (confirming play register action)
    const string BET_REGISTER_SUCCEEDED = "BET_REGISTER_SUCCEEDED";
    const string BET_REGISTER_FAILED = "BET_REGISTER_FAILED";

    // Cashout events (confirming play cashout action)
    const string BET_CASHOUT_SUCCEEDED = "BET_CASHOUT_SUCCEEDED";
    const string BET_CASHOUT_FAILED = "BET_CASHOUT_FAILED";


    /**
     * Client Actions (from client towards server) 
     */
    const string BET_REGISTER = "BET_REGISTER";
    const string BET_CASHOUT = "BET_CASHOUT";
    const string BET_LIST = "BET_LIST";

    async void Start()
    {
        Debug.Log("Calling ConnectToSocket");
        await ConnectToSocket();
    }

    private void Update()
    {
        CurrentGameRoundStatus.ForceMeshUpdate(true);
    }

    private async Task ConnectToSocket()
    {

        Debug.Log("DefaultSocketUrl: " + DefaultSocketUrl);
        Debug.Log("Path: " + Path);
        Debug.Log("UserAccessToken: " + UserAccessToken);
        Debug.Log("UserId: " + UserId);

        // Initialize the client with the socket URL and options
        client = new SocketIOClient.SocketIO(DefaultSocketUrl, new SocketIOOptions
        {
            Path = Path,
            Auth = new Dictionary<string, object>
            {
                { "authorization", $"Bearer {UserAccessToken}" },
                { "userId", UserId }
            },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });

        client.OnDisconnected += (sender, e) =>
        {
            Debug.Log("Disconnected from server");
            Debug.Log(e.ToString());
        };

        client.OnReconnectAttempt += (sender, attempt) =>
        {
            Debug.Log("Reconnect attempt: " + attempt);
        };

        client.OnError += (sender, e) =>
        {
            Debug.Log("Socket Error: " + e);
        };

        client.On("connect_error", response =>
        {
            Debug.Log("Connect Error: " + response.ToString());
        });

        client.OnConnected += (sender, e) =>
        {
            Debug.Log("Connected to Socket server");

            SetupListeners();
        };

        await client.ConnectAsync();
    }

    /**
     * Responsible with listening to events incoming from the server towards the client
     */
    private void SetupListeners()
    {

        /**
         * After the client sends an event towards the server,
         * the server will respond with a Success or Failture event
         * E.g. BET_REGISTER_SUCCEEDED or BET_REGISTER_FAILED
         */
        client.On(BET_REGISTER_SUCCEEDED, response =>
        {
            Debug.Log("Incoming BET_REGISTER_SUCCEEDED:" + response.ToString());
        });

        client.On(BET_REGISTER_FAILED, response =>
        {
            Debug.Log("Incoming BET_REGISTER_FAILED:" + response.ToString());
        });

        client.On(BET_CASHOUT_SUCCEEDED, response =>
        {
            Debug.Log("Incoming BET_CASHOUT_SUCCEEDED:" + response.ToString());
        });

        client.On(BET_CASHOUT_FAILED, response =>
        {
            // Debugging incomint event payload 
            object t0 = response.GetValue<object>();

            object t2 = response;

            Debug.Log("BET_CASHOUT_FAILED before fancy parsing");

            var parsedResponse = response.GetValue<string>();

            Debug.Log("BET_CASHOUT_FAILED reason: " + parsedResponse);
        });

        client.On(USER_BET_LIST_SUCCEEDED, response =>
        {
            Debug.Log("Incoming USER_BET_LIST_SUCCEEDED:" + response.ToString());
        });

        client.On(USER_BET_LIST_FAILED, response =>
        {
            Debug.Log("Incoming USER_BET_LIST_FAILED:" + response.ToString());
        });


        /**
         * Upon connecting to the Socket server, the server will send a
         * welcome event containnig the current status of the game round
         */
        client.On(WELCOME_FROM_SERVER, response =>
        {
            Debug.Log("Incoming WELCOME_FROM_SERVER with GameRoundStatus: " + response.ToString());

            CurrentGameRoundStatus.SetText(WELCOME_FROM_SERVER);
        });

        /**
         * The server sends game round status updates
         */
        client.On(GAME_ROUND_PREPARED, response =>
        {
            Debug.Log("Incoming GameRoundStatus: GAME_ROUND_PREPARED");

            CurrentGameRoundStatus.SetText(GAME_ROUND_PREPARED);
        });

        client.On(GAME_ROUND_LIVE, response =>
        {
            Debug.Log("Incoming GameRoundStatus: GAME_ROUND_LIVE");

            CurrentGameRoundStatus.SetText(GAME_ROUND_LIVE);
        });

        client.On(GAME_ROUND_COMPLETED, response =>
        {
            Debug.Log("Incoming GameRoundStatus: GAME_ROUND_COMPLETED");

            CurrentGameRoundStatus.SetText(GAME_ROUND_COMPLETED);
        });

        Debug.Log("Listeners mounted");
    }

    // Register a bet register action within a gameRound
    public async void BetRegister()
    {
        Debug.Log("Clicked BetRegister");

        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", GameRoundUuid },
            { "userId", UserId },
            { "userNickname", "Richard" },
            { "playAmountInCents", playAmount * 100 },
            { "coinType", coinTypeId },
            { "pictureUrl", "https://lh3.googleusercontent.com/a/ACg8ocLyp0TCe7yq2ydJJm3d32XgcP3yh8T2wEXBHL4zW2dk=s96-c" },
            { "userAccessToken", UserAccessToken },
        };

        // Send the event to the server with the payload
        await client.EmitAsync(BET_REGISTER, payload);
    }

    // Register a bet cashout action within a gameRound
    public async void BetCashout()
    {
        Debug.Log("Clicked BetCashout");

        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", GameRoundUuid },
            { "userId", UserId },
            { "userNickname", "Richard" },
            { "userAccessToken", UserAccessToken },
        };

        // Send the event to the server with the payload
        await client.EmitAsync(BET_CASHOUT, payload);
    }

    // Request from server the list of all registered bets within a gameRound
    public async void BetList()
    {
        Debug.Log("Clicked BetList");

        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", GameRoundUuid },
            { "userId", UserId },
            { "userNickname", "Richard" },
            { "userAccessToken", UserAccessToken },
        };

        // Send the event to the server with the payload
        await client.EmitAsync(BET_LIST, payload);
    }


    void OnDestroy()
    {
        if (client != null)
        {
            client.DisconnectAsync();
        }
    }
}
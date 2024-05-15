using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;

public class GameServerSocketManager: MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void GameServerSocketConnect(string url, string UserId, string UserAccessToken);

    // Type to handle the incoing message payload from the socket.on listener
    public delegate void WebSocketMessageHandler(string message);


    [DllImport("__Internal")]
    private static extern void GameServerSocketOn(string eventName);


    [DllImport("__Internal")]
    private static extern void GameServerSocketSend(string eventName, string payload);

    [DllImport("__Internal")]
    private static extern void GameServerSocketClose();


    public string DefaultSocketUrl = "http://localhost:8080/";
    public string UserAccessToken = "user_access_token_123";
    public string UserId = "15";
    public string Path = "/crash";

    public string GameRoundUuid = "test123";
    public int playAmount = 1;
    public int coinTypeId = 0;

    void Start()
    {

        Debug.Log("Before conecting to the Game Server Socket server");

        GameServerSocketConnect(DefaultSocketUrl, UserId, UserAccessToken);

        Debug.Log("Connected to the Game Server Socket server");


        Debug.Log("Setting up event listener incoming from Game Server Socket server");

        /**
        * Server Actions (from server towards client)
        */
        GameServerSocketOn("WELCOME_FROM_SERVER");

        GameServerSocketOn("GAME_ROUND_PREPARED");
        GameServerSocketOn("GAME_ROUND_LIVE");
        GameServerSocketOn("GAME_ROUND_COMPLETED");

        GameServerSocketOn("USER_BET_LIST_SUCCEEDED");
        GameServerSocketOn("USER_BET_LIST_FAILED");


        // Register events (confirming play register action)
        GameServerSocketOn("BET_REGISTER_SUCCEEDED");
        GameServerSocketOn("BET_REGISTER_FAILED");

        // Cashout events (confirming play cashout action)
        GameServerSocketOn("BET_CASHOUT_SUCCEEDED");
        GameServerSocketOn("BET_CASHOUT_FAILED");

        Debug.Log("Event listener set for incoming from Game Server Socket server");
    }

    public void SendSocketMessage(string eventName, Dictionary<string, object> payload)
    {
        string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
        GameServerSocketSend(eventName, jsonPayload);
    }

    public void CloseConnection()
    {
        GameServerSocketClose();
    }

    public TMPro.TextMeshProUGUI CurrentGameRoundStatus;

    [System.Serializable]
    public class GameData
    {
        public string gameRoundUuid;
        public double timestamp;
    }

    [System.Serializable]
    public class SocketMessage
    {
        public string eventName;
        public GameData data;
    }


    public void HandleSocketOnMessage(string jsonMessage)
    {
        Debug.Log("Received HandleSocketOnMessage: " + jsonMessage);

        SocketMessage message = JsonUtility.FromJson<SocketMessage>(jsonMessage);
        Debug.Log("Received event: " + message.eventName);

        switch (message.eventName)
        {
            case "GAME_ROUND_COMPLETED":
                Debug.Log("Game Round UUID: " + message.data.gameRoundUuid);
                // Handle game round completion logic
                break;
                // Handle other event types similarly
        }
    }
    

    // Register a bet register action within a gameRound
    public void BetRegister()
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

        // Send the event to the game client server with the payload
        this.SendSocketMessage("BET_REGISTER", payload);
    }

    // Register a bet cashout action within a gameRound
    public void BetCashout()
    {
        Debug.Log("Clicked BetCashout");

        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", GameRoundUuid },
            { "userId", UserId },
            { "userNickname", "Richard" },
            { "userAccessToken", UserAccessToken },
        };

        // Send the event to the game client server with the payload
        this.SendSocketMessage("BET_CASHOUT", payload);
    }

    // Request from server the list of all registered bets within a gameRound
    public void BetList()
    {
        Debug.Log("Clicked BetList");

        var payload = new Dictionary<string, object>
        {
            { "gameRoundUuid", GameRoundUuid },
            { "userId", UserId },
            { "userNickname", "Richard" },
            { "userAccessToken", UserAccessToken },
        };

        // Send the event to the game client server with the payload
        this.SendSocketMessage("BET_LIST", payload);
    }


    void OnDestroy()
    {
        GameServerSocketClose();
    }
}
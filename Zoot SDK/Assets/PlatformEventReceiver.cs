using UnityEngine;
using System.Runtime.InteropServices;

public class PlatformEventReceiver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetupMessageEventListeners();

    private GameServerSocketManager gameServerSocketManager;

    void Start()
    {
        gameServerSocketManager = this.GetComponent<GameServerSocketManager>();

        SetupMessageEventListeners();
    }

    public void HandleUserBalance(string message)
    {
        Debug.Log("Received user balance: " + message);
    }

    public void HandleExpandedGameView(string message)
    {
        Debug.Log("Received expanded game view command: " + message);
    }

    // Define a class to match the JSON structure
    [System.Serializable]
    public class UserCurrencyData
    {
        public string currency;
    }

    [System.Serializable]
    public class UserCurrency
    {
        public string type;
        public string event_id;
        public UserCurrencyData data;
    }

    // Method to handle currency event from JavaScript
    public void HandleGetUserCurrency(string jsonMessage)
    {
        Debug.Log("Received currency info: " + jsonMessage);
        // Add your handling code here (parse message, update UI, etc.)

        // Deserialize the JSON string into a UserInformation object
        UserCurrency userCurrency = JsonUtility.FromJson<UserCurrency>(jsonMessage);

        // Update the game server socket manager with the new data
        Debug.Log("Active Currency" + userCurrency.data.currency);
    }

    [System.Serializable]
    public class UserInformation
    {
        public string type;
        public string event_id;
        public UserData data;
    }

    [System.Serializable]
    public class UserData
    {
        public string nickname;
        public string avatar;
        public int id;
        public string accessToken;
    }

    public void HandleUserInformation(string jsonMessage)
    {
        Debug.Log("Received user information: " + jsonMessage);

        // Deserialize the JSON string into a UserInformation object
        UserInformation userInfo = JsonUtility.FromJson<UserInformation>(jsonMessage);

        // Update the game server socket manager with the new data
        gameServerSocketManager.UserAccessToken = userInfo.data.accessToken;
        gameServerSocketManager.UserId = userInfo.data.id.ToString();
        gameServerSocketManager.UserNickname = userInfo.data.nickname;
        gameServerSocketManager.UserPictureUrl = userInfo.data.avatar;

        Debug.Log("AccessToken: " + userInfo.data.accessToken);
        Debug.Log("UserId: " + userInfo.data.id);
        Debug.Log("Nickname: " + userInfo.data.nickname);
        Debug.Log("Avatar URL: " + userInfo.data.avatar);
    }

}

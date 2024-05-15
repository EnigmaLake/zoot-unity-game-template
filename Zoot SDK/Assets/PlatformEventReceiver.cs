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

    // Method to handle currency event from JavaScript
    public void HandleGetUserCurrency(string message)
    {
        Debug.Log("Received currency info: " + message);
        // Add your handling code here (parse message, update UI, etc.)
    }

    public void HandleUserBalance(string message)
    {
        Debug.Log("Received user balance: " + message);
    }

    public void HandleExpandedGameView(string message)
    {
        Debug.Log("Received expanded game view command: " + message);
    }

    public void HandleUserInformation(string message)
    {
        Debug.Log("Received user information: " + message);

        gameServerSocketManager.UserAccessToken = "test-1234567";
        gameServerSocketManager.UserId = "123";
    }

}

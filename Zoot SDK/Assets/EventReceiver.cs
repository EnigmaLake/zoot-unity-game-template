using UnityEngine;
using System.Runtime.InteropServices;

public class EventReceiver : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void SetupMessageEventListeners();

    void Start()
    {
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

    public void HandleUserInformation(string message)
    {
        Debug.Log("Received user information: " + message);
    }

    public void HandleExpandedGameView(string message)
    {
        Debug.Log("Received expanded game view command: " + message);
    }
}

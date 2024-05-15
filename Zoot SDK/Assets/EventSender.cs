using UnityEngine;
using System.Runtime.InteropServices;

public class EventSender : MonoBehaviour
{


    [DllImport("__Internal")]
    private static extern void apple();

    [DllImport("__Internal")]
    private static extern void getUserBalanceEvent();

    [DllImport("__Internal")]
    private static extern void getUserCurrencyEvent();

    [DllImport("__Internal")]
    private static extern void sendSetUserCurrencyEvent();

    [DllImport("__Internal")]
    private static extern void sendSetGameRoundUuidEvent();

    [DllImport("__Internal")]
    private static extern void getUserInformationEvent();

    [DllImport("__Internal")]
    private static extern void loginUserEvent();

    [DllImport("__Internal")]
    private static extern void purchaseCoinsEvent();

    [DllImport("__Internal")]
    private static extern void showNotificationEvent();

    [DllImport("__Internal")]
    private static extern void toggleGameViewEvent();

    [DllImport("__Internal")]
    private static extern void getGameViewEvent();

    [DllImport("__Internal")]
    private static extern void requestInitData();

    void Start()
    {
        requestInitData();
    }
}

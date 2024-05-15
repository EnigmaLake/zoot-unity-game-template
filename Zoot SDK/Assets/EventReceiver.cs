using UnityEngine;
using System.Runtime.InteropServices;

public class EventReceiver : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void Apple();

    [DllImport("__Internal")]
    private static extern void SetupMessageEventListeners();

    void Start()
    {

        Apple();

        SetupMessageEventListeners();
    }
}

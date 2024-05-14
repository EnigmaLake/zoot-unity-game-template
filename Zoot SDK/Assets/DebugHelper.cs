using UnityEngine;

public static class DebugHelper
{
    public static void LogObject(object obj)
    {
        if (obj is UnityEngine.Object)
        {
            Debug.Log(obj);  // Use Unity's built-in ToString()
        }
        else
        {
            string json = JsonUtility.ToJson(obj, true);
            if (!string.IsNullOrEmpty(json))
                Debug.Log(json);
            else
                Debug.Log(obj.ToString());  // Fallback if not serializable
        }
    }
}
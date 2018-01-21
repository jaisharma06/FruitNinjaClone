using UnityEngine;

public class ApplicationData{
    public int lives;
    public int timer;
}

public static class JsonManager
{
    public static T GetData<T>(string data)
    {
        var result = JsonUtility.FromJson<T>(data);
        return result;
    }

    public static string GetJson<T>(T token)
    {
        var result = JsonUtility.ToJson(token);
        return result;
    }
}


public static class DirectLog
{
    public static void Debug(this string str)
    {
        UnityEngine.Debug.Log(str);
    }
    public static void Debug(this string str, string hexColorCode)
    {
        UnityEngine.Debug.LogFormat("<color=#{0}>{1}</color>", hexColorCode, str);
    }
}
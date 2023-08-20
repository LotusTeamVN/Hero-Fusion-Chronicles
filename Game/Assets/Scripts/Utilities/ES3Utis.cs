public static class ES3Utis
{
    public static T GetKey<T>(string key)
    {
        if (ES3.KeyExists(key))
            return ES3.Load<T>(key);
        return default(T);
    }

    public static bool HasKey(string key)
    {
        return ES3.KeyExists(key);
    }

    public static void SetKey<T>(string key, T obj)
    {
        ES3.Save<T>(key, obj);
    }
}

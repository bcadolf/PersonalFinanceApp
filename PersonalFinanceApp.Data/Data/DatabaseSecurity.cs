

using System.Diagnostics;


namespace PersonalFinanceApp.Data.Data;

public static class DatabaseSecurity
{
    private const string EncyptionKeyId = "db_encryption_key";
    private const string EncryptedFlag = "has_converted_to_encrytption";

    public static async Task<string> GetOrGenerateDbKey(string dbPath)
    {
        // var key = await SecureStorage.Default.GetAsync(EncyptionKeyId);
        var key = "testing";
        bool hasConverted = Preferences.Default.Get(EncryptedFlag, false);
        Debug.WriteLine($"key {key}");
        if (!hasConverted)
        {
            key = Guid.NewGuid().ToString("N");
            // await SecureStorage.Default.SetAsync(EncyptionKeyId, key);
            Debug.WriteLine($"new key {key}");
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            Preferences.Default.Set(EncryptedFlag, true);
        }

        if (string.IsNullOrEmpty(key))
        {
            throw new Exception("Security Error: Database key is missing.");
        }
        
        return key;
    }

    public static string GetKeySync(string dbPath)
    {
        Debug.WriteLine("made it to getkeysync");
        return Task.Run(async () => await GetOrGenerateDbKey(dbPath)).Result;
    }
}
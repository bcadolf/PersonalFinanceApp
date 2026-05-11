

using System.Diagnostics;


namespace PersonalFinanceApp.Data.Data;

public static class DatabaseSecurity
{
    private const string EncyptionKeyId = "db_encryption_key";

    public static async Task<string> GetOrGenerateDbKey()
    {
        string? key = string.Empty;

        try
        {
            Debug.WriteLine("tried secure storage");
            key = await SecureStorage.Default.GetAsync(EncyptionKeyId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SecureStorage failed: {ex.Message}");
        }

        if (!string.IsNullOrEmpty(key))
        {
            return key;
        }

        
        
        key = Guid.NewGuid().ToString("N");
        try
        {
            await SecureStorage.Default.SetAsync(EncyptionKeyId, key);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"SecureStorage set failed: {ex.Message}");
            key = "dont-use-in-prod";
        }
   
        
        
        return key;
    }

    public static string GetKeySync()
    {
        return Task.Run(async () => await GetOrGenerateDbKey()).GetAwaiter().GetResult();
    }
}
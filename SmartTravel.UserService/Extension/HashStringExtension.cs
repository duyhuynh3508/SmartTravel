using static System.Net.Mime.MediaTypeNames;

namespace SmartTravel.UserService.Extension
{
    public static class HashStringExtension
    {
        public static string ToHashString(string value)
        {
            if(string.IsNullOrEmpty(value))
                return string.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(value);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
    }
}

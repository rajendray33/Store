using System.Text;

namespace StoreManagment.Models
{
    public class Common
    {
        public enum UserRole
        {
            Employee,
            Admin
        }
        public static string? EncryptPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = Encoding.ASCII.GetBytes(Password);
                string EncryptPass = Convert.ToBase64String(storePassword);
                return EncryptPass;
            }

        }

        //Decrypt Pasword  Method
        public static string DecryptPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return null;
            }
            else
            {
                try
                {
                    byte[] EncryptPassword = Convert.FromBase64String(Password);
                    string DecryptPass = Encoding.UTF8.GetString(EncryptPassword);
                    return DecryptPass;
                }
                catch (Exception ex)
                {
                    // Handle decryption errors if any
                    Console.WriteLine($"Error decrypting password: {ex.Message}");
                    return null; // Return null in case of decryption error
                }
            }
        }
    }

}

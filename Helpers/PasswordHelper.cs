using System.Security.Cryptography;
using System.Text;

namespace SistemacotizacionprestamosAPI.Helpers
{
    public class PasswordHelper
    {
        public static string GenerarHash(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));

                StringBuilder sb = new StringBuilder();

                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }
    }
}
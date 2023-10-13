using System;
using System.Security.Cryptography;
using System.Text;

namespace AED_BE.Utils
{
	public class GenericHasher
	{
		public GenericHasher()
		{
		}

		public static string ComputeHash(String password) {

            using var sha256 = SHA256.Create();
            var byteValue = Encoding.UTF8.GetBytes(password);
			var byteHash = sha256.ComputeHash(byteValue);
            var hash = Convert.ToBase64String(byteHash);
			return hash.ToString();

        }
	}
}


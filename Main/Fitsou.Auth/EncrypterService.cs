using System.Security.Cryptography;
using System.Text;

namespace Auth;

public class EncrypterService
{
    public static string GeneratePasswordHash(string password)
    {
        int iterationCount = 10000; // Number of iterations for the key derivation function
        int saltSize = 16; // Size of the salt in bytes
        int hashSize = 64; // Size of the output hash in bytes

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = new byte[saltSize];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes); // Generate a random salt
        }

        byte[] hashBytes;
        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, iterationCount))
        {
            pbkdf2.IterationCount = iterationCount;
            hashBytes = pbkdf2.GetBytes(hashSize); // Generate the hashed password
        }

        byte[] saltedHashBytes = new byte[hashSize + saltSize]; // Combine the hash and salt into a single byte array
        Array.Copy(hashBytes, 0, saltedHashBytes, 0, hashSize);
        Array.Copy(saltBytes, 0, saltedHashBytes, hashSize, saltSize);

        return Convert.ToBase64String(saltedHashBytes); // Return the Base64-encoded salted hash
    }

    private static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the password and salt strings to byte arrays
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            // Combine the password and salt byte arrays
            byte[] passwordAndSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, passwordAndSaltBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, passwordAndSaltBytes, passwordBytes.Length, saltBytes.Length);

            // Compute the hash value of the combined byte array
            byte[] hashBytes = sha256.ComputeHash(passwordAndSaltBytes);

            // Convert the hash byte array to a base64-encoded string
            string hashString = Convert.ToBase64String(hashBytes);

            return hashString;
        }
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }
        }

        return true;
    }
}
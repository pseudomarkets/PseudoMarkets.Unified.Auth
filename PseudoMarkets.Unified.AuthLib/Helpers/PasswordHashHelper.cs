using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

/*
 * Pseudo Markets Unified Web API
 * Password Hasher
 * Modified from Microsoft Documentation:
 * https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-3.1
 * Author: Shravan Jambukesan <shravan@shravanj.com>
 * (c) 2019 - 2020 Pseudo Markets
 */

namespace PseudoMarkets.Unified.AuthLib.Helpers
{
    public class PasswordHashHelper
    {
        public static string GetHash(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
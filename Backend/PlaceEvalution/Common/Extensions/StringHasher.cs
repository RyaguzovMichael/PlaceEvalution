using System.Security.Cryptography;

namespace PlaceEvolution.API.Common.Extensions;

public static class StringHasher
{
    private const int SaltLength = 0x10;
    private const int KeyLength = 0x20;
    private const int CountOfCryptoIterations = 1000;

    public static string Hash(this string entity)
    {
        using Rfc2898DeriveBytes bytes = new(entity, SaltLength, CountOfCryptoIterations, HashAlgorithmName.SHA256);
        byte[] salt = bytes.Salt;
        byte[] buffer = bytes.GetBytes(KeyLength);

        byte[] result = new byte[SaltLength + KeyLength + 1];
        Buffer.BlockCopy(salt, 0, result, 1, SaltLength);
        Buffer.BlockCopy(buffer, 0, result, SaltLength + 1, KeyLength);

        return Convert.ToBase64String(result);
    }

    public static bool CompareHashedString(this string hashedString, string entity)
    {
        byte[] src = Convert.FromBase64String(hashedString);
        if (src.Length != SaltLength + KeyLength + 1 || src[0] != 0)
        {
            return false;
        }

        byte[] salt = new byte[SaltLength];
        Buffer.BlockCopy(src, 1, salt, 0, SaltLength);

        byte[] hashedStringBuffer = new byte[KeyLength];
        Buffer.BlockCopy(src, SaltLength + 1, hashedStringBuffer, 0, KeyLength);

        using Rfc2898DeriveBytes bytes = new(entity, salt, CountOfCryptoIterations, HashAlgorithmName.SHA256);
        byte[] entityBuffer = bytes.GetBytes(KeyLength);

        return ByteArraysEqual(hashedStringBuffer, entityBuffer);
    }

    private static bool ByteArraysEqual(byte[] buffer1, byte[] buffer2)
    {
        if (buffer1.Length != buffer2.Length) return false;
        for (int i = 0; i < buffer1.Length; i++)
        {
            if (buffer1[i] != buffer2[i]) return false;
        }

        return true;
    }
}
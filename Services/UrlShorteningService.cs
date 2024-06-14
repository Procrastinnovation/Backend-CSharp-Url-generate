using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JLimUrl.Services
{
    public class UrlShorteningService
    {
        private const int NumberOfCharsInShortLink = 8;
        private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        
        private readonly Random _random = new();

        public string GenerateUniqueCode()
        {
            var codeChars = new char[NumberOfCharsInShortLink];

            for (var i = 0; i < NumberOfCharsInShortLink; i++)
            {
                var randomIndex = _random.Next(Alphabet.Length - 1);

                codeChars[i] = Alphabet[randomIndex];
            }

            var code = new string(codeChars);

            return code;
        }

    }
}
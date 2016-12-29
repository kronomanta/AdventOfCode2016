using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Day14
{
    class Hasher
    {
        private Regex _threeReg = new Regex(@"(.)\1\1", RegexOptions.Compiled);
        private MD5 _md5 = MD5.Create();
        private readonly Encoding enc = Encoding.ASCII;
        private readonly string _salt;
        private Dictionary<int, string> generatedKeys;
        public Hasher(string salt)
        {
            _salt = salt;
        }

        public int Part01()
        {
            return GetKeyIndex(multiplyHash: false);
        }

        public int Part02()
        {
            return GetKeyIndex(multiplyHash: true);
        }

        public int GetKeyIndex(bool multiplyHash)
        {
            generatedKeys = new Dictionary<int, string>();
            int keyCnt = 0;
            int index = 0;
            //require 64 keys
            while(keyCnt < 64)
            {
                if (IsKey(index, multiplyHash) && ++keyCnt == 64)
                {
                    break;
                }

                index++;
            }

            return index;
        }

        private string CalculateHash(int index, bool multiplyHash)
        {
            if (!generatedKeys.ContainsKey(index))
            {
                string hash = _md5.ComputeHash(enc.GetBytes(_salt + index)).ToHex();
                if (multiplyHash)
                {
                    for (int i = 0; i < 2016; i++)
                        hash = _md5.ComputeHash(enc.GetBytes(hash)).ToHex();
                }

                generatedKeys.Add(index, hash);
            }

            return generatedKeys[index];
        }

        private bool IsKey(int index, bool multiplyHash)
        {
            Match match = _threeReg.Match(CalculateHash(index, multiplyHash));
            if (!match.Success) return false;

            string pattern = match.Groups[0].Value;
            pattern += pattern[0].ToString() + pattern[0];

            for (int i = index+1; i <= index + 1000; i++)
            {
                if (CalculateHash(i, multiplyHash).Contains(pattern))
                    return true;
            }

            return false;
        }

        
    }
}

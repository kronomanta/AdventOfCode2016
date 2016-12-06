using System.Text;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            string doorId = "uqwqemis";
            int counter = 3231929;


            char[] password = new char[8];
            int lc = 0;
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                while (lc < 8)
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(doorId + counter++);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Convert the byte array to hexadecimal string
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }

                    string hash = sb.ToString();
                    if (hash.StartsWith("00000") && char.IsDigit(hash[5]))
                    {
                        //five leading zeros
                        int pos = int.Parse(hash[5].ToString());
                        if (0 <= pos && pos <= 7 && password[pos] == '\0')
                        {
                            lc++;

                            password[pos] = hash[6];
                            System.Console.WriteLine("partial password: {0}, counter: {1}", string.Join("", password), counter);
                        }

                    }

                }

                System.Console.WriteLine(string.Join("", password));
                System.Console.ReadKey();
            }
        }
    }
}

namespace Day14
{
    static class Extension
    {
        public static string ToHex(this byte[] data)
        {
            char[] c = new char[data.Length * 2];

            byte b;
            for (int bx = 0, cx = 0; bx < data.Length; ++bx, ++cx)
            {
                b = ((byte)(data[bx] >> 4));
                c[cx] = (char)(b > 9 ? b - 10 + 'a' : b + '0');

                b = ((byte)(data[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b - 10 + 'a' : b + '0');
            }

            return new string(c, 0, c.Length);
        }
    }
}

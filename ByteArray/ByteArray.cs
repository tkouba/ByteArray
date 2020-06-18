using System;



public static class ByteArray
{
    public static readonly byte[] Empty = new byte[0];

    public static bool IsEmpty(byte[] array)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        return array.Length == 0;
    }

    public static bool IsNullOrEmpty(byte[] array)
    {
        return array == null || array.Length == 0;
    }

    public static byte[] Parse(string byteString)
    {
        if (String.IsNullOrWhiteSpace(byteString))
            return Empty;

        byte[] buffer = null;
        int separator = 0; // No separator    

        if (byteString.IndexOf('-') >= 0)
            separator = (int)'-';

        if (byteString.IndexOf(':') >= 0)
            separator = (int)':';

        if (separator == 0)
        {
            if (byteString.Length % 2 != 0)  //should be even
                throw new FormatException();
            buffer = new byte[byteString.Length / 2];
        }
        else
        {
            if ((byteString.Length + 1) % 3 != 0)  //should be modulo 3 
                throw new FormatException();
            buffer = new byte[(byteString.Length + 1) / 3];
        }

        int charPosition = 0;
        int bufferIndex = 0;

        for (int i = 0; i < byteString.Length; i++)
        {
            int value = (int)byteString[i];

            if (value >= 0x30 && value <= 0x39) // 0-9
            {
                value -= 0x30;
            }
            else if (value >= 0x41 && value <= 0x46) // A-F
            {
                value -= 0x37;
            }
            else if (value >= 0x61 && value <= 0x66) // a-f
            {
                value -= 0x57;
            }
            else if (separator != 0 && value == separator)
            {
                if (charPosition == 0)
                {
                    continue;
                }
                else
                {
                    throw new FormatException(); // Expected separator missing
                }
            }
            else
            {
                throw new FormatException(); // Unknown character
            }

            if (separator != 0 && charPosition >= 2)
                throw new FormatException(); // Too many characters after separator

            if (charPosition == 0)
            {
                buffer[bufferIndex] = (byte)(value << 4);
                charPosition++;
            }
            else
            {
                buffer[bufferIndex++] |= (byte)value;
                charPosition = 0;
            }
        }
        return buffer;
    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Conversion
{
	public static class TypeConverter
	{
		public static int DateToIntegerAsSecond(DateTime? date)
		{
			int dateInteger = 0;
			try
			{
				DateTimeOffset dateOffset = new DateTimeOffset(date.Value);
				dateInteger = (int)dateOffset.ToUnixTimeSeconds();
			}
			catch(Exception exp)
			{
			}
			return dateInteger;
		}

		public static int DateToIntegerAsMilliSecond(DateTime date)
		{
			int dateInteger = 0;
			try
			{
				DateTimeOffset dateOffset = new DateTimeOffset(date);
				dateInteger = (int)dateOffset.ToUnixTimeMilliseconds();
			}
			catch (Exception exp)
			{
			}
			return dateInteger;
		}

        public static byte[] HexToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];

            try
            {
                for (int i = 0; i < length; i += 2)
                {
                    byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                }
            }
            catch (Exception ex)
            {

            }

            return byteArray;
        }

		public static byte[] StringToByteArray(string input)
        {
            // Handle sign
            bool isNegative = false;
            if (input.StartsWith("-"))
            {
                isNegative = true;
                input = input.Substring(1);
            }

            // Convert the absolute value to bytes using UTF-8 encoding
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            // Optionally, add a sign byte if needed
            if (isNegative)
            {
                byte[] signByte = Encoding.UTF8.GetBytes("-");
                byteArray = ConcatByteArrays(signByte, byteArray);
            }

            return byteArray;
        }

        public static byte[] StringToByteArray(string input, int length)
        {
            //string inputString = "Hello";
            //int fixedSize = 10; // Desired fixed size in bytes

            // Convert the string to a byte array using UTF-8 encoding
            byte[] byteArray = Encoding.UTF8.GetBytes(input);

            // Truncate or pad the byte array to the fixed size
            Array.Resize(ref byteArray, length);

            // If the string is shorter than the fixed size, pad it with zeros
            if (byteArray.Length < length)
            {
                Array.Resize(ref byteArray, length);
            }

            // If the string is longer than the fixed size, truncate it
            if (byteArray.Length > length)
            {
                Array.Resize(ref byteArray, length);
            }
            return byteArray;
        }


        public static byte[] ConcatByteArrays(byte[] array1, byte[] array2)
        {
            byte[] result = new byte[array1.Length + array2.Length];
            Buffer.BlockCopy(array1, 0, result, 0, array1.Length);
            Buffer.BlockCopy(array2, 0, result, array1.Length, array2.Length);
            return result;
        }
    }
}

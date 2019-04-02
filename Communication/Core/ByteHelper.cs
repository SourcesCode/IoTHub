using System;
using System.Globalization;
using System.Text;

namespace Communication.Core
{
    public class ByteHelper
    {
        public static void Test()
        {
            byte[] data = { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            //byteToHexStr(data);
            int length = 5;
            var s3 = ToHexString(data);
            var s4 = ToHexString(data, 0, length);
            string input = "Hello World!";
            var sh = ToHexStringFromString(input);
            var sh1 = ToHexStringFromString2(input);
            string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
            var hs = ToStringFromHexString(hexValues);
            var hs1 = ToStringFromHexString2(hexValues);
            var hexValues2 = "66613030304239353133";
            var vbytes = ToBytesFromHexString(hexValues2);
            var hs2 = ToHexString(vbytes);

        }

        /// <summary>
        /// 字节数组转换成十六进制字符串
        /// 48-65-6C-6C-6F
        /// 48656C6C6F
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        /// <param name="isHasSeparator">是否包含分隔符'-'</param>
        /// <returns></returns>
        public static string ToHexString(byte[] buffer, bool isHasSeparator = false)
        {
            //byte[] data = { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            //int length = 5;
            //默认有分隔符
            string result = BitConverter.ToString(buffer, 0, buffer.Length);
            if (isHasSeparator == false)
            {
                result = result.Replace("-", "");
            }
            return result;
        }

        /// <summary>
        /// 字节数组转换成十六进制字符串
        /// </summary>  
        /// <param name="buffer">要转换的字节数组</param>  
        /// <returns>转换后的字符串</returns>  
        public static string ToHexString(byte[] buffer, int index, int count)
        {
            //byte[] data = { 0x48, 0x65, 0x6C, 0x6C, 0x6F };
            //int length = 5;
            int length = buffer.Length - index;
            if (count > length)
            {
                return string.Empty;
            }
            length = index + count;
            StringBuilder sb = new StringBuilder();
            for (int i = index; i < length; i++)
            {
                sb.Append(Convert.ToString(buffer[i], 16).PadLeft(2, '0'));
                //var temp = buffer[i].ToString("X2");
            }
            string result = sb.ToString().ToUpper();
            return result;
        }

        public static string ToASCIIString(byte[] buffer)
        {
            return ToASCIIString(buffer, 0, buffer.Length);
        }

        public static string ToASCIIString(byte[] buffer, int index, int count)
        {
            return ToEncodingString(buffer, index, count, Encoding.ASCII);
        }

        public static string ToEncodingString(byte[] buffer, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetString(buffer);
        }

        public static string ToEncodingString(byte[] buffer, int index, int count, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding.GetString(buffer, index, count);
        }

        public static string ToBase64String(byte[] buffer)
        {
            return Convert.ToBase64String(buffer);
        }

        public static byte[] ToBytesFromBase64String(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        /// <summary>
        /// 十六进制字符串转换成字节数组
        /// 66613030304239353133
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToBytesFromHexString(string hexString)
        {
            //hexString = hexString.Replace(" ", "");
            //hexString = hexString.Replace("-", "");
            if (hexString.Length <= 0) return new byte[0];
            if (hexString.Length % 2 != 0) return new byte[0];
            byte[] vBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
                if (!byte.TryParse(hexString.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return vBytes;
        }

        public static byte[] ToBytesFromUtf8EncodingString(string utf8EncodingString)
        {
            return ToBytesFromEncodingString(utf8EncodingString, Encoding.UTF8);
        }

        public static byte[] ToBytesFromEncodingString(string encodingString, Encoding encoding)
        {
            var resultBytes = encoding.GetBytes(encodingString);
            return resultBytes;
        }

        /// <summary>
        /// 普通字符串转换成十六进制字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHexStringFromString(string str)
        {
            var bytes = Encoding.Default.GetBytes(str);
            var hexString = BitConverter.ToString(bytes);
            return hexString.Replace("-", "");
        }

        /// <summary>
        /// 十六进制字符串转换成普通字符串
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static string ToStringFromHexString(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            hexString = hexString.Replace("-", "");
            if (hexString.Length <= 0) return "";
            byte[] vBytes = ToBytesFromHexString(hexString);
            return Encoding.Default.GetString(vBytes);
        }

        /// <summary>
        /// 输出 string 中的每个字符的十六进制值。
        /// Hexadecimal value of H is 48 
        /// Hexadecimal value of e is 65 
        /// Hexadecimal value of l is 6C
        /// Hexadecimal value of l is 6C
        /// Hexadecimal value of o is 6F
        /// </summary>
        public static string ToHexStringFromString2(string str)
        {
            //string input = "Hello World!";
            StringBuilder sb = new StringBuilder();
            char[] values = str.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                //也可以,只是会小写
                //var hexOutput = Convert.ToString(letter, 16);
                sb.Append(hexOutput);
                Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 分析十六进制值的 string 并输出对应于每个十六进制值的字符
        /// 首先，它调用 Split(array<Char>[]()[]) 方法以获取每个十六进制值作为数组中的单个 string。然后调用 ToInt32(String, Int32) 以将十六进制转换为表示为 int 的十进制值。示例中演示了用于获取对应于该字符代码的字符的两种不同方法。第一种方法是使用 ConvertFromUtf32(Int32)，它将对应于整型参数的字符作为 string 返回。第二种方法是将 int 显式转换为 char。
        /// 48 65 6C 6C 6F 20 57 6F 72 6C 64 21
        /// hexadecimal value = 48, int value = 72, char value = H or H 
        /// hexadecimal value = 65, int value = 101, char value = e or e
        /// hexadecimal value = 6C, int value = 108, char value = l or l
        /// hexadecimal value = 6C, int value = 108, char value = l or l
        /// hexadecimal value = 6F, int value = 111, char value = o or o
        /// 
        /// </summary>
        public static string ToStringFromHexString2(string hexString)
        {
            //string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
            string[] hexValuesSplit = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            byte[] vBytes = new byte[hexValuesSplit.Length];
            for (int i = 0; i < hexValuesSplit.Length; i++)
            {
                vBytes[i] = Convert.ToByte(hexValuesSplit[i], 16);
                // Convert the number expressed in base-16 to an integer.
                int value = Convert.ToInt32(hexValuesSplit[i], 16);
                // Get the character corresponding to the integral value.
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                hexValuesSplit[i], value, stringValue, charValue);
            }
            return ASCIIEncoding.Default.GetString(vBytes);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace CryptoCheck.Classes
{
    static class Gost28147_89
    {

        private static readonly byte[,] SBox = new byte[8, 16]
        {
            { 0xA, 0x9, 0xD, 0x6, 0xE, 0xB, 0x4, 0x5, 0xF, 0x1, 0x3, 0xC, 0x7, 0x0, 0x8, 0x2 },
            { 0x8, 0x0, 0xC, 0x4, 0x9, 0x6, 0x7, 0xB, 0x2, 0x3, 0x1, 0xF, 0x5, 0xE, 0xA, 0xD },
            { 0xF, 0x6, 0x5, 0x8, 0xE, 0xB, 0xA, 0x4, 0xC, 0x0, 0x3, 0x7, 0x2, 0x9, 0x1, 0xD },
            { 0x3, 0x8, 0xD, 0x9, 0x6, 0xB, 0xF, 0x0, 0x2, 0x5, 0xC, 0xA, 0x4, 0xE, 0x1, 0x7 },
            { 0xF, 0x8, 0xE, 0x9, 0x7, 0x2, 0x0, 0xD, 0xC, 0x6, 0x1, 0x5, 0xB, 0x4, 0x3, 0xA },
            { 0x2, 0x8, 0x9, 0x7, 0x5, 0xF, 0x0, 0xB, 0xC, 0x1, 0xD, 0xE, 0xA, 0x3, 0x6, 0x4 },
            { 0x3, 0x8, 0xB, 0x5, 0x6, 0x4, 0xE, 0xA, 0x2, 0xC, 0x1, 0x7, 0x9, 0xF, 0xD, 0x0 },
            { 0x1, 0x2, 0x3, 0xE, 0x6, 0xD, 0xB, 0x8, 0xF, 0xA, 0xC, 0x5, 0x7, 0x9, 0x0, 0x4 }
        };

        /// <summary> Шифрование сообщения </summary>
        /// <param name="message"> Кодируемое сообщение </param>
        /// <param name="key"> Ключ шифрования сообщения (строка из 32 символов) </param>
        /// <returns> Возвращает зашифрованное сообщение в формате string </returns>
        public static byte[] Encrypt(byte[] message, byte[] key256)
        {
            List<byte> crypted = new List<byte>();
            byte[] crypt;
            UInt32 N1, N2;
            UInt32[] key32 = Block256to32(key256);
            int length = message.Length % 8 == 0 ? message.Length : message.Length + (8 - (message.Length % 8));
            for (int i = 0; i < length; i += 8)
            {
                N1 = (UInt32)(Blocks8to64(message, i) >> 32);
                N2 = (UInt32)Blocks8to64(message, i);
                for (int j = 0; j < 24; j++)
                    FeistelRound(ref N1, ref N2, key32, j);
                for (int j = 31; j > 23; j--)
                    FeistelRound(ref N1, ref N2, key32, j);
                crypt = Block64to8(Block32to64(N1, N2));
                foreach (byte x in crypt) crypted.Add(x);
            }
            return crypted.ToArray();
        }

        /// <summary> Расшифрование сообщения </summary>
        /// <param name="message"> Раскодируемое сообщение </param>
        /// <param name="key"> Ключ расшифровки сообщения (строка из 32 символов) </param>
        /// <returns> Возвращает расшифрованное сообщение в формате string </returns>
        public static byte[] Decrypt(byte[] message, byte[] key256)
        {
            List<byte> decrypted = new List<byte>();
            byte[] decrypt;
            UInt32 N1, N2;
            UInt32[] key32 = Block256to32(key256);
            int length = message.Length % 8 == 0 ? message.Length : message.Length + (8 - (message.Length % 8));
            for (int i = 0; i < length; i += 8)
            {
                N1 = (UInt32)(Blocks8to64(message, i) >> 32);
                N2 = (UInt32)Blocks8to64(message, i);
                for (int j = 0; j < 8; j++)
                    FeistelRound(ref N1, ref N2, key32, j);
                for (int j = 31; j > 7; j--)
                    FeistelRound(ref N1, ref N2, key32, j);
                decrypt = Block64to8(Block32to64(N1, N2));
                foreach (byte x in decrypt) decrypted.Add(x);
            }
            return decrypted.ToArray();
        }

        public static byte[] GetKey(string password)
        {
            var MD5 = (HashAlgorithm)CryptoConfig.CreateFromName("MD5");
            byte[] bkey = MD5.ComputeHash(new UTF8Encoding().GetBytes(password));
            string key = BitConverter.ToString(bkey).Replace("-", string.Empty).ToLower();
            return Encoding.Default.GetBytes(key);
        }

        private static void FeistelRound(ref UInt32 N1, ref UInt32 N2, UInt32[] key32, int CNum) //
        {
            UInt32 RoundRes, buf;
            RoundRes = (N1 + key32[CNum % 8]) % UInt32.MaxValue; 
            RoundRes = STable(RoundRes, CNum % 8);
            RoundRes = CycleShiftL(RoundRes, 11);
            buf = N1;
            N1 = RoundRes ^ N2;
            N2 = buf;
        }

        private static UInt32 STable(UInt32 block32, int Cnum)
        {
            byte b4b1, b4b2;
            byte[] blocks4 = Block32to4(block32);
            for (int i = 0; i < 4; i++)
            {
                b4b1 = SBox[Cnum, Convert.ToInt32(blocks4[i] & 0x0f)];
                b4b2 = SBox[Cnum, Convert.ToInt32(blocks4[i] >> 4)];
                blocks4[i] = b4b2;
                blocks4[i] = (byte)((blocks4[i] << 4) | b4b1);
            }
            return Blocks4to32(blocks4);
        }

        private static UInt32[] Block256to32(byte[] key256) // Разбиение ключа на 8 32-битных подключа
        {
            UInt32[] blocs32 = new UInt32[8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                    blocs32[i] = blocs32[i] << 8 | key256[4 * i + j];
            }
            return blocs32;
        }

        private static UInt64 Blocks8to64(byte[] blocks8, int x) // Объединение 8-битных блоков в один 64-битный
        {
            UInt64 block64 = 0;
            for (int i = x; i < x + 8; i++)
            {
                if (i < blocks8.Length)
                    block64 = block64 << 8 | blocks8[i];
                else
                    block64 = block64 << 8 | 0;
            }
            return block64;
        }

        private static byte[] Block32to4(UInt32 block32) // Разбиение 32-битного блока на 8-ми битные блоки
        {                                         // (c# не умеет делать 4-битные блоки)
            byte[] blocks4 = new byte[4];
            for (int i = 0; i < 4; i++)
                blocks4[i] = (byte)(block32 >> (24 - 8 * i));
            return blocks4;
        }

        private static UInt32 Blocks4to32(byte[] blocks4) // Объединение 8-(4-) битных блоков в один 32-битный
        {
            UInt32 block32 = 0;
            for (int i = 0; i < 4; i++)
                block32 = (block32 << 8) | blocks4[i];
            return block32;
        }

        private static UInt64 Block32to64(UInt32 N1, UInt32 N2) // Объединение двух 32-битных блоков в один 64-битный
        {
            UInt64 block64 = N2;
            block64 = block64 << 32 | N1;
            return block64;
        }

        private static byte[] Block64to8(UInt64 block64) // Разбиение 64-битного блока на 8-ми битные блоки
        {
            byte[] blocks8 = new byte[8];
            for (int i = 0; i < 8; i++)
                blocks8[i] = (byte)(block64 >> ((7 - i) * 8));
            return blocks8;
        }

        private static UInt32 CycleShiftL(UInt32 block32, int n) // Циклический сдвиг 32-битного блока влево на n бит
        {
            return (UInt32)((block32 << n) | (block32 >> (32 - n)));
        }
    }
}

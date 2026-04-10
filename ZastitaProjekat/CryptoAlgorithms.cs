using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaProjekat
{
    public static class CryptoAlgorithms
    {

        //RAIL FENCE CIPHER

        public static byte[] RailFenceEncrypt(byte[] input, int rails)
        {
            if (rails <= 1) return input;

            List<byte>[] fence = new List<byte>[rails];
            for (int i = 0; i < rails; i++) fence[i] = new List<byte>();

            int currentRail = 0;
            bool directionDown = false;

            for (int i = 0; i < input.Length; i++)
            {
                fence[currentRail].Add(input[i]);

                if (currentRail == 0 || currentRail == rails - 1)
                    directionDown = !directionDown;

                currentRail += directionDown ? 1 : -1;
            }

            List<byte> result = new List<byte>();
            foreach (var row in fence)
            {
                result.AddRange(row);
            }

            return result.ToArray();
        }

        public static byte[] RailFenceDecrypt(byte[] input, int rails)
        {
            if (rails <= 1) return input;

            int n = input.Length;
            char[,] matrix = new char[rails, n];

            int currentRail = 0;
            bool directionDown = false;

            for (int i = 0; i < n; i++)
            {
                matrix[currentRail, i] = '*';

                if (currentRail == 0 || currentRail == rails - 1)
                    directionDown = !directionDown;

                currentRail += directionDown ? 1 : -1;
            }

            int index = 0;
            byte[,] byteMatrix = new byte[rails, n];

            for (int r = 0; r < rails; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (matrix[r, c] == '*' && index < n)
                    {
                        byteMatrix[r, c] = input[index++];
                    }
                }
            }

            List<byte> result = new List<byte>();
            currentRail = 0;
            directionDown = false;

            for (int i = 0; i < n; i++)
            {
                result.Add(byteMatrix[currentRail, i]);

                if (currentRail == 0 || currentRail == rails - 1)
                    directionDown = !directionDown;

                currentRail += directionDown ? 1 : -1;
            }

            return result.ToArray();
        }

        //XXTEA

        private const uint DELTA = 0x9e3779b9;
        public static byte[] XXTEAEncrypt(byte[] data, byte[] key)
        {
            uint[] dataAsUint = ToUIntArray(data);
            uint[] keyAsUint = ToUIntArray(key);

            if (dataAsUint.Length < 2) return data;

            int n = dataAsUint.Length;
            uint z = dataAsUint[n - 1], y = dataAsUint[0], sum = 0, e;
            int p, q = 6 + 52 / n;

            while (q-- > 0)
            {
                sum += DELTA;
                e = (sum >> 2) & 3;
                for (p = 0; p < n - 1; p++)
                {
                    y = dataAsUint[p + 1];
                    z = dataAsUint[p] += ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (keyAsUint[(p & 3) ^ e] ^ z));
                }
                y = dataAsUint[0];
                z = dataAsUint[n - 1] += ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (keyAsUint[(p & 3) ^ e] ^ z));
            }

            return ToByteArray(dataAsUint);
        }

        public static byte[] XXTEADecrypt(byte[] data, byte[] key)
        {
            uint[] dataAsUint = ToUIntArray(data);
            uint[] keyAsUint = ToUIntArray(key);

            if (dataAsUint.Length < 2) return data;

            int n = dataAsUint.Length;
            uint z, y = dataAsUint[0], sum, e;
            int p, q = 6 + 52 / n;
            sum = (uint)(q * DELTA);

            while (sum != 0)
            {
                e = (sum >> 2) & 3;
                for (p = n - 1; p > 0; p--)
                {
                    z = dataAsUint[p - 1];
                    y = dataAsUint[p] -= ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (keyAsUint[(p & 3) ^ e] ^ z));
                }
                z = dataAsUint[n - 1];
                y = dataAsUint[0] -= ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (keyAsUint[(p & 3) ^ e] ^ z));
                sum -= DELTA;
            }

            return ToByteArray(dataAsUint);
        }

        #region Pomocne Metode za Konverziju

        private static uint[] ToUIntArray(byte[] data)
        {
            int extendedLength = data.Length;
            if (data.Length % 4 != 0)
            {
                extendedLength = data.Length + (4 - (data.Length % 4));
            }

            byte[] extendedData = new byte[extendedLength];
            Array.Copy(data, extendedData, data.Length);

            uint[] result = new uint[extendedLength / 4];
            Buffer.BlockCopy(extendedData, 0, result, 0, extendedLength);
            return result;
        }

        private static byte[] ToByteArray(uint[] data)
        {
            byte[] result = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, result, 0, result.Length);
            return result;
        }
        #endregion

        //CBC
        private const int CBC_BLOCK_SIZE_BYTES = 16;

        public static byte[] XXTEA_CBC_Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            int originalLength = data.Length;
            int paddedLength = originalLength;
            if (originalLength % CBC_BLOCK_SIZE_BYTES != 0)
            {
                paddedLength = originalLength + (CBC_BLOCK_SIZE_BYTES - (originalLength % CBC_BLOCK_SIZE_BYTES));
            }
            byte[] paddedData = new byte[paddedLength];
            Array.Copy(data, paddedData, originalLength);

            byte[] previousBlock = iv;
            byte[] encryptedData = new byte[paddedLength];

            for (int i = 0; i < paddedLength; i += CBC_BLOCK_SIZE_BYTES)
            {
                byte[] currentBlock = new byte[CBC_BLOCK_SIZE_BYTES];
                Array.Copy(paddedData, i, currentBlock, 0, CBC_BLOCK_SIZE_BYTES);

                byte[] blockToEncrypt = XorBlocks(currentBlock, previousBlock);

                byte[] encryptedBlock = XXTEAEncrypt(blockToEncrypt, key);

                Array.Copy(encryptedBlock, 0, encryptedData, i, CBC_BLOCK_SIZE_BYTES);

                previousBlock = encryptedBlock;
            }

            return encryptedData;
        }

        public static byte[] XXTEA_CBC_Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            byte[] previousBlock = iv;
            byte[] decryptedData = new byte[data.Length];

            for (int i = 0; i < data.Length; i += CBC_BLOCK_SIZE_BYTES)
            {
                byte[] currentBlock = new byte[CBC_BLOCK_SIZE_BYTES];
                Array.Copy(data, i, currentBlock, 0, CBC_BLOCK_SIZE_BYTES);

                byte[] decryptedBlock = XXTEADecrypt(currentBlock, key);

                byte[] finalBlock = XorBlocks(decryptedBlock, previousBlock);

                Array.Copy(finalBlock, 0, decryptedData, i, CBC_BLOCK_SIZE_BYTES);

                previousBlock = currentBlock;
            }

            return decryptedData;
        }

        private static byte[] XorBlocks(byte[] block1, byte[] block2)
        {
            byte[] result = new byte[block1.Length];
            for (int i = 0; i < block1.Length; i++)
            {
                result[i] = (byte)(block1[i] ^ block2[i]);
            }
            return result;
        }
    }
}
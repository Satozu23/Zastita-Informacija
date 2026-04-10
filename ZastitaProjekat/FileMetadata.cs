using System;
using System.Text;

namespace ZastitaProjekat
{
    public class FileMetadata
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string CreationTime { get; set; }
        public string EncryptionAlgorithm { get; set; }
        public string HashAlgorithm { get; set; } = "Tiger";
        public string TigerHashSignature { get; set; }


        public string ToJson()
        {
            string cleanName = FileName.Replace("\\", "\\\\").Replace("\"", "\\\"");

            return "{" +
                   $"\"FileName\":\"{cleanName}\"," +
                   $"\"FileSize\":{FileSize}," +
                   $"\"CreationTime\":\"{CreationTime}\"," +
                   $"\"EncryptionAlgorithm\":\"{EncryptionAlgorithm}\"," +
                   $"\"HashAlgorithm\":\"{HashAlgorithm}\"," +
                   $"\"TigerHashSignature\":\"{TigerHashSignature}\"" +
                   "}";
        }

        public static FileMetadata FromJson(string json)
        {
            var meta = new FileMetadata();

            meta.FileName = GetJsonValue(json, "FileName");
            meta.FileSize = long.Parse(GetJsonValue(json, "FileSize", false));
            meta.CreationTime = GetJsonValue(json, "CreationTime");
            meta.EncryptionAlgorithm = GetJsonValue(json, "EncryptionAlgorithm");
            meta.HashAlgorithm = GetJsonValue(json, "HashAlgorithm");
            meta.TigerHashSignature = GetJsonValue(json, "TigerHashSignature");

            return meta;
        }

        private static string GetJsonValue(string json, string key, bool isString = true)
        {
            string searchKey = $"\"{key}\":";
            int startIndex = json.IndexOf(searchKey) + searchKey.Length;

            if (isString)
            {
                startIndex++;
                int endIndex = json.IndexOf("\"", startIndex);
                return json.Substring(startIndex, endIndex - startIndex);
            }
            else
            {
                int endIndexComma = json.IndexOf(",", startIndex);
                int endIndexBrace = json.IndexOf("}", startIndex);
                int endIndex = (endIndexComma == -1) ? endIndexBrace : Math.Min(endIndexComma, endIndexBrace);
                return json.Substring(startIndex, endIndex - startIndex);
            }
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace SaveData
{
    public enum Skins
    {
        Basic,
        Additional
    }

    public interface ISaveable: ISaveData
    {
        public void Save();
    }

    public interface ILoadable
    {
        public void Load();
    }

    public interface ISaveData
    {
        public GameDataElements gameDataElements { get; set; }
    }

    public class GameDataElements
    {
        public int currentStage = 0;
        public List<bool> clearedStages;
        public List<int> latestScores;
        public List<int> highestScores;
        public List<float> latestTimes;
        public List<float> fastestTimes;
        public Skins currentSkin = Skins.Basic;
        public List<Skins> purchasedSkins = new() { Skins.Basic };
        
        public GameDataElements(int stageCount)
        {
            clearedStages = new List<bool>(new bool[stageCount]);
            latestScores = new List<int>(new int[stageCount]);
            highestScores = new List<int>(new int[stageCount]);
            latestTimes = new List<float>(new float[stageCount]);
            fastestTimes = new List<float>(new float[stageCount]);
        }
    }

    public class GameData : ISaveable, ILoadable
    {
        private static string FilePath => Path.Combine(Application.persistentDataPath, "gamedata.dat");
        public GameDataElements gameDataElements { get; set; }

        public void Save()
        {
            var json = JsonUtility.ToJson(gameDataElements);

            var aesUtil = new AesUtil();
            var encrypted = aesUtil.EncryptString(json, out var iv);

            using var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
            fs.WriteByte((byte)iv.Length);
            fs.Write(iv, 0, iv.Length);
            fs.Write(encrypted, 0, encrypted.Length);
        }

        public void Load()
        {
            if (File.Exists(FilePath))
            {
                using var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                var ivLength = fs.ReadByte();
                var iv = new byte[ivLength];
                fs.Read(iv, 0, ivLength);

                var cipher = new byte[fs.Length - 1 - ivLength];
                fs.Read(cipher, 0, cipher.Length);

                var aesUtil = new AesUtil();
                var json = aesUtil.DecryptString(cipher, iv);

                gameDataElements = JsonUtility.FromJson<GameDataElements>(json);
            }
            else
            {
                const string folderPath = "Assets/Resources/StageInfo/";
                var assetPaths = Directory.GetFiles(folderPath, "*.asset", SearchOption.TopDirectoryOnly);

                var cnt = assetPaths.Count();
                gameDataElements = new GameDataElements(cnt);
            }
        }
    }

    public class AesUtil
    {
        private byte[] key = Encoding.UTF8.GetBytes("6UNVwN80SN2eLUSZFiTUM37o8SkKsQ7l");

        public byte[] EncryptString(string plainText, out byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();
            iv = aes.IV;

            using MemoryStream ms = new MemoryStream();
            using CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using StreamWriter sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Close();
            return ms.ToArray();
        }

        public string DecryptString(byte[] cipherText, byte[] iv)
        {
            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            using MemoryStream ms = new MemoryStream(cipherText);
            using CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
    
    public enum HandMode
    {
        OneHand, TwoHand
    }
    
    public class SettingDataElements
    {
        public float totalVolume = 100;
        public float popVolume = 100;
        public float musicVolume = 100;
        public HandMode handMode = HandMode.OneHand;
    }

    public class SettingData
    {
        private static string FilePath => Path.Combine(Application.persistentDataPath, "settingdata.dat");
        public SettingDataElements settingDataElements { get; private set; }

        public SettingData()
        {
            Load();
        }
        
        public void Save()
        {
            var json = JsonUtility.ToJson(settingDataElements);
            File.WriteAllText(FilePath, json);
        }

        private void Load()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    var json = File.ReadAllText(FilePath);
                    settingDataElements = JsonUtility.FromJson<SettingDataElements>(json);
                }
                catch
                {
                    settingDataElements = new SettingDataElements();
                }
            }
            else
            {
                settingDataElements = new SettingDataElements();
            }
        }
    }
}
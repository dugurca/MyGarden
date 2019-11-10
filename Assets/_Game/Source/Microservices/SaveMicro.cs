using System.Collections.Generic;
using System.IO;
using _Game.Source.Gameplay;
using UnityEngine;

namespace _Game.Source.Microservices
{
    public class SaveMicro : IMicro
    {
        public bool LoadedSaveGame { get; private set; }
        private double _coins;
        private List<Plot> _plots;
        
        public void Init()
        {
            string saveFilePath = GetSaveFilePath();
            if (File.Exists(saveFilePath))
            {
                Load(saveFilePath);
                LoadedSaveGame = true;
            }
        }

        private void Load(string path)
        {
            var saveFileLines = File.ReadAllLines(path);
                
            _coins = double.Parse(saveFileLines[0]);
            _plots = new List<Plot>();

            for (int i = 1; i < saveFileLines.Length; i++)
            {
                var jsonStr = saveFileLines[i];
                
                //Should be handled differently like
                //CheckIfValidJSON(jsonStr)
                if (jsonStr.StartsWith("{") && jsonStr.EndsWith("}"))
                {
                    _plots.Add(JsonUtility.FromJson<Plot>(jsonStr));
                }
            }
        }

        public void Save()
        {
            string saveFilePath = GetSaveFilePath();
            using (var sw = new StreamWriter(saveFilePath, false))
            {
                sw.WriteLine(_coins.ToString());
                foreach (var plot in _plots)
                {
                    string jsonStr = JsonUtility.ToJson(plot);
                    Debug.LogWarning(jsonStr);
                    sw.WriteLine(jsonStr);
                }
            }
        }

        public List<Plot> GetPlots()
        {
            return _plots;
        }

        public void SetPlots(List<Plot> plots)
        {
            _plots = plots;
        }

        public double GetCoins()
        {
            return _coins;
        }

        public void AddCoins(double addAmount)
        {
            _coins += addAmount;
        }

        public void RemoveCoins(double remAmount)
        {
            if (_coins >= remAmount)
            {
                _coins -= remAmount;
            }
        }

        private static string GetSaveFilePath()
        {
            //return Application.persistentDataPath + SaveString;
            return Application.dataPath + SaveString;
        }
        
        private const string SaveString = "/SaveGame.garden";
    }
}
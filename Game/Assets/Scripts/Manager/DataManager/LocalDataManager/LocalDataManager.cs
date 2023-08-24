using System;
using System.IO;
using UnityEngine;
using Runtime.Common.Singleton;
using Runtime.Definition;
using UltimateJson;

namespace Runtime.Manager.Data
{
    public partial class LocalDataManager : PersistentMonoSingleton<LocalDataManager>
    {
        #region Properties

        private SavedLocalData SavedLocalData { get; set; }

        #endregion Properties

        #region API Methods

        protected override void Awake()
        {
            base.Awake();
            LoadData();
        }

        #endregion API Methods

        #region Class Methods

        public void ResetData()
        {
            SavedLocalData = new SavedLocalData();
            Save();
        }

        private void LoadData()
        {
            var path = Application.persistentDataPath + Constant.DATA_SAVE_PATH;
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                try
                {
                    text = RijndaelCryptoAlgorithm.Decrypt(text);
                }
                catch (Exception e)
                {
                    File.Delete(path);
                    File.Create(path);
                    ResetData();
#if UNITY_EDITOR
                    Debug.LogError("Exception: " + e.Message);
#endif
                }
                SavedLocalData = JsonObject.Deserialise<SavedLocalData>(text);
            }
            else
            {
                var directoryFolder = Application.persistentDataPath + Constant.DATA_SAVED_FOLDER;
                if (!Directory.Exists(directoryFolder))
                    Directory.CreateDirectory(directoryFolder);
                ResetData();
            }
        }

        private void Save()
        {
            var path = Application.persistentDataPath + Constant.DATA_SAVE_PATH;
            string jsonText = JsonObject.Serialise(SavedLocalData);
            jsonText = RijndaelCryptoAlgorithm.Encrypt(jsonText);
            File.WriteAllText(path, jsonText);
        }

        #endregion Class Methods
    }
}
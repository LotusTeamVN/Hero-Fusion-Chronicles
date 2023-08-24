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

        //DEMO

        #region Class Methods

        public int GetHeroSlotsData()
        {
            return SavedLocalData.heroSlots;
        }
        public void AddHeroSlotsData(int value)
        {
            SavedLocalData.heroSlots += value;
            Save();
        }

        public int GetCountBuyShopGemItemById(string itemId)
        {
            if (SavedLocalData.shopGemDictionary.ContainsKey(itemId))
                return SavedLocalData.shopGemDictionary[itemId];
            else
                return 0;
        }
        public void AddCountBuyShopGemItemById(string itemId)
        {
            if (SavedLocalData.shopGemDictionary.ContainsKey(itemId))
                SavedLocalData.shopGemDictionary[itemId]++;
            else
                SavedLocalData.shopGemDictionary.Add(itemId, 1);
            Save();
        }

        // Test in SummonHeroScreen.cs (UI Scene)
        #endregion Class Methods
    }
}
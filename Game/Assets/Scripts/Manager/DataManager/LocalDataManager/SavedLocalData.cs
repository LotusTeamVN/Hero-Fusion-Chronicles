using System;
using System.Collections.Generic;
using Runtime.Definition;

namespace Runtime.Manager.Data
{
    [Serializable]
    public class SavedLocalData
    {
        #region Members
        //demo
        public int heroSlots;
        public Dictionary<string, int> shopGemDictionary;
        #endregion Members

        #region Class Methods

        public SavedLocalData()
        {
            this.heroSlots = 4;
            this.shopGemDictionary = new();
        }
      

        #endregion Class Methods
    }
}
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public partial class SummonHeroScreen
    {
        #region Members

        [Header("Value")]
        private int _valueCurrentGold;
        private int _valueCurrentRewardGold;
        private int _valueCurrentHeart;
        private int _valueActiveHeroes;
        private int _valueMaxActiveHeroes;
        private int _valueRequiredGoldSummon;
        private Vector2 originalPosMainUISummon;
        private bool isOnSwiping;
        #endregion Members

        #region Class Methods

        partial void SetupData()
        {
            isOnSwiping = false;

            _valueCurrentGold = 300;
            _valueCurrentRewardGold = 5;
            _valueCurrentHeart = 3;
            _valueActiveHeroes = 0;
            _valueMaxActiveHeroes = 3;
            _valueRequiredGoldSummon = 2;
            originalPosMainUISummon = _mainSummonUI.anchoredPosition;
        }


        #endregion Class Methods
    }
}


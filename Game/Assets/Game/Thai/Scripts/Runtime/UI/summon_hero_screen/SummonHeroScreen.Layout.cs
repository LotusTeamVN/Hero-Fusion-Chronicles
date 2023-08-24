using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public partial class SummonHeroScreen
    {
        #region Members

        [Header("Layout")]
        [SerializeField] private TextMeshProUGUI _currentGoldText;
        [SerializeField] private TextMeshProUGUI _currentCurrentRewardGoldText;
        [SerializeField] private TextMeshProUGUI _currentActiveHeroText;
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private RectTransform _mainSummonUI;


        #endregion Members

        #region Class Methods

        partial void InitLayout()
        {
            _valueCurrentGold = 300;
            _valueCurrentRewardGold = 5;
            _valueCurrentHeart = 3;
            _valueActiveHeroes = 0;
            _valueMaxActiveHeroes = 3;
            _valueRequiredGoldSummon = 2;
        }
        private void SetTextCurrentGold()
        {
            _currentGoldText.text = _valueCurrentGold.ToString();
        }
        private void SetTextCurrentRewardGold()
        {
            _currentCurrentRewardGoldText.text = _valueCurrentRewardGold.ToString();
        }
        private void SetTextActiveHero()
        {
            _currentActiveHeroText.text = $"{_valueActiveHeroes}/{_valueMaxActiveHeroes}";
        }

        private void Swipe(bool isDown)
        {
            isOnSwiping = true;
            if (isDown)
            {  
                _mainSummonUI.DOAnchorPosY(0, 0.1f).OnComplete(() => isOnSwiping = false);
            }
            else
            {
                _mainSummonUI.DOAnchorPosY(originalPosMainUISummon.y, 0.1f).OnComplete(() => isOnSwiping = false);
            }

        }
        private void SwipeUp()
        {
            
        }

    }



    #endregion Class Methods
}



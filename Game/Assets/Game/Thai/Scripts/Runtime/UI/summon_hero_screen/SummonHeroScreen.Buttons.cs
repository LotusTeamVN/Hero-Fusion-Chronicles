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

        [Header("Buttons")]
        [SerializeField] private Button _speedUpGameButton;
        [SerializeField] private Button _autoMergeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _tabHeroTakeDameButton;
        [SerializeField] private Button _summonBtton;
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _explandButton;
        [SerializeField] private Button _fightWhenSwipeDownButton;
        #endregion Members

        #region Class Methods

        partial void InitButtons()
        {
            _speedUpGameButton.onClick.AddListener(OnClickSpeedUpGameButton);
            _autoMergeButton.onClick.AddListener(OnClickAutoMergeButton);
            _settingsButton.onClick.AddListener(OnClickSettingsButton);
            _tabHeroTakeDameButton.onClick.AddListener(OnClickTabHeroTakeDameButton);
            _summonBtton.onClick.AddListener(OnClickSummonButton);
            _fightButton.onClick.AddListener(OnClickFightButton);
            _explandButton.onClick.AddListener(OnClickExplandButton);
            _fightWhenSwipeDownButton.onClick.AddListener(OnClickFightButton);
            _fightWhenSwipeDownButton.gameObject.SetActive(false);

        }

        private void OnClickSpeedUpGameButton()
        {
            Debug.Log("speed up game");
        }
        private void OnClickAutoMergeButton()
        {
            Debug.Log("auto merge button");
        }
        private void OnClickSettingsButton()
        {
            Debug.Log("settings button");
        }
        private void OnClickTabHeroTakeDameButton()
        {
            Debug.Log("open tab hero take dame");
            Debug.Log("close tab hero take dame");
        }
        private void OnClickSummonButton()
        {
            Debug.Log("summon button");
        }
        private void OnClickFightButton()
        {
            Debug.Log("fight button");
        }
        private void OnClickExplandButton()
        {
            Debug.Log("explaned button");
        }
        #endregion Class Methods
    }
}


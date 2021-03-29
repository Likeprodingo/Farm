using Assets.Scripts.Enum;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AnalyticsManager : MonoBehaviour
    {
        IYandexAppMetrica _yandexAppMetrica;

        #region Unity

        private void Start()
        {
            _yandexAppMetrica = AppMetrica.Instance;
            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Session_start.ToString());
        }

        #endregion

        #region Public

        public void LogEvent(AnalyticsEnum eventType)
        {
            _yandexAppMetrica.ReportEvent(eventType.ToString());
        }

        public void LogCrossPromotionEvent(CrossPromoEnum crossPromoType)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Cross_promotion_type", crossPromoType.ToString() },
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Cross_promotion_click.ToString(), eventProperty);
        }

        public void LogSettingsLanguageEvent(string languageType)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Settings_language", languageType },
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Settings_language.ToString(), eventProperty);
        }

        public void LogSettingsOnOf(AnalyticsEnum music, bool isOn)
        {
            string actionText = isOn ? "on" : "off";
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"on/off", actionText },
            };

            _yandexAppMetrica.ReportEvent(music.ToString(), eventProperty);
        }

        public void LogHeroClickEvent(string characterName)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Character_name", characterName },
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Heroes_click.ToString(), eventProperty);
        }

        public void LogWeaponsUpgradeEvent(string currentGameLevel, string weaponLevel, string weaponType)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Weapon_lvl" , weaponLevel },
                {"Weapon_type" , weaponType }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Weapons_upgrade.ToString(), eventProperty);
        }

        public void LogGameExtraPerkPanelConfirmEvent(string currentGameLevel, string perkType, bool isGotPerk)
        {
            string actionText = isGotPerk ? "gotPerk" : "skipped";

            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Perk_type" , perkType },
                {"Action" , actionText }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_extraperk_panel_confirm.ToString(), eventProperty);
        }

        public void LogAdsGameExtraPerkSuccessEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_game_extraperk_success.ToString(), eventProperty);
        }

        public void LogAdsPerkRerollSuccessEvent(string currentGameLevel, string waveNumber)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Wave_number", waveNumber }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_perk_reroll_success.ToString(), eventProperty);
        }

        public void LogGamePerkPanelEvent(string currentGameLevel, string perkType, string currentPerkPanelIndex)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Perk_type" , perkType },
                {"Current_perk_panel" , currentPerkPanelIndex }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_perk_panel.ToString(), eventProperty);
        }

        public void LogGameWaveDoneEvent(string currentGameLevel, string passedWave, string waveTimeDuration)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Passed_wave" , passedWave },
                {"Wave_time_duration" , waveTimeDuration }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_wave_done.ToString(), eventProperty);
        }

        public void LogGameBossWaveStartEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_boss_wave_start.ToString(), eventProperty);
        }

        public void LogHeroChooseEvent(string characterName)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Character_name", characterName },
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Heroes_choose.ToString(), eventProperty);
        }

        public void LogStartGameEvent(string currentGameLevel, string characterName, string weaponType, string weaponLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Character_name" , characterName },
                {"Weapon_type" , weaponType },
                {"Weapon_lvl" , weaponLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_start.ToString(), eventProperty);
        }

        public void LogNextLevelEvent(string currentGameLevel, string characterName, string weaponType, string weaponLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Character_name" , characterName },
                {"Weapon_type" , weaponType },
                {"Weapon_lvl" , weaponLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_next_level_start.ToString(), eventProperty);
        }

        public void LogGameWinScreenEvent(string currentGameLevel, string gameTimeDuration, string weaponType,
            string weaponLevel, string firstPerkType, string secondPerkType, string thirdTerkType, string extraPerkType)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Game_ime_duration" , gameTimeDuration },
                {"Weapon_type" , weaponType },
                {"Weapon_lvl" , weaponLevel },
                {"first_perk_type" , firstPerkType },
                {"second_perk_type" , secondPerkType },
                {"third_perk_type" , thirdTerkType },
                {"extra_perk_type" , extraPerkType }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_Win_screen.ToString(), eventProperty);
        }

        public void LogAdsGameWinDoubleClickEvent(int rewardModifier)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Reward_modifier", rewardModifier }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_game_win_double_click.ToString(), eventProperty);
        }

        public void LogAdsGameWinDoubleSuccessEvent(string currentGameLevel, int rewardModifier)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Reward_modifier", rewardModifier }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_game_win_double_success.ToString(), eventProperty);
        }

        public void LogGameContinuePanelShowEvent(string currentGameLevel, string waveNumber, string weaponType, string weaponLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Wave_number" , waveNumber },
                {"Weapon_type" , weaponType },
                {"Weapon_lvl" , weaponLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_continue_panel_show.ToString(), eventProperty);
        }

        public void LogAdsGameContinueSuccessEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_game_continue_success.ToString(), eventProperty);
        }

        public void LogGameLoseScreenEvent(string currentGameLevel, string gameTimeDuration, string weaponType,
            string weaponLevel, string firstPerkType, string secondPerkType, string thirdTerkType, string extraPerkType, bool isWasGameContinued)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Game_ime_duration" , gameTimeDuration },
                {"Weapon_type" , weaponType },
                {"Weapon_lvl" , weaponLevel },
                {"first_perk_type" , firstPerkType },
                {"second_perk_type" , secondPerkType },
                {"third_perk_type" , thirdTerkType },
                {"extra_perk_type" , extraPerkType },
                {"Was_game_continued" , isWasGameContinued }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Game_Lose_screen.ToString(), eventProperty);
        }

        public void LogLoseScreenMenuClickEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Lose_screen_menu_click.ToString(), eventProperty);
        }

        public void LogLoseScreenRestartClickEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Lose_screen_restart_click.ToString(), eventProperty);
        }

        public void LogAdsInterstitialEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_interstitial.ToString(), eventProperty);
        }

        public void LogEnergyOffPanelShowEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Energy_off_panel_show.ToString(), eventProperty);
        }

        public void LogEnergyOffPanelCloseEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Energy_off_panel_close.ToString(), eventProperty);
        }

        public void LogAdsEnergyRechargedSuccessEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_energy_recharged_success.ToString(), eventProperty);
        }

        public void LogShopEnergyForAdClickEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Shop_energy_for_ad_click.ToString(), eventProperty);
        }

        public void LogAdsShopEnergySuccessEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_shop_energy_success.ToString(), eventProperty);
        }

        public void LogAdsBonusGoldMenuSuccessEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Ads_ads_to_gold_confirm.ToString(), eventProperty);
        }

        public void LogBattlePassBuyConfirmEvent(string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Shop_Battle_pass_buy_confirm.ToString(), eventProperty);
        }

        public void LogShopEnergyForGemsEvent(string currentGameLevel, string amount)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Energy_amount" , amount }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Shop_energy_for_gems.ToString(), eventProperty);
        }


        public void LogBattlePassGetRewardEvent(string currentGameLevel, bool isPremiunmReward)
        {
            string battlePassType = isPremiunmReward ? "premium" : "typical";

            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Battle_pass_type" , battlePassType }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Battle_pass_get_reward.ToString(), eventProperty);
        }

        public void LogShopGoldForGemsEvent(string currentGameLevel, string amount)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Gold_amount" , amount }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Shop_gold_for_gems.ToString(), eventProperty);
        }

        public void LogShopBuyClickEvent(AnalyticsEnum type, string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(type.ToString(), eventProperty);
        }

        public void LogShopBuyConfirmEvent(AnalyticsEnum type, string currentGameLevel)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel }
            };

            _yandexAppMetrica.ReportEvent(type.ToString(), eventProperty);
        }

        public void LogTutorialStepCompleteEvent(string tutorailStep)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Tutorail_step", tutorailStep }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Tutorial_step_complete.ToString(), eventProperty);
        }

        public void LogCalendarClaimRewardEvent(string currentGameLevel, string dayNumber)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Day_number" , dayNumber }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Calendar_claim_reward.ToString(), eventProperty);
        }

        public void LogCalendarAdsClaimRewardEvent(string currentGameLevel, string dayNumber)
        {
            Dictionary<string, object> eventProperty = new Dictionary<string, object>()
            {
                {"Current_game_lvl" , currentGameLevel },
                {"Day_number" , dayNumber }
            };

            _yandexAppMetrica.ReportEvent(AnalyticsEnum.Calendar_ads_claim_reward.ToString(), eventProperty);
        }

        #endregion
    }
}
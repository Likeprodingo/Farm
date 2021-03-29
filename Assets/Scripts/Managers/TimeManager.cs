using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class TimeManager : MonoBehaviour
    {
        public Action CalendarTimeDataLoadedAction { get; set; }
        public bool IsCalendarRewardReady { get; private set; } = false;

        [Header("Components")]
        [SerializeField] private PushNotificationManager _pushNotificationManager;

        [Header("Options")]
        [SerializeField] private float _calendarRewardReloadTime = 20000;//86000000 == 1 day;
        [SerializeField] private float _energyRecoveryReloadTime = 30000;//300000 == 5 min;
        //[SerializeField] private float _energyCounterResetTime = 300000;//86000000 == 1 day;
        //[SerializeField] private int _adsWatchMaxCount = 5;

        private float _calendarRewardMilliseconds = 0;
        private float _energyRewardMilliseconds = 0;
        //private float _energyCounterMilliseconds = 0;
        //private float _rewardAdsCounterMilliseconds = 0;
        //private int _energyPerDay = 0;
        //private int _goldPerDay = 0;
        //private int _rewardAdsPerDay = 0;

        private readonly string CALENDAR_IDENTIFIER = "CalendarRewardTime";
        private readonly string ENERGY_IDENTIFIER = "EnergyRecoveryTime";
        //private readonly string ENERGY_COUNT_PER_DAY = "EnergyCountPerDay";
        //private readonly string GOLD_COUNT_PER_DAY = "GoldCountPerDay";
        //private readonly string COUNT_PER_DAY_TIME = "EnergyCounterTime";
        //private readonly string COUNT_PER_DAY_TIME = "RewardAdsCounterTime";
        //private readonly string REWARD_ADS_COUNT_PER_DAY_TIME = "RewardAdsCountPerDayTime";
        //private readonly string REWARD_ADS_PER_DAY = "RewardAdsPerDay";

        #region Public

        public void SetFullEnergyNotification(int energyNeeded)
        {
            float time = energyNeeded * _energyRecoveryReloadTime;
            TimeSpan pushTime = TimeSpan.FromMilliseconds(time);
            _pushNotificationManager.SetupFullEnergyNotification(pushTime.Hours, pushTime.Minutes, pushTime.Seconds);
        }

        public void SetCalendarRewardFullTimeNotification()
        {
            _calendarRewardMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            PlayerPrefs.SetFloat(CALENDAR_IDENTIFIER, _calendarRewardMilliseconds);

            TimeSpan pushTime = TimeSpan.FromMilliseconds(_calendarRewardReloadTime);

            _pushNotificationManager.SetupCalendarRewardNotification(pushTime.Hours, pushTime.Minutes, pushTime.Seconds);

            IsCalendarRewardReady = false;
        }

        public void StartLoadCalendarTimeData()
        {
            LoadCalendarTimeData();
        }

        public int CheckEnergyTimeData()
        {
            if (PlayerPrefs.HasKey(ENERGY_IDENTIFIER))
            {
                _energyRewardMilliseconds = PlayerPrefs.GetFloat(ENERGY_IDENTIFIER, 0f);
            }
            else
            {
                _energyRewardMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                PlayerPrefs.SetFloat(ENERGY_IDENTIFIER, _energyRewardMilliseconds);
            }

            if ((long)_energyRewardMilliseconds != 0)
            {
                long resultTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_energyRewardMilliseconds;

                if (resultTime > (long)_energyRecoveryReloadTime)
                {
                    _energyRewardMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    PlayerPrefs.SetFloat(ENERGY_IDENTIFIER, _energyRewardMilliseconds);

                    return (int)(resultTime / (long)_energyRecoveryReloadTime);
                }
            }

            return 0;
        }

        //public bool CheckEnergyAndGoldCountPerDay(bool isEnergy)
        //{
        //    if (!PlayerPrefs.HasKey(COUNT_PER_DAY_TIME))
        //    {
        //        SetEnergyAndGoldDefaultValues();

        //        if (isEnergy)
        //        {
        //            _energyPerDay--;
        //            SaveEnergyAndGoldData();

        //            return true;
        //        }
        //        else
        //        {
        //            _goldPerDay--;
        //            SaveEnergyAndGoldData();

        //            return true;
        //        }
        //    }

        //    _energyPerDay = PlayerPrefs.GetInt(ENERGY_COUNT_PER_DAY, _adsWatchMaxCount);
        //    _goldPerDay = PlayerPrefs.GetInt(GOLD_COUNT_PER_DAY, _adsWatchMaxCount);

        //    _energyCounterMilliseconds = PlayerPrefs.GetFloat(COUNT_PER_DAY_TIME, 0f);
        //    long resultTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_energyCounterMilliseconds;

        //    if (resultTime > (long)_energyCounterResetTime)
        //    {
        //        SetEnergyAndGoldDefaultValues();
        //    }

        //    if (_energyPerDay > 0 && isEnergy)
        //    {
        //        _energyPerDay--;
        //        SaveEnergyAndGoldData();
        //        return true;
        //    }

        //    if (_goldPerDay > 0 && !isEnergy)
        //    {
        //        _goldPerDay--;
        //        SaveEnergyAndGoldData();
        //        return true;
        //    }

        //    return false;
        //}

        //public bool IsCanWatchRewardAd()
        //{
        //    if (!PlayerPrefs.HasKey(REWARD_ADS_COUNT_PER_DAY_TIME))
        //    {
        //        SetDefaultRewardAdsCount();

        //        _rewardAdsPerDay--;

        //        SaveRewardAdsCount();

        //        return true;
        //    }

        //    _rewardAdsPerDay = PlayerPrefs.GetInt(REWARD_ADS_PER_DAY, _adsWatchMaxCount);

        //    _rewardAdsCounterMilliseconds = PlayerPrefs.GetFloat(REWARD_ADS_COUNT_PER_DAY_TIME, 0f);
        //    long resultTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_rewardAdsCounterMilliseconds;

        //    if (resultTime > (long)_energyCounterResetTime)
        //    {
        //        SetDefaultRewardAdsCount();
        //    }

        //    if (_rewardAdsPerDay > 0)
        //    {
        //        _rewardAdsPerDay--;
        //        SaveRewardAdsCount();

        //        return true;
        //    }

        //    return false;
        //}

        #endregion

        #region Private

        //private void SetDefaultRewardAdsCount()
        //{
        //    _rewardAdsCounterMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //    _rewardAdsPerDay = _adsWatchMaxCount;
        //}

        //private void SaveRewardAdsCount()
        //{
        //    PlayerPrefs.SetFloat(REWARD_ADS_COUNT_PER_DAY_TIME, _rewardAdsCounterMilliseconds);
        //    PlayerPrefs.SetInt(REWARD_ADS_PER_DAY, _rewardAdsPerDay);
        //}

        //private void SaveEnergyAndGoldData()
        //{
        //    PlayerPrefs.SetFloat(COUNT_PER_DAY_TIME, _energyCounterMilliseconds);
        //    PlayerPrefs.SetInt(ENERGY_COUNT_PER_DAY, _energyPerDay);
        //    PlayerPrefs.SetInt(GOLD_COUNT_PER_DAY, _goldPerDay);
        //}

        //private void SetEnergyAndGoldDefaultValues()
        //{
        //    _energyCounterMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //    _energyPerDay = _adsWatchMaxCount;
        //    _goldPerDay = _adsWatchMaxCount;
        //}

        private void CheckCalendarReward()
        {
            if ((long)_calendarRewardMilliseconds != 0)
            {
                long resultTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_calendarRewardMilliseconds;

                if (resultTime > (long)_calendarRewardReloadTime)
                {
                    IsCalendarRewardReady = true;
                }
                else
                {
                    SetCalendarRewardDifferenceTimeNotification();
                }
            }
        }

        private void SetCalendarRewardDifferenceTimeNotification()
        {
            long notificationTime = (long)_calendarRewardReloadTime - (DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_calendarRewardMilliseconds);

            TimeSpan pushTime = TimeSpan.FromMilliseconds(notificationTime);

            _pushNotificationManager.SetupCalendarRewardNotification(pushTime.Hours, pushTime.Minutes, pushTime.Seconds);
        }

        private void LoadCalendarTimeData()
        {
            if (PlayerPrefs.HasKey(CALENDAR_IDENTIFIER))
            {
                _calendarRewardMilliseconds = PlayerPrefs.GetFloat(CALENDAR_IDENTIFIER, 0f);
            }

            CheckCalendarReward();

            CalendarTimeDataLoadedAction?.Invoke();
        }

        #endregion
    }
}
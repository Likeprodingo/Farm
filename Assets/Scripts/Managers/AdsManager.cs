using System;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Data;
using Assets.Scripts.Controllers;

#if UNITY_IOS
using UnityEngine.iOS;
#endif

namespace Assets.Scripts.Managers
{
    public class AdsManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private AnalyticsManager _analyticsManager;
        [SerializeField] private int _shouldShowIntCounter;
        //[SerializeField] private LoadingAdsUIController _loadingAdsUIController;

        [Header("Options")]
        [SerializeField] private float _waitingLimit = 3f;
        [SerializeField] private int _adsWatchMaxCount = 5;
        [SerializeField] private float _adsCounterResetTime = 86000000;//86000000 == 1 day;

        private Action<bool> _onEndVideo;
        private readonly string INT_COUNTER_KEY = "int_ad_counter";

        private string _appKey;
        private int _intCounter = 0;

        private bool _isRewardVideoEnds = false;

        private float _rewardAdsCounterMilliseconds = 0;
        private int _rewardAdsPerDay = 0;

        private readonly string REWARD_ADS_COUNT_PER_DAY_TIME = "RewardAdsCountPerDayTime";
        private readonly string REWARD_ADS_PER_DAY = "RewardAdsPerDay";

        #region Unity

        private void Awake()
        {
            PrepareAppKey();
            _intCounter = PlayerPrefs.GetInt(INT_COUNTER_KEY);
            //_dataManager.BattlePassDataUpdatedAction += OnDataLoaded;
            //_gameManager.GameStartAction += OnGameStart;
        }

        //private void Start()
        //{
        //    Debug.LogError("Start validateIntegration");
        //    IronSource.Agent.validateIntegration();
        //}

        private void OnEnable()
        {
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedAdClosedEvent;
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedAdFailed;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedAdSuccess;
        }

        #endregion

        #region Private

        private void OnGameStart()
        {
            _intCounter++;

            //if (_intCounter > _shouldShowIntCounter)
            //{
            //    Debug.LogError("ResetIntCounter OnGameStart ");
            //    _intCounter = 0;
            //}

            PlayerPrefs.SetInt(INT_COUNTER_KEY, _intCounter);
        }

        private void OnDataLoaded()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return;
            }

            PrepareAds();
        }

        private void PrepareAppKey()
        {
#if UNITY_ANDROID
            _appKey = "e7336891";
#endif

#if UNITY_IOS
            _appKey = "e7332f09";
#endif
        }

        private void PrepareAds()
        {
            //if (_dataManager.IsAdsBought)
            //{
            //    IronSource.Agent.init(_appKey, IronSourceAdUnits.REWARDED_VIDEO);
            //}
            //else
            //{
            //    IronSource.Agent.init(_appKey, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.REWARDED_VIDEO);
            //    IronSource.Agent.loadInterstitial();
            //}
        }

        private void InterstitialAdClosedEvent()
        {
            TogglePauseOnAds(true);
            IronSource.Agent.loadInterstitial();
        }

        private void InterstitialAdLoadFailedEvent(IronSourceError obj)
        {
            TogglePauseOnAds(true);
            StartCoroutine(LoadInterstitial());
        }

        private IEnumerator LoadInterstitial()
        {
            yield return new WaitForSeconds(3f);
            IronSource.Agent.loadInterstitial();
        }

        private void RewardedAdFailed(IronSourceError obj)
        {
            if (_onEndVideo != null)
            {
                TogglePauseOnAds(true);
                _onEndVideo.Invoke(false);
            }
        }

        private void RewardedAdClosedEvent()
        {
            if (_onEndVideo != null)
            {
                TogglePauseOnAds(true);
                _rewardAdsPerDay--;
                SaveRewardAdsCount();
                _onEndVideo.Invoke(_isRewardVideoEnds);
            }
        }

        private void RewardedAdSuccess(IronSourcePlacement obj)
        {
            if (_onEndVideo != null)
            {
                _isRewardVideoEnds = true;
            }
        }

        private void OnApplicationPause(bool isPaused)
        {
            IronSource.Agent.onApplicationPause(isPaused);
        }

        private IEnumerator ShowNotReadyRewardedCor(Action<bool> onEndVideo)
        {
            float waitingTime = 0f;
            bool adShown = false;

            //_loadingAdsUIController.LoadPanelActivate(true);

            while (waitingTime < _waitingLimit && !adShown)
            {
                if (!IronSource.Agent.isRewardedVideoAvailable())
                {
                    waitingTime++;
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    //_loadingAdsUIController.LoadPanelActivate(false);
                    adShown = true;
                    _isRewardVideoEnds = false;
                    TogglePauseOnAds(false);
                    IronSource.Agent.showRewardedVideo();
                    _onEndVideo = onEndVideo;
                    yield break;
                }

                yield return null;
            }

            //_loadingAdsUIController.LoadPanelActivate(false);
            //_loadingAdsUIController.AdsNotReadyPanelActivate();
            onEndVideo.Invoke(false);
        }

        private void TogglePauseOnAds(bool isActive)
        {
#if UNITY_IOS
            AudioListener.volume = isActive ? 1 : 0;
            Time.timeScale = isActive ? 1 : 0;
#endif
        }

        private bool IsCanWatchRewardAd()
        {
            if (!PlayerPrefs.HasKey(REWARD_ADS_COUNT_PER_DAY_TIME))
            {
                SetDefaultRewardAdsCount();
                SaveRewardAdsCount();

                return true;
            }

            _rewardAdsPerDay = PlayerPrefs.GetInt(REWARD_ADS_PER_DAY, _adsWatchMaxCount);
            _rewardAdsCounterMilliseconds = PlayerPrefs.GetFloat(REWARD_ADS_COUNT_PER_DAY_TIME, 0f);
            long resultTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() - (long)_rewardAdsCounterMilliseconds;

            if (resultTime > (long)_adsCounterResetTime)
            {
                SetDefaultRewardAdsCount();
                SaveRewardAdsCount();
            }

            return _rewardAdsPerDay > 0;
        }

        private void SetDefaultRewardAdsCount()
        {
            _rewardAdsCounterMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _rewardAdsPerDay = _adsWatchMaxCount;
        }

        private void SaveRewardAdsCount()
        {
            PlayerPrefs.SetFloat(REWARD_ADS_COUNT_PER_DAY_TIME, _rewardAdsCounterMilliseconds);
            PlayerPrefs.SetInt(REWARD_ADS_PER_DAY, _rewardAdsPerDay);
        }

        #endregion

        #region Public

        public void ShowRewardVideo(Action<bool> onEndVideo, bool withLoading = false, bool showInt = false)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                onEndVideo.Invoke(false);
                return;
            }

#if UNITY_EDITOR
            onEndVideo.Invoke(true);
            _onEndVideo = onEndVideo;
#else
            if (IsCanWatchRewardAd())
            {
                if (!IronSource.Agent.isRewardedVideoAvailable())
                {
                    if (withLoading)
                    {
                        StartCoroutine(ShowNotReadyRewardedCor(onEndVideo));
                        return;
                    }
                    else
                    {
                        _loadingAdsUIController.AdsNotReadyPanelActivate();
                        onEndVideo.Invoke(false);
                        return;
                    }
                }

                _isRewardVideoEnds = false;
                TogglePauseOnAds(false);
                IronSource.Agent.showRewardedVideo();
                _onEndVideo = onEndVideo;
            }
            else
            {
                if (showInt)
                {
                    ShowInterstitialAd(onEndVideo);
                }
                else
                {
                    _loadingAdsUIController.AdsNotReadyPanelActivate();
                }
            }
#endif
        }

        public void ShowInterstitialAd(Action<bool> onEndInterestitial)
        {
            //_analyticsManager.LogAdsInterstitialEvent(StaticData.CurrentLevel.ToString());

            //if (_dataManager.IsAdsBought)
            //{
                onEndInterestitial.Invoke(true);
                return;
            //}

            if (Application.internetReachability == NetworkReachability.NotReachable || !IronSource.Agent.isInterstitialReady())
            {
                onEndInterestitial.Invoke(false);
                return;
            }

            _intCounter = 0;
            PlayerPrefs.SetInt(INT_COUNTER_KEY, _intCounter);

            TogglePauseOnAds(false);
            IronSource.Agent.showInterstitial();

            onEndInterestitial.Invoke(true);
        }

        public bool CheckShouldShowInt()
        {
            return _intCounter >= _shouldShowIntCounter;
        }

        public void ResetIntCounter()
        {
            if (_intCounter > 0)
            {
                _intCounter--;
            }
            PlayerPrefs.SetInt(INT_COUNTER_KEY, _intCounter);
        }
#endregion
    }
}
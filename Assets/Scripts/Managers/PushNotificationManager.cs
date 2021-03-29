using BayatGames.SaveGameFree;
using System;
using UnityEngine;
using Assets.Scripts.Enum;

#if UNITY_IOS
using System.Collections;
using Unity.Notifications.iOS;
#endif

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

namespace Assets.Scripts.Managers
{
    public class PushNotificationManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TimeManager _timeManager;
        [SerializeField] private AnalyticsManager _amplitudeManager;

        private bool _isPushNotificationsEnable = false;
        private int _energyNotification;
        private readonly string _saveDataIdentifier = "IsPushNotificationsEnable";

        private readonly string _reloadAdsNotificationID = "rewardAdsNotification";
        private readonly string _calendarRewardNotificationID = "calendarRewardNotification";
        private readonly string _fullEnergyNotificationID = "fullEnergyNotification";

#if UNITY_IOS
        private IEnumerator IosPushAuthorizationRequest()
        {
            using (var request = new AuthorizationRequest(AuthorizationOption.Alert | AuthorizationOption.Sound, false))
            {
                while (!request.IsFinished)
                {
                    yield return null;
                };

                _isPushNotificationsEnable = request.Granted;

                if (_isPushNotificationsEnable)
                {
                    _amplitudeManager.LogEvent(AnalyticsEnum.Push_notification_panel_yes_click);
                }
                else
                {
                    _amplitudeManager.LogEvent(AnalyticsEnum.Push_notification_panel_no_click);
                }
                SaveData();
            }
        }
#endif
        private void Awake()
        {
#if UNITY_IOS
            OnPushNotifcationsAllow(true);
#endif

#if UNITY_ANDROID
            _isPushNotificationsEnable = true;
            SaveData();
#endif
        }

        private void OnPushNotifcationsAllow(bool isPushNotificationsAllow)
        {
#if UNITY_IOS
            if (isPushNotificationsAllow)
            {
                StartCoroutine(IosPushAuthorizationRequest());
            }
            else
            {
                _isPushNotificationsEnable = isPushNotificationsAllow;
                SaveData();
            }
#endif
        }

        private void Start()
        {
            LoadData();

            if (_isPushNotificationsEnable)
            {
                ClearNotifications();
            }
        }

        private void SaveData()
        {
            SaveGame.Save(_saveDataIdentifier, _isPushNotificationsEnable);
        }

        private void LoadData()
        {
            if (SaveGame.Exists(_saveDataIdentifier))
            {
                _isPushNotificationsEnable = SaveGame.Load(_saveDataIdentifier, false);
            }
        }

        private void ClearNotifications()
        {
#if UNITY_IOS
            iOSNotificationCenter.RemoveAllScheduledNotifications();
            iOSNotificationCenter.RemoveAllDeliveredNotifications();
#endif

#if UNITY_ANDROID
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelAllDisplayedNotifications();
#endif
        }

        public void SetupFullEnergyNotification(int hours, int minutes, int seconds)
        {
            if (_isPushNotificationsEnable == false)
            {
                return;
            }

#if UNITY_IOS
            iOSNotificationCenter.RemoveScheduledNotification(_fullEnergyNotificationID);
            iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(hours, minutes, seconds),
                Repeats = false
            };

            iOSNotification notification = new iOSNotification()
            {
                Identifier = _fullEnergyNotificationID,
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.FullEnT"),
                Body = SimpleLocalization.LocalizationManager.Localize("PrPol.FullEnD"),
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif

#if UNITY_ANDROID
            AndroidNotificationCenter.DeleteNotificationChannel(_fullEnergyNotificationID);
            AndroidNotificationCenter.CancelScheduledNotification(_energyNotification);
            AndroidNotificationChannel anc = new AndroidNotificationChannel()
            {
                Id = _fullEnergyNotificationID,
                Name = "fullenergyNotification",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(anc);

            AndroidNotification notification = new AndroidNotification
            {
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.FullEnT"),
                Text = SimpleLocalization.LocalizationManager.Localize("PrPol.FullEnD"),
                FireTime = DateTime.Now.Add(new TimeSpan(hours, minutes, seconds)),
                SmallIcon = "daily_reward_icon_small",
                LargeIcon = "daily_reward_icon_large"
            };

            _energyNotification = AndroidNotificationCenter.SendNotification(notification, _fullEnergyNotificationID);
#endif
        }

        public void SetupAdsReloadNotification(int hours, int minutes, int seconds)
        {
            if (_isPushNotificationsEnable == false)
            {
                return;
            }

#if UNITY_IOS
            iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(hours, minutes, seconds),
                Repeats = false
            };

            iOSNotification notification = new iOSNotification()
            {
                Identifier = _reloadAdsNotificationID,
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.GiftT"),
                Body = SimpleLocalization.LocalizationManager.Localize("PrPol.GiftD"),
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif

#if UNITY_ANDROID
            AndroidNotificationChannel anc = new AndroidNotificationChannel()
            {
                Id = _reloadAdsNotificationID,
                Name = "dailyRewardNotification",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(anc);

            AndroidNotification notification = new AndroidNotification
            {
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.GiftT"),
                Text = SimpleLocalization.LocalizationManager.Localize("PrPol.GiftD"),
                FireTime = DateTime.Now.Add(new TimeSpan(hours, minutes, seconds)),
                SmallIcon = "daily_reward_icon_small",
                LargeIcon = "daily_reward_icon_large"
            };

            AndroidNotificationCenter.SendNotification(notification, _reloadAdsNotificationID);
#endif
        }

        public void SetupCalendarRewardNotification(int hours, int minutes, int seconds)
        {
            if (_isPushNotificationsEnable == false)
            {
                return;
            }

#if UNITY_IOS
            iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(hours, minutes, seconds),
                Repeats = false
            };

            iOSNotification notification = new iOSNotification()
            {
                Identifier = _calendarRewardNotificationID,
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.CalendarT"),
                Body = SimpleLocalization.LocalizationManager.Localize("PrPol.CalendarD"),
                ShowInForeground = true,
                ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Sound,
                Trigger = timeTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
#endif

#if UNITY_ANDROID
            AndroidNotificationChannel anc = new AndroidNotificationChannel()
            {
                Id = _calendarRewardNotificationID,
                Name = "calendarRewardNotification",
                Importance = Importance.High,
                Description = "Generic notifications",
            };

            AndroidNotificationCenter.RegisterNotificationChannel(anc);

            AndroidNotification notification = new AndroidNotification
            {
                Title = SimpleLocalization.LocalizationManager.Localize("PrPol.CalendarT"),
                Text = SimpleLocalization.LocalizationManager.Localize("PrPol.CalendarD"),
                FireTime = DateTime.Now.Add(new TimeSpan(hours, minutes, seconds)),
                SmallIcon = "daily_reward_icon_small",
                LargeIcon = "daily_reward_icon_large"
            };

            AndroidNotificationCenter.SendNotification(notification, _calendarRewardNotificationID);
#endif
        }
    }
}
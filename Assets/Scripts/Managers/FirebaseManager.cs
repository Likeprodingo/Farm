using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        public Action<DataSnapshot> CrossPromotionDataLoadedAction { get; set; }

        [Header("Components")]
        [SerializeField] private DataManager _dataManager;

        [Header("Options")]
        [SerializeField] private int _crossPromoShowLevel;

        private const string _DATABASE_PATH = "crossPromotionIndex";

        private bool _isInit = false;
        private bool _isDataLoaded = false;

        #region Unity

        private void Awake()
        {
            //_dataManager.LevelDataUpdated += OnLevelUpdated;
        }

        #endregion

        #region Private

        private void OnLevelUpdated()
        {
            if (_isInit == false)
            {
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.Result == DependencyStatus.Available)
                    {
                        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAppOpen);
                        _isInit = true;

                        //if (_crossPromoShowLevel <= _dataManager.CurrentLevel)
                        //{
                        //    Login();
                        //    return;
                        //}
                    }
                });
            }

            //if (_isDataLoaded == false && _isInit && _crossPromoShowLevel <= _dataManager.CurrentLevel)
            //{
            //    Login();
            //}
        }

        private void Login()
        {
            FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWithOnMainThread((System.Threading.Tasks.Task<FirebaseUser> task) =>
            {
                if (task.IsCompleted)
                {
                    GetCrossPromotionData();
                }
                else
                {
                    Debug.LogError(task.Exception);
                }
            });
        }

        #endregion

        #region Public

        public void GetCrossPromotionData()
        {
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            FirebaseDatabase.DefaultInstance.GetReference(_DATABASE_PATH).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted)
                {
                    _isDataLoaded = true;
                    CrossPromotionDataLoadedAction?.Invoke(task.Result);
                }
                else
                {
                    Debug.LogError(task.Exception);
                }
            });
        }

        #endregion
    }
}
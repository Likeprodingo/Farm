using Assets.Scripts.Data;
using Assets.Scripts.Enum;
using Assets.Scripts.Managers;
using DG.Tweening;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class CrossPromotionUIController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private FirebaseManager _firebaseManager;
        [SerializeField] private AnalyticsManager _analyticsManager;
        [SerializeField] private Animator _gifAnimator;
        [SerializeField] private List<CrossPromotionData> _crossPromotionDatas;

        [Header("UI Components")]
        [SerializeField] private Button _openAppLinkButton;
        [SerializeField] private Image _currentAppIconImage;
        [SerializeField] private Image _nextAppIconImage;

        [Header("Options")]
        [SerializeField] private float _changeAppIconTime;

        private int _nextCrossPromoDataIndex;
        private int _currentCrossPromoDataIndex;
        private float _currentAppIconTime;

        private bool _isDotween = false;
        private bool _isAllApps = false;

        private CrossPromotionData _currentCrossPromotionData;

        #region Unity

        private void Awake()
        {
            _firebaseManager.CrossPromotionDataLoadedAction += OnCrossPromotionDataLoaded;
            _openAppLinkButton.onClick.AddListener(OpenAppLinkOnClick);
        }

        private void Update()
        {
            if (_isAllApps == false)
            {
                return;
            }

            _currentAppIconTime += Time.deltaTime;

            if (_currentAppIconTime >= _changeAppIconTime && _isDotween == false)
            {
                _isDotween = true;

                _currentAppIconImage.transform.DOLocalMoveX(-100f, 1f);

                _nextAppIconImage.transform.DOLocalMoveX(0, 1f).OnComplete(() =>
                {
                    Image tempImage = _currentAppIconImage;

                    _currentAppIconImage = _nextAppIconImage;

                    _nextAppIconImage = tempImage;

                    _nextAppIconImage.transform.DOLocalMoveX(100, 0f);

                    _isDotween = false;
                    _currentAppIconTime = 0f;

                    _currentCrossPromoDataIndex = _nextCrossPromoDataIndex;

                    if (_nextCrossPromoDataIndex == _crossPromotionDatas.Count - 1)
                    {
                        _nextCrossPromoDataIndex = 0;
                    }
                    else
                    {
                        _nextCrossPromoDataIndex++;
                    }

                    _nextAppIconImage.sprite = _crossPromotionDatas[_nextCrossPromoDataIndex].AppIconSprite;
                });
            }
        }

        #endregion

        #region Private

        private void OnCrossPromotionDataLoaded(DataSnapshot dataSnapshot)
        {
            Debug.LogError("Value: " + dataSnapshot);

            CrossPromoEnum appType = (CrossPromoEnum)Convert.ToInt32(dataSnapshot.GetRawJsonValue());

            if (appType == CrossPromoEnum.None)
            {
                return;
            }

            _gifAnimator.gameObject.SetActive(true);

            if (appType == CrossPromoEnum.All)
            {
                _currentAppIconImage.sprite = _crossPromotionDatas[0].AppIconSprite;
                _nextAppIconImage.sprite = _crossPromotionDatas[1].AppIconSprite;

                _currentCrossPromotionData = _crossPromotionDatas[0];

                _nextCrossPromoDataIndex = 1;

                _isAllApps = true;

                return;
            }

            for (int i = 0; i < _crossPromotionDatas.Count; i++)
            {
                if (_crossPromotionDatas[i].AppType == appType)
                {
                    _currentCrossPromotionData = _crossPromotionDatas[i];
                    _currentAppIconImage.sprite = _currentCrossPromotionData.AppIconSprite;
                    break;
                }
            }

            _gifAnimator.enabled = true;
        }

        private void OpenAppLinkOnClick()
        {
            Application.OpenURL(_crossPromotionDatas[_currentCrossPromoDataIndex].AppURL);
            _analyticsManager.LogCrossPromotionEvent(_crossPromotionDatas[_currentCrossPromoDataIndex].AppType);
        }

        #endregion
    }
}
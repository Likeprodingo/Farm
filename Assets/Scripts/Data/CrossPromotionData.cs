using Assets.Scripts.Enum;
using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class CrossPromotionData
    {
        public CrossPromoEnum AppType { get { return _appType; } }
        public Sprite AppIconSprite { get { return _appIconSprite; } }
        public string AppURL { get { return _appURL; } }

        [Header("Options")]
        [SerializeField] private CrossPromoEnum _appType;
        [SerializeField] private Sprite _appIconSprite;
        [SerializeField] private string _appURL;
    }
}
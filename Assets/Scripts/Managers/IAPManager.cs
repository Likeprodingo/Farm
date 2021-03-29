using Assets.Scripts.Enum;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.Purchasing;
using System;
using UnityEngine.Purchasing.Security;

namespace Assets.Scripts.Managers
{
    public class IAPManager : MonoBehaviour, IStoreListener
    {
        public Action<bool> OnStartBuying { get; set; }
        public Action ProductsInitAction { get; set; }

        private static IStoreController m_StoreController;
        private static IExtensionProvider m_StoreExtensionProvider;

        [Header("Components")]
        [SerializeField] private AnalyticsManager _analyticsManager;
        [SerializeField] private DataManager _dataManager;

        private string _currentProductId;

        private bool _isRestoreClicked = false;

        #region Unity

        private void Start()
        {
            if (m_StoreController == null)
            {
                InitializePurchasing();
            }
        }

        #endregion

        #region Private

        private void InitializePurchasing()
        {
            if (IsInitialized())
            {
                return;
            }

            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            var enumNames = System.Enum.GetNames(typeof(IAPEnum));

            for (int i = 0; i < enumNames.Length; i++)
            {
                builder.AddProduct(enumNames[i], CheckIsNonConsumable(enumNames[i]) ? ProductType.NonConsumable : ProductType.Consumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        private bool CheckIsNonConsumable(string type)
        {
            return type == "rr_no_ads" || type == "rr_special_medium" || type == "rr_special_24" || type == "rr_battle_pass";
        }

        private bool IsInitialized()
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }

        private void BuyProductID(string productId)
        {
            if (IsInitialized())
            {
                OnStartBuying?.Invoke(true);

                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    m_StoreController.InitiatePurchase(product);
                }
            }
        }
        #endregion

        #region Public

        public string GetItemPrice(IAPEnum itemType)
        {
            if (m_StoreController == null)
            {
                return "-";
            }

            Product product = m_StoreController.products.WithID(itemType.ToString());
            var price = product.metadata.localizedPriceString;
            return price;
        }

        public void RestorePurchases()
        {
            if (!IsInitialized())
            {
                return;
            }

            OnStartBuying?.Invoke(true);
            _isRestoreClicked = true;

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {
                _isRestoreClicked = false;
                OnStartBuying?.Invoke(false);
            });
        }

        public void PurchaseItem(IAPEnum productType)
        {
            Debug.Log("PurchaseItem " + productType);
            _currentProductId = productType.ToString();

            BuyProductID(_currentProductId);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            m_StoreController = controller;
            m_StoreExtensionProvider = extensions;
            ProductsInitAction?.Invoke();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            //Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            Debug.Log("ProcessPurchase " + args.purchasedProduct.metadata.localizedPrice + " " + args.purchasedProduct.metadata.localizedTitle + " " + args.purchasedProduct.metadata.localizedDescription);
            string transactionID = string.Empty;
            string purchaseDate = string.Empty;

            //if (String.Equals(args.purchasedProduct.definition.id, _currentProductId, StringComparison.Ordinal) || _isRestoreClicked)
            //{
                bool validPurchase = true;

#if UNITY_ANDROID || UNITY_IOS

                var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);

                try
                {
                    var result = validator.Validate(args.purchasedProduct.receipt);

                    foreach (IPurchaseReceipt productReceipt in result)
                    {
                        transactionID = productReceipt.transactionID;
                        purchaseDate = productReceipt.purchaseDate.ToString();
                    }
                }
                catch (IAPSecurityException)
                {
                    validPurchase = false;
                }
#endif
                if (validPurchase)
                {
                    bool parseRes = System.Enum.TryParse(args.purchasedProduct.definition.id, out IAPEnum outType);

                    if (!parseRes)
                    {
                        return PurchaseProcessingResult.Complete;
                    }
                    switch (outType)
                    {
                        //case IAPEnum.rr_gems_small:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gems_small_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGems(outType);
                        //    break;
                        //case IAPEnum.rr_gems_medium:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gems_medium_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGems(outType);
                        //    break;
                        //case IAPEnum.rr_gems_big:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gems_big_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGems(outType);
                        //    break;
                        //case IAPEnum.rr_gold_small:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gold_small_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGold(outType);
                        //    break;
                        //case IAPEnum.rr_gold_medium:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gold_medium_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGold(outType);
                        //    break;
                        //case IAPEnum.rr_gold_big:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_gold_big_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseGold(outType);
                        //    break;
                        //case IAPEnum.rr_no_ads:
                        //    _dataManager.PurchaseNoAds();
                        //    break;
                        //case IAPEnum.rr_special_24:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_special_24_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseSpecial24h();
                        //    break;
                        //case IAPEnum.rr_special_low:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_special_low_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseSpecialLow();
                        //    break;
                        //case IAPEnum.rr_special_medium:
                        //    _analyticsManager.LogShopBuyClickEvent(AnalyticsEnum.Shop_buy_special_medium_click, StaticData.CurrentLevel.ToString());
                        //    _dataManager.PurchaseSpecialVlad();
                        //    break;
                        //case IAPEnum.rr_battle_pass:
                        //    _dataManager.PurchasePremiumBattlePass();
                        //    break;
                    }
                }

            //    _currentProductId = "";
            //}
            //else
            //{
            //    _currentProductId = "";
            //}

            OnStartBuying?.Invoke(false);

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            OnStartBuying?.Invoke(false);
        }

        #endregion
    }
}
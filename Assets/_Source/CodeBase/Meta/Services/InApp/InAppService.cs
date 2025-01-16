using Assets._Source.CodeBase.Meta.Infrastructure.EntryPoint;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityServices = Unity.Services.Core.UnityServices;

namespace Assets._Source.CodeBase.Meta.Services.InApp
{
    public class InAppService : IDetailedStoreListener, ISDKInitializer, IReadonlyStore, IStoreBuyer
    {
        private IStoreController _storeController;
        private IExtensionProvider _storeExtensionProvider;

        public bool IsAdsRemoved { get; private set; }
        private bool IsInitialized => _storeController != null && _storeExtensionProvider != null;

        public async UniTask Init()
        {
            try
            {
                await InitializeGamingServices();
                InitializePurchasing();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to initialize Unity Gaming Services: {e.Message}");
            }

            IsAdsRemoved = CheckAdsRemoved();
        }

        public void BuyRemoveAds()
        {
            if (IsInitialized)
            {
                Product product = _storeController.products.WithID(ProductId.noads.ToString());

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log($"Попытка покупки: {ProductId.noads}");
                    _storeController.InitiatePurchase(product);
                }
                else
                {
                    Debug.LogError("Продукт не доступен для покупки.");
                }
            }
            else
            {
                Debug.LogError("Покупка не инициализирована.");
            }
        }

        public void RestorePurchases()
        {
            if (!IsInitialized)
            {
                Debug.LogError("Покупка не инициализирована, восстановление невозможно.");
                return;
            }

            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
            {
                Debug.Log("Восстановление покупок запущено...");
                _storeExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions((result, message) =>
                {
                    if (result)
                    {
                        Debug.Log($"Restore successful! Message: {message}");
                    }
                    else
                    {
                        Debug.Log($"Restore failed. Message: {message}");
                    }
                });
            }
            else
            {
                Debug.LogWarning("Восстановление покупок поддерживается только на iOS.");
            }
        }

        private async UniTask InitializeGamingServices()
        {
            InitializationOptions options = new InitializationOptions().SetEnvironmentName("product"); // Укажите окружение, если нужно
            await UnityServices.InitializeAsync(options);
            Debug.Log("Unity Gaming Services initialized successfully.");
        }

        private void InitializePurchasing()
        {
            if (IsInitialized)
                return;

            StandardPurchasingModule module = StandardPurchasingModule.Instance();
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

            builder.AddProduct(ProductId.noads.ToString(), ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        private bool CheckAdsRemoved()
        {
            if (_storeController != null)
            {
                Product product = _storeController.products.WithID(ProductId.noads.ToString());

                if (product != null && product.hasReceipt)
                {
                    Debug.Log("Чек для отключения рекламы найден. Покупка была совершена.");
                    return true;
                }
            }

            Debug.Log("Чек для отключения рекламы не найден.");
            return false;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Unity IAP инициализирован.");
            _storeController = controller;
            _storeExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"Ошибка инициализации IAP: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"Ошибка инициализации IAP: {error}, Сообщение: {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (string.Equals(purchaseEvent.purchasedProduct.definition.id, ProductId.noads.ToString(), System.StringComparison.Ordinal))
            {
                Debug.Log("Покупка завершена: Отключение рекламы!");
                IsAdsRemoved = true;
            }
            else
            {
                Debug.LogError($"Неизвестный продукт: {purchaseEvent.purchasedProduct.definition.id}");
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
        }
    }
}
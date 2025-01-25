using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace CompleteProject
{
	public class IAP : MonoBehaviour, IStoreListener
	{
		private static IStoreController m_StoreController;

		private static IExtensionProvider m_StoreExtensionProvider;

		public static string kProductIDRemovesAdvertisment = "com.gameinlife.merged.remove_advertisments";

		public static string kProductIDDoubleCoins = "com.gameinlife.merged.double_coins";

		public static string kProductID1000Coins = "com.gameinlife.merged.1000_coins";

		public static string kProductID4000Coins = "com.gameinlife.merged.4000_coins";

		public static string kProductID20000Coins = "com.gameinlife.merged.20000_coins";

		public static string kProductIDoubleCoinsInAndroid = "com.gameinlife.merged_double.coins";

		public static string kProductI1000CoinsInAndroid = "com.gameinlife.merged_1000.coin";

		public static string kProductI4000CoinsInAndroid = "com.gameinlife.merged_4000.coins";

		public static string kProductI20000CoinsInAndroid = "com.gameinlife.merged_20000.coins";

		private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

		private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

		private static Action<bool> __f__am_cacheD;

		private void Start()
		{
			if (IAP.m_StoreController == null)
			{
				this.InitializePurchasing();
			}
		}

		public void InitializePurchasing()
		{
			if (this.IsInitialized())
			{
				return;
			}
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
			configurationBuilder.AddProduct(IAP.kProductIDoubleCoinsInAndroid, ProductType.Consumable);
			configurationBuilder.AddProduct(IAP.kProductI1000CoinsInAndroid, ProductType.Consumable);
			configurationBuilder.AddProduct(IAP.kProductI4000CoinsInAndroid, ProductType.Consumable);
			configurationBuilder.AddProduct(IAP.kProductI20000CoinsInAndroid, ProductType.Consumable);
			UnityPurchasing.Initialize(this, configurationBuilder);
		}

		private bool IsInitialized()
		{
			return IAP.m_StoreController != null && IAP.m_StoreExtensionProvider != null;
		}

		public void BuyDoubleCoins()
		{
			this.BuyProductID(IAP.kProductIDoubleCoinsInAndroid);
		}

		public void Buy1000Coins()
		{
			this.BuyProductID(IAP.kProductI1000CoinsInAndroid);
		}

		public void Buy4000Coins()
		{
			this.BuyProductID(IAP.kProductI4000CoinsInAndroid);
		}

		public void Buy20000Coins()
		{
			this.BuyProductID(IAP.kProductI20000CoinsInAndroid);
		}

		private void BuyProductID(string productId)
		{
			if (this.IsInitialized())
			{
				Product product = IAP.m_StoreController.products.WithID(productId);
				if (product != null && product.availableToPurchase)
				{
					UnityEngine.Debug.LogError(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
					IAP.m_StoreController.InitiatePurchase(product);
				}
				else
				{
					Store.instance.Test();
					UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				Store.instance.Test();
				UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}

		public void RestorePurchases()
		{
			SoundsManager.instance.PlayAudioSource(SoundsManager.instance.button);
			if (!this.IsInitialized())
			{
				UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
				Store.instance.Test();
				return;
			}
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
			{
				UnityEngine.Debug.Log("RestorePurchases started ...");
				IAppleExtensions extension = IAP.m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
				extension.RestoreTransactions(delegate(bool result)
				{
					UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
				});
				SettingsGUI.instance.CloseSettingGUI();
			}
			else
			{
				SettingsGUI.instance.CloseSettingGUI();
				UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
			}
		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
			UnityEngine.Debug.Log("OnInitialized: PASS");
			IAP.m_StoreController = controller;
			IAP.m_StoreExtensionProvider = extensions;
		}

		public void OnInitializeFailed(InitializationFailureReason error)
		{
			UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductIDRemovesAdvertisment, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductIDDoubleCoins, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.SaveDoubleCoinsState();
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductID1000Coins, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(1000);
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductID4000Coins, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(4000);
			}
			if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductID20000Coins, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(20000);
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductIDoubleCoinsInAndroid, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchaseAndroid: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.SaveDoubleCoinsState();
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductI1000CoinsInAndroid, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchaseAndroid: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(1000);
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductI4000CoinsInAndroid, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchaseAndroid: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(4000);
			}
			else if (string.Equals(args.purchasedProduct.definition.id, IAP.kProductI20000CoinsInAndroid, StringComparison.Ordinal))
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchaseAndroid: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				ScoreHandler.instance.increaseSpecialPoints(20000);
			}
			else
			{
				UnityEngine.Debug.LogError(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
				Store.instance.Test();
			}
			Store.instance.Test();
			return PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			UnityEngine.Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
			Store.instance.Test();
		}

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            throw new NotImplementedException();
        }

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new NotImplementedException();
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new NotImplementedException();
        }

        void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new NotImplementedException();
        }

        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            throw new NotImplementedException();
        }
    }
}

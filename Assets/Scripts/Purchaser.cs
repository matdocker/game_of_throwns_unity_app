using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System.Collections;

public class Purchaser : MonoBehaviour, IStoreListener
{
  public int coinCount;
  public int plusCoin;
  public Text coinTextCounter;
  //public GameObject GameScene;
  private GameScene gs;
  private SoundManager sm;


  private static IStoreController m_StoreController;          // The Unity Purchasing system.
  private static IExtensionProvider m_StoreExtensionProvider;
  //Consumables
  public static string coins_100 = "100_coins";
  public static string coins_500 = "500_coins";
  public static string coins_900 = "900_coins";
  public static string coins_1500 = "1500_coins";
  public static string coinWall_3 = "3_coinwall";
  public static string miniGame_3 = "3_minigame";
  public static string prizePackage = "prizepackage";
  public static string oneTimeOffer = "onetimeoffer";

  public static string kProductIDNonConsumable = "nonconsumable";
  public static string kProductIDSubscription = "subscription";

  // Apple App Store-specific product identifier for the subscription product.
  private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

  // Google Play Store-specific product identifier subscription product.
  private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

  void Start()
  {
    sm = Transform.FindObjectOfType<SoundManager>();
    gs = Transform.FindObjectOfType<GameScene>();

    // If we haven't set up the Unity Purchasing reference
    if (m_StoreController == null)
    {
      // Begin to configure our connection to Purchasing
      InitializePurchasing();
      gs = GetComponent<GameScene>();

    }
  }

  public void InitializePurchasing()
  {
    // If we have already connected to Purchasing ...
    if (IsInitialized())
    {
      // ... we are done here.
      return;
    }

    // Create a builder, first passing in a suite of Unity provided stores.
    var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

    // Add a product to sell / restore by way of its identifier, associating the general identifier
    // with its store-specific identifiers.
    builder.AddProduct(coins_100, ProductType.Consumable);
    builder.AddProduct(coins_500, ProductType.Consumable);
    builder.AddProduct(coins_900, ProductType.Consumable);
    builder.AddProduct(coins_1500, ProductType.Consumable);
    builder.AddProduct(coinWall_3, ProductType.Consumable);
    builder.AddProduct(miniGame_3, ProductType.Consumable);
    builder.AddProduct(prizePackage, ProductType.Consumable);
    builder.AddProduct(oneTimeOffer, ProductType.Consumable);

    // Continue adding the non-consumable product.
    builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
    // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
    // if the Product ID was configured differently between Apple and Google stores. Also note that
    // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
    // must only be referenced here. 
    builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
                { kProductNameAppleSubscription, AppleAppStore.Name },
                { kProductNameGooglePlaySubscription, GooglePlay.Name },
            });

    // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
    // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
    UnityPurchasing.Initialize(this, builder);
  }


  private bool IsInitialized()
  {
    // Only say we are initialized if both the Purchasing references are set.
    return m_StoreController != null && m_StoreExtensionProvider != null;
  }


  public void BuyCoins100()
  {
    BuyProductID(coins_100);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyCoins500()
  {
    BuyProductID(coins_500);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyCoins900()
  {
    BuyProductID(coins_900);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyCoins1500()
  {
    BuyProductID(coins_1500);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyCoinWall_3()
  {
    BuyProductID(coinWall_3);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyMiniGame_3()
  {
    BuyProductID(miniGame_3);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyPrizePackage()
  {
    BuyProductID(prizePackage);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }
  public void BuyoneTimeOffer()
  {
    BuyProductID(oneTimeOffer);
    // Buy the consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.    
  }


  public void BuyNonConsumable()
  {
    // Buy the non-consumable product using its general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    BuyProductID(kProductIDNonConsumable);
  }


  public void BuySubscription()
  {
    // Buy the subscription product using its the general identifier. Expect a response either 
    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    // Notice how we use the general product identifier in spite of this ID being mapped to
    // custom store-specific identifiers above.
    BuyProductID(kProductIDSubscription);
  }


  void BuyProductID(string productId)
  {
    // If Purchasing has been initialized ...
    if (IsInitialized())
    {
      // ... look up the Product reference with the general product identifier and the Purchasing 
      // system's products collection.
      Product product = m_StoreController.products.WithID(productId);

      // If the look up found a product for this device's store and that product is ready to be sold ... 
      if (product != null && product.availableToPurchase)
      {
        Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
        // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
        // asynchronously.
        m_StoreController.InitiatePurchase(product);
      }
      // Otherwise ...
      else
      {
        // ... report the product look-up failure situation  
        Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
      }
    }
    // Otherwise ...
    else
    {
      // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
      // retrying initiailization.
      Debug.Log("BuyProductID FAIL. Not initialized.");
    }
  }


  // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
  // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
  public void RestorePurchases()
  {
    // If Purchasing has not yet been set up ...
    if (!IsInitialized())
    {
      // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
      Debug.Log("RestorePurchases FAIL. Not initialized.");
      return;
    }

    // If we are running on an Apple device ... 
    if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer)
    {
      // ... begin restoring purchases
      Debug.Log("RestorePurchases started ...");

      // Fetch the Apple store-specific subsystem.
      var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
      // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
      // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
      apple.RestoreTransactions((result) =>
      {
        // The first phase of restoration. If no more responses are received on ProcessPurchase then 
        // no purchases are available to be restored.
        Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
      });
    }
    // Otherwise ...
    else
    {
      // We are not running on an Apple device. No work is necessary to restore purchases.
      Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    }
  }


  //  
  // --- IStoreListener
  //

  public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
  {
    // Purchasing has succeeded initializing. Collect our Purchasing references.
    Debug.Log("OnInitialized: PASS");

    // Overall Purchasing system, configured with products for this application.
    m_StoreController = controller;
    // Store specific subsystem, for accessing device-specific store features.
    m_StoreExtensionProvider = extensions;
  }


  public void OnInitializeFailed(InitializationFailureReason error)
  {
    // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
    Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
  }


  public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
  {
    // A consumable product has been purchased by this user.
    if (String.Equals(args.purchasedProduct.definition.id, coins_100, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 100;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);


    }
    else if (String.Equals(args.purchasedProduct.definition.id, coins_500, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 500;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);

    }
    else if (String.Equals(args.purchasedProduct.definition.id, coins_900, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 900;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);

    }
    else if (String.Equals(args.purchasedProduct.definition.id, coins_1500, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 1500;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);
    }
    else if (String.Equals(args.purchasedProduct.definition.id, coinWall_3, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 3;
      gs.UpdateCoinWallCount(coinCount);
      sm.Play(SoundManager.SFX.win2);
    }
    else if (String.Equals(args.purchasedProduct.definition.id, miniGame_3, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 3;
      gs.UpdateMiniGameCount(coinCount);
    }
    else if (String.Equals(args.purchasedProduct.definition.id, prizePackage, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 3;
      gs.UpdateMiniGameCount(coinCount);
      gs.UpdateCoinWallCount(coinCount);
      coinCount = 900;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);
    }
    else if (String.Equals(args.purchasedProduct.definition.id, oneTimeOffer, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
      //Debug.Log("100 coins has been purchased");
      coinCount = 3;
      gs.UpdateMiniGameCount(coinCount);
      gs.UpdateCoinWallCount(coinCount);
      coinCount = 900;
      gs.UpdateCoinCount(coinCount);
      sm.Play(SoundManager.SFX.win2);
      //callbackManager.onActivityResult()
    }
    // Or ... a non-consumable product has been purchased by this user.
    else if (String.Equals(args.purchasedProduct.definition.id, kProductIDNonConsumable, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
    }
    // Or ... a subscription product has been purchased by this user.
    else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
    {
      Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
      // TODO: The subscription item has been successfully purchased, grant this to the player.
    }
    // Or ... an unknown product has been purchased by this user. Fill in additional products here....
    else
    {
      Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
    }

    // Return a flag indicating whether this product has completely been received, or if the application needs 
    // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
    // saving purchased products to the cloud, and when that save is delayed. 
    return PurchaseProcessingResult.Complete;
  }


  public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
  {
    // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
    // this reason with the user to guide their troubleshooting actions.
    Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
  }
}
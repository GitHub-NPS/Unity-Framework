using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
using com.unimob.pattern.singleton;
using com.unimob.mec;

#if UNITY_GG_SIGNIN
using Google;
using Firebase.Extensions;
#endif

#if UNITY_IOS_SIGNIN
using AppleAuth;
using AppleAuth.Native;
using AppleAuth.Enums;
using AppleAuth.Interfaces;
using AppleAuth.Extensions;
#endif

public class LoginManager : MonoSingleton<LoginManager>
{
#if UNITY_GG_SIGNIN
    private string webClientId = "117794470811-r8dliij0nevjfqp6j23mi39j449m0npk.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;
#endif

#if UNITY_IOS_SIGNIN
    private IAppleAuthManager appleAuthManager;
#endif

    public Action<LoginData> OnLogin;
    public Action OnLogout;

    public void Init(Transform parent = null)
    {
        AppManager.Login = this;
        if (parent != null)
        {
            transform.SetParent(parent);
        }

        InitGoogleSignIn();
        InitAppleSignIn();
    }

    private void InitGoogleSignIn()
    {
#if UNITY_GG_SIGNIN
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };
#endif
    }

    private void InitAppleSignIn()
    {
#if UNITY_IOS_SIGNIN
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            appleAuthManager = new AppleAuthManager(new PayloadDeserializer());
        }
#endif
    }

    private void Update()
    {
#if UNITY_IOS_SIGNIN
        appleAuthManager?.Update();
#endif
    }

    public void Login()
    {
        Debug.Log("Login");

#if UNITY_EDITOR || DEVELOPMENT
        LoginData data = null;
        var id = GameManager.S.Link;

        if (!string.IsNullOrEmpty(id))
        {
            data = new LoginData()
            {
                UserId = id,
                DisplayName = "User " + "-" + SystemInfo.deviceUniqueIdentifier.Substring(0, 6),
                ImageUrl = ""
            };
        }

        Timing.RunCoroutine(OnLoginInvoke(data));
        return;
#endif

#if UNITY_GG_SIGNIN
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(OnAuthenticationFinished);
        return;
#endif

#if UNITY_IOS_SIGNIN
        var rawNonce = GenerateRandomString(32);
        var nonce = GenerateSHA256NonceFromRawNonce(rawNonce);
        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName, nonce);

        appleAuthManager.LoginWithAppleId(loginArgs, credential =>
        {
            if (credential is IAppleIDCredential appleIdCredential)
            {
                var userId = appleIdCredential.User;
                var email = appleIdCredential.Email;
                var displayName = "You";

                if (appleIdCredential.FullName != null)
                    displayName = $"{appleIdCredential.FullName.GivenName} ({appleIdCredential.FullName.Nickname})";                

                var identityToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken);
                var authorizationCode = Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode, 0, appleIdCredential.AuthorizationCode.Length);

                Debug.Log("Welcome: " + displayName + "!");

                Timing.RunCoroutine(OnLoginInvoke(new LoginData()
                {
                    UserId = userId,
                    IdToken = identityToken,
                    Email = email,
                    DisplayName = displayName,
                    AuthCode = authorizationCode
                }));
            }
        }, error =>
        {
            Debug.Log("Got Unexpected Exception?!? " + error.GetAuthorizationErrorCode());
            Timing.RunCoroutine(OnLoginInvoke(null));
        });

        return;
#endif
    }

#if UNITY_GG_SIGNIN
    private void OnAuthenticationFinished(System.Threading.Tasks.Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.Log("Got Unexpected Exception?!? " + task.Exception);
            Timing.RunCoroutine(OnLoginInvoke(null));
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Canceled");
            Timing.RunCoroutine(OnLoginInvoke(null));
        }
        else
        {
            Debug.Log($"Google Sign-In: {task.Result.IdToken}");
            var loginData = new LoginData()
            {
                UserId = task.Result.UserId,
                IdToken = task.Result.IdToken,
                Email = task.Result.Email,
                DisplayName = task.Result.DisplayName,
                AuthCode = task.Result.AuthCode,
                ImageUrl = task.Result.ImageUrl.ToString()
            };

            Timing.RunCoroutine(OnLoginInvoke(loginData));
        }
    }
#endif

    private IEnumerator<float> OnLoginInvoke(LoginData data)
    {
        Debug.Log("============= Login Handle =============");
        data.Dump();

        yield return Timing.WaitForOneFrame;

        SetDataLogin(data);

        OnLogin?.Invoke(data);

#if UNITY_APPSFLYER
        AppManager.AppsFlyer.LoginSuccessTracking();
#endif
    }

    public void SetDataLogin(LoginData data)
    {
        if (data != null)
        {
            var userSave = DataManager.Save.User;
#if UNITY_GG_SIGNIN || DEVERLOPMENT || UNITY_EDITOR
            userSave.googleId = data.UserId;
#endif

#if UNITY_IOS_SIGNIN
            userSave.appleId = data.UserId;
#endif
            userSave.name = data.DisplayName;
            userSave.avatar = data.ImageUrl;

            userSave.Save();
        }
    }

    public void Logout()
    {
        Debug.Log("Logout");
#if UNITY_GG_SIGNIN
        GoogleSignIn.DefaultInstance.Disconnect();
        OnLogout?.Invoke();
#endif
    }

    private static string GenerateRandomString(int length)
    {
        if (length <= 0)
        {
            throw new Exception("Expected nonce to have positive length");
        }

        const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
        var cryptographicallySecureRandomNumberGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
        var result = string.Empty;
        var remainingLength = length;

        var randomNumberHolder = new byte[1];
        while (remainingLength > 0)
        {
            var randomNumbers = new List<int>(16);
            for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
            {
                cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
                randomNumbers.Add(randomNumberHolder[0]);
            }

            for (var randomNumberIndex = 0; randomNumberIndex < randomNumbers.Count; randomNumberIndex++)
            {
                if (remainingLength == 0)
                {
                    break;
                }

                var randomNumber = randomNumbers[randomNumberIndex];
                if (randomNumber < charset.Length)
                {
                    result += charset[randomNumber];
                    remainingLength--;
                }
            }
        }

        return result;
    }

    private static string GenerateSHA256NonceFromRawNonce(string rawNonce)
    {
        var sha = new System.Security.Cryptography.SHA256Managed();
        var utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
        var hash = sha.ComputeHash(utf8RawNonce);

        var result = string.Empty;
        for (var i = 0; i < hash.Length; i++)
        {
            result += hash[i].ToString("x2");
        }

        return result;
    }
}
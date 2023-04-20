using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
using System.Text;
#endif

public class LoginManager : MonoSingleton<LoginManager>
{
#if UNITY_GG_SIGNIN
    private string webClientId = "117794470811-r8dliij0nevjfqp6j23mi39j449m0npk.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;
#endif

#if UNITY_IOS_SIGNIN
    private IAppleAuthManager _appleAuthManager;
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
            var deserializer = new PayloadDeserializer();
            _appleAuthManager = new AppleAuthManager(deserializer);
        }
#endif
    }

    private void Update()
    {
#if UNITY_IOS_SIGNIN
        _appleAuthManager?.Update();
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
        if (PlayerPrefs.HasKey("Apple_IdToken"))
        {
            Timing.RunCoroutine(OnLoginInvoke(new LoginData()
            {
                UserId = PlayerPrefs.GetString("Apple_UserId"),
                IdToken = PlayerPrefs.GetString("Apple_IdToken"),
                Email = PlayerPrefs.GetString("Apple_Email"),
                DisplayName = PlayerPrefs.GetString("Apple_DisplayName"),
                AuthCode = PlayerPrefs.GetString("Apple_AuthCode")
            }));

            return;
        }
        else
        {
            var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);
            _appleAuthManager.LoginWithAppleId(loginArgs, credential => {
                if (credential is IAppleIDCredential appleIdCredential)
                {
                    var userId = appleIdCredential.User;
                    var email = appleIdCredential.Email;

                    var displayName = appleIdCredential.FullName.GivenName;
                    if (string.IsNullOrEmpty(appleIdCredential.FullName.MiddleName)) displayName += (" " + appleIdCredential.FullName.MiddleName);
                    if (string.IsNullOrEmpty(appleIdCredential.FullName.FamilyName)) displayName += (" " + appleIdCredential.FullName.FamilyName);

                    var identityToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken);
                    var authorizationCode = Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode, 0, appleIdCredential.AuthorizationCode.Length);

                    Debug.Log("Welcome: " + displayName + "!");

                    PlayerPrefs.SetString("Apple_UserId", userId);
                    PlayerPrefs.SetString("Apple_IdToken", identityToken);
                    PlayerPrefs.SetString("Apple_Email", email);
                    PlayerPrefs.SetString("Apple_DisplayName", displayName);
                    PlayerPrefs.SetString("Apple_AuthCode", authorizationCode);

                    Timing.RunCoroutine(OnLoginInvoke(new LoginData()
                    {
                        UserId = userId,
                        IdToken = identityToken,
                        Email = email,
                        DisplayName = displayName,
                        AuthCode = authorizationCode
                    }));
                }
            }, error => {
                var authorizationErrorCode = error.GetAuthorizationErrorCode();
            });

            return;
        }
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
}
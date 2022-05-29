using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using NPS;
using Core;
using System.Text;

#if UNITY_GG_SIGNIN
using Google;
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
    private string webClientId = "1099439886463-n7j3pi932abho83827ol3ndjr59ctv47.apps.googleusercontent.com";

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

#if UNITY_GG_SIGNIN
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;        

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);

        return;
#endif

#if UNITY_IOS_SIGNIN
        if (PlayerPrefs.HasKey("Apple_IdToken"))
        {
            StartCoroutine(LoginSuccess(new LoginData()
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

                    StartCoroutine(LoginSuccess(new LoginData()
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

#if UNITY_EDITOR || DEVELOPMENT
        StartCoroutine(LoginSuccess(new LoginData()
        {
            DisplayName = "You"
        }));

        return;
#endif
    }

#if UNITY_GG_SIGNIN
    private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                    task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                            (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Canceled");
        }
        else
        {
            Debug.Log("Welcome: " + task.Result.DisplayName + "!");

            StartCoroutine(LoginSuccess(new LoginData()
            {
                UserId = task.Result.UserId,
                IdToken = task.Result.IdToken,
                Email = task.Result.Email,
                DisplayName = task.Result.DisplayName,
                AuthCode = task.Result.AuthCode
            }));
        }
    }
#endif

    private IEnumerator LoginSuccess(LoginData data)
    {
        yield return new WaitForEndOfFrame();
        OnLogin?.Invoke(data);
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
using com.unimob.pattern.singleton;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CloudSaveManager : MonoSingleton<CloudSaveManager>
{
    public event Action<VersionCloud> OnGetVersionCloud;

    public event Action<UserDataCloud> OnGetUserDataCloud;
    public event Action<UserDataCloud> OnPostUserDataCloud;

    private const string BaseUrl = "https://food-fever.unimob.com.vn/api/v1/";
    private const string UserDataUrl = BaseUrl + "user_data";
    private const string AuthUser = "admin";
    private const string AuthPass = "unimob.com.vn";
    private const string ErrorResult = "Error";

    public void Init(Transform parent = null)
    {
        AppManager.Cloud = this;
        if (parent) transform.SetParent(parent);
    }

    public void GetVersion()
    {
        Debug.Log("Get Version");
        var save = DataManager.Save.User;

#if UNITY_EDITOR
        if (string.IsNullOrEmpty(save.devicedId))
        {
            save.devicedId = SystemInfo.deviceUniqueIdentifier.ToLower() + GameManager.S.ID.ToLower();
            save.Save();
        }
#endif

        CheckIdDevice();

        if (!string.IsNullOrEmpty(save.devicedId))
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(UserDataUrl);
            urlBuilder.Append($"/version?");
            urlBuilder.Append($"uid={save.uId}");
            urlBuilder.Append($"&device_id={save.devicedId}");

            if (!string.IsNullOrEmpty(save.googleId))
                urlBuilder.Append($"&google_id={save.googleId}");

            if (!string.IsNullOrEmpty(save.appleId))
                urlBuilder.Append($"&apple_id={save.appleId}");

            if (!string.IsNullOrEmpty(save.facebookId))
                urlBuilder.Append($"&facebook_id={save.facebookId}");

            if (!string.IsNullOrEmpty(save.name))
                urlBuilder.Append($"&name={save.name}");

            if (!string.IsNullOrEmpty(save.avatar))
                urlBuilder.Append($"&avatar={save.avatar}");

            urlBuilder.Append($"&platform={Application.platform}");
            urlBuilder.Append($"&version={Application.version}");

            StartCoroutine(_GetRequest(urlBuilder.ToString(), GetVersionHandle));

            Debug.Log(save.devicedId);
            Debug.Log(urlBuilder);
        }
    }

    private void CheckIdDevice()
    {
        var save = DataManager.Save.User;
        if (string.IsNullOrEmpty(save.devicedId))
        {
            if (DeviceHelper.GetDeviceId(out var deviceId))
            {
                save.devicedId = deviceId.ToLower();
                save.Save();
            }
            else Debug.Log($"ID: {deviceId.ToLower()} is not support!!!");
        }
    }

    private void GetVersionHandle(string result)
    {
        Debug.Log("============= Get Version Handle =============");
        var save = DataManager.Save.User;

#if UNITY_EDITOR
        Debug.Log(result);
#endif

        if (string.IsNullOrEmpty(result) || result.Equals(ErrorResult))
        {
            OnGetVersionCloud?.Invoke(null);
            return;
        }

        try
        {
            var response = JsonUtility.FromJson<VersionCloudResponse>(result);
            if (response.status == 200)
            {
                save.uId = response.data.uid;
                PlayerPrefs.SetString("AppVersion", response.data.version);
                save.Save();

                var ver = PlayerPrefs.GetString("AppVersion", null);
                if (response.data != null && response.data.code > save.CloudVersion && (string.IsNullOrEmpty(ver) || ver.CompareTo(Application.version) <= 0))
                {
                    OnGetVersionCloud?.Invoke(response.data ?? null);
                }
                else
                {
                    OnGetVersionCloud?.Invoke(null);
                }
            }
            else
            {
                OnGetVersionCloud?.Invoke(null);
            }
        }
        catch (Exception e)
        {
            OnGetVersionCloud?.Invoke(null);
        }
    }

    public void PostUserData()
    {
        var save = DataManager.Save.User;

        var ver = PlayerPrefs.GetString("AppVersion", null);
        if (!string.IsNullOrEmpty(ver) && ver.CompareTo(Application.version) > 0) return;

#if DEVELOPMENT
        return;
#endif
        CheckIdDevice();

        Debug.Log("Post User Data");

        var code = ++save.CloudVersion;
        save.Save();

        var url = UserDataUrl + "/set";
        var form = new WWWForm();

        var userDataCloud = DataManager.Save.GetNewUserDataCloud();
        var data = JsonUtility.ToJson(userDataCloud);

        if (!string.IsNullOrEmpty(save.uId))
            form.AddField("uid", save.uId);
        if (!string.IsNullOrEmpty(save.devicedId))
            form.AddField("device_id", save.devicedId);
        if (!string.IsNullOrEmpty(save.googleId))
            form.AddField("googlde_id", save.googleId);
        if (!string.IsNullOrEmpty(save.appleId))
            form.AddField("apple_id", save.appleId);
        if (!string.IsNullOrEmpty(save.facebookId))
            form.AddField("facebook_id", save.facebookId);
        if (!string.IsNullOrEmpty(save.name))
            form.AddField("name", save.name);
        if (!string.IsNullOrEmpty(save.avatar))
            form.AddField("avatar", save.avatar);
        form.AddField("data", data);
        form.AddField("platform", Application.platform.ToString());
        form.AddField("code", code);
        form.AddField("version", Application.version);

        StartCoroutine(_PostRequest(url, form, PostUserDataHandle));
    }

    private void PostUserDataHandle(string result)
    {
        Debug.Log("============= Post User Data Handle =============");
#if UNITY_EDITOR
        Debug.Log(result);
#endif

        if (string.IsNullOrEmpty(result) || result.Equals(ErrorResult))
        {
            OnPostUserDataCloud?.Invoke(null);
            return;
        }

        try
        {
            var response = JsonUtility.FromJson<UserDataResponse>(result);
            if (response.status == 200)
            {
                var userData = JsonUtility.FromJson<UserDataCloud>(response.data.data);
                OnPostUserDataCloud?.Invoke(userData ?? null);
            }
            else
            {
                OnPostUserDataCloud?.Invoke(null);
            }
        }
        catch (Exception e)
        {
            OnPostUserDataCloud?.Invoke(null);
        }
    }

    public void GetUserData()
    {
        Debug.Log("Get User Data");
        var save = DataManager.Save.User;

        var urlBuilder = new StringBuilder();
        urlBuilder.Append(UserDataUrl);
        urlBuilder.Append($"/get?");
        urlBuilder.Append($"uid={save.uId}");
        urlBuilder.Append($"&device_id={save.devicedId}");

        if (!string.IsNullOrEmpty(save.googleId))
            urlBuilder.Append($"&google_id={save.googleId}");

        if (!string.IsNullOrEmpty(save.appleId))
            urlBuilder.Append($"&apple_id={save.appleId}");

        if (!string.IsNullOrEmpty(save.facebookId))
            urlBuilder.Append($"&facebook_id={save.facebookId}");

        if (!string.IsNullOrEmpty(save.name))
            urlBuilder.Append($"&name={save.name}");

        if (!string.IsNullOrEmpty(save.avatar))
            urlBuilder.Append($"&avatar={save.avatar}");

        urlBuilder.Append($"&platform={Application.platform}");
        urlBuilder.Append($"&version={Application.version}");

        StartCoroutine(_GetRequest(urlBuilder.ToString(), OnGetUserData));
    }

    private void OnGetUserData(string result)
    {
        Debug.Log("============= Get User Data Handle =============");
        var save = DataManager.Save.User;
#if UNITY_EDITOR
        Debug.Log(result);
#endif

        if (string.IsNullOrEmpty(result) || result.Equals(ErrorResult))
        {
            OnGetUserDataCloud?.Invoke(null);
            return;
        }

        try
        {
            var response = JsonUtility.FromJson<UserDataResponse>(result);
            if (response.status == 200)
            {
                if (response.data != null && response.data.code > save.CloudVersion)
                {
                    var userData = JsonUtility.FromJson<UserDataCloud>(response.data.data);
                    OnGetUserDataCloud?.Invoke(userData ?? null);
                }
                else
                {
                    OnGetUserDataCloud?.Invoke(null);
                }
            }
            else
            {
                OnGetUserDataCloud?.Invoke(null);
            }
        }
        catch (Exception e)
        {
            OnGetUserDataCloud?.Invoke(null);
        }
    }

    private IEnumerator _PostRequest(string url, WWWForm form, Action<string> onResult = null, int timeOut = 15)
    {
        using (var request = UnityWebRequest.Post(url, form))
        {
            request.timeout = timeOut;
            request.SetRequestHeader("AUTHORIZATION", Authenticate(AuthUser, AuthPass));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                onResult?.Invoke("Error");
            }
            else
            {
                onResult?.Invoke(request.downloadHandler.text);
            }
        }
    }

    private IEnumerator _GetRequest(string url, Action<string> onResult = null, int timeOut = 15)
    {
        using (var request = UnityWebRequest.Get(url))
        {
            request.timeout = timeOut;
            request.SetRequestHeader("AUTHORIZATION", Authenticate(AuthUser, AuthPass));

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                onResult?.Invoke("Error");
            }
            else
            {
                onResult?.Invoke(request.downloadHandler.text);
            }
        }
    }

    private string Authenticate(string username, string password)
    {
        var auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }
}

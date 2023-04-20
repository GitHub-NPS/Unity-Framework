using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS.Pattern.Observer;
using UnityEngine.UI;

public class UISetting: UIView, IPopup
{
    [SerializeField] private Sprite frameOff;
    [SerializeField] private Sprite frameOn;

    [SerializeField] private GameObject btnMusicOn;
    [SerializeField] private GameObject btnMusicOff;
    [SerializeField] private Image frameMusic;

    [SerializeField] private GameObject btnSoundOn;
    [SerializeField] private GameObject btnSoundOff;
    [SerializeField] private Image frameSound;

    [SerializeField] private Text txtId;

    private GeneralSave general;
    private UserSave user;

    protected override void Init()
    {
        base.Init();
        
        general = DataManager.Save.General;
        user = DataManager.Save.User;        
    }

    public override void Show(object obj = null)
    {
        base.Show(obj);

        UpdateUIMusic();
        UpdateUISound();

        txtId.text = $"ID: {user.uId}";
    }

    private void UpdateUISound()
    {
        frameSound.sprite = general.Sound > 0 ? frameOn : frameOff;
        btnSoundOn.SetActive(general.Sound > 0);
        btnSoundOff.SetActive(general.Sound == 0);
    }

    private void UpdateUIMusic()
    {
        frameMusic.sprite = general.Music > 0 ? frameOn : frameOff;
        btnMusicOn.SetActive(general.Music > 0);
        btnMusicOff.SetActive(general.Music == 0);
    }

    public void ToggleSound()
    {
        AudioManager.S.SetSound(general.Sound == 0 ? 1.0f : 0);
        UpdateUISound();
    }

    public void ToggleMusic()
    {
        AudioManager.S.SetMusic(general.Music == 0 ? 1.0f : 0);
        UpdateUIMusic();
    }

    public void ChangeLanguage()
    {
        MainGameScene.S.Show<UILanguage>();
    }

    public void Rate()
    {
        AppManager.Rate.OpenStoreReview();
        // AppManager.Rate.OpenPromptReview();
    }

    public void JoinFaceBook()
    {
#if UNITY_ANDROID
        Application.OpenURL("fb://group/100087389870338");
#elif UNITY_IOS
        Application.OpenURL("fb://group/?id=100087389870338");
#endif
        var startTime = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - startTime <= 1f)
        {
            Application.OpenURL("https://www.facebook.com/food.fever.unimob");
        }
    }

    public void JoinDiscord()
    {
        Application.OpenURL("discord://invite/7pntbW3bdK");
        var startTime = Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - startTime <= 1f)
        {
            Application.OpenURL("https://discord.gg/7pntbW3bdK");
        }
    }

    public void CopyId()
    {
        user.uId.CopyToClipboard();

        MainGameScene.S.Toast.Show($"Copy to Clipboard: {user.uId}");
    }
}
 
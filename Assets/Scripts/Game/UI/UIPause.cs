using com.unimob.mec;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;

public class UIPause : UIView
{
    [SerializeField] private GameObject iconMusicOn;
    [SerializeField] private GameObject iconMusicOff;
    [SerializeField] private GameObject btnMusicOn;
    [SerializeField] private GameObject btnMusicOff;

    [SerializeField] private GameObject iconSoundOn;
    [SerializeField] private GameObject iconSoundOff;
    [SerializeField] private GameObject btnSoundOn;
    [SerializeField] private GameObject btnSoundOff;

    private float oldTimeScale = 1f;
    private GeneralSave general;

    protected override void Init()
    {
        base.Init();

        general = DataManager.Save.General;
    }

    private void UpdateUISound()
    {
        iconSoundOn.SetActive(general.Sound > 0);
        iconSoundOff.SetActive(general.Sound == 0);
        btnSoundOn.SetActive(general.Sound > 0);
        btnSoundOff.SetActive(general.Sound == 0);
    }

    private void UpdateUIMusic()
    {
        iconMusicOn.SetActive(general.Music > 0);
        iconMusicOff.SetActive(general.Music == 0);
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

    public override void Show(object obj = null)
    {
        base.Show(obj);

        UpdateUIMusic();
        UpdateUISound();

        oldTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        base.Hide();

        Time.timeScale = oldTimeScale;
    }

    public void Home()
    {
        this.gameObject.SetActive(false);

        MainGameScene.S.Show<UIConfirm>();
        MainGameScene.S.View<UIConfirm>().Set(ConfirmType.YesNo, "Title Confirm Home", "Des Confirm Home", true, () =>
        {
            Hide();
        }, () =>
        {
            this.gameObject.SetActive(true);
        });
    }

    public void Replay()
    {
        this.gameObject.SetActive(false);

        MainGameScene.S.Show<UIConfirm>();
        MainGameScene.S.View<UIConfirm>().Set(ConfirmType.YesNo, "Title Confirm Replay", "Des Confirm Replay", true, () =>
        {
            Hide();
        }, () =>
        {
            this.gameObject.SetActive(true);
        });
    }
}

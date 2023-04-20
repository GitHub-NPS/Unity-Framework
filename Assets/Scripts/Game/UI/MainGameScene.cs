using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.unimob.timer;
using UnityEngine;
using NPS.Loot;
using NPS.Pattern.Observer;

public class MainGameScene : GameScene<MainGameScene>
{
    public bool LockTutorial = false;

    [SerializeField] private GameObject Fixed;

    public UIMainGame Main;
    public UIToast Toast;

    public UIFixMain Fix;
    public UILoot Loot;

    private RemoteConfigSave remote;

    private TickData tick = new TickData();

    protected override void Awake()
    {
        base.Awake();
        remote = DataManager.Save.RemoteConfig;

        remote.CheckEvent();

        this.RegisterListener(EventID.StartGameSuccess, StartGameSuccess);
        this.RegisterListener(EventID.StartTutorial, OnStartTutorial);
    }

    private void StartGameSuccess(object obj)
    {
        CheckStartPopup();

        tick.Action = CheckBack;
        tick.RegisterTick();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        this.RemoveListener(EventID.StartGameSuccess, StartGameSuccess);
        this.RemoveListener(EventID.StartTutorial, OnStartTutorial);

        tick.RemoveTick();
    }

    private UIView view = null;

    public void ShowFixed(UIView view, bool value = true)
    {
        if (value == Fixed.activeSelf) return;
        if (!value && this.view == null)
        {
            this.view = view;
            Fixed.SetActive(value);
        }

        if (value && view == this.view)
        {
            this.view = null;
            Fixed.SetActive(value);
        }
    }

    private List<IPopup> popups = new List<IPopup>();

    public override void Show<V>(object obj = null)
    {
        base.Show<V>(obj);

        if (views[typeof(V)] is IPopup)
        {
            var v = views[typeof(V)] as IPopup;

            if (popups.Contains(v))
                popups.Remove(v);
            popups.Add(v);
        }
    }

    private List<UIView> backs = new List<UIView>();

    public override void Show(UIView view)
    {
        base.Show(view);

        if (view.IsBack && !backs.Contains(view))
            backs.Add(view);
    }

    public override void Hide(UIView view)
    {
        base.Hide(view);

        if (view.IsBack && backs.Contains(view))
            backs.Remove(view);

        if (view is IPopup)
        {
            var ui = view as IPopup;
            if (popups.Contains(ui))
                popups.Remove(ui);
        }
    }

    private void CheckBack()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        if (!LockTutorial && Input.GetKeyDown(KeyCode.Escape))
        {
            if (backs.Count > 0)
                backs[backs.Count - 1].Back();
            else
            {
                Show<UIConfirm>();
                View<UIConfirm>().Set(ConfirmType.YesNo, "Title Confirm Quit Game", "Des Confirm Quit Game", true, () =>
                {
                    Application.Quit();
                });
            }
        }
#endif
    }

    private void OnStartTutorial(object obj)
    {
        foreach (var item in popups.ToList())
        {
            (item as UIView).Hide();
        }

        popups.Clear();
    }

    private Queue<Action> starts = new Queue<Action>();

    private void CheckStartPopup()
    {
        CheckOffline();

        ShowStartPopup();
    }

    public void ShowStartPopup()
    {
        if (starts.Count > 0)
            starts.Dequeue().Invoke();
    }

    private void CheckOffline()
    {
        // starts.Enqueue(() => Show<UIOfflineReward>(new Tuple<double, TimeSpan, TimeSpan>(coin, time, sum)));
    }
}
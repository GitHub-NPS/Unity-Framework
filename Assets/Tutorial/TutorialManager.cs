using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPS;
using System;

public class TutorialManager : MonoSingleton<TutorialManager>
{
    private TutorialSave save;
    private UserSave userSave;

    private void Awake()
    {
        save = DataManager.Save.Tutorial;
        userSave = DataManager.Save.User;
    }

    public void Clear()
    {
        inits.Clear();
        nexts.Clear();
        completes.Clear();
    }

    #region Start

    private Dictionary<int, Action> inits = new Dictionary<int, Action>();
    private List<int> active = new List<int>();

    public void RegisterInit(int tut, Action action)
    {
        if (save.Complete.Contains(tut)) return;

        if (!inits.ContainsKey(tut))
            inits.Add(tut, null);

        inits[tut] += action;

        //Debug.Log("Tutorial: Register Init: " + tut);
    }

    public bool Init(int tut)
    {
        bool rs = ConditionInit(tut);
        if (rs)
        {
            //Debug.Log("Tutorial: Init: " + tut);

            active.Add(tut);

            save.CurTut = tut;
            save.CurStep = 1;

            if (inits.ContainsKey(tut))
            {
                inits[tut]?.Invoke();
                inits.Remove(tut);
            }
        }

        return rs;
    }

    private bool ConditionInit(int tut)
    {
        if (active.Contains(tut)) return false;
        if (save.Complete.Contains(tut)) return false;

        switch (tut)
        {
            case 1:
                return false;
        }

        return false;
    }

    #endregion

    #region Next

    private Dictionary<int, Action> nexts = new Dictionary<int, Action>();

    public void RegisterNext(int tut, int step, Action action)
    {
        if (save.Complete.Contains(tut)) return;

        int key = tut * 100 + step;
        if (!nexts.ContainsKey(key))
            nexts.Add(key, null);

        nexts[key] += action;

        //Debug.Log("Tutorial: Register Next: " + tut + " / " + step);
    }

    public void Next(int tut, params int[] steps)
    {
        foreach (var step in steps)
        {
            Check(tut, step, () =>
            {
                //Debug.Log("Tutorial: Next: " + tut + " / " + step);

                bool rs = Handler(tut, step);

                int key = tut * 100 + step;
                if (nexts.ContainsKey(key))
                {
                    nexts[key]?.Invoke();
                    nexts.Remove(key);
                }

                if (!rs) save.CurStep++;
            });
        }
    }

    private bool Handler(int tut, int step)
    {
        switch (tut)
        {
            case 1:
                {
                    switch (step)
                    {
                        case 3:
                            //UITutorial.S.HideHand();
                            //Complete(save.CurTut);
                            return true;
                    }
                }
                break;
        }

        return false;
    }

    public bool Check(int tut, int step, Action action = null)
    {
        if (save.Complete.Contains(tut)) return false;
        if (save.CurTut != tut || save.CurStep != step) return false;

        action?.Invoke();

        return true;
    }

    #endregion

    #region Complete

    private Dictionary<int, Action> completes = new Dictionary<int, Action>();

    public void RegisterComplete(int tut, Action action)
    {
        if (save.Complete.Contains(tut)) return;

        if (!completes.ContainsKey(tut))
            completes.Add(tut, null);

        completes[tut] += action;

        //Debug.Log("Tutorial: Register Complete: " + tut);
    }

    private void Complete(int tut)
    {
        if (save.Complete.Contains(tut)) return;

        if (save.CurTut == tut)
        {
            //Debug.Log("Tutorial: Complete: " + tut);

            if (completes.ContainsKey(tut))
            {
                completes[tut]?.Invoke();
                completes.Remove(tut);
            }

            save.Complete.Add(tut);

            save.CurTut = 0;
            save.CurStep = 0;

            save.Save();
        }
    }

    #endregion
}

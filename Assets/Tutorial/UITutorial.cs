using MEC;
using NPS;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    public static UITutorial S;

    #region Properties

    [Header("UI")] [SerializeField] private GameObject blackLock;
    [SerializeField] private GameObject transparentLock;
    [SerializeField] private TextMeshProUGUI txt;

    [SerializeField] private HandTutorialUI handUIPrefab;
    [SerializeField] private HandTutorialObject handObjectPrefab;

    [SerializeField] private GameObject objSkip;
    [SerializeField] private GameObject objTap2Continue;

    [SerializeField] private BoxChatTutorial boxChatPrefab;

    [Header("Properties")] private HandTutorial hand;
    private BoxChatTutorial boxChat;
    private CoroutineHandle handMove;

    #endregion

    private void Awake()
    {
        if (!S) S = this;
    }

    #region Handler 1
    public void Skip()
    {
        HideHand();
        HideBoxChat();
    }

    public void Tap2Continue()
    {
        objTap2Continue.SetActive(false);
        HideHand();
        HideBoxChat();
    }
    #endregion

    #region Handler 2
    public void ShowLock(LockType type)
    {
        blackLock.SetActive(type == LockType.Black);
        transparentLock.SetActive(type == LockType.Transparent);
    }

    public void ShowHand(bool isUI, HandType handT, LockType lockT, bool isSkip, GameObject parent, params GameObject[] objs)
    {
        CreateHand(isUI, handT, parent.transform);
        ShowLock(lockT);
        ShowSkip(isSkip);
        if (objs.Length == 0) RayCast(parent);
        else
        {
            for (int i = 0; i < objs.Length; i++)
            {
                RayCast(objs[i]);
            }
        }

        if (!isUI) LeanTutorial.S.Enable(false);
    }

    public void ShowHand(bool isUI, HandType handT, LockType lockT, bool isSkip, GameObject parent, Transform start, Transform end, params GameObject[] objs)
    {
        CreateHand(isUI, handT, parent.transform);
        ShowLock(lockT);
        ShowSkip(isSkip);

        for (int i = 0; i < objs.Length; i++)
        {
            RayCast(objs[i]);
        }

        if (handT == HandType.Move && hand)
        {
            handMove = Timing.RunCoroutine(hand._Move(end, start, true), Segment.RealtimeUpdate);
        }

        if (!isUI) LeanTutorial.S.Enable(false);
    }

    private void ShowTap2Continue()
    {
        objTap2Continue.SetActive(true);
    }    

    public void ShowBoxChat(string message, Transform parent)
    {
        HideBoxChat();

        boxChat = Instantiate(boxChatPrefab, parent);

        boxChat.Set(message);
    }

    public void ShowText(string content)
    {
        txt.text = content;
        txt.gameObject.SetActive(true);
    }

    private void ShowSkip(bool isShow)
    {
        objSkip.SetActive(isShow);
    }

    public void HideText()
    {
        txt.gameObject.SetActive(false);
    }

    private void HideBoxChat()
    {
        if (boxChat) Destroy(boxChat.gameObject);
    }

    public void HideHand(bool isClear = true)
    {
        if (handMove.IsValid) Timing.KillCoroutines(handMove);

        if (hand) Destroy(hand.gameObject);

        if (isClear) Clear();

        LeanTutorial.S?.Enable();
    }
    #endregion

    #region Handler 3
    List<GraphicRaycaster> lstRc = new List<GraphicRaycaster>();
    List<Tuple<Canvas, bool, int>> lstCv = new List<Tuple<Canvas, bool, int>>();
    List<Tuple<Canvas, bool, int>> lstOldCv = new List<Tuple<Canvas, bool, int>>();
    List<GameObjectLayer> lstOldLayer = new List<GameObjectLayer>();
    List<Tuple<SortingGroup, int>> lstSg = new List<Tuple<SortingGroup, int>>();
    List<Tuple<SortingGroup, int>> lstOldSg = new List<Tuple<SortingGroup, int>>();

    public void RayCast(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogWarning("[Tutorial]: Raycast null");
            return;
        }

        obj.SetActive(true);

        int layer = obj.layer;
        if (layer == 5)
        {
            Canvas cv = obj.GetComponent<Canvas>();
            if (cv == null)
            {
                cv = obj.AddComponent<Canvas>();
                lstCv.Add(new Tuple<Canvas, bool, int>(cv, cv.overrideSorting, cv.sortingOrder));
            }
            else
            {
                lstOldCv.Add(new Tuple<Canvas, bool, int>(cv, cv.overrideSorting, cv.sortingOrder));
            }

            cv.overrideSorting = true;
            cv.sortingOrder = 202;

            GraphicRaycaster rc = obj.GetComponent<GraphicRaycaster>();
            if (rc == null)
            {
                rc = obj.AddComponent<GraphicRaycaster>();
                lstRc.Add(rc);
            }
        }
        else
        {
            ChangeLayer(obj, true);
        }
    }

    public void ChangeLayer(GameObject obj, bool isSave = false)
    {
        iChangeLayer(obj, LayerMask.NameToLayer("UI"), isSave);

        SortingGroup sg = obj.GetComponent<SortingGroup>();
        if (sg == null)
        {
            sg = obj.AddComponent<SortingGroup>();
            lstSg.Add(new Tuple<SortingGroup, int>(sg, sg.sortingOrder));
        }
        else
        {
            lstOldSg.Add(new Tuple<SortingGroup, int>(sg, sg.sortingOrder));
        }

        sg.sortingOrder = 201;
    }

    private void iChangeLayer(GameObject obj, int layer, bool isSave = false)
    {
        if (isSave)
        {
            lstOldLayer.Add(new GameObjectLayer()
            {
                obj = obj,
                layer = obj.layer
            });
        }

        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            iChangeLayer(child.gameObject, layer, isSave);
        }
    }

    private void Clear()
    {
        foreach (var item in lstRc)
        {
            if (item == null) continue;
            Destroy(item);
        }
        foreach (var item in lstCv)
        {
            if (item == null || item.Item1 == null) continue;
            item.Item1.overrideSorting = item.Item2;
            item.Item1.sortingOrder = item.Item3;

            Destroy(item.Item1);
        }
        foreach (var item in lstOldCv)
        {
            if (item == null || item.Item1 == null) continue;

            item.Item1.overrideSorting = item.Item2;
            item.Item1.sortingOrder = item.Item3;

            if (item.Item1.overrideSorting != item.Item2)
            {
                Debug.LogWarning("[Tutorial]: Don't change override sorting");
            }
        }
        foreach (var item in lstOldLayer)
        {
            if (item == null || item.obj == null) continue;
            item.obj.layer = item.layer;
        }
        foreach (var item in lstSg)
        {
            if (item == null || item.Item1 == null) continue;
            item.Item1.sortingOrder = item.Item2;

            Destroy(item.Item1);
        }
        foreach (var item in lstOldSg)
        {
            if (item == null || item.Item1 == null) continue;
            item.Item1.sortingOrder = item.Item2;
        }

        ShowLock(LockType.None);
        ShowSkip(false);

        lstRc.Clear();
        lstCv.Clear();
        lstOldCv.Clear();
    }

    private class GameObjectLayer
    {
        public GameObject obj;
        public int layer;
    }

    private void CreateHand(bool isUI, HandType type, Transform parent, bool isClear = true)
    {
        HideHand(isClear);

        hand = Instantiate(isUI ? (HandTutorial)handUIPrefab : handObjectPrefab, parent);

        hand.Set(type);
    }
    #endregion
}
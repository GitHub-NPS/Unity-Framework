using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public static class Utils
{
    public static bool FindLayer(this int layer, params LayerInt[] layers)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (layer == (int)layers[i]) return true;
        }
        return false;
    }

    public static int FindLayer(params LayerBit[] layers)
    {
        int result = 0;
        for (int i = 0; i < layers.Length; i++)
        {
            result += (int)layers[i];
        }
        return result;
    }

    public static List<ITag> GetObject<T>(this RaycastHit2D[] rs, List<ObjectTag> tags, bool inMain = false, int limit = -1)
    {
        int count = 0;
        List<ITag> objs = new List<ITag>();

        for (int i = 0; i < rs.Length; i++)
        {
            T t = GetObject<T>(rs[i].collider, tags, inMain);
            if (t != null)
            {
                count++;
                if (limit != -1 && count > limit)
                {
                    break;
                }

                objs.Add(rs[i].collider.GetComponent<ITag>());
            }
        }

        return objs;
    }

    public static List<ITag> GetObject<T>(this Collider2D[] cl, List<ObjectTag> tags, bool inMain = false, int limit = -1)
    {
        int count = 0;
        List<ITag> objs = new List<ITag>();

        for (int i = 0; i < cl.Length; i++)
        {
            T t = GetObject<T>(cl[i], tags, inMain);
            if (t != null)
            {
                count++;
                if (limit != -1 && count > limit)
                {
                    break;
                }

                objs.Add(cl[i].GetComponent<ITag>());
            }
        }

        return objs;
    }

    public static T GetObject<T>(this Collider2D cl, List<ObjectTag> tags, bool inMain = false)
    {
        IMain iMain = cl.GetComponent<IMain>();
        if ((inMain && iMain == null) || (!inMain && iMain != null)) return default;

        ITag iTag = cl.GetComponent<ITag>();
        if (iTag != null && tags.Contains(iTag.GetTag()))
        {
            T t = cl.GetComponent<T>();
            if (t != null)
            {
                return t;
            }
        }

        return default;
    }

    public static Vector2 GetRandomPosition(Transform from, float radius)
    {
        NavMeshHit hit;
        float x = 0;
        float y = 0;

        int count = 0;
        while (from)
        {
            Vector2 posFrom = from.position;

            float angle = Random.Range(0, 2 * Mathf.PI);
            x = radius * Mathf.Cos(angle) + posFrom.x;
            y = radius * Mathf.Sin(angle) + posFrom.y;

            if (NavMesh.SamplePosition(new Vector3(x, y), out hit, 0.5f, NavMesh.AllAreas))
            {
                return hit.position;
            }

            count++;
            if (count > 1000) { Debug.LogWarning("Not Get Random Position Agent"); x = posFrom.x; y = posFrom.y; break; }
        }
        return new Vector2(x, y);
    }

    public static Vector2 GetCamPosition(int x, int y)
    {
        return ProCameraManager.S.Camera.ScreenToWorldPoint(new Vector3(x, y, ProCameraManager.S.Camera.nearClipPlane));
    }
}

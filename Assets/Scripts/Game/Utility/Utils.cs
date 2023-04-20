using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPS.Loot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public static class Utils
{
    public static Vector3 GetRandomPosition(Vector3 start, float radius, Collider2D collider)
    {
        float x = 0;
        float y = 0;
        NavMeshHit hit;

        int angle = Random.Range(0, 360);
        int max = angle + 360;

        while (true)
        {
            float a = angle * Mathf.Deg2Rad;
            x = radius * Mathf.Cos(a) + start.x;
            y = radius * Mathf.Sin(a) + start.y;

            bool rs = collider.OverlapPoint(new Vector2(x, y));
            if (rs)
            {
                if (NavMesh.SamplePosition(new Vector3(x, y), out hit, 0.5f, NavMesh.AllAreas))
                {
                    x = hit.position.x;
                    y = hit.position.y;
                    break;
                }
            }

            angle++;
            if (angle > max)
            {
                radius -= 0.5f;

                angle -= 360;
                if (radius == 0)
                    break;
            }
        }
        return new Vector3(x, y, 0);
    }

    public static Vector2 GetCamPosition(int x, int y)
    {
        return ProCameraManager.S.Camera.ScreenToWorldPoint(new Vector3(x, y, ProCameraManager.S.Camera.nearClipPlane));
    }

    public static Color HtmlToColor(string html)
    {
        ColorUtility.TryParseHtmlString(html, out Color color);
        return color;
    }

    public static string Serialize(this object data)
    {
        return JsonConvert.SerializeObject(data);
    }

    public static T Deserialize<T>(this string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static void Dump(this object data)
    {
#if UNITY_EDITOR
        if (data == null)
        {
            Debug.Log("Null");
            return;
        }
        Debug.Log(data.GetType());
        Debug.Log(Serialize(data));
#endif
    }

    public static T Parse<T>(this JObject obj)
    {
        return obj.ToObject<T>();
    }

    public static List<T> Parse<T>(this JArray arr)
    {
        List<T> result = new List<T>();
        foreach (JObject obj in arr)
        {
            result.Add(obj.ToObject<T>());
        }

        return result;
    }

    public static List<LootData> Merge(this List<LootData> data)
    {
        List<LootData> result = new List<LootData>();
        foreach (var item in data)
        {
            var reward = result.Find(x => x.Same(item));
            if (reward == null)
                result.Add(item.Clone());
            else
                reward.Data.Add(item.Data);
        }

        return result;
    }

    public static List<LootData> Extract(this List<LootData> data)
    {
        List<LootData> result = new List<LootData>();
        foreach (var item in data)
        {
            result.Add(item.Clone());
        }

        result = result.Merge();

        return result;
    }

    private static Renderer point;

    public static bool IsObjectVisible(this Vector3 position)
    {
        if (!point)
        {
            var obj = MonoBehaviour.Instantiate(new GameObject());
            obj.name = "Point Visible";
            obj.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            point = obj.AddComponent<SpriteRenderer>();
            point.sortingOrder = -100;
        }

        point.gameObject.transform.position = position;
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), point.bounds);
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

    public static string GetCurrency(CurrencyType type = CurrencyType.Coin)
    {
        return type.ToString();
    }

    public static void CopyToClipboard(this string str)
    {
        GUIUtility.systemCopyBuffer = str;
    }
}

public enum LayerBit
{
    Dish = 65536,
}
public enum LayerInt
{
    Dish = 16,
}

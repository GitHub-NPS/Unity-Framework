using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPS
{
    public static class MathHelper
    {
        #region Convert

        /// <summary>
        /// Chuyển String về Int
        /// </summary>
        public static int ParseInt(this string value)
        {
            int result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }

            return result;
        }

        /// <summary>
        /// Chuyển Char về Int
        /// </summary>
        public static int ParseInt(this char value)
        {
            int result = 0;
            result = 10 * result + (value - 48);
            return result;
        }

        #endregion

        #region List

        /// <summary>
        /// Trả về giá trị random trong List
        /// </summary>
        public static T RandomValue<T>(this IList<T> lst)
        {
            if (lst.Count > 0) return lst[Random.Range(0, lst.Count)];
            return default;
        }

        /// <summary>
        /// Trả về giá trị vị trí random trong List
        /// </summary>
        public static T RandomIndex<T>(this IList<T> lst)
        {
            if (lst.Count > 0) return lst[Random.Range(0, lst.Count)];
            return default;
        }

        /// <summary>
        /// Chuẩn hóa mảng tỷ lệ
        /// </summary>
        public static List<float> RateNormalized(this List<float> list)
        {
            var rs = new List<float>();
            float sum = 0;
            foreach (var item in list)
            {
                sum += item;
                rs.Add(sum);
            }
            return rs;
        }

        /// <summary>
        /// Trả về vị trí ngẫu nhiên mảng tỷ lệ
        /// </summary>
        public static int RandomIndexRate(this List<float> rate)
        {
            float random = Random.Range(0, 1f);
            for (int i = 0; i < rate.Count; i++)
            {
                if (rate[i] >= random)
                {
                    return i;
                }
            }
            return 0;
        }

        /// <summary>
        /// Trộn mảng
        /// </summary>
        public static void Shuffle<T>(this List<T> idxs)
        {
            for (int i = 0; i < idxs.Count - 1; i++)
            {
                int random = Random.Range(i, idxs.Count);
                idxs.Swap(i, random);
            }
        }

        private static void Swap<T>(this List<T> idxs, int a, int b)
        {
            T temp = idxs[a];
            idxs[a] = idxs[b];
            idxs[b] = temp;
        }

        #endregion

        #region Price

        /// <summary>
        /// Compare Double
        /// </summary>
        public static int CompareInt(this double a, double b)
        {
            if (a < 1000 && b < 1000)
            {
                a = Mathf.CeilToInt((float)a);
                b = Mathf.CeilToInt((float)b);
            }

            return a == b ? 0 : ((a > b) ? 1 : -1);
        }

        public static bool Compare(this double a, double b)
        {
            return a.CompareInt(b) >= 0;
        }

        /// <summary>
        /// Double to Score
        /// </summary>
        public static string Show(this double value)
        {
            if (value < 1000)
            {
                return Mathf.CeilToInt((float)value) + "";
            }

            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (value < 1000)
                    break;
                else value = Math.Floor(value / 100d) / 10d;

            if (value == Math.Floor(value))
                result = value.ToString() + ScoreNames[i];
            else result = value.ToString("F1") + ScoreNames[i];
            return result;
        }

        /// <summary>
        /// Decimal to Price
        /// </summary>
        public static string PriceShow(this decimal price)
        {
            string result = string.Format(price == Decimal.ToInt32(price) ? "{0:n0}" : "{0:n}", price);
            return result;
        }

        #endregion

        #region Time

        /// <summary>
        /// TimeSpan to String
        /// </summary>
        public static string Show(this TimeSpan time)
        {
            string str = "";
            if (time.Days > 0) str += time.Days + "d ";
            if (time.Hours > 0) str += time.Hours + "h ";
            if (time.Minutes > 0) str += time.Minutes + "m ";
            if (time.Days == 0 && time.Seconds > 0) str += time.Seconds + "s";

            return str;
        }

        #endregion

        #region Geometry

        /// <summary>
        /// Trả về bình phương cạnh huyền
        /// </summary>
        public static float SqrMagnitude(this Vector3 from, Vector3 to)
        {
            Vector3 dis = to - from;
            return SqrMagnitude(dis);
        }

        public static float SqrMagnitude(this Vector3 vector)
        {
            Vector3 dis = new Vector3(vector.x, vector.y);
            dis.x = Mathf.Abs(dis.x);
            dis.y = Mathf.Abs(dis.y);

            return dis.x * dis.x + dis.y * dis.y;
        }

        /// <summary>
        /// Trả về góc giữa 2 Vector2
        /// </summary>
        public static float AngleTo(this Vector3 from, Vector3 to)
        {
            Vector2 dir = to - from;
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Chuẩn hóa vector max 1
        /// </summary>
        public static Vector3 MaxNormalized(this Vector3 vector)
        {
            float x = Mathf.Abs(vector.x);
            float y = Mathf.Abs(vector.y);

            float rs;
            if (x > y)
            {
                if (x <= 0.1) x = 0.1f;

                rs = 1 / x;
                x = 1;
                y *= rs;
            }
            else
            {
                if (y <= 0.1) y = 0.1f;

                rs = 1 / y;
                y = 1;
                x *= rs;
            }

            return new Vector3((vector.x > 0 ? 1 : -1) * x, (vector.y > 0 ? 1 : -1) * y);
        }

        #endregion
    }
}

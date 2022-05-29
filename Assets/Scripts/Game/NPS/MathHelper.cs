using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NPS
{
    public static class MathHelper
    {
        /// <summary>
        /// Chuyển String về Int
        /// </summary>
        public static int IntParseFast(string value)
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
        public static int IntParseFast(char value)
        {
            int result = 0;
            result = 10 * result + (value - 48);
            return result;
        }

        /// <summary>
        /// Trả về giá trị random trong List
        /// </summary>
        public static T RandomValueInList<T>(List<T> lst)
        {
            if (lst.Count > 0) return lst[Random.Range(0, lst.Count)];
            return default;
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

        /// <summary>
        /// Int to Score
        /// </summary>
        public static string ScoreShow(float Score)
        {
            string result;
            string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
            int i;

            for (i = 0; i < ScoreNames.Length; i++)
                if (Score < 900)
                    break;
                else Score = Mathf.Floor(Score / 100f) / 10f;

            if (Score == Mathf.Floor(Score))
                result = Score.ToString() + ScoreNames[i];
            else result = Score.ToString("F1") + ScoreNames[i];
            return result;
        }

        /// <summary>
        /// TimeSpan to String
        /// </summary>
        public static string TimeShow(TimeSpan time)
        {
            string str = "";
            if (time.Days > 0)
            {
                str += time.Days + "Day";
            }
            str += time.Hours + ":";
            str += time.Minutes + ":";
            str += time.Seconds;

            return str;
        }

        /// <summary>
        /// Trả về bình phương cạnh huyền
        /// </summary>
        public static float SqrMagnitude(Vector3 from, Vector3 to)
        {
            Vector3 dis = to - from;
            dis.x = Mathf.Abs(dis.x);
            dis.y = Mathf.Abs(dis.y);

            return dis.x * dis.x + dis.y * dis.y;
        }

        /// <summary>
        /// Trả về góc giữa 2 Vector2
        /// </summary>
        public static float Angle(Vector2 from, Vector2 to)
        {
            Vector2 dir = to - from;
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        /// <summary>
        /// Chuẩn hóa mảng tỷ lệ
        /// </summary>
        public static List<float> Rate(List<float> rate)
        {
            var rs = new List<float>();
            float sum = 0;
            foreach (var item in rate)
            {
                sum += item;
                rs.Add(sum);
            }
            return rs;
        }

        /// <summary>
        /// Trả về vị trí ngẫu nhiên mảng tỷ lệ
        /// </summary>
        public static int RandomIndexInRate(List<float> rate)
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
    }
}

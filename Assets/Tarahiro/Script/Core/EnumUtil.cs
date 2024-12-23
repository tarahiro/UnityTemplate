using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public static class EnumUtil
    { 
        /// <summary>
      /// 入力されたkeyが設定されたタイプに含まれるか
      /// </summary>
        public static bool ContainsKey<T>(string tagetKey)
        {

            foreach (T t in Enum.GetValues(typeof(T)))
            {
                if (t.ToString() == tagetKey)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 入力されたkeyと同じ列挙型の項目を取得する
        /// </summary>
        public static T KeyToType<T>(string targetKey)
        {
            Log.DebugAssert(ContainsKey<T>(targetKey), targetKey + "が存在しません");
            return (T)Enum.Parse(typeof(T), targetKey);
        }

        /// <summary>
        /// 入力されたNoと同じ列挙型の項目を取得する
        /// </summary>
        public static T NoToType<T>(int targetNo)
        {
            return (T)Enum.ToObject(typeof(T), targetNo);
        }

        /// <summary>
        /// 指定した列挙型の項目数を取得する
        /// </summary>
        public static int GetTypeNum<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }

    }
}

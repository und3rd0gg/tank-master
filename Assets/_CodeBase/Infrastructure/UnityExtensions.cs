using System;
using UnityEngine;
using Random = System.Random;

namespace TankMaster._CodeBase.Infrastructure
{
    public static class UnityExtensions
    {
        private static Random _random;

        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }

        public static bool RandomChance(int chance)
        {
            if (_random == null)
            {
                _random = new Random();
            }

            return _random.NextDouble() < chance / 100.0;
        }

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[]) Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
        
        public static class Remap
        {
            public static float DoRemap(float aMin, float aMax, float bMin, float bMax, float v)
            {
                float t = InvLerp(aMin, aMax, v);
                return Lerp(bMin, bMax, t);
            }

            private static float Lerp(float a, float b, float t)
            {
                return (1.0f - t) * a + b * t;
            }

            private static float InvLerp(float a, float b, float v)
            {
                return (v - a) / (b - a);
            }
        }
    }
}
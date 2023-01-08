using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Utils
{
    public static class TransformationUtils
    {
        public static Vector3 RotateVector2D(this Vector3 vector, float angle)
        {
            RotateVectorInternal(ref vector.x, ref vector.y, angle);
            return vector;
        }

        public static Vector2 RotateVector2D(this Vector2 vector, float angle)
        {
            RotateVectorInternal(ref vector.x, ref vector.y, angle);
            return vector;
        }

        private static void RotateVectorInternal(ref float x, ref float y, float angle)
        {
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            float sx = sin * x;
            float sy = sin * y;
            float cx = cos * x;
            float cy = cos * y;
            x = cx - sy;
            y = sx + cy;
        }
    }
}
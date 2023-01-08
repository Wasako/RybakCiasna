using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom2D.Utils
{
    public static class BBUtils
    {
        public static Rect GetRotatedBB2D(this Mesh mesh, float angle)
        {
            Vector2 min = new Vector2(Mathf.Infinity, Mathf.Infinity);
            Vector2 max = new Vector2(-Mathf.Infinity, -Mathf.Infinity);

            foreach (var vertex in mesh.vertices)
            {
                var rotated = vertex.RotateVector2D(angle);
                min.x = Mathf.Min(min.x, rotated.x);
                min.y = Mathf.Min(min.y, rotated.y);
                
                max.x = Mathf.Max(max.x, rotated.x);
                max.y = Mathf.Max(max.y, rotated.y);
            }

            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static Rect GetTransformedBB2D(this Mesh mesh, Matrix4x4 transform)
        {
            /*Vector2 min = new Vector2(Mathf.Infinity, Mathf.Infinity);
            Vector2 max = new Vector2(-Mathf.Infinity, -Mathf.Infinity);
*/
            /*foreach (var vertex in mesh.vertices)
            {
                var rotated = transform.MultiplyPoint3x4(vertex);
                min.x = Mathf.Min(min.x, rotated.x);
                min.y = Mathf.Min(min.y, rotated.y);

                max.x = Mathf.Max(max.x, rotated.x);
                max.y = Mathf.Max(max.y, rotated.y);
            }*/

            var bounds = mesh.bounds;

            var min = bounds.min;
            var max = bounds.max;

            var rectPos = new Vector2(transform.m03, transform.m13);

            var rectMin = rectPos;
            var rectMax = rectPos;

            for (int i = 0; i < 2; i++) 
                for (int j = 0; j < 3; j++)
                {
                    float a = transform[i, j] * min[j];
                    float b = transform[i, j] * max[j];
                    rectMin[i] += a < b ? a : b;
                    rectMax[i] += a < b ? b : a;
                }

            return Rect.MinMaxRect(rectMin.x, rectMin.y, rectMax.x, rectMax.y);
        }
    }
}
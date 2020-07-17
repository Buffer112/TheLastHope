using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastHope.Helpers
{
    public class BezierCurve : MonoBehaviour
    {

        [SerializeField] Vector3[] points;
        public Vector3[] Points { get { return points; } set { points = value; } }

        public void Reset()
        {
            Points = new Vector3[] {
                        new Vector3(1f, 0f, 0f),
                        new Vector3(2f, 0f, 0f),
                        new Vector3(3f, 0f, 0f),
                        new Vector3(4f, 0f, 0f)};
        }

        public Vector3 GetPoint(float t)
        {
            return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
        }

        public Vector3 GetVelocity(float t)
        {
            return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) -
                transform.position;
        }

        public Vector3 GetDirection(float t)
        {
            return GetVelocity(t).normalized;
        }

        public Quaternion GetQuaternionDirection(float t)
        {
            return Quaternion.FromToRotation(Vector3.forward,GetDirection(t));
        }
    }
}


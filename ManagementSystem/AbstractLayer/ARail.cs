using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheLastHope.Helpers;
using TheLastHope.Management.Data;

namespace TheLastHope.Management.AbstractLayer
{
	/// <summary>
	/// Abstract rail class.
	/// Use this class to create new rails with parameters.
	/// </summary>
	public abstract class ARail : MonoBehaviour
    {

        [SerializeField] BezierCurve curve;
        [SerializeField] float length;
        public BezierCurve Curve { get { return curve; } set { curve = value;} }
        public float Length { get { return length; } set { length = value; } }
        [SerializeField] RailType type;
        public RailType Type
        {
            get { return type; }
            set { type = value; }

        }

    }
}


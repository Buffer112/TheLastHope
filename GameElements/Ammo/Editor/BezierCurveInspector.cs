﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TheLastHope.Helpers;


[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private BezierCurve curve;
    private Transform handleTransform;
    private Quaternion handleRotation;
    private const int lineSteps = 10;

    private void OnSceneGUI()
    {
        curve = target as BezierCurve;
        handleTransform = curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);
        Vector3 p3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p2, p3);

        Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

        //Just for understanding
        //Handles.color = Color.white;
        //Vector3 lineStart = curve.GetPoint(0f);
        //for (int i = 1; i <= lineSteps; i++)
        //{
        //    Vector3 lineEnd = curve.GetPoint(i / (float)lineSteps);
        //    Handles.DrawLine(lineStart, lineEnd);
        //    lineStart = lineEnd;
        //}
    }

    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(curve.Points[index]);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(curve, "Move Point");
            EditorUtility.SetDirty(curve);
            curve.Points[index] = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }



}

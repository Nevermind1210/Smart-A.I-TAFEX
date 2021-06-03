using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfVision))]
public class FeildOfViewEditor : Editor
{
   private void OnSceneGUI()
   {
      FieldOfVision fov = (FieldOfVision)target;
      Handles.color = Color.white;
      Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360,fov.viewRadius);
      Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
      Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);
   }
}

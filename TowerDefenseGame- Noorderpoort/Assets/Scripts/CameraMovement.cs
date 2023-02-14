using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
   public float panSpeed = 20f;
   public float panBorderThickness = 10f;
   public Vector2 panLimit;

   private void Update()
   {
      Vector3 pos = transform.position;

      if (Input.GetKey("w"))
      {
         pos.z += panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("s"))
      {
         pos.z -= panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("d"))
      {
         pos.x += panSpeed * Time.deltaTime;
      }
      if (Input.GetKey("a"))
      {
         pos.x -= panSpeed * Time.deltaTime;
      }

      pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
      pos.z = Mathf.Clamp(pos.z, -pos.y, pos.y);
      transform.position = pos;
   }
}

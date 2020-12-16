using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpGRPC.services;
using Msg;

public class RobotPathShow : MonoBehaviour
{
   public void drawPath(RBPath request)
   {
      GameObject exsistingPath = GameObject.Find("robotPath");
      if (exsistingPath == null)
      {
         exsistingPath = new GameObject("robotPath");
         exsistingPath.transform.parent = this.transform;
      }
      else
      {
         Destroy(exsistingPath);
         exsistingPath = new GameObject("robotPath");
         exsistingPath.transform.parent = this.transform;
      }
      
      Google.Protobuf.Collections.RepeatedField<Point> points = request.Pos.Clone();
      
      LineRenderer path = exsistingPath.AddComponent<LineRenderer>();
      path.positionCount = points.Count;
      path.startWidth = 0.2f;
      path.endWidth = 0.2f;

      int i = 0;

      foreach (Point point in points)
      {
         float posx = (float)(point.Posx / 10.0);
         float posy = (float)(point.Posy / 10.0);
         GameObject pointMark = GameObject.CreatePrimitive(PrimitiveType.Sphere);
         pointMark.transform.parent = exsistingPath.transform;
         pointMark.transform.localScale = new Vector3((float)0.5, (float)0.5, (float)0.5);
         pointMark.transform.position = new Vector3(posx, (float)0.4, posy);
         path.SetPosition(i, pointMark.transform.position);
         i++;
      }
   }
}

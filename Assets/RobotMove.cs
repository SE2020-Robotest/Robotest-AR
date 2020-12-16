using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CSharpGRPC.services;
using Msg;

public class RobotMove : MonoBehaviour
{
    public void updateRobotPosi(RBPosition request)
    {
        double angle = request.Angle;
        double vx = request.Vx;
        double vy = request.Vy;
        double timestamp = request.Timestamp;
        Point position = request.Pos;

        Transform robot = this.transform;
        
        robot.position = new Vector3((float)position.Posx, 1, (float)position.Posy);
        robot.localEulerAngles = new Vector3(0, (float)angle, 0);
    }
}

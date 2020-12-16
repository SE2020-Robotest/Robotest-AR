﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1.5f;//控制移动速度
    public Transform m_transform;

    // Use this for initialization
    void Start () {
        m_transform = this.transform;
    }
	
    // Update is called once per frame
    void Update () {
        //向左
        if (Input.GetKey(KeyCode.A))
        {
            m_transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        //向右
        if (Input.GetKey(KeyCode.D))
        {
            m_transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        //向前
        if (Input.GetKey(KeyCode.W))
        {
            m_transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        //向后
        if (Input.GetKey(KeyCode.S))
        {
            m_transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        */
        if (Input.GetKey(KeyCode.Space))
        {
            m_transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}

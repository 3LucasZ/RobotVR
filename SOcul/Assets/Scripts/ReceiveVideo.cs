﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class ReceiveVideo : MonoBehaviour
{
    UdpClient mySock;
    IPEndPoint otherIPEP;

    RawImage dynamicImage;
    Texture2D texture;
    byte[] data;

    void Start()
    {
        otherIPEP = new IPEndPoint(IPAddress.Any, Config.BOT_CAMERA_PORT);
        dynamicImage = GameObject.Find("DynamicImage").GetComponent<RawImage>();
        texture = new Texture2D(1, 1);

        new Thread(() =>
        {
            mySock = new UdpClient(Config.OPERATOR_CAMERA_PORT);
            while (true)
            {
                data = mySock.Receive(ref otherIPEP);
            }
        }).Start();
    }

    void Update()
    {
        if (data != null)
        {
            texture.LoadImage(data);
            dynamicImage.texture = texture;
        }
    }
}

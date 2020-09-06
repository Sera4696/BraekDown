﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateScr : MonoBehaviour
{
    [SerializeField] public float breakpoint;
    public bool breakensflag;
    public static plateScr plate;
    // Start is called before the first frame update
    void Start()
    {
        plate = this;
        breakensflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        var utils = playerUtilScr.utils;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        var playerutils = playerUtilScr.utils;
        // var Player = PlayerScr.player;

        //もしも、playerの持つ力より耐久値が小さければ
        if (playerutils.breakpower >= breakpoint)
        {
            breakensflag = true;
            //Debug.Log(breakpoint);
            Destroy(gameObject);
        }
    }
}

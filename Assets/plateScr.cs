using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateScr : MonoBehaviour
{
    [SerializeField] public float breakpoint;

    public static plateScr plate;
    // Start is called before the first frame update
    void Start()
    {
        plate = this;
    }

    // Update is called once per frame
    void Update()
    {
        var utils = playerUtilScr.utils;    
    }
}

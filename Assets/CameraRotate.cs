using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            transform.RotateAround(player.transform.position, Vector3.up, -0.5f);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.RotateAround(player.transform.position, Vector3.up, 0.5f);
        }


        if (Input.GetKey(KeyCode.I))
        {
            transform.RotateAround(player.transform.position, Vector3.right, -0.5f);
        }

        else if (Input.GetKey(KeyCode.K))
        {
            transform.RotateAround(player.transform.position, Vector3.right, 0.5f);
        }
    }
}

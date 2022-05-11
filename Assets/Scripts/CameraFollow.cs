using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [SerializeField] private float distance = 5; 

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPos = target.position;

        float yPos = Mathf.Max(targetPos.y * 2.5f, 4);
 
        // no lerping for now
        var pos = targetPos - (target.forward * distance);
        pos.y = yPos;

        transform.position = pos;
        transform.LookAt(target);
    }
}

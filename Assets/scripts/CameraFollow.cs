using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // 主角的 GameObject 引用
    public Vector3 offset; // 摄像头与主角的偏移量
    private Rigidbody2D playerRigidbody; // 主角的 Rigidbody2D 组件引用
    private Vector3 velocity = Vector3.zero; // 平滑插值过程中使用的速度变量

    void Start()
    {
        // 获取主角的 Rigidbody2D 组件
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; // ���ǵ� GameObject ����
    public Vector3 offset; // ����ͷ�����ǵ�ƫ����
    private Rigidbody2D playerRigidbody; // ���ǵ� Rigidbody2D �������
    private Vector3 velocity = Vector3.zero; // ƽ����ֵ������ʹ�õ��ٶȱ���

    void Start()
    {
        // ��ȡ���ǵ� Rigidbody2D ���
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dis = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dis.z = this.transform.position.z; //�̶�z��
        this.transform.position = dis; //ʹ�����������ƶ�
    }
}

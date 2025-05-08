using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AttackBox1;
    public GameObject AttackBox2;
    void Start()
    {
        AttackBox1.SetActive(false);
        AttackBox2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenAttackBox1()
    {
        AttackBox1.SetActive(true);
    }

    public void CloseAttackBox1()
    {
        AttackBox1.SetActive(false);
    }

    public void OpenAttackBox2()
    {
        AttackBox2.SetActive(true);
    }

    public void CloseAttackBox2()
    {
        AttackBox2.SetActive(false);
    }
}

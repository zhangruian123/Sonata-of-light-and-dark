using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS1Attack : MonoBehaviour
{
    public GameObject Attack1Box;
    public GameObject Attack2Box;
    public GameObject BigAttackBox;
    // Start is called before the first frame update
    void Start()
    {
        Attack1Box.SetActive(false);
        Attack2Box.SetActive(false);
        BigAttackBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openAttack1Box()
    {
        Attack1Box.SetActive(true);
    }

    public void openAttack2Box()
    {
        Attack2Box.SetActive(true);
    }

    public void openBigAttackBox()
    {
        BigAttackBox.SetActive(true);
    }

    public void closeAttack1Box()
    {
        Attack1Box.SetActive(false);
    }

    public void closeAttack2Box()
    {
        Attack2Box.SetActive(false);
    }

    public void closeBigAttackBox()
    {
        BigAttackBox.SetActive(false);
    }
}

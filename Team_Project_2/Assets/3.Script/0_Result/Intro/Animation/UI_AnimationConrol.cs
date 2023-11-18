using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnimationConrol : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Intro intro;

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        intro = FindObjectOfType<Intro>();
    }


    public void Check_WarningEnd()
    {
        Debug.Log("��� ��������");
        intro.WarningPanel_Off();
    }
}

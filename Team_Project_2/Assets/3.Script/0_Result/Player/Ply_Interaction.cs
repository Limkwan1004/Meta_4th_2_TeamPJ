using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ply_Interaction : MonoBehaviour
{
    // �÷��̾� ��ȣ�ۿ�

    // ����
    // 1. ���� �� ��� �� ����
    // 2. ���� �� ����Ʈ�� �ӵ� ����
    // 3. �ֺ� ���� ���� ���� ���� �����̴� ����


   [SerializeField] private Occupation occupation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Flag"))
        {
            occupation.ObjEnable(true);

            StopCoroutine(occupation.UnOccu_co());
            StartCoroutine(occupation.Occu_co());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Flag"))
        {
            
            occupation.ObjEnable(false);
            StopCoroutine(occupation.Occu_co());
            StartCoroutine(occupation.UnOccu_co());
        }
    }
    


    
    
    
}

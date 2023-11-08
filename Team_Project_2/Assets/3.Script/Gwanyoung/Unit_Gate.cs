using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Gate : MonoBehaviour
{
    // �÷��̾� ��ȣ�ۿ�

    // ����
    // 1. ���� �� ��� �� ����
    // 2. ���� �� ����Ʈ�� �ӵ� ����
    // 3. �ֺ� ���� ���� ���� ���� �����̴� ����

    [SerializeField] private Gate gate;   // �� ���� ��ũ��Ʈ
    [SerializeField] private Text gateUI; // �� ���� Text


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            gate = other.gameObject.GetComponentInParent<Gate>();

            if (gameObject.CompareTag("Player"))
            {
                gateUI.gameObject.SetActive(true);  // gateUI Ȱ��ȭ

                if (Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(gate.Gate_Interaction());
                }                
            }
            else
            {
                StartCoroutine(gate.Gate_Interaction());
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gate") && gameObject.CompareTag("Player")) 
        {
            gateUI.gameObject.SetActive(false);  // gateUI ��Ȱ��ȭ        
        }
        gate = null;
    }
}

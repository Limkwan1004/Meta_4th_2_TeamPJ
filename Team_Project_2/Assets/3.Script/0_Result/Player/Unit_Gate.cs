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

    private GameObject gateUI; // �� ���� Text
    public bool isMyGate = false;

    private void Start()
    {
        gateUI = GameObject.Find("GateUI");
        gateUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate") && gameObject.CompareTag("Player"))
        {
            gateUI.gameObject.SetActive(true);  // gateUI Ȱ��ȭ 
            isMyGate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gate") && gameObject.CompareTag("Player")) 
        {
            gateUI.gameObject.SetActive(false);  // gateUI ��Ȱ��ȭ        
            isMyGate = false;
        }
    }
}

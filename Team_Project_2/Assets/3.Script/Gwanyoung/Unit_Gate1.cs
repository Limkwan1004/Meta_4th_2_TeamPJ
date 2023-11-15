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
    [SerializeField] private GameObject gateUI; // �� ���� Text

    private void OnEnable()
    {
        gateUI = GameObject.Find("Doorui");
    }

    private void Start()
    {
        gateUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gate"))
        {
            gate = other.gameObject.GetComponent<Gate>();
            if (gameObject.CompareTag("Player"))
            {
                gateUI.gameObject.SetActive(true);  // gateUI Ȱ��ȭ                   
            }
            StartCoroutine(gate.Gate_Interaction(this.gameObject.layer));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gate") && gameObject.CompareTag("Player")) 
        {
            gateUI.gameObject.SetActive(false);  // gateUI ��Ȱ��ȭ        
        }
        if (gate != null) StopCoroutine(gate.Gate_Interaction(this.gameObject.layer));
        gate = null;
    }
}

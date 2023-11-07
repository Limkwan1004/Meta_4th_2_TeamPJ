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

    [SerializeField] private Occupation occupation; // ���� ��ũ��Ʈ
    [SerializeField] private DoorInter Doorinter;   // �� ���� ��ũ��Ʈ
    [SerializeField] private Text Doorui;


    private void OnTriggerEnter(Collider other)
    {
        occupation = other.gameObject.GetComponentInChildren<Occupation>();
        Debug.Log("�ε�����?1");
        if (other.gameObject.CompareTag("Flag"))
        {
        Debug.Log("�ε�����?2");
            if (gameObject.CompareTag("Player"))
            {
                occupation.ObjEnable(true);
            }
            occupation.isOccupating = true;
            StopCoroutine(occupation.UnOccu_co());
            StartCoroutine(occupation.Occu_co());
        }
        if (other.gameObject.CompareTag("Door"))
        {
            if (gameObject.CompareTag("Player"))
            {
                Doorui.gameObject.SetActive(true);
            }
            StartCoroutine(Doorinter.OpenDoor_co());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            if (gameObject.CompareTag("Player"))
            {
                occupation.ObjEnable(false);
            }
            StopCoroutine(occupation.Occu_co());
            StartCoroutine(occupation.UnOccu_co());
        }
        if (other.gameObject.CompareTag("Door"))
        {
            if (gameObject.CompareTag("Player"))
            {
                Doorui.gameObject.SetActive(false);
            }
            StopCoroutine(Doorinter.OpenDoor_co());
        }
    }
    
}

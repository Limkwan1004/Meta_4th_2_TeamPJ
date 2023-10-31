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

    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject OpenDoorui;



    // ���ɰ��� ������
    [SerializeField] Slider OccuValue; // ���� ������
    private float Num_Person = 1.03f; // ��� ���� ���� ����
    public float occu_Speed = 15f; // ���� �ӵ�
    private float Total_Gauge = 100f; // ��ü ���� ������
    private float Current_Gauge = 0;  // ���� ���� ������
    bool isOccupating = false; // ���� ������


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Flag"))
        {
            OccuValue.gameObject.SetActive(true);
            isOccupating = true;
            StartCoroutine(Interacting());
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Flag"))
        {
            isOccupating = false;
            StopCoroutine(Interacting());
            StartCoroutine(noInteracting());
        }
    }
    private IEnumerator Interacting()
    {

        // ���� ��
        while (isOccupating && Current_Gauge <= 100f)
        {
            Current_Gauge += Time.deltaTime * occu_Speed; // ���߿� �ο����� ���� ���� �־���ؿ�
            Debug.Log(Current_Gauge);
            OccuValue.value = Current_Gauge / Total_Gauge;
            yield return null; 
        }

    }
    private IEnumerator noInteracting()
    {
        OccuValue.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        while (!isOccupating && Current_Gauge >= 0f)
        {
            Current_Gauge -= Time.deltaTime * occu_Speed;
            OccuValue.value = Current_Gauge / Total_Gauge;


            yield return null;
        }
    }


    
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupationManager : MonoBehaviour
{
    // 1. ���� �� ����Ʈ�� �ӵ� ����
    // 2. �ֺ� ���� ���� ���� ���� �����̴� ����

    public Flag[] FlagArray;  // �÷��� ������Ʈ �迭

    [HideInInspector] public float Num_Soldier = 1.03f; // ��� ���� ���� ����
    [HideInInspector] public float occu_Speed = 12f; // ���� �ӵ�


   // private void Awake()
   // {
   //     FlagArray = FindObjectsOfType<Flag>();
   //     
   // }
   // private void Update()
   // {
   //     if (Current_Gauge / Total_Gauge >= 1 && !isOccupied)
   //     {
   //         isOccupied = true;
   //     }
   //
   // }
   // public IEnumerator Occu_co()
   // {
   //     // ���� ��
   //     while (isOccupating && Current_Gauge <= 100f)
   //     {
   //         Current_Gauge += Time.deltaTime * occu_Speed * Num_Soldier; // ���߿� �ο����� ���� ���� �־���ؿ�
   //         Debug.Log(Current_Gauge);
   //         yield return null;
   //     }
   //
   // }
   // public IEnumerator UnOccu_co()
   // {
   //     yield return new WaitForSeconds(3.0f);
   //     while (!isOccupied && !isOccupating && Current_Gauge >= 0f)
   //     {
   //         Current_Gauge -= Time.deltaTime * occu_Speed;
   //         yield return null;
   //     }
   // }


}

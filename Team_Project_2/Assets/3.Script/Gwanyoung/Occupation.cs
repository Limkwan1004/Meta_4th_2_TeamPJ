using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Occupation : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����
    // 2. ���� �� ����Ʈ�� �ӵ� ����
    // 3. �ֺ� ���� ���� ���� ���� �����̴� ����


    [Header("���")]
    [SerializeField] GameObject Flag; // ����� ���� ������Ʈ


    SkinnedMeshRenderer skinnedmesh;

    [Header("������ �÷��迭(Material)")]
    [SerializeField] private Material[] Flag_Color; // ��� ���ٲ� Marterial

    public float Num_Person = 1.03f; // ��� ���� ���� ����
    public float occu_Speed = 5f; // ���� �ӵ�

    public float Total_Gauge = 100f;
    public float Current_Gauge = 0;


    private void Start()
    {
        skinnedmesh = Flag.GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
      //  OccuValue.value = Current_Gauge / Total_Gauge;
        //skinnedmesh.material = Flag_Color[0];
    }
 


}

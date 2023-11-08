using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����       

    [HideInInspector] public float Total_Gauge = 100f; // ��ü ���� ������
    [HideInInspector] public float Current_Gauge = 0;  // ���� ���� ������

    [HideInInspector] public bool isOccupating = false; // ���� ������
    [HideInInspector] public bool isOccupied = false; // ������ ��������
    private SkinnedMeshRenderer skinnedmesh;

    private void Awake()
    {
        TryGetComponent<SkinnedMeshRenderer>(out skinnedmesh);
    }
    public void Change_Flag_Color(int TeamNum)
    { 
        skinnedmesh.material = ColorManager.instance.Flag_Color[TeamNum];        
    }
}

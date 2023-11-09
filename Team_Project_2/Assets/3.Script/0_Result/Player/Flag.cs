using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����       

    public float Total_Gauge = 100f; // ��ü ���� ������
    public float Current_Gauge = 0;  // ���� ���� ������

    public bool isOccupating = false; // ���� ������
    public bool isOccupied = false; // ������ ��������
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

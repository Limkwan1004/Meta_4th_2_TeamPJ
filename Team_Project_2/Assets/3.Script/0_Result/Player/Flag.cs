using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����       

    public float Total_Gauge = 100f; // ��ü ���� ������
    public float Current_Gauge = 0;  // ���� ���� ������

    public float Soldier_Multi = 1.03f; // ��� ���� ���� ����
    public float occu_Speed = 12f; // ���� �ӵ�

    public bool isOccupating = false; // ���� ������
    public bool isOccupied = false; // ������ ��������

    private SkinnedMeshRenderer skinnedmesh;
    public Unit_Occupation unit_O;
    OccupationHUD OccuHUD;

    private void Awake()
    {
        OccuHUD = FindObjectOfType<OccupationHUD>();
        TryGetComponent<SkinnedMeshRenderer>(out skinnedmesh);
    }
    public void Change_Flag_Color(int TeamNum)
    { 
        skinnedmesh.material = ColorManager.instance.Flag_Color[TeamNum];        
    }
    private void Update()
    {
        if (Current_Gauge / Total_Gauge >= 1 && !isOccupied)
        {
            isOccupied = true;
        }

    }
    
    public IEnumerator OnOccu_co()
    {
        // ���� ��
        unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, true);

        while (isOccupating && Current_Gauge <= 100f)
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(6, unit_O.Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }
        isOccupied = true;
        unit_O.OccuHUD.Change_Color(6, unit_O.Flag_Num);

        this.transform.parent.gameObject.layer = unit_O.gameObject.layer;
        yield return null;
    }
    public IEnumerator OffOccu_co()
    {
        unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, false);
        yield return new WaitForSeconds(3.0f);

        while (!isOccupied && !isOccupating && Current_Gauge >= 0f) // ���� �ߵ� �ƴϰ� 
        {
            Current_Gauge -= Time.deltaTime * occu_Speed;
            yield return null;
        }
        
        yield return null;
    }


}

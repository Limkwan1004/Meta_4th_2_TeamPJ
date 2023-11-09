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

    private int ParentLayer()
    {
        return this.transform.parent.gameObject.layer;
    }
    private int UnitLayer(Unit_Occupation unit)
    {
        return unit.gameObject.layer;
    }


    public IEnumerator OnOccu_co()
    {
        // Case1 �ٸ����� -> �߸� / �߸� -> ��������
        // Case2 �߸� -> ��������

        // ���� ��
        unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, true);

        isOccupating = true;
        // ���������� ������ ��
        while (isOccupied && isOccupating && Current_Gauge >= 0f && !ParentLayer().Equals(UnitLayer(unit_O))) 
        {
            Current_Gauge -= Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(6, unit_O.Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge <= 0f && isOccupied)
        {
            isOccupied = false;   // ������� -> �߸�
            this.transform.parent.gameObject.layer = 0;
        }

        // �߸������� ������ ��
        while (!isOccupied && isOccupating && Current_Gauge <= Total_Gauge) 
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(6, unit_O.Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge >= Total_Gauge && !isOccupied)
        {
            isOccupied = true;   // �߸� -> ��������
            this.transform.parent.gameObject.layer = UnitLayer(unit_O);
        }

        unit_O.OccuHUD.Change_Color(6, unit_O.Flag_Num);

        
        yield return null;
    }
    public IEnumerator OffOccu_co()
    {
        unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, false);
        yield return new WaitForSeconds(3.0f);

        // ���ɵ� ������ �����ϴٰ� ������ ��
        while (isOccupied && !isOccupating && Current_Gauge <= 100f)
        {
            Current_Gauge += Time.deltaTime * occu_Speed;
        }
        // �߸����� ���� �ϴٰ� ������ ��
        while (!isOccupied && !isOccupating && Current_Gauge >= 0f) 
        {
            Current_Gauge -= Time.deltaTime * occu_Speed;
            yield return null;
        }
        
        
        yield return null;
    }


}

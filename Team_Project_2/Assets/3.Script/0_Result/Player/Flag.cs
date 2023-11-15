using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����       

    public float Total_Gauge = 100f; // ��ü ���� ������
    public float Current_Gauge = 0;  // ���� ���� ������
    private int TeamColor_Temp;

    public float Soldier_Multi = 1.03f; // ��� ���� ���� ����
    public float occu_Speed = 12f; // ���� �ӵ�

    public bool isOccupating = false; // ���� ������
    public bool isOccupied = false; // ������ ��������

    private SkinnedMeshRenderer skinnedmesh;
    public Unit_Occupation unit_O;
    private OccupationHUD OccuHUD;

    private void Awake()
    {
        OccuHUD = FindObjectOfType<OccupationHUD>();
        TryGetComponent<SkinnedMeshRenderer>(out skinnedmesh);
    }

    private void Start()
    {       
        transform.parent.gameObject.layer = transform.root.gameObject.layer;
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


    public IEnumerator OnOccu_co(int TeamColor, int Teamlayer)
    {
        // Case1 �ٸ����� -> �߸� / �߸� -> ��������
        // Case2 �߸� -> ��������

        // ���� ��
        if (Teamlayer.Equals(TeamLayerIdx.Player))
        {
            unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, true);
        }

        // ���������� ������ ��
        while (isOccupied && isOccupating && Current_Gauge >= 0f && !ParentLayer().Equals(UnitLayer(unit_O))) 
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(TeamColor_Temp, unit_O.Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge <= 0f && isOccupied)
        {
            isOccupied = false;   // ������� -> �߸�
            OccuHUD.Ply_Slider((int)ColorIdx.White, unit_O.Flag_Num,Current_Gauge,Total_Gauge);
            OccuHUD.Change_Color((int)ColorIdx.White, unit_O.Flag_Num);

            this.transform.parent.gameObject.layer = 0;
        }

        // �߸������� ������ ��
        while (!isOccupied && isOccupating && Current_Gauge <= Total_Gauge) 
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(TeamColor, unit_O.Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge >= Total_Gauge && !isOccupied)
        {
            isOccupied = true;   // �߸� -> ��������
            TeamColor_Temp = TeamColor;
            unit_O.OccuHUD.Change_Color(TeamColor, unit_O.Flag_Num);
            this.transform.parent.gameObject.layer = UnitLayer(unit_O);
        }


        
        yield return null;
    }
    public IEnumerator OffOccu_co(int Teamlayer)
    {
        if (Teamlayer.Equals((int)TeamLayerIdx.Player))
        {
            unit_O.OccuHUD.Ply_OccuHUD(unit_O.Flag_Num, false);
        }
        yield return new WaitForSeconds(3.0f);

        // ���ɵ� ������ �����ϴٰ� ������ ��
        while (isOccupied && !isOccupating && Current_Gauge <= 100f)
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20);
        }
        // �߸����� ���� �ϴٰ� ������ ��
        while (!isOccupied && !isOccupating && Current_Gauge >= 0f) 
        {
            Current_Gauge -= Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20);
            yield return null;
        }
        
        
        yield return null;
    }


}

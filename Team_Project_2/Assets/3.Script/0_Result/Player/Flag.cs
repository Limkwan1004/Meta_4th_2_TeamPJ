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

    public int Flag_Num;

    [SerializeField] private SkinnedMeshRenderer skinnedmesh;
    private OccupationHUD OccuHUD;
    public List<GameObject> Leaders = new List<GameObject>();



    private void Awake()
    {
        OccuHUD = FindObjectOfType<OccupationHUD>();
    }

    private void Start()
    {            
        gameObject.layer = (transform.parent == null) ? 0 : ParentLayer();
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        switch (Leaders.Count)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.instance.isLive)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Leader"))
            {
                if (other.gameObject.CompareTag("Player")) OccuHUD.Ply_OccuHUD(Flag_Num, true);

                Leaders.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Leader"))
        {
            if (other.gameObject.CompareTag("Player")) OccuHUD.Ply_OccuHUD(Flag_Num, false);

            Leaders.Remove(other.gameObject);
        }
    }

    public void Change_Flag_Color(int TeamNum)
    {
        skinnedmesh.material = ColorManager.instance.Flag_Color[TeamNum];
    }

    private int ParentLayer()
    {
        return this.transform.parent.gameObject.layer;
    }

    IEnumerator Occupating_Co()
    {
        yield return null;
    }



    public IEnumerator OnOccu_co(int TeamColor, int Teamlayer)
    {
        // Case1 �ٸ����� -> �߸� / �߸� -> ��������
        // Case2 �߸� -> ��������

        // ���� ��


        // ���������� ������ ��
        while (isOccupied && isOccupating && Current_Gauge >= 0f) 
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(TeamColor_Temp, Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge <= 0f && isOccupied)
        {
            isOccupied = false;   // ������� -> �߸�
            OccuHUD.Ply_Slider((int)ColorIdx.White, Flag_Num,Current_Gauge,Total_Gauge);
            OccuHUD.Change_Color((int)ColorIdx.White, Flag_Num);

            this.transform.parent.gameObject.layer = 0;
        }

        // �߸������� ������ ��
        while (!isOccupied && isOccupating && Current_Gauge <= Total_Gauge) 
        {
            Current_Gauge += Time.deltaTime * occu_Speed * Mathf.Pow(Soldier_Multi, 20); // ���߿� �ο����� ���� ���� �־���ؿ�
            OccuHUD.Ply_Slider(TeamColor,Flag_Num, Current_Gauge, Total_Gauge);
            Debug.Log(Current_Gauge);
            yield return null;
        }

        if (Current_Gauge >= Total_Gauge && !isOccupied)
        {
            isOccupied = true;   // �߸� -> ��������
            TeamColor_Temp = TeamColor;
            OccuHUD.Change_Color(TeamColor, Flag_Num);
           
        }


        
        yield return null;
    }
    public IEnumerator OffOccu_co(int Teamlayer)
    {
        if (Teamlayer.Equals((int)TeamLayerIdx.Player))
        {
            OccuHUD.Ply_OccuHUD(Flag_Num, false);
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

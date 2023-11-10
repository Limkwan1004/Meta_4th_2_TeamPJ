using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Occupation : MonoBehaviour
{ 
    // ������ ���� ��ȣ�ۿ�   
    private Flag flag; // ��� ��ũ��Ʈ
    [HideInInspector] public OccupationHUD OccuHUD; // ����HUD

    [HideInInspector] public int Flag_Num = 0;  // Flag �ε��� 

    public int Team_Color;  // �� Color

    private void Start()
    {
        switch (this.gameObject.layer)
        {
            case (int)TeamLayerIdx.Player:
                Team_Color = GameManager.instance.Color_Index;
                break;
            case (int)TeamLayerIdx.Team1:
                Team_Color = GameManager.instance.T1_Color;
                break;
            case (int)TeamLayerIdx.Team2:
                Team_Color = GameManager.instance.T2_Color;
                break;
            case (int)TeamLayerIdx.Team3:
                Team_Color = GameManager.instance.T3_Color;
                break;
            default:
                return;
        }  

        OccuHUD = FindObjectOfType<OccupationHUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            flag = other.gameObject.GetComponentInChildren<Flag>();            
            flag.unit_O = this;
            flag.isOccupating = true; // ���� �� true

            for (int i = 0; i < OccuHUD.FlagArray.Length; i++)
            {
                if (flag.Equals(OccuHUD.FlagArray[i]))
                {
                    Flag_Num = i;
                    break;
                }
            }
            StartCoroutine(flag.OnOccu_co(Team_Color, this.gameObject.layer));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            flag.isOccupating = false; // ���� �� false
            StartCoroutine(flag.OffOccu_co(this.gameObject.layer));
            flag.unit_O = null;
        }
    }
}

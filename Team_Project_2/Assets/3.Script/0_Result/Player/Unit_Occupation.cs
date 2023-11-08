using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Occupation : MonoBehaviour
{
    // ������ ���� ��ȣ�ۿ�
    

    private Flag flag; // ��� ��ũ��Ʈ
    private OccupationHUD OccuHUD;
    private OccupationManager OccuManager;

    private void Awake()
    {
        OccuHUD = FindObjectOfType<OccupationHUD>();
        OccuManager = FindObjectOfType<OccupationManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        flag = other.gameObject.GetComponentInChildren<Flag>();       
        if (other.gameObject.CompareTag("Flag"))
        {
            if (gameObject.layer.Equals(8))
            {
                Change_Color(2);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            if (gameObject.CompareTag("Player"))
            {
                
            }

        }
    }
    private void Change_Color(int TeamNum)
    {
        flag.Change_Flag_Color(TeamNum);
        OccuHUD.Change_HUD_Color(TeamNum);
    }


}

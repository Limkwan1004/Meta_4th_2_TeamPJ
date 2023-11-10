using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Occupation : MonoBehaviour
{
    // ������ ���� ��ȣ�ۿ�
    

    private Flag flag; // ��� ��ũ��Ʈ
    public OccupationHUD OccuHUD;
    public int Flag_Num=0;

    private void Awake()
    {
        OccuHUD = FindObjectOfType<OccupationHUD>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            flag = other.gameObject.GetComponentInChildren<Flag>();            
            flag.unit_O = this;
            flag.isOccupating = true;

            for (int i = 0; i < OccuHUD.FlagArray.Length; i++)
            {
                if (flag.Equals(OccuHUD.FlagArray[i]))
                {
                    Flag_Num = i;
                    break;
                }
            }

            if (!gameObject.layer.Equals(7)) // ���̾ �÷��̾ �ƴ� ��
            {                
                StartCoroutine(flag.OnOccu_co(6, this.gameObject.layer));
            }
            else // ���̾ �÷��̾� ��   --> �ڷ�ƾ ������ ����ó���ؼ� ���߿� if-else�� ������ ��.
            {
                StartCoroutine(flag.OnOccu_co(11, this.gameObject.layer));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flag"))
        {
            flag.isOccupating = false;
            StartCoroutine(flag.OffOccu_co(this.gameObject.layer));
        }
    }
}

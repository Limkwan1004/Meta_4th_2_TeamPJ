using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private bool isTab;
    [SerializeField]private Image UI;
    //���ݷ� ü�� �̵��ӵ� ü��
    //�÷��̾� ���þ���
    //���� ���þ���
    //���ɼӵ�
    //�����α�����   -> �Ϸ�
    //����������   -> �Ϸ�
    //�������ð�����


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isTab)
            {
                GameManager.instance.Stop();
                UI.gameObject.SetActive(true);
            }
            else
            {
                GameManager.instance.Resume();
                Time.timeScale = 1f;
                UI.gameObject.SetActive(false);
            }
            isTab = !isTab;


        }
    }
    public void UpgradeMaxCountUp()
    {
        GameManager.instance.Max_MinionCount = 24;
        Debug.Log(GameManager.instance.Max_MinionCount);
    }
    public void UpgradeUnitValueUp()
    {
        //ù��° ���׷��̵� �ȵ������� 
        if (!GameManager.instance.isPossible_Upgrade_1) { 
        GameManager.instance.isPossible_Upgrade_1 = true;
        Debug.Log("1��° ���׷��̵� �Ϸ�");
        }
        //ù��° ���׷��̵� �������� 
        if (GameManager.instance.isPossible_Upgrade_1 && !GameManager.instance.isPossible_Upgrade_2)
        {
            GameManager.instance.isPossible_Upgrade_2 = true;
            Debug.Log("2��° ���׷��̵� �Ϸ�");
        }
    }
    public void UpgradeRespawnTime()
    {
        GameManager.instance.respawnTime = 3f;
        Debug.Log("�������ð� 3�ʷ� ����");
    }
    public void UpgradeAtk()
    {
        int UpgradeCount = 0;
        UpgradeCount++;
        GameManager.instance.Damage += 5f;
        Debug.Log("������ 5����");
    }
    public void UpgradeHP()
    {
        int UpgradeCount = 0;
        UpgradeCount++;
        GameManager.instance.Max_Hp += 10f;
        GameManager.instance.Current_HP += 10f;
        Debug.Log("HP10����");

    }
    //Astar�ϼ��� �ٽ� �ۼ�
    public void UpgradeSpeed()
    {
        
    }
    public void UpgradeRegeneration()
    {
        GameManager.instance.Regeneration += 0.5f;
        Debug.Log("ü�¸��� 0.5����");

    }


}

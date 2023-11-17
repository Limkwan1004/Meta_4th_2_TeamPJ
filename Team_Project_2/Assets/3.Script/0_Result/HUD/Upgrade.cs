using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    /*
        1. ���� �ƽ� ī��Ʈ
        2. ��� ������ Ÿ�� ����
        3. �ε��� 1�� ���� ���
        4. �ε��� 2�� ���� ���
        5. ���� ���ݷ� ����
        6. ���� ü�� ����
        7. ���� ���ݷ� ����
        8. ���� ü�� ����
        9. �̵��ӵ� ����
        10. ��� ���� ����
    */

    [SerializeField] private UpgradeData[] Data;
    [SerializeField] private Text[] Name_Texts;
    [SerializeField] private Image[] Icon_Images;
    [SerializeField] private Text[] Des_Texts;
    [SerializeField] private Text[] Cost_Texts;
    [SerializeField] private Button[] UpIndex_Buttons;


    [SerializeField] private Image UI;
    [SerializeField] private Text Damage;
    [SerializeField] private Text HP;
    [SerializeField] private GameObject Upgrade1_Ob;
    [SerializeField] private GameObject Upgrade2_Ob;
    [SerializeField] private GameObject Content;
    [SerializeField] private Button[] buttons;
    int DamageUpgradeCount = 0;
    int HPUpgradeCount = 0;

    private void OnEnable()
    {
        for(int i = 0; i < UpIndex_Buttons.Length; i++)
        {
            if (i == 2)
            {
                Name_Texts[i].text = GameManager.instance.unit1.unitName;
                Des_Texts[i].text = GameManager.instance.unit1.unitName + Data[i].Upgrade_Des;
            }
            else if (i == 3) 
            {
                Name_Texts[i].text = GameManager.instance.unit2.unitName;
                Des_Texts[i].text = GameManager.instance.unit2.unitName + Data[i].Upgrade_Des;
            }
            else
            {
                Name_Texts[i].text = Data[i].Upgrade_Name;
                Des_Texts[i].text = Data[i].Upgrade_Des;
            }
            Icon_Images[i].sprite = Data[i].Upgrade_Icon;
            Cost_Texts[i].text = $"Cost : {Data[i].Upgrade_Cost} gold";
        }
    }

    public void Upgrade_MaxCountUp()
    {
        if (GameManager.instance.Gold >= Data[0].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[0].Upgrade_Cost;
            GameManager.instance.Max_MinionCount += (int)Data[0].Value;
            UpIndex_Buttons[0].interactable = false;
            Debug.Log(GameManager.instance.Max_MinionCount);
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
    }

    public void Upgrade_RespawnTime()
    {
        if (GameManager.instance.Gold >= Data[1].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[1].Upgrade_Cost;
            GameManager.instance.respawnTime -= Data[1].Value;
            UpIndex_Buttons[1].interactable = false;
            Debug.Log(GameManager.instance.respawnTime);
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Upgrade_Sol1()
    {
        if(GameManager.instance.Gold >= Data[2].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[2].Upgrade_Cost;
            GameManager.instance.isPossible_Upgrade_1 = true;
            Upgrade1_Ob.SetActive(true);
            UpIndex_Buttons[2].interactable = false;
            Debug.Log("1��° ���׷��̵� �Ϸ�");
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Upgrade_Sol2()
    {
        if (GameManager.instance.Gold >= Data[3].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[3].Upgrade_Cost;
            GameManager.instance.isPossible_Upgrade_2 = true;
            Upgrade2_Ob.SetActive(true);
            UpIndex_Buttons[3].interactable = false;
            Debug.Log("2��° ���׷��̵� �Ϸ�");
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Upgrade_LeaderATK()
    {
        if (GameManager.instance.Gold >= Data[4].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[4].Upgrade_Cost;
            GameManager.instance.Damage += Data[4].Value;
            UpIndex_Buttons[4].interactable = false;
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Upgrade_LeaderHP()
    {
        if (GameManager.instance.Gold >= Data[5].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[5].Upgrade_Cost;
            GameManager.instance.Max_Hp += Data[5].Value;
            GameManager.instance.Current_HP += Data[5].Value;
            UpIndex_Buttons[5].interactable = false;
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    public void Upgrade_SolDAM()
    {
        if (GameManager.instance.Gold >= Data[6].Upgrade_Cost)
        {
            GameManager.instance.Gold -= Data[6].Upgrade_Cost;
            
            UpIndex_Buttons[5].interactable = false;
        }
    }

    public void Upgrade_SolHP()
    {

    }

    //Astar�ϼ��� �ٽ� �ۼ�
    public void Upgrade_Speed()
    {

    }
  
    public void Upgrade_Income()
    {

    }

}

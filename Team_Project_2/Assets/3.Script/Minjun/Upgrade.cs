using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private bool isTab;
    [SerializeField] private Image UI;
    [SerializeField] private Text Damage;
    [SerializeField] private Text HP;
    [SerializeField] private GameObject Upgrade1_Ob;
    [SerializeField] private GameObject Upgrade2_Ob;
    [SerializeField] private GameObject Content;
    private Button[] buttons;
    int DamageUpgradeCount = 0;
    int HPUpgradeCount = 0;
    //���ݷ� ü�� �̵��ӵ� ü��
    //�÷��̾� ���þ���
    //���� ���þ���
    //���ɼӵ�
    //�����α�����   -> �Ϸ�
    //����������   -> �Ϸ�
    //�������ð�����
    private void Awake()
    {
        buttons = Content.GetComponentsInChildren<Button>();
       
    }

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
        if (GameManager.instance.Gold >= 200)
        {
            GameManager.instance.Gold -= 200;
            GameManager.instance.Max_MinionCount = 24;
            buttons[0].interactable = false;
            Debug.Log(GameManager.instance.Max_MinionCount);
        }
        else
        {
            
            Debug.Log("��尡 �����մϴ�.");
            return;
        }


    }
    public void UpgradeUnitValueUp()
    {
        if (GameManager.instance.Gold >= 200)
        {
            GameManager.instance.Gold -= 200;
            //ù��° ���׷��̵� �ȵ������� 
            if (!GameManager.instance.isPossible_Upgrade_1)
            {
                GameManager.instance.isPossible_Upgrade_1 = true;
                Upgrade1_Ob.SetActive(true);
                buttons[1].interactable = false;
                Debug.Log("1��° ���׷��̵� �Ϸ�");
            }
            else       //ù��° ���׷��̵� �������� 
            if (GameManager.instance.isPossible_Upgrade_1 && !GameManager.instance.isPossible_Upgrade_2)
            {
                GameManager.instance.isPossible_Upgrade_2 = true;
                Upgrade2_Ob.SetActive(true);
                buttons[2].interactable = false;
                Debug.Log("2��° ���׷��̵� �Ϸ�");
            }
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
    public void UpgradeRespawnTime()
    {
        if (GameManager.instance.Gold >= 200)
        {
            GameManager.instance.Gold -= 200;
            GameManager.instance.respawnTime = 3f;
            buttons[3].interactable = false;
            Debug.Log("�������ð� 3�ʷ� ����");
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
    public void UpgradeAtk()
    {
        if (GameManager.instance.Gold >= 200)
        {
            GameManager.instance.Gold -= 200;
            if (DamageUpgradeCount < 5)
            {
                DamageUpgradeCount++;
                int currentUpgrade = DamageUpgradeCount * 5;
                GameManager.instance.Damage += 5f;
                Debug.Log("������ 5����");
                Damage.text = string.Format("������ ���ݷ��� 5 �����մϴ�. ���� {0}% / �ִ�25", currentUpgrade);
                if (DamageUpgradeCount == 5)
                {
                    buttons[4].interactable = false;
                }
            }
            else
            {
                Debug.Log("���׷��̵� �ִ�ġ�� �����߽��ϴ�.");


            }
         
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
    public void UpgradeHP()
    {
        if (GameManager.instance.Gold >= 200)
        {
            GameManager.instance.Gold -= 200;
            if (HPUpgradeCount < 5)
            {
                HPUpgradeCount++;
                int currentUpgrade = HPUpgradeCount * 10;
                GameManager.instance.Max_Hp += 10f;
                GameManager.instance.Current_HP += 10f;
                Debug.Log("HP10����");
                if (HPUpgradeCount == 5)
                {
                    buttons[5].interactable = false;
                }
                HP.text = string.Format("������ HP�� 10% ����մϴ�. ���� {0}% / �ִ�50%", currentUpgrade);
            }
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
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

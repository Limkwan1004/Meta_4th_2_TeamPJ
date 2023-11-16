using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttoninfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Unit_Information unitInfo;
    public GameObject infoPanel;
    public Text[] texts;

 



    //public float offsetX = f; // ���ʰ� ������������ ������
    //public float offsetY = 0f; // ���ʰ� �Ʒ��������� ������
    private void Start()
    {
        infoPanel.SetActive(false);
        texts = infoPanel.GetComponentsInChildren<Text>();
    }

  

    public void OnPointerEnter(PointerEventData eventData)
    {

        infoPanel.SetActive(true);

        ShowUnitInfo(unitInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.SetActive(false);
        HideUnitInfo();
    }

    private void Update()
    {
        // Update �޼��忡���� �߰����� ������ �ʿ� �����ϴ�.

        if (infoPanel.activeSelf) 
        {
            Vector3 mousePosition = Input.mousePosition;
            infoPanel.transform.position = mousePosition;
        }
    }

    private void ShowUnitInfo(Unit_Information info)
    {
        // info�� ����Ͽ� ������ �гο� ǥ��
        //0 : �̸� 
        //1 : ����
        //2 : HP
        //3 : ���ݷ�
        //4 : ���
        //5 : ��Ÿ�

        texts[0].text = info.unitName;
        texts[2].text = $"{info.maxHP}\n{info.damage}\n{info.cost}\n{info.attackRange}";
        texts[3].text = $"{info.description}";
    }

    private void HideUnitInfo()
    {
        // ���� �г��� ��Ȱ��ȭ
    }
   
}

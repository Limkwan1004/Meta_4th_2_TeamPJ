using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccupationHUD : MonoBehaviour
{
    // ���� HUD
    // 1. ������ ������ �ΰ��� ��ܿ� ������Ȳ ImageColor ����
    // 2. �÷��̾����״� ���� �� Slider�� UI ǥ�õǵ���   

    [SerializeField] private Image[] Occu_Img_Color; // ���� ���� �� ��
    public Flag[] FlagArray;  // �÷��� ������Ʈ �迭
    public Slider OccuSlider;
    private Color colorTemp;

    private void Awake()
    {
        FlagArray = FindObjectsOfType<Flag>();
        
        Occu_Img_Color = GetComponentsInChildren<Image>();
        OccuSlider = GetComponentInChildren<Slider>();
        for (int i = 0; i < 5; i++)
        {
            Occu_Img_Color[(i * 2) + 1].transform.parent.gameObject.SetActive(false);
        }

    }

    private void Update()
    {

    }

    public void ObjEnable(bool act)
    {
        Occu_Img_Color[1].transform.parent.gameObject.SetActive(act);

    }

    public void Change_Color(int TeamNum, int FlagNum)
    {        
        colorTemp = ColorManager.instance.Teamcolor[TeamNum];
        colorTemp.a = 0.4f;

        Occu_Img_Color[FlagNum * 2].color = colorTemp;
        Occu_Img_Color[FlagNum * 2 + 1].color = colorTemp;
        FlagArray[FlagNum].Change_Flag_Color(TeamNum);

    }


}

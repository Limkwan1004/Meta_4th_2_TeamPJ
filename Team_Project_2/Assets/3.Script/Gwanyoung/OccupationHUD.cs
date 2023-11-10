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
    public Slider[] OccuSlider;
    private Color ColorTemp;

    private void Awake()
    {
        FlagArray = FindObjectsOfType<Flag>();        
        Occu_Img_Color = GetComponentsInChildren<Image>();
        OccuSlider = GetComponentsInChildren<Slider>();

        for (int i = 0; i < Occu_Img_Color.Length * 0.5f; i++) 
        {
            Occu_Img_Color[(i * 2) + 1].transform.parent.gameObject.SetActive(false);
           
        }
    }    

    public void Ply_Slider(int TeamColor, int FlagNum, float Current, float Total)
    {
        ColorTemp = ColorManager.instance.Teamcolor[TeamColor];
        ColorTemp.a = 0.431f;

        Occu_Img_Color[FlagNum * 4 + 2].color = ColorTemp; 
        OccuSlider[FlagNum].value = Current / Total;   // �����̴� ���� ������
    }
 
    public void Ply_OccuHUD(int FlagNum, bool Act)
    {
        // �������� ������ Player �� �� SetActive�� �Ұ��� �ֱ� ���� �޼���
        Occu_Img_Color[FlagNum * 4 + 1].transform.parent.gameObject.SetActive(Act);  // ���� �߾��� HUD
        Occu_Img_Color[FlagNum * 4 + 3].transform.parent.gameObject.SetActive(Act);  // ���� �����̴�
    }

    public void Change_Color(int TeamColor, int FlagNum)
    {        
        ColorTemp = ColorManager.instance.Teamcolor[TeamColor];
        ColorTemp.a = 0.431f;

        Occu_Img_Color[FlagNum * 4].color = ColorTemp; // HUD ���               

        Occu_Img_Color[FlagNum * 4 + 1].color = ColorTemp; // �÷��̾�� �� HUD   

        FlagArray[FlagNum].Change_Flag_Color(TeamColor); // ��� �� ����

    }


}

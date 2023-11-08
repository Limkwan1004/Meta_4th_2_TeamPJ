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
    private OccupationManager OccuManager;
    public Slider OccuSlider;
    private Color colorTemp;

    private void Awake()
    {
        Occu_Img_Color = GetComponentsInChildren<Image>();
        OccuManager = FindObjectOfType<OccupationManager>();
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

    public void Change_HUD_Color(int TeamNum)
    {        
        colorTemp = ColorManager.instance.Teamcolor[TeamNum];
        colorTemp.a = 0.4f;

        Occu_Img_Color[0].color = colorTemp;       
        Occu_Img_Color[1].color = colorTemp;
    }


}

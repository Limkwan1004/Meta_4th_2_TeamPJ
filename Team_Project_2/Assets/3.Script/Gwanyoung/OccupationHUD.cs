using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccupationHUD : MonoBehaviour
{
    // ���� HUD
    // 1. ������ ������ �ΰ��� ��ܿ� ������Ȳ ImageColor ����
    // 2. �÷��̾����״� ���� �� Slider�� UI ǥ�õǵ���   


    [Header("�� ����")]
    [SerializeField] private Image[] Occu_Img_Color; // ���� ���� �� ��

    private void Awake()
    {
        Occu_Img_Color = GetComponentsInChildren<Image>();

    }

    private void Update()
    {

    }

    public void ObjEnable(bool act)
    {
        Occu_Img_Color[1].transform.parent.gameObject.SetActive(act);

    }

    public void Change_HUD_Color(int Teamcolor)
    {
        // ���߿� �÷����� ����

        Occu_Img_Color[0].color = ColorManager.instance.Teamcolor[Teamcolor];
        Occu_Img_Color[1].color = ColorManager.instance.Teamcolor[Teamcolor];
    }


}

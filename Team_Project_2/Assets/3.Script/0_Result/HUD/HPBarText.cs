using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarText : TextManager
{
    // HP Text�� Slider�� ����
    // ���� ü��, ��ü ü��, ü�����

    private float Total_HP = 150f;  // �ӽ� �ִ�ü��
    [Range(0f,150f)]
    public float Current_HP = 150f; // �ӽ� ����ü��

    private float Regeneration = 0.5f;   // �ӽ� ü�����

    private Image SliderImg;
    public Gradient gradient;

    [SerializeField] private Slider HPBar;

    private void Awake()
    {
        SliderImg = HPBar.fillRect.GetComponent<Image>();
    }
    private void Update()
    {
        HPBar.value = Current_HP / Total_HP;

        Textarray[0].color = gradient.Evaluate(Current_HP / Total_HP);
        SliderImg.color = gradient.Evaluate(Current_HP / Total_HP); // ����ü�� ��� ü�¹� ������
        Textarray[0].text = $"{(int)Current_HP}<color=FFFFFF>/{(int)Total_HP}</color>";   // ����ü�� / �� ü��
        Textarray[1].text = string.Format("+{0:0.00}", Regeneration);

    }


}

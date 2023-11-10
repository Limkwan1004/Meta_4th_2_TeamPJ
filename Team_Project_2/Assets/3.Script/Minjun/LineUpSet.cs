using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineUpSet : MonoBehaviour
{

    [SerializeField]
    //���ּ��� ��ư�� 6��
    private Button[] buttons;
    //�� ��ư���� üũ�̹���
    private GameObject[] Checkbox;
    //���� ���ξ� ��������Ʈ �ε���
    public List<int> lineupIndexs = new List<int>();
    [Header("Sword > Heavy > Archer > Priest > Spear > Halberdier > Default")]
    //���ֽ�������Ʈ �迭
    [SerializeField] private Sprite[] unitSprite_array;
    //���ξ� UI ��������
    [SerializeField] private GameObject lineupUI;
    //���ξ� UI �� �ִ� ���� ���� ���ֵ�(�̹���)
    private Image[] lineupSprite;
    private Color originalColor;
    void Start()
    {

        lineupSprite = lineupUI.GetComponentsInChildren<Image>();
        buttons = GetComponentsInChildren<Button>();
        Checkbox = new GameObject[buttons.Length]; //
        originalColor = buttons[1].colors.normalColor; // ���� ���� ����

        for (int i = 0; i < buttons.Length; i++)
        {
            Checkbox[i] = buttons[i].transform.GetChild(0).gameObject;
        }


        lineupIndexs.Add(0);




    }
    private void Update()
    {

        for (int i = 0; i < 3; i++)
        {
            try
            {
                // ���� ���� ���� i��° ��������Ʈ = �������� �ε��� ��°�� ���ֽ�������Ʈ
                lineupSprite[i].sprite = unitSprite_array[lineupIndexs[i]];
            }
            catch
            {
                // lineupIndex �� ����Ʈ�� ��������� Default ��������Ʈ �Ҵ�
                lineupSprite[i].sprite = unitSprite_array[6];
            }
        }

    }

    public void ButtonClicked(int buttonIndex)
    {
        if (lineupIndexs.Count < 3)
        {
            //ī��Ʈ�� 2�����ϰ�� �߰�
            SetLineup(buttonIndex);

        }
        else
        {
            //ī��Ʈ�� 3�̻��϶� �߰�����
            //���õ��ִ� ���ֵ鸸 ����Ʈ���� ��������.
            for (int i = 0; i < lineupIndexs.Count; i++)
            {
                if (lineupIndexs[i] == buttonIndex)
                {
                    SetLineup(buttonIndex);
                    break;
                }
            }

        }




    }
    private void SetLineup(int buttonIndex)
    {
        if (buttonIndex >= 1 && buttonIndex < Checkbox.Length)
        {
            //üũ�� ��������� ���ְ� �ȵ�������� ����
            bool isActive = !Checkbox[buttonIndex].gameObject.activeSelf;
            Checkbox[buttonIndex].gameObject.SetActive(isActive);
            if (isActive)
            {
                //üũǥ���ϴ� ���ÿ� ���� ��������Ʈ �ε����� �߰�
                lineupIndexs.Add(buttonIndex);

            }
            else
            {
                //üũǪ�� ���ÿ� ���� ��������Ʈ �ε����� ����
                lineupIndexs.Remove(buttonIndex);
                //Color c = buttons[buttonIndex].colors.selectedColor;
                // c   = originalColor;
            }
        }
    }

}

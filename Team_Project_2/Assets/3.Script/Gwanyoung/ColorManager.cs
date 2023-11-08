using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // ColorManager
    // ���, HUD ���� �� ������ �����ϱ� ����
    
    public static ColorManager instance = null;

    // ������ �÷� ���� �ϴ� �̱������� �ؾ��� �� ���Ƽ� �̱��������� �ϰڽ��ϵ�

    public Color[] Teamcolor; // �� �÷�
    public Material[] Flag_Color; // ��� ���ٲ� Marterial

    private void Awake()    
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        Teamcolor = new Color[System.Enum.GetValues(typeof(Team)).Length];           
        Teamcolor[(int)Team.neutrality] = new Color(255, 255, 255); // ���
        Teamcolor[(int)Team.Team1] = new Color(255, 0, 0);  // ������
        Teamcolor[(int)Team.Team2] = new Color(0, 170, 0);  // �ʷϻ�
        Teamcolor[(int)Team.Team3] = new Color(0, 0, 255);  // �Ķ���
        Teamcolor[(int)Team.Team4] = new Color(230, 0, 230); // ��ũ��
    }
    

}

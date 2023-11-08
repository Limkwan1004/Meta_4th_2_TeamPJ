using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // ColorManager
    // ���, HUD ���� �� ������ �����ϱ� ����
    
    public static ColorManager instance = null;

    //  0 : BK / 1 : BL / 2 : DB / 3 : BR / 4 : GR / 5 : GR_BL / 6 : PK / 7 : PP / 8 : RE / 9 : TAN / 10 : WH / 11 : YE
    //  0 : ���� / 1 : �ϴ� / 2 : �Ķ� / 3 : ���� / 4 : ���� / 5 : �ʷ�
    //  6 : ��ũ / 7 : ���� / 8 : ���� / 9 : ������ / 10 : ��� / 11 : ���
    


    public Color[] Teamcolor; // �� �÷�
    public Material[] Flag_Color; // ��� ���ٲ� Marterial
    public Texture2D[] Color_Texture;


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
    }

    public void RecursiveSearchAndSetTexture(Transform currentTransform, int index)
    {
        foreach (Transform child in currentTransform)
        {
            if (child.gameObject.activeSelf)
            {
                Renderer renderer = child.GetComponent<Renderer>();

                if (renderer != null && child.tag != "Flag")
                {
                    Material material = renderer.material;
                    material.SetTexture("_BaseMap", ColorManager.instance.Color_Texture[index]);
                }

                // ���� �ڽ� ������Ʈ�� ���� �ڽ� ������Ʈ�� ��������� �˻�

                RecursiveSearchAndSetTexture(child, index);
            }
        }
    }

}

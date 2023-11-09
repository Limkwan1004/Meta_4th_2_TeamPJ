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


    public Flag flag;
    public Color[] Teamcolor; // �� Color
    public Material[] Flag_Color; // ��� Material
    public Texture2D[] Unit_Texture; 
    public Texture2D[] Building_Texture; 


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

    public void RecursiveSearchAndSetUnit(Transform currentTransform, int index)
    {
        foreach (Transform child in currentTransform)
        {
            if (child.gameObject.activeSelf)
            {
                Renderer renderer = child.GetComponent<Renderer>();

                if (renderer != null && child.tag != "Flag")
                {
                    Material material = renderer.material;
                    material.SetTexture("_BaseMap", Unit_Texture[index]);
                }

                // ���� �ڽ� ������Ʈ�� ���� �ڽ� ������Ʈ�� ��������� �˻�

                RecursiveSearchAndSetUnit(child, index);
            }
        }
    }
    public void RecursiveSearchAndSetBuilding(Transform currentTransform, int index)
    {
        foreach (Transform child in currentTransform)
        {
            if (child.gameObject.activeSelf)
            {
                Renderer renderer = child.GetComponent<Renderer>();

                if (renderer != null && child.tag != "Flag")
                {
                    Material material = renderer.material;
                    material.SetTexture("_BaseMap", Building_Texture[index]);
                }

                // ���� �ڽ� ������Ʈ�� ���� �ڽ� ������Ʈ�� ��������� �˻�

                RecursiveSearchAndSetBuilding(child, index);
            }
        }
    }
}

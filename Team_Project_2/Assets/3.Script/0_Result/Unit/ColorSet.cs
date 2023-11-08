using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSet : MonoBehaviour
{
    /*
         UI���� ���� ������ �ε����� �ް� �迭(�÷�)���ִ� �ε��� ������ �ҷ���
        ���� ������Ʈ �ڽİ�ü�� Renderer������Ʈ�� �ִ��� �˻� 
        ->������Ʈ�� ������ Renderer material�� texture�� �ε����� �ش��ϴ� �÷��� ����
        -> �˻��ϴ� ��ü�� �ڽİ�ü�� ���������� �ݺ�.(����Լ�)
    */

   


    private void Start()
    {
        //�������ϰ� ��ũ��Ʈ�������ִ¿�����Ʈ���� �����ų�Ÿ� �̰� �����ϱ�
        // RecursiveSearchAndSetTexture(transform , Color_Index);
        //if(player.TryGetComponent<Material>(out 

        // player ���� �ɶ� �̰Ż���ϱ� 
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

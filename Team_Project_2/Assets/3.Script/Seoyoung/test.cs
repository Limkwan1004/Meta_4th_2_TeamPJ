using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Material night_M;
    public Material evening_M;
    public Material afternoon_M;

   

    

    Color color;
    //public Skybox skybox;

    float duration = 3; //3�ʸ���
    float smoothness = 0.1f; //�ε巯��? ���� ��ȭ ��


    private void Awake()
    {

        RenderSettings.skybox = night_M;
        
    }
    private void Update()
    {

        

    }

   




    private IEnumerator Update_SkyBox_Night()
    {


        float progress = 0; //�Ű����� ���

        float increment = smoothness / duration; //������ �������


        while (progress < 1)
        {
            Debug.Log(progress);
            color = Color.Lerp(afternoon_M.color, night_M.color, progress);
            RenderSettings.skybox.color = color;

            progress += increment;
            yield return new WaitForSeconds(smoothness);        //�÷��� �����͸� �ϳ��� ����
        }

    }
}

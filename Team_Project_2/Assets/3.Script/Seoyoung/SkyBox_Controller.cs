using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkyBox_Controller : MonoBehaviour
{

    public Material night_M;
    public Material evening_M;
    public Material afternoon_M;




    public Material current_material;

    float duration = 3; //3�ʸ���
    float smoothness = 0.1f; //�ε巯��? ���� ��ȭ ��


    void Start()
    {
        current_material = afternoon_M;
        gameObject.GetComponent<Skybox>().material = current_material;
 

        StartCoroutine(Update_SkyBox_Night());
    }



    private void Update()
    {
        //if (GameManager.instance.currentTime >= 3f)
        //{
        //    //this.gameObject.GetComponent<Skybox>().material = night_M;
        //    StartCoroutine(Update_SkyBox_Night());
        //}



    }






    private IEnumerator Update_SkyBox_Night()
    {
       
        float progress = 0; //�Ű����� ���

        float increment = smoothness / duration; //������ �������

     
        while (progress < 1)
        {
            Debug.Log(progress);
            //current_material.Lerp(afternoon_M, night_M, progress);
            gameObject.GetComponent<Skybox>().material.Lerp(afternoon_M, night_M, progress);
           // Debug.Log(this.gameObject.GetComponent<Skybox>().material);
            progress += increment;
            yield return new WaitForSeconds(smoothness);        //�÷��� �����͸� �ϳ��� ����
        }
       
    }
}

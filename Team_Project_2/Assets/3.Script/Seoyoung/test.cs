using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Material night_M;
    public Material evening_M;
    public Material afternoon_M;



    public Renderer rend;

    public Material current_material;

    float duration = 3; //3�ʸ���
    float smoothness = 0.1f; //�ε巯��? ���� ��ȭ ��


    void Start()
    {

        rend = GetComponent<Renderer>();
        rend.material = afternoon_M;

      

      

    }



    private void Update()
    {
        //if (GameManager.instance.currentTime >= 3f)
        //{
        //    //this.gameObject.GetComponent<Skybox>().material = night_M;
        //    StartCoroutine(Update_SkyBox_Night());
        //}



    }







    //private IEnumerator Update_SkyBox_Night()
    //{

       

    //}
}

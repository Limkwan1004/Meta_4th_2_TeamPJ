using System.Collections;
using UnityEngine;

public class Archer_Attack : MonoBehaviour
{
    // �ü��� �� ȭ�� ����

    [SerializeField] GameObject Arrow; // ȭ�� ������
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Arrow_ob = Instantiate(Arrow, transform.position + transform.forward, transform.rotation); // �ι�° �Ű������� �ƹ�Ÿ Ȱ �־��ֽʼ�
        }
    }





}

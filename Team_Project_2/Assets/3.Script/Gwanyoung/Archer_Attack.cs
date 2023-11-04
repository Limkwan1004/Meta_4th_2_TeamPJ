using System.Collections;
using UnityEngine;

public class Archer_Attack : MonoBehaviour
{
    // �ü��� �� ȭ�� ����

    [SerializeField] GameObject Arrow; // ȭ�� ������
    [SerializeField] float ArrowSpeed = 15f; // ȭ�� �ӵ�
    [SerializeField] private Rigidbody rigid; 
    private float graph = 5f;  // ������ ������ġ

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Arrow_ob = Instantiate(Arrow, transform.position + transform.forward, transform.rotation); // �ι�° �Ű������� �ƹ�Ÿ Ȱ �־��ֽʼ�
            
            StartCoroutine(Arrow_rot(Arrow_ob));
        }
    }

    IEnumerator Arrow_rot(GameObject Shoot_A)
    {
        rigid = Shoot_A.GetComponent<Rigidbody>();
        rigid.AddForce(Shoot_A.transform.forward * ArrowSpeed, ForceMode.Impulse);


        while (true) // ���߿� ������� ���� �ֱ�
        {
            Quaternion Tar_Q = new Quaternion();
            Tar_Q.eulerAngles = new Vector3(-(graph * rigid.velocity.y), 0, 0);


            Shoot_A.transform.rotation = Tar_Q;
            yield return null;
        }
        Destroy(Shoot_A, 4f);


    }



}

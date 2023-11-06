using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Archer_attack archer_attack;   
    private float ArrowSpeed = 12f;  // ȭ�� �ӵ�
    private float DestroyTime = 6f; // �߻� �� ����� �ð�
    private float CurrentTime = 0;  // �ð� ����
    private bool isHit = false;  // �¾Ҵ���


    private void Start()
    {

        StartCoroutine(Arrow_rot());
    }


    IEnumerator Arrow_rot()
    {
        archer_attack = GetComponentInParent<Archer_attack>();
        Vector3 targetp = archer_attack.Target.position - archer_attack.Bow.position;
        transform.forward = targetp + new Vector3(0, 0.7f, 0);
        gameObject.layer = archer_attack.gameObject.layer;
        gameObject.transform.SetParent(null);
  
        while (!isHit) 
        {
            CurrentTime += Time.deltaTime;
            transform.position += transform.forward * ArrowSpeed * Time.deltaTime;
            // ȭ���� ������ ����.. �Ӱ� ������� 
            if (CurrentTime > DestroyTime)
            {
                Destroy(gameObject);
            }
            yield return null;

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Weapon") && (other.gameObject.layer != gameObject.layer))
        {
            isHit = true;
            Debug.Log("����");

            Destroy(gameObject);
        }
    }
    
}

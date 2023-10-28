using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*
        ���� �Ŵ������� �����ؾ� �� ���� ���
        1. ���
        2. �÷��̾� ü��
        3. ������ (���� ����)
    */

    public static GameManager instance = null;

    public float currentTime = 0f; // ������ �����ϰ� ���� �ð�
    public float EndTime = 1800f; // ���� �ð��� 30��

    public float Gold = 0; // ��差
    private float Magnifi = 2f;  // �⺻ ��� ���� (������Ʈ�� ������ 60 x 2f�� �⺻ ȹ�� ��差�� �д� 120)

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ���� ��� ��·�
    // ���� ��庥Ƽ��
    // ��� ��·� ���׷��̵�
    
    private void Update()
    {
        currentTime += Time.deltaTime;

        Gold += Time.deltaTime * Magnifi; // ������
    }
}

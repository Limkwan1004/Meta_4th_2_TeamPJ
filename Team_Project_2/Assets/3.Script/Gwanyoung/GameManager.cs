using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float currentTime = 0; // ������ �����ϰ� ���� �ð�

    public float Gold = 0; // ��差
    private float Magnifi = 2f;  // ó�� ��� ����

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

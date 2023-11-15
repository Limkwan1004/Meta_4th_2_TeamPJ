using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // ���� ���� �ݴ� ��ũ��Ʈ


    private Animator Gate_Ani;  
    [SerializeField] private Collider Gate_Col;  // Gate ���� Collider
    private bool isOpen = false;

    private void Awake()
    {
        Gate_Ani = GetComponent<Animator>();
        Gate_Col = GetComponent<Collider>();
        Gate_Ani.SetTrigger("OpenDoor");
        isOpen = true;
        Gate_Col.enabled = false;
    }
    // ����Ʈ ��ȣ�ۿ�
    public IEnumerator Gate_Interaction(int TeamLayer)
    {
        if (!TeamLayer.Equals((int)TeamLayerIdx.Player))
        {
            if (!isOpen) // ���� �������� ��
            {
                Gate_Ani.SetTrigger("OpenDoor");
                isOpen = true;
                Gate_Col.enabled = false;
            }
            else  // ���� �������� ��
            {
                Gate_Ani.SetTrigger("CloseDoor");
                isOpen = false;
                Gate_Col.enabled = true;
            }
        }
        else
        {
            while (true)
            {
                if (!isOpen)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        Gate_Ani.SetTrigger("OpenDoor");
                        isOpen = true;
                        Gate_Col.enabled = false;
                    }
                }
                else if (isOpen)
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        Gate_Ani.SetTrigger("CloseDoor");
                        isOpen = false;
                        Gate_Col.enabled = true;
                    }

                }
                yield return null;
                
            }

        }
        yield return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttoninfo : MonoBehaviour
{

    public Unit_Information unitInfo; // ScriptableObject�� �����ϱ� ���� �ʵ�
    public GameObject infoPanel; // ���� �г��� �����ϱ� ���� �ʵ�

    private bool isHovering = false;

    private void Start()
    {
        infoPanel.SetActive(false);
    }

    private void Update()
    {
        if (isHovering)
        {
            // ���콺 ������ ��ġ�� �����ɴϴ�.
            Vector3 mousePosition = Input.mousePosition;

            // �г��� ���콺 ������ ��ġ�� �̵���ŵ�ϴ�.
            infoPanel.transform.position = mousePosition;

            // �г��� Ȱ��ȭ�մϴ�.
            infoPanel.SetActive(true);
        }
        else
        {
            // �г��� ��Ȱ��ȭ�մϴ�.
            infoPanel.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        isHovering = true;
        ShowUnitInfo(unitInfo);
    }

    private void OnMouseExit()
    {
        isHovering = false;
        HideUnitInfo();
    }

    // ������ �гο� ǥ���ϴ� �Լ� (������ ���� �ٸ� �� ����)
    private void ShowUnitInfo(Unit_Information info)
    {
        // info�� ����Ͽ� ������ �гο� ǥ��
    }

    // ���� �г��� ��Ȱ��ȭ�ϴ� �Լ� (������ ���� �ٸ� �� ����)
    private void HideUnitInfo()
    {
        // ���� �г��� ��Ȱ��ȭ
    }
}



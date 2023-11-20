using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    /*
        ��� ���� �� �޴�â�� �Ѱ� �� �� �ִ� ��ũ��Ʈ 
    */
    [SerializeField] private WorldMap worldMap;
    [SerializeField] private GameObject[] Menu;
    [SerializeField] private Optioin_Panel option;

    public bool isMenuOpen = false;

    void Update()
    {
        if(!isMenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Togglemenu(Menu[0]);    // ���׷��̵�
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                worldMap.Change_ColorChip();
                Togglemenu(Menu[1]);    // �����
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Togglemenu(Menu[2]);    // �ɼ�
                option.OptionPanel_On();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Escape))
            {
                for (int i = 0; i < Menu.Length; i++)
                {
                    Menu[i].SetActive(false);
                }
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isMenuOpen = false;
                GameManager.instance.Resume();
            }
        }
    }

    public void Togglemenu(GameObject menu)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        menu.SetActive(true);
        isMenuOpen = true;
        GameManager.instance.Stop();
    }

    public void Close_Menu()
    {
        for (int i = 0; i < Menu.Length; i++)
        {
            Menu[i].SetActive(false);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isMenuOpen = false;
        GameManager.instance.Resume();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldText : TextManager
{
    // ������ HUD

    private void Update()
    {
        Textarray[0].text = $"���: {(int)GameManager.instance.Gold}"; // ��� ���� �־��ּ���
    }

}

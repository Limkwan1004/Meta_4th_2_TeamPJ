using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPointText : TextManager
{
  
    // 1. ������ ����Ʈ�� Slider�� Text�� �ð�ȭ
    // 2. Slider�� ���� ���� ����Ʈ / ����Ʈ�� ���� ���� ���� ����Ʈ          


    private void Update()
    {
        Textarray[1].text = $"{(int)GameManager.instance.currentTime * 2}";
        Textarray[0].text = $"{(int)GameManager.instance.currentTime * 1}";
        Textarray[2].text = $"{(int)GameManager.instance.currentTime * 3}";
        Textarray[3].text = $"{(int)GameManager.instance.currentTime * 4}";

    }


}

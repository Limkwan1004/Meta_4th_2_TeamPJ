using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : TextManager
{
    /*
    1. ���ʿ��� ����ð�
    2. �����ʿ��� ���Ӽ����ð�
     */

    private void Awake()
    {
        Textarray[1].text = string.Format("{0}:{1:00}", ((int)GameManager.instance.EndTime / 60),
            ((int)GameManager.instance.EndTime) % 60); // �� / ��  
    }
    void Update()
    {
        Textarray[0].text = string.Format("{0}:{1:00}", ((int)GameManager.instance.currentTime / 60), 
            ((int)GameManager.instance.currentTime) % 60); // �� / ��
        
    }
}

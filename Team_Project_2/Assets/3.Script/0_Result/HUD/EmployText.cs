using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployText : TextManager
{
    
     //����� �⹰���� State, InputKey, Cost ǥ��
    
    private void Start()
    {
        // ��� �ؽ�Ʈ ���߿� ������ �ٲ��ּ���
        Textarray[0].text = $"��80 /20\n��� Ű: <color=#FF3E3E>1</color>\n���: <color=#B7AF3D>15</color>"; // �κ�
        Textarray[1].text = $"��90 /20\n��� Ű: <color=#FF3E3E>2</color>\n���: <color=#B7AF3D>16</color>"; // â��
        Textarray[2].text = $"��100 /25\n��� Ű: <color=#FF3E3E>3</color>\n���: <color=#B7AF3D>20</color>"; // ����
        Textarray[3].text = $"��80 /20\n��� Ű: <color=#FF3E3E>0</color>\n���: <color=#B7AF3D>20</color>"; // ��
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    // ����
    // 1. ���� �� ��� �� ����       

    private SkinnedMeshRenderer skinnedmesh;   
    
    public void Change_Flag_Color(int Teamcolor)
    { 
        skinnedmesh.material = ColorManager.instance.Flag_Color[Teamcolor];        
    }

   



}

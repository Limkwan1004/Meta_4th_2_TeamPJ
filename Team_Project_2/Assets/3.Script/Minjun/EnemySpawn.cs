using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : LeaderState
{

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        //�������� ��� ��ó������ canSpawn =true;
        if(maxUnitCount <= currentUnitCount)
        {
            canSpawn = false;
        }





        if(Gold >= unitCost && canSpawn)
        {

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_AniState : MonoBehaviour
{



    private void Start()
    {
     
    }
    public enum State
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die

    }
    public State state { get; private set; }


    //private IEnumerator Attack_co()
    //{
    //    //���ݽð����� �̵��Ұ�
    //    //
    //    state = State.Attack;
    //    ani.SetTrigger("Attack");
    //    yield return attackDelay;
    //    isAttacking = false;
    //}
}

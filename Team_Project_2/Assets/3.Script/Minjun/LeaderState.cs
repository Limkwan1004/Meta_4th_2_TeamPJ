using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderState : Unit
{

    public enum BattleState {
    
        Follow, //������ �̵��Ҷ� 
        Attack  // AI�� ���� �����ϰ� �����ð� �Ǵ� �Ÿ��������� 
    }
    public enum JudgmentState
    {
        Ready, //��� �����º��� ������ ������ �̵�
        wait, //��������º��� ������ �������� ����ϸ� �ֵ�̱�
        Ditect,


    }
    public enum AniState 
    {
        Idle,
        Attack,
        shild,
        Order
    }





    [Header("��� ����")]
    public float Gold = 500; // ��差
    // private float Magnifi = 2f;  // �⺻ ��� ���� (������Ʈ�� ������ 60 x 2f�� �⺻ ȹ�� ��差�� �д� 120)

    [Header("AI ����")]
    public bool isLive = true;
    // private bool Ready =true;
    public float Current_HP = 150f;
    public float Max_Hp = 150f;
    public float Regeneration = 0.5f;
    public BattleState bat_State;
    public JudgmentState jud_State;
    public AniState ani_State;
    //AI �ൿ �켱����
    /*
     1. �߸������� ������
     2. �߸������� �ƴ����� �ƹ��� ������ 
     3. 
     
     1.����
     2. �ƹ��������� ����
     
     */

}

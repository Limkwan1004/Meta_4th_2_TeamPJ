using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [CreateAssetMenu(fileName = "UnitInfo", menuName = "Unit Info")]
public class Unit_Information : ScriptableObject
{
    public GameObject unitObject;
    public bool ishealer;
    public string unitName;
    public string description;
    public float damage;
    public float maxHP;
    public float currentHP;
    public float attackRange;
    public float cost;
    public int index;
    // �ٸ� ���� �ʵ���� �߰��� �� �ֽ��ϴ�.

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleProceduralTerrainProject;

public abstract class Targetsetting : MonoBehaviour
{
    [HideInInspector] public TerrainGenerator MapInfo;
    [HideInInspector] public Targetsetting Targetset;

    [HideInInspector] public List<GameObject> ListTemp;
    
    [HideInInspector] public GameObject TargetBase;
    

    [HideInInspector] public Gate[] gates;         // ����Ʈ�� ���� ��
    [HideInInspector] public Gate TargetGate;      // ���� �� ����Ʈ
    [HideInInspector] public float Distance;       // ����Ʈ�� ���� �Ÿ�

    private void Start()
    {
        ListTemp = new List<GameObject>();
        MapInfo = FindObjectOfType<TerrainGenerator>();
        gates = FindObjectsOfType<Gate>();
        
    }

    public abstract Transform Target(Transform StartPos);

}

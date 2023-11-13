using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SimpleProceduralTerrainProject;

public class TargetSetting : MonoBehaviour
{
    public TargetSetting targetset;
    public TerrainGenerator MapInfo;

    private List<GameObject> Bases;
    private GameObject TargetBase;

    private Gate[] gates;         // ����Ʈ�� ���� ��
    private Gate TargetGate;      // ���� �� ����Ʈ
    private float Distance;       // ����Ʈ�� ���� �Ÿ�

    Transform ToEnemyBase()
    {
        List<GameObject> Bases = new List<GameObject>();
        GameObject TargetBase;


        for (int i = 0; i < MapInfo.baseCampPositions.Count; i++)
        {
            if (!MapInfo.baseCampPositions[i].layer.Equals(gameObject.layer))
            {
                Bases.Add(MapInfo.baseCampPositions[i]);
            }
        }
        TargetBase = Bases[Random.Range(0, Bases.Count)];
        return TargetBase.transform;
    }
       
    Transform ToFlag()
    {
        return MapInfo.flagPositions_List[Random.Range(0, MapInfo.flagPositions_List.Count)].transform;
    }
    
    Transform ToGate(Transform StartPos)
    {
        gates = FindObjectsOfType<Gate>();

        TargetGate = gates[0];
        float Distance = Vector3.SqrMagnitude(StartPos.position - gates[0].transform.position); // �񱳿����� 0��° �迭 ����Ʈ �Ÿ����ϱ�

        for (int i = 1; i < gates.Length; i++)
        {
            float DistanceTemp = Vector3.SqrMagnitude(StartPos.position - gates[i].transform.position);
            if (DistanceTemp < Distance) // �Ÿ��� �� ª�� ��
            {
                TargetGate = gates[i];
                Distance = DistanceTemp;
            }
        }
        return TargetGate.transform;
    }
    Transform ToMyBase()
    {
        TerrainGenerator MapInfo = FindObjectOfType<TerrainGenerator>();

        for (int i = 0; i < MapInfo.baseCampPositions.Count; i++)
        {
            if (MapInfo.baseCampPositions[i].layer.Equals(gameObject.layer))
            {
                Bases.Add(MapInfo.baseCampPositions[i]);
            }
        }

        if (Bases.Count > 0)
        {
            TargetBase = Bases[0];

            if (Bases.Count > 1)
            {
                float Distance = Vector3.SqrMagnitude(gameObject.transform.position - MapInfo.baseCampPositions[0].transform.position);

                for (int i = 1; i < Bases.Count; i++)
                {
                    float DistanceTemp = Vector3.SqrMagnitude(gameObject.transform.position - MapInfo.baseCampPositions[i].transform.position);
                    if (DistanceTemp < Distance) // �Ÿ��� �� ª�� ��
                    {
                        TargetBase = MapInfo.baseCampPositions[i];
                        Distance = DistanceTemp;
                    }

                }
            }
            return TargetBase.transform;
        }

        return null;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap_Ge : MonoBehaviour
{
    [SerializeField] private Terrain terrain;

    [Header("�� ũ�� ����")]
    [SerializeField] private float width = 400f;    // x��
    [SerializeField] private float length = 400f;   // z��
    [SerializeField] private float height = -1f;   // y��

    private void Awake()
    {
        SetTerrain();
    }

    private void SetTerrain()
    {
        float LocalX = (width / 2) * (-1);
        float LocalZ = (length / 2) * (-1);

        terrain.transform.localPosition = new Vector3(LocalX, 0, LocalZ);
        terrain.terrainData.size = new Vector3(width, height, length);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMap_Ge : MonoBehaviour
{
    [SerializeField] private Terrain terrain;

    [Header("�� ũ�� ����")]
    [SerializeField] private float width = 400f;    // x��
    [SerializeField] private float length = 400f;   // z��
    [SerializeField] private float height = 100f;   // y��

    [Header("�� ���� ����")]
    [SerializeField] private float mountainHeight = 40f;
    [SerializeField] private float detailScale = 20f;
    [SerializeField] private int numberOfMountains = 8;
    [SerializeField] private int brushSize = 30;

    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        SetTerrain();
        SetHeight();
    }

    private void SetTerrain()
    {
        float LocalX = (width / 2) * (-1);
        float LocalZ = (length / 2) * (-1);

        terrain.transform.localPosition = new Vector3(LocalX, 0, LocalZ);
        terrain.terrainData.size = new Vector3(width, height, length);
    }

    private void SetHeight()
    {
        // TerrainData terrainData = terrain.terrainData;
        // int heightmapWidth = terrainData.heightmapResolution;
        // int heightmapHeight = terrainData.heightmapResolution;
        // 
        // float[,] heights = new float[heightmapWidth, heightmapHeight];
        // 
        // float inverseDetailScale = 1f / detailScale;
        // float inverseHeight = 1f / height;
        // float inverseBrushSizeSquared = 1f / (brushSize * brushSize);
        // 
        // for (int m = 0; m < numberOfMountains; m++)
        // {
        //     // ���� ���� ��ġ�� �������� ����
        //     Vector2 mountainStart = new Vector2(Random.Range(0, heightmapWidth), Random.Range(0, heightmapHeight));
        // 
        //     // ���� ������ �������� ����
        //     Vector2 direction = Random.insideUnitCircle.normalized;
        // 
        //     // ���� �׸���
        //     for (float i = 0; i < heightmapWidth; i += 0.1f)
        //     {
        //         int x = Mathf.RoundToInt(mountainStart.x + direction.x * i);
        //         int z = Mathf.RoundToInt(mountainStart.y + direction.y * i);
        // 
        //         if (x < 0 || x >= heightmapWidth || z < 0 || z >= heightmapHeight) continue;
        // 
        //         Vector2 mountainPoint = new Vector2(x, z);
        //         for (int brushX = -brushSize; brushX <= brushSize; brushX++)
        //         {
        //             for (int brushZ = -brushSize; brushZ <= brushSize; brushZ++)
        //             {
        //                 int posX = x + brushX;
        //                 int posZ = z + brushZ;
        // 
        //                 if (posX < 0 || posX >= heightmapWidth || posZ < 0 || posZ >= heightmapHeight) continue;
        // 
        //                 Vector2 brushPoint = new Vector2(posX, posZ);
        //                 float distanceToCenterSquared = (brushPoint - mountainPoint).sqrMagnitude;
        // 
        //                 if (distanceToCenterSquared > brushSize * brushSize) continue;
        // 
        //                 float intensity = 1.0f - Mathf.Sqrt(distanceToCenterSquared * inverseBrushSizeSquared);
        // 
        //                 float distanceToMountainStart = Vector2.Distance(brushPoint, mountainStart) / heightmapWidth;
        //                 float heightAtPoint = Mathf.PerlinNoise(posX * inverseDetailScale, posZ * inverseDetailScale) * mountainHeight * (1 - distanceToMountainStart) * intensity;
        // 
        //                 heights[posX, posZ] = Mathf.Max(heights[posX, posZ], heightAtPoint * inverseHeight);
        //             }
        //         }
        //     }
        // }
        // terrainData.SetHeights(0, 0, heights);

        TerrainData terrainData = terrain.terrainData;
        int heightmapWidth = terrainData.heightmapResolution;
        int heightmapHeight = terrainData.heightmapResolution;

        // Compute Shader�� �����ϰ� �����մϴ�.
        // ... (Compute Shader ���� �� ���� �ڵ�)

        // GPU���� ����� CPU�� �������� ���� Render Texture�� ����մϴ�.
        RenderTexture heightMapRT = new RenderTexture(heightmapWidth, heightmapHeight, 0, RenderTextureFormat.RFloat);
        heightMapRT.enableRandomWrite = true;
        heightMapRT.Create();

        // Compute Shader ����� Render Texture�� �����մϴ�.
        // ... (Compute Shader ��� ���� �ڵ�)

        // Render Texture���� ����� �о�ͼ� Terrain�� ���̸ʿ� �����մϴ�.
        Texture2D heightMapTexture = new Texture2D(heightmapWidth, heightmapHeight, TextureFormat.RFloat, false);
        RenderTexture.active = heightMapRT;
        heightMapTexture.ReadPixels(new Rect(0, 0, heightmapWidth, heightmapHeight), 0, 0);
        heightMapTexture.Apply();
        RenderTexture.active = null;

        float[,] heights = new float[heightmapWidth, heightmapHeight];
        for (int x = 0; x < heightmapWidth; x++)
        {
            for (int z = 0; z < heightmapHeight; z++)
            {
                heights[x, z] = heightMapTexture.GetPixel(x, z).r;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}

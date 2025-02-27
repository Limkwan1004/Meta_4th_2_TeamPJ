using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccupationHUD : MonoBehaviour
{
    // 점령 HUD
    // 1. 점령이 끝나면 인게임 상단에 점령현황 ImageColor 변경
    // 2. 플레이어한테는 점령 중 Slider와 UI 표시되도록   

    public GameObject[] Occu_image;
    public Image[] Occu_Img_Color; // 점령 중인 팀 색
    public Flag[] FlagArray;  // 플래그 컴포넌트 배열
    public Slider[] OccuSlider;
    public Color ColorTemp;
    int TeamColor;

    private void Start()
    {
        Occu_image = new GameObject[7];
        Occu_Img_Color = GetComponentsInChildren<Image>();
        OccuSlider = GetComponentsInChildren<Slider>();
       
        for (int i = 0; i < Occu_Img_Color.Length * 0.5f; i++) 
        {
            Occu_Img_Color[(i * 2) + 1].transform.parent.gameObject.SetActive(false);
           
        }
    }    

    public void Occu_Set()
    {
        FlagArray = FindObjectsOfType<Flag>();

        // 플래그마다 번호부여
        for (int i = 0; i < FlagArray.Length; i++)
        {
            FlagArray[i].Flag_Sound.clip = AudioManager.instance.clip_SFX[(int)SFXList.Flag_Sound];
            FlagArray[i].Flag_Sound.Play();
            FlagArray[i].Flag_Num = i;
        }

        // 상단 점령현황 위치조정

       

        for (int i = 0; i < FlagArray.Length; i++)
        {
            Occu_image[i] = Occu_Img_Color[i * 4].transform.parent.gameObject;
            Occu_image[i].transform.localPosition = new Vector3((-50 * FlagArray.Length * 0.5f) + (50 * i), 0, 0);
            Change_Color((int)ColorIdx.White, i);
            if (FlagArray[i].transform.parent != null) 
            {
                FlagArray[i].Current_Gauge = 100f;
                
                FlagArray[i].isOccupied = true;
                switch (FlagArray[i].gameObject.layer)
                {
                    case (int)TeamLayerIdx.Player:
                        TeamColor = GameManager.instance.Color_Index;
                        break;
                    case (int)TeamLayerIdx.Team1:
                        TeamColor = GameManager.instance.T1_Color;
                        break;
                    case (int)TeamLayerIdx.Team2:
                        TeamColor = GameManager.instance.T2_Color;
                        break;
                    case (int)TeamLayerIdx.Team3:
                        TeamColor = GameManager.instance.T3_Color;
                        break;
                    default:
                        break;
                }
                Ply_Slider(TeamColor, i, 100f, 100f);
                FlagArray[i].OccuHUD.Change_Color(TeamColor, i);
            }
        }

        // 상단 점령현황 setActive 
        for (int i = FlagArray.Length; i < 7; i++)
        {
            Occu_image[i] = Occu_Img_Color[i * 4].transform.parent.gameObject;
            Occu_image[i].SetActive(false);
        }

    }

    public void Ply_Slider(int TeamColor, int FlagNum, float Current, float Total)
    {
        ColorTemp = ColorManager.instance.Teamcolor[TeamColor];
        ColorTemp.a = 0.431f;

        Occu_Img_Color[FlagNum * 4 + 2].color = ColorTemp; 
        OccuSlider[FlagNum].value = Current / Total;   // 슬라이더 현재 게이지
    }
 
    public void Ply_OccuHUD(int FlagNum, bool Act)
    {
        // 점령중인 유닛이 Player 일 때 SetActive의 불값을 주기 위한 메서드
        Occu_Img_Color[FlagNum * 4 + 1].transform.parent.gameObject.SetActive(Act);  // 점령 중앙쪽 HUD
        Occu_Img_Color[FlagNum * 4 + 3].transform.parent.gameObject.SetActive(Act);  // 점령 슬라이더
    }

    public void Change_Color(int TeamColor, int FlagNum)
    {        
        ColorTemp = ColorManager.instance.Teamcolor[TeamColor];
        ColorTemp.a = 0.431f;

        Occu_Img_Color[FlagNum * 4].color = ColorTemp; // HUD 상단               

        Occu_Img_Color[FlagNum * 4 + 1].color = ColorTemp; // 플레이어에게 뜰 HUD   

        FlagArray[FlagNum].Change_Flag_Color(TeamColor); // 깃발 색 변경

    }


}

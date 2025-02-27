using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineUpSet : MonoBehaviour
{

    [SerializeField]
    //유닛선택 버튼들 6개
    private Button[] buttons;
    //스타트버튼
    [SerializeField] Button startButton;
    //각 버튼들의 체크이미지
    private GameObject[] Checkbox;
    //최종 라인업 스프라이트 인덱스
    public List<int> lineupIndexs = new List<int>();
    [Header("Sword > Heavy > Archer > Priest > Spear > Halberdier > Default")]
    //유닛스프라이트 배열
    [SerializeField] private Sprite[] unitSprite_array;
    //라인업 UI 직접참조
    [SerializeField] private GameObject lineupUI;
    //라인업 UI 에 있는 최종 선택 유닛들(이미지)
    private Image[] lineupSprite;
    private bool isCanStart;
    private Color originalColor;

    #region 병사 선택 버튼
    [SerializeField]
    private Button Swordman_Btn;

    [SerializeField]
    private Button Knight_Btn;

    [SerializeField]
    private Button Archer_Btn;

    [SerializeField]
    private Button Prist_Btn;

    [SerializeField]
    private Button Spearman_Btn;

    [SerializeField]
    private Button Halberdier_Btn;


    #endregion



    void Start()
    {

        lineupSprite = lineupUI.GetComponentsInChildren<Image>();
        buttons = GetComponentsInChildren<Button>();
        Checkbox = new GameObject[buttons.Length]; //
        originalColor = buttons[1].colors.normalColor; // 원래 색상 저장

        for (int i = 0; i < buttons.Length; i++)
        {
            Checkbox[i] = buttons[i].transform.GetChild(0).gameObject;
        }


        lineupIndexs.Add(0);



        Init_Button();
    }
    private void Update()
    {

        for (int i = 0; i < 3; i++)
        {
            try
            {
                // 최종 선택 유닛 i번째 스프라이트 = 최종선택 인덱스 번째의 유닛스프라이트
                lineupSprite[i].sprite = unitSprite_array[lineupIndexs[i]];
            }
            catch
            {
                // lineupIndex 의 리스트가 비어있을땐 Default 스프라이트 할당
                lineupSprite[i].sprite = unitSprite_array[6];
            }
        }
        if (lineupIndexs.Count == 3)
        {
            isCanStart = true;
        }
        if (isCanStart)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }
    private void Init_Button()
    {

        Swordman_Btn = gameObject.transform.GetChild(0).GetComponent<Button>();
        Knight_Btn = gameObject.transform.GetChild(1).GetComponent<Button>();
        Archer_Btn = gameObject.transform.GetChild(2).GetComponent<Button>();
        Prist_Btn = gameObject.transform.GetChild(3).GetComponent<Button>();
        Spearman_Btn = gameObject.transform.GetChild(4).GetComponent<Button>();
        Halberdier_Btn = gameObject.transform.GetChild(5).GetComponent<Button>();


        Set_Buttons(ref Swordman_Btn, GameManager.instance.isCanUse_SwordMan);
        Set_Buttons(ref Knight_Btn, GameManager.instance.isCanUse_Knight);
        Set_Buttons(ref Archer_Btn, GameManager.instance.isCanUse_Archer);
        Set_Buttons(ref Spearman_Btn, GameManager.instance.isCanUse_SpearMan);
        Set_Buttons(ref Halberdier_Btn, GameManager.instance.isCanUse_Halberdier);
        Set_Buttons(ref Prist_Btn, GameManager.instance.isCanUse_Prist);

    }

    private void Set_Buttons(ref Button button, bool iscanuse)
    {
        if (iscanuse)
        {
            button.enabled = true;
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            button.enabled = false;

            button.GetComponent<Image>().color = Color.black;
        }
    }



    public void ButtonClicked(int buttonIndex)
    {
        if (lineupIndexs.Count < 3)
        {
            //카운트가 2이하일경우 추가
            SetLineup(buttonIndex);

        }
        else
        {

            //카운트가 3이상일땐 추가못함
            //선택되있는 유닛들만 리스트에서 뺄수있음.
            for (int i = 0; i < lineupIndexs.Count; i++)
            {
                if (lineupIndexs[i] == buttonIndex)
                {
                    SetLineup(buttonIndex);
                    break;
                }
            }

        }

    }
    private void SetLineup(int buttonIndex)
    {
        if (buttonIndex >= 1 && buttonIndex < Checkbox.Length)
        {
            //체크가 되있을경우 없애고 안되있을경우 생김
            bool isActive = !Checkbox[buttonIndex].gameObject.activeSelf;
            Checkbox[buttonIndex].gameObject.SetActive(isActive);
            if (isActive)
            {
                //체크표시하는 동시에 최종 스프라이트 인덱스에 추가
                lineupIndexs.Add(buttonIndex);

            }
            else
            {

                Debug.Log("리스트지움");
                //체크푸는 동시에 최종 스프라이트 인덱스에 삭제
                lineupIndexs.Remove(buttonIndex);
                //Color c = buttons[buttonIndex].colors.selectedColor;
                // c   = originalColor;
            }
        }
    }

    public void UnitSetToPlayer()
    {
        GameManager.instance.unit0 = GameManager.instance.units[lineupIndexs[0]];
        GameManager.instance.unit1 = GameManager.instance.units[lineupIndexs[1]];
        GameManager.instance.unit2 = GameManager.instance.units[lineupIndexs[2]];

        //이시점에서 resultPanel 초기화
        GameManager.instance.Result = FindObjectOfType<ToggleMenu>().transform.GetChild(4).gameObject;
        GameManager.instance.Result.SetActive(false);
        GameManager.instance.txt = GameManager.instance.Result.transform.GetChild(9).GetComponent<Text>();
    }

    public void Normal_Mode()
    {
        GameManager.instance.GameSpeed = 0;
    }

    public void Speed_Mode()
    {
        GameManager.instance.GameSpeed = 1;
    }
}



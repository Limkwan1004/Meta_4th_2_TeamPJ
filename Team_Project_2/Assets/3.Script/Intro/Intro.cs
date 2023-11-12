using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public struct TeamColor
{
    public int ColorNum;
    public Color color_c;
    public Material color_m;
    public bool isUsing;
}

public class Intro : MonoBehaviour
{
    #region Ÿ��Ʋ �г� 
    [Header("Title Panel")]
    [SerializeField]
    private GameObject Title_Panel;

    [SerializeField]
    private GameObject Login_Panel;

    [SerializeField]
    private GameObject Btn_Panel;

    //�α��� �г�
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Button Confirm_Btn;

    //��ư �г�
    [SerializeField]
    private Button Ready_Btn;

    [SerializeField]
    private Button Upgrade_Btn;

    [SerializeField]
    private Button Option_Btn;

    [SerializeField]
    private Button Exit_Btn;



    //�ڷΰ���
    [SerializeField]
    private Button BackButton;
    #endregion



    #region Setup �г�(Ready �г�)

    [Header("Setup Panel")]
    [SerializeField]
    private GameObject Setup_Panel;

    [SerializeField]
    private Image Map_img;


    [SerializeField]
    private Button TeamColor1_Btn;

    [SerializeField]
    private Button TeamColor2_Btn;

    [SerializeField]
    private Button TeamColor3_Btn;

    [SerializeField]
    private Button TeamColor4_Btn;

    [SerializeField]
    private Button Original_Btn;

    [SerializeField]
    private Button TimeAttack_Btn;

    [SerializeField]
    private Button GameStart_Btn;


    [SerializeField]
    private Text Team1_Text;

    [SerializeField]
    private Text Team2_Text;

    [SerializeField]
    private Text Team3_Text;

    [SerializeField]
    private Text Team4_Text;


    [SerializeField]
    private Text ID1_Text;

    [SerializeField]
    private Text ID2_Text;

    [SerializeField]
    private Text ID3_Text;

    [SerializeField]
    private Text ID4_Text;

    #endregion



    #region ���׷��̵� �г�
    [Header("Upgrade Panel")]
    private GameObject Upgrade_Panel;
    #endregion



    #region �ɼ� �г�
    [Header("Option Panel")]

    [SerializeField]
    private Optioin_Panel Option_Panel;

    #endregion



    #region ��Ÿ
    [Header("��Ÿ")]

    //������
    [SerializeField]
    private GameObject Leader_A;

    [SerializeField]
    private GameObject Leader_B;


   // public float mouseSensitivity;

    private float MouseY;
    private float MouseX;


    //json ����
    private DataManager dataManager;
    private ScriptsData scriptsData;

    //�α��� ����
    public bool isLogined = false;

    public string id = "sunny";

    public int timer = 3;

    //�Ҹ� ����
    public AudioMixer audioMixer;

   

    //������
    [Header("�� �÷�")]
    [SerializeField]
    private Material[] TeamColors;

    [SerializeField]
    public TeamColor[] teamColors = new TeamColor[11];


    //GameManager�� ���� �� �� ���� ��ȣ, ColorSet �ε����� ������ ����
    public int Team1_Color;
    public int Team2_Color;
    public int Team3_Color;
    public int Team4_Color;




    #endregion


    private void Awake()
    {
        // gameObject.SetActive(true);
        
   
        dataManager = new DataManager();
        scriptsData = dataManager.Load("Scripts");
       
    }

    private void Start()
    {
      

        Screen.SetResolution(1920, 1080, true);

        Init_FuntionUI();

        TitlePanel_On();

        BackButton.onClick.AddListener(BackBtn_Clicked);
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Login();
        }
    }

    private void Init_FuntionUI()
    {
        //���� �� �ʱ�ȭ�� �͵�

        BackButton = transform.GetChild(0).GetComponent<Button>();
        Title_Panel = transform.GetChild(1).gameObject;
        Setup_Panel = transform.GetChild(2).gameObject;
        Upgrade_Panel = transform.GetChild(3).gameObject;
        Option_Panel = transform.GetChild(4).GetComponent<Optioin_Panel>();
        


        
        
        //����ü �迭 �ʱ�ȭ
        for (int i = 0; i < TeamColors.Length; i++)
        {
            teamColors[i].color_m = TeamColors[i];
            teamColors[i].color_c = TeamColors[i].color;
            teamColors[i].ColorNum = i;
            teamColors[i].isUsing = false;
        }


       
    }

    public void GameStart_Btn_Clicekd()
    {
        GameManager.instance.Color_Index = Team1_Color;
        GameManager.instance.T1_Color = Team2_Color;
        GameManager.instance.T2_Color = Team3_Color;
        GameManager.instance.T3_Color = Team4_Color;

        SceneManager.LoadScene(1);
    }

    public void TitlePanel_On()
    {
        //�г� on/off
        Title_Panel.SetActive(true);
        Setup_Panel.SetActive(false);
        Upgrade_Panel.SetActive(false);
        Option_Panel.gameObject.SetActive(false);


      
        BackButton.gameObject.SetActive(false);
        BackButton.enabled = false;

        //������Ʈ �� ����
        Login_Panel = Title_Panel.transform.GetChild(1).gameObject;
        Btn_Panel = Title_Panel.transform.GetChild(2).gameObject;

        Ready_Btn = Btn_Panel.transform.GetChild(0).GetComponent<Button>();
        Upgrade_Btn = Btn_Panel.transform.GetChild(1).GetComponent<Button>();
        Option_Btn = Btn_Panel.transform.GetChild(2).GetComponent<Button>();
        Exit_Btn = Btn_Panel.transform.GetChild(3).GetComponent<Button>();


        Ready_Btn.onClick.AddListener(SetupPanel_On);
        Upgrade_Btn.onClick.AddListener(UpgradePanel_On);
        Option_Btn.onClick.AddListener(OptionPanel_On);
        Exit_Btn.onClick.AddListener(Exit);

       
        CheckLogin();
    }

    public void SetupPanel_On()
    {
        Setup_Panel.SetActive(true);
        Upgrade_Panel.SetActive(false);
        Option_Panel.gameObject.SetActive(false);

        BackButton.gameObject.SetActive(true);
        BackButton.enabled = true;

        GameObject Selection = Setup_Panel.transform.GetChild(0).gameObject;

        #region ������Ʈ ����
        TeamColor1_Btn = Selection.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        TeamColor2_Btn = Selection.transform.GetChild(1).GetChild(0).GetComponent<Button>();
        TeamColor3_Btn = Selection.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        TeamColor4_Btn = Selection.transform.GetChild(3).GetChild(0).GetComponent<Button>();

        Map_img = Selection.transform.GetChild(4).GetComponent<Image>();
        Original_Btn = Selection.transform.GetChild(5).GetComponent<Button>();
        TimeAttack_Btn = Selection.transform.GetChild(6).GetComponent<Button>();
        GameStart_Btn = Selection.transform.GetChild(7).GetComponent<Button>();


        Team1_Text = TeamColor1_Btn.transform.GetChild(0).GetComponent<Text>();
        Team2_Text = TeamColor2_Btn.transform.GetChild(0).GetComponent<Text>();
        Team3_Text = TeamColor3_Btn.transform.GetChild(0).GetComponent<Text>();
        Team4_Text = TeamColor4_Btn.transform.GetChild(0).GetComponent<Text>(); 

        ID1_Text = TeamColor1_Btn.transform.GetChild(1).GetComponent<Text>();
        ID2_Text = TeamColor2_Btn.transform.GetChild(1).GetComponent<Text>();
        ID3_Text = TeamColor3_Btn.transform.GetChild(1).GetComponent<Text>();
        ID4_Text = TeamColor4_Btn.transform.GetChild(1).GetComponent<Text>();

        #endregion


        #region ��ư �Լ� ȣ��
        Original_Btn.onClick.AddListener(OriginalBtn_Clicked);
        TimeAttack_Btn.onClick.AddListener(TimeAttackBtn_Clicked);
        GameStart_Btn.onClick.AddListener(GameStart_Btn_Clicekd);

        TeamColor1_Btn.onClick.AddListener(delegate { Change_Team_Color(TeamColor1_Btn, ref Team1_Color); });
        TeamColor2_Btn.onClick.AddListener(delegate { Change_Team_Color(TeamColor2_Btn, ref Team2_Color); });
        TeamColor3_Btn.onClick.AddListener(delegate { Change_Team_Color(TeamColor3_Btn, ref Team3_Color); });
        TeamColor4_Btn.onClick.AddListener(delegate { Change_Team_Color(TeamColor4_Btn, ref Team4_Color); });

        #endregion

        ID1_Text.text = id;

        TeamColor1_Btn.image.color = teamColors[8].color_c;
        TeamColor2_Btn.image.color = teamColors[2].color_c;
        TeamColor3_Btn.image.color = teamColors[5].color_c;
        TeamColor4_Btn.image.color = teamColors[10].color_c;

        teamColors[8].isUsing = true;
        teamColors[2].isUsing = true;
        teamColors[5].isUsing = true;
        teamColors[10].isUsing = true;

        Team1_Color = teamColors[8].ColorNum;
        Team2_Color = teamColors[2].ColorNum;
        Team3_Color = teamColors[5].ColorNum;
        Team4_Color = teamColors[10].ColorNum;



    }

    public void Change_Team_Color(Button button, ref int teamNum)
    {
        Debug.Log("�÷�ü����");
      
        for (int i = teamNum; i < teamColors.Length; i++)
        {
            if (button.image.color == teamColors[i].color_c)
            {
                for (int j = 0; j < teamColors.Length; j++)
                {
                    if (i + j >= 10)
                    {
                        if (teamColors[i + j - 10].isUsing == false)
                        {
                            button.image.color = teamColors[i + j - 10].color_c;
                            teamColors[i + j - 10].isUsing = true;
                            teamColors[i].isUsing = false;
                            teamNum = i + j - 10;
                            break;
                        }
                    }
                    else
                    {
                        if (teamColors[i + j].isUsing == false)
                        {
                            button.image.color = teamColors[i + j].color_c;
                            teamColors[i + j].isUsing = true;
                            teamColors[i].isUsing = false;
                            teamNum = i + j;
                            break;
                        }
                    }
                }
                break;
            }
        }
    }
 



    public void UpgradePanel_On()
    {
        Setup_Panel.SetActive(false);
        Upgrade_Panel.SetActive(true);
        Option_Panel.gameObject.SetActive(false);

        BackButton.gameObject.SetActive(true);
        BackButton.enabled = true;



    }

    public void OptionPanel_On()
    {
        
        Setup_Panel.SetActive(false);
        Upgrade_Panel.SetActive(false);
        //Option_Panel.SetActive(true);
        Option_Panel.gameObject.SetActive(true);
        Option_Panel.OptionPanel_On();

        BackButton.gameObject.SetActive(true);
        BackButton.enabled = true;
    }


  

    public void CheckLogin()
    {
        //�α��� Ȯ��
        if (!isLogined)
        {
            Btn_Panel.SetActive(false);
            Login_Panel.SetActive(true);

            inputField = Login_Panel.transform.GetChild(0).GetComponent<InputField>();
            Confirm_Btn = Login_Panel.transform.GetChild(1).GetComponent<Button>();
            Confirm_Btn.onClick.AddListener(Login);

            //���۽� inputField�� Ŀ��
            inputField.ActivateInputField();
        }
        else
        {
            Btn_Panel.SetActive(true);
            Login_Panel.SetActive(false);
        }
    }

    public void Login()
    {
        //���Ŀ� �α��� ���� ���� ���� ��
        //�ӽ� ���̵� sunny
        if(inputField.text == id)
        {
            isLogined = true;   
        }
        else
        {
            isLogined = false;
        }
        CheckLogin();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }


    public void OriginalBtn_Clicked()
    {
        //���̹��� Original�� ����
        Debug.Log("�������� �� �̹���");
    }

    public void TimeAttackBtn_Clicked()
    {
        //���̹��� Ÿ�Ӿ������� ����
        Debug.Log("Ÿ�Ӿ��� �� �̹���");
    }

    
    public void BackBtn_Clicked()
    {
        Debug.Log("Backbutton Ŭ����");
        TitlePanel_On();
    }


}

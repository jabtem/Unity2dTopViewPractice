using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //Scripts
    public TalkManager talkManager;
    public QuestManager questManager;
    public TypeEffect talk;

    //UI
    public Animator talkPanel;
    public Animator portraitanim;
    //public Text talkText;
    public Image portraitImg;
    public GameObject menuSet;
    public Text questText;
    public Text npcName;

    //INGAME
    public GameObject player;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public Sprite prevPortarit;

    void Start()
    {
        GameLoad();
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {
        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }

    }

    public void SubMenuActive()
    {
        if (menuSet.activeSelf)//이미 menuSet이 켜져있는경우 esc로 다시끌수있게 설정
        {
            menuSet.SetActive(false);
        }
        else
            menuSet.SetActive(true);

    }
    public void Action(GameObject scanObj)
    {
        if (scanObj == null)//Exit Action, 스캔대상이 없을때
        {
            isAction = false;
            talkIndex = 0;
        }
        else//Enter Action, 스캔대상이 있을때
        {
            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNPC,objData.Name);
        }

        //대화UI 활성
        talkPanel.SetBool("isShow",isAction);
    }

    void Talk(int id, bool isNPC, string Name)
    {
        int questTalkIndex=0;
        string talkData ="";
        //Set Talk Data
        if (talk.isanim)//현재 텍스트애니메이션이 출력중일때 조사키 다시입력시 대화문스킵
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }

        //대화 종료
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            npcName.gameObject.SetActive(false);
            return;//강제종료
        }
            

        //대화 진행
        if (isNPC)
        {
            npcName.gameObject.SetActive(true);
            talk.SetMsg(talkData.Split(':')[0]);//대화문
            npcName.text = Name;

            //Show Portrait
            portraitImg.sprite = talkManager.GetPortait(id,  int.Parse(talkData.Split(':')[1])) ;//스프라이트
            portraitImg.color= new Color(1,1,1,1);

            //Animation Portrait
            if(prevPortarit != portraitImg.sprite)
            {
                portraitanim.SetTrigger("doEffect");
                prevPortarit = portraitImg.sprite;
            }

        }
        else
        {
   
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX",player.transform.position.x);
        //Plyaer Y
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //Quest ID
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        //QUest index
        PlayerPrefs.SetInt("QuestIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);

    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))//저장여부확인 저장데이터가 아예없을경우
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questIndex = PlayerPrefs.GetInt("QuestIndex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questIndex;
        questManager.ObjectControl();

    }
    public void GameExit()
    {
        Application.Quit();//프로그램종료함수
    }
}

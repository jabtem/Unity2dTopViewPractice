using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화순서
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();


    }

    // Update is called once per frame
    void GenerateData()
    {
        questList.Add(10, new QuestData("마을사람과 대화",new int[] {1000,2000} ));
        questList.Add(20, new QuestData("동전찾기", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트클리어", new int[] {0}));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //Next Talk Target
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //Control Quest Object
        ObjectControl();

        //NextQuest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();


        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;//퀘스트이름 리턴
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ObjectControl()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;

            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);

                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}

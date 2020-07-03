using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    // Start is called before the first frame update
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraiArr;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        //Talk Data
        //NPCA:1000, B:2000
        //Box:100 , Desk:200

        talkData.Add(1000, new string[] { "안녕!:0" ,
                                          "처음이니?:1",
                                          "반가워!:2" });
        talkData.Add(2000, new string[] { "여어:1", "아름다운 호수야:0","여기엔 무언가 비밀이 있다고해:1" });
        talkData.Add(100, new string[] { "평범한 상자다." });
        talkData.Add(200, new string[] { "평범한 책상이다." });

        //Quest Talk
        //Q1
        talkData.Add(10+1000, new string[] { "어서와!:0" ,
                                             "옆에 호수엔 무슨 비밀이있다고해:1",
                                             "오른쪽 사람에게 물어보면 될거야:2"});
        talkData.Add(11+2000, new string[] { "여어!:0" ,
                                             "호수의 비밀을 들으러온거야?:1",
                                             "내가흘린 동전을 주워다주면 알려줄게:2"});
        //Q2
        talkData.Add(20 + 1000, new string[] { "동전?:0" ,
                                             "호수 근처에서 봤던것같아:1"});
        talkData.Add(20 + 2000, new string[] { "꼭 좀 찾아줘.:1" });

        talkData.Add(20 + 5000, new string[] { "이 동전인것 같다."});
        talkData.Add(21 + 2000, new string[] { "찾아줘서 고마워!:2" });


        //NPCA
        portraitData.Add(1000+0, portraiArr[0]);
        portraitData.Add(1000+1, portraiArr[1]);
        portraitData.Add(1000+2, portraiArr[2]);
        portraitData.Add(1000+3, portraiArr[3]);
        //NPCB
        portraitData.Add(2000 + 0, portraiArr[4]);
        portraitData.Add(2000 + 1, portraiArr[5]);
        portraitData.Add(2000 + 2, portraiArr[6]);
        portraitData.Add(2000 + 3, portraiArr[7]);

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))//Containskey: Dictionary에 key유무 확인
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //퀘스트 대사가 아예 없을경우
                //기본 대사만 가져옴
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {
                //해당퀘스트 진행 순서 대사가 없을 때
                //퀘스트 맨처음 대사를 가져옴
                return GetTalk(id - id % 10, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];

    }

    public Sprite GetPortait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];//id+index -> key , key에 맞는 value return
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int textSpeed;
    public bool isanim;
    public GameObject EndCursor;
    AudioSource audioSource;

    string Msg;
    Text msgText;
    int textIndex;
    

    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string txt)
    {
        if (isanim)//Text Skip
        {
            msgText.text = Msg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            Msg = txt;
            EffectStart();

        }
    }

    void EffectStart()
    {

        msgText.text = "";
        textIndex = 0;
        EndCursor.SetActive(false);
        isanim = true;

        Invoke("Effecting", 0.4f);//대화UI가 올라온후 출력시작, 현재 대화문 show애니메이션 텀이 0.2초

    }
    void Effecting()
    {
        //End Animation
        if (msgText.text == Msg)
        {
            EffectEnd();
            return;
        }
        msgText.text += Msg[textIndex];

        //Sound
        if (Msg[textIndex] != ' ' || Msg[textIndex] != '.')// 공백과 마침표는 제외하고 사운드실행
            audioSource.Play();

        textIndex++;
        Invoke("Effecting", 1f/textSpeed);
    }
    void EffectEnd()
    {
        EndCursor.SetActive(true);
        isanim = false;
    }
}

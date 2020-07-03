using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameManager manager;
    float h;
    float v;
    bool isH;
    public float maxSpeed;
    Vector3 dirVec;
    GameObject scanObject;
    Rigidbody2D rigid;
    Animator anim;

    //Mobile Key
    int U_Value;
    int D_Value;
    int R_Value;
    int L_Value;
    bool u_Down;
    bool d_Down;
    bool r_Down;
    bool l_Down;
    bool u_Up;
    bool d_Up;
    bool r_Up;
    bool l_Up;



    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal")+ R_Value + L_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical")+ U_Value + D_Value;

        //Check Boutton Down & Up
        bool hDown = manager.isAction ? false : Input.GetButton("Horizontal")|| r_Down || l_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal")|| r_Up || l_Up;
        bool vDown = manager.isAction ? false : Input.GetButton("Vertical") || u_Down || d_Down;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || u_Up || d_Up;
        //대화창이 켜져있는동안 행동을 막음



        //Check Horizontal Move && Check Vertical Move

        if (hDown)
        {
            isH = true;
            v = 0;
        }
        else if (hUp || vUp)
        {
            isH = h != 0;
        }

        if (vDown)
        {
            isH = false;
            h = 0;
        }


        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);



        //Direction



        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
        }

        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
        }
        else if (h != 0 || v != 0)
            dirVec = new Vector2(h, v);




        //Scan Object
        if (Input.GetButtonDown("Jump"))
        {
            manager.Action(scanObject);
        }


    }

    void FixedUpdate()
    {
        /*
        if (isH)
        {
            rigid.velocity = new Vector2(h, 0) * maxSpeed;

        }
        else if (isV)
        {
            rigid.velocity = new Vector2(0, v) * maxSpeed;
        }
        */
        //Move
        Vector2 moveVec = isH ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * maxSpeed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f,LayerMask.GetMask("Object"));

        //Rayhit ScanObject
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
            scanObject = null;
    }
    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "L":
                L_Value = -1;
                l_Down = true;
                l_Up = false;
                break;
            case "R":
                R_Value = 1;
                r_Down = true;
                r_Up = false;
                break;
            case "U":
                U_Value = 1;
                u_Down = true;
                u_Up = false;
                break;
            case "D":
                D_Value = -1;
                d_Down = true;
                d_Up = false;
                break;
            case "A":
                manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }
    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "L":
                L_Value = 0;
                l_Up = true;
                l_Down = false;
                break;
            case "R":
                R_Value = 0;
                r_Up = true;
                r_Down = false;
                break;
            case "U":
                U_Value = 0;
                u_Up = true;
                u_Down = false;
                break;
            case "D":
                D_Value = 0;
                d_Up = true;
                d_Down = false;
                break;
        }
    }
}

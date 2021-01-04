using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using Uduino;

public class Monster : MonoBehaviour
{
    GameManager gameManager;
    //设定没打到就自动消失到秒数   
    public float maxSecondsOnScreen=10.0f;
    public float currentSecondsOnScreen=0;
    public Animator animator;


    private Vector2 startPos;
    public int LED = 0;
    [SerializeField] private Transform correctTans1;
    [SerializeField] private Transform correctTans2;
    [SerializeField] private Transform correctTans3;
    [SerializeField] private Transform correctTans4;
    [SerializeField] private Transform correctTans5;//holes
    [SerializeField] private bool isFinished;//拖拽成功与否
    [SerializeField] private bool isInHole = false;
    [SerializeField] public bool InHole = false;
    private bool isHit = false;
    public int GetHole;
    [SerializeField] public int countInHole = 0;


    private int tempNumber;





    private void Start()
    {
        startPos = transform.position;
        gameManager = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        ResetCurrentSecondsOnScreen ();

        ////////////
        UduinoManager.Instance.pinMode(5,PinMode.Output);
        UduinoManager.Instance.pinMode(4,PinMode.Output);
        UduinoManager.Instance.pinMode(14,PinMode.Output);
        UduinoManager.Instance.pinMode(15,PinMode.Output);
        UduinoManager.Instance.pinMode(12,PinMode.Output);
        UduinoManager.Instance.pinMode(16,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(13,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(0,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(2,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(3,PinMode.Input_pullup);
    }
    private void Update()
    {
        if (isFinished)
        {
            isFinished = false;
            Instantiate(this);
            transform.position=new Vector2(startPos.x,startPos.y);
            animator.SetBool("Drag", false);
            isInHole = false;
            countInHole+=1;
            print(countInHole); 
        }
        int cat_here1 = UduinoManager.Instance.digitalRead(16);
        if(cat_here1 == 0){
            UduinoManager.Instance.digitalWrite(5, State.LOW);
            // isHit = true;
            tempNumber = 1;
            comparison();
        }
        int cat_here2 = UduinoManager.Instance.digitalRead(13);
        if(cat_here2 == 0){
            UduinoManager.Instance.digitalWrite(4, State.LOW);
            // isHit = true;
            tempNumber = 2;
            comparison();
            //animator.SetBool("Hit", true);
        }
        int cat_here3 = UduinoManager.Instance.digitalRead(0);
        if(cat_here3 == 0){
            UduinoManager.Instance.digitalWrite(14, State.LOW);
            // isHit = true;
            comparison();
             tempNumber = 3;
            //animator.SetBool("Hit", true);
        }
         int cat_here4= UduinoManager.Instance.digitalRead(2);
        if(cat_here4 == 0){
            UduinoManager.Instance.digitalWrite(15, State.LOW);
            tempNumber = 4;
            comparison();
            //animator.SetBool("Hit", true);
        }
        int cat_here5= UduinoManager.Instance.digitalRead(3);
        if(cat_here5 == 0){
            UduinoManager.Instance.digitalWrite(12, State.LOW);

            //isHit = true;
            //animator.SetBool("Hit", true);
        }
        TryCountDownToHide ();

    }

    private void OnMouseDrag()
    {
        if(!isFinished)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
        // if(isFinished)
        // {
        //     gameManager = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
        //     ResetCurrentSecondsOnScreen ();
        // }
        animator.SetBool("Drag", true);
    }

    private void OnMouseUp()
    {
        // 相对应的怪兽和洞距离之间足够小 0.5f， isFinished = True
        // else false， back to original pos
        // hardcode大法
        // countInHole+=1;
        if (Mathf.Abs(transform.position.x - correctTans1.position.x) <= 0.7f && Mathf.Abs(transform.position.y - correctTans1.position.y) <= 0.7f)
        {
            transform.position = new Vector2(correctTans1.position.x + 0.07f, correctTans1.position.y+0.5f);
            isFinished = true;
            isInHole = true;
            InHole = true;
            LED = 5;
            UduinoManager.Instance.digitalWrite(LED, State.HIGH);
            GetHole = 1;

        }
        else if (Mathf.Abs(transform.position.x - correctTans2.position.x) <= 0.7f && Mathf.Abs(transform.position.y - correctTans2.position.y) <= 0.7f)
        {
            transform.position = new Vector2(correctTans2.position.x + 0.07f, correctTans2.position.y+0.5f);
            isFinished = true;
            isInHole = true;
            InHole = true;
            LED = 4;
            UduinoManager.Instance.digitalWrite(LED, State.HIGH);
            GetHole =2;
        }
        else if (Mathf.Abs(transform.position.x - correctTans3.position.x) <= 0.7f && Mathf.Abs(transform.position.y - correctTans3.position.y) <= 0.7f)
        {
            transform.position = new Vector2(correctTans3.position.x + 0.07f, correctTans3.position.y+0.5f);
            isFinished = true;
            isInHole = true;
            InHole = true;
            LED = 14;
            UduinoManager.Instance.digitalWrite(LED, State.HIGH);
            GetHole =3;

        }
        else if (Mathf.Abs(transform.position.x - correctTans4.position.x) <= 0.7f && Mathf.Abs(transform.position.y - correctTans4.position.y) <= 0.7f)
        {
            transform.position = new Vector2(correctTans4.position.x + 0.07f, correctTans4.position.y+0.5f);
            isFinished = true;
            isInHole = true;
            InHole = true;
            LED = 15;
            UduinoManager.Instance.digitalWrite(LED, State.HIGH);
            GetHole =4;

        }
        else if (Mathf.Abs(transform.position.x - correctTans5.position.x) <= 0.7f && Mathf.Abs(transform.position.y - correctTans5.position.y) <= 0.7f)
        {
            transform.position = new Vector2(correctTans5.position.x + 0.07f, correctTans5.position.y+0.5f);
            isFinished = true;
            isInHole = true;
            InHole = true;
            LED = 12;
            UduinoManager.Instance.digitalWrite(LED, State.HIGH);
            GetHole =5;

        }
        else
        {
            transform.position = new Vector2(startPos.x,startPos.y);
            isFinished = false;
            animator.SetBool("Drag", false);
            
        }
    }
    
    private void comparison()
    {
        if (tempNumber == GetHole)
        {
            
            animator.SetBool("Hit", true);
        
        }

    }
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.07f;
    }
    // void Start()
    // {
    //     Init ();
    // }

    // private void Init ()
    // {
    //     //找到老鼠
    //     gameManager = FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
    //     ResetCurrentSecondsOnScreen ();
    // }

    // private void OnMouseDown ()
    // {
    //     //调用addscore
    //     //Addscore 不再这里调用了 在猫爪那里
    //     // 能够实现复制 拖拽 
    //     // hide 改成 show
    //     gameManager.AddScore ();
    //     ResetCurrentSecondsOnScreen ();
    //     Show ();
    // }

    // 改成show 老鼠
    private void Show ()
    {
        // 改成 .ShowMonster
        gameManager.ShowMonster ( gameObject );
    }

    private void Hide ()
    {
        // 改成 .ShowMonster
        gameManager.HideMonster ( gameObject );
        countInHole = countInHole-1;
        print(countInHole); 
        UduinoManager.Instance.digitalWrite(LED, State.LOW);
    }

    // private void Hit()
    // {
    //     gameManager.HitMonster ( gameObject );
    // } 


    // IsActive条件改成检测是否在洞里
    public bool IsActive => gameObject.activeSelf;
    bool OnScreenTimeUp => currentSecondsOnScreen < 0;

    // 更新游戏 ///////////////////////////////////////////////////////////////////////////////////
    // if count >=4?
    //  random 0 1 0是删除 1是保留
    // void FixedUpdate()
    // {
    //     TryCountDownToHide ();
    // }

    private void TryCountDownToHide ()
    {
        //如果是在荧幕上的话 就开始即时
        // 改如果是在洞里
        if ( IsActive && isInHole)
        {
            // UduinoManager.Instance.digitalWrite(5, State.HIGH);
            CountDownCurrentSecondsOnScrren ();
            animator.SetBool("Inhole", true);
            // gameManager.AddScore ();
            // if (isHit){

            // }
        }


        //如果时间到了 重制时间  hide
        if ( OnScreenTimeUp )
        {
            ResetCurrentSecondsOnScreen ();
            Hide ();
        }
    }
    //一直剪短时间
    private void CountDownCurrentSecondsOnScrren ()
    {
        currentSecondsOnScreen -= Time.fixedDeltaTime;
    }
    //重制秒数
    private void ResetCurrentSecondsOnScreen ()
    {
        currentSecondsOnScreen = maxSecondsOnScreen;
    }

    //////读取整栋的信息 
    //// 1 灯消失 加 播放动画 
}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.Linq;
using Uduino;


//多加一个 老鼠按钮点击的
public class GameManager : MonoBehaviour
{

    [SerializeField] float showMonsterIntervalSeconds=2;
    [SerializeField] float countDownShowMonsterSeconds;
    int MAX_MONSTERS_ON_SCREEN=3;

    public List<Monster> monsters;
    Text score;
    public Animator animator;
    int scoreNumber=0;

    // private int tempNumber;

    public void HideMonster ( GameObject monster ) {
        monster.SetActive ( false );
    }

    // public void HitMonster ( GameObject monster ) {

    //     monster.GetComponent<Animation>().Play("mouse_hit1");
    // }
    public void ShowMonster ( GameObject monster ) { 
        monster.SetActive (true);
    }
    

    //增加分数
    public void AddScore () {
        //一只10分
        scoreNumber += 10;
        score.text = scoreNumber.ToString();
    }

    void Start()
    {
        InitScore ();
        InitMonsterList ();
        // HideAllMonsters ();
        //ShowRandomMonster ();
        ResetShowMonserSeconds ();
        monsters = new List<Monster>();
        UduinoManager.Instance.pinMode(16,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(13,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(0,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(2,PinMode.Input_pullup);
        UduinoManager.Instance.pinMode(3,PinMode.Input_pullup);
    }

    private void ResetShowMonserSeconds ()
    {
        countDownShowMonsterSeconds = showMonsterIntervalSeconds;
    }

    // //可能要改 把所以monster都隐藏起来
    // void HideAllMonsters () {
    //     foreach ( var m in monsters )
    //     {
    //         HideMonster ( m.gameObject );
    //     }
    // }

    //所有hidden的monster ////////////////////////////////////////////////////
    List<Monster> InHoleMonsters {
        get {
            var result=new List<Monster>();

            foreach ( var m in monsters )
            {
                //如果不是active 
                //之后要改 如果是active 加到这个list里面
                if (m.InHole)
                {
                    result.Add(m);
                }
            }

            return result;
        }
    }
    

    // int MonsterCountOnScreen {
    //     get {
    //         int result=0;
    //         foreach ( var m in monsters )
    //         {
    //             if ( m.InHole )
    //             {
    //                 result += 1;
    //             }
    //         }
    //         return result;
    //     }
    // }

    // 这里可以改 
    // 改成随机大一只老鼠

    void HideRandomMonster () {
        // 0 到 最大值老鼠到总数 
        // print("count");
        // print(InHoleMonsters.Count);
        int r=Random.Range(0,InHoleMonsters.Count);
        //取老鼠
        Monster m=InHoleMonsters[r];
        HideMonster ( m.gameObject );
    }

    private void InitMonsterList ()
    {
        monsters = GameObject.FindObjectsOfType<Monster> ().ToList ();
    }

    // private void GetMonster(){
    //     GameObject[] ms = GameObject.FindGameObjectWithTag("Monster");

    //     foreach(var o in ms){
    //         if(!monsters.Contains(o.GetComponent<Monster>())){
    //             monsters.Add(o.GetComponent<Monster>());
    //         }

    //     }
    // }

    // private void comparison()
    // {
    //     if(InHoleMonsters.Count == 0){
    //         Debug.Log("meiyou");
    //         return;
    //     }

    //     foreach(var o in InHoleMonsters)
    //     {
    //         if (tempNumber == o.GetHole)
    //         {
    //             InHoleMonsters.Remove(o);
    //             animator.SetBool("Hit", true);
    //             Destroy(o.gameObject);
    //             Debug.Log(o.GetHole);
    //         }
    //     }

    // }

    private void InitScore ()
    {
        // 找score并且找到component Text类型物件
        score = GameObject.FindGameObjectWithTag ( "Score" ).GetComponent<Text> ();
        // 空的文字内容
        score.text = "";
    }

    void FixedUpdate()
    {
        // TryCountDownToShowMonster ();
        if (InHoleMonsters.Count >= MAX_MONSTERS_ON_SCREEN)
        {
            print("1");
            HideRandomMonster ();
        }

        // int cat_here1 = UduinoManager.Instance.digitalRead(16);
        // if(cat_here1 == 0){
        //     UduinoManager.Instance.digitalWrite(5, State.LOW);
        //     tempNumber = 1;
        //     comparison();
        //     animator.SetBool("Hit", true);

        // }
        // int cat_here2 = UduinoManager.Instance.digitalRead(13);
        // if(cat_here2 == 0){
        //     UduinoManager.Instance.digitalWrite(4, State.LOW);
        //     tempNumber = 2;
        //     comparison();
        //     //animator.SetBool("Hit", true);
        // }
        // int cat_here3 = UduinoManager.Instance.digitalRead(0);
        // if(cat_here3 == 0){
        //     UduinoManager.Instance.digitalWrite(14, State.LOW);
        //     tempNumber = 3;
        //     comparison();
        //     //animator.SetBool("Hit", true);
        // }
        //  int cat_here4= UduinoManager.Instance.digitalRead(2);
        // if(cat_here4 == 0){
        //     UduinoManager.Instance.digitalWrite(15, State.LOW);
        //     tempNumber = 4;
        //     comparison();

        //     //animator.SetBool("Hit", true);
        // }
        // int cat_here5= UduinoManager.Instance.digitalRead(3);
        // if(cat_here5 == 0){
        //     UduinoManager.Instance.digitalWrite(12, State.LOW);
        //     //isHit = true;
        //     //animator.SetBool("Hit", true);
        // }
    }


    // bool CountDownShowMonsterTimeUp => countDownShowMonsterSeconds <= 0;
    // bool MonstersOnScreenAreFull => InHoleMonsters.Count >= MAX_MONSTERS_ON_SCREEN;

    // private void TryCountDownToShowMonster ()
    // {
    //     // countDownShowMonsterSeconds -= Time.fixedDeltaTime;
    //     // if (CountDownShowMonsterTimeUp)
    //     // {
    //     //     ResetShowMonserSeconds ();

        // if (!MonstersOnScreenAreFull)
        // {
        //     HideRandomMonster ();
        // }
    //     //}
    // }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5f; //移动速度

	private int maxHealth = 5; //最大生命值

	private int currentHealth; //当前生命值

	public int MyMaxHealth { get { return maxHealth; } }

	public int MyCurrentHealth { get { return currentHealth; } }

	private float invincibleTime = 2f; //无敌时间2秒

	private float invincibleTimer; //无敌计时器

	private bool isInvincible;//是否处于无敌状态

	public GameObject bulletPrefab; //子弹

	//=================玩家音效
	public AudioClip hitClip; //受伤
	public AudioClip launchClip;//齿轮

	//===============玩家方向===============
	private Vector2 lookDirection = new Vector2(1, 0);//默认方向

	//===========玩家子弹数量=================
	[SerializeField]
	private int maxbulletCount = 99;

	private int curBulletCount;

	public int MyCurBulletCount { get { return curBulletCount; } } 
	public int MyMaxBulletCount { get { return maxbulletCount; } } 


	Rigidbody2D rbody; //刚体组件

	Animator anim;

	// Use this for initialization
	void Start () {
		
		currentHealth = maxHealth;
		curBulletCount = 5;  //初始化子弹数量为5
		invincibleTimer = 0;
		rbody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		UImanager.instance.UpdateBulletCount(curBulletCount, maxbulletCount);
	}
	
	// Update is called once per frame
	void Update () {
		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		Vector2 moveVector = new Vector2(moveX, moveY);
		if (moveVector.x != 0 || moveVector.y != 0)
        {
			lookDirection = moveVector;
        }
		anim.SetFloat("Look X", lookDirection.x);
		anim.SetFloat("Look Y", lookDirection.y);
		anim.SetFloat("Speed", moveVector.magnitude);


		//移动
		Vector2 position = rbody.position;
		//position.x += moveX * speed * Time.deltaTime;
		//position.y += moveY * speed * Time.deltaTime;
		position += moveVector * speed * Time.deltaTime;
		rbody.MovePosition(position);

		//无敌计时
		if (isInvincible)
        {
			invincibleTimer -= Time.deltaTime;
			if (invincibleTimer < 0)
            {
				isInvincible = false; //倒计时结束，取消无敌
            }
        }

		//检测玩家是否按下Q键进行攻击
		if (Input.GetKeyDown(KeyCode.Q) && curBulletCount > 0)
        {
			anim.SetTrigger("Launch");//播放攻击动画
			ChangeBulletCount(-1);
			AudioManager.instance.AudioPlay(launchClip);
			GameObject bullet = Instantiate(bulletPrefab, rbody.position + Vector2.up * 0.5f, Quaternion.identity);
			BulletController bc = bullet.GetComponent<BulletController>();
			if (bc != null)
            {
				bc.Move(lookDirection, 300);
            }
        }

		//按下E建进行NPC交互
		if (Input.GetKeyDown(KeyCode.E))
        {
			RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
			if (hit.collider != null)
            {
				NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
				if (npc != null)
                {
					npc.ShowDialog();//显示对话框
                }
            }
        }
	}

	/// <summary>
    /// 玩家生命值操作
    /// </summary>
    /// <param name="amount"></param>
	public void ChangeHealth(int amount)
    {
		//如果玩家收到伤害
		if (amount < 0)
        {
			if (isInvincible == true)
            {
				return;
			}
			isInvincible = true;
			anim.SetTrigger("Hit");
			AudioManager.instance.AudioPlay(hitClip);
			invincibleTimer = invincibleTime;
				
        }

		//约束玩家生命值，最大不超过maxHealth，最小不能是0
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		UImanager.instance.UpdateHealthBar(currentHealth,maxHealth);//更新血条
		Debug.Log("当前生命值："+currentHealth+"血，最大生命值为："+maxHealth);
    }
	public void ChangeBulletCount(int amount)
    {
		curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxbulletCount);
		UImanager.instance.UpdateBulletCount(curBulletCount, maxbulletCount);
    }
}

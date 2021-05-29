using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敌人控制
/// </summary>
public class EnemyController : MonoBehaviour {

	public float speed = 3; //移动

	public float changeDirectionTime = 2f; //改变方向的时间

	public bool isVertical;//是否垂直方向移动

	private float changeTimer; //改变方向的计时器

	private Vector2 moveDirection; //移动方向

	public ParticleSystem brokenEffect;//损坏特效

	public AudioClip fixedClip; //被修复的音效

	private bool isFixed; // 是否被修复了

	private Rigidbody2D rbody;

	private Animator anim;

	// Use this for initialization
	void Start () {

		rbody = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();

		moveDirection = isVertical ? Vector2.up : Vector2.right;

		changeTimer = changeDirectionTime;

		isFixed = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (isFixed) return; 
		changeTimer -= Time.deltaTime;
		if (changeTimer < 0)
        {
			moveDirection *= -1;
			changeTimer = changeDirectionTime;
        }

		Vector2 position = rbody.position;
		position.x += moveDirection.x * speed * Time.deltaTime;
		position.y += moveDirection.y * speed * Time.deltaTime;
		rbody.MovePosition(position);
		anim.SetFloat("moveX", moveDirection.x);
		anim.SetFloat("moveY", moveDirection.y);

	}

	void OnCollisionEnter2D(Collision2D other)
    {
		PlayerController pc = other.gameObject.GetComponent<PlayerController>();
		if (pc != null)
        {
			pc.ChangeHealth(-1);
		}
    }

	/// <summary>
    /// 敌人被修复
    /// </summary>
  	public void Fixed()
    {
		isFixed = true;
		rbody.simulated = false; //禁用物理
		AudioManager.instance.AudioPlay(fixedClip);
		anim.SetTrigger("fix");//播放被修复的动画
		if (brokenEffect.isPlaying == true)
		{
			brokenEffect.Stop();
		}
	}
}

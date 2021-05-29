using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制子弹的移动碰撞
/// </summary>
public class BulletController : MonoBehaviour {

	Rigidbody2D rbody;

	public AudioClip hitClip;

	// Use this for initialization
	void Awake () {
		rbody = GetComponent<Rigidbody2D>();
		Destroy(this.gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
    /// 子弹的移动
    /// </summary>
	public void Move(Vector2 moveDirection,float moveForce)
    {
		rbody.AddForce(moveDirection * moveForce);
    }

	void OnCollisionEnter2D(Collision2D other)
    {

		EnemyController ec = other.gameObject.GetComponent<EnemyController>();
		if(ec != null)
        {
			ec.Fixed();
        }
		AudioManager.instance.AudioPlay(hitClip);//播放命中
		Destroy(this.gameObject);
    }
}

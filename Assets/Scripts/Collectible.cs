using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 获取生命脚本
/// </summary>
public class Collectible : MonoBehaviour {

	public ParticleSystem collectEffect; //拾取特效

	public AudioClip collectClip; //拾取音效

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
    /// 碰撞检测
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerEnter2D(Collider2D other)
    {
		PlayerController pc = other.GetComponent<PlayerController>();
		if (pc != null)
        {
			if (pc.MyCurrentHealth < pc.MyMaxHealth)
            {
				pc.ChangeHealth(1);
				Instantiate(collectEffect, transform.position, Quaternion.identity); //生成特效
				AudioManager.instance.AudioPlay(collectClip);//播放音效
				Destroy(this.gameObject);
            }
        }
    }
}

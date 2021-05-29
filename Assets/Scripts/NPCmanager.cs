using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC交互
/// </summary>
public class NPCmanager : MonoBehaviour {

	public GameObject tipImage; //按键提示

	public GameObject dialogImage;//对话

	public float showTime = 4;//对话框显示时间

	private float showTimer; //对话框显示计时器

	// Use this for initialization
	void Start () {
		tipImage.SetActive(true); //初始默认显示提示
		dialogImage.SetActive(false);//初始隐藏对话框
		showTimer = -1;
	}
	
	// Update is called once per frame
	void Update () {
		showTimer -= Time.deltaTime;
		if (showTimer < 0)
        {
			tipImage.SetActive(true);
			dialogImage.SetActive(false);
        }
	}

	public void ShowDialog()
    {
		showTimer = showTime;
		tipImage.SetActive(false);
		dialogImage.SetActive(true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 血条
/// </summary>
public class UImanager : MonoBehaviour {

    
    public static UImanager instance { get; private set; }

    void Start()
    {
        instance = this;
    }

	public Image healthBar; // 角色血条

    public Text bulletCountText; //子弹的数量

    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>
	public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    
    public void UpdateBulletCount(int curAmount,int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + "/" + maxAmount.ToString();
    }
}

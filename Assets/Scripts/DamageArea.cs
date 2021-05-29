﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 伤害陷阱
/// </summary>
public class DamageArea : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }
}

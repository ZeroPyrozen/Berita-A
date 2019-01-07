﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "Win")
        {
            GameManager.instance.RoundEnd(true);
        }
    }
}

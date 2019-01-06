using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour {

    public Canvas dimensionChange;

    [SerializeField] private float cooldown;
    [SerializeField] private float skillCD;

    [SerializeField] private bool changed;

	void Start () {
        changed = false;
        cooldown = 0f;
	}
	
	void Update () {
        if(cooldown>0) cooldown -= Time.deltaTime;
        Skill();
	}

    private void Skill()
    {
        if (Input.GetKeyDown(KeyCode.C) && cooldown <= 0)
        {
            changed = !changed;
            cooldown = 5f;
        }
        if (changed)
        {
            dimensionChange.gameObject.SetActive(true);
            GameManager.instance.dimensionType = 2;
        }
        else
        {
            dimensionChange.gameObject.SetActive(false);
            GameManager.instance.dimensionType = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour {

    public GameObject background;
    public Canvas dimensionChange;
    private Animator bgAnim;

    [SerializeField] private float cooldown;
    [SerializeField] private float skillCD;

    [SerializeField] private bool changed;

	void Start () {
        changed = false;
        cooldown = 0f;
        bgAnim = background.GetComponent<Animator>();
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
            bgAnim.SetBool("changeDim", true);
            dimensionChange.gameObject.SetActive(true);
            GameManager.instance.dimensionType = 2;
        }
        else
        {
            bgAnim.SetBool("changeDim", false);
            dimensionChange.gameObject.SetActive(false);
            GameManager.instance.dimensionType = 1;
        }
    }
}

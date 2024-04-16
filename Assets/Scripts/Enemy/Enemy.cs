using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform targetTransform;
	[SerializeField] private int moveSpeed;
	[SerializeField] private int attackDamage;
	[SerializeField] private int hp;
	void Start()
    {
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
    void Update()
    {
		if (GameManager.Instance.isGameOver)
		{
			return;
		}

		Vector3 moveTo = (targetTransform.position - transform.position).normalized;
		transform.position += moveTo * moveSpeed * Time.deltaTime;

		Vector3 currScale = transform.localScale;
		if (moveTo.x > 0)
		{
			transform.localScale = new Vector3
				(-Mathf.Abs(currScale.x), currScale.y, currScale.z);

		}
		else
		{
			transform.localScale = new Vector3
				(Mathf.Abs(currScale.x), currScale.y, currScale.z);

		}
	}

    public void EnemyDamaged(int dmg)
    {
	    hp -= dmg;
	    if(hp <= 0)
	    {
		    hp = 0;
		    this.gameObject.SetActive(false);
	    }
    }
}

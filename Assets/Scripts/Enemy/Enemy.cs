using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform targetTransform;
	[SerializeField] private float moveSpeed;
	[SerializeField] private float attackDamage;
	void Start()
    {
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
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
}

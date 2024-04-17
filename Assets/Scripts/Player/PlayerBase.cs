using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;
public abstract class PlayerBase : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	[SerializeField] private Vector3 inputVec;
	//private Rigidbody2D _rigid;
	private SkeletonAnimation _skeletonAnimation;
	private Spine.AnimationState _spineAnimationState;
	//Protected Transform PlayerTrans;
	protected virtual void Awake()
	{
		//_rigid = GetComponent<Rigidbody2D>();
		_skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
		_spineAnimationState = _skeletonAnimation.AnimationState;
	}
	protected virtual void Start()
	{
		//PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;
		PlayAnimation("start", false);
	}

	protected virtual void Update()
	{
		if (inputVec.x != 0)
		{
			if (inputVec.x > 0)
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			else if (inputVec.x < 0)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
		}

		if (inputVec != Vector3.zero)
		{
			PlayAnimation("run", true);
		}
		else
		{
			PlayAnimation("idle", true);
		}
		
	}
	protected virtual void FixedUpdate()
	{
		Vector3 nextVec = inputVec * playerSpeed * Time.fixedDeltaTime;
		transform.position += nextVec * playerSpeed * Time.fixedDeltaTime;
		//_rigid.MovePosition(_rigid.position + nextVec);
	}
	private void PlayAnimation(string animationName, bool loop)
	{
		// 현재 재생 중인 애니메이션이 이미 지정된 애니메이션이면 재생하지 않음
		if (_spineAnimationState.GetCurrent(0) == null || 
		    _spineAnimationState.GetCurrent(0).Animation.Name != animationName)
		{
			_spineAnimationState.SetAnimation(0, animationName, loop);
		}
	}
	protected void PlayHitAnimation()
	{
		PlayAnimation("hurt", false);
	}

	protected void PlayDeadAnimation()
	{
		PlayAnimation("die", false);
	}


	protected void OnMove(InputValue value)
	{
		inputVec = value.Get<Vector2>();
	}

	protected abstract void CharSkill();

}

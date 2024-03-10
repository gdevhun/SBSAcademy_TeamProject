using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	[SerializeField] private Vector2 inputVec;
	private Rigidbody2D _rigid;
	private Animator _anim;
	public SkeletonAnimation skeletonAnimation;
	private Spine.AnimationState _spineAnimationState;
	[SpineAnimation] public string runAnimationName;
	[SpineAnimation] public string idleAnimationName;
	[SpineAnimation] public string startAnimationName;
	[SpineAnimation] public string deadAnimationName;
	[SpineAnimation] public string hitAnimationName;

	private void Awake()
	{
		_rigid = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
	}
	private void Start()
	{
		skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
		_spineAnimationState = skeletonAnimation.AnimationState;

		PlayAnimation("start", true);
	}

	private void Update()
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

		if (inputVec != Vector2.zero)
		{
			PlayAnimation("run", true);
		}
		else
		{
			PlayAnimation("idle", true);
		}
		
	}
	private void FixedUpdate()
	{

		Vector2 nextVec = inputVec * playerSpeed * Time.fixedDeltaTime;
		_rigid.MovePosition(_rigid.position + nextVec);
	}
	void PlayAnimation(string animationName, bool loop)
	{
		// 현재 재생 중인 애니메이션이 이미 지정된 애니메이션이면 재생하지 않음
		if (_spineAnimationState.GetCurrent(0) == null || _spineAnimationState.GetCurrent(0).Animation.Name != animationName)
		{
			_spineAnimationState.SetAnimation(0, animationName, loop);
		}
	}
	public void PlayHitAnimation()
	{
		PlayAnimation("hurt", false);
	}

	public void PlayDeadAnimation()
	{
		PlayAnimation("die", false);
	}


	private void OnMove(InputValue value)
	{
		inputVec = value.Get<Vector2>();
	}
}

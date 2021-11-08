using UnityEngine;
using System.Collections.Generic;

public class HealBehaviour : GenericBehaviour
{
	public string healButton = "Heal";
	private int healBool;
	private bool heal = false;
	public bool healTrigger = false;
	public List<GameObject> particles = new List<GameObject> ();

	void Start()
	{

		healBool = Animator.StringToHash("Heal");

		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if ((Input.GetButtonDown(healButton)||healTrigger) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			heal = !heal;
			healTrigger = false;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			if (heal)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);

				ParticleEffects();
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		heal = heal && behaviourManager.IsCurrentBehaviour(this.behaviourCode);
		behaviourManager.GetAnim.SetBool(healBool, heal);
	}

	public override void OnOverride()
	{

	}

	public override void LocalFixedUpdate()
	{
		AttackManagement(behaviourManager.GetH, behaviourManager.GetV);
	}
	void AttackManagement(float horizontal, float vertical)
	{
		if (behaviourManager.GetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
			!behaviourManager.GetAnim.IsInTransition(0))
		{
			heal = false;
			behaviourManager.GetAnim.SetBool(healBool, heal);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
		}
	}

	void ParticleEffects()
	{
		for (int i = 0; i < particles.Count; i++)
		{
			particles[i].SetActive(true);
		}
	}

}
using UnityEngine;
using System.Collections.Generic;
public class TauntBehaviour : GenericBehaviour
{
	public string tauntButton = "Taunt";
	private int tauntBool;
	private bool taunt = false;
	public List<GameObject> particles = new List<GameObject> ();

	void Start()
	{

		tauntBool = Animator.StringToHash("Taunt");

		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if (Input.GetButtonDown(tauntButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			taunt = !taunt;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			if (taunt)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);

				ParticleEffects ();
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		taunt = taunt && behaviourManager.IsCurrentBehaviour(this.behaviourCode);
		behaviourManager.GetAnim.SetBool(tauntBool, taunt);
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
			taunt = false;
			behaviourManager.GetAnim.SetBool(tauntBool, taunt);
			behaviourManager.UnregisterBehaviour(this.behaviourCode);
		}
	}

	void ParticleEffects()
    {
		for (int i = 0;  i < particles.Count; i++)
        {
			particles [ i ].SetActive( true );
        }
    }

}

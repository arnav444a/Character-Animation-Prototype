using UnityEngine;
using Unity.Collections;
using System.Collections;

public class AttackBehaviour : GenericBehaviour
{
	public string AttackButton = "Attack";           
	private int attackBool;                          
	private bool attack = false;
	private bool isAttacking = false;

	void Start()
	{
		
		attackBool = Animator.StringToHash("Attack");
		
		behaviourManager.SubscribeBehaviour(this);
	}

	void Update()
	{
		if (Input.GetButtonDown(AttackButton) && !behaviourManager.IsOverriding()
			&& !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
		{
			attack = !attack;

			behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

			if (attack)
			{
				behaviourManager.RegisterBehaviour(this.behaviourCode);

				StartCoroutine ( ParticleEffects () );
			}
			else
			{
				behaviourManager.GetCamScript.ResetTargetOffsets();

				behaviourManager.UnregisterBehaviour(this.behaviourCode);
			}
		}

		attack = attack && behaviourManager.IsCurrentBehaviour(this.behaviourCode);
		behaviourManager.GetAnim.SetBool(attackBool, attack);
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
			attack = false;
			behaviourManager.GetAnim.SetBool( attackBool, attack );
			behaviourManager.UnregisterBehaviour( this.behaviourCode );
        }
	}
	public GameObject particleAttack = null;
	public GameObject socket = null;
	public float createTime1 = 0.1f;
	public float createTime2 = 0.1f;
	public float createTime3 = 0.1f;

	public float attackTime1 = 0.8f;
	public float attackTime2 = 0.5f;
	public float attackTime3 = 1.0f;
	IEnumerator ParticleEffects()
    {
		if ( !isAttacking )
        {
			isAttacking = true;
			yield return new WaitForSeconds(createTime1);
			GameObject p1 = CreateParticleEffect();
			yield return new WaitForSeconds(attackTime1);
			MoveParticleEffect(p1);


			yield return new WaitForSeconds(createTime2);
			GameObject p2 = CreateParticleEffect();
			yield return new WaitForSeconds(attackTime2);
			MoveParticleEffect(p2);


			yield return new WaitForSeconds(createTime3);
			GameObject p3 = CreateParticleEffect();
			yield return new WaitForSeconds(attackTime3);
			MoveParticleEffect(p3);
			isAttacking = false;

		}
	}

	GameObject CreateParticleEffect()
    {
		GameObject obj = Instantiate(particleAttack);
		obj.transform.position = socket.transform.position;
		obj.transform.parent = socket.transform;
		return obj;
	}
	void MoveParticleEffect(GameObject obj)
    {
		obj.transform.parent = null;
		obj.transform.localRotation = this.transform.localRotation;
		obj.GetComponent<Mover> ().enabled = true;
    }
}

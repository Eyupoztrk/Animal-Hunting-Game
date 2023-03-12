using UnityEngine;
[RequireComponent(typeof(CapsuleCollider))]
public class Hit_Head : AS_BulletHiter
{
	public float DamageMult = 3;
	public DamageManager damageManage;

	void Start(){
		
		if (damageManage == null) {
			if (this.transform.root) {
				damageManage = this.transform.root.GetComponentInChildren<DamageManager> ();
			}
		} 
	}
	
	public override void OnHit (RaycastHit hit, AS_Bullet bullet)
	{
		float distance = Vector3.Distance (bullet.pointShoot, hit.point);
		if (damageManage) {
			int damage = (int)((float)bullet.Damage * DamageMult);
			damageManage.ApplyDamage (damage, bullet.transform.forward * bullet.HitForce,this.gameObject, distance, Suffix);
		}
		AddAudio (hit.point);
		base.OnHit (hit, bullet);
	}
}

using UnityEngine;
public class Hit_Normal : AS_BulletHiter
{
	public override void OnHit (RaycastHit hit, AS_Bullet bullet)
	{
		AddAudio (hit.point);
		base.OnHit (hit, bullet);
	}
}

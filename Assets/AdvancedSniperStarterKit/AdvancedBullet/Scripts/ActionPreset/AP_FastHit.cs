using UnityEngine;
using System.Collections;

public class AP_FastHit : AS_ActionPreset
{
	public float ZoomMulti = 1;
	public override void Shoot (GameObject bullet)
	{
		if (!ActionCam) {
			return;	
		}
		ActionCam.ObjectLookAt = null;
		base.Shoot (bullet);
	}
	
	public override void FirstDetected (AS_Bullet bullet, AS_BulletHiter target,Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		
		base.FirstDetected (bullet, target, point);
	}
	public override void TargetDetected (AS_Bullet bullet, AS_BulletHiter target,Vector3 point)
	{
		
		if (!ActionCam) {
			return;	
		}

		base.TargetDetected (bullet, target, point);
	}
	
	public override void TargetHited (AS_Bullet bullet, AS_BulletHiter target,Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}

		ActionCam.SlowmotionNow (0.1f, 1);
		base.TargetHited (bullet, target, point);
		
	}
}

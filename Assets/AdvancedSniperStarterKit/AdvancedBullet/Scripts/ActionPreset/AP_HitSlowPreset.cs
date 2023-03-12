using UnityEngine;
using System.Collections;

public class AP_HitSlowPreset : AS_ActionPreset
{
	public override void Shoot (GameObject bullet)
	{
		if (!ActionCam) {
			return;	
		}
		ActionCam.ObjectLookAt = null;
		base.Shoot (bullet);
	}

	public override void FirstDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		
		base.FirstDetected (bullet, target, point);
	}

	public override void TargetDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		
		if (!ActionCam) {
			return;	
		}
		base.TargetDetected (bullet, target, point);
	}

	public override void TargetHited (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}

		ActionCam.SetPositionDistance (point, true);
		ActionCam.lookAtPosition = point;
		ActionCam.ActionBullet (2.0f);
		ActionCam.SlowmotionNow (0.03f, 1);
		ActionCam.SetFOV (20, true, 0.1f);
		ActionCam.ObjectFollowing = null;
		ActionCam.Follow = true;
		ActionCam.SetPosition (point - bullet.transform.forward * 5, false);

		base.TargetHited (bullet, target, point);
	}
}

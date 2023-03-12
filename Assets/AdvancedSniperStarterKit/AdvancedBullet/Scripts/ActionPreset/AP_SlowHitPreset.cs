using UnityEngine;

public class AP_SlowHitPreset : AS_ActionPreset
{
	public override void Shoot (GameObject bullet)
	{
		if (!ActionCam) {
			return;	
		}
		ActionCam.ObjectLookAt = null;
		ActionCam.FOVTarget = 20;
		ActionCam.SetPosition (bullet.transform.position, false);
		base.Shoot (bullet);
	}

	public override void FirstDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		if (!ActionCam.InAction) {
			ActionCam.Slowmotion (0.5f, 1);
		}


		base.FirstDetected (bullet, target, point);
	}

	public override void TargetDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		ActionCam.lookAtPosition = point;
		ActionCam.Follow = false;
		ActionCam.SlowmotionNow (0.02f, 3);
		ActionCam.SetPositionDistance (point, false);
		ActionCam.ActionBullet (10.0f);
		ActionCam.SetFOV (10, true, 0.1f);
		base.TargetDetected (bullet, target, point);
	}

	public override void TargetHited (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		if (ActionCam.Detected) {
			
			ActionCam.ActionBullet (3.0f);

		} else {
			ActionCam.SetPositionDistance (point, true);
			ActionCam.ActionBullet (3.0f);
			ActionCam.SlowmotionNow (0.03f, 2);
			ActionCam.ObjectFollowing = null;
			ActionCam.Follow = false;
			//ActionCam.ObjectLookAt = null;
		}
		base.TargetHited (bullet, target, point);
	}
	
}

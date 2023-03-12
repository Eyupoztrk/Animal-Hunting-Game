using UnityEngine;
using System.Collections;

public class AP_BulletFollowPreset : AS_ActionPreset
{
	private bool detected = false;

	public override void Shoot (GameObject bullet)
	{
		if (!ActionCam) {
			return;	
		}
		detected = false;
		ActionCam.FOVTarget = 20;
		ActionCam.InAction = false;
		ActionCam.ObjectLookAt = bullet;
		ActionCam.SetPosition (bullet.transform.position, false);
		base.Shoot (bullet);
	}
	
	public override void FirstDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		if (!ActionCam) {
			return;	
		}
		
		if (!ActionCam.InAction) {
			ActionCam.ObjectLookAt = bullet.gameObject;
			ActionCam.Follow = true;
			ActionCam.ActionBullet (10.0f);
			ActionCam.SlowmotionNow (0.5f, 3.0f);
			ActionCam.LengthMult = 0.1f;
			ActionCam.SetPosition (bullet.transform.position + (bullet.transform.right) - (bullet.transform.forward), ActionCam.Detected);
			ActionCam.CameraOffset = ((bullet.transform.right) - (bullet.transform.forward)) * 0.2f;
			detected = true;

		}
		
		
		base.FirstDetected (bullet, target, point);
	}
	
	public override void TargetDetected (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		
		if (!ActionCam) {
			return;	
		}
		if (!ActionCam.HitTarget) {
			ActionCam.Follow = true;
			ActionCam.ActionBullet (10.0f);
			ActionCam.Slowmotion (0.015f, 10f);
			ActionCam.LengthMult = 0.1f;
			ActionCam.SetFOV (10, true, 0.1f);
			ActionCam.SetPosition (bullet.transform.position + (bullet.transform.right) - (bullet.transform.forward), ActionCam.Detected);
			ActionCam.CameraOffset = ((bullet.transform.right * 0.2f) - (bullet.transform.forward));
		}

		base.TargetDetected (bullet, target, point);
	}
	
	public override void TargetHited (AS_Bullet bullet, AS_BulletHiter target, Vector3 point)
	{
		
		if (!ActionCam) {
			return;	
		}
		if (ActionCam.ObjectLookAt == bullet.gameObject)
			ActionCam.ObjectLookAt = null;
		
		if(detected){
			ActionCam.ActionBullet (2.0f);
			//ActionCam.ObjectLookAt = null;
			ActionCam.SlowmotionNow (0.1f, 1.6f);
			ActionCam.Follow = false;
			ActionCam.lookAtPosition = point;
			ActionCam.SetPosition (bullet.transform.position + (bullet.transform.right), false);
			ActionCam.CameraOffset = Vector3.zero;
			ActionCam.ResetFOV();
			
		}else{
			// in case of missed prediction of hit point.
			// action camera will try to get new position as close as hit point
			ActionCam.ResetFOV();
			ActionCam.FOVTarget = 20;
			ActionCam.ActionBullet (2.0f);
			//ActionCam.ObjectLookAt = null;
			ActionCam.SlowmotionNow (0.1f, 1.6f);
			ActionCam.Follow = false;
			ActionCam.SetPosition (point - bullet.transform.forward * 5, false);
			ActionCam.lookAtPosition = point;
		}
		
		base.TargetHited (bullet, target, point);
		
	}
}

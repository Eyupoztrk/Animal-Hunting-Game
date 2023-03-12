using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]


public class GunMecanim : Gun
{
	public Animator animator;

	void Start ()
	{
		if (animator == null)
			animator = this.GetComponent<Animator> ();
		
	}


	public override void UpdateGun ()
	{
		if (HideGunWhileZooming && FPSmotor && NormalCamera.enabled) {
			FPSmotor.HideGun (!Zooming);
		}
		
		if (animator == null || !Active)
			return;

		AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo (0);
		
		switch (gunState) {
		case 0:
			// Start Bolt
			if (AmmoIn <= 0) {
				// Check Ammo in clip
				if (Clip > 0) {
					animator.SetTrigger ("Bolt");
					gunState = 2;
					// scope rotation a bit when reloading
					if (FPSmotor && Zooming) {
						FPSmotor.CameraForceRotation (new Vector3 (0, 0, 20));
						FPSmotor.Stun (0.2f);
					}
					if (SoundBoltStart && audiosource != null) {
						audiosource.PlayOneShot (SoundBoltStart);
					}
					Clip -= 1;
				} else {
					gunState = 3;	
				}
			}
			break;
		case 1:
			// Countdown to idle state
			if (Time.time >= cooldowntime + CooldownTime) {
				gunState = 0;
			}
			break;
		case 2:
			
			// finish bold animation
			if (animationState.IsName (BoltPose)) {
				if (animationState.normalizedTime > BoltTime) {
					if (Shell && ShellSpawn) {
						if (!boltout) {
							GameObject shell = (GameObject)Instantiate (Shell, ShellSpawn.position, ShellSpawn.rotation);
							Rigidbody shelrigidbody = shell.GetComponent<Rigidbody> ();
							shelrigidbody.AddForce (ShellSpawn.transform.right * 2);
							shelrigidbody.AddTorque (Random.rotation.eulerAngles * 10);
							GameObject.Destroy (shell, 5);
							boltout = true;
							if (FPSmotor && Zooming) {
								FPSmotor.CameraForceRotation (new Vector3 (0, 0, -5));
								FPSmotor.Stun (0.1f);
							}		
						}
					}	
				}
				if (animationState.normalizedTime > 0.9f) {
					gunState = 0;
					AmmoIn = 1;
					if (SoundBoltEnd && audiosource != null) {
						audiosource.PlayOneShot (SoundBoltEnd);
					}
				}
			}
			break;
		case 3:
			// Start Reloading
			if ((AmmoPack > 0 || InfinityAmmo)) {
				animator.SetTrigger ("Reload");
				gunState = 4;
				Zooming = false;
				if (SoundReloadStart && audiosource != null) {
					audiosource.PlayOneShot (SoundReloadStart);
				}
			} else {
				gunState = 0;
			}
			break;
		case 4:
			
			animator.SetTrigger ("Reload");

			if (animationState.IsName (ReloadPose)) {
				if (animationState.normalizedTime > 0.8f) {
					gunState = 0;
					if (InfinityAmmo) {
						Clip = ClipSize;
					} else {
						if (AmmoPack >= ClipSize) {
							Clip = ClipSize;
							AmmoPack -= ClipSize;
						} else {
							Clip = AmmoPack;
							AmmoPack = 0;
						}
					}
					
					if (Clip > 0) {
						animator.ResetTrigger ("Reload");
						if (SoundReloadEnd && audiosource != null) {
							audiosource.PlayOneShot (SoundReloadEnd);
						}
					}
				}
			} 
			break;
		}
	
		if (FPSmotor) {
			if (Zooming) {
				FPSmotor.sensitivityXMult = MouseSensitiveZoom;
				FPSmotor.sensitivityYMult = MouseSensitiveZoom;
				FPSmotor.Noise = true;
			} else {
				FPSmotor.sensitivityXMult = MouseSensitive;
				FPSmotor.sensitivityYMult = MouseSensitive;
				FPSmotor.Noise = false;
			}
		}
	
		if (Zooming) {
			if (ZoomFOVLists.Length > 0) {
				MouseSensitiveZoom = ((MouseSensitive * 0.16f) / 10) * ZoomFOVLists [IndexZoom];
				NormalCamera.fieldOfView += (ZoomFOVLists [IndexZoom] - NormalCamera.GetComponent<Camera> ().fieldOfView) / 10;
			}
		} else {
			NormalCamera.fieldOfView += (fovTemp - NormalCamera.fieldOfView) / 10;
		}

		if (audiosource != null) {
			audiosource.pitch = Time.timeScale;
			if (audiosource.pitch < 0.5f) {
				audiosource.pitch = 0.5f;
			}
		}
	}


	public override void ShootAnimation ()
	{
		if (animator)
			animator.SetTrigger ("Shoot");
		
	}


	

}

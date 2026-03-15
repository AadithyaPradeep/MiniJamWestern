using NUnit.Framework;
using UnityEngine;

public class GameplayAudio : MonoBehaviour
{
[Header("SFX")]
[SerializeField] private AudioClip[]shootWeapon01;
[SerializeField] private AudioClip[]shootWeaponAK;
[SerializeField] private AudioClip[]axeSwing;
[SerializeField] private AudioClip[]footstepsRobot;
[SerializeField] private AudioClip[]footstepsHumans;

private int lastShootIndex = -1;
private int lastShootAKIndex = -1;
private int lastAxeSwingIndex = -1;
private int lastFootstepRobotIndex = -1;
private int lastFootstepHumansIndex = -1;



public void PlayShootWeapon01()
    {
        if (shootWeapon01 == null || shootWeapon01.Length == 0) return;
        if (AudioManager.master == null) return;

        int randomIndex;
        if (shootWeapon01.Length > 1)
        {
            do
            {
                randomIndex = Random.Range(0,shootWeapon01.Length);
            }
            while (randomIndex == lastShootIndex);
            lastShootIndex = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }
        
        AudioManager.master.SFX.Play2DOneShot(shootWeapon01[randomIndex]);
    }

public void PlayShootWeaponAK(Vector3 position)
    {
        if (shootWeaponAK == null || shootWeaponAK.Length == 0) return;
        if (AudioManager.master == null) return;

        int randomIndex;
        if (shootWeaponAK.Length > 1)
        {
            do
            {
                randomIndex = Random.Range(0,shootWeaponAK.Length);
            }
            while (randomIndex == lastShootAKIndex);
            lastShootAKIndex = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }

        AudioManager.master.SFX.Play3DOneShot(shootWeaponAK[randomIndex], position);
    }

public void PlayWeaponAxe(Vector3 position)
    {
        if (axeSwing == null || axeSwing.Length == 0) return;
        if (AudioManager.master == null) return;

        int randomIndex;
        if (axeSwing.Length > 1)
        {
            do
            {
                randomIndex = Random.Range(0,axeSwing.Length);
            }
            while (randomIndex == lastAxeSwingIndex);
            lastAxeSwingIndex = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }

        AudioManager.master.SFX.Play3DOneShot(axeSwing[randomIndex], position);
    }
    
public void PlayFootstepsRobot()
    {
        if (footstepsRobot == null || footstepsRobot.Length == 0) return;
        if (AudioManager.master == null) return;

        int randomIndex;
        if (footstepsRobot.Length > 1)
        {
            do
            {
                randomIndex = Random.Range(0,footstepsRobot.Length);
            }
            while (randomIndex == lastFootstepRobotIndex);
            lastFootstepRobotIndex = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }

        AudioManager.master.SFX.Play2DOneShot(footstepsRobot[randomIndex]);
    }

public void PlayFootstepsHumans(Vector3 position)
    {
        if (footstepsHumans == null || footstepsHumans.Length == 0) return;
        if (AudioManager.master == null) return;

        int randomIndex;
        if (footstepsHumans.Length > 1)
        {
            do
            {
                randomIndex = Random.Range(0,footstepsHumans.Length);
            }
            while (randomIndex == lastFootstepHumansIndex);
            lastFootstepHumansIndex = randomIndex;
        }
        else
        {
            randomIndex = 0;
        }

        AudioManager.master.SFX.Play3DOneShot(footstepsHumans[randomIndex], position);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData",menuName = "ScriptableObjects/WeaponData",order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int bulletDamage;
    public float rateOfFire;
    public int bulletPerShot;
    public float reloadTime;
    public int nbOfBulletsInMagazine;
    public int nbOfBulletsInTotal;
    public int bulletSpeed;
    public int hitForce;
    public float aimingSpeed;
    public float aimSensitivity;
    public float aimMovementSpeed;
    public int fieldOfViewAim;

}

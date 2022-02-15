using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData",menuName = "ScriptableObjects/WeaponData",order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int bulletDamage;
    public int rateOfFire;
    public int bulletPerShot;
    public int reloadTime;
    public int nbOfBulletsInMagazine;
    public int nbOfBulletsInTotal;
    public int bulletSpeed;
    public float aimingSpeed;
    public float aimSensitivity;
    public float aimMovementSpeed;
}

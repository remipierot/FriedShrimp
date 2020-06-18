using UnityEngine;

[CreateAssetMenu(fileName = "ShootProfile", menuName = "Shooting Profile", order = 1)]
public class ShootProfile : ScriptableObject
{
	public float Speed;
	public float Damage;
	public float FireRate;
	public float Interval;
	public float DestroyRate;
	public float Spread;

	public int Amount;
}

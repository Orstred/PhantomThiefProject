using UnityEngine;

public interface IDamage 
{
    public void TakeDamage(int Damage);
    public void DealDamage(GameObject Character,int Damage);
}

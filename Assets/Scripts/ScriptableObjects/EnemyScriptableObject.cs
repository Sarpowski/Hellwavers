using UnityEngine;

[CreateAssetMenu(fileName = "enemyType", menuName = "Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
   public float _speed;
   public float _health;
   public string enemyName;
   public AnimatorOverrideController animatorOverride;
}

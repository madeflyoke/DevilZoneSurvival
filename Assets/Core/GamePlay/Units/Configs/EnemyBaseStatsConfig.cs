using UnityEngine;

namespace Core.GamePlay.Units.Configs
{
    [CreateAssetMenu(fileName = "DefaultEnemyConfig", menuName = "Configs/Enemies/new EnemyStatsConfig")]
    public class EnemyBaseStatsConfig : ScriptableObject
    {
        public float health;
        public float speed;
        public float attackRange;
        public float damage;
    }
}

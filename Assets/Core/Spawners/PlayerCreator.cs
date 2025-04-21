using Core.Units.UnitBrains;
using Core.WeaponSystem.Enums;
using EasyButtons;
using UnityEngine;

namespace Core.Spawners
{
    public class PlayerCreator : MonoBehaviour
    {
        public PlayerBrain PlayerBrain { get; private set; }
        
        [SerializeField] private PlayerBrain _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;

        public void Create()
        {
            PlayerBrain = Instantiate(_playerPrefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation);
            PlayerBrain.Initialize();
        }
        
        [Button]
        public void Get()
        {
            GameplaySceneContext.Instance.WeaponProvider.GetWeapon(PlayerBrain, WeaponType.EXTENDABLE_SPEAR);
        }
    }
}

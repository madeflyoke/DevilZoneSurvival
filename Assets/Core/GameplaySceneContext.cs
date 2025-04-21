using Core.GameField;
using Core.Loot;
using Core.Spawners;
using Core.Utils;
using Core.WeaponSystem;
using UnityEngine;

namespace Core
{
    public class GameplaySceneContext : MonoSingleton<GameplaySceneContext>
    {
        [field: SerializeField] public CameraProvider CameraProvider { get; private set; }
        [field: SerializeField] public Field Field { get; private set; }
        [field: SerializeField] public LootController LootController { get; private set; }
        [field: SerializeField] public PlayerCreator PlayerCreator { get; private set; }
        [field: SerializeField] public WeaponProvider WeaponProvider { get; private set; }
    }
}

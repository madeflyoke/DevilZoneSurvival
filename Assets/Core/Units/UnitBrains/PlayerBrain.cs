using Core.Units.Components;

namespace Core.Units.UnitBrains
{
    public class PlayerBrain : UnitBrain
    {
        public override void Initialize()
        {
            base.Initialize();
            GameplaySceneContext.Instance.CameraProvider.SetObjectToFollow(UnitContext.Brain.GetUnitComponent<ViewHolderComponent>().Data.UnitT);
        }
    }
}
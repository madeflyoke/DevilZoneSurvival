namespace Core.Units.UnitBrains
{
    public class PlayerBrain : UnitBrain
    {
        private void Start()
        {
            MonoContext.Instance.CameraProvider.SetObjectToFollow(UnitContext.UnitT);
        }
    }
}
using Core.WeaponSystem;

public class MonoContext : MonoSingleton<MonoContext>
{
    public CameraProvider CameraProvider;
    public Field Field;
    public WeaponGiver WeaponGiver;
}

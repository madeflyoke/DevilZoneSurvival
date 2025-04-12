namespace Core.Services.Pause.Interfaces
{
    public interface IPausable
    {
        public bool IsPaused { get;}
        public void SetPause(bool value);
    }
}

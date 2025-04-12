using System.Collections.Generic;
using Core.Services.Pause.Interfaces;

namespace Core.Services.Pause
{
    public class PauseService
    {
        private List<IPausable> _pausables = new List<IPausable>();
        private bool _isPaused;
        
        public void SetPause(bool value)
        {
            _pausables.ForEach(x => x.SetPause(value));
            _isPaused = value;
        }
        
        public void Register(IPausable pausable)
        {
            if (_pausables.Contains(pausable)==false)
            {
                _pausables.Add(pausable);   
            }
        }

        public void Unregister(IPausable pausable)
        {
            if (_pausables.Contains(pausable))
            {
                _pausables.Remove(pausable);
            }
        }
    }
}

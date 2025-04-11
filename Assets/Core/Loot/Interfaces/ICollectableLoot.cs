using UnityEngine;

namespace Core.Loot.Interfaces
{
    public interface ICollectableLoot
    {
        public Transform SelfTransform { get; }
        public void CallOnCollected(IActionReceiversOwner actionReceiversOwner);
        public void CallOnStartCollecting();
    }
}

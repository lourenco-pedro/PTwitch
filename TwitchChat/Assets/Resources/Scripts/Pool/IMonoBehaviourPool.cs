using UnityEngine;

namespace PoolSystem
{
    public interface IMonoBehaviourPool
    {
        MonoBehaviour MonoBehaviourReference { get; }
        GameObject GameObjectReference { get; }
    }
}
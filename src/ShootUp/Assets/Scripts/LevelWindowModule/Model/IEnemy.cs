using UnityEngine;

namespace LevelWindowModule
{
    public interface IEnemy
    {
        Collider2D Collider { get; }
    }
}
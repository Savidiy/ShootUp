using System;
using Savidiy.Utils;
using UnityEngine;

namespace LevelWindowModule
{
    [Serializable]
    public class EnemiesPrefabs : SerializedDictionary<EEnemyType, EnemyHierarchy>
    {
    }

    [CreateAssetMenu(fileName = "EnemyPrefabProvider", menuName = "EnemyPrefabProvider", order = 0)]
    public class EnemyPrefabProvider : AutoSaveScriptableObject
    {
        [SerializeField] private EnemiesPrefabs _prefabs;

        public EnemyHierarchy GetPrefab(EEnemyType enemyType)
        {
            if (_prefabs.TryGetValue(enemyType, out EnemyHierarchy hierarchy))
                return hierarchy;

            throw new Exception($"Can't find prefab for '{enemyType}' enemy");
        }
    }
}
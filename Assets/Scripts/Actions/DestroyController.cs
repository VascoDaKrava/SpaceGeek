using System.Collections.Generic;
using UnityEngine;

namespace GeekSpace
{
    internal sealed class DestroyController
    {
        private GameObject _playerObject;

        internal DestroyController()
        {
            var playerProvider = GameObject.FindObjectOfType<PlayerProvider>();
            var playerModel = playerProvider.PlayerModel;
            _playerObject = playerModel.Object;

            GameEventSystem.current.onDestroyNearby += DestroyNearby;
        }

        internal GameObject GetNearby()
        {
            var enemyObjects = new List<GameObject>();
            var delta = Mathf.Infinity;
            var playerPosition = _playerObject.transform.position;
            GameObject closet = null;

            var enemyProviders = GameObject.FindObjectsOfType<EnemyProvider>();
            for (int i = 0; i < enemyProviders.Length - 1; i++)
            {
                enemyObjects.Add(enemyProviders[i].EnemyModel.Object);
            }

            foreach (GameObject go in enemyObjects)
            {
                var deltaVector = go.transform.position - playerPosition;
                var currentDelta = deltaVector.sqrMagnitude;
                if (currentDelta < delta)
                {
                    closet = go;
                    delta = currentDelta;
                }
            }
            return closet;
        }

        internal void DestroyNearby()
        {
            var nearbyEnemy = GetNearby();
            var enemyProvider = nearbyEnemy.GetComponent<EnemyProvider>();
            var enemyPool = enemyProvider.EnemyModel.Pool;
            enemyPool.Push(nearbyEnemy);
        }

        ~DestroyController()
        {
            GameEventSystem.current.onDestroyNearby -= DestroyNearby;
        }
    }
}

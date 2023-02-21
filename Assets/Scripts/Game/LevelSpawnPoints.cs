using UnityEngine;

namespace JoyWay.Game
{
    public class LevelSpawnPoints : MonoBehaviour
    {
        [SerializeField]
        private Transform[] spawnPoints;

        public Transform[] GetSpawnPoints()
        {
            return spawnPoints;
        }

        public Transform GetRandomSpawnPoint()
        {
            int index = Random.Range(0, spawnPoints.Length);

            return spawnPoints[index];
        }
    }
}
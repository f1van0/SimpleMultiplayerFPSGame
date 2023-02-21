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
    }
}
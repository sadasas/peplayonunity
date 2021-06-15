using UnityEngine;
using Mirror;

namespace Peplayon
{
    internal class SpawnItem

    {
        internal static void Spawnitemrandom()
        {
            if (!NetworkServer.active) return;

            for (int a = 0; a < ((NetworkManagerTesting)NetworkManager.singleton).itemPrefab.Length; a++)
            {
                int b = Random.Range(0, ((NetworkManagerTesting)NetworkManager.singleton).itemPrefab.Length);
                NetworkServer.Spawn(Object.Instantiate(((NetworkManagerTesting)NetworkManager.singleton).itemPrefab[b].gameObject, ((NetworkManagerTesting)NetworkManager.singleton).spawnPointItem[a].position, Quaternion.identity));
            }
        }
    }
}
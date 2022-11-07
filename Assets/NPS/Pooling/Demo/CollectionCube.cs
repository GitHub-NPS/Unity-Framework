using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS.Pooling
{
    public class CollectionCube : MonoBehaviour
    {
        [SerializeField] private MyCube prefab;
        [SerializeField] private Vector2 spawn = new Vector2(0.4f, 1.8f);

        private void Start()
        {
            StartCoroutine(Create());
        }

        private IEnumerator Create()
        {
            var element = Manager.S.Spawn(prefab, this.transform);
            element.transform.position = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-2.5f, 2.5f), 1);

            yield return new WaitForSeconds(Random.Range(spawn.x, spawn.y));
            StartCoroutine(Create());
        }
    }
}
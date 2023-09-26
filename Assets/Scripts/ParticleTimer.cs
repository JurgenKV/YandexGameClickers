
    using System;
    using UnityEngine;

    public class ParticleTimer : MonoBehaviour
    {
        [SerializeField] private float timer = 1.5f;
        private void Awake()
        {
            Invoke(nameof(Delete), timer);
        }

        private void Delete()
        {
            Destroy(gameObject);
        }
    }

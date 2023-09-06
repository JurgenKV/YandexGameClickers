
    using System;
    using UnityEngine;

    public class ParticleTimer : MonoBehaviour
    {
        [SerializeField] private float timer = 3;
        private void Awake()
        {
            Invoke(nameof(Delete), timer);
        }

        private void Delete()
        {
            Destroy(this);
        }
    }

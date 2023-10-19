
    using System;
    using UnityEngine;

    public class DeleteTimeGO : MonoBehaviour
    {
        [SerializeField] private float time = 3;
        private void Start()
        {
            //Invoke(nameof(DeleteObject), time);
            Destroy(gameObject, time);
        }

        // private void DeleteObject()
        // {
        //     Destroy(gameObject, time);
        // }
    }

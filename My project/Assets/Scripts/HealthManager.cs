using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern double _requestAuthorization();

        [DllImport("__Internal")]
        private static extern double _getCurrentMoveRingValue();

        [DllImport("__Internal")]
        private static extern double _getCurrentExerciseRingValue();

        public double moveRing;
        public double exerciseRing;

        // Start is called before the first frame update
        void Start()
        {
            _requestAuthorization();
            moveRing = _getCurrentMoveRingValue();
            exerciseRing = _getCurrentExerciseRingValue();
        }

        // Update is called once per frame
        void Update()
        {
            moveRing = _getCurrentMoveRingValue();
            exerciseRing = _getCurrentExerciseRingValue();
        }
    }
}


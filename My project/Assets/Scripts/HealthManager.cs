using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void _requestAuthorization();

        [DllImport("__Internal")]
        private static extern double _getCurrentMoveRingValue();

        [DllImport("__Internal")]
        private static extern double _getCurrentExerciseRingValue();

        [DllImport("__Internal")]
        private static extern double _getMoveRingGoal();

        public double moveRing;
        public double exerciseRing;
        public double moveGoal;

        // Start is called before the first frame update
        void Start()
        {
            if (!Application.isEditor)
            {
                _requestAuthorization();
                moveRing = _getCurrentMoveRingValue();
                exerciseRing = _getCurrentExerciseRingValue();
                moveGoal = _getMoveRingGoal();
            }
        }

        // // Update is called once per frame
        // void Update()
        // {
        //     moveRing = _getCurrentMoveRingValue();
        //     exerciseRing = _getCurrentExerciseRingValue();
        //     moveGoal = _getMoveRingGoal();
        // }

        public double GetMoveRing()
        {
            moveRing = _getCurrentMoveRingValue();
            return moveRing;
        }

        public double GetMoveGoal()
        {
            moveGoal = _getMoveRingGoal();
            return moveGoal;
        }

        public double GetExerciseRing()
        {
            exerciseRing = _getCurrentExerciseRingValue();
            return exerciseRing;
        }
    }
}


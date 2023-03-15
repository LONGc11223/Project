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

        [DllImport("__Internal")]
        private static extern void _getMoveRingForDay(int day, int month, int year, MoveRingCallbackDelegate callback);

        [DllImport("__Internal")]
        private static extern void _getExerciseRingForDay(int day, int month, int year, ExerciseRingCallbackDelegate callback);

        public delegate void MoveRingCallbackDelegate(double value);
        public delegate void ExerciseRingCallbackDelegate(double value);

        [System.Serializable]
        public struct RingValues {
            public double moveRingValue;
            public double exerciseRingValue;
        }

        // [DllImport("__Internal")]
        // private static extern RingValues _getRingDataForDate(int day, int month, int year);        

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

        // public RingValues GetRingData(int day, int month, int year, MoveRingCallbackDelegate moveCallback, ExerciseRingCallbackDelegate exerciseCallback)
        // {
        //     RingValues ring = new RingValues();
        //     ring.moveRingValue = _getMoveRingForDay(day, month, year, moveCallback);
        //     ring.exerciseRingValue = _getExerciseRingForDay(day, month, exerciseCallback);
        //     return ring;
        // }

        public IEnumerator GetRingData(int day, int month, int year, MoveRingCallbackDelegate moveCallback, ExerciseRingCallbackDelegate exerciseCallback)
        {
            bool moveDone = false;
            bool exerciseDone = false;

            _getMoveRingForDay(day, month, year,  (double value) => {
                moveDone = true;
                moveCallback(value);
            });

            _getExerciseRingForDay(day, month, year, (double value) => {
                exerciseDone = true;
                exerciseCallback(value);
            });

            while (!moveDone || !exerciseDone)
            {
                yield return null;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
        private static extern double _getCurrentMoveRingGoal();

        [DllImport("__Internal")]
        private static extern double _getMoveRingValue(int day, int month, int year);

        [DllImport("__Internal")]
        private static extern double _getMoveRingGoal(int day, int month, int year);

        [DllImport("__Internal")]
        private static extern double _getExerciseRingValue(int day, int month, int year);

        // public delegate void MoveRingCallbackDelegate(double value);
        // public delegate void ExerciseRingCallbackDelegate(double value);

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
                moveGoal = _getCurrentMoveRingGoal();
            }
        }

        void Update()
        {
            if (!Application.isEditor)
            {
                moveRing = _getCurrentMoveRingValue();
                exerciseRing = _getCurrentExerciseRingValue();
                moveGoal = _getCurrentMoveRingGoal();
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
            moveGoal = _getCurrentMoveRingGoal();
            return moveGoal;
        }

        public double GetExerciseRing()
        {
            exerciseRing = _getCurrentExerciseRingValue();
            return exerciseRing;
        }

        // Get data for specific dates:

        public double GetMoveRingDate(int day, int month, int year)
        {
            return _getMoveRingValue(day, month, year);
        }

        public double GetMoveGoalDate(int day, int month, int year)
        {
            // return 500.0;
            return _getMoveRingGoal(day, month, year);
        }

        public double GetExerciseRingDate(int day, int month, int year)
        {
            return _getExerciseRingValue(day, month, year);
        }

        public async Task<double> GetMoveRingValueAsync(int day, int month, int year)
        {
            double result = await Task.Run(() => _getMoveRingValue(day, month, year));
            return result;
        }

        public async Task<double> GetExerciseRingValueAsync(int day, int month, int year)
        {
            double result = await Task.Run(() => _getExerciseRingValue(day, month, year));
            return result;
        }

        public async Task<double> GetMoveRingGoalAsync(int day, int month, int year)
        {
            double result = await Task.Run(() => _getMoveRingGoal(day, month, year));
            return result;
        }

        // async Task<(double, double, double)> GetRings(int day, int month, int year)
        // {
        //     double moveRing = await GetMoveRingValueAsync(day, month, year);
        //     double exerciseRing = await GetExerciseRingValueAsync(day, month, year);
        //     double moveGoal = await GetMoveRingGoalAsync(day, month, year);

        //     return (moveRing, exerciseRing, moveGoal);
        // }

        // IEnumerator UpdateRingDataCoroutine(int day, int month, int year)
        // {
        //     (double, double, double) result = GetRings(day, month, year);
        //     isUpdateRingRunning = false;
        //     yield return null;
        // }


        // public RingValues GetRingData(int day, int month, int year, MoveRingCallbackDelegate moveCallback, ExerciseRingCallbackDelegate exerciseCallback)
        // {
        //     RingValues ring = new RingValues();
        //     ring.moveRingValue = _getMoveRingForDay(day, month, year, moveCallback);
        //     ring.exerciseRingValue = _getExerciseRingForDay(day, month, exerciseCallback);
        //     return ring;
        // }

        // public IEnumerator GetRingData(int day, int month, int year, MoveRingCallbackDelegate moveCallback, ExerciseRingCallbackDelegate exerciseCallback)
        // {
        //     bool moveDone = false;
        //     bool exerciseDone = false;

        //     _getMoveRingForDay(day, month, year,  (double value) => {
        //         moveDone = true;
        //         moveCallback(value);
        //     });

        //     _getExerciseRingForDay(day, month, year, (double value) => {
        //         exerciseDone = true;
        //         exerciseCallback(value);
        //     });

        //     while (!moveDone || !exerciseDone)
        //     {
        //         yield return null;
        //     }
        // }
    }
}


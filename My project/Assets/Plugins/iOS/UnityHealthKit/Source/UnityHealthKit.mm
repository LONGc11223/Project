//
//  HUnityHealthKitBridge.m
//  UnityHealthKit
//
//  Created by Andrew Park on 2/20/23.
//

#import <Foundation/Foundation.h>
#include "UnityFramework/UnityFramework-Swift.h"

extern "C" {
    
#pragma mark - Functions
    
    void _requestAuthorization() {
        [[UnityHealthKit shared] requestAuthorization];
    }

    double _getCurrentMoveRingValue() {
        double result = [[UnityHealthKit shared] getCurrentMoveRingValue];
        return result;
    }

    double _getCurrentExerciseRingValue() {
        double result = [[UnityHealthKit shared] getCurrentExerciseRingValue];
        return result;
    }

    double _getCurrentMoveRingGoal() {
        double result = [[UnityHealthKit shared] getCurrentMoveRingGoal];
        return result;
    }

    double _getMoveRingValue(int day, int month, int year) {
        double result = [[UnityHealthKit shared] getMoveRingValueWithDay:day month:month year:year];
        return result;
    }

    double _getMoveRingGoal(int day, int month, int year) {
        double result = [[UnityHealthKit shared] getMoveRingGoalWithDay:day month:month year:year];
        return result;
    }
    
    double _getExerciseRingValue(int day, int month, int year) {
        double result = [[UnityHealthKit shared] getExerciseRingValueWithDay:day month:month year:year];
        return result;
    }
}

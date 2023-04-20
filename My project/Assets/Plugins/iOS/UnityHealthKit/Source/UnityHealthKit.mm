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
        __block double result = 0.0;
        dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);

        [[UnityHealthKit shared] getMoveRingValueWithDay:day month:month year:year completion:^(double moveRingValue) {
            result = moveRingValue;
            dispatch_semaphore_signal(semaphore);
        }];

        // Wait for the completion handler to be called
        dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);

        return result;
    }

    double _getMoveRingGoal(int day, int month, int year) {
        __block double result = 0.0;
        dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);

        [[UnityHealthKit shared] getMoveRingGoalWithDay:day month:month year:year completion:^(double moveRingGoal) {
            result = moveRingGoal;
            dispatch_semaphore_signal(semaphore);
        }];

        // Wait for the completion handler to be called
        dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);

        return result;
    }
    
    double _getExerciseRingValue(int day, int month, int year) {
        __block double result = 0.0;
        dispatch_semaphore_t semaphore = dispatch_semaphore_create(0);

        [[UnityHealthKit shared] getExerciseRingValueWithDay:day month:month year:year completion:^(double exerciseRingValue) {
            result = exerciseRingValue;
            dispatch_semaphore_signal(semaphore);
        }];

        // Wait for the completion handler to be called
        dispatch_semaphore_wait(semaphore, DISPATCH_TIME_FOREVER);

        return result;
    }
}

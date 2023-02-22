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

    double _getMoveRingGoal() {
        double result = [[UnityHealthKit shared] getMoveRingGoal];
        return result;
    }

}
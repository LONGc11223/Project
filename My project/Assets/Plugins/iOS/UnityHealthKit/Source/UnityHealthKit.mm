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

    double _getMoveRingValue(int day, int month, int year) {
        double result = [[UnityHealthKit shared] getMoveRingValueWithDay:day month:month year:year];
        return result;
    }
    double _getExerciseRingValue(int day, int month, int year) {
        double result = [[UnityHealthKit shared] getExerciseRingValueWithDay:day month:month year:year];
        return result;
    }

//    void _getMoveRingForDay(int day, int month, int year, void(*callback)(double)) {
//        [[UnityHealthKit shared] getMoveRingForDayWithDay:day month:month year:year completion:^(double value) {
//            callback(value);
//        }];
//    }
//
//    void _getExerciseRingForDay(int day, int month, int year, void(*callback)(double)) {
//        [[UnityHealthKit shared] getExerciseRingForDayWithDay:day month:month year:year completion:^(double value) {
//            callback(value);
//        }];
//    }

    // typedef struct {
    //     double moveRingValue;
    //     double exerciseRingValue;
    //     double standHoursValue;
    // } RingValues;

    // RingValues _getRingDataForDate(int day, int month, int year) {
    //     NSDateComponents *components = [[NSDateComponents alloc] init];
    //     components.year = year;
    //     components.month = month;
    //     components.day = day;
    //     NSDate *date = [[NSCalendar currentCalendar] dateFromComponents:components];
    //     NSArray<NSNumber *> *ringData = [[UnityHealthKit shared] getRingDataForDateWithDay:day month:month year:year];
    //     double moveRingValue = [ringData[0] doubleValue];
    //     double exerciseRingValue = [ringData[1] doubleValue];
    //     double standRingValue = [ringData[2] doubleValue];
    //     RingValues ringValues = {moveRingValue, exerciseRingValue, standRingValue};
    //     return ringValues;
    // }

}

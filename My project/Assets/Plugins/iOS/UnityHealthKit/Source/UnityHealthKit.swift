//
//  HealthPlugin.swift
//  UnityHealthKit
//
//  Created by Andrew Park on 2/20/23.
//

import Foundation
import HealthKit
//import <UnityFramework/UnityFramework-Swift.h>


@objc public class UnityHealthKit: NSObject {
    @objc public static let shared = UnityHealthKit()
    
    private let healthStore = HKHealthStore()
    private let activeEnergyBurnedType = HKQuantityType.quantityType(forIdentifier: HKQuantityTypeIdentifier.activeEnergyBurned)!
    private let exerciseTimeType = HKQuantityType.quantityType(forIdentifier: HKQuantityTypeIdentifier.appleExerciseTime)!
    
    private var moveRingValue: Double = 0.0
    private var exerciseRingValue: Double = 0.0
    private var moveGoalValue: Double = 0.0
    
    @objc public func requestAuthorization() {
        let typesToRead = Set([activeEnergyBurnedType, exerciseTimeType])
        let typesToShare = Set<HKSampleType>()
        healthStore.requestAuthorization(toShare: typesToShare, read: typesToRead) { success, error in
            if !success {
                print("Authorization failed")
            }
        }
    }
    
    @objc public func getCurrentMoveRingValue() -> Double {
        guard let sampleType = HKSampleType.quantityType(forIdentifier: .activeEnergyBurned) else { return 0 }
        let startDate = Calendar.current.startOfDay(for: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictEndDate)
        let query = HKStatisticsQuery(quantityType: sampleType, quantitySamplePredicate: predicate, options: .cumulativeSum) { _, result, _ in
            if let sum = result?.sumQuantity() {
                self.moveRingValue = sum.doubleValue(for: HKUnit.jouleUnit(with: .kilo)) * 0.239
//                print("Move ring value: \(self.moveRingValue)")
            }
        }
        healthStore.execute(query)
        return moveRingValue
    }
    
    @objc public func getCurrentExerciseRingValue() -> Double {
        guard let sampleType = HKSampleType.quantityType(forIdentifier: .appleExerciseTime) else { return 0 }
        let startDate = Calendar.current.startOfDay(for: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictEndDate)
        let query = HKStatisticsQuery(quantityType: sampleType, quantitySamplePredicate: predicate, options: .cumulativeSum) { _, result, _ in
            if let sum = result?.sumQuantity() {
                self.exerciseRingValue = sum.doubleValue(for: HKUnit.minute())
//                print("Exercise ring value: \(self.exerciseRingValue)")
            }
        }
        healthStore.execute(query)
        return exerciseRingValue
    }

    @objc public func getMoveRingGoal() -> Double {
        func createPredicate() -> NSPredicate? {
            let calendar = Calendar.autoupdatingCurrent

            var dateComponents = calendar.dateComponents([.year, .month, .day],
                                                         from: Date())
            dateComponents.calendar = calendar

            let predicate = HKQuery.predicateForActivitySummary(with: dateComponents)
            return predicate
        }

        let queryPredicate = createPredicate()
        let query = HKActivitySummaryQuery(predicate: queryPredicate) { (query, summaries, error) -> Void in
            if let summaries = summaries {
                for summary in summaries {
                    let activeEnergyBurned = summary.activeEnergyBurned.doubleValue(for: HKUnit.kilocalorie())
                    let activeEnergyBurnedGoal = summary.activeEnergyBurnedGoal.doubleValue(for: HKUnit.kilocalorie())
                    self.moveGoalValue = activeEnergyBurnedGoal
                    let activeEnergyBurnGoalPercent = round(activeEnergyBurned/activeEnergyBurnedGoal)

                    print("Goal: \(activeEnergyBurnedGoal)")
                    print(activeEnergyBurnGoalPercent)
                }
            }
        }
        
        healthStore.execute(query)
        
        return moveGoalValue
    }

    // @objc public func getMoveRingGoal() -> Double {
    //     let activitySummaryType = HKObjectType.activitySummaryType()
        
    //     let query = HKActivitySummaryQuery.init(predicate: nil) { (query, summaries, error) in
    //         if let error = error {
    //             print("Error getting activity summary: \(error.localizedDescription)")
    //             return
    //         }
            
    //         guard let summaries = summaries, summaries.count > 0 else {
    //             print("No activity summaries found")
    //             return
    //         }
            
    //         let moveGoal = summaries[0].activeEnergyBurnedGoal.doubleValue(for: HKUnit.kilocalorie())
    //         self.moveGoalValue = moveGoal
    //         print("Move ring goal: \(String(describing: moveGoal))")
    //     }
        
    //     healthStore.execute(query)
        
    //     return moveGoalValue
    // }
}

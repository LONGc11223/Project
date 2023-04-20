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
    private var moveRingValueForDate: Double = 0.0
    
    private var moveGoalValue: Double = 0.0
    private var moveGoalValueForDate: Double = 0.0
    
    // private var standRingValue: Double = 0.0
    private var exerciseRingValue: Double = 0.0
    private var exerciseRingValueForDate: Double = 0.0
//    private var moveGoalValue: Double = 0.0
    
    @objc public func requestAuthorization() {
        let typesToRead = Set([activeEnergyBurnedType, exerciseTimeType, HKObjectType.activitySummaryType()])
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

    @objc public func getCurrentMoveRingGoal() -> Double {
        let calendar = Calendar.current
        let today = Date()

        // Set the start and end of the day for which you want to retrieve the move goal
        let startOfDay = calendar.startOfDay(for: today)

        // Define the predicate to retrieve samples for the specified day
        var dateComponents = calendar.dateComponents([.year, .month, .day], from: Date())
        dateComponents.calendar = calendar
        let predicate = HKQuery.predicateForActivitySummary(with: dateComponents)

        // Define the query to retrieve the move goal for the specified day
        let query = HKActivitySummaryQuery(predicate: predicate) { (query, summaries, error) in
            guard let summaries = summaries, let summary = summaries.first else {
                print("Failed to retrieve move goal: \(error?.localizedDescription ?? "unknown error")")
                return
            }
            
            // Retrieve the move goal value in kilocalories
            self.moveGoalValue = summary.activeEnergyBurnedGoal.doubleValue(for: HKUnit.kilocalorie())
            
            print("Move goal for \(startOfDay): \(self.moveGoalValue) kilocalories")
        }

        healthStore.execute(query)
        return moveGoalValue
    }

    @objc public func getMoveRingValue(day: Int, month: Int, year: Int, completion: @escaping (Double) -> Void) {
        let calendar = Calendar.current
        let components = DateComponents(year: year, month: month, day: day)
        let startOfDay = calendar.startOfDay(for: calendar.date(from: components)!)
        let endOfDay = calendar.date(byAdding: .day, value: 1, to: startOfDay)!
//        var moveRing = 0.0
        
        // print("Move ring: \(startOfDay)")
        
        guard let sampleType = HKSampleType.quantityType(forIdentifier: .activeEnergyBurned) else { return }
//        let startDate = Calendar.current.startOfDay(for: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startOfDay, end: endOfDay, options: .strictEndDate)
        let query = HKStatisticsQuery(quantityType: sampleType, quantitySamplePredicate: predicate, options: .cumulativeSum) { _, result, _ in
            if let sum = result?.sumQuantity() {
                var moveRing = sum.doubleValue(for: HKUnit.jouleUnit(with: .kilo)) * 0.239
                // Call the completion handler with the result
                completion(moveRing)
            }
        }
        healthStore.execute(query)
        // print("Move for date: \(moveRingValueForDate)");
        // return moveRingValueForDate
    }

    @objc public func getMoveRingGoal(day: Int, month: Int, year: Int, completion: @escaping (Double) -> Void) {
        let calendar = Calendar.current
        var components = DateComponents(year: year, month: month, day: day)
        let startOfDay = calendar.startOfDay(for: calendar.date(from: components)!)
        let endOfDay = calendar.date(byAdding: .day, value: 1, to: startOfDay)!

        // Define the predicate to retrieve samples for the specified day
        components.calendar = calendar
        let predicate = HKQuery.predicateForActivitySummary(with: components)

        // Define the query to retrieve the move goal for the specified day
        let query = HKActivitySummaryQuery(predicate: predicate) { (query, summaries, error) in
            guard let summaries = summaries, let summary = summaries.first else {
                print("Failed to retrieve move goal: \(error?.localizedDescription ?? "unknown error")")
                return
            }
            
            // Retrieve the move goal value in kilocalories
            var moveGoal = summary.activeEnergyBurnedGoal.doubleValue(for: HKUnit.kilocalorie())
            
            print("Move goal for \(startOfDay): \(moveGoal) kilocalories")
            completion(moveGoal)
        }

        healthStore.execute(query)
        // return moveGoalValueForDate
    }
    
    @objc public func getExerciseRingValue(day: Int, month: Int, year: Int, completion: @escaping (Double) -> Void) {
        let calendar = Calendar.current
        let components = DateComponents(year: year, month: month, day: day)
        let startOfDay = calendar.startOfDay(for: calendar.date(from: components)!)
        let endOfDay = calendar.date(byAdding: .day, value: 1, to: startOfDay)!
//        var exerciseRing = 0.0
        
        // print("Exercise ring: \(startOfDay)")
        
        guard let sampleType = HKSampleType.quantityType(forIdentifier: .appleExerciseTime) else { return }
//        let startDate = Calendar.current.startOfDay(for: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startOfDay, end: endOfDay, options: .strictEndDate)
        let query = HKStatisticsQuery(quantityType: sampleType, quantitySamplePredicate: predicate, options: .cumulativeSum) { _, result, _ in
            if let sum = result?.sumQuantity() {
                var exerciseRing = sum.doubleValue(for: HKUnit.minute())
                // print("Exercise ring value: \(self.exerciseRingValueForDate)")
                // Call the completion handler with the result
                completion(exerciseRing)
            }
        }
        healthStore.execute(query)
        // return exerciseRingValueForDate
    }
}

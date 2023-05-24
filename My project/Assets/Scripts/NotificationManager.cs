using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using System.Threading.Tasks;
using Unity.Notifications.iOS;
using UnityEngine;

namespace Managers
{
    public class NotificationManager : MonoBehaviour
    {
        public static IEnumerator RequestAuthorization()
        {
            var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
            using (var req = new AuthorizationRequest(authorizationOption, true))
            {
                while (!req.IsFinished)
                {
                    yield return null;
                };

                string res = "\n RequestAuthorization:";
                res += "\n finished: " + req.IsFinished;
                res += "\n granted :  " + req.Granted;
                res += "\n error:  " + req.Error;
                res += "\n deviceToken:  " + req.DeviceToken;
                Debug.Log(res);
            }
        }

        public static void PrepareNotification()
        {
            // Get all scheduled notifications
            var notifications = iOSNotificationCenter.GetScheduledNotifications();

            // Check if our notification is already scheduled
            foreach (var notification in notifications)
            {
                if (notification.Identifier == "_notification_01")
                {
                    // Our notification is already scheduled, so we can return early
                    return;
                }
            }

            var calendarTrigger = new iOSNotificationCalendarTrigger()
            {
                // Year = 2020,
                // Month = 6,
                //Day = 1,
                Hour = 6,
                Minute = 0,
                // Second = 0
                Repeats = true
            };

            var notification = new iOSNotification()
            {
                Identifier = "_notification_01",
                Title = "Workout Reminder",
                Body = "Make sure to stay active today!",
                Subtitle = "notification",
                ShowInForeground = false,
                ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
                CategoryIdentifier = "category_a",
                ThreadIdentifier = "thread1",
                Trigger = calendarTrigger,
            };

            iOSNotificationCenter.ScheduleNotification(notification);
        }
    }
}

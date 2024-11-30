using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task=>
        {
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSignUp);
        });
    }
}

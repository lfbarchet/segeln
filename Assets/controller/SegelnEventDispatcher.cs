using System.Collections;
using System.Collections.Generic;
using MQTTnet;
using Newtonsoft.Json;
using PuzzleCubes.Communication;
using PuzzleCubes.Controller;
using UnityEngine;
using PuzzleCubes.Models;

public class SegelnEventDispatcher : EventDispatcher
{

    private const string segelnNavigationStateTopic = "segeln/app/navigation";   
    public GameObject objectToRotate;                       // from server



    protected override void Initialize()
    {
        base.Initialize();
        objectToRotate = GameObject.Find("World");


        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(segelnNavigationStateTopic).Build(), HandleSegelnNavigationState);
        Debug.Log($"Subscribed to {segelnNavigationStateTopic}");
    }
    public void HandleSegelnNavigationState(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<NavigationState>(data);
        Debug.Log("HandleSegelnNavigationState");
        Debug.Log(result);
        Debug.Log("objekt:");
        Debug.Log(objectToRotate);
        Debug.Log(result.Orientation);
        // Debug.Log($"Orientation: {result.Orientation}, Speed: {result.Speed}, Position: {result.Position}");
        objectToRotate.transform.rotation = Quaternion.Euler(0, result.Orientation, 0);


        var newState = new NavigationState
        {
            Orientation = result.Orientation,
            Speed = result.Speed,
            Position = result.Position
        };

        NavigationStateChangedEvent.Instance.Invoke(newState);
    }

}

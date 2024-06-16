using System.Collections;
using System.Collections.Generic;
using MQTTnet;
using Newtonsoft.Json;
using PuzzleCubes.Communication;
using PuzzleCubes.Controller;
using UnityEngine;
using PuzzleCubes.Models;
using System;

public class SegelnEventDispatcher : EventDispatcher
{
    private const string segelnNavigationStateTopic = "segeln/app/navigation";                          // from server



    protected override void Initialize()
    {
        base.Initialize();


        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(segelnNavigationStateTopic).Build(), HandleSegelnNavigationState);
        Debug.Log($"Subscribed to {segelnNavigationStateTopic}");
    }
    public void HandleSegelnNavigationState(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<NavigationState>(data);
        Debug.Log("HandleSegelnNavigationState");

        WheelService.Instance.HandleWheelRawWheelData(result.Orientation);
    }


}

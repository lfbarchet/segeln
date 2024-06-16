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
    private const string wheelStateTopic = "segeln/app/wheel";

    public static SegelnEventDispatcher Instance;

    protected override void Initialize()
    {
        Instance = this;
        base.Initialize();


        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(wheelStateTopic).Build(), HandleWheelStateChangedEvent);
        Debug.Log($"Subscribed to {wheelStateTopic}");
    }
    public void HandleWheelStateChangedEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<WheelState>(data);
        Debug.Log("HandleWheelStateChangedEvent");
        WheelService.Instance.HandleWheelStateChangeFromServer(result);
    }


    public void DispatchWheelStateChangedEvent(WheelState state)
    {
        var json = JsonConvert.SerializeObject(state, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Objects
        });

        var msg = new MqttApplicationMessage
        {
            Topic = wheelStateTopic,
            Payload = System.Text.Encoding.UTF8.GetBytes(json),
            MessageExpiryInterval = 3600
        };


        mqttCommunication.Send(msg);
    }
}

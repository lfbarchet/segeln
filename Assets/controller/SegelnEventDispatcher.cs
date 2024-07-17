using System.Collections;
using System.Collections.Generic;
using MQTTnet;
using Newtonsoft.Json;
using PuzzleCubes.Communication;
using PuzzleCubes.Controller;
using UnityEngine;
using PuzzleCubes.Models;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;

public class SegelnEventDispatcher : EventDispatcher
{
    private const string gameStateTopic = "segeln/app/gameState";

    private const string wheelStateTopic = "segeln/app/wheel";
    private const string sailStateTopic = "segeln/app/sail";
    private const string eventsTopic = "segeln/app/events";

    private const string performanceEventTopic = "segeln/app/performance";

    public static SegelnEventDispatcher Instance;

    protected override void Initialize()
    {
        Instance = this;
        base.Initialize();

        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(gameStateTopic).Build(), HandleGameStateEvent);
        Debug.Log($"Subscribed to {gameStateTopic}");

        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(wheelStateTopic).Build(), HandleWheelStateChangedEvent);
        Debug.Log($"Subscribed to {wheelStateTopic}");

        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(sailStateTopic).Build(), HandleSailStateChangedEvent);
        Debug.Log($"Subscribed to {sailStateTopic}");

        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(performanceEventTopic).Build(), HandlePerformanceEventStateChangedEvent);
        Debug.Log($"Subscribed to {performanceEventTopic}");
        subscriptions.Add(new MqttTopicFilterBuilder().WithTopic(eventsTopic).Build(), HandleEventTriggeredEvent);
        Debug.Log($"Subscribed to {eventsTopic}");
    }

    public void HandleEventTriggeredEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        Debug.Log("Event triggered");

        try
        {
            var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            print("jsonob: " + jsonObject);

            if (jsonObject != null) //&& jsonObject.ContainsKey("name") && jsonObject["name"] == "start")
            {
                //Debug.Log("jetzt wechseln");

                SceneManager.LoadScene(1);


                var roles = jsonObject["roles"] as JObject;
                string wheelRole = roles["wheel"].Value<string>();
                string sailRole = roles["sail"].Value<string>();
                string mapRole = roles["map"].Value<string>();
                MqttCommunication mqttCommunication = FindObjectOfType<MqttCommunication>();
                Debug.LogWarning("mqttCommunication.clientId: " + mqttCommunication.clientId);
                Debug.LogWarning("wheelRole: " + wheelRole);
                Debug.LogWarning("sailRole: " + sailRole);
                Debug.LogWarning("mapRole: " + mapRole);

                var clientId = mqttCommunication.clientId;
                Debug.LogWarning("clientId: " + clientId);

                if (wheelRole.Equals(clientId))
                {
                    GameManager.Instance.SetCubeRole(CubeRole.Wheel);
                }
                if (sailRole.Equals(clientId))
                {
                    GameManager.Instance.SetCubeRole(CubeRole.Sail);
                }
                if (mapRole.Equals(clientId))
                {
                    GameManager.Instance.SetCubeRole(CubeRole.Map);
                }


            }
        }
        catch (JsonException jsonEx)
        {
            Debug.LogError($"JSON Parsing Error: {jsonEx.Message}");
        }

    }

    public void DispatchGameStateEvent(GameState state)
    {
        DispatchEvent(gameStateTopic, state);
    }

    public void HandleGameStateEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        HandleEvent<GameState>(msg, wildcardItem, GameStateService.Instance.HandleGameStateChangeFromServer);
    }

    public void HandleWheelStateChangedEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        HandleEvent<WheelState>(msg, wildcardItem, WheelService.Instance.HandleWheelStateChangeFromServer);
    }


    public void DispatchWheelStateChangedEvent(WheelState state)
    {
        DispatchEvent(wheelStateTopic, state);
    }

    public void HandleSailStateChangedEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        HandleEvent<SailState>(msg, wildcardItem, SailService.Instance.HandleSailStateChangeFromServer);
    }

    public void DispatchSailStateChangedEvent(SailState state)
    {
        DispatchEvent(sailStateTopic, state);
    }

    public void HandlePerformanceEventStateChangedEvent(MqttApplicationMessage msg, IList<string> wildcardItem)
    {
        HandleEvent<PerformanceEventState>(msg, wildcardItem, PerformanceEventService.Instance.HandlePerformanceEventStateChangeFromServer);
    }

    public void DispatchPerformanceEventStateChangedEvent(PerformanceEventState state)
    {
        DispatchEvent(performanceEventTopic, state);
    }

    // generic methods

    private void HandleEvent<T>(MqttApplicationMessage msg, IList<string> wildcardItem, Action<T> action)
    {
        var data = System.Text.Encoding.UTF8.GetString(msg.Payload);
        var result = JsonConvert.DeserializeObject<T>(data);

        action(result);
    }

    private void DispatchEvent(string topic, object state)
    {
        var json = JsonConvert.SerializeObject(state, Formatting.Indented, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Objects
        });

        var msg = new MqttApplicationMessage
        {
            Topic = topic,
            Payload = System.Text.Encoding.UTF8.GetBytes(json),
            MessageExpiryInterval = 3600
        };

        mqttCommunication.Send(msg);
    }
}

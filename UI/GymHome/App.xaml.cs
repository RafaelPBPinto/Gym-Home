﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GymHome
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            if (!File.Exists(m_loggerPath))
            {
                var file = File.Create(m_loggerPath);
                file.Dispose();
                //file.Close();
            }

            Logger.Init(m_loggerPath);
            Logger.Log("logger initialized");
            Logger.Log("Initializing base component");

            this.InitializeComponent();

            Logger.Log("Initializing mqtt");
            m_mqttFactory = new MqttFactory();
            m_mqttClient = m_mqttFactory.CreateMqttClient();
            try
            {
                StartMqttListener("localhost", 1883).GetAwaiter();
            }
            catch(Exception ex) 
            {
                Logger.Error($"Failed to initialize mqtt. {ex.Message}");
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            //save the window in case its needed
            m_window = new Window();

            //create a new frame and navigate to the initial page
            m_window.Content = m_rootFrame = new Frame();
            m_window.Title = "GymHome";
            m_window.Activate();
            m_rootFrame.Navigate(typeof(LogInPage));
        }

        /// <summary>
        /// Navigate through pages using the root frame.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        public void Navigate(Type pageType,object param = null)
        {
            m_rootFrame.Navigate(pageType,param);
        }

        /// <summary>
        /// Adds a command to be called when the given keyword is received from the mqtt system.
        /// Any existing command is overriden if the <paramref name="keyword"/> is the same.
        /// If <paramref name="keyword"/> is null then the command wont be added.
        /// </summary>
        /// <param name="action">Action to be called</param>
        /// <param name="keyword">Keyword used to call <paramref name="action"/></param>
        public void AddCommand(Action<string> action,string keyword)
        {
            if (keyword == null)
                return;

            Logger.Info($"Added command with keyword \"{keyword}\"");
            m_mqttActions[keyword] = action;
        }

        public void RemoveCommand(string keyword) 
        {
            Logger.Info($"Removing command wiht keyword \"{keyword}\"");
            m_mqttActions.Remove(keyword);
        }

        /// <summary>
        /// Checks if the <paramref name="keyword"/> exists in the list.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public bool keywordExists(string keyword) 
        {
            return m_mqttActions.ContainsKey(keyword);
        }

        /// <summary>
        /// Navigate to the most recent page in the page history.
        /// </summary>
        public void NavigateToPreviousPage()
        {
            try
            {
                m_rootFrame.GoBack();
            }
            catch(Exception ex) 
            {
                Logger.Error($"Failed to navigate to previous page. {ex.Message}");
            }
        }

        private Window m_window;
        private Frame m_rootFrame;
        private MqttFactory m_mqttFactory;
        private IMqttClient m_mqttClient;
        private Dictionary<string,Action<string>> m_mqttActions = new Dictionary<string, Action<string>>();
        private readonly DispatcherQueue m_dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        private readonly string m_loggerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"log.txt");

        private async Task StartMqttListener(string serverName,int serverPort)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder().WithClientId("GymHomeUI").WithTcpServer(serverName,serverPort).Build();

            m_mqttClient.ApplicationMessageReceivedAsync += MessageReceived;

            await m_mqttClient.ConnectAsync(mqttClientOptions,CancellationToken.None);

            var mqttSubscriberOptions = m_mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(f =>
            {
                f.WithTopic("comandos/voz/UI");
            }).Build();

            await m_mqttClient.SubscribeAsync(mqttSubscriberOptions,CancellationToken.None);
        }

        private Task MessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            string message = Encoding.UTF8.GetString(arg.ApplicationMessage.Payload);
            Logger.Log($"Mqtt message received. Message: {message}");
            try
            {
                MqttCommand command = JsonSerializer.Deserialize<MqttCommand>(message);
                bool res = m_dispatcherQueue.TryEnqueue(DispatcherQueuePriority.Normal,
                    () =>
                    {
                        if (m_mqttActions.ContainsKey(command.Command))
                               m_mqttActions[command.Command].Invoke(command.Arg);
                        else
                            Logger.Warning($"Unknown command received. {command.Command}");
                    });
            }
            catch(Exception ex)
            {
                Logger.Error($"Caught exception while trying to execute received mqtt command. {ex.Message}");
                Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;

        }
    }
}

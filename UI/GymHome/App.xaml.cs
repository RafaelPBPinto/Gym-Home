﻿// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
            this.InitializeComponent();
            m_mqttFactory = new MqttFactory();
            m_mqttClient = m_mqttFactory.CreateMqttClient();
            try
            {
                StartMqttListener("localhost", 1883);
            }
            catch(Exception ex) 
            {
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

            //create a new frame and navigate to the MainPage
            m_window.Content = m_rootFrame = new Frame();
            m_window.Title = "GymHome";
            m_window.Activate();
            m_rootFrame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Navigate through pages using the root frame.
        /// </summary>
        /// <param name="pageType">The type of the page to navigate to.</param>
        public void Navigate(Type pageType)
        {
            m_rootFrame.Navigate(pageType);
        }

        /// <summary>
        /// Navigate to the most recent page in the page history.
        /// </summary>
        public void NavigateToPreviousPage()
        {
            m_rootFrame.GoBack();
        }

        private Window m_window;
        private Frame m_rootFrame;
        private MqttFactory m_mqttFactory;
        private IMqttClient m_mqttClient;

        private async Task StartMqttListener(string serverName,int serverPort)
        {
            var mqttClientOptions = new MqttClientOptionsBuilder().WithClientId("GymHomeUI").WithTcpServer(serverName,serverPort).Build();

            m_mqttClient.ApplicationMessageReceivedAsync += MessageReceived;

            await m_mqttClient.ConnectAsync(mqttClientOptions,CancellationToken.None);

            var mqttSubscriberOptions = m_mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(f =>
            {
                f.WithTopic("comandos");
            }).Build();

            await m_mqttClient.SubscribeAsync(mqttSubscriberOptions,CancellationToken.None);
        }

        private Task MessageReceived(MqttApplicationMessageReceivedEventArgs arg)
        {
            throw new NotImplementedException();
        }
    }
}

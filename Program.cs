﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Linq;
using System.Threading;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace EyeCare
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Any((arg) => arg.Contains("ToastActivated"))) Environment.Exit(0);

            // Register COM server and activator type
            DesktopNotificationManagerCompat.RegisterActivator<LinkNotificationActivator>();
            DesktopNotificationManagerCompat.RegisterAumidAndComServer<LinkNotificationActivator>("Aanand.EyeCare");

            // Construct the visuals of the toast (using Notifications library)
            ToastContent toastContent = new ToastContent()
            {
                // Arguments when the user taps body of toast
                Launch = "action=viewConversation&conversationId=5",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
            {
                new AdaptiveText()
                {
                    Text = "Give your eyes a break!"
                },
                new AdaptiveText()
                {
                    Text = "Refocus your eyes on something at least 20 feet away"
                },
            }
                    }
                }
            };

            // Create the XML document (BE SURE TO REFERENCE WINDOWS.DATA.XML.DOM)
            var doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());

            // And create the toast notification
            var toast = new ToastNotification(doc);

            // And then show it
            DesktopNotificationManagerCompat.CreateToastNotifier().Show(toast);

            Thread.Sleep(1000);
        }
    }
}
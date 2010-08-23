using Microsoft.WindowsMobile.Status;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using XbmcEventClient;

namespace XBMC_Remote
{
    class BackgroundThread
    {
        public string IpAddress;

        EventClient BackgroundClient = new EventClient();

        public void StartBackgroundWorker()
        {
            Thread BackgroundThread = new Thread(new ThreadStart(BackgroundWork));
            BackgroundThread.IsBackground = true;
            BackgroundThread.Name = "BackgroundWorker";
            BackgroundThread.Start();
        }

        public void BackgroundWork()
        {
            string Caller;

            BackgroundClient.Connect(IpAddress);

            while (true)
            {
                if (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCall == true)
                {
                    if (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerName != null)
                    {
                        Caller = Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerName;
                    }
                    else
                    {
                        Caller = Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCallerNumber;
                    }
                    BackgroundClient.SendNotification("Incoming Call", Caller, IconType.ICON_NONE, null);
                    BackgroundClient.SendButton("pause", "r1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
                    while (Microsoft.WindowsMobile.Status.SystemState.PhoneIncomingCall == true)
                    {
                        Thread.Sleep(200);
                    }
                    while (Microsoft.WindowsMobile.Status.SystemState.PhoneCallTalking == true)
                    {
                        Thread.Sleep(200);
                    }
                    BackgroundClient.SendButton("play", "r1", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
                }
                Thread.Sleep(500);
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsCE.Forms;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

    /// <summary>
    /// Watch out for Win32 messaging from RTRule.dll which tells us when there is a new incoming text message
    /// </summary>
public class NewMsgWindow : MessageWindow
{
    /// <summary>
    /// Fire when there is an incoming SMS message from the specified sender.
    /// Return TRUE to indicate that the emssage has been processed (native msg app should not be notified)
    /// </summary>
    public event NewTextMessageEventHandler OnNewTextMessage;

    /// <summary>
    /// Fire when there is an incoming SMS message from the specified sender.
    /// Return TRUE to indicate that the emssage has been processed (native msg app should not be notified)
    /// </summary>
    public delegate bool NewTextMessageEventHandler(string sender, string messageText);

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_COPYDATA) //we received a message from RTRule.dll telling us that there is an incoming SMS message
        {
            CopyDataStruct cs = (CopyDataStruct)Marshal.PtrToStructure(m.LParam, typeof(CopyDataStruct));

            // RTRule.dll sends the message sender & text in cs.Data in the format sender @ MessageText
            // we parse the data and fire the event
            char delim = '\n';

            if (cs.Data.Contains(delim))
            {
                string sender = cs.Data.Split(delim)[0];
                string messageText = cs.Data.Split(delim)[1];

                //TODO: Why all messages ends with "- GSM"? We have to remove it manually here.
                if (messageText.EndsWith("- GSM")) messageText = messageText.Remove(messageText.Length - 5, 5);

                if (OnNewTextMessage != null && OnNewTextMessage(sender, messageText))
                {
                    // a LRESULT of 1 marks message as processed. 
                    // and native msg app will not show msg in Inbox
                    m.Result = new IntPtr(0);
                }
                else
                    // Other LRESULT indicates we did not process the message
                    // and the message will be passed on to native messaging application.
                    m.Result = new IntPtr(0);
            }
            else
            {
                Debug.WriteLine("Invalid WM_COPYDATA message. Received: " + cs.Data);
            }
        }
        else //other Window messages, do not handle
        {
            base.WndProc(ref m);
        }
    }

    private const uint WM_COPYDATA = 0x004a;

    [StructLayout(LayoutKind.Sequential)]
    private struct CopyDataStruct
    {
        public int IntData;
        public int Length;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string Data;
    }
}
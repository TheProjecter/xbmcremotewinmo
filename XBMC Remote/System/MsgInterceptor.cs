using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace XBMC_Remote
{
    class MsgInterceptorSetup
    {
        // GUID of RTRule.dll. Must match with actual declaration in the file
        private string CLSID_RT = "{708C1547-D4AB-49d2-94D0-988431784506}";

        /// <summary>
        /// Get the application directory.
        /// </summary>
        public static string getAppDir()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        }

        /// <summary>
        /// Create our own incoming message interceptor by extending MAPIRule from SDK Samples.
        /// This will work on all devices. The default .NET MessageInterceptor will fail to work on HTC HD2
        /// </summary>
        public void CreateInterceptorMethod2()
        {
            RemoveInterceptorMethod2();

            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\SVC\\SMS\\Rules");

            if (r != null)
            {
                try
                {
                    Debug.WriteLine("Creating Rule Method 2");
                    r.SetValue(CLSID_RT, 1);
                    Debug.WriteLine("Rule method 2 was created");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error creating Rule method 2: " + e.ToString());
                }

                r.Close();
            }

            RegistryKey r2 = Registry.ClassesRoot.CreateSubKey("\\CLSID\\" + CLSID_RT + "\\InprocServer32");

            if (r2 != null)
            {
                try
                {
                    Debug.WriteLine("Creating CLSID Method 2");
                    r2.SetValue("Default", getAppDir() + @"\RTRule.dll");
                    Debug.WriteLine("CLSID method 2 was created");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error creating CLSID method 2: " + e.ToString());
                }

                r2.Close();
            }
        }

        /// <summary>
        /// Clear our custom message interceptor registry setup
        /// </summary>
        public void RemoveInterceptorMethod2()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Inbox\\SVC\\SMS\\Rules");

            if (r != null)
            {
                try
                {
                    Debug.WriteLine("Deleting Rule Method 2");
                    r.DeleteValue(CLSID_RT);
                    Debug.WriteLine("Rule method 2 was deleted");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error deleting Rule method 2: " + e.ToString());
                }

                r.Close();
            }

            RegistryKey r2 = Registry.ClassesRoot.CreateSubKey("\\CLSID");

            try
            {
                r2.DeleteSubKeyTree(CLSID_RT);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error deleting Rule method 2: " + e.ToString());
            }

            if (r2 != null)
                r2.Close();
        }

        /// <summary>
        /// Reboot the device
        /// </summary>
        public void ResetUnit()
        {
            int bytesReturned = 0;
            long IOCTL_HAL_REBOOT = 0x101003c;
            KernelIoControl(IOCTL_HAL_REBOOT, null, 0, null, 0, ref bytesReturned);
        }

        [DllImport("coredll.dll")]
        public static extern void KernelIoControl(long dwIoControlCode, int[] lpInBuf, long nInBufSize, byte[] lpOutBuf, byte nOutBufSize, ref int lpBytesReturned);
    }
}

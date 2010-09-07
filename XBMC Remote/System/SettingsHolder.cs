using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XBMC_Remote
{
    public class App
    {
        public static Settings Configuration;

        static App()
        {
            Configuration = new Settings();
        }
    }

    public class Settings
    {
        public string IpAddress;
        public string WebPort;
        public string Username;
        public string Password;
        public string Timeout;

        public int MinimumMovement = 25;
        public int ThreadSleep = 75;
        public float Velocity = .99f;
        public float Springback = .35f;


        public Settings()
        {
            ReadSettings();
        }

        public void ReadSettings()
        {
            try
            {
                XmlDocument settingsReader = new XmlDocument();
                settingsReader.Load("\\Application Data\\XBMC_Remote\\settings.xml");
                XmlNode oNode = settingsReader.DocumentElement;

                foreach (XmlNode x in oNode.ChildNodes)
                {
                    if (x.Name == "IpAddress")
                    {
                        IpAddress = x.InnerText;
                    }
                    if (x.Name == "WebPort")
                    {
                        WebPort = x.InnerText;
                    }
                    if (x.Name == "Username")
                    {
                        Username = x.InnerText;
                    }
                    if (x.Name == "Password")
                    {
                        Password = x.InnerText;
                    }
                    if (x.Name == "Timeout")
                    {
                        Timeout = x.InnerText;
                    }
                }
            }
            catch
            {
                Directory.CreateDirectory("\\Application Data\\XBMC_Remote");
                Directory.CreateDirectory("\\Application Data\\XBMC_Remote\\cache");
            }
        }

        public void WriteSettings()
        {
            try
            {
                XmlTextWriter textWriter = new XmlTextWriter("\\Application Data\\XBMC_Remote\\settings.xml", null);
                textWriter.WriteStartDocument();
                textWriter.WriteComment("XBMC Remote settings file.");
                textWriter.WriteStartElement("Settings");
                
                textWriter.WriteStartElement("IpAddress");
                textWriter.WriteString(IpAddress);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("WebPort");
                textWriter.WriteString(WebPort);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Username");
                textWriter.WriteString(Username);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Password");
                textWriter.WriteString(Password);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Timeout");
                textWriter.WriteString(Timeout);
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                System.IO.Directory.CreateDirectory("\\Application Data\\XBMC_Remote");
            }
        }
    }
}
﻿using System;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using System.IO;
using System.Net;

/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/

namespace ConsoleApp1
{
    public class Program
    {
        public static string xmlURL = "https://yourusername.github.io/Hotels.xml";           // Replace with actual URL
        public static string xmlErrorURL = "https://yourusername.github.io/HotelsErrors.xml"; // Replace with actual URL
        public static string xsdURL = "https://yourusername.github.io/Hotels.xsd";            // Replace with actual URL

        public static void Main(string[] args)
        {
            string result;

            result = Verification(xmlURL, xsdURL);
            Console.WriteLine("Verification of Hotels.xml:");
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine("\nVerification of HotelsErrors.xml:");
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine("\nXML to JSON conversion of Hotels.xml:");
            Console.WriteLine(result);
        }

        // Q2.1 XML Schema Validation
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, XmlReader.Create(xsdUrl));
                settings.ValidationType = ValidationType.Schema;

                string errorMessage = "No Error";

                settings.ValidationEventHandler += (sender, args) =>
                {
                    errorMessage = args.Message;
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }

                return errorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Q2.2 XML to JSON conversion
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                string xmlContent;
                using (WebClient client = new WebClient())
                {
                    xmlContent = client.DownloadString(xmlUrl);
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlContent);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);

                return jsonText;
            }
            catch (Exception ex)
            {
                return $"Error during XML to JSON conversion: {ex.Message}";
            }
        }
    }
}

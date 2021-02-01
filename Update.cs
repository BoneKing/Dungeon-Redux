﻿using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO.Compression;

namespace Update{
    class Program{
        //string serverPath = "/var/www/html/Dungeon-Redux/Version.txt";
        static void Main(string[] args){
            //Get Local Version
            //StreamReader F = new StreamReader("Version.txt");
            //Lversion = F.ReadLine();
            string Lversion = "0.1.14"; //local version
            string Sversion; //Server Version
            string remoteURI = "http://www.fortrash.com/Dungeon-Redux/";
            string pwd = Directory.GetCurrentDirectory();
            string fileName;
            Console.WriteLine("Local Version = {0}", Lversion);
            //Get Remote Version
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("http://www.fortrash.com/Dungeon-Redux/Version.txt");
            StreamReader reader = new StreamReader(stream);
            Sversion = reader.ReadToEnd();
            Console.WriteLine("Server Version = {0}", Sversion);
            char delimiter = '.';
            string[] LV = Lversion.Split(delimiter);
            string[] SV = Sversion.Split(delimiter);
            for(int i = 0; i < 3; i++)
            {
                if(Int32.Parse(SV[i]) > Int32.Parse(LV[i])){
                    Console.WriteLine("New version found! Would you like to download it now? [y/n]");
                    string ans = Console.ReadLine();
                    if(ans == "y"){
                        fileName = checkPlatform();
                        downloadNewVersion(remoteURI, fileName, pwd);
                        Console.WriteLine("Update Complete!");
                    }
                    break;
                }
            }
        }
        static public string checkPlatform(){
            string fileName;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "linux-x64.zip";
                //path = @"../../../../";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                fileName = "osx.10.11-x64.zip";
            }
            else{
                fileName = "win10-x64.zip";
            }
            return fileName;
        }
        static public void downloadNewVersion(string remoteURI, string fileName, string pwd){
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            string myStringWebResource = remoteURI + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource,fileName);		
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            ZipFile.ExtractToDirectory(fileName, pwd, true);
            Console.WriteLine("Installing {0} in folder {1}", fileName, pwd);
            Console.WriteLine("Install Complete");
        }
    }
}
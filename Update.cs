using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO.Compression;

namespace Dungeon_Redux{
    class Update{
        //string serverPath = "/var/www/html/Dungeon-Redux/Version.txt";
        public void StartUpdate(){
            //Get Local Version
            //StreamReader F = new StreamReader("Version.txt");
            //Lversion = F.ReadLine();
            int updateFlag = 0;
            string Lversion = "0.1.14"; //local version
            string Sversion; //Server Version
            string remoteURI = "http://www.fortrash.com/Dungeon-Redux/";
            string pwd = Directory.GetCurrentDirectory();
            string fileName = "";
            string tempDir = "";
            GetUpdateInfo(ref updateFlag, ref Lversion);
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
                        checkPlatform(ref fileName, ref tempDir);
                        downloadNewVersion(remoteURI, fileName, pwd, tempDir);
                        Console.WriteLine("Update Complete!");
                    }
                    break;
                }
            }
        }
        public void checkPlatform(ref string fileName, ref string tempDir){ //checks platform and selects proper file and folder structure syntax
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "linux-x64.zip";
                tempDir = "../../";
                //path = @"../../../../";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                fileName = "osx.10.11-x64.zip";
                tempDir = "../";
            }
            else{
                fileName = @".\win10-x64.zip";
                tempDir = @"..\..\";
            }
        }
        public void downloadNewVersion(string remoteURI, string fileName, string pwd, string tempDir){
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            string myStringWebResource = remoteURI + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // Download the Web resource and save it into the current filesystem folder.
            myWebClient.DownloadFile(myStringWebResource,fileName);		
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            Console.WriteLine("Downloaded {0} in path {1}", fileName, pwd);
            File.Copy(fileName, "..\\");
            //Assembly currentAssembly = Assembly.GetEntryAssembly();
            //if (currentAssembly == null){
                //currentAssembly = Assembly.GetCallingAssembly();
            //}
            //ZipFile.ExtractToDirectory(fileName, currentAssembly+tempDir, true);
            Console.WriteLine("Press any key to close program, unzip and relaunch from new folder and delete the old one.");
            Console.ReadLine(); //wait for input before closing
        }
        public void GetUpdateInfo(ref int updateFlag, ref string Lversion){
            
        }
    }
}
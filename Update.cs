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
            string Lversion = "0.1.14"; //local version
            string Sversion; //Server Version
            string remoteURI = "http://www.fortrash.com/Dungeon-Redux/";
            string pwd = Directory.GetCurrentDirectory();
            string fileName = "";
            string tempDir = "";
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
        public void checkPlatform(ref string fileName, ref string tempDir){
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "linux-x64.zip";
                tempDir = "/Update/linux-x64";
                //path = @"../../../../";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                fileName = "osx.10.11-x64.zip";
                tempDir = "/Update/osx.10.11-x64";
            }
            else{
                fileName = "win10-x64.zip";
                tempDir = @"\Update\win10-x64";
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
            Console.WriteLine("Installing {0} in folder {1}", fileName, pwd);
            ZipFile.ExtractToDirectory(fileName, pwd+tempDir, true);
            Assembly currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null){
                currentAssembly = Assembly.GetCallingAssembly();
            }
            Console.WriteLine("TIME TO GET UPDATE FOLDER");
            string UpdateFolder = Path.GetDirectoryName(currentAssembly+tempDir);
            DirectoryInfo d = new DirectoryInfo(UpdateFolder);
            var destinationFile = "";
            foreach (var file in d.GetFiles())
            {
                Console.WriteLine("suck peen");
                destinationFile = file.Name;
            }
            if (currentAssembly.Location.ToUpper() == destinationFile.ToUpper()){
                string appFolder = Path.GetDirectoryName(currentAssembly.Location);
                string appName = Path.GetFileNameWithoutExtension(currentAssembly.Location);
                string appExtension = Path.GetExtension(currentAssembly.Location);
                string archivePath = Path.Combine(appFolder, appName + "_OldVersion" + appExtension);
                if (File.Exists(archivePath)){
                    File.Delete(archivePath);
                }
                File.Move(destinationFile, archivePath);
            }
                //File.Move(destinationFile, archivePath);
                //ZipFile.ExtractToDirectory(fileName, pwd+tempDir, true);
            Console.WriteLine("Install Complete");
        }
    }
}
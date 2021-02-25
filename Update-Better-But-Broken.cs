/* remove this comment and one at the bottom to use
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO.Compression;
//using System.Windows.Forms;

namespace Dungeon_Redux{
    class BrokenUpdate{ //CHANGE THIS BACK TO UPDATE WHEN THIS WORKS
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
        public void checkPlatform(ref string fileName, ref string tempDir){
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                fileName = "linux-x64.zip";
                tempDir = "/Update";
                //path = @"../../../../";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                fileName = "osx.10.11-x64.zip";
                tempDir = "/Update";
            }
            else{
                fileName = @".\win10-x64.zip";
                tempDir = @".\Update";
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
            //ZipFile.ExtractToDirectory(fileName, pwd+tempDir, true);
            Assembly currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null){
                currentAssembly = Assembly.GetCallingAssembly();
            }
            ZipFile.ExtractToDirectory(fileName, currentAssembly+tempDir, true);
            Console.WriteLine("TIME TO GET UPDATE FOLDER");
            //string UpdateFolder = Path.GetDirectoryName(pwd);
            string UpdateFolder = @".\Dungeon-Redux, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Update\win10-x64";
            Console.WriteLine("Update folder = {0}", UpdateFolder);
            //UpdateFolder += tempDir;
            //Console.WriteLine("Update folder = {0}", UpdateFolder);
            string sourceFile = UpdateFolder+"/Dungeon-Redux.exe";
            DirectoryInfo d = new DirectoryInfo(UpdateFolder);
            //Console.WriteLine(d);
            var destinationFile = "Dungeon-Redux.exe";
            if(currentAssembly.Location.ToUpper() == destinationFile.ToUpper()){
                string appFolder = Path.GetDirectoryName(currentAssembly.Location);
                string appName = Path.GetFileNameWithoutExtension(currentAssembly.Location);
                string appExtension = Path.GetExtension(currentAssembly.Location);
                string archivePath = Path.Combine(appFolder, appName + "_OldVersion" + appExtension);
                if(File.Exists(archivePath)){
                    File.Delete(archivePath);
                }
                File.Move(destinationFile, archivePath);
                File.Copy(sourceFile, destinationFile, true);
                Application.Restart();
            }
            Console.WriteLine("TIME TO GET FILES");
            string appFolder2 = Path.GetDirectoryName(currentAssembly.Location);
            foreach(var file in d.GetFiles()){
                if(file.Name != "Dungeon-Redux.exe"){
                    if(File.Exists(appFolder2+file.Name)){
                        File.Move(appFolder2+file.Name, appFolder2+file.Name+".bak");
                    }
                    //File.Move(file.Name, archivePath);
                    Console.WriteLine("Moving file {0} to {1} from {2}", file.Name, appFolder2, d);
                    File.Move(d+@"\"+file.Name, appFolder2+@"\"+file.Name, true);
                }
            }
            /*foreach (var file in d.GetFiles())
            {
                Console.WriteLine("suck peen");
                destinationFile = file.Name;
                Console.WriteLine("destinationFile = {0}", destinationFile);
                //if (currentAssembly.Location.ToUpper() == destinationFile.ToUpper()){
                string appFolder = Path.GetDirectoryName(currentAssembly.Location);
                string appName = Path.GetFileNameWithoutExtension(currentAssembly.Location);
                string appExtension = Path.GetExtension(currentAssembly.Location);
                string archivePath = Path.Combine(appFolder, appName + "_OldVersion" + appExtension);
                if (File.Exists(archivePath)){
                    //File.SetAttributes(archivePath, FileAttributes.Normal);
                    try {
                        File.Delete(archivePath);
                    }
                    catch(Exception e){
                        Console.WriteLine("Exception thrown in  trying to delete file. {0}", e);
                    }
                }
                try {
                    File.Move(destinationFile, archivePath);
                }
                catch(Exception e){
                    Console.WriteLine("Exception thrown in  trying to move file. {0}", e);
                }
                //File.SetAttributes(destinationFile, FileAttributes.Normal);
                Console.WriteLine("Moved {0} into {1}", destinationFile, archivePath);
                //}
            }
                //File.Move(destinationFile, archivePath);
                //ZipFile.ExtractToDirectory(fileName, pwd+tempDir, true);
            
            Console.WriteLine("Install Complete");
            Console.ReadLine();
        }
        public void GetUpdateInfo(ref int updateFlag, ref string Lversion){
            
        }
    }
}
*/
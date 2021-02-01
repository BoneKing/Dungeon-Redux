using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Dungeon_Redux
{
    class GoToUpdate{
        string pwd = Directory.GetCurrentDirectory();
        public void StartUpdater(){
            Process.Start("Update.exe");
        }
    }
}
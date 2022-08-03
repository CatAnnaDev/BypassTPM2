using Microsoft.Win32;
using static Microsoft.Win32.Registry;

namespace BypassTPM2
{
    internal class Program
    {
        private static string[] path = {  @"SYSTEM\Setup\MoSetup", @"SYSTEM\Setup\LabConfig" };
        
        private static void Main(string[] args)
        {

            Console.Write("Enable BypassTPM2: 1\nDisable BypassTPM2: 2\n=>");
            int val = int.Parse(Console.ReadLine() ?? string.Empty);

            switch(val){
                case 1:
                    key(path, 1);
                    break;
                case 2:
                    key(path, 0);
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
       }

        private static void key(string[] path, int value)
        {
            foreach (var dataPath in path)
            {
                using (RegistryKey moSetup = LocalMachine.CreateSubKey(dataPath))
                {
                    if(dataPath == @"SYSTEM\Setup\MoSetup")
                    {
                        moSetup.SetValue("AllowUpgradesWithUnsupportedTPMOrCPU", value);
                        moSetup.SetValue("AllowUpdatesWithUnsupportedTPMOrCPU", value);
                    }
                    else if (dataPath == @"SYSTEM\Setup\LabConfig")
                    {
                        moSetup.SetValue("BypassRAMCheck", value);
                        moSetup.SetValue("BypassSecureBootCheck", value);
                        moSetup.SetValue("BypassTPMCheck", value);
                    }
                    moSetup.Close();
                }
            }
            Environment.Exit(0);
        }
    }
}

// Computer\HKEY_LOCAL_MACHINE\SYSTEM\Setup\MoSetup > AllowUpdatesWithUnsupportedTPMOrCPU ?? 1 / AllowUpgradesWithUnsupportedTPMOrCPU 1

// Computer\HKEY_LOCAL_MACHINE\SYSTEM\Setup > LabConfig >  BypassRAMCheck 1 / BypassSecureBootCheck 1 / BypassTPMCheck 1

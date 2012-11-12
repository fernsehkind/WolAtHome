using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WolAtHome
{
    class Program
    {
        static void PrintError(string errormessage)
        {
            Console.WriteLine(errormessage);
            Console.WriteLine("Example 1: WolAtHome.exe 00245843a44h");
            Console.WriteLine("Example 2: WolAtHome.exe 00245843a44h 192.168.1.1");
            Console.WriteLine("Example 3: WolAtHome.exe 00245843a44h 192.168.1.1 9");
        }

        static void Main(string[] args)
        {
            if ((args.Length < 1) || (args.Length > 3))
            {
                PrintError("ERROR: Please set at least MAC address");
                return;
            }

            WOL wol = new WOL(string.Empty);
            
            if (args.Length >= 1)
            {
                wol.Mac = args[0];
                if (!Regex.IsMatch(wol.Mac, @"(([a-f]|[0-9]|[A-F]){2}){6}\b"))
                {
                    PrintError("ERROR: Invalid MAC address");
                    return;
                }
            }

            if (args.Length >= 2)
            {
                wol.Ip = args[1];
                if (!Regex.IsMatch(wol.Ip, @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$\b"))
                {
                    PrintError("ERROR: Invalid IP address");
                    return;
                }
            }

            if (args.Length == 3)
            {
                int n;
                if ((!int.TryParse(args[2], out n)) || (n < 0) || (n > 65535))
                {
                    PrintError("ERROR: Invalid port");
                    return;
                }

                wol.Port = n;
            }

            wol.WakeUp();
        }
    }
}

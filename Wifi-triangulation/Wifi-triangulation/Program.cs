
using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Runtime.InteropServices;



namespace ConsoleApp1

{

    internal class Program

    {

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]

        public static extern int system(string format);

        static void Main(string[] args)

        {

            string cmd = "netsh wlan show networks mode = bssid";//komut satırı veya powershell için komut

            system(cmd);



        }

    }

}

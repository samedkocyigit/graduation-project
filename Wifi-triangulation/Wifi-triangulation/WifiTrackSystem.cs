
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    public struct objectPositions
    {
        public float x, y;

        public objectPositions(float firstVal, float secVal)
        {
            x=firstVal; y=secVal;
        }
    }; 

    public class Program
    {
        //[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]

        //public static extern int system(string format);
        static float averageDistanceFromFirstAccessPoint() { return 0f; }
        static float averageDistanceFromSecAccessPoint() { return 0f; }
        static float averageDistanceFromThirdAccessPoint() { return 0f; }
        static int calculateUserPosition() { return 0; }
        static void Main(string[] args)
        {
            objectPositions firstAccessP = new objectPositions(0f, 600f);
            objectPositions secAccessP = new objectPositions(0f, 0f);
            objectPositions thirdAccessP = new objectPositions(600f, 0f);
            objectPositions userPosition=  new objectPositions();
            float distanceOne = averageDistanceFromFirstAccessPoint();
            float distanceSec=averageDistanceFromSecAccessPoint();
            float distanceThird=averageDistanceFromThirdAccessPoint();
        }     
    }
        //string cmd = "netsh wlan show networks mode = bssid";//komut satırı veya powershell için komut
        //system(cmd);
}



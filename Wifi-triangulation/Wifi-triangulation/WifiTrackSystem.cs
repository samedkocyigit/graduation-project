
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
        public double x, y,signal;

        public objectPositions(double firstVal, double secVal,double signalStrenght)
        {
            x=firstVal; y = secVal; signal=signalStrenght;
        }
    }; 

    public class Program
    {
        //[DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]

        //public static extern int system(string format);
        static double averageSignalStrenghtFromFirstAccessPoint() { return 0f; }
        static double averageSignalStrenghtFromSecAccessPoint() { return 0f; }
        static double averageSignalStrenghtFromThirdAccessPoint() { return 0f; }
        private static void calculateUserPosition(objectPositions accessP1, objectPositions accessP2, objectPositions accessP3, double distanceOne, double distanceSec, double distanceThird)
        {
            objectPositions userPosition = new objectPositions();

        }
        private static void GetUserLocation(objectPositions firstAP, objectPositions secAP, objectPositions thirdAP)
        {
            // Calculate the distance between the user and each modem
            double distance1 = CalculateDistanceFromSignalStrength(firstAP.signal);
            double distance2 = CalculateDistanceFromSignalStrength(secAP.signal);
            double distance3 = CalculateDistanceFromSignalStrength(thirdAP.signal);

            // Calculate the intersection point of the 3 circles formed by the user's distance from each modem
            calculateUserPosition(firstAP, secAP, thirdAP, distance1, distance2, distance3);
        }
        private static double CalculateDistanceFromSignalStrength(double signalStrength)
        {
            double Ptx = 0.1;  // transmit power of the Wi-Fi modem in Watts
            double Gtx = 2.0;  // transmit antenna gain of the Wi-Fi modem in dBi
            double Grx = 2.0;  // receive antenna gain of the Wi-Fi modem in dBi
            double f = (2.4f * Math.Pow(10, 9));  // frequency of the Wi-Fi signal in Hz
            double lambda = 3.0 * Math.Pow(10, 8) / f;  // wavelength of the Wi-Fi signal in meters
            double L = 1.0;  // system loss factor in dB
            double signalLoss = L + 20 * Math.Log10(lambda / (4 * Math.PI)) + Gtx + Grx - signalStrength;  // signal loss in dB
            double distance = Math.Pow(10, signalLoss / 20) * Math.Sqrt(Ptx);  // distance between the modem and user in meters

            return distance;
        }
        static void Main(string[] args)
        {
            double signalFirst = averageSignalStrenghtFromFirstAccessPoint();   //distance from first access point between user location
            double signalSec = averageSignalStrenghtFromSecAccessPoint();     //distance from second access point between user location
            double signalThird = averageSignalStrenghtFromThirdAccessPoint(); //distance from third access point between user location
            objectPositions firstAccessP = new objectPositions(0.0, 600.0, signalFirst);
            objectPositions secAccessP = new objectPositions(0.0, 0.0, signalSec);
            objectPositions thirdAccessP = new objectPositions(600.0, 0.0, signalThird);

            GetUserLocation(firstAccessP,secAccessP,thirdAccessP);

            
        }

    }
        //string cmd = "netsh wlan show networks mode = bssid";//komut satırı veya powershell için komut
        //system(cmd);
}



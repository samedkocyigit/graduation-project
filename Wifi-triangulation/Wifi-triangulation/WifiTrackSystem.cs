
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

        public objectPositions(double firstVal, double secVal, double signalStrenght)
        {
            x = firstVal; y = secVal; signal = signalStrenght;
        }
    }; 

    public class Program
    {
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]

        public static extern int system(string format);
        public static double PercentageToDbm(double percentage) //Convertion for a signal percentage to dbm 
        {
            if (percentage <= 0)
            {
                return -100.0;
            }
            else if (percentage >= 100)
            {
                return -50.0;
            }
            else
            {
                return (percentage / 2) - 100.0;
            }
        }
        private static double signalStrenghtFromFirstAccessPoint()
        {
            string filePath = @"C:\Users\samed\Desktop\ConsoleApp1\Test.txt";

            StreamReader streamReader = new StreamReader(filePath);

            double signalStrenght1;

            string templateString;
            string wanted = "TP-LINK_1074";
            int index = -1;
            int lineCount = 0;
            while (!streamReader.EndOfStream)
            {
                string wantedLine = streamReader.ReadLine();
                lineCount++;
                if (wantedLine.Contains(wanted))
                {
                    index = lineCount - 1; // keep the line index
                }
            }

            // access to after index fifth line
            string sonrakiDorduncuSatir = File.ReadLines(filePath).Skip(index + 5).First();

            templateString = sonrakiDorduncuSatir.Replace("Signal", "");
            templateString = templateString.Replace(" ", "");
            templateString = templateString.Replace(":", "");
            templateString = templateString.Replace("%", "");

            signalStrenght1 = double.Parse(templateString);
            signalStrenght1 = PercentageToDbm(signalStrenght1);

            return signalStrenght1;
        }
        private static double signalStrenghtFromSecAccessPoint()
        {
            string filePath = @"C:\Users\samed\Desktop\ConsoleApp1\Test.txt";

            StreamReader streamReader = new StreamReader(filePath);
            
            double signalStrenght2;
                
            string templateString;
            string wanted = "SUPERONLINE-WiFi_8853";
            int index = -1;
            int lineCount = 0;
            while (!streamReader.EndOfStream)
            {
                string wantedLine = streamReader.ReadLine();
                lineCount++;
                if (wantedLine.Contains(wanted))
                {
                    index = lineCount - 1; // keep the line index
                }
            }
            
              // access to after index fifth line
            string sonrakiDorduncuSatir = File.ReadLines(filePath).Skip(index + 5).First();
                
            templateString = sonrakiDorduncuSatir.Replace("Signal", "");
            templateString = templateString.Replace(" ", "");
            templateString = templateString.Replace(":", "");
            templateString = templateString.Replace("%", "");

            signalStrenght2 = double.Parse(templateString);
            signalStrenght2 = PercentageToDbm(signalStrenght2);

            return signalStrenght2;
        }
        private static double signalStrenghtFromThirdAccessPoint()
        {
            string filePath = @"C:\Users\samed\Desktop\ConsoleApp1\Test.txt";

            StreamReader streamReader = new StreamReader(filePath);

            double signalStrenght3;

            string templateString;
            string wanted = "ibbWiFi";
            int index = -1;
            int lineCount = 0;
            while (!streamReader.EndOfStream)
            {
                string wantedLine = streamReader.ReadLine();
                lineCount++;
                if (wantedLine.Contains(wanted))
                {
                    index = lineCount - 1; // keep the line index
                }
            }
            // access to after index fifth line
            string sonrakiDorduncuSatir = File.ReadLines(filePath).Skip(index + 5).First();

            templateString = sonrakiDorduncuSatir.Replace("Signal", "");
            templateString = templateString.Replace(" ", "");
            templateString = templateString.Replace(":", "");
            templateString = templateString.Replace("%", "");

            signalStrenght3 = double.Parse(templateString);
            signalStrenght3 = PercentageToDbm(signalStrenght3);

            return signalStrenght3;
        }
        private static void calculateUserPositionWithTrileteration(objectPositions accessP1, objectPositions accessP2, objectPositions accessP3, double distanceOne, double distanceSec, double distanceThird)
        { 
            objectPositions userPosition = new objectPositions();
            double A = (2 * accessP2.x) - (2 * accessP1.x);
            double B = (2 * accessP2.y) - (2 * accessP1.y);
            double C = Math.Pow(distanceOne,2) - Math.Pow(distanceSec,2) -Math.Pow(accessP1.x,2) + Math.Pow(accessP2.x, 2) - Math.Pow(accessP1.y, 2) + Math.Pow(accessP2.y, 2);
            double D = (2 * accessP3.x) - (2 * accessP2.x);
            double E = (2 * accessP3.y) - (2 * accessP2.y);
            double F = Math.Pow(distanceSec, 2) - Math.Pow(distanceThird, 2) - Math.Pow(accessP2.x, 2) + Math.Pow(accessP3.x, 2) - Math.Pow(accessP2.y, 2) + Math.Pow(accessP3.y, 2);
            userPosition.x = (C * E - F * B) / (E * A - B * D);
            userPosition.y = (C * D - F * A) / (B * D - A * E);
            userPosition.signal =0;  
            Console.WriteLine("User Estimate Position x ={0} y={1}" ,userPosition.x,userPosition.y);
        }
        private static void GetUserLocation(objectPositions firstAP, objectPositions secAP, objectPositions thirdAP)
        {
            // Calculate the distance between the user and each modem
            double distance1 = CalculateDistanceFromSignalStrength(firstAP.signal);
            double distance2 = CalculateDistanceFromSignalStrength(secAP.signal);
            double distance3 = CalculateDistanceFromSignalStrength(thirdAP.signal);

            // Calculate the intersection point of the 3 circles formed by the user's distance from each modem
            calculateUserPositionWithTrileteration(firstAP, secAP, thirdAP, distance1, distance2, distance3);
        }
        private static double CalculateDistanceFromSignalStrength(double signalStrength)
        {
            double Ptx = 0.1;  // transmit power of the Wi-Fi modem in Watts
            double Gtx = 2.0;  // transmit antenna gain of the Wi-Fi modem in dBi
            double Grx = 2.0;  // receive antenna gain of the Wi-Fi modem in dBi
            double f = (2.4f * Math.Pow(10, 9));  // frequency of the Wi-Fi signal in Hz
            double lambda = 3.0 * Math.Pow(10, 8) / f;  // wavelength of the Wi-Fi signal in meters
            lambda *= 100;   //convert to centimeters
            double L = 1.0;  // system loss factor in dB
            double signalLoss = L + 20 * Math.Log10(lambda / (4 * Math.PI)) + Gtx + Grx - signalStrength;  // signal loss in dB
            double distance = Math.Pow(10, signalLoss / 20) * Math.Sqrt(Ptx);  // distance between the modem and user in meters
            distance *= 100; //convert to centimeter

            return distance;
        }
        static void Main(string[] args)
        {
            double signalFirst = signalStrenghtFromFirstAccessPoint();   //signal from first access point between user location
            double signalSec = signalStrenghtFromSecAccessPoint();     //signal from second access point between user location
            double signalThird = signalStrenghtFromThirdAccessPoint(); //signal from third access point between user location
            objectPositions firstAccessP = new objectPositions(0.0, 0.0, signalFirst);
            objectPositions secAccessP = new objectPositions(0.0, 600.0, signalSec);
            objectPositions thirdAccessP = new objectPositions(600.0, 0.0, signalThird);

            GetUserLocation(firstAccessP,secAccessP,thirdAccessP);
        }
    }
}



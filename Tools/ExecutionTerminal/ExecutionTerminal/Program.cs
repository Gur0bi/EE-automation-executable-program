using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;


namespace ExecutionTerminal
{
    class Program
    {
        static void Main()
        {
            Phase1();                
        }

        public static void Phase1()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 1; i <= 58; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("\n**\t\tICARUS Automation Scripts\t\t**");
            Console.WriteLine("**\t\t\t\t\t\t\t**");
            Console.WriteLine("**\t\t\t\t" + DateTime.Now.ToString() + "\t**");
            for (int i = 1; i <= 58; i++)
            {
                Console.Write("*");
            }

            Console.ResetColor();
            Console.WriteLine("\n**   1. Regression 1\t\t\t\t\t**");
            Console.WriteLine("**   2. Regression 2\t\t\t\t\t**");
            Console.WriteLine("**   3. Platform\t\t\t\t\t**");
            Console.WriteLine("**   4. Forms\t\t\t\t\t\t**");
            for (int i = 1; i <= 58; i++)
            {
                Console.Write("*");
            }

            Console.WriteLine("\n[-] Please type the Application Number to check:");
            string sApplication;
            sApplication = Console.ReadLine();

            string sScript = sApplication;
            string sBatpath = @"C:\EE_4NET\Tools\Personal_Folder.bat";
            string importExcelPath = @"C:\EE_4NET\EE_4NET\OrderTest\Regression_1.csv";
            Program obj = new Program();
            Thread thread = new Thread(() => obj.Method1(sScript, sBatpath, importExcelPath));
            thread.Start();
        }

        public void Method1(string sScript, string sBatpath, string importExcelPath, string QEname = "Jiacheng Wang")
        {
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorFore = Console.ForegroundColor;

            int iTotal = 50;
            string path = @"\\shexablox01\Public\Jiacheng Wang\EE Automation Channel\Regression 1\TestReport";

            // Initial                
            var files = Directory.GetFiles(path, "*.txt");
            int iWork = files.Length;
            Console.WriteLine("[Auto Message]: " + DateTime.Now.ToString() + " [" + sScript + " Total]: " + iTotal);

            Console.WriteLine("Loading...");
            double prePercent = 0;
            Thread.Sleep(5000);

            Console.WriteLine("********************* Loading *********************");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; ++i <= 50;)
            {
                Console.Write(" ");
            }
            Console.WriteLine(" ");
            Console.BackgroundColor = colorBack;
            Console.WriteLine("0%");
            Console.WriteLine("***************************************************");

            double percent;
            if (iWork <= iTotal)
            {
                percent = (double)iWork / iTotal;
                percent = Math.Ceiling(percent * 100);
            }
            else
            {
                percent = 1;
                percent = Math.Ceiling(percent * 100);
            }

            for (int i = Convert.ToInt32(prePercent); i <= percent; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;                
                Console.SetCursorPosition(i / 2, 15);                
                Console.Write(" ");               
                Console.BackgroundColor = colorBack;                               
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 16);
                Console.Write("{0}%", i);
                Console.ForegroundColor = colorFore;
                Thread.Sleep(50);
            }
            Thread.CurrentThread.Join(60000);

            // Loop
            while (true)
            {
                files = Directory.GetFiles(path, "*.txt");
                iWork = files.Length;

                if (iWork <= iTotal)
                {
                    percent = (double)iWork / iTotal;
                    percent = Math.Ceiling(percent * 100);
                }
                else
                {
                    percent = 1;
                    percent = Math.Ceiling(percent * 100);
                }

                int ii = Convert.ToInt32(percent);
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(ii / 2, 15);
                Console.Write(" ");
                Console.BackgroundColor = colorBack;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 16);
                Console.Write("{0}%", ii);
                Console.ForegroundColor = colorFore;
                Thread.Sleep(50);
                if (iWork == iTotal)
                {
                    Console.SetCursorPosition(0, 18);
                    Console.WriteLine("Running Complete.");
                    break;
                }
                Thread.CurrentThread.Join(60000);
            }         
        }
    }
}

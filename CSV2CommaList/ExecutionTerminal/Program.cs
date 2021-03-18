using System;
using System.Data;


namespace ExecutionTerminal
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Phase1();
            }       
        }

        public static void Phase1()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 1; i <= 58; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine("\n**\t\tCSV to Comma separated list\t\t**");
            Console.WriteLine("**\t\t\t\t\t\t\t**");
            Console.WriteLine("**\t\t\t\t" + DateTime.Now.ToString() + "\t**");
            for (int i = 1; i <= 58; i++)
            {
                Console.Write("*");
            }


            string sCurrentDirectory = Environment.CurrentDirectory;
            Console.ResetColor();
            Console.WriteLine("\n[-] Type the csv file name (do not include extension)");
            string sCSVname = Console.ReadLine();

            Console.WriteLine("\n[-] Confirm the csv file path:");
            string sCSVPath = sCurrentDirectory + "\\" + sCSVname + ".csv";
            Console.WriteLine(sCSVPath);

            
            DataTable BigTable = Frame.ReadCSV(@sCSVPath);
            int iTotal = BigTable.Rows.Count;
            object oUnit;
            string sOutput = "";
            for ( int i=1; i<(iTotal-1)/2; i++ )
            {
                oUnit = BigTable.Rows[1+2*(i-1)][0];
                sOutput = sOutput + "," + oUnit;
            }

            Console.WriteLine(sOutput.TrimStart(',') + "\n");
            
        }
    }
}

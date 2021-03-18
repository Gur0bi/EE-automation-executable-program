using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultTool
{
    class Program
    {
        static void Main()
        {
            string failcsvpath = @"C:\EE_4NET\TestResults\TestResult_Forms_COMP_Kbase_IP_GenerateForm_03-37-01.csv";

            // Load ordertest(.csv) file
            DataTable m = ReadCSV(failcsvpath);
            List<string> strOrdertest = new List<string>();
            foreach (DataRow row in m.Rows)
            {
                string n = row[0].ToString().Replace("Kbase_IP_GenerateForm_", "");
                strOrdertest.Add(n);
                Console.WriteLine(n);
            }

            // Create ordertest(.csv) file for rerun
            string csvoutputpath = @"C:\EE_4NET\EE_4NET\OrderTest\aaaRerun_Forms_COMP_Kbase_IP_GenerateForm.csv"; 
            StreamWriter sw = new StreamWriter(csvoutputpath, false, Encoding.Default);
            foreach (string p in strOrdertest)
            {
                string q = $"Kbase_IP_GenerateForm_{p}";
                sw.WriteLine(q);
            }
            sw.Close();

            // Create batch(.bat) file for rerun
            string newcsvname = "aaaRerun_Forms_COMP_Kbase_IP_GenerateForm.csv";
            string fileContent1 = "@echo off";
            string fileContent2 = "echo executing " + newcsvname;
            string fileContent3_1 = "start \"\" C:\\EE_4NET\\Execution\\ExecutionOrderedTestCases.exe EE_4NET \"C:\\EE_4NET\\EE_4NET\\OrderTest\\";
            string fileContent3_2 = "\" \"\" false xiaoqing.pei@aspentech.com xiaoqing.pei@aspentech.com 123456";
            string fileContent3 = fileContent3_1 + newcsvname + fileContent3_2;
            string fileContent4 = "echo executing " + newcsvname + " finished";
            string fileContent5 = "pause";
            string batoutputpath = @"C:\EE_4NET\Execution\Forms\COMP\Kbase_IP\aaaRerun_Forms_COMP_Kbase_IP_GenerateForm_Execute.bat";
            if (!File.Exists(batoutputpath))
            {
                FileStream fs1 = new FileStream(batoutputpath, FileMode.Create, FileAccess.Write);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.WriteLine(fileContent1);
                sw1.WriteLine(fileContent2);
                sw1.WriteLine(fileContent3);
                sw1.WriteLine(fileContent4);
                sw1.WriteLine(fileContent5);
                sw1.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs2 = new FileStream(batoutputpath, FileMode.Open, FileAccess.Write);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.WriteLine(fileContent1);
                sw2.WriteLine(fileContent2);
                sw2.WriteLine(fileContent3);
                sw2.WriteLine(fileContent4);
                sw2.WriteLine(fileContent5);
                sw2.Close();
                fs2.Close();

            }


        }

        public static DataTable ReadCSV(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string filepath = fileInfo.DirectoryName;
            string filename = fileInfo.Name;
            DataTable dt = new DataTable();
            if (filename.Trim().ToUpper().EndsWith("CSV"))
            {
                string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties='text;HDR=NO;FMT=Delimited'";
                string commandText = "select * from [" + filename + "]";
                OleDbConnection olconn = new OleDbConnection(connStr);
                olconn.Open();
                OleDbDataAdapter odp = new OleDbDataAdapter(commandText, olconn);
                odp.Fill(dt);
                olconn.Close();
                odp.Dispose();
                olconn.Dispose();
            }
            return dt;
        }
    }
}

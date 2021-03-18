using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;



namespace ExecutionTerminal
{
    class Frame
    {
        public static void RunBat(string filename)
        {
            Process pro = new Process();
            FileInfo file = new FileInfo(filename);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
            pro.StartInfo.FileName = filename;
            pro.StartInfo.CreateNoWindow = false;
            pro.Start();
            pro.WaitForExit();
        }

        public static void DeleteDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(i.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }


        public static void CopyFolder(string strFromPath, string strToPath)
        {
            if (!Directory.Exists(strFromPath))
            {
                Directory.CreateDirectory(strFromPath);
            }
            string strFolderName = strFromPath.Substring(strFromPath.LastIndexOf("\\") + 1, strFromPath.Length - strFromPath.LastIndexOf("\\") - 1);
            if (!Directory.Exists(strToPath + "\\" + strFolderName))
            {
                Directory.CreateDirectory(strToPath + "\\" + strFolderName);
            }
            string[] strFiles = Directory.GetFiles(strFromPath);
            for (int i = 0; i < strFiles.Length; i++)
            {
                string strFileName = strFiles[i].Substring(strFiles[i].LastIndexOf("\\") + 1, strFiles[i].Length - strFiles[i].LastIndexOf("\\") - 1);
                File.Copy(strFiles[i], strToPath + "\\" + strFolderName + "\\" + strFileName, true);
            }
            DirectoryInfo dirInfo = new DirectoryInfo(strFromPath);
            DirectoryInfo[] ZiPath = dirInfo.GetDirectories();
            for (int j = 0; j < ZiPath.Length; j++)
            {
                string strZiPath = strFromPath + "\\" + ZiPath[j].ToString();
                CopyFolder(strZiPath, strToPath + "\\" + strFolderName);
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

        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MidStrEx Err:" + ex.Message);
            }
            return result;
        }

        public static void WriteDat(string path, string content)
        {
            FileStream myStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryWriter myWriter = new BinaryWriter(myStream);
            myWriter.Write(content);
            myWriter.Close();
            myStream.Close();
        }


        public static string ReadDat(string path)
        {
            FileStream myStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader myReader = new BinaryReader(myStream);
            if (myReader.PeekChar() != -1)
            {
                return Convert.ToString(myReader.ReadString());
            }
            myReader.Close();
            myStream.Close();
            return null;
        }



        public void Writetxt(string path, string content)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Flush();
            sw.Close();
            fs.Close();
        }



    }

}


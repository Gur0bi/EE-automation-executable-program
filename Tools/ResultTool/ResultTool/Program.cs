using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorFore = Console.ForegroundColor;

            //Console.SetCursorPosition(0, 0);
            Console.WriteLine("Loading...");
            ////绘制界面
            //Console.SetCursorPosition(0, 2);//指定输出位置
            //Console.WriteLine("*************** Acknowledged order ***************");
            //Console.BackgroundColor = ConsoleColor.DarkCyan;
            //for (int i = 0; ++i <= 50; )
            //{
            //    Console.Write(" ");
            //}
            //Console.SetCursorPosition(0, 3);
            //Console.WriteLine(" ");
            //Console.BackgroundColor = colorBack;
            //Console.SetCursorPosition(0, 4);
            //Console.WriteLine("0%");
            //Console.SetCursorPosition(0, 5);
            //Console.WriteLine("**************************************************");

            int count = 0;
            int index = 0;
            double prePercent = 0;

            List<string> list = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            count = list.Count;
            //Console.SetCursorPosition(0, 1);
            //Console.WriteLine("Total:" + count);
            //count = 0;
            System.Threading.Thread.Sleep(5000);//模拟加载等待
            if (count > 0)
            {
                //Console.SetCursorPosition(0, 1);
                Console.WriteLine("Total:" + count);
                //绘制界面
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

                foreach (string str in list)
                {
                    #region 绘制界面
                    //绘制界面
                    index++;
                    double percent;
                    if (index <= count)
                    {
                        percent = (double)index / count;
                        percent = Math.Ceiling(percent * 100);
                    }
                    else
                    {
                        percent = 1;
                        percent = Math.Ceiling(percent * 100);
                    }
                    // 开始控制进度条和进度变化
                    for (int i = Convert.ToInt32(prePercent); i <= percent; i++)
                    {
                        //绘制进度条进度   
                        Console.WriteLine(i);
                        Console.BackgroundColor = ConsoleColor.Yellow;//设置进度条颜色                
                        Console.SetCursorPosition(i / 2, 3);//设置光标位置,参数为第几列和第几行                
                        Console.Write(" ");//移动进度条                
                        Console.BackgroundColor = colorBack;//恢复输出颜色                
                        //更新进度百分比,原理同上.                
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(0, 4);
                        Console.Write("{0}%", i);
                        Console.ForegroundColor = colorFore;
                        //模拟实际工作中的延迟,否则进度太快
                        System.Threading.Thread.Sleep(50);
                    }
                    prePercent = percent;
                    #endregion
                }

                Console.SetCursorPosition(0, 6);

                Console.WriteLine("Loading Complete.");
            }
            Console.ReadLine();//会等待直到用户按下回车，一次读入一行
        }
    }
}
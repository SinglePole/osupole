// See https://aka.ms/new-console-template for more information
using System;

namespace osuPole
{
    class Program
    {
        ///<summary>
        ///主程序入口
        ///</summary>
        static void Main(string[] args)
        {
            //Welcome!
            Console.WriteLine("osu!pole Version:" + @const.ver);
            Console.WriteLine("osuApi Version:" + ApiParsing.apiver);
            Console.WriteLine("@SinglePole 2022\n");
            PoleConsole.WriteLog("待输入...", -1);
            while (true)
            {
                PoleConsole.StartConsole();
            }
        }
    }
}

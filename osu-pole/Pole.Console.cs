using System;
using System.IO;
using static osuApi;

namespace osuPole
{
    interface PoleConsole
    {
        ///<summary>
        /// 將日誌輸出至控制台, level -1=提示, level 0=日志, level 1=警告, level 2=错误, level 3=终止(按任意键结束进程)
        ///</summary>
        public static void WriteLog(string log, int level = 0)
        {
            switch (level)
            {
                case -1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[" + DateTime.Now.ToString() + "] [提示]:" + log);
                    Console.ResetColor();
                break;
                case 0:
                    Console.WriteLine("[" + DateTime.Now.ToString() + "] [日志]:" + log);
                break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("[" + DateTime.Now.ToString() + "] [警告]:" + log);
                    Console.ResetColor();
                break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[" + DateTime.Now.ToString() + "] [错误]:" + log);
                    Console.ResetColor();
                break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("[" +DateTime.Now.ToString() + "] [终止]:" + log);
                    Console.Beep();
                    Console.ResetColor();
                    Console.WriteLine("進程已被終止，按任意鍵結束...", 1);
                    Console.ReadKey();
                    System.Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(DateTime.Now.ToString() + " [日志]:" + log);
                break;
            }
        }
        ///<summary>
        /// 獲取控制台中的輸入值
        ///</summary>
        public static string ReadLine(string text = null)
        {
            if (text == null)
            {
                Console.WriteLine("鍵入一個值:");
                return Console.ReadLine();
            }
            else
            {
                Console.WriteLine(text);
                return Console.ReadLine();
            }

        }
        ///<summary>
        /// 默認控制台命令
        ///</summary>
        public static void StartConsole()
        {
            string apikey = null;
            string respath = null;
            string outpath = null;
            string user_id = null;
            while (true) 
            {
                string input = PoleConsole.ReadLine("");
                switch (input)
                {
                    case "setapikey":
                        {
                            apikey = PoleConsole.ReadLine("鍵入一個Apikey");
                            PoleConsole.WriteLog("已執行");
                        }
                        break;
                    case "setrespath":
                        {
                            respath = PoleConsole.ReadLine("鍵入資源路徑");
                            PoleConsole.WriteLog("資源路徑已設定 " + respath);
                        }
                        break;
                    case "setoutpath":
                        {
                            outpath = PoleConsole.ReadLine("鍵入輸出路徑");
                            PoleConsole.WriteLog("輸出路徑已設定 " + outpath);
                        }
                        break;
                    case "exit":
                        {
                            Environment.Exit(0);
                        }
                        break;
                    case "setuser":
                        {
                            PoleConsole.WriteLog("鍵入一個user_id");
                            user_id = Console.ReadLine();
                            PoleConsole.WriteLog("user_id 已設定為 " + user_id);
                        }
                        break;
                    case "default":
                        {
                            respath = @"D:\osu-pole\res";
                            outpath = @"D:\osu-pole\out";
                            PoleConsole.WriteLog("應用默認配置");
                        }
                        break;
                    case "test":
                        {
                            DebugManager.ProfileTest();
                        }
                        break;
                    case "drawprofile":
                        {
                            if (apikey == null)
                            {
                                PoleConsole.WriteLog("apikey未設定");
                                break;
                            }
                            if (respath == null)
                            {
                                PoleConsole.WriteLog("資源路徑未設定");
                                break;
                            }
                            if (outpath == null)
                            {
                                PoleConsole.WriteLog("輸出路徑未設定");
                                break;
                            }
                            if (user_id == null)
                            {
                                PoleConsole.WriteLog("請鍵入一個user_id");
                                user_id = Console.ReadLine();
                            }
                            dataPretreat.drawprofile_initialize(user_id, apikey, respath, outpath);
                        }
                        break;
                    default:
                        {
                            PoleConsole.WriteLog("未識別的指令.");
                        }
                        break;
                }
            }
        }
    }
}

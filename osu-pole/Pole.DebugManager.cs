using System.IO;
using static osuApi;

namespace osuPole
{
    class DebugManager
    {
        public static void ProfileTest()
        {
            string respath = @"D:\osupole-main\res";
            string outpath = @"D:\osupole-main\out";
            string user_id = "peppy";
            string apikey = "";
            PoleConsole.WriteLog("正在處理" + user_id + "的Profile...");
            Api_userData udata = new Api_userData();
            ProfileMappingInfo PaintData = new ProfileMappingInfo();
            string uJson = tools.HttpToString(@"https://osu.ppy.sh/api/get_user?k=" + apikey + @"&u=" + user_id + @"&m=0");
            if (uJson == "[]")
            {
                PoleConsole.WriteLog("用戶 " + user_id + " 不存在。", 1);
                return;
            }
            if (uJson.StartsWith("["))
            {
                uJson = uJson.Substring(1, uJson.Length - 1);
            }
            if (uJson.EndsWith("]"))
            {
                uJson = uJson.Substring(0, uJson.Length - 1);
            }
            udata = ApiParsing.userApiParsing(uJson, udata);
            if (string.IsNullOrEmpty(udata.user_id) == false)
            {
                dataPretreat.Api_UserData_Pretreat(udata);
                tools.outputApinfo(udata);
                if (Directory.Exists(respath + @"\avatar\" + udata.user_id + ".jpg") == false)
                {
                    tools.HttpDownload("https://a.ppy.sh/" + udata.user_id, respath + @"\avatar\" + udata.user_id + ".jpg");
                }
                if (Directory.Exists(respath + @"\flags\" + udata.country + ".png") == false)
                {
                    tools.HttpDownload("https://assets.ppy.sh/old-flags/" + udata.country + ".png", respath + @"\flags\" + udata.country + ".png");
                }
                string filename = Painter.draw_profile(udata, respath, outpath);
                if (filename == "0")
                {
                    PoleConsole.WriteLog(user_id + "的Profile輸出失敗.", 2);
                }
                else
                {
                    PoleConsole.WriteLog(user_id + "的Profile已輸出到 " + filename);
                }
            }
            else
            {
                PoleConsole.WriteLog(user_id + "的Profile輸出失敗, 可能是Json獲取失敗或損壞, 請檢查網絡配置.", 2);
            }
        }
    }
}

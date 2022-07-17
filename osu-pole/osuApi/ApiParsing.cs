using LitJson;
using osuPole;
using System;
using static osuApi;
interface ApiParsing
    {
        public const string apiver = "1.0";
        public static Api_userData userApiParsing(string Json, Api_userData apinfo)
        {
            if (Json == "")
            {
                return apinfo;
            }
            else
            {
                try
                {
                    PoleConsole.WriteLog(Json);
                    apinfo = JsonMapper.ToObject<Api_userData>(Json);
                    return apinfo;
                }
                catch(Exception e)
                {
                    PoleConsole.WriteLog("Json解析失败! BaseException: " + "\n" + e.GetBaseException(),1);
                    apinfo = new Api_userData();
                    return apinfo;
                }

            }
        }
        public static void recordApiParsing(string Json, api_record apinfo)
        {
            if (Json == "")
            {
                apinfo.isNull = true;
            }
            else
            {
                apinfo.isNull = false;
            }
        }
    }

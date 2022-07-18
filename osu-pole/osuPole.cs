using System;
using System.Net;
using System.IO;
using static osuApi;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Linq;

namespace osuPole
{
    /// <summary>
    /// 用戶數據預處理
    /// </summary>
    interface dataPretreat
    {
        public static ProfileMappingInfo profile_data(userPlugins userPlugins, string respath)
        {
            if (Directory.Exists(respath + @"\avatar\" + userPlugins.id + ".png") == false)
            {
                tools.HttpDownload("https://a.ppy.sh/" + userPlugins.id, respath + @"\avatar\" + userPlugins.id + ".png");
            }

            ProfileMappingInfo PaintData = new ProfileMappingInfo();
            //面板語言
            switch (userPlugins.language)
            {
                case "cns":
                    PaintData.Label_1 = @const.cns_text_profile;
                    break;
                case "cnt":
                    PaintData.Label_1 = @const.cnt_text_profile;
                    break;
                case "en":
                    PaintData.Label_1 = @const.en_text_profile;
                    break;
                default:
                    PaintData.Label_1 = @const.cnt_text_profile;
                    break;
            }
            //面板樣式
            switch (userPlugins.style)
            {
                default:
                    break;
            }
            //背景圖
            switch (userPlugins.background)
            {
                default:
                    break;
            }
            //...
            return PaintData;
        }
        public static void Api_UserData_Pretreat(Api_userData uData)
        {
            int total_hits = Convert.ToInt32(uData.count50) + Convert.ToInt32(uData.count50) + Convert.ToInt32(uData.count300);
            //int gametime_total = Convert.ToInt32(uData.total_seconds_played);
            //int gametime_day = (int)gametime_total / 84000;
            //double gametime_hour_t = (double)((gametime_total / 84000) - gametime_day) * 24;
            //int gametime_hour = (int)gametime_hour_t;
            //double gametime_minute_t = (double)((((gametime_total / 84000) - gametime_day) * 24) - gametime_hour) * 60;
            //int gametime_minute = (int)gametime_minute_t;
            int oneDays = 60 * 60 * 24;
            int oneHours = 60 * 60;
            int oneMins = 60;
            int gametime_day = Convert.ToInt32(uData.total_seconds_played) / oneDays;
            int gametime_hour = Convert.ToInt32(uData.total_seconds_played) % oneDays / oneHours;
            int gametime_min = Convert.ToInt32(uData.total_seconds_played) % oneHours / oneMins;
            int gametime_s = Convert.ToInt32(uData.total_seconds_played) % oneMins;
            uData.total_hits = Convert.ToString(total_hits);
            uData.gametime = Convert.ToString(gametime_day) + "d" + Convert.ToString(gametime_hour) + "h" + Convert.ToString(gametime_min) + "m" + Convert.ToString(gametime_s) + "s";
            uData.ranked_score = tools.UInt64ToFormat(uData.ranked_score);
            uData.total_score = tools.UInt64ToFormat(uData.total_score);
            uData.total_hits = tools.UInt64ToFormat(uData.total_hits);
            uData.playcount = tools.UInt64ToFormat(uData.playcount);
            uData.pp_rank = tools.UInt64ToFormat(uData.pp_rank);
            uData.pp_country_rank = tools.UInt64ToFormat(uData.pp_country_rank);
            uData.accuracy = tools.DoubleToFormat(uData.accuracy);
            uData.accuracy = uData.accuracy + "%";
            uData.pp_raw = tools.DoubleToFormat(uData.pp_raw);
        }
        public static bool drawprofile_initialize(string user_id, string apikey, string respath, string outpath)
        {
            PoleConsole.WriteLog("正在處理" + user_id + "的Profile...");
            Api_userData udata = new Api_userData();
            ProfileMappingInfo PaintData = new ProfileMappingInfo();
            string uJson = tools.HttpToString(@"https://osu.ppy.sh/api/get_user?k=" + apikey + @"&u=" + user_id + @"&m=0");
            if (uJson == "[]")
            {
                PoleConsole.WriteLog("用戶 " + user_id + " 不存在。", 1);
                return false;
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
                    return false;
                }
                else
                {
                    PoleConsole.WriteLog(user_id + "的Profile已輸出至 " + filename);
                    return false;
                }
            }
            else
            {
                PoleConsole.WriteLog(user_id + "的Profile輸出失敗, 可能是Json獲取失敗或損壞, 請檢查網絡配置或apikey是否正確.", 2);
                return false;
            }
            return false;
        }
        public static bool[] mods(string enable_modes)
        {
            //能運行，反正我也看不懂，因為這是從Serbot上直接抄下來的，總之他就是能運行。
            int i = 0;
            bool[] mods = new bool[31];
            if (enable_modes == "")
            {
                i = 0;
            }
            else
            {
                i = int.Parse(enable_modes);
            }
            if (i == 0)
            {
                for (int j = 0; j <= 30; j++)
                {
                    mods[i] = false;
                }
            }
            else
            {
                for (int j = 0; j <= 30; j++)
                {
                    if ((i & 1) == 0)
                    {
                        mods[j] = false;
                    }
                    else
                    {
                        mods[j] = true;
                    }
                    i = i >> 1;
                }
            }
            return mods;
        }
        public static PPoint PPointParsing(string Json, PPoint pp)
        {
            return pp;
        }
    }
    /// <summary>
    /// 執行時工具
    /// </summary>
    interface tools
    {
        private static readonly HttpClient client = new HttpClient();
        public static string UInt64ToFormat(string str)
        {
            UInt64 Ui;
            Ui = Convert.ToUInt64(str);
            str = Ui.ToString("N0");
            return str;
        }
        public static string DoubleToFormat(string str)
        {
            double Do;
            Do = Convert.ToDouble(str);
            str = Math.Round(Do, 2).ToString();
            return str;
        }
        public static void outputApinfo(Api_userData apinfo)
        {
            PoleConsole.WriteLog(apinfo.accuracy);
            PoleConsole.WriteLog(apinfo.count100);
            PoleConsole.WriteLog(apinfo.count300);
            PoleConsole.WriteLog(apinfo.count50);
            PoleConsole.WriteLog(apinfo.country);
            PoleConsole.WriteLog(apinfo.count_rank_a);
            PoleConsole.WriteLog(apinfo.count_rank_s);
            PoleConsole.WriteLog(apinfo.count_rank_sh);
            PoleConsole.WriteLog(apinfo.count_rank_ss);
            PoleConsole.WriteLog(apinfo.count_rank_ssh);
            PoleConsole.WriteLog(apinfo.events.ToString());
            PoleConsole.WriteLog(apinfo.gametime);
            PoleConsole.WriteLog(apinfo.join_date);
            PoleConsole.WriteLog(apinfo.level);
            PoleConsole.WriteLog(apinfo.playcount);
            PoleConsole.WriteLog(apinfo.pp_country_rank);
            PoleConsole.WriteLog(apinfo.pp_rank);
            PoleConsole.WriteLog(apinfo.pp_raw);
            PoleConsole.WriteLog(apinfo.ranked_score);
            PoleConsole.WriteLog(apinfo.total_hits);
            PoleConsole.WriteLog(apinfo.total_score);
            PoleConsole.WriteLog(apinfo.total_seconds_played);
            PoleConsole.WriteLog(apinfo.username);
            PoleConsole.WriteLog(apinfo.user_id);
        }
        public static string HttpToString(string url)
        {
            try
            {
                //創建一個請求對象
                string content = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //超時時間
                request.Timeout = 60000;
                //獲取回寫流
                WebResponse response = request.GetResponse();
                //把網頁對象讀成流
                using (Stream stream = response.GetResponseStream())
                {
                    //用streamreader讀取stream到string
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //讀取到結尾賦值給Content
                        content = reader.ReadToEnd();
                        reader.Close();
                        reader.Dispose();
                    }
                    stream.Close();
                    stream.Dispose();
                }
                response.Close();
                response.Dispose();
                return content;
            }
            catch (WebException ex)
            {
                PoleConsole.WriteLog(ex.GetBaseException().ToString(), 2);
                return "";
            }
            catch (Exception ex)
            {
                PoleConsole.WriteLog(ex.GetBaseException().ToString(), 2);
                return "";
            }
        }
        /// <summary>
        /// 通過Http下載文件
        /// </summary>
        public static bool HttpDownload(string url, string path)
        {
            try
            {

                //設置參數
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //發送請求并獲取相應回應數據
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才開始向目標網頁发發送Post請求
                Stream responseStream = response.GetResponseStream();
                //創建本地文件寫成流
                Stream stream = new FileStream(path, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                return true;
            }
            catch (Exception e)
            {
                PoleConsole.WriteLog(e.GetBaseException().ToString(), 2);
                return false;
            }

        }
        /// <summary>
        /// 通过国家简称获取国家描述（枚举值获取枚举描述）
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        static string GetCountryOneFull(string countryName)
        {

            if(countryName == "")
            {
                return "Unknow";
            }
            try
            {
                var country = (CountryCollections)Enum.Parse(typeof(CountryCollections), countryName);
                var countryFull = (country.GetType().GetField(countryName).GetCustomAttributes(false)[0] as DescriptionAttribute).Description;
                return countryFull;
            }
            catch
            {
                return "Unknow";
            }
        }
        /// <summary>
        /// 通过国家全称获取国家简称(通过枚举描述获取枚举值)
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static string GetCountryOne(string countryName)
        {
            var fileds = typeof(CountryCollections).GetFields();
            //"CountryOne".Equals(x.FieldType.Name)   筛选类型为CountryCollections的枚举值
            //countryName.Equals((x?.GetCustomAttributes(false)[0] as DescriptionAttribute).Description)   筛选描述为countryName的枚举值
            var filed = fileds.FirstOrDefault(x => "CountryOne".Equals(x.FieldType.Name) && countryName.Equals((x?.GetCustomAttributes(false)[0] as DescriptionAttribute).Description));
            return filed.Name;
        }
        public enum CountryCollections
        {
            [Description("Albania")]
            AL,
            [Description("Algeria")]
            DZ,
            [Description("Afghanistan")]
            AF,
            [Description("Argentina")]
            AR,
            [Description("Azerbaijan")]
            AZ,
            [Description("United Arab Emirates")]
            AE,
            [Description("Aruba")]
            AW,
            [Description("Oman")]
            OM,
            [Description("Egypt")]
            EG,
            [Description("Ethiopia")]
            ET,
            [Description("Ireland")]
            IE,
            [Description("Estonia")]
            EE,
            [Description("Andorra")]
            AD,
            [Description("Angola")]
            AO,
            [Description("Angola")]
            AI,
            [Description("Ntigua and Barbuda")]
            AG,
            [Description("Austria")]
            AT,
            [Description("Australia")]
            AU,
            [Description("Barbados")]
            BB,
            [Description("Papua,Territory of")]
            PG,
            [Description("Bahamas")]
            BS,
            [Description("Pakistan")]
            PK,
            [Description("Paraguay")]
            PY,
            [Description("Bahrain")]
            BH,
            [Description("Panama")]
            PA,
            [Description("Brazil")]
            BR,
            [Description("Belarus")]
            BY,
            [Description("Bermuda")]
            BM,
            [Description("Bulgaria")]
            BG,
            [Description("Benin")]
            BJ,
            [Description("Belgium")]
            BE,
            [Description("Iceland")]
            IS,
            [Description("Puerto Rico")]
            PR,
            [Description("Poland")]
            PL,
            [Description("Bosnia and Herzegovina")]
            BA,
            [Description("Bolivia")]
            BO,
            [Description("Belize")]
            BZ,
            [Description("Botswana")]
            BW,
            [Description("Bhutan")]
            BT,
            [Description("Virgin Islands, U.S.")]
            VI,
            [Description("Virgin Islands, British")]
            VG,
            [Description("Burkina Faso")]
            BF,
            [Description("Burundi")]
            BI,
            [Description("Bouvet Island")]
            BV,
            [Description("North Korea")]
            KP,
            [Description("Equatorial Guinea")]
            GQ,
            [Description("Denmark")]
            DK,
            [Description("Germany")]
            DE,
            [Description("East Timor")]
            TP,
            [Description("Togo")]
            TG,
            [Description("Dominica")]
            DO,
            [Description("Gominica")]
            DM,
            [Description("Russia")]
            RU,
            [Description("Ecuador")]
            EC,
            [Description("France")]
            FR,
            [Description("French Polynesia")]
            PF,
            [Description("French Guiana")]
            GF,
            [Description("French Southern Territoties")]
            TF,
            [Description("Vatican")]
            VA,
            [Description("Philippines")]
            PH,
            [Description("Fiji")]
            FJ,
            [Description("Finland")]
            FI,
            [Description("Cabo Verde")]
            CV,
            [Description("Falkland Islands (Malvinas)")]
            FK,
            [Description("Gambia")]
            GM,
            [Description("Congo")]
            CG,
            [Description("Colombia")]
            CO,
            [Description("Costa Rica")]
            CR,
            [Description("Grenada")]
            GD,
            [Description("Greenland")]
            GL,
            [Description("Georgia")]
            GE,
            [Description("Cuba")]
            CU,
            [Description("Guadeloupe")]
            GP,
            [Description("Guam")]
            GU,
            [Description("Guyana")]
            GY,
            [Description("Kazakstan")]
            KZ,
            [Description("Haiti")]
            HT,
            [Description("Korea")]
            KR,
            [Description("Netherlands")]
            NL,
            [Description("Honduras")]
            HN,
            [Description("Kiribati")]
            KI,
            [Description("Djibouti")]
            DJ,
            [Description("Kyrgyzstan")]
            KG,
            [Description("Guinea")]
            GN,
            [Description("Guinea-Bissau")]
            GW,
            [Description("Canada")]
            CA,
            [Description("Ghana")]
            GH,
            [Description("Gabon")]
            GA,
            [Description("Cambodia")]
            KH,
            [Description("Czech Republic")]
            CZ,
            [Description("Zimbabwe")]
            ZW,
            [Description("Cameroon")]
            CM,
            [Description("Qatar")]
            QA,
            [Description("Cayman Islands")]
            KY,
            [Description("Comoros")]
            KM,
            [Description("kuwait")]
            KW,
            [Description("COCOS Islands")]
            CC,
            [Description("Croatia")]
            HR,
            [Description("Kenya")]
            KE,
            [Description("Cook Island")]
            CK,
            [Description("Latvia")]
            LV,
            [Description("Lesotho")]
            LS,
            [Description("Laos")]
            LA,
            [Description("Lebanon")]
            LB,
            [Description("Lithuania")]
            LT,
            [Description("Liberia")]
            LR,
            [Description("Libya")]
            LY,
            [Description("Liechtenstein")]
            LI,
            [Description("Luxembourg")]
            LU,
            [Description("Rwanda")]
            RW,
            [Description("Romania")]
            RO,
            [Description("Malagasy")]
            MG,
            [Description("Maldives")]
            MV,
            [Description("Malta")]
            MT,
            [Description("Malawi")]
            MW,
            [Description("Malaysia")]
            MY,
            [Description("Mali")]
            ML,
            [Description("Marshall Islands")]
            MH,
            [Description("Mauritius")]
            MU,
            [Description("Mauritania")]
            MR,
            [Description("America")]
            US,
            [Description("Mongolia")]
            MN,
            [Description("Bangladesh")]
            BD,
            [Description("Peru")]
            PE,
            [Description("Micronesia")]
            FM,
            [Description("Burma")]
            MM,
            [Description("Moldova,Republic of")]
            MD,
            [Description("Morocco")]
            MA,
            [Description("Monaco")]
            MC,
            [Description("Mozambique")]
            MZ,
            [Description("Mexico")]
            MX,
            [Description("Namibia")]
            NA,
            [Description("South Africa")]
            ZA,
            [Description("Antarctica")]
            AQ,
            [Description("Yugoslavia")]
            YU,
            [Description("Naura")]
            NR,
            [Description("Nepal")]
            NP,
            [Description("Nicaragua")]
            NI,
            [Description("Niger")]
            NE,
            [Description("Nigeria")]
            NG,
            [Description("Niue")]
            NU,
            [Description("Norway")]
            NO,
            [Description("Palau")]
            PW,
            [Description("Pitcairn Islands")]
            PN,
            [Description("Portugal")]
            PT,
            [Description("Japan")]
            JP,
            [Description("Sweden")]
            SE,
            [Description("Switzerland")]
            CH,
            [Description("El Salvador")]
            SV,
            [Description("Sierra leone")]
            SL,
            [Description("Senegal")]
            SN,
            [Description("Cyprus")]
            CY,
            [Description("Seychelles")]
            SC,
            [Description("Saudi Arabia")]
            SA,
            [Description("Christmas Island")]
            CX,
            [Description("Sao Tome and Principe")]
            ST,
            [Description("St.Helena")]
            SH,
            [Description("St. Lucia")]
            LC,
            [Description("San Marino")]
            SM,
            [Description("Sri Lanka")]
            LK,
            [Description("Slovakia")]
            SK,
            [Description("Slovene")]
            SI,
            [Description("Swaziland")]
            SZ,
            [Description("Sudan")]
            SD,
            [Description("Surinam")]
            SR,
            [Description("USSR(formerly)")]
            SU,
            [Description("Solomon Islands")]
            SB,
            [Description("Somali")]
            SO,
            [Description("Tsjikistan")]
            TJ,
            [Description("Thailand")]
            TH,
            [Description("Tanzania")]
            TZ,
            [Description("Tonga")]
            TO,
            [Description("Trinidad and Tobago")]
            TT,
            [Description("Tunisia")]
            TN,
            [Description("Tuvalu")]
            TV,
            [Description("Turkey")]
            TR,
            [Description("Turkomanstan")]
            TM,
            [Description("Tokela")]
            TK,
            [Description("Guatemala")]
            GT,
            [Description("Venezuela")]
            VE,
            [Description("Brunei Darussalam")]
            BN,
            [Description("Uganda")]
            UG,
            [Description("Ukiain")]
            UA,
            [Description("uruguay")]
            UY,
            [Description("Uzbekstan")]
            UZ,
            [Description("Spain")]
            ES,
            [Description("West Sahara")]
            EH,
            [Description("Western Samoa")]
            WS,
            [Description("Greece")]
            GR,
            [Description("Lvory Coast")]
            CI,
            [Description("Singapore")]
            SG,
            [Description("New Caledonia")]
            NC,
            [Description("New Zealand")]
            NZ,
            [Description("Hungary")]
            HU,
            [Description("Syria")]
            SY,
            [Description("Jamaica")]
            JM,
            [Description("Armenia")]
            AM,
            [Description("Yemen")]
            YE,
            [Description("Iraq")]
            IQ,
            [Description("Iran")]
            IR,
            [Description("Israel")]
            IL,
            [Description("Italy")]
            IT,
            [Description("India")]
            IN,
            [Description("Indonesia")]
            ID,
            [Description("United Kingdom")]
            GB,
            [Description("British Indian Ocean Territory")]
            IO,
            [Description("Jordan")]
            JO,
            [Description("Vietnam")]
            VN,
            [Description("Zambia")]
            ZM,
            [Description("Zaire")]
            ZR,
            [Description("Chad")]
            TD,
            [Description("Gibraltar")]
            GI,
            [Description("Chile")]
            CL,
            [Description("The Central African Republic")]
            CF,
            [Description("China")]
            CN,
            [Description("Macao")]
            MO,
            [Description("Taiwan")]
            TW,
            [Description("Hong Kong")]
            HK,
        }

    }
}
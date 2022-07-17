
namespace osuPole
{
    public class @const
    {
        public const string ver = "0.0.1";
        public const string creator = "SinglePole";
        public const string cnt_text_profile =
            "加入时间" + "\n" +
            "Ranked谱面总分" + "\n" +
            "准确率" + "\n" +
            "游戏次数" + "\n" +
            "总分" + "\n" +
            "总命中次数" + "\n" +
            "游戏时间";
        public const string cns_text_profile = 
            "加入时间" + "\n" +
            "Ranked谱面总分" + "\n" +
            "准确率" + "\n" +
            "游戏次数" + "\n" +
            "总分" + "\n" +
            "总命中次数" + "\n" +
            "游戏时间";
        public const string en_text_profile =
            "Joined" + "\n" +
            "Ranked Score" + "\n" +
            "Hit Accuracy" + "\n" +
            "Play Count" + "\n" +
            "Total Score" + "\n" +
            "Total Hits" + "\n" +
            "Total Play Time";
        enum Mods
        {
            None = 0,
            NoFail = 1,
            Easy = 2,
            TouchDevice = 4,
            Hidden = 8,
            HardRock = 16,
            SuddenDeath = 32,
            DoubleTime = 64,
            Relax = 128,
            HalfTime = 256,
            Nightcore = 512, // Only set along with DoubleTime. i.e: NC only gives 576
            Flashlight = 1024,
            Autoplay = 2048,
            SpunOut = 4096,
            Relax2 = 8192,    // Autopilot
            Perfect = 16384, // Only set along with SuddenDeath. i.e: PF only gives 16416  
            Key4 = 32768,
            Key5 = 65536,
            Key6 = 131072,
            Key7 = 262144,
            Key8 = 524288,
            FadeIn = 1048576,
            Random = 2097152,
            Cinema = 4194304,
            Target = 8388608,
            Key9 = 16777216,
            KeyCoop = 33554432,
            Key1 = 67108864,
            Key3 = 134217728,
            Key2 = 268435456,
            ScoreV2 = 536870912,
            Mirror = 1073741824,
            KeyMod = Key1 | Key2 | Key3 | Key4 | Key5 | Key6 | Key7 | Key8 | Key9 | KeyCoop,
            FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight | FadeIn | Relax | Relax2 | SpunOut | KeyMod,
            ScoreIncreaseMods = Hidden | HardRock | DoubleTime | Flashlight | FadeIn
        }
    }
    public class ProfileMappingInfo
    {
        public string user_id;
        public string username;
        public string join_date;
        public string count300;
        public string count100;
        public string count50;
        public string playcount;
        public string ranked_score;
        public string total_score;
        public string pp_rank;
        public string level;
        public string pp_raw;
        public string accuracy;
        public string count_rank_ss;
        public string count_rank_ssh;
        public string count_rank_s;
        public string count_rank_sh;
        public string count_rank_a;
        public string country;
        public string total_seconds_played;
        public string pp_country_rank;
        //基础文本
        public string Label_1;
    }
    public class userPlugins
    {
        public string id;
        public string style;
        public string language;
        public string background;
    }
}


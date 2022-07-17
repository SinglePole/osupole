using System.Collections.Generic;
public class osuApi
    {
        public class Api_userData
        {
            public string user_id { get; set; }
            public string username { get; set; }
            public string join_date { get; set; }
            public string count300 { get; set; }
            public string count100 { get; set; }
            public string count50 { get; set; }
            public string playcount { get; set; }
            public string ranked_score { get; set; }
            public string total_score { get; set; }
            public string pp_rank { get; set; }
            public string level { get; set; }
            public string pp_raw { get; set; }
            public string accuracy { get; set; }
            public string count_rank_ss { get; set; }
            public string count_rank_ssh { get; set; }
            public string count_rank_s { get; set; }
            public string count_rank_sh { get; set; }
            public string count_rank_a { get; set; }
            public string country { get; set; }
            public string total_seconds_played { get; set; }
            public string pp_country_rank { get; set; }
            public List<EventsItem> events { get; set; }
            //Api拓展
            public string total_hits;
            public string gametime;
        }
        public class api_record
        {
            public bool isNull;
            public string beatmap_id;
            public string score;
            public string maxcombo;
            public string count50;
            public string count100;
            public string count300;
            public string countmiss;
            public string countkatu;
            public string countgeki;
            public string perfect;
            public string enabled_mods;
            public string user_id;
            public string date;
            public string rank;
        }
        public class PPoint
        {
            public bool isNull;
            public string sum;
            public string aim;
            public string speed;
            public string accuracy;
        }
        public class Events
        {
        }
        public class EventsItem
        {
            public string display_html { get; set; }
            public string beatmap_id { get; set; }
            public string beatmapset_id { get; set; }
            public string date { get; set; }
            public string epicfactor { get; set; }
        }
    }
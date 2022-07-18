using System;
using ImageMagick;
using static osuApi;

namespace osuPole
{
    interface Painter
    {
        /// <summary>
        /// 初版Profile面板繪製
        /// </summary>
        /// <param name="user">
        /// 用戶數據
        /// </param>
        /// <param name="respath">
        /// 默認的資源路徑
        /// </param>
        /// <param name="outpath">
        /// 輸出路徑
        /// </param>
        /// <returns>
        /// 返回輸出路徑
        /// </returns>
        public static string draw_profile(Api_userData user, string respath, string outpath)
        {
            try
            {
                using (var image = new MagickImage(respath + @"\resources\template.png"))
                {
                    //置入封面，x=0, y=0, Gravity.North
                    using (MagickImage cover = new MagickImage(respath + @"\resources\cover.jpg"))
                    {
                        image.Composite(cover, Gravity.North, CompositeOperator.Over);
                    }
                    //置入背景圖片,x=0,y=0
                    using (MagickImage background = new MagickImage(respath + @"\resources\background.png"))
                    {
                        image.Composite(background, Gravity.South, CompositeOperator.Over);
                    }
                    //置入頭像，x=70,y=165
                    using (MagickImage reavatar = new MagickImage(respath + @"\resources\re_avatar.png"))
                    {
                        //頭像修正+蒙版裁切
                        using (var avatar = new MagickImage(respath + @"\avatar\" + user.user_id + ".jpg"))
                        {
                            avatar.Resize(new MagickGeometry(270, 270));
                            reavatar.Composite(avatar, Gravity.Center, 0, -22, CompositeOperator.Atop);
                        }
                        image.Composite(reavatar, 40, 165, CompositeOperator.Over);
                    }
                    //置入頭像外環，x=70,y=165
                    using (MagickImage avatar_ring = new MagickImage(respath + @"\resources\avatar_ring.png"))
                    {
                        image.Composite(avatar_ring, 40, 165, CompositeOperator.Over);
                    }
                    //置入國旗圖標
                    {
                    MagickImage i_flags = new MagickImage(respath + @"\flags\" + user.country + @".png");
                    i_flags.Resize(new MagickGeometry(70, 47));
                    image.Composite(i_flags, 380, 315, CompositeOperator.Over);
                    }
                    //置入默認文本
                    {
                        new Drawables()
                            .FontPointSize(48)
                            .Gravity(Gravity.Northwest)
                            .Font(respath + @"\resources\fonts\NotoSansSCRegular-04.ttf")
                            .FillColor(new MagickColor("white"))
                            .Text(50, 600, @const.en_text_profile)
                            .Draw(image);
                    }
                    //置入基礎信息
                    {
                        new Drawables()
                            .FontPointSize(48)
                            .Gravity(Gravity.Northeast)
                            .Font(respath + @"\resources\fonts\NotoSansSCRegular-04.ttf")
                            .FillColor(new MagickColor("white"))
                            .Text(50, 600, user.join_date + "\n" + user.ranked_score + "\n" +
                            user.accuracy + "\n" + user.playcount + "\n" + user.total_score + "\n" + user.total_hits + "\n" + user.gametime)
                            .Draw(image);
                    }
                    //置入用戶名、地区名
                    {
                        new Drawables()
                            .FontPointSize(72)
                            .Gravity(Gravity.Forget)
                            .Font(respath + @"\resources\fonts\Torus-Regular.otf")
                            .FillColor(new MagickColor("white"))
                            .Text(375, 425, user.username)
                            .Draw(image);
                        new Drawables()
                            .FontPointSize(42)
                            .Gravity(Gravity.Forget)
                            .Font(respath + @"\resources\fonts\Torus-Regular.otf")
                            .FillColor(new MagickColor("white"))
                            .Text(455, 355, tools.GetCountryOneFull(user.country))
                            .Draw(image);
                        //new Drawables()
                        //    .FontPointSize(42)
                        //    .Gravity(Gravity.Forget)
                        //    .Font(respath + @"\resources\fonts\Torus-Regular.otf")
                        //    .FillColor(new MagickColor("white"))
                        //    .Text(455, 400, "")
                        //    .Draw(image);
                    }
                    //置入进阶信息
                    {
                        //new Drawables()
                        //    .FontPointSize(48)
                        //    .Gravity(Gravity.Northeast)
                        //    .Font(respath + @"\resources\fonts\Torus-Regular.otf")
                        //    .FillColor(new MagickColor("white"))
                        //    .Text(50, 600, user.join_date + "\n" + user.ranked_score + "\n" +
                        //    user.accuracy + "\n" + user.playcount + "\n" + user.total_score + "\n" + user.total_hits + "\n" + user.gametime)
                        //    .Draw(image);
                    }
                    image.Draw();
                    image.Write(outpath + @"\" + user.user_id + @"_temp.png");
                }
                string filename = outpath + @"\" + user.user_id + @"_temp.png";
                return filename;
            }
            catch (Exception e)
            {
                PoleConsole.WriteLog(e.GetBaseException().ToString (), 2);
                return "0";
            }

        }
        public string draw_record(api_record record)
        {
            string filename = "";
            return filename;
        }
    }
}

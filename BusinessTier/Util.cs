using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;

using DataAccessTier;

namespace BusinessTier
{
    public class Util
    {
        public static Random rnd = new Random(DateTime.Now.Millisecond);
        public const int MAX_ROWS = 50;
        public const string ONE_MONTH = "4.99";
        public const string SIX_MONTH = "24.99";
        public const string TWELVE_MONTH = "44.99";
        public const string REG_SIX_MONTH = "29.99";
        public const string REG_TWELVE_MONTH = "59.99";

        public const string TIME_INTERVAL = "5000";

        public const int MAX_NUMBERS = 9;

        //public const string ONE_MONTH = "9.99";
        //public const string SIX_MONTH = "49.99";
        //public const string TWELVE_MONTH = "89.99";
        //public const string REG_SIX_MONTH = "59.99";
        //public const string REG_TWELVE_MONTH = "119.99";

        public static string[] NumberRange = { "1s", "10s",  "20s",  "30s",  "40s",  "50s",  "60s",  "70s",  "80s",  "90s",  
                                               "100s","110s", "120s", "130s", "140s", "150s", "160s", "170s", "180s", "190s",
                                               "200s", "210+"       
                                             };
        public static string[] LargeNumberRange = { "70s",  "80s",  "90s",  "100s", "110s", "120s", "130s", "140s", 
                                                    "150s", "160s", "170s", "180s", "190s", "200s", "210s", "220s", "230s", "240s", 
                                                    "250s", "260s", "270s", "280+"  
                                                  };
        public const int SMALL_COLUMN_NUMBERS = 3;
        public const int SMALL_LOTTO_NUMBERS = 40;

        private static Dictionary<Database, string> LogoDic = new Dictionary<Database, string>();
        private static Dictionary<Database, int> ColumnnsOfLotto_no_bonusDic = new Dictionary<Database, int>();
        private static Dictionary<Database, int> ColumnnsOfLottoDic = new Dictionary<Database, int>();
        private static Dictionary<Database, string> BonusColNameDic = new Dictionary<Database, string>();
        private static Dictionary<Database, string> CacheFileDic = new Dictionary<Database, string>();
        private static Dictionary<Database, int> LottoNumberRangesDic = new Dictionary<Database, int>();
        private static Dictionary<Database, int> TotalLottoNumbersDic = new Dictionary<Database, int>();

        public static int retrieveNumFromMethodName(string thisMethodName)
        {         
            // Retrieve numbers from current method name
            int num = int.Parse(Regex.Match(thisMethodName, @"\d+", RegexOptions.RightToLeft).Value);
            return num;
        }

        public static int getColumnnsOfLotto(Database db)
        {
            return ColumnnsOfLottoDic[db];
        }

        public static void InitColumnnsOfLotto()
        {
            // including bonus column
            ColumnnsOfLottoDic[Database.Lottery] = 7;
            ColumnnsOfLottoDic[Database.LottoMax] = 8;
            ColumnnsOfLottoDic[Database.BC49] = 7;
            ColumnnsOfLottoDic[Database.FloridaLotto] = 7;
            ColumnnsOfLottoDic[Database.MegaMillions] = 5; 
            ColumnnsOfLottoDic[Database.MegaMillions_MegaBall] = 1; 
            ColumnnsOfLottoDic[Database.PowerBall] = 5; 
            ColumnnsOfLottoDic[Database.PowerBall_PowerBall] = 1;
            ColumnnsOfLottoDic[Database.NYLotto] = 7;
            ColumnnsOfLottoDic[Database.EuroMillions] = 5;
            ColumnnsOfLottoDic[Database.EuroMillions_LuckyStars] = 2;
            ColumnnsOfLottoDic[Database.EuroJackpot] = 5;
            ColumnnsOfLottoDic[Database.EuroJackpot_Euros] = 2;
            ColumnnsOfLottoDic[Database.OZLottoTue] = 9;
            ColumnnsOfLottoDic[Database.SSQ] = 6;
            ColumnnsOfLottoDic[Database.SSQ_Blue] = 1;
            ColumnnsOfLottoDic[Database.SevenLotto] = 8;
            ColumnnsOfLottoDic[Database.SuperLotto] = 5;
            ColumnnsOfLottoDic[Database.SuperLotto_Rear] = 2;
            ColumnnsOfLottoDic[Database.NYSweetMillion] = 6;
            ColumnnsOfLottoDic[Database.ColoradoLotto] = 6;
            ColumnnsOfLottoDic[Database.FloridaLucky] = 5;
            ColumnnsOfLottoDic[Database.GermanLotto] = 7;
            ColumnnsOfLottoDic[Database.BritishLotto] = 7;
            ColumnnsOfLottoDic[Database.OZLottoSat] = 8;
            ColumnnsOfLottoDic[Database.FloridaFantasy5] = 5;
            ColumnnsOfLottoDic[Database.OZLottoMon] = 8;
            ColumnnsOfLottoDic[Database.OZLottoWed] = 8;
            ColumnnsOfLottoDic[Database.ConnecticutLotto] = 6;
            ColumnnsOfLottoDic[Database.CaSuperlottoPlus] = 5;
            ColumnnsOfLottoDic[Database.CaSuperlottoPlus_Mega] = 1;
            ColumnnsOfLottoDic[Database.NewJerseyPick6Lotto] = 6;
            ColumnnsOfLottoDic[Database.OregonMegabucks] = 6;
            ColumnnsOfLottoDic[Database.NewYorkTake5] = 5;
            ColumnnsOfLottoDic[Database.TexasCashFive] = 5;
            ColumnnsOfLottoDic[Database.DailyGrand] = 5;
            ColumnnsOfLottoDic[Database.DailyGrand_GrandNumber] = 1;
            ColumnnsOfLottoDic[Database.Cash4Life] = 5;

        }

        public static int getColumnnsOfLotto_no_bonus(Database db)
        {
            return ColumnnsOfLotto_no_bonusDic[db];
        }
        public static void InitColumnnsOfLotto_no_bonus()
        {
            // Not including bonus column
            ColumnnsOfLotto_no_bonusDic[Database.Lottery] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.LottoMax] = 7;
            ColumnnsOfLotto_no_bonusDic[Database.BC49] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.FloridaLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.FloridaLucky] = 4;
            ColumnnsOfLotto_no_bonusDic[Database.MegaMillions] = 5; 
            ColumnnsOfLotto_no_bonusDic[Database.MegaMillions_MegaBall] = 1; 
            ColumnnsOfLotto_no_bonusDic[Database.PowerBall] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.PowerBall_PowerBall] = 1;
            ColumnnsOfLotto_no_bonusDic[Database.NYLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.EuroMillions] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.EuroMillions_LuckyStars] = 2;
            ColumnnsOfLotto_no_bonusDic[Database.EuroJackpot] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.EuroJackpot_Euros] = 2;
            ColumnnsOfLotto_no_bonusDic[Database.OZLottoTue] = 7;
            ColumnnsOfLotto_no_bonusDic[Database.SSQ] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.SSQ_Blue] = 1;
            ColumnnsOfLotto_no_bonusDic[Database.SevenLotto] = 7;
            ColumnnsOfLotto_no_bonusDic[Database.SuperLotto] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.SuperLotto_Rear] = 2;
            ColumnnsOfLotto_no_bonusDic[Database.NYSweetMillion] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.ColoradoLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.GermanLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.BritishLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.OZLottoSat] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.FloridaFantasy5] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.OZLottoMon] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.OZLottoWed] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.ConnecticutLotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.CaSuperlottoPlus] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.CaSuperlottoPlus_Mega] = 1;
            ColumnnsOfLotto_no_bonusDic[Database.NewJerseyPick6Lotto] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.OregonMegabucks] = 6;
            ColumnnsOfLotto_no_bonusDic[Database.NewYorkTake5] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.TexasCashFive] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.DailyGrand] = 5;
            ColumnnsOfLotto_no_bonusDic[Database.DailyGrand_GrandNumber] = 1;
            ColumnnsOfLotto_no_bonusDic[Database.Cash4Life] = 5;

        }

        public static void InitBonusColName()
        {         
            // including bonus column
            BonusColNameDic[Database.Lottery] = "Bonus";
            BonusColNameDic[Database.LottoMax] = "Bonus";
            BonusColNameDic[Database.BC49] = "Bonus";
            BonusColNameDic[Database.FloridaLotto] = "Bonus";
            BonusColNameDic[Database.FloridaLucky] = "LuckyBall";
            BonusColNameDic[Database.MegaMillions] = null;
            BonusColNameDic[Database.MegaMillions_MegaBall] = "MegaBall";
            BonusColNameDic[Database.PowerBall] = null;
            BonusColNameDic[Database.PowerBall_PowerBall] = "PowerBall";
            BonusColNameDic[Database.NYLotto] = "Bonus";
            BonusColNameDic[Database.EuroMillions] = null;
            BonusColNameDic[Database.EuroMillions_LuckyStars] = "Star";
            BonusColNameDic[Database.EuroJackpot] = null;
            BonusColNameDic[Database.EuroJackpot_Euros] = "Euro";
            BonusColNameDic[Database.OZLottoTue] = "Supp";
            BonusColNameDic[Database.SSQ] = null;
            BonusColNameDic[Database.SSQ_Blue] = "Blue";
            BonusColNameDic[Database.SevenLotto] = "Special";
            BonusColNameDic[Database.SuperLotto] = null;
            BonusColNameDic[Database.SuperLotto_Rear] = "RearNumber";
            BonusColNameDic[Database.NYSweetMillion] = null;
            BonusColNameDic[Database.ColoradoLotto] = null;
            BonusColNameDic[Database.GermanLotto] = "Bonus";
            BonusColNameDic[Database.BritishLotto] = "Bonus";
            BonusColNameDic[Database.OZLottoSat] = "Supp";
            BonusColNameDic[Database.FloridaFantasy5] = null;
            BonusColNameDic[Database.OZLottoMon] = "Supp";
            BonusColNameDic[Database.OZLottoWed] = "Supp";
            BonusColNameDic[Database.ConnecticutLotto] = null;
            BonusColNameDic[Database.CaSuperlottoPlus] = null;
            BonusColNameDic[Database.CaSuperlottoPlus_Mega] = "Mega";
            BonusColNameDic[Database.NewJerseyPick6Lotto] = null;
            BonusColNameDic[Database.OregonMegabucks] = null;
            BonusColNameDic[Database.NewYorkTake5] = null;
            BonusColNameDic[Database.TexasCashFive] = null;
            BonusColNameDic[Database.DailyGrand] = null;
            BonusColNameDic[Database.DailyGrand_GrandNumber] = "Grand";
            BonusColNameDic[Database.Cash4Life] = "CashBall";
        }

        public static string getBonusColName(Database db)
        {
            return BonusColNameDic[db];
        }


        public static void InitLogoDic()
        {
            LogoDic[Database.Lottery] = "/images/649.png";
            LogoDic[Database.LottoMax] = "/images/Lottomax.png";
            LogoDic[Database.BC49] = "/images/bc49.png";
            LogoDic[Database.FloridaLotto] = "/images/FloridaLotto.png";
            LogoDic[Database.FloridaLucky] = "/images/FloridaLuckyMoney.jpg";
            LogoDic[Database.MegaMillions] = "/images/MegaMillions.png";
            LogoDic[Database.MegaMillions_MegaBall] = "/images/MegaMillions.png";
            LogoDic[Database.PowerBall] = "/images/Powerball.png";
            LogoDic[Database.PowerBall_PowerBall] = "/images/Powerball.png";
            LogoDic[Database.NYLotto] = "/images/NYLotto.png";
            LogoDic[Database.EuroMillions] = "/images/EuroMillions.png";
            LogoDic[Database.EuroMillions_LuckyStars] = "/images/EuroMillions.png";
            LogoDic[Database.EuroJackpot] = "/images/EuroJackpot.png";
            LogoDic[Database.EuroJackpot_Euros] = "/images/EuroJackpot.png";
            LogoDic[Database.OZLottoTue] = "/images/OZTuesdayLotto.png";
            LogoDic[Database.SSQ] = "/images/ssq.png";
            LogoDic[Database.SSQ_Blue] = "/images/ssq.png";
            LogoDic[Database.SevenLotto] = "/images/SevenLotto.png";
            LogoDic[Database.SuperLotto] = "/images/SuperLotto.png";
            LogoDic[Database.SuperLotto_Rear] = "/images/SuperLotto.png";
            LogoDic[Database.NYSweetMillion] = "/images/NYSweetMillion.png";
            LogoDic[Database.ColoradoLotto] = "/images/ColoradoLotto.png";
            LogoDic[Database.GermanLotto] = "/images/GermanLotto.png";
            LogoDic[Database.BritishLotto] = "/images/BritishLotto.png";
            LogoDic[Database.OZLottoSat] = "/images/OZSaturdayLotto.png";
            LogoDic[Database.FloridaFantasy5] = "/images/FloridaFantasy5.png";
            LogoDic[Database.OZLottoMon] = "/images/OZMondayLotto.png";
            LogoDic[Database.OZLottoWed] = "/images/OZWednesdayLotto.png";
            LogoDic[Database.ConnecticutLotto] = "/images/ConnecticutLotto.png";
            LogoDic[Database.CaSuperlottoPlus] = "/images/CaliforniaSuperlotto.png";
            LogoDic[Database.CaSuperlottoPlus_Mega] = "/images/CaliforniaSuperlotto.png";
            LogoDic[Database.NewJerseyPick6Lotto] = "/images/NewJerseyPick6Lotto.png";
            LogoDic[Database.OregonMegabucks] = "/images/OregonMegabucks.png";
            LogoDic[Database.NewYorkTake5] = "/images/NewYorkTake5.png";
            LogoDic[Database.TexasCashFive] = "/images/TexasCashFive.png";
            LogoDic[Database.DailyGrand] = "/images/tile-daily-grand-3x2.png";
            LogoDic[Database.DailyGrand_GrandNumber] = "/images/tile-daily-grand-3x2.png";
            LogoDic[Database.Cash4Life] = "/images/c4l-game-pg-banner.jpg";

        }

        public static string getLottoImage(Database db)
        {
            return LogoDic[db];
        }

        public static string getCacheName(Database db, int statisitcs)
        {
            Dictionary<Database, string> dic = new Dictionary<Database, string>();
            dic[Database.Lottery] = "Statistics" + statisitcs.ToString() + "_Lottery";
            dic[Database.LottoMax] = "Statistics" + statisitcs.ToString() + "_LottoMax";
            dic[Database.BC49] = "Statistics" + statisitcs.ToString() + "_BC49";
            dic[Database.FloridaLotto] = "Statistics" + statisitcs.ToString() + "_FloridaLotto";
            dic[Database.MegaMillions] = "Statistics" + statisitcs.ToString() + "_MegaMillions";
            dic[Database.MegaMillions_MegaBall] = "Statistics" + statisitcs.ToString() + "_MegaMillions_MegaBall";
            dic[Database.PowerBall] = "Statistics" + statisitcs.ToString() + "_PowerBall";
            dic[Database.PowerBall_PowerBall] = "Statistics" + statisitcs.ToString() + "_PowerBall_PowerBall";
            dic[Database.NYLotto] = "Statistics" + statisitcs.ToString() + "_NYLotto";
            dic[Database.EuroMillions] = "Statistics" + statisitcs.ToString() + "_EuroMillions";
            dic[Database.EuroMillions_LuckyStars] = "Statistics" + statisitcs.ToString() + "_EuroMillions_LuckyStars";
            dic[Database.OZLottoTue] = "Statistics" + statisitcs.ToString() + "_OZLotto";
            dic[Database.SSQ] = "Statistics" + statisitcs.ToString() + "_SSQ";
            dic[Database.SSQ_Blue] = "Statistics" + statisitcs.ToString() + "_SSQ_Blue";
            dic[Database.SevenLotto] = "Statistics" + statisitcs.ToString() + "_SevenLotto";
            dic[Database.SuperLotto] = "Statistics" + statisitcs.ToString() + "_SuperLotto";
            dic[Database.SuperLotto_Rear] = "Statistics" + statisitcs.ToString() + "_SuperLotto_Rear";
            dic[Database.NYSweetMillion] = "Statistics" + statisitcs.ToString() + "_NYSweetMillion";
            dic[Database.ColoradoLotto] = "Statistics" + statisitcs.ToString() + "_ColoradoLotto";
            dic[Database.FloridaLucky] = "Statistics" + statisitcs.ToString() + "_FloridaLucky";
            dic[Database.EuroJackpot] = "Statistics" + statisitcs.ToString() + "_EuroJackpot";
            dic[Database.EuroJackpot_Euros] = "Statistics" + statisitcs.ToString() + "_EuroJackpot_Euros";
            dic[Database.GermanLotto] = "Statistics" + statisitcs.ToString() + "_GermanLotto";
            dic[Database.BritishLotto] = "Statistics" + statisitcs.ToString() + "_BritishLotto";
            dic[Database.OZLottoSat] = "Statistics" + statisitcs.ToString() + "_OZLottoSat";
            dic[Database.FloridaFantasy5] = "Statistics" + statisitcs.ToString() + "_FloridaFantasy5";
            dic[Database.OZLottoMon] = "Statistics" + statisitcs.ToString() + "_OZLottoMon";
            dic[Database.OZLottoWed] = "Statistics" + statisitcs.ToString() + "_OZLottoWed";
            dic[Database.ConnecticutLotto] = "Statistics" + statisitcs.ToString() + "_ConnecticutLotto";
            dic[Database.CaSuperlottoPlus] = "Statistics" + statisitcs.ToString() + "_CaSuperlottoPlus";
            dic[Database.CaSuperlottoPlus_Mega] = "Statistics" + statisitcs.ToString() + "_CaSuperlottoPlus_Mega";
            dic[Database.NewJerseyPick6Lotto] = "Statistics" + statisitcs.ToString() + "_NewJerseyPick6Lotto";
            dic[Database.OregonMegabucks] = "Statistics" + statisitcs.ToString() + "_OregonMegabucks";
            dic[Database.NewYorkTake5] = "Statistics" + statisitcs.ToString() + "_NewYorkTake5";
            dic[Database.TexasCashFive] = "Statistics" + statisitcs.ToString() + "_TexasCashFive";
            dic[Database.DailyGrand] = "Statistics" + statisitcs.ToString() + "_DailyGrand";
            dic[Database.DailyGrand_GrandNumber] = "Statistics" + statisitcs.ToString() + "_DailyGrand_GrandNumber";
            dic[Database.Cash4Life] = "Statistics" + statisitcs.ToString() + "_Cash4Life";


            return dic[db];
        }

        public static void InitCacheFile(string rootDir)
        {
            CacheFileDic[Database.Lottery] = rootDir + "/lotto649.xml";
            CacheFileDic[Database.BC49] = rootDir + "/bc49.xml";
            CacheFileDic[Database.EuroMillions] = rootDir + "/EuroMillions.xml";
            CacheFileDic[Database.EuroMillions_LuckyStars] = rootDir + "/EuroMillions.xml";
            CacheFileDic[Database.FloridaLotto] = rootDir + "/FloridaLotto.xml";
            CacheFileDic[Database.LottoMax] = rootDir + "/LottoMax.xml";
            CacheFileDic[Database.MegaMillions] = rootDir + "/MegaMillions.xml";
            CacheFileDic[Database.MegaMillions_MegaBall] = rootDir + "/MegaMillions.xml";
            CacheFileDic[Database.NYLotto] = rootDir + "/NYLotto.xml";
            CacheFileDic[Database.OZLottoTue] = rootDir + "/OZLottoTue.xml";
            CacheFileDic[Database.PowerBall] = rootDir + "/PowerBall.xml";
            CacheFileDic[Database.PowerBall_PowerBall] = rootDir + "/PowerBall.xml";
            CacheFileDic[Database.SSQ] = rootDir + "/SSQ.xml";
            CacheFileDic[Database.SSQ_Blue] = rootDir + "/SSQ.xml";
            CacheFileDic[Database.SevenLotto] = rootDir + "/SevenLotto.xml";
            CacheFileDic[Database.SuperLotto] = rootDir + "/SuperLotto.xml";
            CacheFileDic[Database.SuperLotto_Rear] = rootDir + "/SuperLotto.xml";
            CacheFileDic[Database.NYSweetMillion] = rootDir + "/NYSweetMillion.xml";
            CacheFileDic[Database.ColoradoLotto] = rootDir + "/ColoradoLotto.xml";
            CacheFileDic[Database.FloridaLucky] = rootDir + "/FloridaLucky.xml";
            CacheFileDic[Database.EuroJackpot] = rootDir + "/EuroJackpot.xml";
            CacheFileDic[Database.EuroJackpot_Euros] = rootDir + "/EuroJackpot_Euros.xml";
            CacheFileDic[Database.GermanLotto] = rootDir + "/GermanLotto.xml";
            CacheFileDic[Database.BritishLotto] = rootDir + "/BritishLotto.xml";
            CacheFileDic[Database.OZLottoSat] = rootDir + "/OZLottoSat.xml";
            CacheFileDic[Database.FloridaFantasy5] = rootDir + "/FloridaFantasy5.xml";
            CacheFileDic[Database.OZLottoMon] = rootDir + "/OZLottoMon.xml";
            CacheFileDic[Database.OZLottoWed] = rootDir + "/OZLottoWed.xml";
            CacheFileDic[Database.ConnecticutLotto] = rootDir + "/ConnecticutLotto.xml";
            CacheFileDic[Database.CaSuperlottoPlus] = rootDir + "/CaSuperlottoPlus.xml";
            CacheFileDic[Database.CaSuperlottoPlus_Mega] = rootDir + "/CaSuperlottoPlus_Mega.xml";
            CacheFileDic[Database.NewJerseyPick6Lotto] = rootDir + "/NewJerseyPick6Lotto.xml";
            CacheFileDic[Database.OregonMegabucks] = rootDir + "/OregonMegabucks.xml";
            CacheFileDic[Database.NewYorkTake5] = rootDir + "/NewYorkTake5.xml";
            CacheFileDic[Database.TexasCashFive] = rootDir + "/TexasCashFive.xml";
            CacheFileDic[Database.DailyGrand] = rootDir + "/DailyGrand.xml";
            CacheFileDic[Database.DailyGrand_GrandNumber] = rootDir + "/DailyGrand.xml";
            CacheFileDic[Database.Cash4Life] = rootDir + "/Cash4Life.xml";
        }

        public static string getCacheFile(Database db)
        {
            return CacheFileDic[db];
        }

        public static void InitLottoNumberRanges()
        {
            // including bonus column
            LottoNumberRangesDic[Database.Lottery] = 5;
            LottoNumberRangesDic[Database.LottoMax] = 5;
            LottoNumberRangesDic[Database.BC49] = 5;
            LottoNumberRangesDic[Database.FloridaLotto] = 6;
            LottoNumberRangesDic[Database.FloridaLucky] = 5;
            LottoNumberRangesDic[Database.MegaMillions] = 8;  
            LottoNumberRangesDic[Database.MegaMillions_MegaBall] = 2;  
            LottoNumberRangesDic[Database.PowerBall] = 6;
            LottoNumberRangesDic[Database.PowerBall_PowerBall] = 4;
            LottoNumberRangesDic[Database.NYLotto] = 6;
            LottoNumberRangesDic[Database.EuroMillions] = 6;
            LottoNumberRangesDic[Database.EuroMillions_LuckyStars] = 2;
            LottoNumberRangesDic[Database.EuroJackpot] = 6;
            LottoNumberRangesDic[Database.EuroJackpot_Euros] = 1;
            LottoNumberRangesDic[Database.OZLottoTue] = 5;
            LottoNumberRangesDic[Database.SSQ] = 4;
            LottoNumberRangesDic[Database.SSQ_Blue] = 2;
            LottoNumberRangesDic[Database.SevenLotto] = 4;
            LottoNumberRangesDic[Database.SuperLotto] = 4;
            LottoNumberRangesDic[Database.SuperLotto_Rear] = 2;
            LottoNumberRangesDic[Database.NYSweetMillion] = 5;
            LottoNumberRangesDic[Database.ColoradoLotto] = 5;
            LottoNumberRangesDic[Database.GermanLotto] = 5;
            LottoNumberRangesDic[Database.BritishLotto] = 5;
            LottoNumberRangesDic[Database.OZLottoSat] = 5;
            LottoNumberRangesDic[Database.FloridaFantasy5] = 4;
            LottoNumberRangesDic[Database.OZLottoMon] = 5;
            LottoNumberRangesDic[Database.OZLottoWed] = 5;
            LottoNumberRangesDic[Database.ConnecticutLotto] = 5;
            LottoNumberRangesDic[Database.CaSuperlottoPlus] = 5;
            LottoNumberRangesDic[Database.CaSuperlottoPlus_Mega] = 3;
            LottoNumberRangesDic[Database.NewJerseyPick6Lotto] = 5;
            LottoNumberRangesDic[Database.OregonMegabucks] = 5;
            LottoNumberRangesDic[Database.NewYorkTake5] = 4;
            LottoNumberRangesDic[Database.TexasCashFive] = 4;
            LottoNumberRangesDic[Database.DailyGrand] = 5;
            LottoNumberRangesDic[Database.DailyGrand_GrandNumber] = 1;
            LottoNumberRangesDic[Database.Cash4Life] = 5;
        }

        public static int getLottoNumberRanges(Database db)
        {
            return LottoNumberRangesDic[db];
        }

        public static void InitTotalLottoNumbers()
        {
            // including bonus column
            TotalLottoNumbersDic[Database.Lottery] = 49;
            TotalLottoNumbersDic[Database.LottoMax] = 50;
            TotalLottoNumbersDic[Database.BC49] = 49;
            TotalLottoNumbersDic[Database.FloridaLotto] = 53;
            TotalLottoNumbersDic[Database.FloridaLucky] = 47;
            TotalLottoNumbersDic[Database.MegaMillions] = 75;
            TotalLottoNumbersDic[Database.MegaMillions_MegaBall] = 25; // Now changed to 1-15, used to be 1-46
            TotalLottoNumbersDic[Database.PowerBall] = 69;
            TotalLottoNumbersDic[Database.PowerBall_PowerBall] = 39;
            TotalLottoNumbersDic[Database.NYLotto] = 59;
            TotalLottoNumbersDic[Database.EuroMillions] = 50;
            TotalLottoNumbersDic[Database.EuroMillions_LuckyStars] = 12;
            TotalLottoNumbersDic[Database.EuroJackpot] = 50;
            TotalLottoNumbersDic[Database.EuroJackpot_Euros] = 10;
            TotalLottoNumbersDic[Database.OZLottoTue] = 45;
            TotalLottoNumbersDic[Database.SSQ] = 33;
            TotalLottoNumbersDic[Database.SSQ_Blue] = 16;
            TotalLottoNumbersDic[Database.SevenLotto] = 30;
            TotalLottoNumbersDic[Database.SuperLotto] = 35;
            TotalLottoNumbersDic[Database.SuperLotto_Rear] = 12;
            TotalLottoNumbersDic[Database.NYSweetMillion] = 40;
            TotalLottoNumbersDic[Database.ColoradoLotto] = 42;
            TotalLottoNumbersDic[Database.GermanLotto] = 49;
            TotalLottoNumbersDic[Database.BritishLotto] = 49;
            TotalLottoNumbersDic[Database.OZLottoSat] = 45;
            TotalLottoNumbersDic[Database.FloridaFantasy5] = 36;
            TotalLottoNumbersDic[Database.OZLottoMon] = 45;
            TotalLottoNumbersDic[Database.OZLottoWed] = 45;
            TotalLottoNumbersDic[Database.ConnecticutLotto] = 44;
            TotalLottoNumbersDic[Database.CaSuperlottoPlus] = 47;
            TotalLottoNumbersDic[Database.CaSuperlottoPlus_Mega] = 27;
            TotalLottoNumbersDic[Database.NewJerseyPick6Lotto] = 49;
            TotalLottoNumbersDic[Database.OregonMegabucks] = 48;
            TotalLottoNumbersDic[Database.NewYorkTake5] = 39;
            TotalLottoNumbersDic[Database.TexasCashFive] = 39;
            TotalLottoNumbersDic[Database.DailyGrand] = 49;
            TotalLottoNumbersDic[Database.DailyGrand_GrandNumber] = 7;
            TotalLottoNumbersDic[Database.Cash4Life] = 60;
        }

        public static int getTotalLottoNumbers(Database db)
        {
            return TotalLottoNumbersDic[db];
        }

        

        public static void selection_sort(int[] arr)
        {
            int min;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j] < arr[min])
                    {
                        min = j;
                    }
                }
                // swap arr[i] and arr[min]
                int tmp = arr[i];
                arr[i] = arr[min];
                arr[min] = tmp;
            }
        }

        // Bubble Sort on frequence
        public static void bubbleSort(Statistics[] st)
        {
            for (int i = 0; i < st.Length; i++)
            {
                for (int j = 1; j < (st.Length - i); j++)
                {
                    /* If adjacent items out of order, swap */
                    if (st[j - 1].Cnt > st[j].Cnt)
                    {
                        Statistics t;
                        t = st[j - 1];
                        st[j - 1] = st[j];
                        st[j] = t;
                    }
                }
            }
        }

        public static void selection_sort(Statistics[] arr)
        {
            int min;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j].Cnt < arr[min].Cnt)
                    {
                        min = j;
                    }
                }
                // swap arr[i] and arr[min]
                Statistics tmp = arr[i];
                arr[i] = arr[min];
                arr[min] = tmp;
            }
        }

        public static void selection_sort(Statistics[] arr, int dist)
        {
            int min;
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                min = i;
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j].getDist(dist) < arr[min].getDist(dist))
                    {
                        min = j;
                    }
                }
                // swap arr[i] and arr[min]
                Statistics tmp = arr[i];
                arr[i] = arr[min];
                arr[min] = tmp;
            }
        }




        // Set up a call to the actual Quicksort method. 
        public static void qsort(int[] items)
        {
            qs(items, 0, items.Length - 1);
        }

        // A recursive version of Quicksort for characters.  
        static void qs(int[] items, int left, int right)
        {
            int i, j;
            int x, y;

            i = left; j = right;
            x = items[(left + right) / 2];

            do
            {
                while ((items[i] < x) && (i < right)) i++;
                while ((x < items[j]) && (j > left)) j--;

                if (i <= j)
                {
                    y = items[i];
                    items[i] = items[j];
                    items[j] = y;
                    i++; j--;
                }
            } while (i <= j);

            if (left < j) qs(items, left, j);
            if (i < right) qs(items, i, right);
        }

        private static bool compGT(int a, int b)
        {
            return (a > b);
        }

        // Shell Sort
        public static void shellSort(Statistics[] a, int lb, int ub)
        {
            int n, h, i, j;
            Statistics t;

            /**************************
             *  sort array a[lb..ub]  *
             **************************/

            /* compute largest increment */
            n = ub - lb + 1;
            h = 1;
            if (n < 14)
            {
                h = 1;
            }
            else
            {
                while (h < n) h = 3 * h + 1;
                h /= 3;
                h /= 3;
            }

            while (h > 0)
            {
                /* sort-by-insertion in increments of h */
                for (i = lb + h; i <= ub; i++)
                {
                    t = a[i];
                    for (j = i - h; j >= lb && compGT(a[j].Cnt, t.Cnt); j -= h)
                    {
                        a[j + h] = a[j];
                        //cnt++;
                    }
                    a[j + h] = t;
                }

                /* compute next increment */
                h /= 3;
            }
        }

        // Shell Sort
        public static void shellSort(int[] a, int lb, int ub)
        {
            int n, h, i, j;
            int t;

            /**************************
             *  sort array a[lb..ub]  *
             **************************/

            /* compute largest increment */
            n = ub - lb + 1;
            h = 1;
            if (n < 14)
            {
                h = 1;
            }
            else
            {
                while (h < n) h = 3 * h + 1;
                h /= 3;
                h /= 3;
            }

            while (h > 0)
            {
                /* sort-by-insertion in increments of h */
                for (i = lb + h; i <= ub; i++)
                {
                    t = a[i];
                    for (j = i - h; j >= lb && compGT(a[j], t); j -= h)
                    {
                        a[j + h] = a[j];
                        //cnt++;
                    }
                    a[j + h] = t;
                }

                /* compute next increment */
                h /= 3;
            }
        }

        // Modefied Shell Sort
        public static void shellSort_Dist(Statistics[] a, int lb, int ub, int dist)
        {
            int n, h, i, j;
            Statistics t;

            /**************************
             *  sort array a[lb..ub]  *
             **************************/

            /* compute largest increment */
            n = ub - lb + 1;
            h = 1;
            if (n < 14)
            {
                h = 1;
            }
            else
            {
                while (h < n) h = 3 * h + 1;
                h /= 3;
                h /= 3;
            }

            while (h > 0)
            {
                /* sort-by-insertion in increments of h */
                for (i = lb + h; i <= ub; i++)
                {
                    t = a[i];
                    for (j = i - h; j >= lb && compGT(a[j].getDist(dist), t.getDist(dist)); j -= h)
                    {
                        a[j + h] = a[j];
                        //cnt++;
                    }
                    a[j + h] = t;
                }

                /* compute next increment */
                h /= 3;
            }
        }

        public static void initDictionary(Dictionary<int, bool> dic, int len)
        {
            for (int i = 1; i <= len; ++i)
            {
                dic[i] = false;
            }
        }

        public static bool amongNumbers(int n, int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                if (n == arr[i]) return true;
            }
            return false;
        }

        public static bool amongNumbers(Dictionary<int, bool> targetDic, int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                int key = arr[i];
                if (!targetDic[key])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool amongNumbers(int n, Dictionary<int, bool> targetDic)
        {
            return targetDic[n];
        }

        public static bool amongNumbers(Dictionary<int, bool> targetDic, Dictionary<int, bool> amongDic)
        {
            foreach (int key in targetDic.Keys)
            {
                if (!amongDic.ContainsKey(key))
                {
                    return false;
                }
            }
            return true;
        }


        public static bool processMatch(Dictionary<int, bool> targetDic, int[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                int key = arr[i];
                if (!targetDic.ContainsKey(key))
                {
                    return false;
                }
            }
            return true;
        }


        public static string CreateHTML_Tail()
        {
            string stmt = " ";
            stmt += "</BODY>\n"
                + "</HTML>";
            return stmt;
        }

        public static string CreateHTML_Tail(long dbExecTime)
        {
            string stmt = " ";
            stmt += "<p class='dbexectime'>Database execution time: " + dbExecTime.ToString() + " ms.<br /></p>";

            stmt += "</BODY>\n"
                + "</HTML>";
            return stmt;
        }

        public static string CreateHTML_Header(string title, Database db, bool NoLogo = false)
        {
            string stmt = " ";
            stmt = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n";
            stmt += "<html xmlns=\"http://www.w3.org/1999/xhtml\" >\n";

            stmt += "<link href=\"/App_Themes/LottoTheme/GeneratedPages.css\" rel=\"stylesheet\" type=\"text/css\" />\n";

            // meta name, content and title for SEO
            stmt += "<meta name=\"author\" content=\"SoftSolution\"/>\n";
            stmt += "<meta name=\"keywords\" content=\"Lottery, lotto, loto, pick, auto draw, prediction, winning number, 649, LottoMax, lotto app, lotto software, Most of North American Lotteries\"/>\n";
            stmt += "<meta name=\"description\"\n";
            stmt += "content=\"A lotto magic web site for try, prediction, lotto pick, auto draw, forecast, guess and analysis to hit next draw of lottery numbers.\"/>\n";
            stmt += "<title>Lottotry.com - A Unique Web Based Lottery Apps for Lottery Prediction, Lotto Statistics, Lotto Guessing</title>\n";

            //// include js files
            //stmt += "<script type=\"text/javascript\" src=\"/jquery.js\"></script>\n";
            //stmt += "<script type=\"text/javascript\">\n";
            //stmt += "$(document).ready(function () {\n";
            //stmt += "$('table tr:even').addClass('highlight');});\n";
            //stmt += "</script>\n";

            // google analytic code for SEO
            stmt += "<script type=\"text/javascript\">\n";
            stmt += "var _gaq = _gaq || [];\n";
            stmt += "_gaq.push(['_setAccount', 'UA-10934632-5']);\n";
            stmt += "_gaq.push(['_trackPageview']);\n";

            stmt += "(function() {\n";
            stmt += "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;\n";
            stmt += "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n";
            stmt += "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n";
            stmt += "})();\n";


            stmt += "</script>\n";
            // end of google analytic
            stmt += "</head>\n<body>\n";
            stmt += "<CENTER>\n";
            stmt += "<h3>" + title + "   ";
            if (!NoLogo)
                stmt += Util.LottoLogos(db);
            stmt += "</CENTER>\n";
            return stmt;
        }

        public static string CreateHTML_Header()
        {
            string stmt = " ";
            stmt = "<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n";
            stmt += "<html xmlns=\"http://www.w3.org/1999/xhtml\" >\n";

            stmt += "<link href=\"/App_Themes/LottoTheme/GeneratedPages.css\" rel=\"stylesheet\" type=\"text/css\" />\n";

            // meta name, content and title for SEO
            stmt += "<meta name=\"author\" content=\"SoftSolution\"/>\n";
            stmt += "<meta name=\"keywords\" content=\"Lottery, lotto, loto, pick, auto draw, prediction, winning number, 649, LottoMax, lotto app, lotto software, Most of North American Lotteries\"/>\n";
            stmt += "<meta name=\"description\"\n";
            stmt += "content=\"A lotto magic web site for try, prediction, lotto pick, auto draw, forecast, guess and analysis to hit next draw of lottery numbers.\"/>\n";
            stmt += "<title>Lottotry.com - A Unique Web Based Lottery Apps for Lottery Prediction, Lotto Statistics, Lotto Guessing</title>\n";

            //// include js files
            //stmt += "<script type=\"text/javascript\" src=\"/jquery.js\"></script>\n";
            //stmt += "<script type=\"text/javascript\">\n";
            //stmt += "$(document).ready(function () {\n";
            //stmt += "$('table tr:even').addClass('highlight');});\n";
            //stmt += "</script>\n";

            // google analytic code for SEO
            stmt += "<script type=\"text/javascript\">\n";
            stmt += "var _gaq = _gaq || [];\n";
            stmt += "_gaq.push(['_setAccount', 'UA-10934632-5']);\n";
            stmt += "_gaq.push(['_trackPageview']);\n";

            stmt += "(function() {\n";
            stmt += "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;\n";
            stmt += "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';\n";
            stmt += "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);\n";
            stmt += "})();\n";

            stmt += "</script>\n";
            // end of google analytic
            stmt += "</head>\n<body>\n";
            return stmt;
        }

        private static string displayTableRow(Dictionary<int, bool> dic,
                                       Dictionary<int, SubStatistics> statDic,
                                       Dictionary<int, bool> tmpDic,
                                       ref int numCount,
                                       int colStart, int colEnd)
        {
            string stmt = "";
            if (colStart > 1)
            {
                stmt += "<TR>\n";
            }
            stmt += "<TH bgcolor=\"#FFFFF0\"></TH>";
            for (int i = colStart; i <= colEnd; i++)
            {
                stmt += "<TH bgcolor=\"#FFFFF0\"><font color=\"#F6358A\">" + i.ToString() + "</TH>";
                if (dic[i])
                {
                    numCount++;
                }
            }
            stmt += "</TR>\n";
            stmt += "<TR>\n";
            stmt += "<TD><font color=\"#ffffff\">" + numCount.ToString() + "</TD>";
            if (colStart == 1)
            {
                stmt += "<TD>" + null + "</TD>";
            }
            for (int i = colStart; i <= colEnd; i++)
            {
                if (dic[i] && statDic[i] != null)
                {
                    if (tmpDic[i])
                    {

                        stmt += "<TD width=\"50\"> <FONT style=\"FONT-STYLE: italic; font-weight:bold\" color=\"#FF0000\">"
                            + i.ToString() + "<br />(R=" + statDic[i].RelativeDist.ToString() + ")"
                            + "<br />(S=" + statDic[i].SavedDist.ToString() + ")</TD>";
                    }
                    else
                    {
                        stmt += "<TD width=\"50\"> <FONT style=\"FONT-STYLE: italic; font-weight:bold\" color=\"#FFFFF0\">"
                            + i.ToString() + "<br />(R=" + statDic[i].RelativeDist.ToString() + ")"
                            + "<br />(S=" + statDic[i].SavedDist.ToString() + ")</TD>";
                    }
                }
                else
                {
                    stmt += "<TD>" + null + "</TD>";
                }
            }
            stmt += "</TR>";

            return stmt;
        }


        public static string DisplayPotentialNumbers(Dictionary<int, bool> dic, int[] arr, List<SubStatistics> list, Database db, int len)
        {
            string stmt = "";

            //stmt += Util.CreateHTML_Header("", db, true);
            stmt += "<script type=\"text/javascript\" src=\"/js/highlight.js\"></script>\n";
            stmt += "<TABLE class=\"table\" border=\"1\" align=\"center\" width=\"100%\">\n"
                + "<TR>\n";
            stmt += "<TH bgcolor=\"#FFFFF0\"><font color=\"#F6358A\">Total</TH>\n";


            Dictionary<int, bool> tmpDic = new Dictionary<int, bool>();
            Dictionary<int, SubStatistics> statDic = new Dictionary<int, SubStatistics>();

            for (int i = 0; i <= len; ++i)
            {
                tmpDic[i] = false;
                statDic[i] = null;
            }
            for (int i = 0; i < arr.Length; ++i)
            {
                int key = arr[i];
                tmpDic[key] = true;
            }
            for (int i = 0; i < list.Count; ++i)
            {
                statDic[list[i].Num] = list[i];
            }

            int numCount = 0;
            int totalNumbers = Util.getTotalLottoNumbers(db);

            // Numbers 1 - 9
            if (totalNumbers < 10)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, totalNumbers);
            }
            else if (totalNumbers < 20)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, totalNumbers);
            }
            else if (totalNumbers < 30)
            {

                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, totalNumbers);
            }
            else if (totalNumbers < 40)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, 29);

                // Numbers 30 - 39
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 30, totalNumbers);
            }
            else if (totalNumbers < 50)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, 29);

                // Numbers 30 - 39
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 30, 39);

                // Numbers 40 - 49
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 40, totalNumbers);
            }
            else if (totalNumbers < 60)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, 29);

                // Numbers 30 - 39
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 30, 39);

                // Numbers 40 - 49
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 40, 49);

                // Numbers 50 - 59
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 50, totalNumbers);

             }

            else if (totalNumbers < 70)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, 29);

                // Numbers 30 - 39
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 30, 39);

                // Numbers 40 - 49
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 40, 49);

                // Numbers 50 - 59
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 50, 59);

                // Numbers 60 - 69
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 60, totalNumbers);

            }

            else if (totalNumbers < 80)
            {
                // Numbers 1 - 9
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 1, 9);
                // Numbers 10 - 19
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 10, 19);

                // Numbers 20 - 29
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 20, 29);

                // Numbers 30 - 39
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 30, 39);

                // Numbers 40 - 49
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 40, 49);

                // Numbers 50 - 59
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 50, 59);

                // Numbers 60 - 69
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 60, 69);

                // Numbers 70 - 79
                stmt += displayTableRow(dic, statDic, tmpDic, ref numCount, 70, totalNumbers);
            }

            stmt += "</TABLE>";
            //stmt += Util.CreateHTML_Tail();
            return stmt;
        }


        public static void initSumRangeDic(Dictionary<int, int> dic, int len)
        {
            for (int i = 0; i < len; ++i)
            {
                dic[i] = 0;
            }
        }

        public static int getSum(int[] arr, int len)
        {
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += arr[i];
            }
            return sum;
        }
        public static int getSum(int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        public static int numOdds(int[] arr)
        {
            int odds = 0;

            for (int i = 0; i < arr.Length; ++i)
            {
                if ((arr[i] & 1) == 1)
                {
                    odds++;
                }
            }
            return odds;
        }

        public static int getSumRange(int sum, ref Dictionary<int, int> dic)
        {


            if (sum >= 1 && sum < 10)
            {
                dic[0]++;
                return dic[0];
            }
            else if (sum >= 10 && sum < 20)
            {
                dic[1]++;
                return dic[1];
            }
            else if (sum >= 20 && sum < 30)
            {
                dic[2]++;
                return dic[2];
            }
            else if (sum >= 30 && sum < 40)
            {
                dic[3]++;
                return dic[3];
            }
            else if (sum >= 40 && sum < 50)
            {
                dic[4]++;
                return dic[4];
            }
            else if (sum >= 50 && sum < 60)
            {
                dic[5]++;
                return dic[5];
            }
            else if (sum >= 60 && sum < 70)
            {
                dic[6]++;
                return dic[6];
            }
            else if (sum >= 70 && sum < 80)
            {
                dic[7]++;
                return dic[7];
            }
            else if (sum >= 80 && sum < 90)
            {
                dic[8]++;
                return dic[8];
            }
            else if (sum >= 90 && sum < 100)
            {
                dic[9]++;
                return dic[9];
            }
            else if (sum >= 100 && sum < 110)
            {
                dic[10]++;
                return dic[10];
            }
            else if (sum >= 110 && sum < 120)
            {
                dic[11]++;
                return dic[11];
            }
            else if (sum >= 120 && sum < 130)
            {
                dic[12]++;
                return dic[12];
            }
            else if (sum >= 130 && sum < 140)
            {
                dic[13]++;
                return dic[13];
            }
            else if (sum >= 140 && sum < 150)
            {
                dic[14]++;
                return dic[14];
            }
            else if (sum >= 150 && sum < 160)
            {
                dic[15]++;
                return dic[15];
            }
            else if (sum >= 160 && sum < 170)
            {
                dic[16]++;
                return dic[16];
            }
            else if (sum >= 170 && sum < 180)
            {
                dic[17]++;
                return dic[17];
            }
            else if (sum >= 180 && sum < 190)
            {
                dic[18]++;
                return dic[18];
            }
            else if (sum >= 190 && sum < 200)
            {
                dic[19]++;
                return dic[19];
            }
            else if (sum >= 200 && sum < 210)
            {
                dic[20]++;
                return dic[20];
            }
            else
            {
                dic[21]++;
                return dic[21];
            }

        }

        public static int getSumLargeRange(int sum, ref Dictionary<int, int> dic)
        {


            if (sum >= 70 && sum < 80)
            {
                dic[0]++;
                return dic[0];
            }
            else if (sum >= 80 && sum < 90)
            {
                dic[1]++;
                return dic[1];
            }
            else if (sum >= 90 && sum < 100)
            {
                dic[2]++;
                return dic[2];
            }
            else if (sum >= 100 && sum < 110)
            {
                dic[3]++;
                return dic[3];
            }
            else if (sum >= 110 && sum < 120)
            {
                dic[4]++;
                return dic[4];
            }
            else if (sum >= 120 && sum < 130)
            {
                dic[5]++;
                return dic[5];
            }
            else if (sum >= 130 && sum < 140)
            {
                dic[6]++;
                return dic[6];
            }
            else if (sum >= 140 && sum < 150)
            {
                dic[7]++;
                return dic[7];
            }
            else if (sum >= 150 && sum < 160)
            {
                dic[8]++;
                return dic[8];
            }
            else if (sum >= 160 && sum < 170)
            {
                dic[9]++;
                return dic[9];
            }
            else if (sum >= 170 && sum < 180)
            {
                dic[10]++;
                return dic[10];
            }
            else if (sum >= 180 && sum < 190)
            {
                dic[11]++;
                return dic[11];
            }
            else if (sum >= 190 && sum < 200)
            {
                dic[12]++;
                return dic[12];
            }
            else if (sum >= 200 && sum < 210)
            {
                dic[13]++;
                return dic[13];
            }
            else if (sum >= 210 && sum < 220)
            {
                dic[14]++;
                return dic[14];
            }
            else if (sum >= 220 && sum < 230)
            {
                dic[15]++;
                return dic[15];
            }
            else if (sum >= 230 && sum < 240)
            {
                dic[16]++;
                return dic[16];
            }
            else if (sum >= 240 && sum < 250)
            {
                dic[17]++;
                return dic[17];
            }
            else if (sum >= 250 && sum < 260)
            {
                dic[18]++;
                return dic[18];
            }
            else if (sum >= 260 && sum < 270)
            {
                dic[19]++;
                return dic[19];
            }
            else if (sum >= 270 && sum < 280)
            {
                dic[20]++;
                return dic[20];
            }
            else
            {
                dic[21]++;
                return dic[21];
            }

        }

        public static string Distribution(SubStatistics[] stat, int[] drawNumArray, Database db)
        {
            Dictionary<int, string> drawNumber = new Dictionary<int, string>();
            Dictionary<int, string> drawDate = new Dictionary<int, string>();
            Dictionary<int, int> sumRange = new Dictionary<int, int>();

            string stmt = "";
            //stmt += Util.CreateHTML_Header();
            stmt += "<BR /><TABLE class=\"generated_tables\" border=\"1\" align=\"center\" >\n";

            // <TH> part for the top of table
            stmt += "<TR style=\"color:#FFFFFF; background-color:#CC0000\">";
            for (int i = 0; i < drawNumArray.Length; ++i)
            {
                stmt += "<TH >No. " + (i + 1).ToString() + "</TH>";
            }
            stmt += "<TH>Sum</TH>";
            stmt += "<TH>Odds/Evens</TH>";
            stmt += "</TR>";

            int oddNum = 0, evenNum = 0, sum = 0;

            Util.initSumRangeDic(sumRange, 26);

            // <TD> part
            stmt += "<TR>";
            for (int j = 0; j < drawNumArray.Length; ++j)
            {
                stmt += "<TD style=\"color:#FF0000;background-color:#FFF380; font-weight:bold;font-size:normal;\">" + drawNumArray[j]
                        + " <font style=\"font-style:italic;color:#0000FF; font-weight:bold;font-size:90%;\">("
                        + stat[drawNumArray[j]].RelativeDist + ")</font>"
                        + " <font style=\"font-style:italic;font-weight:bold;color:Maroon;font-size:90%;\">("
                        + stat[drawNumArray[j]].SavedDist + ")</font></TD>";

            }

            sum = Util.getSum(drawNumArray);
            stmt += "<TD style=\"color:#F6358A; background-color:#FFF380; font-weight:bold;\">" + sum.ToString() + "</TD>";

            oddNum = Util.numOdds(drawNumArray);
            evenNum = drawNumArray.Length - oddNum;
            stmt += "<TD style=\"color:#F52887; background-color:#FFF380; font-weight:bold;\">" + oddNum.ToString()
                + "/<font style=\"color:#8467D7; background-color:#FFF380; font-weight:bold;\">" + evenNum.ToString() + "</TD>";
            stmt += "</TR>";

            stmt += "</TABLE>";
            //stmt += Util.CreateHTML_Tail();

            return stmt;


        }

        public static string LottoLogos(Database db)
        {
            return "<IMG class=image_icons border=0 src=" + Util.getLottoImage(db) + " height=40 ></H3>\n";
        }

        public static void SetLottoPageLayout(System.Web.UI.HtmlControls.HtmlIframe lottolinks)
        {
            string width = ConfigurationManager.AppSettings["DocWidth"];
            string height = ConfigurationManager.AppSettings["DocHeight"];
            lottolinks.Attributes.Add("width", width);
            lottolinks.Attributes.Add("height", height);
        }
    }
}
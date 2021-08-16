using BusinessTier;
using DataAccessTier;
using Lottotry.BusinessTier;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;

namespace Lottotry.Members
{

    public partial class Lotto : System.Web.UI.Page
    {

        const string fromSite = "Lotto.aspx";
        private clsLotto lotto;
        DataAccessLayer dataAccess = new DataAccessLayer();
        TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
        AutoDraw autoDraw = null;
        
        PotentialNumbers pn = null;
        PotentialNumbers potent = null;
        static bool potentialDrawCacheNeedUpdate = true;
        static bool chartCacheNeedUpdate = true;

        static bool stat3CacheNeedUpdate = false;
        static bool stat4CacheNeedUpdate = false;
        static bool stat6CacheNeedUpdate = false;
        static bool stat7CacheNeedUpdate = false;
        string rootDir = string.Empty;
        int cacheHours = int.Parse(ConfigurationManager.AppSettings["Cache_Hours"]);

        string uid;
        static int prevScale = 7;
        protected void Page_Load(object sender, EventArgs e)
        {
            output.Text = "";
            output.Visible = false;
            uid = HttpContext.Current.User.Identity.Name;
            
 
            //Add jQuery function
            tbTargetDraw4.Attributes.Add("onclick", "highLightBackColor();");
            tbTargetDraw8.Attributes.Add("onclick", "highLightBackColor();");
            tbTargetDraw10.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow3.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow4.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow5.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow6.Attributes.Add("onclick", "highLightBackColor();");
            tbStartRow7.Attributes.Add("onclick", "highLightBackColor();");

            

            Chart1.Visible = false;
            if (!this.IsPostBack)  // Only do below on first time the page is loaded.
            {
                rootDir = Server.MapPath(".");
                rootDir = rootDir.Replace("Members", "XML");

                Util.InitLogoDic();
                Util.InitColumnnsOfLotto_no_bonus();
                Util.InitColumnnsOfLotto(); 
                Util.InitBonusColName();
                Util.InitCacheFile(rootDir);
                Util.InitLottoNumberRanges();
                Util.InitTotalLottoNumbers();


                for (int i = 1; i <= 15; i++)
                {
                    scalesDdl.Items.Add(i.ToString());
                    scalesDdl5.Items.Add(i.ToString());

                }
                scalesDdl.SelectedIndex = 6;
                scalesDdl5.SelectedIndex = 6;

                for (int i = 0; i <= 50; i++)
                {
                    DistDdl.Items.Add(i.ToString());
                    DistDdl.Items[i].Text = i.ToString();
                    DistDdl.Items[i].Value = i.ToString();

                    DistDdl4.Items.Add(i.ToString());
                    DistDdl4.Items[i].Text = i.ToString();
                    DistDdl4.Items[i].Value = i.ToString();
                }
                DistDdl.SelectedIndex = 30;
                DistDdl4.SelectedIndex = 10;

                

                // Fill All DropDownList
                try
                {

                    dataAccess.OpenConnection();
                    
                   
#if false
                    SqlDataReader reader = dataAccess.SpGetLottoName();
                    while (reader.Read())
                    {                
                        DBDdl1.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl2.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl3.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl4.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl5.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl6.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl7.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl8.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl10.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl12.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                        DBDdl13.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                    }
#else
                    int dummy = 1;
                    DataTable dt = dataAccess.SpGetLottoName(dummy);
                    
                    DBDdl1.DataSource = dt;
                    DBDdl1.DataTextField = "Name";
                    DBDdl1.DataValueField = "id";
                    DBDdl1.DataBind();

                    DBDdl2.DataSource = dt;
                    DBDdl2.DataTextField = "Name";
                    DBDdl2.DataValueField = "id";
                    DBDdl2.DataBind();

                    DBDdl3.DataSource = dt;
                    DBDdl3.DataTextField = "Name";
                    DBDdl3.DataValueField = "id";
                    DBDdl3.DataBind();

                    DBDdl4.DataSource = dt;
                    DBDdl4.DataTextField = "Name";
                    DBDdl4.DataValueField = "id";
                    DBDdl4.DataBind();

                    DBDdl5.DataSource = dt;
                    DBDdl5.DataTextField = "Name";
                    DBDdl5.DataValueField = "id";
                    DBDdl5.DataBind();

                    DBDdl6.DataSource = dt;
                    DBDdl6.DataTextField = "Name";
                    DBDdl6.DataValueField = "id";
                    DBDdl6.DataBind();

                    DBDdl7.DataSource = dt;
                    DBDdl7.DataTextField = "Name";
                    DBDdl7.DataValueField = "id";
                    DBDdl7.DataBind();

                    DBDdl8.DataSource = dt;
                    DBDdl8.DataTextField = "Name";
                    DBDdl8.DataValueField = "id";
                    DBDdl8.DataBind();

                    DBDdl10.DataSource = dt;
                    DBDdl10.DataTextField = "Name";
                    DBDdl10.DataValueField = "id";
                    DBDdl10.DataBind();

                    DBDdl12.DataSource = dt;
                    DBDdl12.DataTextField = "Name";
                    DBDdl12.DataValueField = "id";
                    DBDdl12.DataBind();

                    DBDdl13.DataSource = dt;
                    DBDdl13.DataTextField = "Name";
                    DBDdl13.DataValueField = "id";
                    DBDdl13.DataBind();

#endif
                    setDBDropDownSelectedItem(Session["SelectedDBDdl"] != null ? (int)Session["SelectedDBDdl"] : 1);
                   
                    setLoadLottoLogo((Database)int.Parse(DBDdl12.SelectedValue));
                    
                    
                }
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    dataAccess.CloseConnection();
                }
            }
            
        }

        protected void setDBDropDownSelectedItem(int index)
        {
            Session["SelectedDBDdl"] = index;
            DBDdl1.SelectedIndex = index;
            DBDdl2.SelectedIndex = index;
            DBDdl3.SelectedIndex = index;
            DBDdl4.SelectedIndex = index;
            DBDdl5.SelectedIndex = index;
            DBDdl6.SelectedIndex = index;
            DBDdl7.SelectedIndex = index;
            DBDdl8.SelectedIndex = index;
            DBDdl10.SelectedIndex = index;
            DBDdl12.SelectedIndex = index;
            DBDdl13.SelectedIndex = index;
        }

        protected void setLoadLottoLogo(Database db)
        {
            ImagePredict.ImageUrl = Util.getLottoImage(db);
            ImageAuto.ImageUrl = Util.getLottoImage(db);
            ImagePotential.ImageUrl = Util.getLottoImage(db);
            ImageChart.ImageUrl = Util.getLottoImage(db);
        }

        // Statistics 1
        protected void submit1_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 3;
          

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
               dataAccess.CloseConnection();             

               Response.Redirect("/memberLogin.aspx");
            }
            dataAccess.CloseConnection();

            int start = 0;
            int target = 0;

            if (tbTargetRow.Text != "")
            {
                target = int.Parse(tbTargetRow.Text);
            }
            Database db = (Database)int.Parse(DBDdl1.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl1.SelectedIndex);
            
            if (tbStartRow.Text != "")
            {
                start = int.Parse(tbStartRow.Text);
            }

            this.Visible = false;
            long dbExecTime = 0;
            try
            {
                lotto = new BusinessTier.clsLotto(db, fromSite);
                string stmt = lotto.classifyLottoNumbers(start, target, out dbExecTime);

                stopWatch.Stop();
                if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
                {
                    stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                    stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);

                }
                Response.Write(stmt);
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);
            }

        }

        // Statistics 5
        protected void submit2_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 7;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();
            int target = 0;
            if (tbTarget.Text != "")
            {
                target = int.Parse(tbTarget.Text);
            }
            Database db = (Database)int.Parse(DBDdl2.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl2.SelectedIndex);
           
            int dist = int.Parse(DistDdl.SelectedItem.Value);
            int scale = int.Parse(scalesDdl.SelectedItem.Value);

            this.Visible = false;
            long dbExecTime = 0;
            try
            {
                lotto = new BusinessTier.clsLotto(db, fromSite);
                string stmt = lotto.createHTML_ScaleDistMatrix(target,
                                                               dist,
                                                               scale,
                                                               out dbExecTime);

                stopWatch.Stop();
                if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
                {
                    stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                    stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);

                }
                Response.Write(stmt);
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);
            }
        }

        // Statistics 6
        protected void submit3_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 8;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();
            int start = 0;
            int target = 0;
            if (tbStartRow3.Text != "")
            {
                start = int.Parse(tbStartRow3.Text);
            }
            if (tbTargetRow3.Text != "")
            {
                target = int.Parse(tbTargetRow3.Text);
            }
            Database db = (Database)int.Parse(DBDdl3.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl3.SelectedIndex);
            this.Visible = false;
            lotto = new BusinessTier.clsLotto(db, fromSite);
            //CacheDependency dependency = new CacheDependency(Util.getCacheFile(db));
            string stmt = "";

            long dbExecTime = 0;
            int num = Util.retrieveNumFromMethodName(System.Reflection.MethodBase.GetCurrentMethod().Name);
            string cacheKey = Util.getCacheName(db, num);
            if (Cache[cacheKey] == null || stat6CacheNeedUpdate)
            {
                try
                {
                    if (int.Parse(rangeDdl.SelectedItem.Value) == 0)
                    {
                        stmt = lotto.createHTML_NumDist(start, target, out dbExecTime);
                    }
                    else
                    {
                        stmt = lotto.retrieveAllNumDist(start, target, out dbExecTime);
                    }

                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }

                    // cache stmtLottery
                    Cache.Insert(cacheKey,                       // key
                                 stmt,                           // object
                                 null,                      // dependency   
                                 DateTime.Now.AddHours(cacheHours),
                                 Cache.NoSlidingExpiration
                                );
                    stat6CacheNeedUpdate = false;
                }
                catch (Exception exp)
                {                   
                    Response.Write(exp.Message);
                }
            }
            else
            {
                stmt = (string)Cache[cacheKey];

            }
            stopWatch.Stop();
            if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
            {
                stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);
                
            }
            Response.Write(stmt);
        }

        // Statistics 2
        protected void submit4_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 4;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();
            int start = 0;
            int target = 0;
            if (tbStartRow4.Text != "")
            {
                start = int.Parse(tbStartRow4.Text);
            }
            if (tbTargetRow4.Text != "")
            {
                target = int.Parse(tbTargetRow4.Text);
            }
            int distRange = int.Parse(DistDdl4.SelectedItem.Value);
            Database db = (Database)int.Parse(DBDdl4.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl4.SelectedIndex);

            this.Visible = false;
            long dbExecTime = 0;
            try
            {
                lotto = new BusinessTier.clsLotto(db, fromSite);
                string stmt = lotto.highFreqDistribution(start, target, distRange, out dbExecTime);

                stopWatch.Stop();
                if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
                {
                    stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                    stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);
                
                }
                Response.Write(stmt);
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);
            }
        }

        // Statistics 3
        protected void submit5_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 5;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();

            int start = 0;

            if (tbStartRow5.Text != "")
            {
                start = int.Parse(tbStartRow5.Text);
            }
            int target = 0;
            if (tbTargetRow5.Text != "")
            {
                target = int.Parse(tbTargetRow5.Text);
            }

            int scale = int.Parse(scalesDdl5.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl5.SelectedIndex);
            if (prevScale != scale)
            {
                stat3CacheNeedUpdate = true;
            }

            prevScale = scale;

            Database db = (Database)int.Parse(DBDdl5.SelectedItem.Value);

            this.Visible = false;

            //CacheDependency dependency = new CacheDependency(Util.getCacheFile(db));


            lotto = new BusinessTier.clsLotto(db, fromSite);
            string stmt = "";
            long dbExecTime = 0;
            int num = Util.retrieveNumFromMethodName(System.Reflection.MethodBase.GetCurrentMethod().Name);
            string cacheKey = Util.getCacheName(db, num);

            if (Cache[Util.getCacheName(db, num)] == null || stat3CacheNeedUpdate)
            {
                try
                {
                    //string stmtLottery = lotto.segmentFreqBand(start, target, scale, fromSite);
                    stmt = lotto.createAllNumAllDrawsTable(scale, start, target, out dbExecTime);

                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }
                    // cache stmtLottery
                    Cache.Insert(cacheKey,                         // key
                                 stmt,                             // object
                                 null,                             // dependency 
                                 DateTime.Now.AddHours(cacheHours),
                                 Cache.NoSlidingExpiration
                                );
                    stat3CacheNeedUpdate = false;
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message);
                }
            }
            else
            {
                stmt = (string)Cache[cacheKey];
            }
            stopWatch.Stop();
            if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
            {
                stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);

            }
            Response.Write(stmt);
        }

        // Statistics 4
        protected void submit6_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 6;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();

            int start = 0;
            if (tbStartRow6.Text != "")
            {
                start = int.Parse(tbStartRow6.Text);
            }
            int target = 0;
            if (tbTargetRow6.Text != "")
            {
                target = int.Parse(tbTargetRow6.Text);
            }


            Database db = (Database)int.Parse(DBDdl6.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl6.SelectedIndex);

            this.Visible = false;
            lotto = new BusinessTier.clsLotto(db, fromSite);


            //CacheDependency dependency = new CacheDependency(Util.getCacheFile(db));

            string stmt = "";
            long dbExecTime = 0;
            int num = Util.retrieveNumFromMethodName(System.Reflection.MethodBase.GetCurrentMethod().Name);
            string cacheKey = Util.getCacheName(db, num);
            if (Cache[cacheKey] == null || stat4CacheNeedUpdate)
            {
                try
                {
                    stmt = lotto.createAllNumAllDrawsTableByDist(start, target, out dbExecTime);
                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }

                    // cache stmtLottoMax
                    Cache.Insert(cacheKey,                         // key
                                 stmt,                             // object
                                 null,                             // dependency 
                                 DateTime.Now.AddHours(cacheHours),
                                 Cache.NoSlidingExpiration
                                );
                    stat4CacheNeedUpdate = false;
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message);
                }
            }
            else
            {
                stmt = (string)Cache[cacheKey];
            }
            stopWatch.Stop();
            if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
            {
                stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);

            }
            Response.Write(stmt);
        }

        // Statistics 7
        protected void submit7_Click(object sender, EventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Session["DefaultTabbedPanel"] = 9;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();

            int start = 0, target = 0;
            if (tbStartRow7.Text != "")
            {
                start = int.Parse(tbStartRow7.Text.Trim());
            }
            if (tbTargetRow7.Text != "")
            {
                target = int.Parse(tbTargetRow7.Text.Trim());
            }
            Database db = (Database)int.Parse(DBDdl7.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl7.SelectedIndex);

            this.Visible = false;
   
            //CacheDependency dependency = new CacheDependency(Util.getCacheFile(db));
            string stmt = "";
            long dbExecTime = 0;
            int num = Util.retrieveNumFromMethodName(System.Reflection.MethodBase.GetCurrentMethod().Name);
            string cacheKey = Util.getCacheName(db, num);
            if (Cache[cacheKey] == null || stat7CacheNeedUpdate)
            {
                try
                {
                    PotentialNumbers pn = new PotentialNumbers(db, fromSite);
                    stmt = pn.distribution(db, start, target, out dbExecTime);

                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }

                    // cache 
                    Cache.Insert(cacheKey,                         // key
                                 stmt,                             // object
                                 null,                             // dependency 
                                 DateTime.Now.AddHours(cacheHours),
                                 Cache.NoSlidingExpiration
                                );
                    stat7CacheNeedUpdate = false;
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message);
                }
            }
            else
            {
                stmt = (string)Cache[cacheKey];

            }
            stopWatch.Stop();
            if (HttpContext.Current.User.Identity.Name.Equals("hma14"))
            {
                stmt += string.Format("<p class='dbexectime'>Database execution time: {0} ms</p>", dbExecTime);
                stmt += string.Format("<p class='dbexectime'>Round trip execution time: {0} ms</p>", stopWatch.Elapsed.Milliseconds);

            }
            Response.Write(stmt);
        }

        // Auto Draws
        protected void submit8_Click(object sender, EventArgs e)
        {
            Chart1.Visible = false;
            Session["DefaultTabbedPanel"] = 1;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }

            
            dataAccess.CloseConnection();
            int target = 0;
            int numHot, hotMin, hotMax,
                numSemiHot, semiHotMin, semiHotMax,
                numCold, coldMin, coldMax,
                numSemiCold, semiColdMin, semiColdMax,
                numVeryCold, veryColdMin, veryHot,
                algo, range;

            numHot = int.Parse(AutoDrawConfig1.TbHot);
            hotMin = int.Parse(AutoDrawConfig1.TbHotMin);
            hotMax = int.Parse(AutoDrawConfig1.TbHotMax);
            numSemiHot = int.Parse(AutoDrawConfig1.TbSemiHot);
            semiHotMin = int.Parse(AutoDrawConfig1.TbSemiHotMin);
            semiHotMax = int.Parse(AutoDrawConfig1.TbSemiHotMax);
            numCold = int.Parse(AutoDrawConfig1.TbCold);
            coldMin = int.Parse(AutoDrawConfig1.TbColdMin);
            coldMax = int.Parse(AutoDrawConfig1.TbColdMax);
            numSemiCold = int.Parse(AutoDrawConfig1.TbSemiCold);
            semiColdMin = int.Parse(AutoDrawConfig1.TbSemiColdMin);
            semiColdMax = int.Parse(AutoDrawConfig1.TbSemiColdMax);
            numVeryCold = int.Parse(AutoDrawConfig1.TbVeryCold);
            veryColdMin = int.Parse(AutoDrawConfig1.TbVeryColdMin);
            veryHot = int.Parse(AutoDrawConfig1.TbVeryHot);


            algo = int.Parse(AutoDrawConfig1.DdlSelectMode.SelectedValue);
            range = int.Parse(AutoDrawConfig1.DdlRange.SelectedValue);

            if (tbTargetDraw8.Text != "")
            {
                target = int.Parse(tbTargetDraw8.Text);
            }

            Database db = (Database)int.Parse(DBDdl8.SelectedItem.Value);
            //setDBDropDownSelectedItem(DBDdl8.SelectedIndex);
            output.Text = "";
            ConfigForm1.Visible = false;
            AutoDrawConfig1.Visible = false;

            string cachedAutoDraw = "CachedAutoDraw";

            try
            {
                if (Cache[cachedAutoDraw] == null || AutoDrawConfig1.AutoDrawCacheNeedUpdate)
                {
                    autoDraw = new AutoDraw(db, target,
                                            numHot, hotMin, hotMax,
                                            numSemiHot, semiHotMin, semiHotMax,
                                            numCold, coldMin, coldMax,
                                            numSemiCold, semiColdMin, semiColdMax,
                                            numVeryCold, veryColdMin, veryHot,
                                            algo, range
                                        );

                    if (Cache[cachedAutoDraw] != null)
                    {
                        Cache.Remove(cachedAutoDraw);
                    }
                    Cache.Add(cachedAutoDraw,                  // key
                                autoDraw,                      // object
                                null,                          // dependency                              
                                Cache.NoAbsoluteExpiration,    // expiry
                                SlidingExpiration,             // slide exp
                                CacheItemPriority.Default,     // priority
                                null);

                    AutoDrawConfig1.AutoDrawCacheNeedUpdate = false;
                }
                else
                {
                    autoDraw = (AutoDraw)Cache[cachedAutoDraw];
                }

                string stmt = autoDraw.PlayNextDraw(db);
                output.Visible = true;
                output.Text = stmt;
            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
            }
        }

#if false
        protected void submit9_Click(object sender, EventArgs e)
        {
            submit9.Enabled = false;
            //submit9.Visible = false;

            int target = 0;
            if (tbTargetDraw.Text != "")
            {
                target = int.Parse(tbTargetDraw.Text);
            }

            Database db = (Database)int.Parse(DBDdl9.SelectedItem.Value);

            output.Text = "";
            //this.Visible = false;
            try
            {
                PotentialNumbers pn = new PotentialNumbers(db, fromSite);
                string stmt = pn.genNumbersToMatchTargetDraw(target);
                output.Visible = true;
                output.Text = stmt;
            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
            }
            //Thread.SpinWait(1000000);
            Thread.Sleep(5000);
            submit9.Enabled = true;
            //submit9.Visible = true;

        }

#endif

        // Potential
        protected void submit10_Click(object sender, EventArgs e)
        {
            Session["DefaultTabbedPanel"] = 2;
            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();
            int target = 0;
            int numHot, hotMin, hotMax,
                numSemiHot, semiHotMin, semiHotMax,
                numCold, coldMin, coldMax,
                numSemiCold, semiColdMin, semiColdMax,
                numVeryCold, veryColdMin, veryHot,
                sumMin, sumMax, odds;

            numHot = int.Parse(ConfigForm2.TbHot);
            hotMin = int.Parse(ConfigForm2.TbHotMin);
            hotMax = int.Parse(ConfigForm2.TbHotMax);
            numSemiHot = int.Parse(ConfigForm2.TbSemiHot);
            semiHotMin = int.Parse(ConfigForm2.TbSemiHotMin);
            semiHotMax = int.Parse(ConfigForm2.TbSemiHotMax);
            numCold = int.Parse(ConfigForm2.TbCold);
            coldMin = int.Parse(ConfigForm2.TbColdMin);
            coldMax = int.Parse(ConfigForm2.TbColdMax);
            numSemiCold = int.Parse(ConfigForm2.TbSemiCold);
            semiColdMin = int.Parse(ConfigForm2.TbSemiColdMin);
            semiColdMax = int.Parse(ConfigForm2.TbSemiColdMax);
            numVeryCold = int.Parse(ConfigForm2.TbVeryCold);
            veryColdMin = int.Parse(ConfigForm2.TbVeryColdMin);
            veryHot = int.Parse(ConfigForm2.TbVeryHot);
            sumMin = int.Parse(ConfigForm2.TbSumMin);
            sumMax = int.Parse(ConfigForm2.TbSumMax);
            odds = int.Parse(ConfigForm2.TbOdds);

            


            if (tbTargetDraw10.Text != "")
            {
                target = int.Parse(tbTargetDraw10.Text);
            }

            Database db = (Database)int.Parse(DBDdl10.SelectedItem.Value);

            output.Visible = true;
            output.Text = "";
            ConfigForm1.Visible = false;
            ConfigForm2.Visible = false;
            Chart1.Visible = false;

            string cachedPotentialDraw = "CachedPotentialDraw";
            try
            {
                if (Cache[cachedPotentialDraw] == null || potentialDrawCacheNeedUpdate )
                {

                    potent = new PotentialNumbers(db, fromSite);
                    potent.genPotentialNumbers(target,
                                            numHot, hotMin, hotMax,
                                            numSemiHot, semiHotMin, semiHotMax,
                                            numCold, coldMin, coldMax,
                                            numSemiCold, semiColdMin, semiColdMax,
                                            numVeryCold, veryColdMin, veryHot
                                            );
                    potent.genTargetDrawArray(db, target);

                    if (Cache[cachedPotentialDraw] != null)
                    {
                        Cache.Remove(cachedPotentialDraw);
                    }

                    Cache.Add(cachedPotentialDraw,             // key
                                potent,                        // object
                                null,                          // dependency                              
                                Cache.NoAbsoluteExpiration,    // expiry
                                SlidingExpiration,             // slide exp
                                CacheItemPriority.Default,     // priority
                                null);

                    potentialDrawCacheNeedUpdate = false;

                }
                else
                {
                    potent = (PotentialNumbers)Cache[cachedPotentialDraw];
                }

                string stmt = potent.genPotentialDraws();
                output.Text = stmt;

            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
                output.Style.Add("color", "Red");
            }


        }

#if false
        protected void submit11_Click(object sender, EventArgs e)
        {
            int target = 0;
            int numHot, hotMin, hotMax,
                numSemiHot, semiHotMin, semiHotMax,
                numCold, coldMin, coldMax,
                numSemiCold, semiColdMin, semiColdMax,
                numVeryCold, veryColdMin, veryHot,
                sumMin, sumMax, odds;

            numHot = int.Parse(tbHot.Text.Trim());
            hotMin = int.Parse(tbHotMin.Text.Trim());
            hotMax = int.Parse(tbHotMax.Text.Trim());
            numSemiHot = int.Parse(tbSemiHot.Text.Trim());
            semiHotMin = int.Parse(tbSemiHotMin.Text.Trim());
            semiHotMax = int.Parse(tbSemiHotMax.Text.Trim());
            numCold = int.Parse(tbCold.Text.Trim());
            coldMin = int.Parse(tbColdMin.Text.Trim());
            coldMax = int.Parse(tbColdMax.Text.Trim());
            numSemiCold = int.Parse(tbSemiCold.Text.Trim());
            semiColdMin = int.Parse(tbSemiColdMin.Text.Trim());
            semiColdMax = int.Parse(tbSemiColdMax.Text.Trim());
            numVeryCold = int.Parse(tbVeryCold.Text.Trim());
            veryColdMin = int.Parse(tbVeryColdMin.Text.Trim());
            veryHot = int.Parse(tbVeryHot.Text.Trim());
            sumMin = int.Parse(tbSumMin3.Text.Trim());
            sumMax = int.Parse(tbSumMax3.Text.Trim());
            odds = int.Parse(tbOdds3.Text.Trim());



            if (tbTargetDraw3.Text != "")
            {
                target = int.Parse(tbTargetDraw3.Text);
            }

            Database db = (Database)int.Parse(DBDdl11.SelectedItem.Value);

            output.Visible = true;
            output.Text = "";
            tblScope.Visible = false;
            try
            {
                PotentialNumbers pn = new BusinessTier.PotentialNumbers(db, fromSite);
                string stmt = pn.genPotentialNumbers(target,
                                                        numHot, hotMin, hotMax,
                                                        numSemiHot, semiHotMin, semiHotMax,
                                                        numCold, coldMin, coldMax,
                                                        numSemiCold, semiColdMin, semiColdMax,
                                                        numVeryCold, veryColdMin, veryHot,
                                                        sumMin, sumMax, odds
                                                       );

                output.Text = stmt;
            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
            }

        }

#endif

#if false 
        // Predict Draws WITH CACHE
        protected void submit12_Click(object sender, EventArgs e)
        {
            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("/memberLogin.aspx");
            }
            dataAccess.CloseConnection();
            int target = 0;
            int numHot, hotMin, hotMax,
                numSemiHot, semiHotMin, semiHotMax,
                numCold, coldMin, coldMax,
                numSemiCold, semiColdMin, semiColdMax,
                numVeryCold, veryColdMin, veryHot,
                sumMin, sumMax, odds;

            numHot = int.Parse(tbHot2.Text.Trim());
            hotMin = int.Parse(tbHotMin2.Text.Trim());
            hotMax = int.Parse(tbHotMax2.Text.Trim());
            numSemiHot = int.Parse(tbSemiHot2.Text.Trim());
            semiHotMin = int.Parse(tbSemiHotMin2.Text.Trim());
            semiHotMax = int.Parse(tbSemiHotMax2.Text.Trim());
            numCold = int.Parse(tbCold2.Text.Trim());
            coldMin = int.Parse(tbColdMin2.Text.Trim());
            coldMax = int.Parse(tbColdMax2.Text.Trim());
            numSemiCold = int.Parse(tbSemiCold2.Text.Trim());
            semiColdMin = int.Parse(tbSemiColdMin2.Text.Trim());
            semiColdMax = int.Parse(tbSemiColdMax2.Text.Trim());
            numVeryCold = int.Parse(tbVeryCold2.Text.Trim());
            veryColdMin = int.Parse(tbVeryColdMin2.Text.Trim());
            veryHot = int.Parse(tbVeryHot2.Text.Trim());
            sumMin = int.Parse(tbSumMin2.Text.Trim());
            sumMax = int.Parse(tbSumMax2.Text.Trim());
            odds = int.Parse(tbOdds2.Text.Trim());


            if (tbTargetDraw4.Text != "")
            {
                target = int.Parse(tbTargetDraw4.Text);
            }

            Database db = (Database)int.Parse(DBDdl12.SelectedItem.Value);
 
            output.Visible = true;
            output.Text = "";
            tblGen.Visible = false;

            string cachedPredictDraw = "CachedPredictDraw";
            
            try
            {
                if (Cache[cachedPredictDraw] == null || predictDrawCacheNeedUpdate)
                {

                    pn = new BusinessTier.PotentialNumbers(db, fromSite);
                    pn.genPotentialNumbers(target,
                                            numHot, hotMin, hotMax,
                                            numSemiHot, semiHotMin, semiHotMax,
                                            numCold, coldMin, coldMax,
                                            numSemiCold, semiColdMin, semiColdMax,
                                            numVeryCold, veryColdMin, veryHot
                                            );

                    if (Cache[cachedPredictDraw] != null)
                    {
                        Cache.Remove(cachedPredictDraw);
                    }

                    Cache.Add(cachedPredictDraw,             // key
                                pn,                            // object
                                null,                          // dependency                              
                                Cache.NoAbsoluteExpiration,    // expiry
                                SlidingExpiration,             // slide exp
                                CacheItemPriority.Default,     // priority
                                null);

                    predictDrawCacheNeedUpdate = false;

                }
                else
                {
                    pn = (PotentialNumbers) Cache[cachedPredictDraw];
                }
                string stmt = pn.PredictNextDraws(sumMin, sumMax, odds);
                output.Visible = true;
                output.Text = stmt;
            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
            }

        }

#else
        // Predict Draws WITHOUT CACHE
        protected void submit12_Click(object sender, EventArgs e)
        {
            Chart1.Visible = false;
            Session["DefaultTabbedPanel"] = 0;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("~/memberLogin.aspx");
            }

            

            dataAccess.CloseConnection();
            int target = 0;
            int numHot, hotMin, hotMax,
                numSemiHot, semiHotMin, semiHotMax,
                numCold, coldMin, coldMax,
                numSemiCold, semiColdMin, semiColdMax,
                numVeryCold, veryColdMin, veryHot,
                sumMin, sumMax, odds;

            numHot = int.Parse(ConfigForm1.TbHot);
            hotMin = int.Parse(ConfigForm1.TbHotMin);
            hotMax = int.Parse(ConfigForm1.TbHotMax);
            numSemiHot = int.Parse(ConfigForm1.TbSemiHot);
            semiHotMin = int.Parse(ConfigForm1.TbSemiHotMin);
            semiHotMax = int.Parse(ConfigForm1.TbSemiHotMax);
            numCold = int.Parse(ConfigForm1.TbCold);
            coldMin = int.Parse(ConfigForm1.TbColdMin);
            coldMax = int.Parse(ConfigForm1.TbColdMax);
            numSemiCold = int.Parse(ConfigForm1.TbSemiCold);
            semiColdMin = int.Parse(ConfigForm1.TbSemiColdMin);
            semiColdMax = int.Parse(ConfigForm1.TbSemiColdMax);
            numVeryCold = int.Parse(ConfigForm1.TbVeryCold);
            veryColdMin = int.Parse(ConfigForm1.TbVeryColdMin);
            veryHot = int.Parse(ConfigForm1.TbVeryHot);
            sumMin = int.Parse(ConfigForm1.TbSumMin);
            sumMax = int.Parse(ConfigForm1.TbSumMax);
            odds = int.Parse(ConfigForm1.TbOdds);

            if (tbTargetDraw4.Text != "")
            {
                target = int.Parse(tbTargetDraw4.Text);
            }         

            Database db = (Database)int.Parse(DBDdl12.SelectedItem.Value);
           

            output.Visible = true;
            output.Text = "";
            ConfigForm1.Visible = false;

            try
            {
                pn = new PotentialNumbers(db, fromSite);
                pn.genPotentialNumbers(target,
                                        numHot, hotMin, hotMax,
                                        numSemiHot, semiHotMin, semiHotMax,
                                        numCold, coldMin, coldMax,
                                        numSemiCold, semiColdMin, semiColdMax,
                                        numVeryCold, veryColdMin, veryHot
                                        );

                string stmt = pn.PredictNextDraws(sumMin, sumMax, odds);
                output.Visible = true;
                output.Text = stmt;
            }
            catch (Exception exp)
            {
                //Response.Write(exp.Message);
                output.Text = exp.Message;
            }

        }

#endif

        protected void submit13_Click(object sender, EventArgs e)
        {
            Session["DefaultTabbedPanel"] = 10;

            dataAccess.OpenConnection();
            if (!dataAccess.SpIsSameSession(uid, Session.SessionID) || !dataAccess.SpIsLoggedIn(uid))
            {
                dataAccess.CloseConnection();
                Response.Redirect("~/memberLogin.aspx");
            }
            
            dataAccess.CloseConnection();

            int start = 0, target = 0;
            if (tbStartRow13.Text != "")
            {
                start = int.Parse(tbStartRow13.Text.Trim());
            }
            if (tbTargetRow13.Text != "")
            {
                target = int.Parse(tbTargetRow13.Text.Trim());
            }
            Database db = (Database)int.Parse(DBDdl13.SelectedItem.Value);
            
            //CacheDependency dependency = new CacheDependency(Util.getCacheFile(db));

            int num = Util.retrieveNumFromMethodName(System.Reflection.MethodBase.GetCurrentMethod().Name);           
            string cacheKey = Util.getCacheName(db, num);

            Dictionary<int, int> freq = new Dictionary<int, int>();

            if (Cache[cacheKey] == null || chartCacheNeedUpdate)
            {
                try
                {
                    PotentialNumbers pn = new PotentialNumbers(db, fromSite);
                    
                    freq = pn.getChartData(db, start, target);

                    if (Cache[cacheKey] != null)
                    {
                        Cache.Remove(cacheKey);
                    }

                    // cache 
                    Cache.Insert(cacheKey,                         // key
                                 freq,                             // object
                                 null,                             // dependency 
                                 DateTime.Now.AddHours(cacheHours),
                                 Cache.NoSlidingExpiration
                                );
                    chartCacheNeedUpdate = false;
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message);
                }
            }
            else
            {
                freq = (Dictionary<int, int>)Cache[cacheKey];

            }

            var table = new DataTable();
            table.Columns.Add("Freq", typeof(int));
            table.Columns.Add("NumberRange", typeof(string));
            Chart1.Visible = true;
            int totalLottoNumbers = Util.getTotalLottoNumbers(db);
            int columnNumbers = Util.getColumnnsOfLotto(db);

            for (int i = 0; i < freq.Count; ++i)
            {
                var row = table.NewRow();
                
                row["Freq"] = freq[i];

                if (totalLottoNumbers > Util.SMALL_LOTTO_NUMBERS && columnNumbers > Util.SMALL_COLUMN_NUMBERS)
                {
                    row["NumberRange"] = Util.LargeNumberRange[i];
                }
                else
                {
                    row["NumberRange"] = Util.NumberRange[i];
                }
                table.Rows.Add(row);
            }


            Chart1.DataSource = table;
            Chart1.DataBind();
        }

        protected void CalibrateScope_Click(object sender, EventArgs e)
        {

            //tblScope.Visible = true;
            output.Visible = false;
        }

        protected void CalibrateGen_Click(object sender, EventArgs e)
        {
            ConfigForm1.Visible = true;
            output.Text = "";
            output.Visible = false;
        }
        protected void CalibrateAutoDraw_Click(object sender, EventArgs e)
        {
            AutoDrawConfig1.Visible = true;
            output.Text = "";
            output.Visible = false;
        }

        protected void DBDdl12_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = (Database)int.Parse(DBDdl12.SelectedValue);
            setLoadLottoLogo(db);
            setDBDropDownSelectedItem(DBDdl12.SelectedIndex);
            potentialDrawCacheNeedUpdate = true;
            ConfigForm1.setSumMinMax(db);
        }

        protected void DBDdl8_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = (Database)int.Parse(DBDdl8.SelectedValue);
            setLoadLottoLogo(db);
            setDBDropDownSelectedItem(DBDdl8.SelectedIndex);
            AutoDrawConfig1.AutoDrawCacheNeedUpdate = true;

        }

        protected void DBDdl13_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = (Database)int.Parse(DBDdl13.SelectedValue);
            setLoadLottoLogo(db);
            setDBDropDownSelectedItem(DBDdl13.SelectedIndex);
            chartCacheNeedUpdate = true;
            submit13_Click(sender, e);


        }


        

#if false
        protected void scalesDdl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            stat3CacheNeedUpdate = true;
        }
#endif
        protected void DBDdl10_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database db = (Database)int.Parse(DBDdl10.SelectedValue);
            setLoadLottoLogo(db);
            setDBDropDownSelectedItem(DBDdl10.SelectedIndex);
            potentialDrawCacheNeedUpdate = true;
            ConfigForm2.setSumMinMax(db);
        }

       

        protected void tbTargetDraw8_TextChanged(object sender, EventArgs e)
        {
            AutoDrawConfig1.AutoDrawCacheNeedUpdate = true;
        }

        

        protected void tbTargetDraw10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbHot10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbHotMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbHotMax10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSumMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHot10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHotMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHotMax10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSumMax10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiCold10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiColdMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbSemiColdMax10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbOdds10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbCold10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbColdMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbColdMax10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbVeryCold10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbVeryColdMin10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void tbVeryHot10_TextChanged(object sender, EventArgs e)
        {
            potentialDrawCacheNeedUpdate = true;
        }

        protected void CalibratePotential_Click(object sender, EventArgs e)
        {
            ConfigForm2.Visible = true;
            output.Visible = false;
        }

        protected void tbStartRow5_TextChanged(object sender, EventArgs e)
        {
            stat3CacheNeedUpdate = true;
        }

        protected void tbTargetRow5_TextChanged(object sender, EventArgs e)
        {
            stat3CacheNeedUpdate = true;
        }

        protected void tbStartRow6_TextChanged(object sender, EventArgs e)
        {
            stat4CacheNeedUpdate = true;
        }

        protected void tbTargetRow6_TextChanged(object sender, EventArgs e)
        {
            stat4CacheNeedUpdate = true;
        }

        protected void tbStartRow3_TextChanged(object sender, EventArgs e)
        {
            stat6CacheNeedUpdate = true;
        }

        protected void tbTargetRow3_TextChanged(object sender, EventArgs e)
        {
            stat6CacheNeedUpdate = true;
        }

        protected void rangeDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            stat6CacheNeedUpdate = true;
        }

        protected void tbStartRow7_TextChanged(object sender, EventArgs e)
        {
            stat7CacheNeedUpdate = true;
        }

        protected void tbTargetRow7_TextChanged(object sender, EventArgs e)
        {
            stat7CacheNeedUpdate = true;
        }

        protected void rangeDdl_TextChanged(object sender, EventArgs e)
        {
            stat7CacheNeedUpdate = true;
        }


        protected void DBDdl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl1.SelectedIndex);
        }

        protected void DBDdl5_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl5.SelectedIndex);
        }

        protected void DBDdl4_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl4.SelectedIndex);
        }

        protected void DBDdl6_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl6.SelectedIndex);
        }

        protected void DBDdl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl2.SelectedIndex);
        }

        protected void DBDdl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl3.SelectedIndex);
        }

        protected void DBDdl7_SelectedIndexChanged(object sender, EventArgs e)
        {
            setDBDropDownSelectedItem(DBDdl7.SelectedIndex);
        }

        protected void tbStartRow13_TextChanged(object sender, EventArgs e)
        {
            chartCacheNeedUpdate = true;           
        }

        protected void tbTargetRow13_TextChanged(object sender, EventArgs e)
        {
            chartCacheNeedUpdate = true;
        }

       

    }
}



using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

using BusinessTier;

namespace DataAccessTier
{
    public enum DbMode { Add, Update, Delete }
    public enum Database
    {
        Lottery,
        LottoMax,
        BC49,
        FloridaLotto,
        MegaMillions,
        MegaMillions_MegaBall,
        PowerBall,
        PowerBall_PowerBall,
        NYLotto,
        EuroMillions,
        EuroMillions_LuckyStars,
        OZLottoTue,
        SSQ, 
        SSQ_Blue,
        SevenLotto,
        SuperLotto,
        SuperLotto_Rear,
        NYSweetMillion,
        ColoradoLotto,
        FloridaLucky,
        EuroJackpot,
        EuroJackpot_Euros,
        GermanLotto,
        BritishLotto,
        OZLottoSat,
        FloridaFantasy5,
        OZLottoMon,
        OZLottoWed,
        ConnecticutLotto,
        CaSuperlottoPlus,
        CaSuperlottoPlus_Mega,
        NewJerseyPick6Lotto,
        OregonMegabucks,
        NewYorkTake5 =35,
        TexasCashFive = 37,
        DailyGrand,
    }


    public class DataAccessLayer
    {
        private SqlConnection sqlConnection;


        private string connectionString()
        {
            return ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
        }

        public DataAccessLayer()
        {
            try
            {
                if (sqlConnection == null)
                {
                    sqlConnection = new SqlConnection(connectionString());
                }


                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }


            sqlConnection.StatisticsEnabled = true;
        }

        public SqlConnection OpenConnection()
        {

            try
            {
                //sqlConnection = new SqlConnection();
                //sqlConnection.ConnectionString = connectionString();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sqlConnection;
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }



        //public bool LoginAuth(string uid, string pass)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("LoginAuth", sqlConnection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@uname", uid);
        //        cmd.Parameters.AddWithValue("@passwd", pass);
        //        cmd.Prepare();
        //        if (cmd.Connection.State == ConnectionState.Closed)
        //        {
        //            cmd.Connection.Open();
        //        }
        //        return Convert.ToBoolean(cmd.ExecuteScalar());
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public void SpAddBlackList(string uid, string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spBlackList", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@email", email);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool SpIsEmailExistInBlackList(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spIsEmailInBlackList", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }

        }


        public SqlDataReader SelectData(Database db)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SelectAll", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", db);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }

        }

        public SqlDataReader SelectAllOnRangeOfDrawNo(Database dbMode, int startRow, int targetRow)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SelectAllOnRangeOfDrawNo", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", dbMode);
                    cmd.Parameters.AddWithValue("@start", startRow);
                    cmd.Parameters.AddWithValue("@end", targetRow);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

#if false
        public SqlDataReader GetPastRangeDraws(Database dbMode, int targetRow, int range)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetPastRangeDraws", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", dbMode);
                    cmd.Parameters.AddWithValue("@targetRow", targetRow);
                    cmd.Parameters.AddWithValue("@range", range);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }


        public SqlDataReader RetrieveRows(Database dbMode, string op, int targetdrawno)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("RetrieveRows", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", dbMode);
                    cmd.Parameters.AddWithValue("@op", op);
                    cmd.Parameters.AddWithValue("@targetdrawno", targetdrawno);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }
#endif
        public int GetLastRow(Database db)
        {
            try
            {
                // Access Stored Procedure
                using (SqlCommand cmd = new SqlCommand("GetLastRow", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", db);
                    cmd.Prepare();
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }

        }

        public SqlDataReader SpGetTargetDraw(Database db, int drawno)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("GetTargetDraw", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@db", db);
                    cmd.Parameters.AddWithValue("@drawNum", drawno);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        public long CloseConnection(SqlDataReader sqlReader)
        {
            if (sqlReader != null)
            {
                sqlReader.Close();
            }
            IDictionary statistics = sqlConnection.RetrieveStatistics();
            return (long)statistics["ExecutionTime"];
        }

        public long CloseSqlConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();

            IDictionary statistics = sqlConnection.RetrieveStatistics();
            return (long)statistics["ExecutionTime"];
        }

        //Membership related methods - newly added
        private SqlCommand CommandObject(
                                string procName,
                                string[] paramNames,
                                string[] paramValues)
        {
            using (SqlCommand cmd = new SqlCommand(procName, sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (paramNames == null && paramValues == null)
                    return cmd;

                for (int i = 0; i < paramValues.Length; i++)
                {
                    cmd.Parameters.Add(
                        new SqlParameter(paramNames[i], paramValues[i]));
                }

                return cmd;
            }
        }

        private SqlDataReader ExecuteReader(
                                            string procname,
                                            string[] paramNames,
                                            string[] paramValues)
        {



            try
            {
                using (SqlCommand cmd = CommandObject(procname, paramNames, paramValues))
                {
                    return cmd.ExecuteReader();
                }
            }

            catch (Exception ex)
            {
                string error;
                error = ex.Message;
            }


            return null;
        }

        public string SpGetUserFullName(string uid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetUserFullName", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        public int SpCountUsers()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spCountUsers", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }
        public SqlDataReader SpGetClientCloseExpired()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetClientCloseExpired", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }


        public void SpUpdateClientStatus()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spUpdateClientStatus", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        public SqlDataReader SpGetClientExpired()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetClientExpired", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }
        public bool SpIsClientExpired(string userName, string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spIsClientExpired", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", userName);
                    cmd.Parameters.AddWithValue("@email", email);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpRetrieveUserProfile(string uid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spRetrieveUserProfile", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SpRemoveClient(string uid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spRemoveClient", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        public void SpRemoveExpiredClient()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spRemoveExpiredClient", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SpRegisterUser(string userName,
                                    string password,
                                    string userFName,
                                    string userLName,
                                    string userEmail,
                                    string userPhone,
                                    string userStreet,
                                    string userStreet2,
                                    string userCity,
                                    string userProvince,
                                    string userPostalCode,
                                    string userCountry,
                                    string userRole,
                                    DateTime signupDate,
                                    DateTime expiryDate,
                                    bool isLoggedIn)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(password);
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spRegisterUser", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    sqlCmd.Parameters.AddWithValue("@passwordHash", encryptedPwd);
                    sqlCmd.Parameters.AddWithValue("@userFName", userFName);
                    sqlCmd.Parameters.AddWithValue("@userLName", userLName);
                    sqlCmd.Parameters.AddWithValue("@userEmail", userEmail);
                    sqlCmd.Parameters.AddWithValue("@userPhone", userPhone);
                    sqlCmd.Parameters.AddWithValue("@userStreet", userStreet);
                    sqlCmd.Parameters.AddWithValue("@userStreet2", userStreet2);
                    sqlCmd.Parameters.AddWithValue("@userCity", userCity);
                    sqlCmd.Parameters.AddWithValue("@userProvince", userProvince);
                    sqlCmd.Parameters.AddWithValue("@userPostalCode", userPostalCode);
                    sqlCmd.Parameters.AddWithValue("@userCountry", userCountry);
                    sqlCmd.Parameters.AddWithValue("@userRole", userRole);
                    sqlCmd.Parameters.AddWithValue("@signupDate", signupDate);
                    sqlCmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                    sqlCmd.Parameters.AddWithValue("@isLoggedIn", isLoggedIn);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SpRegisterUser(string userName,
                             string password,
                             string userFName,
                             string userLName,
                             string userEmail,
                             string userCity,
                             string userCountry,
                             string userRole,
                             DateTime signupDate,
                             DateTime expiryDate,
                             bool isLoggedIn)
        {
            string encryptedPwd = EncryptionManger.Encrypt(password);
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spRegisterUser", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    sqlCmd.Parameters.AddWithValue("@passwordHash", encryptedPwd);
                    sqlCmd.Parameters.AddWithValue("@userFName", userFName);
                    sqlCmd.Parameters.AddWithValue("@userLName", userLName);
                    sqlCmd.Parameters.AddWithValue("@userEmail", userEmail);
                    sqlCmd.Parameters.AddWithValue("@userCity", userCity);
                    sqlCmd.Parameters.AddWithValue("@userCountry", userCountry);
                    sqlCmd.Parameters.AddWithValue("@userRole", userRole);
                    sqlCmd.Parameters.AddWithValue("@signupDate", signupDate);
                    sqlCmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                    sqlCmd.Parameters.AddWithValue("@isLoggedIn", isLoggedIn);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SpUpdateUser(string userName,
                                    string password,
                                    string userFName,
                                    string userLName,
                                    string userEmail,
                                    string userCity,
                                    string userCountry,
                                    string userRole,
                                    DateTime signupDate,
                                    DateTime expiryDate,
                                    bool isLoggedIn)
        {
            string encryptedPwd = EncryptionManger.Encrypt(password);
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spUpdateUser", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    sqlCmd.Parameters.AddWithValue("@passwordHash", encryptedPwd);
                    sqlCmd.Parameters.AddWithValue("@userFName", userFName);
                    sqlCmd.Parameters.AddWithValue("@userLName", userLName);
                    sqlCmd.Parameters.AddWithValue("@userEmail", userEmail);
                    sqlCmd.Parameters.AddWithValue("@userCity", userCity);
                    sqlCmd.Parameters.AddWithValue("@userCountry", userCountry);
                    sqlCmd.Parameters.AddWithValue("@userRole", userRole);
                    sqlCmd.Parameters.AddWithValue("@signupDate", signupDate);
                    sqlCmd.Parameters.AddWithValue("@expiryDate", expiryDate);
                    sqlCmd.Parameters.AddWithValue("@isLoggedIn", isLoggedIn);

                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void SpUpdateUserInfo(string userName,
                                     string password,
                                     string userFName,
                                     string userLName,
                                     string userEmail
                                    )
        {
            string encryptedPwd = EncryptionManger.Encrypt(password);
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spUpdateUserInfo", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    sqlCmd.Parameters.AddWithValue("@passwordHash", encryptedPwd);
                    sqlCmd.Parameters.AddWithValue("@userFName", userFName);
                    sqlCmd.Parameters.AddWithValue("@userLName", userLName);
                    sqlCmd.Parameters.AddWithValue("@userEmail", userEmail);


                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpRegisterUser(string userName, string passwordHash)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("SpRegisterUser", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUserName = sqlCmd.Parameters.Add(
                        "@userName", SqlDbType.VarChar, 20);
                    paramUserName.Value = userName;

                    SqlParameter paramPassword = sqlCmd.Parameters.Add(
                        "@passwordHash", SqlDbType.VarChar, 200);
                    paramUserName.Value = passwordHash;


                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception verifying pwd " + ex.Message);
            }
        }

        public bool SpIsLoggedIn(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spIsLoggedIn", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", userName);

                    return Convert.ToBoolean(sqlCmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool SpIsSameSession(string uid, string ses)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spIsSameSession", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.Parameters.AddWithValue("@ses", ses);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }

                    return Convert.ToBoolean(sqlCmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpSaveSession(string uid, string ses)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spSaveSession", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.Parameters.AddWithValue("@ses", ses);

                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpClearSession(string uid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spClearSession", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpStoreReceipt(string uid, string transID, string cct, string ccn, string ccExpiryDate,
                                   string fullName, string plan, string startDate, string expiredDate)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spStoreReceipt", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.Parameters.AddWithValue("@TransactionID", transID);
                    sqlCmd.Parameters.AddWithValue("@CCType", cct);
                    sqlCmd.Parameters.AddWithValue("@CCNumber", ccn);
                    sqlCmd.Parameters.AddWithValue("@CCExpiryDate", ccExpiryDate);
                    sqlCmd.Parameters.AddWithValue("@FullName", fullName);
                    sqlCmd.Parameters.AddWithValue("@MemberPlan", plan);
                    sqlCmd.Parameters.AddWithValue("@StartDate", startDate);
                    sqlCmd.Parameters.AddWithValue("@ExpiredDate ", expiredDate);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public SqlDataReader SpGetReceipt(string uid, string transid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetReceipt", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.Parameters.AddWithValue("@TransactionID", transid);
                    return sqlCmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public SqlDataReader SpGetAllReceipt(string uid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetAllReceipt", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    return sqlCmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public SqlDataReader SpGetTransactionID(string uid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetTransactionID", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    return sqlCmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpRemoveReceipt(string uid, string transid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spRemoveReceipt", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.Parameters.AddWithValue("@transactionID", transid);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpRemoveAllReceipts(string uid)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spRemoveAllReceipts", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", uid);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void SpLoggedIn(string userName, int flag)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spLoggedIn", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@uid", userName);
                    sqlCmd.Parameters.AddWithValue("@flag", flag);
                    sqlCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
#if false
        public SqlDataAdapter spViewAllUsers()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spViewAllUsers", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                    return adapter;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all users " + ex.Message);
            }
        }
#endif

        public void SpRegistAsAdmin(string uid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spRegistAsAdmin", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }

        }

        public SqlDataReader spDoesUserExist(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spDoesUserExist", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUserName = sqlCmd.Parameters.Add(
                        "@userName", SqlDbType.VarChar, 20);
                    paramUserName.Value = userName;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception looking up username "
                    + ex.Message);
            }
        }

        public int SpIsUserExist(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spIsUserExist", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    return Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }

        public int SpIsUserExistAndExpired(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spIsUserExistAndExpired", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    return Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }

        public string SpGetUserPwHash(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetUserPwHash", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@userName", userName);
                    if (sqlCmd.Connection.State == ConnectionState.Closed)
                    {
                        sqlCmd.Connection.Open();
                    }
                    return (string) sqlCmd.ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }
        }


        public SqlDataReader spGetUserPwHash(string userName)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetUserPwHash", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUserName = sqlCmd.Parameters.Add(
                        "@userName", SqlDbType.VarChar, 20);
                    paramUserName.Value = userName;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting hash "
                    + ex.Message);
            }
        }

        public SqlDataReader spGetUserGroup(string userName)
        {

            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetUserGroup", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramUserName = sqlCmd.Parameters.Add(
                        "@userName", SqlDbType.VarChar, 20);
                    paramUserName.Value = userName;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting user group "
                    + ex.Message);
            }
        }


        public bool LoginAuth(string uid, string pass)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(pass);
            try
            {
                using (SqlCommand cmd = new SqlCommand("spLoginAuth", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@pwd", encryptedPwd);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }
        public string GetUserRole(string uid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetUserRole", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uid", uid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch
            {
                throw;
            }
        }
        public string SpFindPassword(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spFindPassword", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    if (cmd.ExecuteScalar() != null)
                    {
                        return CryptoManager.GetDecryptPassword(cmd.ExecuteScalar().ToString());
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void SpUpdatePassword(string email, string oldPassword)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(oldPassword);
            try
            {
                using (SqlCommand cmd = new SqlCommand("spUpdatePassword", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@oldPassword", encryptedPwd);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
#if false
        public void SpAddMembers(string uid, string pass, string role,
                                 string fName, string lName, string email,
                                 string postCode)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(pass);
            try
            {
                using (SqlCommand cmd = new SqlCommand("spAddMembers", sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@uid", uid);
                    cmd.Parameters.AddWithValue("@pass", encryptedPwd);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@fName", fName);
                    cmd.Parameters.AddWithValue("@lName", lName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@postCode", postCode);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }


        public SqlDataReader SpGetData()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetData", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }
#endif
        public void SpPurchaseTransaction(string tid, decimal amount, string name)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spPurchaseTransaction", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transID", tid);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@name", name);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public void spRefundTransaction(string tid, decimal amount)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spRefundTransaction", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transID", tid);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }

        public string SpGetAmount(string tid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetAmount", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@transID", tid);
                    if (cmd.Connection.State == ConnectionState.Closed)
                    {
                        cmd.Connection.Open();
                    }
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpGetAllMemberInfo()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetAllMemberInfo", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: " + ex.Message);
            }
        }

        public SqlDataReader SpGetAllMemberInfoOrderBy(string orderby)
        {

            string query_string = "select * from tblUsers order by " + orderby;
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand(query_string, sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.Text;
                    return sqlCmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        //public SqlDataReader SpGetMemberInfoByCity(string cityName)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetMemberInfoByCity", sqlConnection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@cityName", cityName);
        //        if (cmd.Connection.State == ConnectionState.Closed)
        //        {
        //            cmd.Connection.Open();
        //        }
        //        return cmd.ExecuteReader();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public SqlDataReader SpGetEmailByCity(string cityName)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("spGetEmailByCity", sqlConnection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@cityName", cityName);
        //        if (cmd.Connection.State == ConnectionState.Closed)
        //        {
        //            cmd.Connection.Open();
        //        }
        //        return cmd.ExecuteReader();
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        //public SqlDataReader SpShowCityList()
        //{

        //    SqlCommand sqlCmd = new SqlCommand("spShowCityList", sqlConnection);
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    try
        //    {
        //        SqlDataReader reader = sqlCmd.ExecuteReader();
        //        return reader;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception getting all videos "
        //            + ex.Message);
        //    }
        //}

        public string spGetAboutContent()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetAboutContent", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    return sqlCmd.ExecuteScalar().ToString();
                }

            }
            catch (SqlException ex)
            {
                //throw new Exception("Exception getting about content: " + ex.Message);
                throw ex;
            }
        }

        public SqlDataReader spPublishAboutContent(string content)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spPublishAboutContent", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramContent = sqlCmd.Parameters.Add
                        ("@content", SqlDbType.Text);
                    paramContent.Value = content;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing about content" + ex.Message);
            }
        }

        public SqlDataReader spGetTermsContent()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetTermsContent", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting terms content" + ex.Message);
            }
        }

        public SqlDataReader spPublishTermsContent(string content)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spPublishTermsContent", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramContent = sqlCmd.Parameters.Add
                        ("@content", SqlDbType.Text);
                    paramContent.Value = content;
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing terms content" + ex.Message);
            }
        }

        public SqlDataReader SpGetLottoName()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetLottoName", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    return sqlCmd.ExecuteReader();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable SpGetLottoName(int dummy)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("spGetLottoName", sqlConnection))
                {
                    adapter.Fill(dt);
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
#if false
        public SqlDataReader SpSelectAllProvinceState()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spSelectAllProvinceState", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    return sqlCmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpSelectAllCountry()
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spSelectAllCountry", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    return sqlCmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }
        public int SpGetCountryID(string name)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetCountryID", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    return Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }
        public int SpGetProvinceID(string name)
        {
            try
            {
                using (SqlCommand sqlCmd = new SqlCommand("spGetProvinceID", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    return Convert.ToInt32(sqlCmd.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }
#endif
        public string[] SpAllDrawNumbers(Database db, string prefixText)
        {
            try
            {
                OpenConnection();
                using (SqlCommand sqlCmd = new SqlCommand("spAllDrawNumbers", sqlConnection))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@db", db);
                    sqlCmd.Parameters.AddWithValue("@dnum", prefixText);
                    SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    List<string> nums = new List<string>();
                    while (dr.Read())
                    {
                        nums.Add(dr["DrawNumber"].ToString());
                    }
                    return nums.ToArray();
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
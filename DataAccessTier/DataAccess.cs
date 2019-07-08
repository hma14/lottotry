using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

using BusinessTier;

namespace DataAccessTier
{
    public class DataAccessLayer
    {

        private SqlConnection sqlConnection;

        private string LocalConnectionString()
        {
            return
            ConfigurationManager.ConnectionStrings["RemoteSqlServer"].ConnectionString;
        }

        public SqlConnection OpenConnection()
        {

            try
            {
                sqlConnection = new SqlConnection();
                sqlConnection.ConnectionString = LocalConnectionString();
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                string error;
                error = ex.Message;
            }
            return sqlConnection;
        }

        public void CloseConnection()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
        }

        private SqlCommand CommandObject(
                                        string procName,
                                        string[] paramNames,
                                        string[] paramValues)
        {
            SqlCommand cmd = new SqlCommand(procName, sqlConnection);
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

        private SqlDataReader ExecuteReader(
                                            string procname,
                                            string[] paramNames,
                                            string[] paramValues)
        {

            SqlCommand cmd = CommandObject(procname, paramNames, paramValues);

            try
            {
                return cmd.ExecuteReader();
            }

            catch (Exception ex)
            {
                string error;
                error = ex.Message;
            }

            return null;
        }

        public SqlDataReader SpGetLottoName()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("spGetLottoName", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.Connection.State == ConnectionState.Closed)
                        cmd.Connection.Open();
                    return cmd.ExecuteReader();
                }
            }
            catch
            {
                throw;
            }
        }

        public string SpGetUserFullName(string uid)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetUserFullName", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteScalar().ToString();

            }
            catch
            {
                throw;
            }
        }

        public int SpCountUsers()
        {
            try {
                SqlCommand cmd = new SqlCommand("spCountUsers", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return Convert.ToInt32(cmd.ExecuteScalar());
            } catch 
            {
                throw;
            }
        }
        public SqlDataReader SpGetClientCloseExpired()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetClientCloseExpired", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
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
                SqlCommand cmd = new SqlCommand("spGetClientExpired", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
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
                SqlCommand cmd = new SqlCommand("spRetrieveUserProfile", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
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
                SqlCommand cmd = new SqlCommand("spRemoveClient", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
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
                SqlCommand cmd = new SqlCommand("spRemoveExpiredClient", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
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
                                    DateTime expiryDate)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(password);
            try
            {
                SqlCommand sqlCmd = new SqlCommand("spRegisterUser", sqlConnection);
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
                if (sqlCmd.Connection.State == ConnectionState.Closed)
                {
                    sqlCmd.Connection.Open();
                }
                sqlCmd.ExecuteNonQuery();
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
                                    string userPhone,
                                    string userStreet,
                                    string userStreet2,
                                    string userCity,
                                    string userProvince,
                                    string userPostalCode,
                                    string userCountry,
                                    string userRole)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(password);
            try
            {
                SqlCommand sqlCmd = new SqlCommand("spUpdateUser", sqlConnection);
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
                if (sqlCmd.Connection.State == ConnectionState.Closed)
                {
                    sqlCmd.Connection.Open();
                }
                sqlCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpRegisterUser(string userName, string passwordHash)
        {

            SqlCommand sqlCmd = new SqlCommand("SpRegisterUser", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = sqlCmd.Parameters.Add(
                "@userName", SqlDbType.VarChar, 20);
            paramUserName.Value = userName;

            SqlParameter paramPassword = sqlCmd.Parameters.Add(
                "@passwordHash", SqlDbType.VarChar, 200);
            paramUserName.Value = passwordHash;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception verifying pwd " + ex.Message);
            }
        }

        public SqlDataAdapter spViewAllUsers()
        {

            SqlCommand sqlCmd = new SqlCommand("spViewAllUsers", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all users " + ex.Message);
            }
        }
        public void SpRegistAsAdmin(string uid)
        {
            try {
                SqlCommand cmd = new SqlCommand("spRegistAsAdmin", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
            } catch {
                throw;
            }

        }

        public SqlDataReader spDoesUserExist(string userName)
        {

            SqlCommand sqlCmd = new SqlCommand("spDoesUserExist", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = sqlCmd.Parameters.Add(
                "@userName", SqlDbType.VarChar, 20);
            paramUserName.Value = userName;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception looking up username "
                    + ex.Message);
            }
        }

        public bool SpIsUserExist(string userName)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand("spIsUserExist", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userName", userName);
                if (sqlCmd.Connection.State == ConnectionState.Closed)
                {
                    sqlCmd.Connection.Open();
                }
                return Convert.ToBoolean(sqlCmd.ExecuteScalar());
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
                SqlCommand sqlCmd = new SqlCommand("spGetUserPwHash", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@userName", userName);
                if (sqlCmd.Connection.State == ConnectionState.Closed)
                {
                    sqlCmd.Connection.Open();
                }
                return CryptoManager.GetDecryptPassword(sqlCmd.ExecuteScalar().ToString());
            }
            catch
            {
                throw;
            }
        }


        public SqlDataReader spGetUserPwHash(string userName)
        {

            SqlCommand sqlCmd = new SqlCommand("spGetUserPwHash", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = sqlCmd.Parameters.Add(
                "@userName", SqlDbType.VarChar, 20);
            paramUserName.Value = userName;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting hash "
                    + ex.Message);
            }
        }

        public SqlDataReader spGetUserGroup(string userName)
        {

            SqlCommand sqlCmd = new SqlCommand("spGetUserGroup", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = sqlCmd.Parameters.Add(
                "@userName", SqlDbType.VarChar, 20);
            paramUserName.Value = userName;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting user group "
                    + ex.Message);
            }
        }

        public SqlDataReader spAddEmbeddedVideo(string videoTitle,
                                                    string videoURL,
                                                    string videoDescription)
        {

            SqlCommand sqlCmd = new SqlCommand("spAddEmbeddedVideo", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramVideoTitle = sqlCmd.Parameters.Add(
            "@videoTitle", SqlDbType.VarChar, 100);
            paramVideoTitle.Value = videoTitle;

            SqlParameter paramVideoURL = sqlCmd.Parameters.Add(
            "@videoURL", SqlDbType.VarChar, 400);
            paramVideoURL.Value = videoURL;

            SqlParameter paramVideoDescription = sqlCmd.Parameters.Add(
            "@videoDescription", SqlDbType.VarChar, 100);
            paramVideoDescription.Value = videoDescription;



            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding embedded video " + ex.Message);
            }
        }


        public SqlDataReader spGetAllVideos()
        {

            SqlCommand sqlCmd = new SqlCommand("spGetAllVideos", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all videos "
                    + ex.Message);
            }
        }


        public SqlDataReader spAddVideoComment
            (
                string videoCommentText,
                string videoID,
                string videoCommentUserName,
                string videoCommentPostMonth,
                string videoCommentPostDay,
                int videoCommentPostHour,
                int videoCommentPostMinute,
                int videoCommentPostYear
            )
        {
            SqlCommand sqlCmd = new SqlCommand("spAddVideoComment", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = sqlCmd.Parameters.Add(
                "@videoCommentText", SqlDbType.VarChar, 500);
            param1.Value = videoCommentText;

            SqlParameter param2 = sqlCmd.Parameters.Add(
                "@videoID", SqlDbType.VarChar, 255);
            param2.Value = videoID;

            SqlParameter param3 = sqlCmd.Parameters.Add(
                "@videoCommentUserName", SqlDbType.VarChar, 20);
            param3.Value = videoCommentUserName;

            SqlParameter param4 = sqlCmd.Parameters.Add(
                "@videoCommentPostMonth", SqlDbType.VarChar, 20);
            param4.Value = videoCommentPostMonth;

            SqlParameter param5 = sqlCmd.Parameters.Add(
                "@videoCommentPostDay", SqlDbType.VarChar, 20);
            param5.Value = videoCommentPostDay;

            SqlParameter param6 = sqlCmd.Parameters.Add(
                "@videoCommentPostHour", SqlDbType.Int);
            param6.Value = videoCommentPostHour;

            SqlParameter param7 = sqlCmd.Parameters.Add(
                "@videoCommentPostMinute", SqlDbType.Int);
            param7.Value = videoCommentPostMinute;

            SqlParameter param8 = sqlCmd.Parameters.Add(
                "@videoCommentPostYear", SqlDbType.Int);
            param8.Value = videoCommentPostYear;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding video comment "
                    + ex.Message);
            }

        }

        public SqlDataReader spGetVideoCommentsByVideoID(string videoID)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetVideoCommentsByVideoID", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = sqlCmd.Parameters.Add(
                "@videoID", SqlDbType.VarChar, 255);
            param1.Value = videoID;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting video comments by ID "
                    + ex.Message);
            }

        }

        public SqlDataReader spGetVideoByID(string videoID)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetVideoByID", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = sqlCmd.Parameters.Add(
                "@videoID", SqlDbType.VarChar, 255);
            param1.Value = videoID;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting video by ID "
                    + ex.Message);
            }

        }

        public bool LoginAuth(string uid, string pass)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(pass);
            try
            {
                SqlCommand cmd = new SqlCommand("spLoginAuth", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@pwd", encryptedPwd);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return Convert.ToBoolean(cmd.ExecuteScalar());
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
                SqlCommand cmd = new SqlCommand("spGetUserRole", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uid", uid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteScalar().ToString();
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
                SqlCommand cmd = new SqlCommand("spFindPassword", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return CryptoManager.GetDecryptPassword(cmd.ExecuteScalar().ToString());
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
                SqlCommand cmd = new SqlCommand("spUpdatePassword", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@oldPassword", encryptedPwd);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void SpAddMembers(string uid, string pass, string role,
                                 string fName, string lName, string email,
                                 string postCode)
        {
            string encryptedPwd = CryptoManager.GetEncryptPassword(pass);
            try {
                SqlCommand cmd = new SqlCommand("spAddMembers", sqlConnection);
                cmd.Parameters.AddWithValue("@uid",uid);
                cmd.Parameters.AddWithValue("@pass", encryptedPwd);
                cmd.Parameters.AddWithValue("@role",role);
                cmd.Parameters.AddWithValue("@fName",fName);
                cmd.Parameters.AddWithValue("@lName",lName);
                cmd.Parameters.AddWithValue("@email",email);
                cmd.Parameters.AddWithValue("@postCode",postCode);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
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
                SqlCommand cmd = new SqlCommand("spGetData", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public void SpPurchaseTransaction(string tid, decimal amount, string name)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spPurchaseTransaction", sqlConnection);
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
            catch
            {
                throw;
            }
        }

        public void spRefundTransaction(string tid, decimal amount) 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spRefundTransaction", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transID", tid);
                cmd.Parameters.AddWithValue("@amount", amount);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
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
                SqlCommand cmd = new SqlCommand("spGetAmount", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transID", tid);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteScalar().ToString();
            }
            catch
            {
                throw;
            }
        }
        public SqlDataReader SpGetAllMemberInfo()
           // public SqlDataReader SpGetAllEmailAddress()
        {

            SqlCommand sqlCmd = new SqlCommand("spGetAllMemberInfo", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all videos "
                    + ex.Message);
            }
        }


        public SqlDataReader spAddSurveyTemplate(string title)
        {
            SqlCommand sqlCmd = new SqlCommand("spAddSurveyTemplate", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramUserName = sqlCmd.Parameters.Add(
            "@templateName", SqlDbType.Text);
            paramUserName.Value = title;

            SqlParameter paramTemplateStatus= sqlCmd.Parameters.Add(
            "@templateStatus", SqlDbType.VarChar, 20);
            paramTemplateStatus.Value = "INCOMPLETE";

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding survey template" + ex.Message);
            }
        }

        public SqlDataReader spAddQuestionToSurvey(
            string questionText,
            string questionType,
            string surveyID,
            string answer1,
            string answer2,
            string answer3,
            string answer4,
            string answer5,
            string answer6,
            string answer7,
            string answer8,
            string answer9,
            string answer10
            )
        {
            SqlCommand sqlCmd = new SqlCommand("spAddQuestionToSurvey", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramQuestionText = sqlCmd.Parameters.Add(
            "@questionText", SqlDbType.Text);
            paramQuestionText.Value = questionText;

            SqlParameter paramQuestionType = sqlCmd.Parameters.Add(
            "@questionType", SqlDbType.Text);
            paramQuestionType.Value = questionType;

            SqlParameter paramSurveyID = sqlCmd.Parameters.Add(
            "@surveyID", SqlDbType.VarChar, 255);
            paramSurveyID.Value = surveyID;

            SqlParameter paramAnswer1 = sqlCmd.Parameters.Add(
            "@answer1", SqlDbType.Text);
            paramAnswer1.Value = answer1;

            SqlParameter paramAnswer2 = sqlCmd.Parameters.Add(
            "@answer2", SqlDbType.Text);
            paramAnswer2.Value = answer2;


            SqlParameter paramAnswer3 = sqlCmd.Parameters.Add(
            "@answer3", SqlDbType.Text);
            paramAnswer3.Value = answer3;


            SqlParameter paramAnswer4 = sqlCmd.Parameters.Add(
            "@answer4", SqlDbType.Text);
            paramAnswer4.Value = answer4;


            SqlParameter paramAnswer5 = sqlCmd.Parameters.Add(
            "@answer5", SqlDbType.Text);
            paramAnswer5.Value = answer5;


            SqlParameter paramAnswer6 = sqlCmd.Parameters.Add(
            "@answer6", SqlDbType.Text);
            paramAnswer6.Value = answer6;


            SqlParameter paramAnswer7 = sqlCmd.Parameters.Add(
            "@answer7", SqlDbType.Text);
            paramAnswer7.Value = answer7;

            SqlParameter paramAnswer8 = sqlCmd.Parameters.Add(
            "@answer8", SqlDbType.Text);
            paramAnswer8.Value = answer8;

            SqlParameter paramAnswer9 = sqlCmd.Parameters.Add(
            "@answer9", SqlDbType.Text);
            paramAnswer9.Value = answer9;

            SqlParameter paramAnswer10 = sqlCmd.Parameters.Add(
            "@answer10", SqlDbType.Text);
            paramAnswer10.Value = answer10;
        
            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding survey template" + ex.Message);
            }
        }
      

        public SqlDataAdapter SpTblMLA_PopulateDDL()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMLA_PopulateDDL", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return adapter;
            }
            catch
            {
                throw;
            }
        }

        public SqlDataAdapter SpTblMP_PopulateDDL()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMP_PopulateDDL", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return adapter;
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMLA_AddMember(string mlaConstituency, string mlaFirstName, string mlaLastName, string mlaParty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMLA_AddMember", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("mlaConstituency", mlaConstituency);
                cmd.Parameters.AddWithValue("mlaFirstName", mlaFirstName);
                cmd.Parameters.AddWithValue("mlaLastName", mlaLastName);
                cmd.Parameters.AddWithValue("mlaParty", mlaParty);

                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMP_AddMember(string mpConstituency, string mpFirstName, string mpLastName, string mpParty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMP_AddMember", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("mpConstituency", mpConstituency);
                cmd.Parameters.AddWithValue("mpFirstName", mpFirstName);
                cmd.Parameters.AddWithValue("mpLastName", mpLastName);
                cmd.Parameters.AddWithValue("mpParty", mpParty);

                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMLA_EditMember(string mlaID, string mlaConstituency, string mlaFirstName, string mlaLastName, string mlaParty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMLA_EditMember", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mlaID", mlaID);
                cmd.Parameters.AddWithValue("mlaConstituency", mlaConstituency);
                cmd.Parameters.AddWithValue("mlaFirstName", mlaFirstName);
                cmd.Parameters.AddWithValue("mlaLastName", mlaLastName);
                cmd.Parameters.AddWithValue("mlaParty", mlaParty);

                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMP_EditMember(string mpID, string mpConstituency, string mpFirstName, string mpLastName, string mpParty)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMP_EditMember", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mpID", mpID);
                cmd.Parameters.AddWithValue("mpConstituency", mpConstituency);
                cmd.Parameters.AddWithValue("mpFirstName", mpFirstName);
                cmd.Parameters.AddWithValue("mpLastName", mpLastName);
                cmd.Parameters.AddWithValue("mpParty", mpParty);

                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMLA_ViewAll(string mlaID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMLA_ViewAll", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mlaID", mlaID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblMP_ViewAll(string mpID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblMP_ViewAll", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mpID", mpID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ViewMLA(string mlaID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ViewMLA", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mlaID", mlaID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpInsertCommentByMLA(string mlaID, string UserName, string commentPostYear,
            string commentPostMonth, string commentPostDay, string comments)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spInsertCommentByMLA", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mlaID", mlaID);
                cmd.Parameters.AddWithValue("UserName", UserName);
                cmd.Parameters.AddWithValue("@commentPostYear", commentPostYear);
                cmd.Parameters.AddWithValue("@commentPostMonth", commentPostMonth);
                cmd.Parameters.AddWithValue("@commentPostDay", commentPostDay);
                cmd.Parameters.AddWithValue("comments", comments);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ViewMP(string mpID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ViewMP", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mpID", mpID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpInsertCommentByMP(string mpID, string UserName, string commentPostYear,
            string commentPostMonth, string commentPostDay, string comments)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spInsertCommentByMP", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mpID", mpID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@commentPostYear", commentPostYear);
                cmd.Parameters.AddWithValue("@commentPostMonth", commentPostMonth);
                cmd.Parameters.AddWithValue("@commentPostDay", commentPostDay);
                cmd.Parameters.AddWithValue("@comments", comments);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ClearCommentsMLA(string mlaID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ClearCommentsMLA", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mlaID", mlaID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ClearCommentsMP(string mpID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ClearCommentsMP", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mpID", mpID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ClearIndCommentMLA(string comment_Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ClearIndCommentMLA", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@comment_Id", comment_Id);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblComments_ClearIndCommentMP(string comment_Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblComments_ClearIndCommentMP", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@comment_Id", comment_Id);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpTblQuotes_GetQuote(string quoteID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spTblQuotes_GetQuote", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@quoteID", quoteID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataReader SpGetMemberInfoByCity(string cityName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetMemberInfoByCity", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cityName", cityName);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }

        public SqlDataAdapter spGetQuestionCount(string surveyID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetQuestionCount", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@surveyID", surveyID));
                
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return adapter;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting number of questions" + ex.Message);
            }
        }

        public SqlDataReader spModifySurveyStatus(string surveyID, string surveyStatus)
        {
            SqlCommand sqlCmd = new SqlCommand("spModifySurveyStatus", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyID = sqlCmd.Parameters.Add(
            "@surveyID", SqlDbType.Text);
            paramSurveyID.Value = surveyID;

            SqlParameter paramSurveyStatus = sqlCmd.Parameters.Add(
            "@surveyStatus", SqlDbType.Text);
            paramSurveyStatus.Value = surveyStatus;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception modifying survey status" + ex.Message);
            }
        }

        public SqlDataReader spGetAllSurveyTemplates()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetAllSurveyTemplates", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all survey templates" + ex.Message);
            }
        }

        public SqlDataReader spGetCompletedSurveys()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetCompletedSurveys", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting completed surveys" + ex.Message);
            }
        }


        public SqlDataReader spGetQuestionsBySurveyID(string surveyID)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetQuestionsBySurveyID", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyID = sqlCmd.Parameters.Add
                ("@surveyID", SqlDbType.VarChar, 255);
            paramSurveyID.Value = surveyID;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting questions by survey ID" + ex.Message);
            }
        }

        public SqlDataReader spAddAnsweredQuestion(
            string questionID,
            string userName,
            string selectedAnswer)
        {
            SqlCommand sqlCmd = new SqlCommand("spAddAnsweredQuestion", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramQuestionID = sqlCmd.Parameters.Add
                ("@questionID", SqlDbType.VarChar, 255);
            paramQuestionID.Value = questionID;

            SqlParameter paramUserName = sqlCmd.Parameters.Add
                ("@userName", SqlDbType.VarChar, 20);
            paramUserName.Value = userName;

            SqlParameter paramSelectedAnswer = sqlCmd.Parameters.Add
                ("@selectedAnswer", SqlDbType.Int);
            paramSelectedAnswer.Value = selectedAnswer;


            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding an answered question" + ex.Message);
            }
        }

     
        public SqlDataReader SpGetEmailByCity(string cityName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spGetEmailByCity", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cityName", cityName);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }
        }
        public SqlDataReader SpShowCityList()
        {

            SqlCommand sqlCmd = new SqlCommand("spShowCityList", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all videos "
                    + ex.Message);
            }
        }

        public SqlDataReader spAddNews(
            string newsTitle,
	        string newsText,
	        string newsPostYear,
	        string newsPostMonth,
	        string newsPostDay,
	        string newsPostHour,
	        string newsPostMinute
           )
        {
            SqlCommand sqlCmd = new SqlCommand("spAddNews", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramNewsTitle = sqlCmd.Parameters.Add(
            "@newsTitle", SqlDbType.Text);
            paramNewsTitle.Value = newsTitle;

            SqlParameter paramText = sqlCmd.Parameters.Add(
            "@newsText", SqlDbType.Text);
            paramText.Value = newsText;

            SqlParameter paramNewsPostYear = sqlCmd.Parameters.Add(
            "@newsPostYear", SqlDbType.Text);
            paramNewsPostYear.Value = newsPostYear;

            SqlParameter paramNewsPostMonth = sqlCmd.Parameters.Add(
            "@newsPostMonth", SqlDbType.Text);
            paramNewsPostMonth.Value = newsPostMonth;

            SqlParameter paramNewsPostDay = sqlCmd.Parameters.Add(
            "@newsPostDay", SqlDbType.Text);
            paramNewsPostDay.Value = newsPostDay;

            
            SqlParameter paramNewsPostHour = sqlCmd.Parameters.Add(
            "@newsPostHour", SqlDbType.Text);
            paramNewsPostHour.Value = newsPostHour;

            
            SqlParameter paramNewsPostMinute = sqlCmd.Parameters.Add(
            "@newsPostMinute", SqlDbType.Text);
            paramNewsPostMinute.Value = newsPostMinute;

         

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception adding news item " + ex.Message);
            }
        }

        public SqlDataReader spGetNewsByID(string newsID)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetNewsByID", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramNewsID = sqlCmd.Parameters.Add
                ("@newsID", SqlDbType.VarChar, 255);
            paramNewsID.Value = newsID;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting news by ID" + ex.Message);
            }
        }

        public SqlDataReader spGetAllNews()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetAllNews", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting all news" + ex.Message);
            }
        }

        public SqlDataReader spGetAboutContent()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetAboutContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting about content" + ex.Message);
            }
        }

        public SqlDataReader spPublishAboutContent(string content)
        {
            SqlCommand sqlCmd = new SqlCommand("spPublishAboutContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramContent= sqlCmd.Parameters.Add
                ("@content", SqlDbType.Text);
            paramContent.Value = content;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing about content" + ex.Message);
            }
        }


        public SqlDataReader spGetLinksContent()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetLinksContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting links content" + ex.Message);
            }
        }

        public SqlDataReader spPublishLinksContent(string content)
        {
            SqlCommand sqlCmd = new SqlCommand("spPublishLinksContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramContent = sqlCmd.Parameters.Add
                ("@content", SqlDbType.Text);
            paramContent.Value = content;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing links content" + ex.Message);
            }
        }

        public SqlDataReader spGetTermsContent()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetTermsContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting terms content" + ex.Message);
            }
        }

        public SqlDataReader spPublishTermsContent(string content)
        {
            SqlCommand sqlCmd = new SqlCommand("spPublishTermsContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramContent = sqlCmd.Parameters.Add
                ("@content", SqlDbType.Text);
            paramContent.Value = content;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing terms content" + ex.Message);
            }
        }

        public SqlDataReader spGetDefaultContent()
        {
            SqlCommand sqlCmd = new SqlCommand("spGetDefaultContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting default content" + ex.Message);
            }
        }

        public SqlDataReader spPublishDefaultContent(string content)
        {
            SqlCommand sqlCmd = new SqlCommand("spPublishDefaultContent", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramContent = sqlCmd.Parameters.Add
                ("@content", SqlDbType.Text);
            paramContent.Value = content;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception publishing default content" + ex.Message);
            }
        }

        public SqlDataReader spGetSurveyAnswersByID(string surveyID, string questionID)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetSurveyAnswersByID", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyID = sqlCmd.Parameters.Add
                ("@surveyID", SqlDbType.VarChar, 255);
            paramSurveyID.Value = surveyID;


            SqlParameter paramQuestionID = sqlCmd.Parameters.Add
                ("@questionID", SqlDbType.VarChar, 255);
            paramQuestionID.Value = questionID;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting survey answers" + ex.Message);
            }
        }


        public SqlDataReader spGetQuestionTextByIndex(string surveyID, string questionIndex)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetQuestionTextByIndex", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramSurveyID = sqlCmd.Parameters.Add
                ("@surveyID", SqlDbType.VarChar, 255);
            paramSurveyID.Value = surveyID;

            SqlParameter paramQuestionIndex = sqlCmd.Parameters.Add
            ("@questionIndex", SqlDbType.Int);
            paramQuestionIndex.Value = questionIndex;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting question text" + ex.Message);
            }
        }

        public SqlDataReader spGetAnswerCount(string questionID, string answerIndex)
        {
            SqlCommand sqlCmd = new SqlCommand("spGetAnswerCount", sqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            SqlParameter paramQuestionID = sqlCmd.Parameters.Add
                ("@questionID", SqlDbType.VarChar, 255);
            paramQuestionID.Value = questionID;

            SqlParameter paramAnswerIndex = sqlCmd.Parameters.Add
            ("@answerIndex", SqlDbType.Int);
            paramAnswerIndex.Value = answerIndex;

            try
            {
                SqlDataReader reader = sqlCmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception getting answer count" + ex.Message);
            }
        }
        public void SpDeleteNews(string newsID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteNews", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsID", newsID);
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DataAccessTier;

namespace DataAccessTier
{

    public class UserAuthManager
    {
        private static DataAccessLayer dbManager = new DataAccessLayer();

        public static class USER_GROUPS
        {
            public const string ADMIN = "admin";
            public const string NORMAL = "normal";
            public const string BANNED = "banned";
        }

#if true
        public static bool AuthUser(string userName, string password)
        {
            dbManager.OpenConnection();
            if (dbManager.SpIsUserExist(userName) > 0)
            {
                string encryptedPasswordFromDB = dbManager.SpGetUserPwHash(userName);
                string encryptedPasswordFromInput= EncryptionManger.Encrypt(password);     
                dbManager.CloseConnection();
                return encryptedPasswordFromDB.Equals(encryptedPasswordFromInput);
            }
            else
            {
                dbManager.CloseConnection();
                return false;
            }
        }
#else
        public static bool AuthUser(string userName, string password, ref string retData)
        {
            dbManager.OpenConnection();
            if (dbManager.SpIsUserExist(userName))
            {
                string passwordHashWithSalt = dbManager.SpGetUserPwHash(userName);
                string Salt = passwordHashWithSalt.Substring(
                                                    passwordHashWithSalt.Length -
                                                    (EncryptionManger.saltSize + 3));

                string suppliedPwAndSalt = EncryptionManger.CreatePasswordHash(password, Salt);

                retData = Salt;
                dbManager.CloseConnection();
                return passwordHashWithSalt.Equals(suppliedPwAndSalt);
            }
            else
            {
                dbManager.CloseConnection();
                return false;
            }
        }
#endif
    }
}

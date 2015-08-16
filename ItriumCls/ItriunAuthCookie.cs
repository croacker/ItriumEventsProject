using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ItriumCls
{
    /// <summary>
    /// Куки для авторизации
    /// </summary>
    class ItriumAuthCookie : CookieContainer
    {
        private const string COOKIE_NAME = "itrium_auth";
        private const string COOKIE_DEFAULT = "iZDG1emZcb81FyDnJYP3yleW5X/7necQqwVBfahn+Hg=";

        public ItriumAuthCookie()
        {
            init();
        }

        private void init()
        {
            Add(new Uri(AppProperties.WS_URL), new Cookie(COOKIE_NAME, COOKIE_DEFAULT));
        }

        /// <summary>
        /// Получить значение хеша для авторизации
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        private string getAuthValue(string userName, string userPassword)
        {
            string result = string.Empty;
            StringBuilder stringBuider = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] hashResult = hash.ComputeHash(enc.GetBytes(userName + ":" + userPassword));
                result = Convert.ToBase64String(hashResult);
            }
            return result;
        }
    }
}

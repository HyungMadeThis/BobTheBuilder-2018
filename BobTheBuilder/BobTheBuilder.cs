using UnityEngine;
using System.Text;
using System;

namespace BobBuildTools
{
    public static class BobTheBuilder
    {
        private static string AUTO_BUILD_CONTENT_KEY = "BOB_AUTO_BUILD_CONTENT";
        public static bool GetAutoBuildContent()
        {
            if (PlayerPrefs.HasKey(AUTO_BUILD_CONTENT_KEY))
            {
                return PlayerPrefs.GetInt(AUTO_BUILD_CONTENT_KEY) == 1;
            }
            else
            {
                return false;
            }
        }
        public static void SetAutoBuildContent(bool val)
        {
            PlayerPrefs.SetInt(AUTO_BUILD_CONTENT_KEY, val ? 1 : 0);
        }

        private static string IDENTIFIER_SHOW_PRODUCT_NAME = "BOB_SHOW_PRODUCT_NAME";
        public static bool GetIdentifierShowProductName()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_SHOW_PRODUCT_NAME))
            {
                return PlayerPrefs.GetInt(IDENTIFIER_SHOW_PRODUCT_NAME) == 1;
            }
            else
            {
                return true;
            }
        }
        public static void SetIdentifierShowProductName(bool val)
        {
            PlayerPrefs.SetInt(IDENTIFIER_SHOW_PRODUCT_NAME, val ? 1 : 0);
        }

        private static string IDENTIFIER_SHOW_CUSTOM_TEXT = "BOB_SHOW_CUSTOM_TEXT";
        public static bool GetIdentifierShowCustomText()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_SHOW_CUSTOM_TEXT))
            {
                return PlayerPrefs.GetInt(IDENTIFIER_SHOW_CUSTOM_TEXT) == 1;
            }
            else
            {
                return false;
            }
        }
        public static void SetIdentifierShowCustomText(bool val)
        {
            PlayerPrefs.SetInt(IDENTIFIER_SHOW_CUSTOM_TEXT, val ? 1 : 0);
        }

        private static string IDENTIFIER_SHOW_DATE = "BOB_SHOW_DATE";
        public static bool GetIdentifierShowDate()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_SHOW_DATE))
            {
                return PlayerPrefs.GetInt(IDENTIFIER_SHOW_DATE) == 1;
            }
            else
            {
                return true;
            }
        }
        public static void SetIdentifierShowDate(bool val)
        {
            PlayerPrefs.SetInt(IDENTIFIER_SHOW_DATE, val ? 1 : 0);
        }

        private static string IDENTIFIER_SHOW_TIME = "BOB_SHOW_TIME";
        public static bool GetIdentfierShowTime()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_SHOW_TIME))
            {
                return PlayerPrefs.GetInt(IDENTIFIER_SHOW_TIME) == 1;
            }
            else
            {
                return false;
            }
        }
        public static void SetIdentifierShowTime(bool val)
        {
            PlayerPrefs.SetInt(IDENTIFIER_SHOW_TIME, val ? 1 : 0);
        }

        private static string IDENTIFIER_SHOW_RANDOM_ID = "BOB_SHOW_RANDOM_ID";
        public static bool GetIdentifierShowRandomId()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_SHOW_RANDOM_ID))
            {
                return PlayerPrefs.GetInt(IDENTIFIER_SHOW_RANDOM_ID) == 1;
            }
            else
            {
                return true;
            }
        }
        public static void SetIdentifierShowRandomId(bool val)
        {
            PlayerPrefs.SetInt(IDENTIFIER_SHOW_RANDOM_ID, val ? 1 : 0);
        }

        private static string IDENTIFIER_CUSTOM_TEXT = "BOB_CUSTOM_TEXT";
        public static string GetIdentifierCustomText()
        {
            if (PlayerPrefs.HasKey(IDENTIFIER_CUSTOM_TEXT))
            {
                return PlayerPrefs.GetString(IDENTIFIER_CUSTOM_TEXT);
            }
            else
            {
                return "";
            }
        }
        public static void SetIdentifierCustomText(string val)
        {
            PlayerPrefs.SetString(IDENTIFIER_CUSTOM_TEXT, val);
        }

        /// <summary>
        /// TODO: THE FINAL IDENTIFIER SHOULD BE SAVED OUT TO FILE.
        /// EDITOR PLAYER PREFS DO NOT PERSIST INTO A BUILD!
        /// </summary>
        public static string IDENTIFIER_ID = "BOB_IDENTIFIER_ID";
        public static string GetFinalIdentifierId()
        {
            return PlayerPrefs.GetString(IDENTIFIER_ID);
        }
        public static void SetFinalIdentifierId(string id)
        {
            PlayerPrefs.SetString(IDENTIFIER_ID, id);
        }

        public static string GenerateIdentifierId(bool generateId = false)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (GetIdentifierShowProductName())
            {
                stringBuilder.Append(Application.productName);
                stringBuilder.Append("_");
            }
            if (GetIdentifierShowDate())
            {
                stringBuilder.Append(GetDateString());
                stringBuilder.Append("_");

            }
            if (GetIdentfierShowTime())
            {
                stringBuilder.Append(GetTimeString());
                stringBuilder.Append("_");
            }
            if (GetIdentifierShowCustomText())
            {
                stringBuilder.Append(GetIdentifierCustomText());
                stringBuilder.Append("_");
            }
            if (GetIdentifierShowRandomId())
            {
                stringBuilder.Append(generateId ? GetRandomString(3) : "???");
                stringBuilder.Append("_");
            }

            if (stringBuilder.Length != 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            else
            {
                stringBuilder.Append("No-Name");
            }
            return stringBuilder.ToString();
        }

        public static string GetDateString()
        {
            DateTime dt = System.DateTime.Now;
            return string.Format("{0}{1}{2}", dt.ToString("yy"), dt.Month.ToString("00"), dt.Day.ToString("00"));
        }
        public static string GetTimeString()
        {
            TimeSpan ts = System.DateTime.Now.TimeOfDay;
            return ts.ToString("hhmm");
        }
        private static string GetRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new System.Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
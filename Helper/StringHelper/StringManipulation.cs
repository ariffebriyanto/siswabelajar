using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Helper.StringHelper
{
    public class StringManipulation
    {
        public static List<string> SplitToList(string Text, string Symbol)
        {
            List<string> result = new List<string>();
            if (Text == null)
            {
                return null;
            }

            string[] splitted = Text.Split(Symbol);
            foreach(var item in splitted)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static List<int> ConvertListStringToListInt(List<string> List)
        {
            List<int> collection = new List<int>();
            if (List == null)
            {
                return new List<int>();
            }

            foreach (string v in List)
            {
                if (!String.IsNullOrEmpty(v))
                {
                    try
                    {
                        collection.Add(int.Parse(v));
                    }
                    catch
                    {
                        return new List<int>();
                    }
                }
            }
            return collection;
        }

        public static List<string> ConvertListIntToListString(List<int> List)
        {
            List<string> collection = new List<string>();
            if (List == null)
            {
                return new List<string>();
            }

            foreach (int v in List)
            {
                try
                {
                    collection.Add(v.ToString());
                }
                catch
                {
                    return new List<string>();
                }
            }
            return collection;
        }

        public static List<string> ToLowerListString(List<string> List)
        {
            List<string> collection = new List<string>();
            if (List == null)
            {
                return new List<string>();
            }

            foreach (string v in List)
            {
                if (!String.IsNullOrEmpty(v))
                {
                    try
                    {
                        collection.Add(v.ToLower());
                    }
                    catch
                    {
                        return new List<string>();
                    }
                }
            }
            return collection;
        }

        public static List<string> ToUpperListString(List<string> List)
        {
            List<string> collection = new List<string>();
            if (List == null)
            {
                return new List<string>();
            }

            foreach (string v in List)
            {
                if (!String.IsNullOrEmpty(v))
                {
                    try
                    {
                        collection.Add(v.ToUpper());
                    }
                    catch
                    {
                        return new List<string>();
                    }
                }
            }
            return collection;
        }

        public static string MergeToString(List<string> Text, string Symbol)
        {
            string result = "";
            foreach(var item in Text)
            {
                result += item + Symbol;
            }
            return result;
        }

        public static string AddSpace(string Text)
        {
            if (Text == null)
            {
                return "";
            }

            Text = Regex.Replace(Text, "([a-z])([A-Z])", "$1 $2");
            Text = Regex.Replace(Text, "([A-Z])([A-Z][a-z])", "$1 $2");
            return Text;
        }

        public static string RemoveSpace(string Text)
        {
            if (Text == null)
            {
                return "";
            }

            return Text.Replace(" ", "");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisIcd
{
    public class HisIcdUtil
    {
        public static string RemoveDuplicateIcd(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] inputs = input.Split(';');
                string result = "";
                if (inputs != null && inputs.Length > 0)
                {
                    foreach (string text in inputs)
                    {
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            string tmp = text.Trim();
                            if ((";" + result + ";").ToLower().Contains((";" + tmp + ";").ToLower()))
                            {
                                continue;
                            }
                            else
                            {
                                result = string.Format("{0};{1}", result, tmp);
                            }
                        }
                    }
                    return result.Replace(";;", ";");
                }
            }
            return input;
        }

        public static long? Remove(long? input, long? toRemove)
        {
            if (input != null && toRemove != null)
            {
                return input == toRemove ? null : input;
            }
            return input;
        }

        public static string Remove(string input, string toRemove)
        {
            if (!string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(toRemove))
            {
                toRemove = ";" + toRemove + ";";
                toRemove = toRemove.Replace(";;", ";");

                string result = (";" + input + ";").Replace(toRemove, ";");
                result = result.Replace(";;", ";");
                result = result != null && result.Trim() == ";" ? null : result;
                return result;
            }
            return input;
        }

        public static string RemoveInList(string input, string toRemove)
        {
            if (!string.IsNullOrWhiteSpace(input) && !string.IsNullOrWhiteSpace(toRemove))
            {
                string[] toRemoves = toRemove.Split(';');
                foreach (string s in toRemoves)
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        input = (";" + input + ";").Replace(";;", ";").Replace(";" + s + ";", ";");
                    }
                }
                return input.Replace(";;", ";");
            }
            return input;
        }
    }
}

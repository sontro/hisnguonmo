using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class LogParamUtil
    {
        /// <summary>
        /// Log content
        /// </summary>
        /// <param name="inputs"></param>
        public static string LogContent(params object[] inputs)
        {
            string logContent = "";
            if (inputs != null && inputs.Length > 0 && inputs.Length > 1)
            {
                int i = 0;
                while (i < inputs.Length - 1)
                {
                    logContent += ToString(inputs[i]) + ":" + ToString(inputs[i + 1]) + ";";
                    i = i + 2;
                }
            }
            else
            {
                logContent = ToString(inputs[0]);
            }

            return logContent;
        }

        private static string ToString(object input)
        {
            if (input != null)
            {
                if (input.GetType() == typeof(string) || input.GetType() == typeof(String))
                {
                    return "\"" + input.ToString() + "\"";
                }
                else
                {
                    return input.ToString();
                }
            }
            return "null";
        }
    }
}

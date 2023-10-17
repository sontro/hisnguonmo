using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public class RocheHl7Util
    {
        private const string ELEMENT_SPLITTER = "|";
        private const string CHILD_ELEMENT_SPLITTER = "^";

        internal static string NVL(string input)
        {
            return !string.IsNullOrWhiteSpace(input) ? input.Replace("|", "").Replace("\r", "").Replace("\n", "").Trim() : "";
        }

        public static RocheHl7BaseMessage DetechReceivingMessage(string messageStr)
        {
            LogSystem.Info("Roche HL7 message: " + messageStr);
            RocheHl7BaseMessage message = null;
            try
            {
                message = new RocheHl7SampleSeenMessage(messageStr);
            }
            catch (Exception ex1)
            {
                try
                {
                    message = new RocheHl7ResultMessage(messageStr);
                }
                catch (Exception ex2)
                {
                    try
                    {
                        message = new RocheHl7AntibioticResultMessage(messageStr);
                    }
                    catch (Exception ex3)
                    {
                        LogSystem.Error(ex1);
                        LogSystem.Error(ex2);
                        LogSystem.Error(ex3);
                    }
                }
            }

            return message;
        }

        public static string GetElement(string indicator, string data, int elementPos, ref string[] elements)
        {
            if (IsElement(indicator, data))
            {
                return RocheHl7Util.ParseElement(data, elementPos, ELEMENT_SPLITTER, ref elements);
            }
            return null;
        }

        public static bool IsElement(string indicator, string data)
        {
            return !string.IsNullOrWhiteSpace(data) && !string.IsNullOrWhiteSpace(indicator) && data.StartsWith(indicator);
        }

        public static string GetElement(string[] elements, int elementPos)
        {
            if (elements != null && elements.Length > elementPos + 1)
            {
                return elements[elementPos];
            }
            return null;
        }

        public static string GetElement(string indicator, string data, int elementPos)
        {
            string[] elements = null;
            return RocheHl7Util.GetElement(indicator, data, elementPos, ref elements);
        }

        public static string[] GetElements(string indicator, string data)
        {
            string[] elements = null;
            RocheHl7Util.GetElement(indicator, data, 0, ref elements);
            return elements;
        }

        public static string GetChildElement(string[] elements, int elementPosition, int childElementPosition)
        {
            if (elements == null || elements.Length < elementPosition || childElementPosition <= 0)
            {
                return null;
            }
            string[] childrenElements = null;

            //cac phan tu con thi danh so tu 1 (index theo 1-base chu ko phai theo 0-base)
            //Vi vay, neu ghi la ORC-2^1 thi duoc hieu la lay phan tu con dau tien cua ORC-2 --> tuc la vi tri 0 index --> can tru di 1
            return RocheHl7Util.ParseElement(elements[elementPosition], childElementPosition - 1, CHILD_ELEMENT_SPLITTER, ref childrenElements);
        }

        private static string ParseElement(string data, int position, string splitter, ref string[] elements)
        {
            elements = data.Split(new[] { splitter }, StringSplitOptions.None);
            if (elements == null || elements.Length < position + 1)
            {
                return null;
            }
            return elements[position];
        }

        public static string DateOfBirthToString(DateTime date)
        {
            if (date != null)
            {
                return date.ToString("yyyyMMdd");
            }
            return "";
        }
    }
}

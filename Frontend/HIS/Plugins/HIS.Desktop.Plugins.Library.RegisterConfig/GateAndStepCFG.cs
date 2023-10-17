using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.RegisterConfig
{
    public class GateAndStepCFG
    {
        private static string value = System.Configuration.ConfigurationSettings.AppSettings["HIS.Desktop.Plugins.Library.RegisterConfig.GateAndStep"];

        private static string gate_Number = null;
        public static string GateNumber
        {
            get
            {
                if (gate_Number == null)
                {
                    gate_Number = GetValue(0);
                }
                return gate_Number;
            }
            set { gate_Number = value; }
        }

        private static string step_Number = null;
        public static string StepNumber
        {
            get
            {
                if (step_Number == null)
                {
                    step_Number = GetValue(1);
                }
                return step_Number;
            }
            set { step_Number = value; }
        }

        private static string GetValue(int index)
        {
            string result = "";
            try
            {
                if (!String.IsNullOrEmpty(value))
                {
                    var arr = value.Split(':');
                    if (arr != null && arr.Length > 1)
                    {
                        result = arr[index];
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = "";
            }
            return result;
        }
    }
}

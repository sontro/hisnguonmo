using HIS.Desktop.Plugins.Library.PrintOtherForm.Base;
using MPS.ProcessorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.MpsBehavior.Mps000419
{
    public partial class Mps000419Behavior : MpsDataBase, ILoad
    {
        private void SetDicParamCommon(ref Dictionary<string, object> dicParamPlus)
        {
            try
            {
                if (!String.IsNullOrEmpty(PrintConfig.OrganizationName))
                    dicParamPlus.Add("ORGANIZATION_NAME", PrintConfig.OrganizationName);
                else if (patient != null)
                    dicParamPlus.Add("VIR_PATIENT_NAME", patient.VIR_PATIENT_NAME);
                else
                    dicParamPlus.Add("VIR_PATIENT_NAME", "");

                dicParamPlus.Add("CURRENT_DATE_SEPARATE_STR", HIS.Desktop.Utilities.GlobalReportQuery.GetCurrentTime());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Debug(ex);
            }
        }

        internal string CalculateFullAge(long ageNumber)
        {
            string tuoi;
            try
            {
                DateTime dtNgSinh = Inventec.Common.TypeConvert.Parse.ToDateTime(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(ageNumber));
                TimeSpan diff = DateTime.Now - dtNgSinh;
                long tongsogiay = diff.Ticks;
                if (tongsogiay < 0)
                {
                    tuoi = "";
                    return "";
                }
                DateTime newDate = new DateTime(tongsogiay);

                int nam = newDate.Year - 1;
                int thang = newDate.Month - 1;
                int ngay = newDate.Day - 1;
                int gio = newDate.Hour;
                int phut = newDate.Minute;
                int giay = newDate.Second;

                if (nam > 0)
                {
                    tuoi = nam.ToString();
                }
                else
                {
                    if (thang > 0)
                    {
                        tuoi = thang.ToString();
                    }
                    else
                    {
                        if (ngay > 0)
                        {
                            tuoi = ngay.ToString();
                        }
                        else
                        {
                            tuoi = "";
                        }
                    }
                }
                return tuoi;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return "";
            }
        }

        private void AddKeyIntoDictionaryPrint<T>(T data, Dictionary<string, object> dicParamPlus)
        {
            try
            {
                PropertyInfo[] pis = typeof(T).GetProperties();
                if (pis != null && pis.Length > 0)
                {
                    foreach (var pi in pis)
                    {
                        var searchKey = dicParamPlus.SingleOrDefault(o => o.Key == pi.Name);
                        if (String.IsNullOrEmpty(searchKey.Key))
                        {
                            dicParamPlus.Add(pi.Name, pi.GetValue(data) != null ? pi.GetValue(data) : "");
                        }
                        else
                        {
                            dicParamPlus[pi.Name] = pi.GetValue(data) != null ? pi.GetValue(data) : "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}

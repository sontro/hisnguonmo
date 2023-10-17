using DevExpress.XtraGrid.Views.Base;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Controls.Session;
using HIS.Desktop.LocalStorage.ConfigSystem;
using HIS.Desktop.LocalStorage.Location;
using HIS.Desktop.Plugins.OtherFormAssTreatment.Base;
using HIS.Desktop.Print;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using Inventec.Common.RichEditor;
using Inventec.Common.RichEditor.Base;
using Inventec.Common.WordContent;
using Inventec.Core;
using Inventec.Desktop.Common.LanguageManager;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using SAR.EFMODEL.DataModels;
using SAR.Filter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.OtherFormAssTreatment
{
    public partial class frmOtherFormAssTreatment : HIS.Desktop.Utility.FormBase
    {
        private void SetDicParamPatient(ref Dictionary<string, object> dicParamPlus)
        {
            try
            {
                if (this.Patient != null)
                {
                    TemplateKeyProcessor.AddKeyIntoDictionaryPrint<V_HIS_PATIENT>(Patient, dicParamPlus);
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "AGE", this.CalculateFullAge(Patient.DOB));
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "DOB_YEAR", Patient.DOB.ToString().Substring(0, 4));
                    if (!String.IsNullOrWhiteSpace(Patient.CMND_NUMBER))
                    {
                        dicParamPlus["CMND_CCCD_NUMBER"] = Patient.CMND_NUMBER;
                        dicParamPlus["CMND_CCCD_DATE"] = Inventec.Common.DateTime.Convert.TimeNumberToDateString(Patient.CMND_DATE ?? 0);
                        dicParamPlus["CMND_CCCD_PLACE"] = Patient.CMND_PLACE;
                    }
                    else
                    {
                        dicParamPlus["CMND_CCCD_NUMBER"] = Patient.CCCD_NUMBER;
                        dicParamPlus["CMND_CCCD_DATE"] = Inventec.Common.DateTime.Convert.TimeNumberToDateString(Patient.CCCD_DATE ?? 0);
                        dicParamPlus["CMND_CCCD_PLACE"] = Patient.CCCD_PLACE;
                    }
                }
                else
                {
                    V_HIS_PATIENT temp = new V_HIS_PATIENT();
                    TemplateKeyProcessor.AddKeyIntoDictionaryPrint<V_HIS_PATIENT>(temp, dicParamPlus);
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "AGE", "");
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "DOB_YEAR", "");
                    dicParamPlus["CMND_CCCD_NUMBER"] = "";
                    dicParamPlus["CMND_CCCD_DATE"] = "";
                    dicParamPlus["CMND_CCCD_PLACE"] = "";
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetDicParamTreatment(ref Dictionary<string, object> dicParamPlus)
        {
            try
            {
                if (this.Treatment != null)
                {
                    // AddKeyIntoDictionaryPrint<V_HIS_TREATMENT>(this.Treatment, dicParamPlus);
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "AGE_TREATMENT", this.CalculateFullAge(this.Treatment.TDL_PATIENT_DOB));
                }
                else
                {
                    //V_HIS_TREATMENT temp = new V_HIS_TREATMENT();
                    //AddKeyIntoDictionaryPrint<V_HIS_TREATMENT>(temp, dicParamPlus);
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "AGE_TREATMENT", "");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void SetDicParamBedAndBedRoomFromTreatment(ref Dictionary<string, object> dicParamPlus)
        {
            try
            {
                if (this.TreatmentBedRooms != null && this.TreatmentBedRooms.Count > 0)
                {
                    var bedRoom = this.TreatmentBedRooms.FirstOrDefault(o => o.REMOVE_TIME == null);
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "BED_ROOM_NAME", bedRoom != null ? bedRoom.BED_ROOM_NAME : "");
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "BED_NAME", bedRoom != null ? bedRoom.BED_NAME : "");
                }
                else
                {
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "BED_ROOM_NAME", "");
                    TemplateKeyProcessor.SetSingleKey(dicParamPlus, "BED_NAME", "");
                }
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal string CalculateFullAge(long ageNumber)
        {
            string tuoi;
            string cboAge;
            try
            {
                DateTime dtNgSinh = Inventec.Common.TypeConvert.Parse.ToDateTime(Inventec.Common.DateTime.Convert.TimeNumberToTimeString(ageNumber));
                TimeSpan diff = DateTime.Now - dtNgSinh;
                long tongsogiay = diff.Ticks;
                if (tongsogiay < 0)
                {
                    tuoi = "";
                    cboAge = "Tuổi";
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
                    cboAge = "Tuổi";
                }
                else
                {
                    if (thang > 0)
                    {
                        tuoi = thang.ToString();
                        cboAge = "Tháng";
                    }
                    else
                    {
                        if (ngay > 0)
                        {
                            tuoi = ngay.ToString();
                            cboAge = "ngày";
                        }
                        else
                        {
                            tuoi = "";
                            cboAge = "Giờ";
                        }
                    }
                }
                return tuoi + " " + cboAge;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                return "";
            }
        }

        //private void AddKeyIntoDictionaryPrint<T>(T data, Dictionary<string, object> dicParamPlus)
        //{
        //    try
        //    {
        //        PropertyInfo[] pis = typeof(T).GetProperties();
        //        if (pis != null && pis.Length > 0)
        //        {
        //            foreach (var pi in pis)
        //            {
        //                var searchKey = dicParamPlus.SingleOrDefault(o => o.Key == pi.Name);
        //                if (String.IsNullOrEmpty(searchKey.Key))
        //                {
        //                    TemplateKeyProcessor.SetSingleKey(dicParamPlus,pi.Name, pi.GetValue(data) != null ? pi.GetValue(data) : "");
        //                }
        //                else
        //                {
        //                    dicParamPlus[pi.Name] = pi.GetValue(data) != null ? pi.GetValue(data) : "";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Warn(ex);
        //    }
        //}
    }
}

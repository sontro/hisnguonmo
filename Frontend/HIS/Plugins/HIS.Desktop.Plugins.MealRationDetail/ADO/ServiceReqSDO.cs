using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MealRationDetail.ADO
{
    public class ServiceReqSDO : HIS_SERVICE_REQ 
    {
        public decimal AMOUNT { get; set; }
        public string PATIENT_CLASSIFY_NAME { get; set; }

        public ServiceReqSDO()
        { }

        public ServiceReqSDO(HIS_SERVICE_REQ _data)
        {
            try
            {
                if (_data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<HIS_SERVICE_REQ>();

                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
                    }
                    var patientClassify = BackendDataWorker.Get<HIS_PATIENT_CLASSIFY>().FirstOrDefault(o => o.ID == _data.TDL_PATIENT_CLASSIFY_ID);

                    this.PATIENT_CLASSIFY_NAME = patientClassify != null? patientClassify.PATIENT_CLASSIFY_NAME : null;
                }

            }

            catch (Exception)
            {
                
            }
        }
    }
}

using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.ADO
{
    public class ContraindicatedADO
    {
        public string ICD_NAME { get; set; }
        public string SERVICE_NAME { get; set; }
        public string CONTRAINDICATION_CONTENT { get; set; }
        public ContraindicatedADO()
        {
        }
        public ContraindicatedADO(HIS_ICD_SERVICE data)
        {
            try
            {
                if (data != null)
                {
                    this.ICD_NAME = data.ICD_CODE + "-" + data.ICD_NAME;
                    this.CONTRAINDICATION_CONTENT = data.CONTRAINDICATION_CONTENT;
                    this.SERVICE_NAME = BackendDataWorker.Get<V_HIS_SERVICE>().Where(o => o.ID == data.SERVICE_ID).Select(o => o.SERVICE_NAME).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

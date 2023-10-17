using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.Library.OtherTreatmentHistory.Base;
using HIS.Desktop.Plugins.Library.OtherTreatmentHistory.ProviderBehavior;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.OtherTreatmentHistory
{
    public class OtherTreatmentHistoryProcessor
    {
        private InitDataADO InitData;

        public OtherTreatmentHistoryProcessor(InitDataADO data)
        {
            this.InitData = data;
        }

        public void Run(Enum type)
        {
            try
            {
                if (CheckData())
                {
                    IRun iRun = null;
                    switch (this.InitData.ProviderType)
                    {
                        case ProviderType.Medisoft:
                            iRun = new MedisoftBehavior(this.InitData);
                            break;
                        default:
                            break;
                    }

                    if (iRun != null)
                        iRun.Run(type);
                    else
                        Inventec.Common.Logging.LogSystem.Error("OtherTreatmentHistoryProcessor IRun is null");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool CheckData()
        {
            bool result = true;
            try
            {
                if (this.InitData == null)
                {
                    MessageBox.Show("Lỗi dữ liệu");
                    Inventec.Common.Logging.LogSystem.Error("InitData is null");
                    result = false;
                }
                else if (String.IsNullOrWhiteSpace(this.InitData.ProviderType))
                {
                    MessageBox.Show("Không xác định được loại hệ thống");
                    Inventec.Common.Logging.LogSystem.Error("ProviderType is null");
                    result = false;
                }
                else if (this.InitData.PatientId == 0 && this.InitData.Patient == null)
                {
                    MessageBox.Show("Không xác định được bệnh nhân");
                    Inventec.Common.Logging.LogSystem.Error("PatientId, Patient is null");
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}

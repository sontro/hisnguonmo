using AutoMapper;
using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Common.Adapter;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CareCreate
{
    public partial class CareCreate : HIS.Desktop.Utility.FormBase
    {
        //private bool SaveDHSTTProcess()
        //{
        //    bool result = false;
        //    try
        //    {
        //        MOS.EFMODEL.DataModels.HIS_DHST hdst = new MOS.EFMODEL.DataModels.HIS_DHST();
        //        MOS.EFMODEL.DataModels.HIS_DHST rsDhst = null;
        //        if (action == GlobalVariables.ActionEdit)
        //        {
        //            Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>();
        //            hdst = Mapper.Map<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>(this.currentDhst);
        //        }
        //        if (rsDhst == null)
        //        {
        //            rsDhst = new MOS.EFMODEL.DataModels.HIS_DHST();
        //        }
        //        hdst.BLOOD_PRESSURE_MAX = spinBloodPressureMax.Value;
        //        hdst.BLOOD_PRESSURE_MIN = spinBloodPressureMin.Value;
        //        hdst.WEIGHT = spinWeight.Value;
        //        hdst.HEIGHT = null;
        //        hdst.PULSE = spinPulse.Value;
        //        hdst.TEMPERATURE = spinTemperature.Value;
        //        hdst.EXECUTE_TIME = Inventec.Common.TypeConvert.Parse.ToInt64(Convert.ToDateTime(dtExcuteTime.EditValue).ToString("yyyyMMddHHmmss"));
        //        hdst.BREATH_RATE = spinBreathRate.Value;
        //        hdst.TREATMENT_ID = this.currentTreatmentId;
        //        hdst.EXECUTE_LOGINNAME = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName();
        //        hdst.EXECUTE_USERNAME = Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetUserName();

        //        switch (action)
        //        {
        //            case GlobalVariables.ActionAdd:
        //                rsDhst = new BackendAdapter(param).Post<HIS_DHST>(HisRequestUriStore.HIS_DHST_CREATE, ApiConsumers.MosConsumer, hdst, param);
        //                break;
        //            case GlobalVariables.ActionEdit:
        //                rsDhst = new BackendAdapter(param).Post<HIS_DHST>(HisRequestUriStore.HIS_DHST_UPDATE, ApiConsumers.MosConsumer, hdst, param);
        //                break;
        //            default:
        //                break;
        //        }

        //        if (rsDhst != null && rsDhst.ID > 0)
        //        {
        //            Mapper.CreateMap<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>();
        //            this.currentDhst = Mapper.Map<MOS.EFMODEL.DataModels.HIS_DHST, MOS.EFMODEL.DataModels.HIS_DHST>(rsDhst);
        //            result = true;
        //        }

        //        if (!result)
        //        {
        //            LogSystem.Info("Khong tao duoc du lieu dau hieu sinh ton, kiem tra lai du lieu. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.currentDhst), this.currentDhst));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.currentDhst = null;
        //        Inventec.Common.Logging.LogSystem.Warn(ex);
        //    }

        //    return result;
        //}
    }
}

using HIS.Desktop.ApiConsumer;
using HIS.Desktop.Plugins.Library.PrintOtherForm.Base;
using Inventec.Common.Adapter;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.MpsBehavior.Mps000417
{
    public partial class Mps000417Behavior : MpsDataBase, ILoad
    {
        private void LoadData()
        {
            try
            {
                this.LoadTreatment();
                this.LoadDhst();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadTreatment()
        {
            try
            {
                if (this.TreatmentId > 0)
                {
                    CommonParam param = new CommonParam();
                    HisTreatmentViewFilter filter = new HisTreatmentViewFilter();
                    filter.ID = this.TreatmentId;
                    var listTreatment = new BackendAdapter(param).Get<List<V_HIS_TREATMENT>>("api/HisTreatment/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (listTreatment != null && listTreatment.Count > 0)
                    {
                        this.treatment = listTreatment.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadDhst()
        {
            try
            {
                if (this.treatment != null)
                {
                    CommonParam param = new CommonParam();
                    HisDhstFilter filter = new HisDhstFilter();
                    filter.TREATMENT_ID = this.treatment.ID;
                    filter.ORDER_DIRECTION = "DESC";
                    filter.ORDER_FIELD = "EXECUTE_TIME";
                    var listDhst = new BackendAdapter(param).Get<List<HIS_DHST>>("api/HisDhst/Get", ApiConsumers.MosConsumer, filter, param);
                    if (listDhst != null && listDhst.Count > 0)
                    {
                        if (listDhst.Any(a => a.WEIGHT.HasValue && a.TEMPERATURE.HasValue && a.BLOOD_PRESSURE_MIN.HasValue && a.BLOOD_PRESSURE_MAX.HasValue && a.BREATH_RATE.HasValue))
                            this.dhst = listDhst.FirstOrDefault(a => a.WEIGHT.HasValue && a.TEMPERATURE.HasValue && a.BLOOD_PRESSURE_MIN.HasValue && a.BLOOD_PRESSURE_MAX.HasValue && a.BREATH_RATE.HasValue);
                        else
                            this.dhst = listDhst.FirstOrDefault();
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

using HIS.Desktop.ApiConsumer;
using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.Plugins.Library.PrintOtherForm.Base;
using HIS.Desktop.Plugins.Library.PrintOtherForm.RunPrintTemplate;
using Inventec.Common.Adapter;
using Inventec.Common.ThreadCustom;
using Inventec.Core;
using Inventec.Desktop.Common.Message;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.MpsBehavior.Mps000406
{
    public partial class Mps000406Behavior : MpsDataBase, ILoad
    {
        private void LoadData()
        {
            try
            {
                if (this.inputAdo != null)
                {
                    List<Action> methods = new List<Action>();
                    methods.Add(LoadTreatment);
                    methods.Add(LoadTreatmentBedRoom);
                    ThreadCustomManager.MultipleThreadWithJoin(methods);
                    if (this.inputAdo.DepartmentId.HasValue)
                    {
                        this.department = BackendDataWorker.Get<V_HIS_DEPARTMENT>().FirstOrDefault(o => o.ID == this.inputAdo.DepartmentId.Value);
                    }
                    if (inputAdo.RoomId.HasValue)
                    {
                        this.room = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(o => o.ID == inputAdo.RoomId.Value);
                    }

                }
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
                CommonParam param = new CommonParam();
                HisTreatmentViewFilter filter = new HisTreatmentViewFilter();
                filter.ID = this.inputAdo.TreatmentId;
                var listTreatment = new BackendAdapter(param).Get<List<V_HIS_TREATMENT>>("api/HisTreatment/GetView", ApiConsumers.MosConsumer, filter, param);
                if (listTreatment != null && listTreatment.Count > 0)
                {
                    this.treatment = listTreatment.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private void LoadTreatmentBedRoom()
        {
            try
            {
                if (this.inputAdo.TreatmentBedRoomId.HasValue)
                {
                    CommonParam param = new CommonParam();
                    HisTreatmentBedRoomViewFilter filter = new HisTreatmentBedRoomViewFilter();
                    filter.ID = this.inputAdo.TreatmentBedRoomId;
                    var listTreatBedRoom = new BackendAdapter(param).Get<List<V_HIS_TREATMENT_BED_ROOM>>("api/HisTreatmentBedRoom/GetView", ApiConsumers.MosConsumer, filter, param);
                    if (listTreatBedRoom != null && listTreatBedRoom.Count > 0)
                    {
                        this.treatmentBedRoom = listTreatBedRoom.FirstOrDefault();
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

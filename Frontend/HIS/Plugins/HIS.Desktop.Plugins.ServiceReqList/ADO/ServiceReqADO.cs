using HIS.Desktop.LocalStorage.BackendData;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.ADO
{
    public class ServiceReqADO : HIS_SERVICE_REQ
    {
        public bool isCheck { get; set; }
        public string EXECUTE_DEPARTMENT_CODE { get; set; }
        public string EXECUTE_DEPARTMENT_NAME { get; set; }
        public string EXECUTE_ROOM_CODE { get; set; }
        public string EXECUTE_ROOM_NAME { get; set; }
        public string EXECUTE_ROOM_ADDRESS { get; set; }
        public string REQUEST_DEPARTMENT_CODE { get; set; }
        public string REQUEST_DEPARTMENT_NAME { get; set; }
        public string REQUEST_ROOM_CODE { get; set; }
        public string REQUEST_ROOM_NAME { get; set; }
        public string SERVICE_REQ_STT_CODE { get; set; }
        public string SERVICE_REQ_STT_NAME { get; set; }
        public string SERVICE_REQ_TYPE_CODE { get; set; }
        public string SERVICE_REQ_TYPE_NAME { get; set; }
        public bool DeleteCheck { get; set; }
        public bool AddInforPTTT { get; set; }

        public string SAMPLE_ROOM_CODE { get; set; }
        public string SAMPLE_ROOM_NAME { get; set; }


        public ServiceReqADO()
        {

        }

        public ServiceReqADO(HIS_SERVICE_REQ data)
        {
            try
            {
                if (data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.HIS_SERVICE_REQ>();
                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(data)));
                    }

                    var executeRoom = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(o => o.ID == data.EXECUTE_ROOM_ID);
                    if (executeRoom != null)
                    {
                        this.EXECUTE_DEPARTMENT_CODE = executeRoom.DEPARTMENT_CODE;
                        this.EXECUTE_DEPARTMENT_NAME = executeRoom.DEPARTMENT_NAME;
                        this.EXECUTE_ROOM_CODE = executeRoom.ROOM_CODE;
                        this.EXECUTE_ROOM_NAME = executeRoom.ROOM_NAME;
                        this.EXECUTE_ROOM_ADDRESS = executeRoom.ADDRESS;
                    }
                    else
                    {
                        this.EXECUTE_DEPARTMENT_ID = data.EXECUTE_DEPARTMENT_ID;
                        this.EXECUTE_ROOM_ID = data.EXECUTE_ROOM_ID;
                    }
                    var reqRoom = BackendDataWorker.Get<V_HIS_ROOM>().FirstOrDefault(o => o.ID == data.REQUEST_ROOM_ID);
                    if (reqRoom != null)
                    {
                        this.REQUEST_DEPARTMENT_CODE = reqRoom.DEPARTMENT_CODE;
                        this.REQUEST_DEPARTMENT_NAME = reqRoom.DEPARTMENT_NAME;
                        this.REQUEST_ROOM_CODE = reqRoom.ROOM_CODE;
                        this.REQUEST_ROOM_NAME = reqRoom.ROOM_NAME;
                    }
                    else
                    {
                        this.REQUEST_DEPARTMENT_ID = data.REQUEST_DEPARTMENT_ID;
                        this.REQUEST_ROOM_ID = data.REQUEST_ROOM_ID;
                    }
                    var serviceReqStt = BackendDataWorker.Get<HIS_SERVICE_REQ_STT>().FirstOrDefault(o => o.ID == data.SERVICE_REQ_STT_ID);
                    if (serviceReqStt != null)
                    {
                        this.SERVICE_REQ_STT_CODE = serviceReqStt.SERVICE_REQ_STT_CODE;
                        this.SERVICE_REQ_STT_NAME = serviceReqStt.SERVICE_REQ_STT_NAME;
                    }
                    var serviceReqType = BackendDataWorker.Get<HIS_SERVICE_REQ_TYPE>().FirstOrDefault(o => o.ID == data.SERVICE_REQ_TYPE_ID);
                    if (serviceReqType != null)
                    {
                        this.SERVICE_REQ_TYPE_CODE = serviceReqType.SERVICE_REQ_TYPE_CODE;
                        this.SERVICE_REQ_TYPE_NAME = serviceReqType.SERVICE_REQ_TYPE_NAME;
                    }

                    if (data.SAMPLE_ROOM_ID != null)
                    {
                        var sampleRoom = BackendDataWorker.Get<HIS_SAMPLE_ROOM>().FirstOrDefault(o => o.ID == data.SAMPLE_ROOM_ID);
                        if (sampleRoom != null)
                        {
                            this.SAMPLE_ROOM_CODE = sampleRoom.SAMPLE_ROOM_CODE;
                            this.SAMPLE_ROOM_NAME  = sampleRoom.SAMPLE_ROOM_NAME;
                        }
                    }

                    this.isCheck = false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

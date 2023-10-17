using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    public class HisSereServProcessor : BusinessBase
    {
        private HisSereServCreate hisSereServCreate;

        internal HisSereServProcessor()
            : base()
        {
            this.hisSereServCreate = new HisSereServCreate(param);
        }

        internal HisSereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServCreate = new HisSereServCreate(param);
        }

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<RationRequest> rationRequests, ref List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                List<HIS_SERE_SERV> toInserts = this.MakeData(treatment, serviceReqs, rationRequests);
                if (this.hisSereServCreate.CreateList(toInserts, serviceReqs, false))
                {
                    sereServs = toInserts;
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        /// <summary>
        /// Tao du lieu sere_serv
        /// Luu y co nghiep vu xu ly cac truong TDL, va bo sung thong tin gia
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="serviceReqs"></param>
        /// <param name="rationRequests"></param>
        /// <returns></returns>
        private List<HIS_SERE_SERV> MakeData(HIS_TREATMENT treatment, List<HIS_SERVICE_REQ> serviceReqs, List<RationRequest> rationRequests)
        {
            List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();
            foreach (RationRequest rr in rationRequests)
            {
                HIS_SERVICE_REQ serviceReq = serviceReqs.Where(o => o.RATION_TIME_ID == rr.RationTimeId && o.INTRUCTION_TIME == rr.IntructionTime && o.EXECUTE_ROOM_ID == rr.RoomId).FirstOrDefault();
                if (serviceReq == null)
                {
                    throw new Exception("Loi he thong");
                }
                long executeBranchId = HisDepartmentCFG.DATA
                    .Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID)
                    .FirstOrDefault().BRANCH_ID;

                HIS_SERE_SERV toInsert = new HIS_SERE_SERV();
                toInsert.SERVICE_REQ_ID = serviceReq.ID;
                toInsert.SERVICE_ID = rr.ServiceId;
                toInsert.AMOUNT = rr.Amount;
                toInsert.PATIENT_TYPE_ID = rr.PatientTypeId;
                HisSereServUtil.SetTdl(toInsert, serviceReq);

                //Cap nhat thong tin gia theo dich vu moi
                HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, null);
                if (!priceAdder.AddPrice(toInsert, serviceReq.INTRUCTION_TIME, executeBranchId, serviceReq.REQUEST_ROOM_ID, serviceReq.REQUEST_DEPARTMENT_ID, serviceReq.EXECUTE_ROOM_ID))
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                result.Add(toInsert);
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServCreate.RollbackData();
        }
    }
}

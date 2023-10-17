using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServRation;
using MOS.SDO;
using MOS.ServicePaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    public class HisSereServRationProcessor : BusinessBase
    {
        private HisSereServRationCreate hisSereServRationCreate;

        internal HisSereServRationProcessor()
            : base()
        {
            this.hisSereServRationCreate = new HisSereServRationCreate(param);
        }

        internal HisSereServRationProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServRationCreate = new HisSereServRationCreate(param);
        }

        internal bool Run(List<HIS_TREATMENT> treatments, List<HIS_SERVICE_REQ> serviceReqs, List<RationRequest> rationRequests, Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP, ref List<HIS_SERE_SERV_RATION> sereServRations)
        {
            try
            {
                List<HIS_SERE_SERV_RATION> toInserts = this.MakeData(treatments, serviceReqs, rationRequests, SR_RATIONREQ_MAP);
                if (this.hisSereServRationCreate.CreateList(toInserts))
                {
                    sereServRations = toInserts;
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
        private List<HIS_SERE_SERV_RATION> MakeData(List<HIS_TREATMENT> treatments, List<HIS_SERVICE_REQ> serviceReqs, List<RationRequest> rationRequests, Dictionary<HIS_SERVICE_REQ, List<RationRequest>> SR_RATIONREQ_MAP)
        {
            List<HIS_SERE_SERV_RATION> result = new List<HIS_SERE_SERV_RATION>();
            foreach (var req in serviceReqs)
            {
                var rationReqs = SR_RATIONREQ_MAP[req];
                if (IsNotNullOrEmpty(rationReqs))
                {
                    foreach (RationRequest rr in rationReqs)
                    {
                        HIS_TREATMENT treatment = treatments.Where(o => o.ID == req.TREATMENT_ID).FirstOrDefault();

                        long executeBranchId = HisDepartmentCFG.DATA
                            .Where(o => o.ID == req.EXECUTE_DEPARTMENT_ID)
                            .FirstOrDefault().BRANCH_ID;

                        HIS_SERE_SERV_RATION toInsert = new HIS_SERE_SERV_RATION();
                        toInsert.SERVICE_REQ_ID = req.ID;
                        toInsert.SERVICE_ID = rr.ServiceId;
                        toInsert.AMOUNT = rr.Amount;
                        toInsert.PATIENT_TYPE_ID = rr.PatientTypeId;
                        toInsert.INSTRUCTION_NOTE = rr.InstructionNote;
                        result.Add(toInsert);
                    }
                }
            }

            return result;
        }

        internal void Rollback()
        {
            this.hisSereServRationCreate.RollbackData();
        }
    }
}

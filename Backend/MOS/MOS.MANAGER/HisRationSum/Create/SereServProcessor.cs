using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisTreatment;
using MOS.ServicePaty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Create
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServCreate hisSereServCreate;
        private HisSereServExtCreate hisSereServExtCreate;

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
        }

        internal bool Run(List<HIS_TREATMENT> listTreatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV_RATION> sereServRations)
        {
            bool result = false;
            try
            {
                Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT> dicSereServExt = new Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT>();

                List<HIS_SERE_SERV> toInserts = this.MakeData(listTreatment, serviceReqs, sereServRations, ref dicSereServExt);

                if (!this.hisSereServCreate.CreateList(toInserts, serviceReqs, false))
                {
                    throw new Exception("hisSereServCreate. Ket thuc nghiep vu");
                }

                if (dicSereServExt.Count > 0)
                {
                    List<HIS_SERE_SERV_EXT> createList = new List<HIS_SERE_SERV_EXT>();
                    foreach (var dic in dicSereServExt)
                    {
                        HisSereServExtUtil.SetTdl(dic.Value, dic.Key);
                        createList.Add(dic.Value);
                    }
                    if (!this.hisSereServExtCreate.CreateList(createList))
                    {
                        throw new Exception("hisSereServExtCreate. Ket thuc nghiep vu");
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private List<HIS_SERE_SERV> MakeData(List<HIS_TREATMENT> listTreatment, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV_RATION> sereServRations, ref Dictionary<HIS_SERE_SERV, HIS_SERE_SERV_EXT> dicSSExt)
        {
            List<HIS_SERE_SERV> result = new List<HIS_SERE_SERV>();
            foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
            {
                List<HIS_SERE_SERV_RATION> ssRations = sereServRations.Where(o => o.SERVICE_REQ_ID == serviceReq.ID).ToList();

                long executeBranchId = HisDepartmentCFG.DATA
                    .Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID)
                    .FirstOrDefault().BRANCH_ID;

                var treatment = listTreatment.FirstOrDefault(o => o.ID == serviceReq.TREATMENT_ID);
                if (treatment == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc Treatment theo Id: " + serviceReq.TREATMENT_ID);
                }
                foreach (var ration in ssRations)
                {
                    HIS_SERE_SERV toInsert = new HIS_SERE_SERV();
                    toInsert.SERVICE_REQ_ID = serviceReq.ID;
                    toInsert.SERVICE_ID = ration.SERVICE_ID;
                    toInsert.AMOUNT = ration.AMOUNT;
                    toInsert.PATIENT_TYPE_ID = ration.PATIENT_TYPE_ID;
                    toInsert.DISCOUNT = ration.DISCOUNT;

                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == serviceReq.EXECUTE_DEPARTMENT_ID).FirstOrDefault();
                    V_HIS_SERVICE_PATY servicePaty = ServicePatyUtil.GetApplied(HisServicePatyCFG.DATA, department.BRANCH_ID, serviceReq.EXECUTE_ROOM_ID, serviceReq.REQUEST_ROOM_ID, serviceReq.REQUEST_DEPARTMENT_ID, serviceReq.INTRUCTION_TIME, treatment.IN_TIME, ration.SERVICE_ID, ration.PATIENT_TYPE_ID, null, null, null, null, treatment.TDL_PATIENT_CLASSIFY_ID, serviceReq.RATION_TIME_ID);
                    if (servicePaty == null)
                    {
                        HIS_PATIENT_TYPE hisPatientType = HisPatientTypeCFG.DATA.Where(o => o.ID == ration.PATIENT_TYPE_ID).FirstOrDefault();
                        V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == ration.SERVICE_ID).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTonTaiDuLieuPhuHop, hisService.SERVICE_NAME, hisService.SERVICE_TYPE_CODE, hisPatientType.PATIENT_TYPE_NAME);
                        throw new Exception("Khong ton tai chinh sach gia tuong ung voi suat an: " + ration.SERVICE_ID + "va patient_type_id: " + ration.PATIENT_TYPE_ID);
                    }

                    toInsert.PRICE = servicePaty.PRICE;
                    toInsert.VAT_RATIO = servicePaty.VAT_RATIO;
                    toInsert.ACTUAL_PRICE = servicePaty.ACTUAL_PRICE;
                    toInsert.ORIGINAL_PRICE = servicePaty.PRICE;

                    HisSereServUtil.SetTdl(toInsert, serviceReq);
                    if (!String.IsNullOrWhiteSpace(ration.INSTRUCTION_NOTE))
                    {
                        HIS_SERE_SERV_EXT ssExt = new HIS_SERE_SERV_EXT();
                        ssExt.INSTRUCTION_NOTE = ration.INSTRUCTION_NOTE;
                        dicSSExt[toInsert] = ssExt;
                    }
                    result.Add(toInsert);
                }
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisSereServCreate.RollbackData();
        }
    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.Token;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(List<HIS_SERVICE_REQ> inStockServiceReqs, InPatientPresSDO sdo, ref List<HIS_EXP_MEST> resultData)
        {
            try
            {
                if (IsNotNullOrEmpty(inStockServiceReqs))
                {
                    List<HIS_EXP_MEST> data = new List<HIS_EXP_MEST>();
                    long? expMestReasonId = null;
                    if (IsNotNullOrEmpty(sdo.Medicines)) expMestReasonId = sdo.Medicines.FirstOrDefault().ExpMestReasonId;
                    else if (IsNotNullOrEmpty(sdo.Materials)) expMestReasonId = sdo.Materials.FirstOrDefault().ExpMestReasonId;

                    //Tao thong tin exp_mest tu thong tin service_req
                    foreach (HIS_SERVICE_REQ sr in inStockServiceReqs)
                    {
                        //lay thong tin kho xuat dua vao execute_room_id cua service_req
                        V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == sr.EXECUTE_ROOM_ID).FirstOrDefault();
                        HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                        expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                        expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT;
                        expMest.IS_STAR_MARK = sr.IS_STAR_MARK;
                        expMest.SERVICE_REQ_ID = sr.ID;
                        expMest.MEDI_STOCK_ID = mediStock.ID;
                        expMest.IS_HOME_PRES = sr.IS_HOME_PRES;
                        expMest.IS_KIDNEY = sr.IS_KIDNEY;
                        expMest.SPECIAL_MEDICINE_TYPE = sr.SPECIAL_MEDICINE_TYPE;
                        expMest.ICD_CODE = sr.ICD_CODE;
                        expMest.ICD_NAME = sr.ICD_NAME;
                        expMest.ICD_SUB_CODE = sr.ICD_SUB_CODE;
                        expMest.ICD_TEXT = sr.ICD_TEXT;
                        expMest.EXP_MEST_REASON_ID = expMestReasonId;
                        expMest.REMEDY_COUNT = sdo.RemedyCount;
                        data.Add(expMest);
                    }

                    if (!this.hisExpMestCreate.CreateList(data, inStockServiceReqs))
                    {
                        return false;
                    }
                    resultData = data;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}

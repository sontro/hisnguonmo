using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create.SaleExpMest
{
    class HisExpMestProcessor: BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Run(HIS_SERVICE_REQ prescription, List<PresOutStockMatySDO> serviceReqMaties, List<PresOutStockMetySDO> serviceReqMeties, long mediStockId, long patientTypeId, ref HIS_EXP_MEST hisExpMest)
        {
            try
            {
                HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                long? expMestReasonId = null;
                if (IsNotNullOrEmpty(serviceReqMeties))
                    expMestReasonId = serviceReqMeties.FirstOrDefault().ExpMestReasonId;
                else if (IsNotNullOrEmpty(serviceReqMaties))
                    expMestReasonId = serviceReqMaties.FirstOrDefault().ExpMestReasonId;
                //Tao exp_mest
                expMest.MEDI_STOCK_ID = mediStockId;
                expMest.SALE_PATIENT_TYPE_ID = patientTypeId;
                expMest.REQ_ROOM_ID = prescription.REQUEST_ROOM_ID;
                expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN; //xuat ban
                expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST; //xuat ban khi tao ra o trang thai duyet (Thay doi van de o trang thai yeu cau)
                expMest.PRESCRIPTION_ID = prescription.ID;
                expMest.TDL_PRESCRIPTION_REQ_USERNAME = prescription.REQUEST_USERNAME;
                expMest.TDL_PRESCRIPTION_REQ_LOGINNAME = prescription.REQUEST_LOGINNAME;
                expMest.TDL_PRES_REQ_USER_TITLE = HisEmployeeUtil.GetTitle(prescription.REQUEST_LOGINNAME);
                expMest.DESCRIPTION = prescription.DESCRIPTION;
                expMest.TDL_PRESCRIPTION_CODE = prescription.SERVICE_REQ_CODE;
                expMest.SPECIAL_MEDICINE_TYPE = prescription.SPECIAL_MEDICINE_TYPE;
                expMest.REQ_DEPARTMENT_ID = prescription.REQUEST_DEPARTMENT_ID;
                expMest.EXP_MEST_REASON_ID = expMestReasonId;

                long createYear = DateTime.Now.Year;

                //Neu co cau hinh cap STT cho loai thuoc dac biet (gay nghien, huong than, thuoc doc)
                //thi can xu ly de cap STT
                if (expMest.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN && HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK && expMest.SPECIAL_MEDICINE_TYPE.HasValue)
                {
                    expMest.SPECIAL_MEDICINE_NUM_ORDER = HisExpMestUtil.GetNextSpeciaMedicineTypeNumOrder(expMest.SPECIAL_MEDICINE_TYPE, expMest.REQ_DEPARTMENT_ID, createYear, expMest.MEDI_STOCK_ID);
                }

                HisExpMestUtil.SetTdl(expMest, prescription, true);
                
                if (!this.hisExpMestCreate.Create(expMest))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                hisExpMest = expMest;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}

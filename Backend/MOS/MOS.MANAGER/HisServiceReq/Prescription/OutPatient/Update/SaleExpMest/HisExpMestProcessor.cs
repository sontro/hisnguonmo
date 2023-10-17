using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update.SaleExpMest
{
    class HisExpMestProcessor: BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HIS_SERVICE_REQ prescription, List<PresOutStockMatySDO> serviceReqMaties, List<PresOutStockMetySDO> serviceReqMeties, long mediStockId, long patientTypeId, HIS_EXP_MEST hisExpMest)
        {
            try
            {
                long? expMestReasonId = null;
                if (IsNotNullOrEmpty(serviceReqMeties))
                    expMestReasonId = serviceReqMeties.FirstOrDefault().ExpMestReasonId;
                else if (IsNotNullOrEmpty(serviceReqMaties))
                    expMestReasonId = serviceReqMaties.FirstOrDefault().ExpMestReasonId;

                if (hisExpMest != null)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(hisExpMest);

                    this.MakeData(prescription, hisExpMest, mediStockId, patientTypeId, expMestReasonId);
                    if (!this.hisExpMestUpdate.Update(hisExpMest, before))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else
                {
                    hisExpMest = new HIS_EXP_MEST();
                    this.MakeData(prescription, hisExpMest, mediStockId, patientTypeId, expMestReasonId);
                    if (!this.hisExpMestCreate.Create(hisExpMest))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void MakeData(HIS_SERVICE_REQ prescription, HIS_EXP_MEST expMest, long mediStockId, long patientTypeId, long? expMestReasonId)
        {
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
            expMest.EXP_MEST_REASON_ID = expMestReasonId;
            HisExpMestUtil.SetTdl(expMest, prescription, true);
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}

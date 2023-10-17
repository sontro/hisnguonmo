using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Delete;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisIcd;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    class HisExpMestProcessor : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;
        private HisExpMestUpdate hisExpMestUpdate;

        internal HisExpMestProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(InPatientPresSDO data, HIS_SERVICE_REQ serviceReq, ref HIS_EXP_MEST expMest, ref List<string> sqls)
        {
            try
            {
                //Neu ko co du lieu ke thuoc trong kho thi thuc hien xoa exp_mest
                if (expMest != null && !IsNotNullOrEmpty(data.Materials) && !IsNotNullOrEmpty(data.Medicines) && !IsNotNullOrEmpty(data.SerialNumbers))
                {
                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }
                    string sql = string.Format("DELETE FROM HIS_EXP_MEST WHERE ID = {0} ", expMest.ID);
                    sqls.Add(sql);
                }
                //Neu phieu cu ko co ke don trong kho nhung phieu moi co ke thuoc trong kho thi thuc hien tao moi exp_mest
                else if (expMest == null && (IsNotNullOrEmpty(data.SerialNumbers) || IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.Medicines)))
                {
                    long? expMestReasonId = null;
                    if (IsNotNullOrEmpty(data.Medicines)) expMestReasonId = data.Medicines.FirstOrDefault().ExpMestReasonId;
                    else if (IsNotNullOrEmpty(data.Materials)) expMestReasonId = data.Materials.FirstOrDefault().ExpMestReasonId;

                    //lay thong tin kho xuat dua vao execute_room_id cua service_req
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ROOM_ID == serviceReq.EXECUTE_ROOM_ID).FirstOrDefault();
                    expMest = new HIS_EXP_MEST();
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;

                    //neu ke vao tu truc thi loai don la don tu truc, nguoc lai, la don phong kham
                    expMest.EXP_MEST_TYPE_ID = mediStock.IS_CABINET == MOS.UTILITY.Constant.IS_TRUE ?
                        IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT : IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT;
                    expMest.SERVICE_REQ_ID = serviceReq.ID;
                    expMest.MEDI_STOCK_ID = mediStock.ID;
                    expMest.IS_HOME_PRES = serviceReq.IS_HOME_PRES;
                    expMest.IS_KIDNEY = serviceReq.IS_KIDNEY;
                    expMest.ICD_CODE = serviceReq.ICD_CODE;
                    expMest.ICD_NAME = serviceReq.ICD_NAME;
                    expMest.ICD_SUB_CODE = serviceReq.ICD_SUB_CODE;
                    expMest.ICD_TEXT = serviceReq.ICD_TEXT;
                    expMest.EXP_MEST_REASON_ID = expMestReasonId;
                    expMest.REMEDY_COUNT = data.RemedyCount;

                    if (!this.hisExpMestCreate.Create(expMest, serviceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else if (expMest != null && (IsNotNullOrEmpty(data.Materials) || IsNotNullOrEmpty(data.Medicines)))
                {
                    long? expMestReasonId = null;
                    if (IsNotNullOrEmpty(data.Medicines)) expMestReasonId = data.Medicines.FirstOrDefault().ExpMestReasonId;
                    else if (IsNotNullOrEmpty(data.Materials)) expMestReasonId = data.Materials.FirstOrDefault().ExpMestReasonId;

                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST old = Mapper.Map<HIS_EXP_MEST>(expMest);

                    expMest.ICD_CODE = serviceReq.ICD_CODE;
                    expMest.ICD_NAME = serviceReq.ICD_NAME;
                    expMest.ICD_SUB_CODE = serviceReq.ICD_SUB_CODE;
                    expMest.ICD_TEXT = serviceReq.ICD_TEXT;
                    expMest.EXP_MEST_REASON_ID = expMestReasonId;
                    expMest.REMEDY_COUNT = data.RemedyCount;
                    HisExpMestUtil.SetTdl(expMest, serviceReq);
                    if (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_EXP_MEST>(old, expMest))
                    {
                        this.hisExpMestUpdate.Update(expMest, old);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal void Rollback()
        {
            this.hisExpMestCreate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}

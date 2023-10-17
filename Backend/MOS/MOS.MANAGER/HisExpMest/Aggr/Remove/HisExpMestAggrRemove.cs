using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisExpMest.Aggr.Remove
{
    /// <summary>
    /// Remove 1 phiếu nội trú ra khỏi phiếu lĩnh:
    /// - Phải làm việc tại kho hoặc tại khoa yêu cầu
    /// - Phiếu lĩnh phải chưa được duyệt
    /// </summary>
    partial class HisExpMestAggrRemove : BusinessBase
    {
        private HisExpMestUpdate hisExpMestUpdate;
        private HisExpMestUpdate hisParentUpdate;

        internal HisExpMestAggrRemove()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrRemove(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestUpdate = new HisExpMestUpdate(param);
            this.hisParentUpdate = new HisExpMestUpdate(param);
        }

        internal bool Run(HisExpMestSDO data, ref HIS_EXP_MEST resultData)
        {
            bool result = false;
            try
            {
                HIS_EXP_MEST expMest = null;
                HIS_EXP_MEST parent = null;
                List<HIS_EXP_MEST> allChilds = null;
                bool valid = true;
                HisExpMestAggrRemoveCheck checker = new HisExpMestAggrRemoveCheck(param);
                HisExpMestCheck commonChecker = new HisExpMestCheck(param);
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && checker.IsAllowed(data, ref expMest);
                valid = valid && commonChecker.VerifyId(expMest.AGGR_EXP_MEST_ID.Value, ref parent);
                valid = valid && checker.IsValidChilds(expMest.AGGR_EXP_MEST_ID.Value, expMest.EXP_MEST_CODE, ref allChilds);
                if (valid)
                {
                    Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
                    HIS_EXP_MEST before = Mapper.Map<HIS_EXP_MEST>(expMest);
                    expMest.AGGR_EXP_MEST_ID = null;
                    expMest.TDL_AGGR_EXP_MEST_CODE = null;
                    if (!this.hisExpMestUpdate.Update(expMest, before))
                    {
                        throw new Exception("Cap nhat aggr_exp_mest_id ==> null that bai");
                    }

                    this.ProcessParent(expMest, parent, allChilds);

                    List<string> sqls = new List<string>();
                    sqls.Add(String.Format("UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = NULL WHERE EXP_MEST_ID = {0} ", expMest.ID));
                    sqls.Add(String.Format("UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = NULL WHERE EXP_MEST_ID = {0} ", expMest.ID));
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sqls: " + sqls.ToString());
                    }

                    resultData = expMest;

                    new EventLogGenerator(EventLog.Enum.HisExpMest_XoaKhoiPhieuLinh).AggrExpMestCode(before.TDL_AGGR_EXP_MEST_CODE).ExpMestCode(resultData.EXP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void ProcessParent(HIS_EXP_MEST expMest, HIS_EXP_MEST parent, List<HIS_EXP_MEST> allChilds)
        {

            Mapper.CreateMap<HIS_EXP_MEST, HIS_EXP_MEST>();
            HIS_EXP_MEST beforeParent = Mapper.Map<HIS_EXP_MEST>(parent);

            allChilds = allChilds.Where(o => o.ID != expMest.ID).ToList();


            bool isUpdate = false;
            if (!string.IsNullOrWhiteSpace(expMest.TDL_TREATMENT_CODE) && !string.IsNullOrWhiteSpace(parent.TDL_AGGR_TREATMENT_CODE))
            {
                if (!allChilds.Exists(o => o.TDL_TREATMENT_CODE == expMest.TDL_TREATMENT_CODE))
                {
                    List<string> listStr = parent.TDL_AGGR_TREATMENT_CODE.Split(';').ToList();
                    if (IsNotNullOrEmpty(listStr))
                    {
                        if (listStr.Contains(expMest.TDL_TREATMENT_CODE))
                            listStr.Remove(expMest.TDL_TREATMENT_CODE);
                        parent.TDL_AGGR_TREATMENT_CODE = string.Join(";", listStr);
                        isUpdate = true;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(expMest.TDL_PATIENT_CODE) && !string.IsNullOrWhiteSpace(parent.TDL_AGGR_PATIENT_CODE))
            {
                if (!allChilds.Exists(o => o.TDL_PATIENT_CODE == expMest.TDL_PATIENT_CODE))
                {
                    List<string> listStr = parent.TDL_AGGR_PATIENT_CODE.Split(';').ToList();
                    if (IsNotNullOrEmpty(listStr))
                    {
                        if (listStr.Contains(expMest.TDL_PATIENT_CODE))
                            listStr.Remove(expMest.TDL_PATIENT_CODE);
                        parent.TDL_AGGR_PATIENT_CODE = string.Join(";", listStr);
                        isUpdate = true;
                    }
                }
            }
            if (isUpdate)
            {
                if (!this.hisParentUpdate.Update(parent, beforeParent))
                {
                    throw new Exception("Cap nhat phieu linh cha that bai.Rollback du lieu");
                }
            }
        }

        private void RollBack()
        {
            this.hisParentUpdate.RollbackData();
            this.hisExpMestUpdate.RollbackData();
        }
    }
}

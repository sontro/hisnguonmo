using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Manu
{
    class HisImpMestManuUpdateInfo : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;

        internal HisImpMestManuUpdateInfo()
            : base()
        {

        }

        internal HisImpMestManuUpdateInfo(CommonParam param)
            : base(param)
        {

        }

        internal bool ManuUpdateInfo(HIS_IMP_MEST data, ref HIS_IMP_MEST resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsValidApprovalTime(data.APPROVAL_TIME);
                valid = valid && checker.IsValidImpTime(data.IMP_TIME);
                valid = valid && checker.IsValidExpMedicineAndMaterial(raw, data.IMP_TIME);
                if (valid)
                {
                    if (raw.IMP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Phieu nhap khong phai la nhap tu NCC");
                    }

                    this.ProcessImpMest(data, raw);

                    this.ProcessDetail(data);

                    resultData = raw;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void ProcessImpMest(HIS_IMP_MEST data, HIS_IMP_MEST raw)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST before = Mapper.Map<HIS_IMP_MEST>(raw);
            raw.DELIVERER = data.DELIVERER;
            raw.DESCRIPTION = data.DESCRIPTION;
            raw.DISCOUNT = data.DISCOUNT;
            raw.DISCOUNT_RATIO = data.DISCOUNT_RATIO;
            raw.DOCUMENT_DATE = data.DOCUMENT_DATE;
            raw.DOCUMENT_NUMBER = data.DOCUMENT_NUMBER;
            raw.DOCUMENT_PRICE = data.DOCUMENT_PRICE;
            raw.INVOICE_SYMBOL = data.INVOICE_SYMBOL;
            raw.SUPPLIER_ID = data.SUPPLIER_ID;
            raw.IMP_TIME = data.IMP_TIME;
            raw.APPROVAL_TIME = data.APPROVAL_TIME;
            if (!DAOWorker.HisImpMestDAO.Update(raw))
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin phieu nhap NCC that bai");
            }
            this.recentHisImpMest = before;
        }

        private void ProcessDetail(HIS_IMP_MEST data)
        {
            if (data != null && this.recentHisImpMest != null && data.SUPPLIER_ID != this.recentHisImpMest.SUPPLIER_ID)
            {
                List<string> sqls = new List<string>();

                //medicine
                sqls.Add("UPDATE HIS_MEDICINE M SET M.MODIFIER = :param0, M.SUPPLIER_ID = :param1 WHERE EXISTS (SELECT 1 FROM HIS_IMP_MEST_MEDICINE IM WHERE IM.IMP_MEST_ID = :param2 AND IM.MEDICINE_ID = M.ID)");

                //material
                sqls.Add("UPDATE HIS_MATERIAL M SET M.MODIFIER = :param0, M.SUPPLIER_ID = :param1 WHERE EXISTS (SELECT 1 FROM HIS_IMP_MEST_MATERIAL IM WHERE IM.IMP_MEST_ID = :param2 AND IM.MATERIAL_ID = M.ID)");

                //blood
                sqls.Add("UPDATE HIS_BLOOD M SET M.MODIFIER = :param0, M.SUPPLIER_ID = :param1 WHERE EXISTS (SELECT 1 FROM HIS_IMP_MEST_BLOOD IM WHERE IM.IMP_MEST_ID = :param2 AND IM.BLOOD_ID = M.ID)");
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (!DAOWorker.SqlDAO.Execute(sqls, loginname, data.SUPPLIER_ID, data.ID))
                {
                    throw new Exception("Update Detail that bai sql: " + sqls.ToString());
                }
            }
        }

        private void RollbackData()
        {
            if (this.recentHisImpMest != null)
            {
                if (!DAOWorker.HisImpMestDAO.Update(this.recentHisImpMest))
                {
                    LogSystem.Warn("Rollback that bai. Kiem tra lai du lieu");
                }
            }
        }
    }
}

using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCare;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCareSum
{
    class HisCareSumCreate : BusinessBase
    {
        private HIS_CARE_SUM recentHisCareSum;

        private HisCareUpdate HisCareUpdate;

        internal HisCareSumCreate()
            : base()
        {
            this.HisCareUpdate = new HisCareUpdate(param);
        }

        internal HisCareSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.HisCareUpdate = new HisCareUpdate(param);
        }

        internal bool Create(HisCareSumSDO data, ref HIS_CARE_SUM resultData)
        {
            bool result = false;
            try
            {
                this.ProcessCareSum(data);
                this.ProcessCare(data);
                this.PassResult(ref resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
        }

        private void PassResult(ref HIS_CARE_SUM resultData)
        {
            resultData = this.recentHisCareSum;
        }

        private void ProcessCare(HisCareSumSDO data)
        {
            if (!IsNotNullOrEmpty(data.CareIds))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            List<HIS_CARE> cares = new HisCareGet().GetByIds(data.CareIds);
            if (!IsNotNullOrEmpty(cares))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Du lieu ko hop le" + LogUtil.TraceData("cares", cares));
            }

            List<HIS_CARE> belongSums = cares.Where(o => o.CARE_SUM_ID.HasValue).ToList();
            if (IsNotNullOrEmpty(belongSums))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisCareSum_TonTaiPhieuChamSocDaDuocTongHop);
                throw new Exception("Phieu theo doi da duoc tong hop, ko cho phep tong hop lai" + LogUtil.TraceData("belongSums", belongSums));
            }

            cares.ForEach(o => o.CARE_SUM_ID = this.recentHisCareSum.ID);
            if (!this.HisCareUpdate.UpdateList(cares))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private void ProcessCareSum(HisCareSumSDO data)
        {
            Mapper.CreateMap<HisCareSumSDO, HIS_CARE_SUM>();
            HIS_CARE_SUM CARESum = Mapper.Map<HIS_CARE_SUM>(data);
            if (!this.Create(CARESum))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        internal bool Create(HIS_CARE_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareSumCheck checker = new HisCareSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisCareSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCareSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCareSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCareSum = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.HisCareUpdate.RollbackData();

            if (this.recentHisCareSum != null)
            {
                if (!DAOWorker.HisCareSumDAO.Truncate(this.recentHisCareSum))
                {
                    LogSystem.Warn("Rollback du lieu HisCareSum that bai, can kiem tra lai." + LogUtil.TraceData("HisCareSum", this.recentHisCareSum));
                }
            }
        }
    }
}

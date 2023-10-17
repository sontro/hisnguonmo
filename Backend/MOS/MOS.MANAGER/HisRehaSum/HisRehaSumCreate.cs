using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisServiceReq;

namespace MOS.MANAGER.HisRehaSum
{
    class HisRehaSumCreate : BusinessBase
    {
        private HIS_REHA_SUM recentHisRehaSum;

        private HisServiceReqUpdate hisServiceReqUpdate;

        internal HisRehaSumCreate()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisRehaSumCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal bool Create(HisRehaSumSDO data, ref HIS_REHA_SUM resultData)
        {
            bool result = false;
            try
            {
                this.ProcessRehaSum(data);
                this.ProcessRehaServiceReq(data);
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

        private void PassResult(ref HIS_REHA_SUM resultData)
        {
            resultData = this.recentHisRehaSum;
        }

        private void ProcessRehaServiceReq(HisRehaSumSDO data)
        {
            if (!IsNotNullOrEmpty(data.ServiceReqIds))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }

            List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(data.ServiceReqIds);
            if (!IsNotNullOrEmpty(serviceReqs))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Du lieu ko hop le" + LogUtil.TraceData("serviceReqs", serviceReqs));
            }

            List<HIS_SERVICE_REQ> belongSums = serviceReqs.Where(o => o.REHA_SUM_ID.HasValue).ToList();
            if (IsNotNullOrEmpty(belongSums))
            {
                string rehaCodes = string.Join(", ", belongSums.Select(o => o.SERVICE_REQ_CODE).ToArray());
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisRehaSum_PhieuPhucHoiChucNangDaDuocTongHopKhongChoPhepTongHopLai, rehaCodes);
                throw new Exception("Phieu phuc hoi chuc nang da duoc tong hop, ko cho phep tong hop lai" + LogUtil.TraceData("belongSums", belongSums));
            }

            serviceReqs.ForEach(o => o.REHA_SUM_ID = this.recentHisRehaSum.ID);
            if (!this.hisServiceReqUpdate.UpdateList(serviceReqs))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private void ProcessRehaSum(HisRehaSumSDO data)
        {
            Mapper.CreateMap<HisRehaSumSDO, HIS_REHA_SUM>();
            HIS_REHA_SUM rehaSum = Mapper.Map<HIS_REHA_SUM>(data);
            if (!this.Create(rehaSum))
            {
                throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
            }
        }

        private bool Create(HIS_REHA_SUM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaSumCheck checker = new HisRehaSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisRehaSumDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRehaSum_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRehaSum that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRehaSum = data;
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
            this.hisServiceReqUpdate.RollbackData();

            if (this.recentHisRehaSum != null)
            {
                if (!new HisRehaSumTruncate(param).Truncate(this.recentHisRehaSum.ID))
                {
                    LogSystem.Warn("Rollback du lieu HisRehaSum that bai, can kiem tra lai." + LogUtil.TraceData("HisRehaSum", this.recentHisRehaSum));
                }
            }
        }
    }
}

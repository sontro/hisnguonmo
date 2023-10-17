using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.LibraryEventLog;
using MOS.MANAGER.EventLogUtil;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Update.BedAmount
{
    class HisSereServUpdateBedTempAmount : BusinessBase
    {
        private HIS_SERE_SERV before = null;

        internal HisSereServUpdateBedTempAmount()
            : base()
        {
            this.Init();
        }

        internal HisSereServUpdateBedTempAmount(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Run(UpdateBedAmountSDO data, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                HIS_SERE_SERV sereServ = null;
                HIS_SERVICE_REQ serviceReq = null;
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workPlace = null;

                HisSereServCheck checker = new HisSereServCheck(param);
                HisServiceReqCheck reqCheck = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisSereServUpdateBedAmountCheck commonChecker = new HisSereServUpdateBedAmountCheck(param);

                bool valid = true;
                valid = valid && commonChecker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.VerifyId(data.SereServId, ref sereServ);
                valid = valid && checker.IsGServiceType(sereServ);
                valid = valid && commonChecker.IsValidDataForTempAmount(data, sereServ, workPlace, ref serviceReq, ref treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    this.before = Mapper.Map<HIS_SERE_SERV>(sereServ);

                    sereServ.AMOUNT_TEMP = data.Amount;
                    if (!DAOWorker.HisSereServDAO.Update(sereServ))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("sereServ", sereServ));
                    }

                    result = true;
                    resultData = sereServ;
                    new EventLogGenerator(EventLog.Enum.HisSereServ_CapNhatSoLuongGiuongTamTinh, this.before.AMOUNT, sereServ.AMOUNT)
                            .TreatmentCode(treatment.TREATMENT_CODE)
                            .ServiceReqCode(serviceReq.SERVICE_REQ_CODE).Run();
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
            if (IsNotNull(this.before))
            {
                if (!DAOWorker.HisSereServDAO.Update(this.before))
                {
                    LogSystem.Warn("Rollback cap nhat du lieu HisSereServ that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServs", this.before));
                }
            }
        }
    }
}

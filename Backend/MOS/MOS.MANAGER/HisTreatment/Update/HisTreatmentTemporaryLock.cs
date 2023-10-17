using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using His.Bhyt.ExportXml;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisHeinApproval;
using AutoMapper;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using His.Bhyt.ExportXml.Base;
using MOS.MANAGER.HisEmployee;
using Inventec.Fss.Utility;
using Inventec.Fss.Client;
using MOS.UTILITY;
using System.IO;
using MOS.MANAGER.HisSereServTein;
using MOS.MANAGER.HisTracking;

namespace MOS.MANAGER.HisTreatment
{
    class HisTreatmentTemporaryLock : BusinessBase
    {
        internal HisTreatmentTemporaryLock()
            : base()
        {

        }

        internal HisTreatmentTemporaryLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        public bool Lock(HisTreatmentTemporaryLockSDO sdo, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                WorkPlaceSDO workPlace = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                valid = valid && checker.VerifyId(sdo.TreatmentId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnLockHein(raw);
                if (valid)
                {
                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }
                    raw.IS_TEMPORARY_LOCK = Constant.IS_TRUE;

                    if (DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        result = true;
                        resultData = raw;

                        new EventLogGenerator(EventLog.Enum.HisTreatment_TamKhoaVienPhi)
                            .TreatmentCode(raw.TREATMENT_CODE).Run();
                    }
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

        public bool Unlock(HisTreatmentTemporaryLockSDO sdo, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                WorkPlaceSDO workPlace = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace);
                valid = valid && checker.VerifyId(sdo.TreatmentId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsUnLockHein(raw);
                if (valid)
                {
                    if (!workPlace.CashierRoomId.HasValue)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                        return false;
                    }
                    raw.IS_TEMPORARY_LOCK = null;

                    if (DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        result = true;
                        resultData = raw;

                        new EventLogGenerator(EventLog.Enum.HisTreatment_HuyTamKhoaVienPhi)
                            .TreatmentCode(raw.TREATMENT_CODE).Run();
                    }
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
    }
}

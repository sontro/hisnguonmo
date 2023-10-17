using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.UpdateSampleInfo
{
    class HisServiceReqUpdateSampleInfoCheck : BusinessBase
    {
        internal HisServiceReqUpdateSampleInfoCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateSampleInfoCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(ServiceReqSampleInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");
                if (!IsGreaterThanZero(data.ReqRoomId)) throw new ArgumentNullException("data.ReqRoomId");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidForCancel(HIS_SERVICE_REQ serviceReq, bool isCancel)
        {
            try
            {
                if (isCancel && IsNotNull(serviceReq))
                {
                    string sql = "SELECT * FROM HIS_SERE_SERV WHERE IS_SPECIMEN = 1 AND SERVICE_REQ_ID = :param";

                    List<HIS_SERE_SERV> existsSpecimen = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(sql, serviceReq.ID);
                    if (IsNotNullOrEmpty(existsSpecimen))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_MauDaTiepNhan);
                        return false;
                    }
                }
                else if (!isCancel && IsNotNull(serviceReq) && serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChiChoPhepThucHienKhiTrangThaiYLenhLaDangXuLy);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}

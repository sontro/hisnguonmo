using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.ApproveNotTaken
{
    class HisExpMestApproveNotTakenCheck : BusinessBase
    {
        internal HisExpMestApproveNotTakenCheck()
            : base()
        {

        }

        internal HisExpMestApproveNotTakenCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(ApproveNotTakenPresSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MediStockId)) throw new ArgumentNullException("data.MediStockId");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
                if (!IsNotNullOrEmpty(data.ExpMestIds)) throw new ArgumentNullException("data.ExpMestIds");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkPlaceIsStock(ApproveNotTakenPresSDO data, WorkPlaceSDO wp)
        {
            bool valid = true;
            try
            {
                if (data != null && wp != null)
                {
                    if (!wp.MediStockId.HasValue || wp.MediStockId.Value != data.MediStockId)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckExpMest(ApproveNotTakenPresSDO data, ref List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.MEDI_STOCK_ID = data.MediStockId;
                filter.EXP_MEST_TYPE_IDs = new List<long>(){
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT
                };
                filter.EXP_MEST_STT_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                };
                filter.IS_NOT_TAKEN = false;
                filter.IDs = data.ExpMestIds;

                List<HIS_EXP_MEST> listExp = new HisExpMestGet().Get(filter);
                if (listExp == null || listExp.Count <= 0)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongCoDonNaoCanDuyet);
                    return false;
                }
                expMests = listExp;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidAndAddArrgExamChildren(List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST> arrgExamchildren)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(expMests))
                {
                    var arrgExamIds = expMests
                        .Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                        .Select(s => s.ID).ToList();

                    if (IsNotNullOrEmpty(arrgExamIds))
                    {
                        HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                        filter.AGGR_EXP_MEST_IDs = arrgExamIds;
                        arrgExamchildren = new HisExpMestGet().Get(filter);

                        if (!IsNotNullOrEmpty(arrgExamchildren))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Khong lay duoc cac phieu xuat child theo don tong hop phong kham");
                            valid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }

            return valid;
        }
    }
}

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.TestInfo
{
    class HisExpMestTestInfoCheck : BusinessBase
    {
        internal HisExpMestTestInfoCheck()
            : base()
        {

        }

        internal HisExpMestTestInfoCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisExpMestTestInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.ExpMestBloods)) throw new ArgumentNullException("data.ExpMestBloods");
                if (data.ExpMestId <= 0) throw new ArgumentNullException("data.ExpMestId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool CheckExpMest(HIS_EXP_MEST expMest, List<HIS_EXP_MEST_BLOOD> listData)
        {
            bool valid = true;
            try
            {
                if (expMest != null && expMest.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DM)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai la don mau");
                }
                if (listData != null && listData.Exists(e => e.EXP_MEST_ID != expMest.ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai tui mau khong thuoc phieu xuat");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkPlace(long reqRoomId, HIS_EXP_MEST expMest)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO workPlace = null;
                if (!this.HasWorkPlaceInfo(reqRoomId, ref workPlace))
                {
                    return false;
                }

                if (workPlace.MediStockId != expMest.MEDI_STOCK_ID)
                {
                    V_HIS_MEDI_STOCK stock = HisMediStockCFG.DATA.FirstOrDefault(o => o.ID == expMest.MEDI_STOCK_ID);
                    string name = stock != null ? stock.MEDI_STOCK_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKho, name);
                    return false;
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

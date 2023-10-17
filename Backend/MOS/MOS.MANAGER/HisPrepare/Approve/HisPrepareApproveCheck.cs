using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPrepareMaty;
using MOS.MANAGER.HisPrepareMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisPrepare.Approve
{
    class HisPrepareApproveCheck : BusinessBase
    {
        internal HisPrepareApproveCheck()
            : base()
        {

        }

        internal HisPrepareApproveCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisPrepareApproveSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.Id <= 0) throw new ArgumentNullException("data.Id");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId");
                if (!IsNotNullOrEmpty(data.PrepareMatys) && !IsNotNullOrEmpty(data.PrepareMetys)) throw new ArgumentNullException("data.PrepareMatys && data.PrepareMetys");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool CheckApproveDetail(HisPrepareApproveSDO data, ref List<HIS_PREPARE_MATY> materials, ref List<HIS_PREPARE_METY> medicines)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.PrepareMatys) && data.PrepareMatys.GroupBy(g => g.PrepareMatyId).Any(a => a.ToList().Count > 1))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai du lieu trung PrepareMatyId");
                }
                if (IsNotNullOrEmpty(data.PrepareMetys) && data.PrepareMetys.GroupBy(g => g.PrepareMetyId).Any(a => a.ToList().Count > 1))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai du lieu trung PrepareMetyId");
                }

                if (IsNotNullOrEmpty(data.PrepareMatys) && data.PrepareMatys.Any(a => a.ApproveAmount < 0 || a.PrepareMatyId <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("ApproveAmount or PrepareMatyId invalid");
                }
                if (IsNotNullOrEmpty(data.PrepareMetys) && data.PrepareMetys.Any(a => a.ApproveAmount < 0 || a.PrepareMetyId <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("ApproveAmount or PrepareMetyId invalid");
                }

                List<HIS_PREPARE_MATY> matys = new HisPrepareMatyGet().GetByPrepareId(data.Id);
                List<HIS_PREPARE_METY> metys = new HisPrepareMetyGet().GetByPrepareId(data.Id);

                if ((IsNotNullOrEmpty(data.PrepareMatys) && !IsNotNullOrEmpty(matys))
                    || (!IsNotNullOrEmpty(data.PrepareMatys) && IsNotNullOrEmpty(matys))
                    || (IsNotNullOrEmpty(data.PrepareMatys) && IsNotNullOrEmpty(matys) && data.PrepareMatys.Count != matys.Count))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Lech du lieu vat tu gui len va trong database");
                }

                if ((IsNotNullOrEmpty(data.PrepareMetys) && !IsNotNullOrEmpty(metys))
                    || (!IsNotNullOrEmpty(data.PrepareMetys) && IsNotNullOrEmpty(metys))
                    || (IsNotNullOrEmpty(data.PrepareMetys) && IsNotNullOrEmpty(metys) && data.PrepareMetys.Count != metys.Count))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Lech du lieu thuoc gui len va trong database");
                }

                materials = matys;
                medicines = metys;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool ValidData(HisPrepareApproveListSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.Ids)) throw new ArgumentNullException("data.Ids");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

    }
}

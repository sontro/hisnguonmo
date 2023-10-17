using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisUserAccountBook;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAccountBook.Authority
{
    class HisAccountBookAuthorityCheck: BusinessBase
    {
        internal HisAccountBookAuthorityCheck()
            : base()
        {

        }

        internal HisAccountBookAuthorityCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsCashier(string loginName)
        {
            bool valid = true;
            try
            {
                HisUserRoomViewFilterQuery filter = new HisUserRoomViewFilterQuery();
                filter.LOGINNAME = loginName;
                filter.ROOM_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TN;
                filter.IS_ACTIVE = Constant.IS_TRUE;

                List<V_HIS_USER_ROOM> userRooms = new HisUserRoomGet().GetView(filter);
                if (!IsNotNullOrEmpty(userRooms))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongPhaiThuNgan, loginName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        public bool IsCashierRoom(WorkPlaceSDO workPlace)
        {
            try
            {
                if (workPlace == null || !workPlace.CashierRoomId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        public bool HasRequest(string requestLoginName, string cashierLoginName, Dictionary<string, AuthorityAccountBookSDO> authorityAccountBook, ref AuthorityAccountBookSDO request)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(requestLoginName) 
                    && authorityAccountBook != null
                    && authorityAccountBook.ContainsKey(requestLoginName))
                {
                    AuthorityAccountBookSDO r = authorityAccountBook[requestLoginName];
                    if (r != null && r.CashierLoginName == cashierLoginName)
                    {
                        request = r;
                    }
                }

                if (request == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_KhongTonTaiThongTinYeuCau, requestLoginName);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool IsAllowedAccountBook(long accountBookId, long cashierRoomId)
        {
            bool valid = true;
            try
            {
                HisCaroAccountBookFilterQuery caroFilter = new HisCaroAccountBookFilterQuery();
                caroFilter.ACCOUNT_BOOK_ID = accountBookId;
                caroFilter.CASHIER_ROOM_ID = cashierRoomId;
                var caroAccountBook = new HisCaroAccountBookGet().Get(caroFilter);

                HisUserAccountBookFilterQuery userFilter = new HisUserAccountBookFilterQuery();
                userFilter.ACCOUNT_BOOK_ID = accountBookId;
                userFilter.LOGINNAME__EXACT = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                var userAccountBook = new HisUserAccountBookGet().Get(userFilter);

                if (!IsNotNullOrEmpty(caroAccountBook) && !IsNotNullOrEmpty(userAccountBook))
                {
                    HIS_ACCOUNT_BOOK accountBook = new HisAccountBookGet().GetById(accountBookId);
                    if (accountBook == null)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("accountBookId khong ton tai");
                    }
                    else
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenSuDungSo, accountBook.ACCOUNT_BOOK_NAME);
                    }
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsAllowedRequestRoom(long requestRoomId)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                bool exists = HisUserRoomCFG.DATA != null && HisUserRoomCFG.DATA.Exists(o => o.ROOM_ID == requestRoomId && o.LOGINNAME == loginname && o.IS_ACTIVE == Constant.IS_TRUE);
                if (!exists)
                {
                    V_HIS_ROOM r = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == requestRoomId);
                    string roomName = r != null ? r.ROOM_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.TaiKhoanKhongCoQuyenVaoPhong, loginname, roomName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}

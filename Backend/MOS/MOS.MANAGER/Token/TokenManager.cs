using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.AcsUser;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisCashierRoom;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisReceptionRoom;
using MOS.MANAGER.HisBedRoom;
using MOS.MANAGER.HisDataStore;
using MOS.MANAGER.HisSampleRoom;
using MOS.MANAGER.HisWorkingShift;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisDesk;
using MOS.MANAGER.HisKskAccess;
using Inventec.Token.ResourceSystem;

namespace MOS.MANAGER.Token
{
    /// <summary>
    /// Khong cho phep thua ke
    /// </summary>
    public class TokenManager : BusinessBase
    {
        public TokenManager()
            : base()
        {

        }

        public TokenManager(CommonParam param)
            : base(param)
        {

        }

        private static string WorkingShiftKey
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "WORKING_SHIFT_KEY");
            }
        }

        private static string NurseLoginName
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "NURSE_LOGIN_NAME");
            }
        }

        private static string NurseUserName
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "NURSE_USER_NAME");
            }
        }

        private static string BranchKey
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "BRANCH_KEY");
            }
        }

        private static string WorkPlaceKey
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "WORK_PLACE");
            }
        }

        private static string AccessibleKskContract
        {
            get
            {
                return string.Format("{0}|{1}", ResourceTokenManager.GetApplicationCode(), "ACCESSIBLE_KSK_CONTRACT");
            }
        }

        /// <summary>
        /// Thiet lap theo danh sach room_id
        /// </summary>
        /// <param name="roomIds"></param>
        /// <returns></returns>
        
        public ApiResultObject<bool> RemoveOtherSession()
        {
            return this.PackSingleResult(ResourceTokenManager.RemoveOtherSession());
        }

        public ApiResultObject<List<WorkPlaceSDO>> UpdateWorkPlaceList(List<long> roomIds)
        {
            WorkInfoSDO sdo = new WorkInfoSDO();
            sdo.Rooms = roomIds != null ? roomIds.Select(o => new RoomSDO
            {
                DeskId = null,
                RoomId = o
            }).ToList() : null;
            return this.UpdateWorkInfo(sdo);
        }


        public ApiResultObject<List<WorkPlaceSDO>> UpdateWorkInfo(WorkInfoSDO sdo)
        {
            List<WorkPlaceSDO> workPlaces = null;
            string loginName = ResourceTokenManager.GetLoginName();
            bool rs = false;
            
            //Tam thoi bo sung xu ly nay de tranh loi trong truong hop client chua sua lai ham chon phong
            if (!IsNotNullOrEmpty(sdo.Rooms) && IsNotNullOrEmpty(sdo.RoomIds))
            {
                sdo.Rooms = sdo.RoomIds.Select(o => new RoomSDO { DeskId = null, RoomId = o }).ToList();
            }

            if (sdo.WorkingShiftId.HasValue)
            {
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                if (checker.VerifyId(sdo.WorkingShiftId.Value))
                {
                    ResourceTokenManager.SetCredentialData(WorkingShiftKey, sdo.WorkingShiftId);
                }
            }

            if (!string.IsNullOrWhiteSpace(sdo.NurseLoginName))
            {
                ResourceTokenManager.SetCredentialData(NurseLoginName, sdo.NurseLoginName);
                ResourceTokenManager.SetCredentialData(NurseUserName, sdo.NurseUserName);
            }

            if (IsNotNullOrEmpty(sdo.Rooms))
            {
                List<long> roomIds = sdo.Rooms.Select(o => o.RoomId).ToList();

                List<V_HIS_USER_ROOM> userRooms = this.GetUserRoom(roomIds);

                if (this.ValidateWorkPlace(userRooms, sdo.Rooms))
                {
                    workPlaces = this.MakeWorkPlaceSdo(userRooms, sdo.Rooms);
                    if (IsNotNullOrEmpty(workPlaces))
                    {
                        ResourceTokenManager.SetCredentialData(WorkPlaceKey, workPlaces);
                        //vi tat ca cac workplace phai cung 1 chi nhanh ==> chi can lay branch_id cua workplace[0]
                        this.SetBranch(workPlaces[0].BranchId);

                        this.SetRoomResponsibilityUser(roomIds);
                    }
                }
                rs = workPlaces != null;
            }
            else
            {
                ResourceTokenManager.SetCredentialData(WorkPlaceKey, null);
                rs = true;
            }
            this.SetAccessibleKskContract(loginName);

            return this.PackResult(workPlaces, rs);
        }

        private void SetAccessibleKskContract(string loginName)
        {
            List<V_HIS_KSK_ACCESS> kskAccesses = new HisKskAccessGet().GetViewByLoginName(loginName);
            List<long> kskContractIds = IsNotNullOrEmpty(kskAccesses) ? kskAccesses.Select(o => o.KSK_CONTRACT_ID).ToList(): null;
            if (IsNotNullOrEmpty(kskContractIds))
            {
                ResourceTokenManager.SetCredentialData(AccessibleKskContract, kskContractIds);
            }
        }

        public static WorkPlaceSDO GetWorkPlace(long roomId)
        {
            List<WorkPlaceSDO> workPlaceSdos = GetWorkPlaceList();
            if (workPlaceSdos != null)
            {
                return workPlaceSdos.Where(o => o.RoomId == roomId).FirstOrDefault();
            }
            return null;
        }

        internal static List<WorkPlaceSDO> GetWorkPlaceList()
        {
            List<WorkPlaceSDO> workPlaceSdos = ResourceTokenManager.GetCredentialData<List<WorkPlaceSDO>>(WorkPlaceKey);
            if (workPlaceSdos == null)
            {
                LogSystem.Warn("workPlaceList null");
            }
            return workPlaceSdos;
        }

        public HIS_BRANCH GetBranch()
        {
            HIS_BRANCH branch = ResourceTokenManager.GetCredentialData<HIS_BRANCH>(BranchKey);
            if (branch == null)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServicePaty_KhongTimThayThongTinChiNhanhDangLamViec);
                LogSystem.Warn("branch null");
            }
            return branch;
        }

        public static long? GetWorkingShift()
        {
            try
            {
                return ResourceTokenManager.GetCredentialData<long?>(WorkingShiftKey);
            }
            catch (Exception ex)
            {
                //LogSystem.Warn(ex);
            }
            return null;
        }

        public static string GetNurseLoginName()
        {
            try
            {
                return ResourceTokenManager.GetCredentialData<string>(NurseLoginName);
            }
            catch (Exception ex)
            {
               // LogSystem.Warn(ex);
            }
            return null;
        }

        public static string GetNurseUserName()
        {
            try
            {
                return ResourceTokenManager.GetCredentialData<string>(NurseUserName);
            }
            catch (Exception ex)
            {
                //LogSystem.Warn(ex);
            }
            return null;
        }

        private void SetBranch(long branchId)
        {
            HIS_BRANCH branch = HisBranchCFG.DATA != null ? HisBranchCFG.DATA.Where(o => o.ID == branchId).FirstOrDefault() : null;
            ResourceTokenManager.SetCredentialData(BranchKey, branch);
        }

        public static List<long> GetAccessibleKskContract()
        {
            try
            {
                return ResourceTokenManager.GetCredentialData<List<long>>(AccessibleKskContract);
            }
            catch (Exception ex)
            {
                //LogSystem.Warn(ex);
            }
            return null;
        }

        /// <summary>
        /// Validate du lieu
        /// </summary>
        /// <param name="userRooms"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
        private bool ValidateWorkPlace(List<V_HIS_USER_ROOM> userRooms, List<RoomSDO> rooms)
        {
            bool valid = true;
            try
            {
                List<long> roomIds = rooms != null ? rooms.Select(o => o.RoomId).ToList() : null;
                
                if (IsNotNullOrEmpty(roomIds))
                {
                    List<long> deskIds = rooms != null ? rooms.Where(o => o.DeskId.HasValue).Select(o => o.DeskId.Value).ToList() : null;

                    List<HIS_DESK> desks = new HisDeskGet().GetByIds(deskIds);
                    List<RoomSDO> invalids = rooms.Where(o => o.DeskId.HasValue && (desks == null || !desks.Exists(t => t.ID == o.DeskId.Value && t.ROOM_ID == o.RoomId))).ToList();
                    if (IsNotNullOrEmpty(invalids))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Du lieu 'ban' khong hop le" + LogUtil.TraceData("invalids", invalids));
                        return false;
                    }

                    if (!IsNotNullOrEmpty(userRooms) || roomIds.Count > userRooms.Count)
                    {
                        List<long> notExists = roomIds.Where(o => userRooms == null || !userRooms.Where(t => t.ROOM_ID == o).Any()).ToList();
                        if (IsNotNullOrEmpty(notExists))
                        {
                            List<string> roomNames = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => notExists.Contains(o.ID)).Select(o => o.ROOM_NAME).ToList() : null;
                            string roomNameStr = IsNotNullOrEmpty(roomNames) ? string.Join(",", roomNames) : "";
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.Token_KhongChoPhepTruyCapVaoCacPhong, roomNameStr);
                            return false;
                        }
                    }

                    List<long> distinctBranchIds = userRooms.Select(o => o.BRANCH_ID).Distinct().ToList();
                    if (distinctBranchIds != null && distinctBranchIds.Count > 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.Token_KhongChoPhepChonPhongTaiNhieuChiNhanh);
                        return false;
                    }
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

        //Tao data
        private List<WorkPlaceSDO> MakeWorkPlaceSdo(List<V_HIS_USER_ROOM> userRooms, List<RoomSDO> rooms)
        {
            if (IsNotNullOrEmpty(rooms))
            {
                List<WorkPlaceSDO> result = new List<WorkPlaceSDO>();
                foreach (RoomSDO roomSdo in rooms)
                {
                    WorkPlaceSDO sdo = new WorkPlaceSDO();
                    V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == roomSdo.RoomId).FirstOrDefault();

                    sdo.GCode = room.G_CODE;
                    sdo.GroupCode = room.GROUP_CODE;
                    sdo.BranchId = room.BRANCH_ID;
                    sdo.BranchCode = room.BRANCH_CODE;
                    sdo.BranchName = room.BRANCH_NAME;
                    sdo.RoomId = room.ID;
                    sdo.RoomCode = room.ROOM_CODE;
                    sdo.RoomName = room.ROOM_NAME;
                    sdo.DepartmentId = room.DEPARTMENT_ID;
                    sdo.DepartmentName = room.DEPARTMENT_NAME;
                    sdo.DepartmentCode = room.DEPARTMENT_CODE;
                    sdo.RoomTypeId = room.ROOM_TYPE_ID;
                    sdo.DeskId = roomSdo.DeskId;

                    if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BUONG)
                    {
                        V_HIS_BED_ROOM r = HisBedRoomCFG.DATA != null ? HisBedRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.BedRoomId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TN)
                    {
                        V_HIS_CASHIER_ROOM r = HisCashierRoomCFG.DATA != null ? HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.CashierRoomId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__XL)
                    {
                        V_HIS_EXECUTE_ROOM r = HisExecuteRoomCFG.DATA != null ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.ExecuteRoomId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TD)
                    {
                        V_HIS_RECEPTION_ROOM r = HisReceptionRoomCFG.DATA != null ? HisReceptionRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.ReceptionRoomId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__BP)
                    {
                        V_HIS_SAMPLE_ROOM r = HisSampleRoomCFG.DATA != null ? HisSampleRoomCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.SampleRoomId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__KHO)
                    {
                        V_HIS_MEDI_STOCK r = HisMediStockCFG.DATA != null ? HisMediStockCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.MediStockId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__LT)
                    {
                        V_HIS_DATA_STORE r = HisDataStoreCFG.DATA != null ? HisDataStoreCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.DataStoreId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    else if (room.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TR)
                    {
                        V_HIS_STATION r = HisStationCFG.DATA != null ? HisStationCFG.DATA.Where(o => o.ROOM_ID == sdo.RoomId && o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE).FirstOrDefault() : null;
                        sdo.StationId = r != null ? new Nullable<long>(r.ID) : null;
                    }
                    result.Add(sdo);
                }
                return result;
            }
            return null;
        }

        private List<V_HIS_USER_ROOM> GetUserRoom(List<long> roomIds)
        {
            HisUserRoomViewFilterQuery filter = new HisUserRoomViewFilterQuery();
            filter.ROOM_IDs = roomIds;
            filter.LOGINNAME = ResourceTokenManager.GetLoginName();
            return new HisUserRoomGet().GetView(filter);
        }

        private void SetRoomResponsibilityUser(List<long> roomIds)
        {
            try
            {
                if (IsNotNullOrEmpty(roomIds) && HisRoomCFG.UPDATE_RESPONSIBILITY_USER_OF_ROOM)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    HIS_EMPLOYEE employee = HisEmployeeUtil.GetEmployee(loginName);
                    if (employee != null && !employee.IS_ADMIN.HasValue)
                    {
                        string ids = string.Join(",", roomIds);
                        string sql = string.Format("UPDATE HIS_ROOM SET RESPONSIBLE_LOGINNAME = :param1, RESPONSIBLE_USERNAME = :param2 WHERE ID IN ({0})", ids);
                        if (!DAOWorker.SqlDAO.Execute(sql, loginName, userName))
                        {
                            LogSystem.Warn("Cap nhat RESPONSIBLE_LOGINNAME, RESPONSIBLE_USERNAME that bai");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}

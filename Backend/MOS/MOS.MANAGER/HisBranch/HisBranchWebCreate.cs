using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBedRoom;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBranch
{
    partial class HisBranchWebCreate : BusinessBase
    {
        private HIS_BRANCH recentHisBranch;
        private HIS_DEPARTMENT recentHisDepartment;
        private List<HIS_EXECUTE_ROOM> recentHisExecuteRooms = new List<HIS_EXECUTE_ROOM>();
        private List<HIS_BED_ROOM> recentHisBedRooms = new List<HIS_BED_ROOM>();
        private List<HIS_ROOM> recentHisRooms = new List<HIS_ROOM>();

        internal HisBranchWebCreate()
            : base()
        {
            this.Init();
        }

        internal HisBranchWebCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
        }

        internal bool Create(HisBranchWebSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBranchCheck checker = new HisBranchCheck(param);
                valid = valid && checker.VerifyRequireField(data.Branch);
                valid = valid && this.VerifyRequireField(data.Branch);
                valid = valid && checker.ExistsCode(data.Branch.BRANCH_CODE, null);
                if (valid)
                {
                    //tao tai khoan truoc
                    this.ProcessAcsUser(data);

                    this.ProcessImage(data);

                    if (!DAOWorker.HisBranchDAO.Create(data.Branch))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranch_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBranch that bai." + LogUtil.TraceData("data", data));
                    }

                    this.recentHisBranch = data.Branch;

                    //tao khoa
                    this.ProcessDepartment();

                    //Gan tai khoan phong
                    this.ProcessUserRoom();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.Rollback();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessUserRoom()
        {
            if (IsNotNullOrEmpty(this.recentHisRooms))
            {
                List<HIS_USER_ROOM> listUserRoom = (from r in this.recentHisRooms select new HIS_USER_ROOM() { ROOM_ID = r.ID, LOGINNAME = this.recentHisBranch.BRANCH_CODE.ToLower() }).ToList();

                if (!new HisUserRoomCreate(param).CreateList(listUserRoom))
                {
                    throw new Exception("Them moi thong tin tai khoan phong that bai." + LogUtil.TraceData("listUserRoom", listUserRoom));
                }
            }
        }

        private void ProcessDepartment()
        {
            HIS_DEPARTMENT department = new HIS_DEPARTMENT();
            department.BRANCH_ID = this.recentHisBranch.ID;
            department.G_CODE = "0000000001";
            department.DEPARTMENT_CODE = "KKB_" + this.recentHisBranch.HEIN_MEDI_ORG_CODE;
            department.DEPARTMENT_NAME = "Khoa Khám bệnh";

            if (!new HisDepartmentCreate(param).Create(department))
            {
                throw new Exception("Them moi thong tin khoa that bai." + LogUtil.TraceData("department", department));
            }

            this.recentHisDepartment = department;

            List<HisExecuteRoomSDO> listExecuteRoom = new List<HisExecuteRoomSDO>();

            HisExecuteRoomSDO examRoom = new HisExecuteRoomSDO();
            examRoom.HisExecuteRoom = new HIS_EXECUTE_ROOM();
            examRoom.HisRoom = new HIS_ROOM();

            examRoom.HisExecuteRoom.IS_EXAM = Constant.IS_TRUE;
            examRoom.HisExecuteRoom.EXECUTE_ROOM_CODE = "PK_" + this.recentHisBranch.HEIN_MEDI_ORG_CODE;
            examRoom.HisExecuteRoom.EXECUTE_ROOM_NAME = "Phòng khám";

            examRoom.HisRoom.DEPARTMENT_ID = department.ID;

            listExecuteRoom.Add(examRoom);

            HisExecuteRoomSDO testRoom = new HisExecuteRoomSDO();
            testRoom.HisExecuteRoom = new HIS_EXECUTE_ROOM();
            testRoom.HisRoom = new HIS_ROOM();

            testRoom.HisExecuteRoom.IS_TEST = Constant.IS_TRUE;
            testRoom.HisExecuteRoom.EXECUTE_ROOM_CODE = "PXN_" + this.recentHisBranch.HEIN_MEDI_ORG_CODE;
            testRoom.HisExecuteRoom.EXECUTE_ROOM_NAME = "Phòng xét nghiệm";

            testRoom.HisRoom.DEPARTMENT_ID = department.ID;

            listExecuteRoom.Add(testRoom);

            if (!new HisExecuteRoomCreate(param).CreateList(listExecuteRoom))
            {
                throw new Exception("Them moi thong tin phong xu ly that bai." + LogUtil.TraceData("listExecuteRoom", listExecuteRoom));
            }

            this.recentHisExecuteRooms.AddRange(listExecuteRoom.Select(s => s.HisExecuteRoom));
            this.recentHisRooms.AddRange(listExecuteRoom.Select(s => s.HisRoom));

            HisBedRoomSDO bedRoom = new HisBedRoomSDO();
            bedRoom.HisBedRoom = new HIS_BED_ROOM();
            bedRoom.HisRoom = new HIS_ROOM();

            bedRoom.HisBedRoom.BED_ROOM_CODE = "BB_" + this.recentHisBranch.HEIN_MEDI_ORG_CODE;
            bedRoom.HisBedRoom.BED_ROOM_NAME = "Buồng bệnh";

            bedRoom.HisRoom.DEPARTMENT_ID = department.ID;

            if (!new HisBedRoomCreate(param).Create(bedRoom))
            {
                throw new Exception("Them moi thong tin buong benh that bai." + LogUtil.TraceData("bedRoom", bedRoom));
            }

            this.recentHisBedRooms.Add(bedRoom.HisBedRoom);
            this.recentHisRooms.Add(bedRoom.HisRoom);
        }

        private void ProcessAcsUser(HisBranchWebSDO data)
        {
            ACS.SDO.CreateAndGrantUserSDO sdo = new ACS.SDO.CreateAndGrantUserSDO();
            sdo.RoleCode = HisBranchCFG.ACS_USER_ROLE;
            sdo.AppCode = data.AppCode;
            sdo.LoginName = data.Branch.BRANCH_CODE;
            sdo.UserName = data.Branch.BRANCH_NAME;
            sdo.Email = data.Email;
            sdo.Mobile = data.Mobile;
            sdo.IsActive = true;

            bool apiResult = ApiConsumerManager.ApiConsumerStore.AcsConsumerWrapper.Post<bool>(true, "api/AcsUser/CreateAndGrant", this.param, sdo);
            if (!apiResult)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBranch_ThemMoiThatBai);
                throw new Exception("Them moi thong tin tai khoan dang nhap that bai." + LogUtil.TraceData("sdo", sdo));
            }
        }

        private void ProcessImage(HisBranchWebSDO data)
        {
            try
            {
                if (data.ImageData != null && data.ImageData.Length > 0)
                {
                    List<FileHolder> fileHolders = new List<FileHolder>();
                    FileHolder bhytFile = new FileHolder();
                    MemoryStream bhytStream = new MemoryStream();
                    bhytStream.Write(data.ImageData, 0, data.ImageData.Length);
                    bhytStream.Position = 0;
                    bhytFile.Content = bhytStream;
                    bhytFile.FileName = data.Branch.BRANCH_CODE + "_LOGO.jpeg";
                    fileHolders.Add(bhytFile);
                    string url = "";
                    List<FileUploadInfo> fileUploadInfos = FileUpload.UploadFile(Constant.APPLICATION_CODE, FileStoreLocation.BRANCH, fileHolders, true);
                    if (fileUploadInfos != null && fileUploadInfos.Count == fileHolders.Count)
                    {
                        foreach (FileUploadInfo info in fileUploadInfos)
                        {
                            if (!String.IsNullOrWhiteSpace(info.OriginalName))
                            {
                                if (info.OriginalName.Contains(data.Branch.BRANCH_CODE))
                                {
                                    url = info.Url;
                                    data.Branch.LOGO_URL = info.Url;
                                }
                            }
                        }
                    }
                    if (String.IsNullOrWhiteSpace(url))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisBranch_LuuAnhLogoThatBai);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private bool VerifyRequireField(HIS_BRANCH data)
        {
            bool valid = true;
            try
            {
                if (data.HEIN_MEDI_ORG_CODE == null) throw new ArgumentNullException("HEIN_MEDI_ORG_CODE");

                if (string.IsNullOrWhiteSpace(data.BRANCH_CODE))
                {
                    data.BRANCH_CODE = string.Format("BV_{0}", data.HEIN_MEDI_ORG_CODE);
                }
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

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentHisExecuteRooms))
                {
                    if (!new HisExecuteRoomTruncate().TruncateList(this.recentHisExecuteRooms))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Rollback. Xoa thong tin phong xu ly that bai");
                    }
                }

                if (IsNotNullOrEmpty(this.recentHisBedRooms))
                {
                    if (!new HisBedRoomTruncate().TruncateList(this.recentHisBedRooms))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Rollback. Xoa thong tin buong benh that bai");
                    }
                }

                if (IsNotNullOrEmpty(this.recentHisRooms))
                {
                    if (!new HisRoomTruncate().TruncateList(this.recentHisRooms))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Rollback. Xoa thong tin phong that bai");
                    }
                }

                if (this.recentHisDepartment != null)
                {
                    if (!new HisDepartmentTruncate().Truncate(this.recentHisDepartment))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Rollback. Xoa thong tin khoa that bai");
                    }
                }

                if (this.recentHisBranch != null)
                {
                    if (!DAOWorker.HisBranchDAO.Truncate(this.recentHisBranch))
                    {
                        Inventec.Common.Logging.LogSystem.Error("Rollback. Xoa thong tin chi nhanh that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

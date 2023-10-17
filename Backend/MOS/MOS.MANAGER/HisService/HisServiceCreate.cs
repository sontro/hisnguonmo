using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisService
{
    partial class HisServiceCreate : BusinessBase
    {
        private List<HIS_SERVICE> recentHisServiceDTOs = new List<HIS_SERVICE>();

        internal HisServiceCreate()
            : base()
        {

        }

        internal HisServiceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceCheck checker = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_CODE, null);
                valid = valid && checker.IsValidData(data);

                if (valid)
                {
                    HIS_SERVICE parent = null;
                    var updateParent = (data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU);
                    if (data.PARENT_ID.HasValue)
                    {
                        parent = new HisServiceGet().GetById(data.PARENT_ID.Value);
                        if (parent == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ParentId invalid:" + LogUtil.TraceData("data", data.PARENT_ID));
                        }
                        if (parent.SERVICE_TYPE_ID != data.SERVICE_TYPE_ID)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ServiceTypeId cua parent khac ServiceTypeId cua con:" + LogUtil.TraceData("data", data));
                        }
                    }
                    data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                    if (!DAOWorker.HisServiceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisService that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceDTOs.Add(data);//phuc vu rollback

                    //Cap nhat Is_leaf cho dich vu cha
                    if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE && !updateParent)
                    {
                        parent.IS_LEAF = null;
                        if (!DAOWorker.HisServiceDAO.Update(parent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HIS_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceCheck checker = new HisServiceCheck(param);
                List<HIS_SERVICE> listParent = new List<HIS_SERVICE>();
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsValidData(data);

                    var updateParent = (data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU);
                    if (data.PARENT_ID.HasValue)
                    {
                        var parent = new HisServiceGet().GetById(data.PARENT_ID.Value);
                        if (parent == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ParentId invalid:" + LogUtil.TraceData("data", data.PARENT_ID));
                        }
                        if (parent.SERVICE_TYPE_ID != data.SERVICE_TYPE_ID)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("ServiceTypeId cua parent khac ServiceTypeId cua con:" + LogUtil.TraceData("data", data));
                        }
                        if (parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE && !listParent.Exists(o => o.ID == parent.ID) && !updateParent)
                        {
                            listParent.Add(parent);
                        }
                    }
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisService that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceDTOs.AddRange(listData);//Phuc vu rollback

                    if (IsNotNullOrEmpty(listParent))
                    {
                        listParent.ForEach(o => o.IS_LEAF = null);
                        if (!DAOWorker.HisServiceDAO.UpdateList(listParent))
                        {
                            throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                        }
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }

            return result;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.recentHisServiceDTOs))
            {
                if (!new HisServiceTruncate(param).TruncateList(this.recentHisServiceDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisService that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceDTOs", this.recentHisServiceDTOs));
                }
            }
        }
    }
}

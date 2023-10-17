using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisService
{
    partial class HisServiceUpdate : BusinessBase
    {
        private List<HIS_SERVICE> beforeUpdateHisServices = new List<HIS_SERVICE>();
        private List<long> typeIdNotPtttInfos = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__AN,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__G,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
        };

        internal HisServiceUpdate()
            : base()
        {

        }

        internal HisServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE data)
        {
            bool result = false;
            try
            {
                HIS_SERVICE old = null;
                result = this.Update(data, ref old);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Update(HIS_SERVICE data, ref HIS_SERVICE oldService)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceCheck checker = new HisServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERVICE_CODE, data.ID);
                valid = valid && checker.IsValidData(data);

                if (valid)
                {
                    HIS_SERVICE parent = null;
                    this.beforeUpdateHisServices.Add(raw);

                    bool updateIsLeaf = (data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__MAU
                       || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__THUOC
                       || data.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__VT);

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

                    if (!updateIsLeaf)
                    {

                        List<HIS_SERVICE> existChildren = new HisServiceGet().GetByParentId(data.ID);
                        if (IsNotNullOrEmpty(existChildren))
                        {
                            data.IS_LEAF = null;
                        }
                        else
                        {
                            data.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                        }
                    }

                    if (!DAOWorker.HisServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisService that bai." + LogUtil.TraceData("data", data));
                    }

                    if (!updateIsLeaf)
                    {
                        //set lai is_leaf = null cho dich vu parent
                        if (parent != null && parent.IS_LEAF == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            parent.IS_LEAF = null;
                            if (!new HisServiceUpdate(param).Update(parent))
                            {
                                throw new Exception("Cap nhat IS_LEAF du lieu parent that bai. Ket thuc xu ly. Rollback du lieu");
                            }
                        }
                        //set lai is_leaf = null cho dich vu parent cu
                        if (raw.PARENT_ID.HasValue)
                        {
                            List<HIS_SERVICE> children = new HisServiceGet().GetByParentId(raw.PARENT_ID.Value);
                            if (!IsNotNullOrEmpty(children))
                            {
                                HIS_SERVICE oldParent = new HisServiceGet().GetById(raw.PARENT_ID.Value);
                                oldParent.IS_LEAF = MOS.UTILITY.Constant.IS_TRUE;
                                if (!new HisServiceUpdate(param).Update(oldParent))
                                {
                                    throw new Exception("Cap nhat IS_LEAF du lieu oldParent that bai. Ket thuc xu ly. Rollback du lieu");
                                }
                            }
                        }
                    }
                    result = true;
                    oldService = raw;
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

        internal bool UpdateList(List<HIS_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceCheck checker = new HisServiceCheck(param);
                List<HIS_SERVICE> listRaw = new List<HIS_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsValidData(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisServices.AddRange(listRaw);
                    if (!DAOWorker.HisServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisService that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServices))
            {
                if (!new HisServiceUpdate(param).UpdateList(this.beforeUpdateHisServices))
                {
                    LogSystem.Warn("Rollback du lieu HisService that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceDTOs", this.beforeUpdateHisServices));
                }
            }
        }
    }
}

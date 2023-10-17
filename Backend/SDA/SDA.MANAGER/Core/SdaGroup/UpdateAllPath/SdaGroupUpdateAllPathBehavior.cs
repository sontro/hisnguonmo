using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SDA.EFMODEL.DataModels;
using System.Collections.Generic;
using SDA.MANAGER.Core.SdaGroup.Get;

namespace SDA.MANAGER.Core.SdaGroup.UpdateAllPath
{
    class SdaGroupUpdateAllPathBehavior : BeanObjectBase, ISdaGroupUpdateAllPath
    {
        SDA_GROUP entity;
        private const string SEPARATE_SEGMENT = "/";

        internal SdaGroupUpdateAllPathBehavior(CommonParam param, SDA_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupUpdateAllPath.Run()
        {
            bool result = false;
            try
            {
                SdaGroupFilterQuery filter = new SdaGroupFilterQuery();
                filter.IS_ROOT = true;
                List<SDA_GROUP> listRoot = new SdaGroupBO().Get<List<SDA_GROUP>>(filter);
                if (listRoot != null && listRoot.Count > 0)
                {
                    bool valid = true;
                    List<SDA_GROUP> listUpdate = new List<SDA_GROUP>();
                    foreach (var root in listRoot)
                    {
                        valid = valid && UpdatePath(null, root, listUpdate); //Stop xu ly ngay neu co 1 ban ghi ko thanh cong
                    }
                    if (valid)
                    {
                        result = ((listUpdate.Count == 0) || DAOWorker.SdaGroupDAO.UpdateList(listUpdate));
                    }
                    else
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.SdaGroup_CoLoiTrongQuaTrinhCapNhatPathCuaTatCaDonVi);
                        Logging("Co loi xay ra trong viec update path cua tat ca cac don vi tren he thong. Nen ham DAO.UpdateList khong duoc thuc hien.", LogType.Error);
                    }
                }
                else
                {
                    ///Co the xay ra 2 truong hop
                    ///1 - He thong chua co don vi nao
                    ///2 - Khong truy van duoc du lieu
                    ///2 truong hop nay deu duoc quy ve ket qua la xu ly that bai, NSD se can cu theo thuc te de biet la tinh huong nao
                    Logging("Khong xac dinh duoc don vi root nao tren he thong. Ham xu ly se tra lai ket qua false.", LogType.Warn);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Ham de quy, kiem tra & cap nhat lai path cua don vi neu co sai sot.
        /// Neu co sai sot --> cap nhat lai path & dua vao listUpdate.
        /// Neu khong co sai sot gi --> bo qua.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="listUpdate"></param>
        /// <returns></returns>
        private bool UpdatePath(SDA_GROUP parent, SDA_GROUP entity, List<SDA_GROUP> listUpdate)
        {
            bool result = false;
            try
            {
                bool needUpdate = false;
                string trueCodePath = "";
                string trueIdPath = "";
                if (parent != null)
                {
                    trueCodePath = parent.CODE_PATH + entity.GROUP_CODE + SEPARATE_SEGMENT;
                    trueIdPath = parent.ID_PATH + entity.ID + SEPARATE_SEGMENT;
                }
                else
                {
                    trueCodePath = SEPARATE_SEGMENT + entity.GROUP_CODE + SEPARATE_SEGMENT;
                    trueIdPath = SEPARATE_SEGMENT + entity.ID + SEPARATE_SEGMENT;
                }
                if (entity.CODE_PATH != trueCodePath)
                {
                    needUpdate = true;
                    entity.CODE_PATH = trueCodePath;
                }
                if (entity.ID_PATH != trueIdPath)
                {
                    needUpdate = true;
                    entity.ID_PATH = trueIdPath;
                }
                if (needUpdate) listUpdate.Add(entity);

                //Xu ly cac don vi con (de quy)
                SdaGroupFilterQuery filter = new SdaGroupFilterQuery();
                filter.PARENT_ID = entity.ID;
                CommonParam paramGet = new CommonParam();
                List<SDA_GROUP> listChild = new SdaGroupBO().Get<List<SDA_GROUP>>(filter);
                if (listChild != null && listChild.Count > 0)
                {
                    bool valid = true;
                    foreach (var child in listChild)
                    {
                        //De quy. Stop xu ly ngay neu co 1 ban ghi ko thanh cong
                        valid = valid && UpdatePath(entity, child, listUpdate);
                    }
                    result = valid; //Ket qua xu ly thanh cong neu de quy tat ca cac don vi con phia sau deu thanh cong
                }
                else if (paramGet.HasException)
                {
                    Logging("Co exception tai DAO.GET, khong the xu ly kiem tra path cua cac don vi con." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), LogType.Error);
                    //Khong set bugcode tai ham nay vi la ham de quy (rat nhieu bugcode lap). Set bugcode tai ham khoi tao.
                }
                else
                {
                    //entity la don vi la cuoi cung, ko co con
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logging("Co exception khi xu ly kiem tra path cua don vi." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), LogType.Error);
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}

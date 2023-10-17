using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using SDA.EFMODEL.DataModels;
using System.Collections.Generic;
using SDA.MANAGER.Core.SdaGroup.Get;

namespace SDA.MANAGER.Core.SdaGroup.UpdateWithUpdatePath
{
    class SdaGroupUpdateWithUpdatePathBehavior : BeanObjectBase, ISdaGroupUpdateWithUpdatePath
    {
        SDA_GROUP entity;
        private const string SEPARATE_SEGMENT = "/";

        internal SdaGroupUpdateWithUpdatePathBehavior(CommonParam param, SDA_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupUpdateWithUpdatePath.Run()
        {
            bool result = true;
            try
            {
                SDA_GROUP raw = null;
                if (Check(ref raw))
                {
                    entity.GROUP_CODE = raw.GROUP_CODE;
                    if (raw.PARENT_ID != entity.PARENT_ID)
                    {
                        ///Thay doi don vi cha, se thay doi path, va can cap nhat lai path cho tat ca cac don vi con neu co
                        ///Vi du:
                        ///Truoc khi update
                        /// /1/2/3/
                        /// /1/2/3/4/
                        /// /1/2/3/4/5/
                        /// /1/2/3/6/
                        /// /1/2/3/7/
                        ///Sau khi update
                        /// /10/11/3/
                        /// /10/11/3/4/
                        /// /10/11/3/4/5/
                        /// /10/11/3/6/
                        /// /10/11/3/7/
                        ///1. Lay ra doan se bi replace --> chinh la path hien tai cua du lieu (raw.ID_PATH)
                        ///2. Xac dinh doanh se replace --> chinh la path cua cha moi + group_code cua du lieu (parent_path + raw.group_code + /)
                        ///3. Replace cho toan bo du lieu bao gom don vi & tat ca cac con cua no
                        string oldStartSegmentIdPath = raw.ID_PATH;
                        string oldStartSegmentCodePath = raw.CODE_PATH;
                        string newStartSegmentIdPath = SEPARATE_SEGMENT;
                        string newStartSegmentCodePath = SEPARATE_SEGMENT;
                        if (entity.PARENT_ID.HasValue)
                        {
                            SDA_GROUP newParent = new SdaGroupBO().Get<SDA_GROUP>(entity.PARENT_ID.Value);
                            if (newParent == null)
                            {
                                result = false;
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                                Logging("Khong xac dinh duoc cha moi cua don vi." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), LogType.Error);
                            }
                            else
                            {
                                ///Can luu y viec verify khong duoc sua don vi cha --> thanh don vi con & nguoc lai da duoc xu ly truoc do
                                ///Nen o day ko can check lai
                                newStartSegmentCodePath = newParent.CODE_PATH + raw.GROUP_CODE + SEPARATE_SEGMENT;
                                newStartSegmentIdPath = newParent.ID_PATH + raw.ID + SEPARATE_SEGMENT;
                            }
                        }
                        else
                        {
                            //Khong co cha (chuyen thanh root)
                            newStartSegmentCodePath = SEPARATE_SEGMENT;
                            newStartSegmentIdPath = SEPARATE_SEGMENT;
                        }

                        if (result)
                        {
                            SdaGroupFilterQuery filter = new SdaGroupFilterQuery();
                            filter.ID_PATH = oldStartSegmentIdPath;
                            List<SDA_GROUP> listChild = new SdaGroupBO().Get<List<SDA_GROUP>>(filter);
                            if (listChild != null && listChild.Count > 0)
                            {
                                List<SDA_GROUP> listUpdate = new List<SDA_GROUP>();
                                listUpdate.Add(entity);
                                foreach (var child in listChild)
                                {
                                    if (child.ID == entity.ID)
                                    {
                                        entity.ID_PATH = newStartSegmentIdPath;
                                        entity.CODE_PATH = newStartSegmentCodePath;
                                    }
                                    else
                                    {
                                        child.ID_PATH = child.ID_PATH.Replace(oldStartSegmentIdPath, newStartSegmentIdPath);
                                        child.CODE_PATH = child.CODE_PATH.Replace(oldStartSegmentCodePath, newStartSegmentCodePath);
                                        listUpdate.Add(child);
                                    }
                                }
                                result = DAOWorker.SdaGroupDAO.UpdateList(listUpdate);
                            }
                            else
                            {
                                //Khong tim duoc ban ghi nao, vo ly vi toi thieu phai co 1 ban ghi chinh la don vi dang bi chinh sua
                                Logging("Khong tim duoc don vi nao de cap nhat lai path (bao gom ca chinh don vi dang duoc chinh sua). Day la 1 loi vo ly can kiem tra lai." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity), LogType.Error);
                                Logging("Do khong update duoc path cua cac don vi con, trong khi du lieu nay rat quan trong nen he thong khong cho phep cap nhat don vi trong truong hop nay.", LogType.Error);
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                            }
                        }
                    }
                    else
                    {
                        entity.CODE_PATH = raw.CODE_PATH;
                        entity.ID_PATH = raw.ID_PATH;
                        result = DAOWorker.SdaGroupDAO.Update(entity);
                    }
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

        bool Check(ref SDA_GROUP raw)
        {
            bool result = true;
            try
            {
                result = result && SdaGroupCheckVerifyValidData.Verify(param, entity);
                result = result && SdaGroupCheckVerifyId.Verify(param, entity.ID, ref raw);
                result = result && SdaGroupCheckVerifyIsUnlock.Verify(param, raw);
                result = result && SdaGroupCheckVerifyValidParent.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool UpdateAllPath()
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

using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAnticipateBlty;
using MOS.MANAGER.HisAnticipateMaty;
using MOS.MANAGER.HisAnticipateMety;
using MOS.MANAGER.HisBidBloodType;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBidMedicineType;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSupplier
{
    class HisSupplierCheck : BusinessBase
    {
        internal HisSupplierCheck()
            : base()
        {

        }

        internal HisSupplierCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_SUPPLIER data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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


        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_SUPPLIER data)
        {
            bool valid = true;
            try
            {
                data = new HisSupplierGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
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

        /// <summary>
        /// Kiem tra su ton tai cua danh sach cac id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyIds(List<long> listId, List<HIS_SUPPLIER> listObject)
        {
            bool valid = true;
            try
            {
                if (listId != null && listId.Count > 0)
                {
                    HisSupplierFilterQuery filter = new HisSupplierFilterQuery();
                    filter.IDs = listId;
                    List<HIS_SUPPLIER> listData = new HisSupplierGet().Get(filter);
                    if (listData == null || listId.Count != listData.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listId), listId), LogType.Error);
                        valid = false;
                    }
                    else
                    {
                        listObject.AddRange(listData);
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

        internal bool ExistsCode(string code, long? id)
        {
            bool valid = true;
            try
            {
                if (DAOWorker.HisSupplierDAO.ExistsCode(code, id))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
                    valid = false;
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

        internal bool IsUnLock(HIS_SUPPLIER data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
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

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisSupplierDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
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

        internal bool CheckConstraint(long id)
        {
            bool valid = true;
            try
            {
                List<HIS_IMP_MEST> hisImpMests = new HisImpMestGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisImpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_IMP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_EXP_MEST> hisExpMests = new HisExpMestGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisManuExpMest_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_EXP_MEST, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MATERIAL> hisMaterials = new HisMaterialGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisMaterials))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMaterial_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MATERIAL, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisMedicines))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicine_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_MEDICINE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_BLOOD> hisBloods = new HisBloodGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisBloods))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBlood_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_BLOOD, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_ANTICIPATE_METY> hisAnticipateMetys = new HisAnticipateMetyGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisAnticipateMetys))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAnticipateMety_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_ANTICIPATE_METY, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_ANTICIPATE_MATY> hisAnticipateMatys = new HisAnticipateMatyGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisAnticipateMatys))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAnticipateMaty_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_ANTICIPATE_MATY, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_ANTICIPATE_BLTY> hisAnticipateBltys = new HisAnticipateBltyGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisAnticipateBltys))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAnticipateBlty_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_ANTICIPATE_BLTY, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_BID_MEDICINE_TYPE> hisBidMedicines = new HisBidMedicineTypeGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisBidMedicines))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBidMedicineType_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_BID_MEDICINE_TYPE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_BID_MATERIAL_TYPE> hisBidMaterials = new HisBidMaterialTypeGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisBidMaterials))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBidMaterialType_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_BID_MATERIAL_TYPE, khong cho phep xoa" + LogUtil.TraceData("id", id));
                }

                List<HIS_BID_BLOOD_TYPE> hisBidBloods = new HisBidBloodTypeGet().GetBySupplierId(id);
                if (IsNotNullOrEmpty(hisBidBloods))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBidBloodType_TonTaiDuLieu);
                    throw new Exception("Ton tai du lieu HIS_BID_BLOOD_TYPE, khong cho phep xoa" + LogUtil.TraceData("id", id));
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

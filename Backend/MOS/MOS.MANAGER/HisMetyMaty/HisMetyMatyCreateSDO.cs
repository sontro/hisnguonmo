using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMetyMaty
{
    class HisMetyMatyCreateSDO : BusinessBase
    {
        private HisMetyMatyCreate hisMetyMatyCreate;
        private HisMetyMatyUpdate hisMetyMatyUpdate;
        private HisMetyMatyTruncate hisMetyMatyTruncate;

        internal HisMetyMatyCreateSDO()
            : base()
        {

        }

        internal HisMetyMatyCreateSDO(CommonParam param)
            : base(param)
        {

        }

        private void Init()
        {
            this.hisMetyMatyCreate = new HisMetyMatyCreate(param);
            this.hisMetyMatyTruncate = new HisMetyMatyTruncate(param);
            this.hisMetyMatyUpdate = new HisMetyMatyUpdate(param);
        }

        internal bool Create(HisMetyMatySDO data, ref List<HIS_METY_MATY> resultData)
        {
            bool result = false;
            try
            {
                if (this.CheckValidData(data))
                {
                    List<HIS_METY_MATY> listCreate = new List<HIS_METY_MATY>();
                    List<HIS_METY_MATY> listUpdate = new List<HIS_METY_MATY>();
                    List<HIS_METY_MATY> listDelete = new List<HIS_METY_MATY>();
                    List<HIS_METY_MATY> beforeUpdates = new List<HIS_METY_MATY>();

                    this.ProcessMetyMaty(data, ref listCreate, ref listUpdate, ref listDelete, ref beforeUpdates);

                    if (IsNotNullOrEmpty(listCreate))
                    {
                        if (!this.hisMetyMatyCreate.CreateList(listCreate))
                        {
                            throw new Exception("hisMetyMatyCreate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(listUpdate))
                    {
                        if (!this.hisMetyMatyUpdate.UpdateList(listUpdate, beforeUpdates))
                        {
                            throw new Exception("hisMetyMatyUpdate. Ket thuc nghiep vu");
                        }
                    }

                    if (IsNotNullOrEmpty(listDelete))
                    {
                        if (!this.hisMetyMatyTruncate.TruncateList(listDelete))
                        {
                            throw new Exception("hisMetyMatyTruncate. Ket thuc nghiep vu");
                        }
                    }

                    resultData = new List<HIS_METY_MATY>();
                    if (IsNotNullOrEmpty(listUpdate)) resultData.AddRange(listUpdate);
                    if (IsNotNullOrEmpty(listCreate)) resultData.AddRange(listCreate);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = false; ;
                result = false;
            }
            return result;
        }

        internal bool CreateList(List<HisMetyMatySDO> listData, ref List<HIS_METY_MATY> resultData)
        {
            bool result = false;
            try
            {
                if (!IsNotNullOrEmpty(listData))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    return false;
                }
                List<HIS_METY_MATY> listCreate = new List<HIS_METY_MATY>();
                List<HIS_METY_MATY> listUpdate = new List<HIS_METY_MATY>();
                List<HIS_METY_MATY> listDelete = new List<HIS_METY_MATY>();
                List<HIS_METY_MATY> beforeUpdates = new List<HIS_METY_MATY>();

                foreach (HisMetyMatySDO data in listData)
                {
                    if (!this.CheckValidData(data)) throw new Exception();
                    this.ProcessMetyMaty(data, ref listCreate, ref listUpdate, ref listDelete, ref beforeUpdates);
                }

                if (IsNotNullOrEmpty(listCreate))
                {
                    if (!this.hisMetyMatyCreate.CreateList(listCreate))
                    {
                        throw new Exception("hisMetyMatyCreate. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(listUpdate))
                {
                    if (!this.hisMetyMatyUpdate.UpdateList(listUpdate, beforeUpdates))
                    {
                        throw new Exception("hisMetyMatyUpdate. Ket thuc nghiep vu");
                    }
                }

                if (IsNotNullOrEmpty(listDelete))
                {
                    if (!this.hisMetyMatyTruncate.TruncateList(listDelete))
                    {
                        throw new Exception("hisMetyMatyTruncate. Ket thuc nghiep vu");
                    }
                }

                resultData = new List<HIS_METY_MATY>();
                if (IsNotNullOrEmpty(listUpdate)) resultData.AddRange(listUpdate);
                if (IsNotNullOrEmpty(listCreate)) resultData.AddRange(listCreate);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = false; ;
                result = false;
            }
            return result;
        }

        private void ProcessMetyMaty(HisMetyMatySDO data, ref List<HIS_METY_MATY> createList, ref List<HIS_METY_MATY> updateList, ref List<HIS_METY_MATY> deleteList, ref List<HIS_METY_MATY> beforeUpdates)
        {
            List<HIS_METY_MATY> existsDatas = new HisMetyMatyGet().GetByMedicineTypeId(data.MedicineTypeId);
            List<HIS_METY_MATY> deletes = existsDatas != null ? existsDatas.Where(o => !data.MaterialTypeSDOs.Exists(e => e.MaterialTypeId == o.MATERIAL_TYPE_AMOUNT)).ToList() : null;

            if (IsNotNullOrEmpty(deletes)) deleteList.AddRange(deletes);

            Mapper.CreateMap<HIS_METY_MATY, HIS_METY_MATY>();
            foreach (HisMatyAmoutSDO maty in data.MaterialTypeSDOs)
            {
                HIS_METY_MATY mema = existsDatas != null ? existsDatas.FirstOrDefault(o => o.MATERIAL_TYPE_ID == maty.MaterialTypeId) : null;
                if (mema == null)
                {
                    mema = new HIS_METY_MATY();
                    mema.MEDICINE_TYPE_ID = data.MedicineTypeId;
                    mema.MATERIAL_TYPE_ID = maty.MaterialTypeId;
                    mema.MATERIAL_TYPE_AMOUNT = maty.Amount;
                    createList.Add(mema);
                }
                else
                {
                    beforeUpdates.Add(Mapper.Map<HIS_METY_MATY>(mema));
                    mema.MEDICINE_TYPE_ID = data.MedicineTypeId;
                    mema.MATERIAL_TYPE_ID = maty.MaterialTypeId;
                    mema.MATERIAL_TYPE_AMOUNT = maty.Amount;
                    updateList.Add(mema);
                }
            }
        }

        private bool CheckValidData(HisMetyMatySDO data)
        {
            bool valid = true;
            try
            {
                valid = valid && IsNotNull(data);
                valid = valid && IsGreaterThanZero(data.MedicineTypeId);
                valid = valid && IsNotNullOrEmpty(data.MaterialTypeSDOs);
                if (!valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Thieu truong thong tin bat buoc");
                }
                foreach (HisMatyAmoutSDO maty in data.MaterialTypeSDOs)
                {
                    valid = valid && IsGreaterThanZero(maty.MaterialTypeId);
                    valid = valid && (maty.Amount > 0);
                }
                if (valid)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Du lieu dau vao khong hop le");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal void RollbackData()
        {
            try
            {
                this.hisMetyMatyUpdate.RollbackData();
                this.hisMetyMatyCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

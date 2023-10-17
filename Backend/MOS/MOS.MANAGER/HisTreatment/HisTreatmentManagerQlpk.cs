using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<ThongTinBenhNhanQlpkSDO>> GetThongTinBenhNhanQlpk(string id, string tukhoa)
        {
            ApiResultObject<List<ThongTinBenhNhanQlpkSDO>> result = null;
            try
            {
                List<ThongTinBenhNhanQlpkSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetThongTinBenhNhanQlpk(id, tukhoa);
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<DanhSachDangKyKhamQlpkSDO>> GetDanhSachDangKyKhamQlpk(string fromdate, string todate, string trangthai, string tukhoa)
        {
            ApiResultObject<List<DanhSachDangKyKhamQlpkSDO>> result = null;
            try
            {
                List<DanhSachDangKyKhamQlpkSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetDanhSachDangKyKhamQlpk(fromdate, todate, trangthai, tukhoa);
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<DuLieuDonThuocQlpkSDO>> GetDuLieuDonThuocQlpk(string id, string date)
        {
            ApiResultObject<List<DuLieuDonThuocQlpkSDO>> result = null;
            try
            {
                List<DuLieuDonThuocQlpkSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetDuLieuDonThuocQlpk(id, date);
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<DuLieuXetNghiemQlpkSDO> GetDuLieuXetNghiemQlpk(string id, string date, string loaidulieu)
        {
            ApiResultObject<DuLieuXetNghiemQlpkSDO> result = null;
            try
            {
                DuLieuXetNghiemQlpkSDO resultData = null;
                resultData = new HisTreatmentGet(param).GetDuLieuXetNghiemQlpk(id, date, loaidulieu);
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }
    }
}

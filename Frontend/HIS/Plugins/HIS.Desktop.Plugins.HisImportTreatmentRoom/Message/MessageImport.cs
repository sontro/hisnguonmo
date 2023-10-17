﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportTreatmentRoom.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá maxlength|";
        internal const string KhongHopLe = "{0} không hợp lệ|";
        internal const string ThieuTruongDL = "Thiếu trường {0}|";
        internal const string DaTonTai = " {0} đã tồn tại trong buồng bệnh|";
        internal const string DaTonTaiLoaiGiuong = " {0} đã tồn tại trong loại phòng|";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại {0} trùng nhau trong file import|";
        internal const string CoThiPhaiNhap = "Có {0} thì phải nhập {1}|";
        internal const string MaBuongDaKhoa = "Mã buồng {0} đã bị khóa| ";
        internal const string MaLoaiGiuongDaKhoa = "Mã loại phòng {0} đã bị khóa| ";
        internal const string MaPhongDieuTri= "Mã phòng điều trị {0} đã bị khóa| ";
        internal const string FileImportDaTonTai = "File import tồn tại phòng giống nhau {0} | ";
    }
}
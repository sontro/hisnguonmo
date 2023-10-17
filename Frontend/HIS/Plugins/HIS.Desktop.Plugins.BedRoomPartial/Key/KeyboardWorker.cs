using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomPartial
{
    [KeyboardAction("bbtnSearch", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "bbtnSearch", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("keyF2Focused", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "keyF2Focused", KeyStroke = XKeys.F2)]
    [KeyboardAction("BangKe", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "BangKe", KeyStroke = XKeys.F5)]
    [KeyboardAction("ChiDinhDichVu", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "ChiDinhDichVu", KeyStroke = XKeys.F9)]
    [KeyboardAction("KeDonThuoc", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "KeDonThuoc", KeyStroke = XKeys.F8)]
    [KeyboardAction("KetThucDieuTri", "HIS.Desktop.Plugins.BedRoomPartial.UCBedRoomPartial", "KetThucDieuTri", KeyStroke = XKeys.F10)]

    [ExtensionOf(typeof(DesktopToolExtensionPoint))]
    public sealed class KeyboardWorker : Tool<IDesktopToolContext>
    {
        public KeyboardWorker() : base() { }

        public override IActionSet Actions
        {
            get
            {
                return base.Actions;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}

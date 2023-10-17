using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SampleCollectionRoom
{
    [KeyboardAction("SEARCH", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "SEARCH", KeyStroke = XKeys.Control | XKeys.F)]
    [KeyboardAction("FocusF1", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "FocusF1", KeyStroke = XKeys.F1)]
    [KeyboardAction("FocusF2", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "FocusF2", KeyStroke = XKeys.F2)]
    [KeyboardAction("FocusF3", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "FocusF3", KeyStroke = XKeys.F3)]
    [KeyboardAction("PrintBarcode", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "PrintBarcode", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("ShotcurtCall", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "ShotcurtCall", KeyStroke = XKeys.F6)]
    [KeyboardAction("ShotcurtReCall", "HIS.Desktop.Plugins.SampleCollectionRoom.SampleCollectionRoomUC", "ShotcurtReCall", KeyStroke = XKeys.F7)]

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

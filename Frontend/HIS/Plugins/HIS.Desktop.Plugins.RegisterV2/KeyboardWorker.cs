using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV2
{
    [KeyboardAction("PatientNew", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "PatientNew", KeyStroke = XKeys.Control | XKeys.R)]
    [KeyboardAction("Save", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "Save", KeyStroke = XKeys.Control | XKeys.S)]
    [KeyboardAction("SaveAndPrint", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "SaveAndPrint", KeyStroke = XKeys.Control | XKeys.I)]
    [KeyboardAction("PrintKeyboard", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "PrintKeyboard", KeyStroke = XKeys.Control | XKeys.P)]
    [KeyboardAction("AssignService", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "AssignService", KeyStroke = XKeys.Control | XKeys.D)]
    [KeyboardAction("BillKeyboard", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "BillKeyboard", KeyStroke = XKeys.Control | XKeys.B)]
    [KeyboardAction("Deposit", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "Deposit", KeyStroke = XKeys.Control | XKeys.T)]
    [KeyboardAction("DepositRequest", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "DepositRequest", KeyStroke = XKeys.Control | XKeys.Shift | XKeys.T)]
    [KeyboardAction("InBed", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "InBed", KeyStroke = XKeys.Control | XKeys.G)]
    [KeyboardAction("New", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "New", KeyStroke = XKeys.Control | XKeys.N)]
    //[KeyboardAction("ClickF1", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF1", KeyStroke = XKeys.F1)]  
    [KeyboardAction("ClickF2", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF2", KeyStroke = XKeys.F2)]
    [KeyboardAction("ClickF3", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF3", KeyStroke = XKeys.F3)]
    [KeyboardAction("ClickF4", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF4", KeyStroke = XKeys.F4)]
    [KeyboardAction("ClickF5", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF5", KeyStroke = XKeys.F5)]
    [KeyboardAction("ClickF6", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF6", KeyStroke = XKeys.F6)]
    [KeyboardAction("ClickF7", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF7", KeyStroke = XKeys.F7)]
    [KeyboardAction("ClickF8", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF8", KeyStroke = XKeys.F8)]
    [KeyboardAction("ClickF9", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF9", KeyStroke = XKeys.F9)]
    [KeyboardAction("ClickF10", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF10", KeyStroke = XKeys.F10)]
    //[KeyboardAction("ClickF11", "HIS.Desktop.Plugins.RegisterV2.Run2.UCRegister", "ClickF11", KeyStroke = XKeys.F11)]
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

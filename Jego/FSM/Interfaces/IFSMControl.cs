using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Interfaces {
    interface IFSMControl {
        void RegisterCollector();
        void UnRegisterCollector();
    }
}

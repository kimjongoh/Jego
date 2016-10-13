using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Interfaces.FMS.Output {
     public interface IFSMChangeDateOutput {
        void IFSMO_ChangeDate(IFSMChangeDateParam param);
    }
}

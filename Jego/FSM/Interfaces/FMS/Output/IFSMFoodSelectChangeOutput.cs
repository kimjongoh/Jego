﻿using Jego.FSM.Interfaces.FMS.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Output {
    public interface IFSMFoodSelectChangeOutput {
        void IFSMOFoodSelectChangeOutput(IFSMFoodSelectChangeParam param);
    }
}
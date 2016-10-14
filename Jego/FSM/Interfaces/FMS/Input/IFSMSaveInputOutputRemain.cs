using JegoDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.FSM.Interfaces.FMS.Input {
    public interface IFSMSaveInputOutputRemain {
        List<Remain> GetTodayRemains();
    }
}

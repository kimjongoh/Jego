using JegoDatabase.Entities;
using JegoDatabase.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jego.Helper {
    public class RemainDeadlineHelper {
        public static void setDeadLine(Remain todayRemain, Remain exRemain) {
            if (exRemain.extra_amount + todayRemain.use_amount > 0) {
                todayRemain.deadline = exRemain.deadline;
                todayRemain.deadline_date = exRemain.deadline_date;
                todayRemain.extra_amount = exRemain.extra_amount + todayRemain.use_amount;
            } else {
                JegoManager.GetExtraAmount(todayRemain, exRemain);
            }
        }
    }
}

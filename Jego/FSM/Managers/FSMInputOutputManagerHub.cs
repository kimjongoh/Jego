using Jego.Controls.MainPages;
using Jego.Controls.MainPages.InputOutputControls;
using Jego.Controls.MainPages.RemainControls;
using Jego.Dialogs;
using Jego.FSM.Collectors;
using Jego.FSM.Managers.SubFSMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Managers {
    public class FSMInputOutputManagerHub {
        public static FSMAddFoodTypeManager GetAddFoodTypeManager() {
            FSMAddFoodTypeManager manager = new FSMAddFoodTypeManager();
            
            InputOutputContents contents = InputOutputUICollector.inputOutputContents;
            TypeManageDialog typeManageDialog = InputOutputUICollector.typeManageDialog;

            if (contents != null) {
                manager.addOutputUI(contents);
            }

            if (typeManageDialog != null) {
                manager.addOutputUI(typeManageDialog);
            }

            return manager;
        }

        public static FSMRemoveFoodTypeManager GetRemoveFoodTypeManager() {
            FSMRemoveFoodTypeManager manager = new FSMRemoveFoodTypeManager();

            InputOutputContents contents = InputOutputUICollector.inputOutputContents;

            if (contents != null) {
                manager.addOutput(contents);
            }
            return manager;
        }

        public static FSMChangeDateManager GetChangeDateManager() {
            FSMChangeDateManager manager = new FSMChangeDateManager();

            DateControl dateControl = InputOutputUICollector.dateControl;
            InputOutputContents contents = InputOutputUICollector.inputOutputContents;
            RemainContents remainContents = InputOutputUICollector.remainContents;

            if (dateControl != null) manager.addDuringUI(dateControl);
            if (contents != null) manager.addOutputUI(contents);
            if (remainContents != null) {
                manager.addDuringUI(remainContents);
                manager.addOutputUI(remainContents);
            }

            return manager;
        }

        public static FSMSaveInputOutputManager GetSaveInputOutputManager() {
            FSMSaveInputOutputManager manager = new FSMSaveInputOutputManager();

            DateControl dateControl = InputOutputUICollector.dateControl;
            InputOutputContents contents = InputOutputUICollector.inputOutputContents;

            if (dateControl != null) manager.setDateModule(dateControl);
            if (contents != null) manager.setDataModule(contents);

            return manager;
        }

        public static FSMTodayExcelWriteManager GetTodayExcelWriteManager() {
            DateControl dateControl = InputOutputUICollector.dateControl;
            FSMTodayExcelWriteManager manager;
            if (dateControl != null) {
                manager = new FSMTodayExcelWriteManager(dateControl.GetDate());
            } else {
                manager = new FSMTodayExcelWriteManager(DateTime.Now);
            }
            return manager;
        }

        public static FSMTodaySimpleExcelWriteManager GetTodaySimpleExcelWriteManager() {
            DateControl dateControl = InputOutputUICollector.dateControl;
            FSMTodaySimpleExcelWriteManager manager;
            if (dateControl != null) {
                manager = new FSMTodaySimpleExcelWriteManager(dateControl.GetDate());
            } else {
                manager = new FSMTodaySimpleExcelWriteManager(DateTime.Now);
            }
            return manager;
        }

        public static FSMExcelLoadingWindowManager GetExcelLoadingWindowManager() {
            ExcelProgressWindow progressWindow = InputOutputUICollector.excelProgressWindow;
            if(progressWindow != null)
                return new FSMExcelLoadingWindowManager(progressWindow);
            return null;
        }

        public static FSMFoodInfomationChangeManager GetFoodInfomationChangeManager() {
            RemainContents remainContents = InputOutputUICollector.remainContents;
            FSMFoodInfomationChangeManager manager = new FSMFoodInfomationChangeManager();
            if (remainContents != null)
                manager.AddOutput(remainContents);
            return manager;
        }

        
    }
}

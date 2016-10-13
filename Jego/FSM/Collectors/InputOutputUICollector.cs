using Jego.Controls.MainPages;
using Jego.Controls.MainPages.InputOutputControls;
using Jego.Controls.MainPages.RemainControls;
using Jego.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jego.FSM.Collectors {
    public static class InputOutputUICollector {
        public static InputOutputHeader inputOutputHeader = null;
        public static InputOutputFrame inputOutputFrame = null;
        public static InputOutputContents inputOutputContents = null;
        public static TypeManageDialog typeManageDialog = null;

        public static DateControl dateControl = null;
        public static ExcelProgressWindow excelProgressWindow = null;
        public static RemainContents remainContents = null;

        public static void registerInputOutputFrame(InputOutputFrame _inputOutputFrame) {
            inputOutputFrame = _inputOutputFrame;
        }
        public static void unRegisterInputOutputFrame(InputOutputFrame _inputOutputFrame) {
            if (inputOutputFrame == _inputOutputFrame)
                inputOutputFrame = null;
        }

        public static void registerInputOutputHeader(InputOutputHeader _inputOutputHeader) {
            inputOutputHeader = _inputOutputHeader;
        }

        public static void unRegisterInputOutputHeader(InputOutputHeader _inputOutputHeader) {
            if (inputOutputHeader == _inputOutputHeader)
                inputOutputHeader = null;
        }

        public static void registerInputOutputContents(InputOutputContents _inputOutputContents) {
            inputOutputContents = _inputOutputContents;
        }

        public static void unRegisterInputOutputContents(InputOutputContents _inputOutputContents) {
            if (inputOutputContents == _inputOutputContents)
                inputOutputContents = null;
        }

        public static void registerDateControl(DateControl _dateControl) {
            dateControl = _dateControl;
        }

        public static void unRegisterDateControl(DateControl _dateControl) {
            if (dateControl == _dateControl)
                dateControl = null;
        }

        public static void registerTypeManageDialog(TypeManageDialog _typeManageDialog) {
            typeManageDialog = _typeManageDialog;
        }

        public static void unRegisterTypeManageDialog(TypeManageDialog _typeManageDialog) {
            if (typeManageDialog == _typeManageDialog)
                typeManageDialog = null;
        }

        public static void registerExcelProgressWindow(ExcelProgressWindow _excelProgressWindow) {
            excelProgressWindow = _excelProgressWindow;
        }

        public static void unRegisterExcelProgressWindow(ExcelProgressWindow _excelProgressWindow) {
            if (excelProgressWindow == _excelProgressWindow)
                excelProgressWindow = null;
        }

        public static void registerRemainContents(RemainContents _remainContents) {
            remainContents = _remainContents;
        }

        public static void unRegisterRemainContents(RemainContents _remainContents) {
            if (remainContents == _remainContents)
                remainContents = null;
        }
    }
}

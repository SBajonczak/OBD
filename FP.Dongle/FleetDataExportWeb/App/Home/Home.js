/// <reference path="../App.js" />
/// <reference path="D:\TFS\GIT\FP\FP.Dongle\FleetDataExportWeb\Scripts/Fleet/FleetDataViewModel.js" />
/// <reference path="D:\TFS\GIT\FP\FP.Dongle\FleetDataExportWeb\Scripts/Fleet/FleetModel.js" />

(function () {
    "use strict";

    // The initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();

            $('#get-data-from-selection').click(getDataFromSelection);
        });
    };

    // Reads data from current document selection and displays a notification
    function getDataFromSelection() {
        var vm = new Fleet.FleetDataViewModel();
        vm.GetData();

        var myTable = new Office.TableData();
        myTable.headers = [["Cities"]];
        myTable.rows = vm.generateExcelOutput();


        Office.context.document.setSelectedDataAsync(
            vm.generateExcelOutput(), {
                coercionType: Office.CoercionType.Matrix
            }
            , function (result) {
                console.log(result);
            });
     }
})();
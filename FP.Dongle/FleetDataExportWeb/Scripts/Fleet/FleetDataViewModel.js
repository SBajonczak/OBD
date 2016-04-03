var Fleet;
(function (Fleet) {
    var FleetDataViewModel = (function () {
        function FleetDataViewModel() {
            this.Collection = new Array();
        }
        /*
            Ermittelt die Daten.
        */
        FleetDataViewModel.prototype.GetData = function () {
            for (var i = 0; i <= 1000; i++) {
                var model = new Fleet.FleetModel();
                model.generateDemoData();
                this.Collection.push(model);
            }
        };
        FleetDataViewModel.prototype.generateExcelOutput = function () {
            var data = new Array();
            $.each(this.Collection, function (index, element) {
                var subArray = new Array();
                subArray.push(element.Odometer);
                subArray.push(element.ErrorCode);
                data.push(subArray);
            });
            return data;
        };
        return FleetDataViewModel;
    })();
    Fleet.FleetDataViewModel = FleetDataViewModel;
})(Fleet || (Fleet = {}));
//# sourceMappingURL=FleetDataViewModel.js.map
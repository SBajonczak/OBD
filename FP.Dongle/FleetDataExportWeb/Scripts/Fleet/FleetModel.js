var Fleet;
(function (Fleet) {
    var FleetModel = (function () {
        function FleetModel() {
        }
        /*
            Bindet die Daten
        */
        FleetModel.prototype.bind = function (data) {
            // Noch nichts implementiert.
        };
        /*
            Generiert wirkürliche Testdaten.
        */
        FleetModel.prototype.generateDemoData = function () {
            this.Odometer = Math.random() * 1000;
            this.ErrorCode = (Math.random() * 10000).toString();
        };
        return FleetModel;
    })();
    Fleet.FleetModel = FleetModel;
})(Fleet || (Fleet = {}));
//# sourceMappingURL=FleetModel.js.map
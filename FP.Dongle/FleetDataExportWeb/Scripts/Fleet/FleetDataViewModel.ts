
module Fleet {

    export class FleetDataViewModel {

        public Collection: Array<FleetModel>;

        constructor() {

            this.Collection = new Array();
        }


        /*
            Ermittelt die Daten.
        */
        public GetData() {

            for (var i: number = 0; i <= 1000;i++) {

                var model: FleetModel = new FleetModel();
                model.generateDemoData();
                this.Collection.push(model);
            }
        }

        public generateExcelOutput(): Array<any> {
            var data: Array<any> = new Array();
            $.each(this.Collection, (index, element: FleetModel) => {

                var subArray: Array<any> = new Array();
                subArray.push(element.Odometer);
                subArray.push(element.ErrorCode);
                data.push(subArray);
            });
            return data;
        }
    }    
}
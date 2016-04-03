module Fleet {
    export class FleetModel {

        public Odometer: number;
        public ErrorCode: string;

        constructor() {


        }

        /*
            Bindet die Daten
        */
        public bind(data: any) {
            // Noch nichts implementiert.
        }

        /* 
            Generiert wirkürliche Testdaten.
        */
        public generateDemoData() {
            this.Odometer = Math.random() * 1000;
            this.ErrorCode = (Math.random() * 10000).toString();
        }

    }    
}
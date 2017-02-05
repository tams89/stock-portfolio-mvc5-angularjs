import {UpgradeAdapter} from "@angular/upgrade";

let upgradeAdapter = new UpgradeAdapter();

app.directive("MyApp",
    upgradeAdapter.downgradeNg2Component(Angular2SmallComponent));
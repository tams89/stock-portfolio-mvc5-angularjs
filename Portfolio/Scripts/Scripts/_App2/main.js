"use strict";
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var static_1 = require("@angular/upgrade/static");
var boot_1 = require("./boot");
var platform = platform_browser_dynamic_1.platformBrowserDynamic();
platform.bootstrapModule(boot_1.AppModule).then(function (platformRef) {
    var upgrade = platformRef.injector.get(static_1.UpgradeModule);
    upgrade.bootstrap(document.body, ['AlgoTrader'], { strictDi: true });
});
//# sourceMappingURL=main.js.map
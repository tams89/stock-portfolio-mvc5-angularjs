angular.module("ngLocale", [], ["$provide", function($provide) {
    var PLURAL_CATEGORY = { ZERO: "zero", ONE: "one", TWO: "two", FEW: "few", MANY: "many", OTHER: "other" };
    $provide.value("$locale", {
        "DATETIME_FORMATS": {
            "AMPMS": [
                "vorm.",
                "nachm."
            ],
            "DAY": [
                "Sonntag",
                "Montag",
                "Dienstag",
                "Mittwoch",
                "Donnerstag",
                "Freitag",
                "Samstag"
            ],
            "MONTH": [
                "J\u00e4nner",
                "Februar",
                "M\u00e4rz",
                "April",
                "Mai",
                "Juni",
                "Juli",
                "August",
                "September",
                "Oktober",
                "November",
                "Dezember"
            ],
            "SHORTDAY": [
                "So.",
                "Mo.",
                "Di.",
                "Mi.",
                "Do.",
                "Fr.",
                "Sa."
            ],
            "SHORTMONTH": [
                "J\u00e4n",
                "Feb",
                "M\u00e4r",
                "Apr",
                "Mai",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
                "Okt",
                "Nov",
                "Dez"
            ],
            "fullDate": "EEEE, dd. MMMM y",
            "longDate": "dd. MMMM y",
            "medium": "dd.MM.yyyy HH:mm:ss",
            "mediumDate": "dd.MM.yyyy",
            "mediumTime": "HH:mm:ss",
            "short": "dd.MM.yy HH:mm",
            "shortDate": "dd.MM.yy",
            "shortTime": "HH:mm"
        },
        "NUMBER_FORMATS": {
            "CURRENCY_SYM": "\u20ac",
            "DECIMAL_SEP": ",",
            "GROUP_SEP": ".",
            "PATTERNS": [
                {
                    "gSize": 3,
                    "lgSize": 3,
                    "macFrac": 0,
                    "maxFrac": 3,
                    "minFrac": 0,
                    "minInt": 1,
                    "negPre": "-",
                    "negSuf": "",
                    "posPre": "",
                    "posSuf": ""
                },
                {
                    "gSize": 3,
                    "lgSize": 3,
                    "macFrac": 0,
                    "maxFrac": 2,
                    "minFrac": 2,
                    "minInt": 1,
                    "negPre": "\u00a4\u00a0-",
                    "negSuf": "",
                    "posPre": "\u00a4\u00a0",
                    "posSuf": ""
                }
            ]
        },
        "id": "de-at",
        "pluralCat": function(n) {
            if (n == 1) {
                return PLURAL_CATEGORY.ONE;
            }
            return PLURAL_CATEGORY.OTHER;
        }
    });
}]);
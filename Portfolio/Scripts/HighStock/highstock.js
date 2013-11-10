/*
 Highstock JS v1.3.7 (2013-10-24)

 (c) 2009-2013 Torstein Hønsi

 License: www.highcharts.com/license
*/
(function() {

    function x(a, b) {
        var c;
        a || (a = {});
        for (c in b) a[c] = b[c];
        return a;
    }

    function u() {
        var a, b = arguments.length, c = {}, d = function(a, b) {
            var c, h;
            typeof a !== "object" && (a = {});
            for (h in b) b.hasOwnProperty(h) && (c = b[h], a[h] = c && typeof c === "object" && Object.prototype.toString.call(c) !== "[object Array]" && typeof c.nodeType !== "number" ? d(a[h] || {}, c) : b[h]);
            return a;
        };
        for (a = 0; a < b; a++) c = d(c, arguments[a]);
        return c;
    }

    function lb() {
        for (var a = 0, b = arguments, c = b.length, d = {}; a < c; a++) d[b[a++]] = b[a];
        return d;
    }

    function B(a, b) {
        return parseInt(a,
            b || 10);
    }

    function ka(a) { return typeof a === "string"; }

    function $(a) { return typeof a === "object"; }

    function Ya(a) { return Object.prototype.toString.call(a) === "[object Array]"; }

    function sa(a) { return typeof a === "number"; }

    function ta(a) { return S.log(a) / S.LN10; }

    function la(a) { return S.pow(10, a); }

    function ma(a, b) {
        for (var c = a.length; c--;)
            if (a[c] === b) {
                a.splice(c, 1);
                break;
            }
    }

    function r(a) { return a !== s && a !== null; }

    function C(a, b, c) {
        var d, e;
        if (ka(b)) r(c) ? a.setAttribute(b, c) : a && a.getAttribute && (e = a.getAttribute(b));
        else if (r(b) &&
            $(b)) for (d in b) a.setAttribute(d, b[d]);
        return e;
    }

    function fa(a) { return Ya(a) ? a : [a]; }

    function o() {
        var a = arguments, b, c, d = a.length;
        for (b = 0; b < d; b++) if (c = a[b], typeof c !== "undefined" && c !== null) return c;
    }

    function G(a, b) {
        if (Ba && b && b.opacity !== s) b.filter = "alpha(opacity=" + b.opacity * 100 + ")";
        x(a.style, b);
    }

    function Y(a, b, c, d, e) {
        a = J.createElement(a);
        b && x(a, b);
        e && G(a, { padding: 0, border: Z, margin: 0 });
        c && G(a, c);
        d && d.appendChild(a);
        return a;
    }

    function ba(a, b) {
        var c = function() {
        };
        c.prototype = new a;
        x(c.prototype, b);
        return c;
    }

    function Ca(a, b, c, d) {
        var e = K.lang, a = +a || 0, f = b === -1 ? (a.toString().split(".")[1] || "").length : isNaN(b = M(b)) ? 2 : b, b = c === void 0 ? e.decimalPoint : c, d = d === void 0 ? e.thousandsSep : d, e = a < 0 ? "-" : "", c = String(B(a = M(a).toFixed(f))), g = c.length > 3 ? c.length % 3 : 0;
        return e + (g ? c.substr(0, g) + d : "") + c.substr(g).replace(/(\d{3})(?=\d)/g, "$1" + d) + (f ? b + M(a - c).toFixed(f).slice(2) : "");
    }

    function Na(a, b) { return Array((b || 2) + 1 - String(a).length).join(0) + a; }

    function ga(a, b, c) {
        var d = a[b];
        a[b] = function() {
            var a = Array.prototype.slice.call(arguments);
            a.unshift(d);
            return c.apply(this, a);
        };
    }

    function Oa(a, b) {
        for (var c = "{", d = !1, e, f, g, h, i, k = []; (c = a.indexOf(c)) !== -1;) {
            e = a.slice(0, c);
            if (d) {
                f = e.split(":");
                g = f.shift().split(".");
                i = g.length;
                e = b;
                for (h = 0; h < i; h++) e = e[g[h]];
                if (f.length) f = f.join(":"), g = /\.([0-9])/, h = K.lang, i = void 0, /f$/.test(f) ? (i = (i = f.match(g)) ? i[1] : -1, e = Ca(e, i, h.decimalPoint, f.indexOf(",") > -1 ? h.thousandsSep : "")) : e = qa(f, e);
            }
            k.push(e);
            a = a.slice(c + 1);
            c = (d = !d) ? "}" : "{";
        }
        k.push(a);
        return k.join("");
    }

    function yb(a) { return S.pow(10, O(S.log(a) / S.LN10)); }

    function zb(a, b, c, d) {
        var e, c = o(c, 1);
        e = a / c;
        b || (b = [1, 2, 2.5, 5, 10], d && d.allowDecimals === !1 && (c === 1 ? b = [1, 2, 5, 10] : c <= 0.1 && (b = [1 / c])));
        for (d = 0; d < b.length; d++) if (a = b[d], e <= (b[d] + (b[d + 1] || b[d])) / 2) break;
        a *= c;
        return a;
    }

    function Ab(a, b) {
        var c = b || [[mb, [1, 2, 5, 10, 20, 25, 50, 100, 200, 500]], [eb, [1, 2, 5, 10, 15, 30]], [Za, [1, 2, 5, 10, 15, 30]], [Da, [1, 2, 3, 4, 6, 8, 12]], [da, [1, 2]], [Pa, [1, 2]], [Qa, [1, 2, 3, 4, 6]], [ua, null]], d = c[c.length - 1], e = H[d[0]], f = d[1], g;
        for (g = 0; g < c.length; g++)
            if (d = c[g], e = H[d[0]], f = d[1], c[g + 1] && a <= (e * f[f.length - 1] +
                H[c[g + 1][0]]) / 2) break;
        e === H[ua] && a < 5 * e && (f = [1, 2, 5]);
        c = zb(a / e, f, d[0] === ua ? v(yb(a / e), 1) : 1);
        return { unitRange: e, count: c, unitName: d[0] };
    }

    function fb(a, b, c, d) {
        var e = [], f = {}, g = K.global.useUTC, h, i = new Date(b), k = a.unitRange, j = a.count;
        if (r(b)) {
            k >= H[eb] && (i.setMilliseconds(0), i.setSeconds(k >= H[Za] ? 0 : j * O(i.getSeconds() / j)));
            if (k >= H[Za]) i[Nb](k >= H[Da] ? 0 : j * O(i[Bb]() / j));
            if (k >= H[Da]) i[Ob](k >= H[da] ? 0 : j * O(i[Cb]() / j));
            if (k >= H[da]) i[Db](k >= H[Qa] ? 1 : j * O(i[Ra]() / j));
            k >= H[Qa] && (i[Pb](k >= H[ua] ? 0 : j * O(i[nb]() / j)), h = i[ob]());
            k >= H[ua] && (h -= h % j, i[Qb](h));
            if (k === H[Pa]) i[Db](i[Ra]() - i[Eb]() + o(d, 1));
            b = 1;
            h = i[ob]();
            for (var d = i.getTime(), l = i[nb](), m = i[Ra](), p = g ? 0 : (864E5 + i.getTimezoneOffset() * 6E4) % 864E5; d < c;) e.push(d), k === H[ua] ? d = pb(h + b * j, 0) : k === H[Qa] ? d = pb(h, l + b * j) : !g && (k === H[da] || k === H[Pa]) ? d = pb(h, l, m + b * j * (k === H[da] ? 1 : 7)) : d += k * j, b++;
            e.push(d);
            q(Fb(e, function(a) { return k <= H[Da] && a % H[da] === p; }), function(a) { f[a] = da; });
        }
        e.info = x(a, { higherRanks: f, totalRange: k * j });
        return e;
    }

    function Rb() { this.symbol = this.color = 0; }

    function Sb(a, b) {
        var c =
                a.length, d, e;
        for (e = 0; e < c; e++) a[e].ss_i = e;
        a.sort(function(a, c) {
            d = b(a, c);
            return d === 0 ? a.ss_i - c.ss_i : d;
        });
        for (e = 0; e < c; e++) delete a[e].ss_i;
    }

    function Sa(a) {
        for (var b = a.length, c = a[0]; b--;) a[b] < c && (c = a[b]);
        return c;
    }

    function va(a) {
        for (var b = a.length, c = a[0]; b--;) a[b] > c && (c = a[b]);
        return c;
    }

    function Ea(a, b) { for (var c in a) a[c] && a[c] !== b && a[c].destroy && a[c].destroy(), delete a[c]; }

    function $a(a) {
        qb || (qb = Y(Ta));
        a && qb.appendChild(a);
        qb.innerHTML = "";
    }

    function ra(a, b) {
        var c = "Highcharts error #" + a + ": www.highcharts.com/errors/" +
            a;
        if (b) throw c;
        else W.console && console.log(c);
    }

    function na(a) { return parseFloat(a.toPrecision(14)); }

    function ab(a, b) { Ua = o(a, b.animation); }

    function Tb() {
        var a = K.global.useUTC, b = a ? "getUTC" : "get", c = a ? "setUTC" : "set";
        pb = a ? Date.UTC : function(a, b, c, g, h, i) { return (new Date(a, b, o(c, 1), o(g, 0), o(h, 0), o(i, 0))).getTime(); };
        Bb = b + "Minutes";
        Cb = b + "Hours";
        Eb = b + "Day";
        Ra = b + "Date";
        nb = b + "Month";
        ob = b + "FullYear";
        Nb = c + "Minutes";
        Ob = c + "Hours";
        Db = c + "Date";
        Pb = c + "Month";
        Qb = c + "FullYear";
    }

    function Fa() {
    }

    function bb(a, b, c, d) {
        this.axis =
            a;
        this.pos = b;
        this.type = c || "";
        this.isNew = !0;
        !c && !d && this.addLabel();
    }

    function Gb(a, b) {
        this.axis = a;
        if (b) this.options = b, this.id = b.id;
    }

    function Ub(a, b, c, d, e, f) {
        var g = a.chart.inverted;
        this.axis = a;
        this.isNegative = c;
        this.options = b;
        this.x = d;
        this.total = null;
        this.points = {};
        this.stack = e;
        this.percent = f === "percent";
        this.alignOptions = { align: b.align || (g ? c ? "left" : "right" : "center"), verticalAlign: b.verticalAlign || (g ? "middle" : c ? "bottom" : "top"), y: o(b.y, g ? 4 : c ? 14 : -6), x: o(b.x, g ? c ? -6 : 6 : 0) };
        this.textAlign = b.textAlign || (g ? c ?
            "right" : "left" : "center");
    }

    function oa() { this.init.apply(this, arguments); }

    function Hb() { this.init.apply(this, arguments); }

    function rb(a, b) { this.init(a, b); }

    function sb(a, b) { this.init(a, b); }

    function Va() { this.init.apply(this, arguments); }

    function Ib(a) {
        var b = a.options, c = b.navigator, d = c.enabled, b = b.scrollbar, e = b.enabled, f = d ? c.height : 0, g = e ? b.height : 0;
        this.handles = [];
        this.scrollbarButtons = [];
        this.elementsToDestroy = [];
        this.chart = a;
        this.setBaseSeries();
        this.height = f;
        this.scrollbarHeight = g;
        this.scrollbarEnabled = e;
        this.navigatorEnabled = d;
        this.navigatorOptions = c;
        this.scrollbarOptions = b;
        this.outlineHeight = f + g;
        this.init();
    }

    function Jb(a) { this.init(a); }

    var s, J = document, W = window, S = Math, t = S.round, O = S.floor, Ga = S.ceil, v = S.max, z = S.min, M = S.abs, ca = S.cos, ha = S.sin, Ha = S.PI, gb = Ha * 2 / 360, wa = navigator.userAgent, Vb = W.opera, Ba = /msie/i.test(wa) && !Vb, tb = J.documentMode === 8, ub = /AppleWebKit/.test(wa), vb = /Firefox/.test(wa), hb = /(Mobile|Android|Windows Phone)/.test(wa), Ia = "http://www.w3.org/2000/svg", aa = !!J.createElementNS && !!J.createElementNS(Ia,
        "svg").createSVGRect, bc = vb && parseInt(wa.split("Firefox/")[1], 10) < 4, ia = !aa && !Ba && !!J.createElement("canvas").getContext, cb, ib = J.documentElement.ontouchstart !== s, Wb = {}, Kb = 0, qb, K, qa, Ua, Lb, H, pa = function() {
        }, Wa = [], Ta = "div", Z = "none", Xb = "rgba(192,192,192," + (aa ? 1.0E-4 : 0.002) + ")", mb = "millisecond", eb = "second", Za = "minute", Da = "hour", da = "day", Pa = "week", Qa = "month", ua = "year", Yb = "stroke-width", pb, Bb, Cb, Eb, Ra, nb, ob, Nb, Ob, Db, Pb, Qb, N = {};
    W.Highcharts = W.Highcharts ? ra(16, !0) : {};
    qa = function(a, b, c) {
        if (!r(b) || isNaN(b)) return "Invalid date";
        var a = o(a, "%Y-%m-%d %H:%M:%S"), d = new Date(b), e, f = d[Cb](), g = d[Eb](), h = d[Ra](), i = d[nb](), k = d[ob](), j = K.lang, l = j.weekdays, d = x({ a: l[g].substr(0, 3), A: l[g], d: Na(h), e: h, b: j.shortMonths[i], B: j.months[i], m: Na(i + 1), y: k.toString().substr(2, 2), Y: k, H: Na(f), I: Na(f % 12 || 12), l: f % 12 || 12, M: Na(d[Bb]()), p: f < 12 ? "AM" : "PM", P: f < 12 ? "am" : "pm", S: Na(d.getSeconds()), L: Na(t(b % 1E3), 3) }, Highcharts.dateFormats);
        for (e in d) for (; a.indexOf("%" + e) !== -1;) a = a.replace("%" + e, typeof d[e] === "function" ? d[e](b) : d[e]);
        return c ? a.substr(0, 1).toUpperCase() +
            a.substr(1) : a;
    };
    Rb.prototype = { wrapColor: function(a) { if (this.color >= a) this.color = 0; }, wrapSymbol: function(a) { if (this.symbol >= a) this.symbol = 0; } };
    H = lb(mb, 1, eb, 1E3, Za, 6E4, Da, 36E5, da, 864E5, Pa, 6048E5, Qa, 26784E5, ua, 31556952E3);
    Lb = {
        init: function(a, b, c) {
            var b = b || "", d = a.shift, e = b.indexOf("C") > -1, f = e ? 7 : 3, g, b = b.split(" "), c = [].concat(c), h, i, k = function(a) { for (g = a.length; g--;) a[g] === "M" && a.splice(g + 1, 0, a[g + 1], a[g + 2], a[g + 1], a[g + 2]); };
            e && (k(b), k(c));
            a.isArea && (h = b.splice(b.length - 6, 6), i = c.splice(c.length - 6, 6));
            if (d <=
                c.length / f && b.length === c.length) for (; d--;) c = [].concat(c).splice(0, f).concat(c);
            a.shift = 0;
            if (b.length) for (a = c.length; b.length < a;) d = [].concat(b).splice(b.length - f, f), e && (d[f - 6] = d[f - 2], d[f - 5] = d[f - 1]), b = b.concat(d);
            h && (b = b.concat(h), c = c.concat(i));
            return [b, c];
        },
        step: function(a, b, c, d) {
            var e = [], f = a.length;
            if (c === 1) e = d;
            else if (f === b.length && c < 1) for (; f--;) d = parseFloat(a[f]), e[f] = isNaN(d) ? a[f] : c * parseFloat(b[f] - d) + d;
            else e = b;
            return e;
        }
    };
    (function(a) {
        W.HighchartsAdapter = W.HighchartsAdapter || a && {
            init: function(b) {
                var c =
                        a.fx, d = c.step, e, f = a.Tween, g = f && f.propHooks;
                e = a.cssHooks.opacity;
                a.extend(a.easing, { easeOutQuad: function(a, b, c, d, e) { return -d * (b /= e) * (b - 2) + c; } });
                a.each(["cur", "_default", "width", "height", "opacity"], function(a, b) {
                    var e = d, j, l;
                    b === "cur" ? e = c.prototype : b === "_default" && f && (e = g[b], b = "set");
                    (j = e[b]) && (e[b] = function(c) {
                        c = a ? c : this;
                        if (c.prop !== "align") return l = c.elem, l.attr ? l.attr(c.prop, b === "cur" ? s : c.now) : j.apply(this, arguments);
                    });
                });
                ga(e, "get", function(a, b, c) { return b.attr ? b.opacity || 0 : a.call(this, b, c); });
                e = function(a) {
                    var c =
                            a.elem, d;
                    if (!a.started) d = b.init(c, c.d, c.toD), a.start = d[0], a.end = d[1], a.started = !0;
                    c.attr("d", b.step(a.start, a.end, a.pos, c.toD));
                };
                f ? g.d = { set: e } : d.d = e;
                this.each = Array.prototype.forEach ? function(a, b) { return Array.prototype.forEach.call(a, b); } : function(a, b) { for (var c = 0, d = a.length; c < d; c++) if (b.call(a[c], a[c], c, a) === !1) return c; };
                a.fn.highcharts = function() {
                    var a = "Chart", b = arguments, c, d;
                    ka(b[0]) && (a = b[0], b = Array.prototype.slice.call(b, 1));
                    c = b[0];
                    if (c !== s)
                        c.chart = c.chart || {}, c.chart.renderTo = this[0], new Highcharts[a](c,
                            b[1]), d = this;
                    c === s && (d = Wa[C(this[0], "data-highcharts-chart")]);
                    return d;
                };
            },
            getScript: a.getScript,
            inArray: a.inArray,
            adapterRun: function(b, c) { return a(b)[c](); },
            grep: a.grep,
            map: function(a, c) {
                for (var d = [], e = 0, f = a.length; e < f; e++) d[e] = c.call(a[e], a[e], e, a);
                return d;
            },
            offset: function(b) { return a(b).offset(); },
            addEvent: function(b, c, d) { a(b).bind(c, d); },
            removeEvent: function(b, c, d) {
                var e = J.removeEventListener ? "removeEventListener" : "detachEvent";
                J[e] && b && !b[e] && (b[e] = function() {
                });
                a(b).unbind(c, d);
            },
            fireEvent: function(b,
                c, d, e) {
                var f = a.Event(c), g = "detached" + c, h;
                !Ba && d && (delete d.layerX, delete d.layerY);
                x(f, d);
                b[c] && (b[g] = b[c], b[c] = null);
                a.each(["preventDefault", "stopPropagation"], function(a, b) {
                    var c = f[b];
                    f[b] = function() {
                        try {
                            c.call(f);
                        } catch(a) {
                            b === "preventDefault" && (h = !0);
                        }
                    };
                });
                a(b).trigger(f);
                b[g] && (b[c] = b[g], b[g] = null);
                e && !f.isDefaultPrevented() && !h && e(f);
            },
            washMouseEvent: function(a) {
                var c = a.originalEvent || a;
                if (c.pageX === s) c.pageX = a.pageX, c.pageY = a.pageY;
                return c;
            },
            animate: function(b, c, d) {
                var e = a(b);
                if (!b.style)
                    b.style =
                        {};
                if (c.d) b.toD = c.d, c.d = 1;
                e.stop();
                c.opacity !== s && b.attr && (c.opacity += "px");
                e.animate(c, d);
            },
            stop: function(b) { a(b).stop(); }
        };
    })(W.jQuery);
    var P = W.HighchartsAdapter, D = P || {};
    P && P.init.call(P, Lb);
    var wb = D.adapterRun, cc = D.getScript, xa = D.inArray, q = D.each, Fb = D.grep, dc = D.offset, Ja = D.map, E = D.addEvent, U = D.removeEvent, L = D.fireEvent, ec = D.washMouseEvent, Mb = D.animate, jb = D.stop, D = { enabled: !0, x: 0, y: 15, style: { color: "#666", cursor: "default", fontSize: "11px", lineHeight: "14px" } };
    K = {
        colors: "#2f7ed8,#0d233a,#8bbc21,#910000,#1aadce,#492970,#f28f43,#77a1e5,#c42525,#a6c96a".split(","),
        symbols: ["circle", "diamond", "square", "triangle", "triangle-down"],
        lang: { loading: "Loading...", months: "January,February,March,April,May,June,July,August,September,October,November,December".split(","), shortMonths: "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(","), weekdays: "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday".split(","), decimalPoint: ".", numericSymbols: "k,M,G,T,P,E".split(","), resetZoom: "Reset zoom", resetZoomTitle: "Reset zoom level 1:1", thousandsSep: "," },
        global: {
            useUTC: !0,
            canvasToolsURL: "http://code.highcharts.com/stock/1.3.7/modules/canvas-tools.js",
            VMLRadialGradientURL: "http://code.highcharts.com/stock/1.3.7/gfx/vml-radial-gradient.png"
        },
        chart: {
            borderColor: "#4572A7",
            borderRadius: 5,
            defaultSeriesType: "line",
            ignoreHiddenSeries: !0,
            spacing: [10, 10, 15, 10],
            style: { fontFamily: '"Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif', fontSize: "12px" },
            backgroundColor: "#FFFFFF",
            plotBorderColor: "#C0C0C0",
            resetZoomButton: {
                theme: { zIndex: 20 },
                position: {
                    align: "right",
                    x: -10,
                    y: 10
                }
            }
        },
        title: { text: "Chart title", align: "center", margin: 15, style: { color: "#274b6d", fontSize: "16px" } },
        subtitle: { text: "", align: "center", style: { color: "#4d759e" } },
        plotOptions: {
            line: {
                allowPointSelect: !1,
                showCheckbox: !1,
                animation: { duration: 1E3 },
                events: {},
                lineWidth: 2,
                marker: { enabled: !0, lineWidth: 0, radius: 4, lineColor: "#FFFFFF", states: { hover: { enabled: !0 }, select: { fillColor: "#FFFFFF", lineColor: "#000000", lineWidth: 2 } } },
                point: { events: {} },
                dataLabels: u(D, {
                    align: "center",
                    enabled: !1,
                    formatter: function() {
                        return this.y ===
                            null ? "" : Ca(this.y, -1);
                    },
                    verticalAlign: "bottom",
                    y: 0
                }),
                cropThreshold: 300,
                pointRange: 0,
                states: { hover: { marker: {} }, select: { marker: {} } },
                stickyTracking: !0
            }
        },
        labels: { style: { position: "absolute", color: "#3E576F" } },
        legend: {
            enabled: !0,
            align: "center",
            layout: "horizontal",
            labelFormatter: function() { return this.name; },
            borderWidth: 1,
            borderColor: "#909090",
            borderRadius: 5,
            navigation: { activeColor: "#274b6d", inactiveColor: "#CCC" },
            shadow: !1,
            itemStyle: { cursor: "pointer", color: "#274b6d", fontSize: "12px" },
            itemHoverStyle: { color: "#000" },
            itemHiddenStyle: { color: "#CCC" },
            itemCheckboxStyle: { position: "absolute", width: "13px", height: "13px" },
            symbolWidth: 16,
            symbolPadding: 5,
            verticalAlign: "bottom",
            x: 0,
            y: 0,
            title: { style: { fontWeight: "bold" } }
        },
        loading: { labelStyle: { fontWeight: "bold", position: "relative", top: "1em" }, style: { position: "absolute", backgroundColor: "white", opacity: 0.5, textAlign: "center" } },
        tooltip: {
            enabled: !0,
            animation: aa,
            backgroundColor: "rgba(255, 255, 255, .85)",
            borderWidth: 1,
            borderRadius: 3,
            dateTimeLabelFormats: {
                millisecond: "%A, %b %e, %H:%M:%S.%L",
                second: "%A, %b %e, %H:%M:%S",
                minute: "%A, %b %e, %H:%M",
                hour: "%A, %b %e, %H:%M",
                day: "%A, %b %e, %Y",
                week: "Week from %A, %b %e, %Y",
                month: "%B %Y",
                year: "%Y"
            },
            headerFormat: '<span style="font-size: 10px">{point.key}</span><br/>',
            pointFormat: '<span style="color:{series.color}">{series.name}</span>: <b>{point.y}</b><br/>',
            shadow: !0,
            snap: hb ? 25 : 10,
            style: { color: "#333333", cursor: "default", fontSize: "12px", padding: "8px", whiteSpace: "nowrap" }
        },
        credits: {
            enabled: !0,
            text: "Highcharts.com",
            href: "http://www.highcharts.com",
            position: { align: "right", x: -10, verticalAlign: "bottom", y: -5 },
            style: { cursor: "pointer", color: "#909090", fontSize: "9px" }
        }
    };
    var Q = K.plotOptions, P = Q.line;
    Tb();
    var fc = /rgba\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]?(?:\.[0-9]+)?)\s*\)/, gc = /#([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})/, hc = /rgb\(\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*,\s*([0-9]{1,3})\s*\)/, ya = function(a) {
        var b = [], c, d;
        (function(a) {
            a && a.stops ? d = Ja(a.stops, function(a) { return ya(a[1]); }) : (c = fc.exec(a)) ? b = [B(c[1]), B(c[2]),
                B(c[3]), parseFloat(c[4], 10)] : (c = gc.exec(a)) ? b = [B(c[1], 16), B(c[2], 16), B(c[3], 16), 1] : (c = hc.exec(a)) && (b = [B(c[1]), B(c[2]), B(c[3]), 1]);
        })(a);
        return {
            get: function(c) {
                var f;
                d ? (f = u(a), f.stops = [].concat(f.stops), q(d, function(a, b) { f.stops[b] = [f.stops[b][0], a.get(c)]; })) : f = b && !isNaN(b[0]) ? c === "rgb" ? "rgb(" + b[0] + "," + b[1] + "," + b[2] + ")" : c === "a" ? b[3] : "rgba(" + b.join(",") + ")" : a;
                return f;
            },
            brighten: function(a) {
                if (d) q(d, function(b) { b.brighten(a); });
                else if (sa(a) && a !== 0) {
                    var c;
                    for (c = 0; c < 3; c++)
                        b[c] += B(a * 255), b[c] < 0 && (b[c] =
                            0), b[c] > 255 && (b[c] = 255);
                }
                return this;
            },
            rgba: b,
            setOpacity: function(a) {
                b[3] = a;
                return this;
            }
        };
    };
    Fa.prototype = {
        init: function(a, b) {
            this.element = b === "span" ? Y(b) : J.createElementNS(Ia, b);
            this.renderer = a;
            this.attrSetters = {};
        },
        opacity: 1,
        animate: function(a, b, c) {
            b = o(b, Ua, !0);
            jb(this);
            if (b) {
                b = u(b);
                if (c) b.complete = c;
                Mb(this, a, b);
            } else this.attr(a), c && c();
        },
        attr: function(a, b) {
            var c, d, e, f, g = this.element, h = g.nodeName.toLowerCase(), i = this.renderer, k, j = this.attrSetters, l = this.shadows, m, p, n = this;
            ka(a) && r(b) && (c = a, a = {}, a[c] =
                b);
            if (ka(a)) c = a, h === "circle" ? c = { x: "cx", y: "cy" }[c] || c : c === "strokeWidth" && (c = "stroke-width"), n = C(g, c) || this[c] || 0, c !== "d" && c !== "visibility" && c !== "fill" && (n = parseFloat(n));
            else {
                for (c in a)
                    if (k = !1, d = a[c], e = j[c] && j[c].call(this, d, c), e !== !1) {
                        e !== s && (d = e);
                        if (c === "d") d && d.join && (d = d.join(" ")), /(NaN| {2}|^$)/.test(d) && (d = "M 0 0");
                        else if (c === "x" && h === "text") for (e = 0; e < g.childNodes.length; e++) f = g.childNodes[e], C(f, "x") === C(g, "x") && C(f, "x", d);
                        else if (this.rotation && (c === "x" || c === "y")) p = !0;
                        else if (c === "fill")
                            d =
                                i.color(d, g, c);
                        else if (h === "circle" && (c === "x" || c === "y")) c = { x: "cx", y: "cy" }[c] || c;
                        else if (h === "rect" && c === "r") C(g, { rx: d, ry: d }), k = !0;
                        else if (c === "translateX" || c === "translateY" || c === "rotation" || c === "verticalAlign" || c === "scaleX" || c === "scaleY") k = p = !0;
                        else if (c === "stroke") d = i.color(d, g, c);
                        else if (c === "dashstyle")
                            if (c = "stroke-dasharray", d = d && d.toLowerCase(), d === "solid") d = Z;
                            else {
                                if (d) {
                                    d = d.replace("shortdashdotdot", "3,1,1,1,1,1,").replace("shortdashdot", "3,1,1,1").replace("shortdot", "1,1,").replace("shortdash",
                                        "3,1,").replace("longdash", "8,3,").replace(/dot/g, "1,3,").replace("dash", "4,3,").replace(/,$/, "").split(",");
                                    for (e = d.length; e--;) d[e] = B(d[e]) * o(a["stroke-width"], this["stroke-width"]);
                                    d = d.join(",");
                                }
                            }
                        else if (c === "width") d = B(d);
                        else if (c === "align") c = "text-anchor", d = { left: "start", center: "middle", right: "end" }[d];
                        else if (c === "title") e = g.getElementsByTagName("title")[0], e || (e = J.createElementNS(Ia, "title"), g.appendChild(e)), e.textContent = d;
                        c === "strokeWidth" && (c = "stroke-width");
                        if (c === "stroke-width" || c ===
                            "stroke") {
                            this[c] = d;
                            if (this.stroke && this["stroke-width"]) C(g, "stroke", this.stroke), C(g, "stroke-width", this["stroke-width"]), this.hasStroke = !0;
                            else if (c === "stroke-width" && d === 0 && this.hasStroke) g.removeAttribute("stroke"), this.hasStroke = !1;
                            k = !0;
                        }
                        this.symbolName && /^(x|y|width|height|r|start|end|innerR|anchorX|anchorY)/.test(c) && (m || (this.symbolAttr(a), m = !0), k = !0);
                        if (l && /^(width|height|visibility|x|y|d|transform|cx|cy|r)$/.test(c)) for (e = l.length; e--;) C(l[e], c, c === "height" ? v(d - (l[e].cutHeight || 0), 0) : d);
                        if ((c === "width" || c === "height") && h === "rect" && d < 0) d = 0;
                        this[c] = d;
                        c === "text" ? (d !== this.textStr && delete this.bBox, this.textStr = d, this.added && i.buildText(this)) : k || C(g, c, d);
                    }
                p && this.updateTransform();
            }
            return n;
        },
        addClass: function(a) {
            var b = this.element, c = C(b, "class") || "";
            c.indexOf(a) === -1 && C(b, "class", c + " " + a);
            return this;
        },
        symbolAttr: function(a) {
            var b = this;
            q("x,y,r,start,end,width,height,innerR,anchorX,anchorY".split(","), function(c) { b[c] = o(a[c], b[c]); });
            b.attr({
                d: b.renderer.symbols[b.symbolName](b.x, b.y, b.width,
                    b.height, b)
            });
        },
        clip: function(a) { return this.attr("clip-path", a ? "url(" + this.renderer.url + "#" + a.id + ")" : Z); },
        crisp: function(a, b, c, d, e) {
            var f, g = {}, h = {}, i, a = a || this.strokeWidth || this.attr && this.attr("stroke-width") || 0;
            i = t(a) % 2 / 2;
            h.x = O(b || this.x || 0) + i;
            h.y = O(c || this.y || 0) + i;
            h.width = O((d || this.width || 0) - 2 * i);
            h.height = O((e || this.height || 0) - 2 * i);
            h.strokeWidth = a;
            for (f in h) this[f] !== h[f] && (this[f] = g[f] = h[f]);
            return g;
        },
        css: function(a) {
            var b = this.element, c = this.textWidth = a && a.width && b.nodeName.toLowerCase() ===
                "text" && B(a.width), d, e = "", f = function(a, b) { return "-" + b.toLowerCase(); };
            if (a && a.color) a.fill = a.color;
            this.styles = a = x(this.styles, a);
            c && delete a.width;
            if (Ba && !aa) G(this.element, a);
            else {
                for (d in a) e += d.replace(/([A-Z])/g, f) + ":" + a[d] + ";";
                C(b, "style", e);
            }
            c && this.added && this.renderer.buildText(this);
            return this;
        },
        on: function(a, b) {
            var c = this, d = c.element;
            ib && a === "click" ? (d.ontouchstart = function(a) {
                c.touchEventFired = Date.now();
                a.preventDefault();
                b.call(d, a);
            }, d.onclick = function(a) {
                (wa.indexOf("Android") === -1 ||
                    Date.now() - (c.touchEventFired || 0) > 1100) && b.call(d, a);
            }) : d["on" + a] = b;
            return this;
        },
        setRadialReference: function(a) {
            this.element.radialReference = a;
            return this;
        },
        translate: function(a, b) { return this.attr({ translateX: a, translateY: b }); },
        invert: function() {
            this.inverted = !0;
            this.updateTransform();
            return this;
        },
        htmlCss: function(a) {
            var b = this.element;
            if (b = a && b.tagName === "SPAN" && a.width) delete a.width, this.textWidth = b, this.updateTransform();
            this.styles = x(this.styles, a);
            G(this.element, a);
            return this;
        },
        htmlGetBBox: function() {
            var a =
                    this.element, b = this.bBox;
            if (!b) {
                if (a.nodeName === "text") a.style.position = "absolute";
                b = this.bBox = { x: a.offsetLeft, y: a.offsetTop, width: a.offsetWidth, height: a.offsetHeight };
            }
            return b;
        },
        htmlUpdateTransform: function() {
            if (this.added) {
                var a = this.renderer, b = this.element, c = this.translateX || 0, d = this.translateY || 0, e = this.x || 0, f = this.y || 0, g = this.textAlign || "left", h = { left: 0, center: 0.5, right: 1 }[g], i = g && g !== "left", k = this.shadows;
                G(b, { marginLeft: c, marginTop: d });
                k && q(k, function(a) { G(a, { marginLeft: c + 1, marginTop: d + 1 }); });
                this.inverted && q(b.childNodes, function(c) { a.invertChild(c, b); });
                if (b.tagName === "SPAN") {
                    var j, l, k = this.rotation, m;
                    j = 0;
                    var p = 1, n = 0, ea;
                    m = B(this.textWidth);
                    var w = this.xCorr || 0, y = this.yCorr || 0, T = [k, g, b.innerHTML, this.textWidth].join(",");
                    if (T !== this.cTT) {
                        r(k) && (j = k * gb, p = ca(j), n = ha(j), this.setSpanRotation(k, n, p));
                        j = o(this.elemWidth, b.offsetWidth);
                        l = o(this.elemHeight, b.offsetHeight);
                        if (j > m && /[ \-]/.test(b.textContent || b.innerText)) G(b, { width: m + "px", display: "block", whiteSpace: "normal" }), j = m;
                        m = a.fontMetrics(b.style.fontSize).b;
                        w = p < 0 && -j;
                        y = n < 0 && -l;
                        ea = p * n < 0;
                        w += n * m * (ea ? 1 - h : h);
                        y -= p * m * (k ? ea ? h : 1 - h : 1);
                        i && (w -= j * h * (p < 0 ? -1 : 1), k && (y -= l * h * (n < 0 ? -1 : 1)), G(b, { textAlign: g }));
                        this.xCorr = w;
                        this.yCorr = y;
                    }
                    G(b, { left: e + w + "px", top: f + y + "px" });
                    if (ub) l = b.offsetHeight;
                    this.cTT = T;
                }
            } else this.alignOnAdd = !0;
        },
        setSpanRotation: function(a) {
            var b = {};
            b[Ba ? "-ms-transform" : ub ? "-webkit-transform" : vb ? "MozTransform" : Vb ? "-o-transform" : ""] = b.transform = "rotate(" + a + "deg)";
            G(this.element, b);
        },
        updateTransform: function() {
            var a = this.translateX || 0, b = this.translateY || 0, c = this.scaleX,
                d = this.scaleY, e = this.inverted, f = this.rotation;
            e && (a += this.attr("width"), b += this.attr("height"));
            a = ["translate(" + a + "," + b + ")"];
            e ? a.push("rotate(90) scale(-1,1)") : f && a.push("rotate(" + f + " " + (this.x || 0) + " " + (this.y || 0) + ")");
            (r(c) || r(d)) && a.push("scale(" + o(c, 1) + " " + o(d, 1) + ")");
            a.length && C(this.element, "transform", a.join(" "));
        },
        toFront: function() {
            var a = this.element;
            a.parentNode.appendChild(a);
            return this;
        },
        align: function(a, b, c) {
            var d, e, f, g, h = {};
            e = this.renderer;
            f = e.alignedObjects;
            if (a) {
                if (this.alignOptions =
                    a, this.alignByTranslate = b, !c || ka(c)) this.alignTo = d = c || "renderer", ma(f, this), f.push(this), c = null;
            } else a = this.alignOptions, b = this.alignByTranslate, d = this.alignTo;
            c = o(c, e[d], e);
            d = a.align;
            e = a.verticalAlign;
            f = (c.x || 0) + (a.x || 0);
            g = (c.y || 0) + (a.y || 0);
            if (d === "right" || d === "center") f += (c.width - (a.width || 0)) / { right: 1, center: 2 }[d];
            h[b ? "translateX" : "x"] = t(f);
            if (e === "bottom" || e === "middle") g += (c.height - (a.height || 0)) / ({ bottom: 1, middle: 2 }[e] || 1);
            h[b ? "translateY" : "y"] = t(g);
            this[this.placed ? "animate" : "attr"](h);
            this.placed =
                !0;
            this.alignAttr = h;
            return this;
        },
        getBBox: function() {
            var a = this.bBox, b = this.renderer, c, d = this.rotation;
            c = this.element;
            var e = this.styles, f = d * gb;
            if (!a) {
                if (c.namespaceURI === Ia || b.forExport) {
                    try {
                        a = c.getBBox ? x({}, c.getBBox()) : { width: c.offsetWidth, height: c.offsetHeight };
                    } catch(g) {
                    }
                    if (!a || a.width < 0) a = { width: 0, height: 0 };
                } else a = this.htmlGetBBox();
                if (b.isSVG) {
                    b = a.width;
                    c = a.height;
                    if (Ba && e && e.fontSize === "11px" && c.toPrecision(3) === "22.7") a.height = c = 14;
                    if (d)
                        a.width = M(c * ha(f)) + M(b * ca(f)), a.height = M(c * ca(f)) + M(b *
                            ha(f));
                }
                this.bBox = a;
            }
            return a;
        },
        show: function() { return this.attr({ visibility: "visible" }); },
        hide: function() { return this.attr({ visibility: "hidden" }); },
        fadeOut: function(a) {
            var b = this;
            b.animate({ opacity: 0 }, { duration: a || 150, complete: function() { b.hide(); } });
        },
        add: function(a) {
            var b = this.renderer, c = a || b, d = c.element || b.box, e = d.childNodes, f = this.element, g = C(f, "zIndex"), h;
            if (a) this.parentGroup = a;
            this.parentInverted = a && a.inverted;
            this.textStr !== void 0 && b.buildText(this);
            if (g) c.handleZ = !0, g = B(g);
            if (c.handleZ)
                for (c = 0; c <
                    e.length; c++)
                    if (a = e[c], b = C(a, "zIndex"), a !== f && (B(b) > g || !r(g) && r(b))) {
                        d.insertBefore(f, a);
                        h = !0;
                        break;
                    }
            h || d.appendChild(f);
            this.added = !0;
            L(this, "add");
            return this;
        },
        safeRemoveChild: function(a) {
            var b = a.parentNode;
            b && b.removeChild(a);
        },
        destroy: function() {
            var a = this, b = a.element || {}, c = a.shadows, d = a.renderer.isSVG && b.nodeName === "SPAN" && a.parentGroup, e, f;
            b.onclick = b.onmouseout = b.onmouseover = b.onmousemove = b.point = null;
            jb(a);
            if (a.clipPath) a.clipPath = a.clipPath.destroy();
            if (a.stops) {
                for (f = 0; f < a.stops.length; f++)
                    a.stops[f] =
                        a.stops[f].destroy();
                a.stops = null;
            }
            a.safeRemoveChild(b);
            for (c && q(c, function(b) { a.safeRemoveChild(b); }); d && d.div.childNodes.length === 0;) b = d.parentGroup, a.safeRemoveChild(d.div), delete d.div, d = b;
            a.alignTo && ma(a.renderer.alignedObjects, a);
            for (e in a) delete a[e];
            return null;
        },
        shadow: function(a, b, c) {
            var d = [], e, f, g = this.element, h, i, k, j;
            if (a) {
                i = o(a.width, 3);
                k = (a.opacity || 0.15) / i;
                j = this.parentInverted ? "(-1,-1)" : "(" + o(a.offsetX, 1) + ", " + o(a.offsetY, 1) + ")";
                for (e = 1; e <= i; e++) {
                    f = g.cloneNode(0);
                    h = i * 2 + 1 - 2 * e;
                    C(f, {
                        isShadow: "true",
                        stroke: a.color || "black",
                        "stroke-opacity": k * e,
                        "stroke-width": h,
                        transform: "translate" + j,
                        fill: Z
                    });
                    if (c) C(f, "height", v(C(f, "height") - h, 0)), f.cutHeight = h;
                    b ? b.element.appendChild(f) : g.parentNode.insertBefore(f, g);
                    d.push(f);
                }
                this.shadows = d;
            }
            return this;
        }
    };
    var za = function() { this.init.apply(this, arguments); };
    za.prototype = {
        Element: Fa,
        init: function(a, b, c, d) {
            var e = location, f, g;
            f = this.createElement("svg").attr({ version: "1.1" });
            g = f.element;
            a.appendChild(g);
            a.innerHTML.indexOf("xmlns") === -1 && C(g, "xmlns", Ia);
            this.isSVG =
                !0;
            this.box = g;
            this.boxWrapper = f;
            this.alignedObjects = [];
            this.url = (vb || ub) && J.getElementsByTagName("base").length ? e.href.replace(/#.*?$/, "").replace(/([\('\)])/g, "\\$1").replace(/ /g, "%20") : "";
            this.createElement("desc").add().element.appendChild(J.createTextNode("Created with Highstock 1.3.7"));
            this.defs = this.createElement("defs").add();
            this.forExport = d;
            this.gradients = {};
            this.setSize(b, c, !1);
            var h;
            if (vb && a.getBoundingClientRect)
                this.subPixelFix = b = function() {
                    G(a, { left: 0, top: 0 });
                    h = a.getBoundingClientRect();
                    G(a, { left: Ga(h.left) - h.left + "px", top: Ga(h.top) - h.top + "px" });
                }, b(), E(W, "resize", b);
        },
        isHidden: function() { return !this.boxWrapper.getBBox().width; },
        destroy: function() {
            var a = this.defs;
            this.box = null;
            this.boxWrapper = this.boxWrapper.destroy();
            Ea(this.gradients || {});
            this.gradients = null;
            if (a) this.defs = a.destroy();
            this.subPixelFix && U(W, "resize", this.subPixelFix);
            return this.alignedObjects = null;
        },
        createElement: function(a) {
            var b = new this.Element;
            b.init(this, a);
            return b;
        },
        draw: function() {
        },
        buildText: function(a) {
            for (var b =
                a.element, c = this, d = c.forExport, e = o(a.textStr, "").toString().replace(/<(b|strong)>/g, '<span style="font-weight:bold">').replace(/<(i|em)>/g, '<span style="font-style:italic">').replace(/<a/g, "<span").replace(/<\/(b|strong|i|em|a)>/g, "</span>").split(/<br.*?>/g), f = b.childNodes, g = /style="([^"]+)"/, h = /href="(http[^"]+)"/, i = C(b, "x"), k = a.styles, j = a.textWidth, l = k && k.lineHeight, m = f.length; m--;) b.removeChild(f[m]);
            j && !a.added && this.box.appendChild(b);
            e[e.length - 1] === "" && e.pop();
            q(e, function(e, f) {
                var m, w = 0,
                    e = e.replace(/<span/g, "|||<span").replace(/<\/span>/g, "</span>|||");
                m = e.split("|||");
                q(m, function(e) {
                    if (e !== "" || m.length === 1) {
                        var p = {}, o = J.createElementNS(Ia, "tspan"), q;
                        g.test(e) && (q = e.match(g)[1].replace(/(;| |^)color([ :])/, "$1fill$2"), C(o, "style", q));
                        h.test(e) && !d && (C(o, "onclick", 'location.href="' + e.match(h)[1] + '"'), G(o, { cursor: "pointer" }));
                        e = (e.replace(/<(.|\n)*?>/g, "") || " ").replace(/&lt;/g, "<").replace(/&gt;/g, ">");
                        if (e !== " " && (o.appendChild(J.createTextNode(e)), w ? p.dx = 0 : p.x = i, C(o, p), !w && f &&
                            (!aa && d && G(o, { display: "block" }), C(o, "dy", l || c.fontMetrics(/px$/.test(o.style.fontSize) ? o.style.fontSize : k.fontSize).h, ub && o.offsetHeight)), b.appendChild(o), w++, j))
                            for (var e = e.replace(/([^\^])-/g, "$1- ").split(" "), s, A, p = a._clipHeight, F = [], v = B(l || 16), t = 1; e.length || F.length;)
                                delete a.bBox, s = a.getBBox(), A = s.width, !aa && c.forExport && (A = c.measureSpanWidth(o.firstChild.data, a.styles)), s = A > j, !s || e.length === 1 ? (e = F, F = [], e.length && (t++, p && t * v > p ? (e = ["..."], a.attr("title", a.textStr)) : (o = J.createElementNS(Ia, "tspan"),
                                C(o, { dy: v, x: i }), q && C(o, "style", q), b.appendChild(o), A > j && (j = A)))) : (o.removeChild(o.firstChild), F.unshift(e.pop())), e.length && o.appendChild(J.createTextNode(e.join(" ").replace(/- /g, "-")));
                    }
                });
            });
        },
        button: function(a, b, c, d, e, f, g, h, i) {
            var k = this.label(a, b, c, i, null, null, null, null, "button"), j = 0, l, m, p, n, o, w, a = { x1: 0, y1: 0, x2: 0, y2: 1 }, e = u({ "stroke-width": 1, stroke: "#CCCCCC", fill: { linearGradient: a, stops: [[0, "#FEFEFE"], [1, "#F6F6F6"]] }, r: 2, padding: 5, style: { color: "black" } }, e);
            p = e.style;
            delete e.style;
            f = u(e, {
                stroke: "#68A",
                fill: { linearGradient: a, stops: [[0, "#FFF"], [1, "#ACF"]] }
            }, f);
            n = f.style;
            delete f.style;
            g = u(e, { stroke: "#68A", fill: { linearGradient: a, stops: [[0, "#9BD"], [1, "#CDF"]] } }, g);
            o = g.style;
            delete g.style;
            h = u(e, { style: { color: "#CCC" } }, h);
            w = h.style;
            delete h.style;
            E(k.element, Ba ? "mouseover" : "mouseenter", function() { j !== 3 && k.attr(f).css(n); });
            E(k.element, Ba ? "mouseout" : "mouseleave", function() { j !== 3 && (l = [e, f, g][j], m = [p, n, o][j], k.attr(l).css(m)); });
            k.setState = function(a) {
                (k.state = j = a) ? a === 2 ? k.attr(g).css(o) : a === 3 && k.attr(h).css(w) :
                    k.attr(e).css(p);
            };
            return k.on("click", function() { j !== 3 && d.call(k); }).attr(e).css(x({ cursor: "default" }, p));
        },
        crispLine: function(a, b) {
            a[1] === a[4] && (a[1] = a[4] = t(a[1]) - b % 2 / 2);
            a[2] === a[5] && (a[2] = a[5] = t(a[2]) + b % 2 / 2);
            return a;
        },
        path: function(a) {
            var b = { fill: Z };
            Ya(a) ? b.d = a : $(a) && x(b, a);
            return this.createElement("path").attr(b);
        },
        circle: function(a, b, c) {
            a = $(a) ? a : { x: a, y: b, r: c };
            return this.createElement("circle").attr(a);
        },
        arc: function(a, b, c, d, e, f) {
            if ($(a)) b = a.y, c = a.r, d = a.innerR, e = a.start, f = a.end, a = a.x;
            a = this.symbol("arc",
                a || 0, b || 0, c || 0, c || 0, { innerR: d || 0, start: e || 0, end: f || 0 });
            a.r = c;
            return a;
        },
        rect: function(a, b, c, d, e, f) {
            e = $(a) ? a.r : e;
            e = this.createElement("rect").attr({ rx: e, ry: e, fill: Z });
            return e.attr($(a) ? a : e.crisp(f, a, b, v(c, 0), v(d, 0)));
        },
        setSize: function(a, b, c) {
            var d = this.alignedObjects, e = d.length;
            this.width = a;
            this.height = b;
            for (this.boxWrapper[o(c, !0) ? "animate" : "attr"]({ width: a, height: b }); e--;) d[e].align();
        },
        g: function(a) {
            var b = this.createElement("g");
            return r(a) ? b.attr({ "class": "highcharts-" + a }) : b;
        },
        image: function(a, b,
            c, d, e) {
            var f = { preserveAspectRatio: Z };
            arguments.length > 1 && x(f, { x: b, y: c, width: d, height: e });
            f = this.createElement("image").attr(f);
            f.element.setAttributeNS ? f.element.setAttributeNS("http://www.w3.org/1999/xlink", "href", a) : f.element.setAttribute("hc-svg-href", a);
            return f;
        },
        symbol: function(a, b, c, d, e, f) {
            var g, h = this.symbols[a], h = h && h(t(b), t(c), d, e, f), i = /^url\((.*?)\)$/, k, j;
            if (h) g = this.path(h), x(g, { symbolName: a, x: b, y: c, width: d, height: e }), f && x(g, f);
            else if (i.test(a))
                j = function(a, b) {
                    a.element && (a.attr({
                        width: b[0],
                        height: b[1]
                    }), a.alignByTranslate || a.translate(t((d - b[0]) / 2), t((e - b[1]) / 2)));
                }, k = a.match(i)[1], a = Wb[k], g = this.image(k).attr({ x: b, y: c }), g.isImg = !0, a ? j(g, a) : (g.attr({ width: 0, height: 0 }), Y("img", { onload: function() { j(g, Wb[k] = [this.width, this.height]); }, src: k }));
            return g;
        },
        symbols: {
            circle: function(a, b, c, d) {
                var e = 0.166 * c;
                return ["M", a + c / 2, b, "C", a + c + e, b, a + c + e, b + d, a + c / 2, b + d, "C", a - e, b + d, a - e, b, a + c / 2, b, "Z"];
            },
            square: function(a, b, c, d) { return ["M", a, b, "L", a + c, b, a + c, b + d, a, b + d, "Z"]; },
            triangle: function(a, b, c, d) {
                return ["M",
                    a + c / 2, b, "L", a + c, b + d, a, b + d, "Z"];
            },
            "triangle-down": function(a, b, c, d) { return ["M", a, b, "L", a + c, b, a + c / 2, b + d, "Z"]; },
            diamond: function(a, b, c, d) { return ["M", a + c / 2, b, "L", a + c, b + d / 2, a + c / 2, b + d, a, b + d / 2, "Z"]; },
            arc: function(a, b, c, d, e) {
                var f = e.start, c = e.r || c || d, g = e.end - 0.001, d = e.innerR, h = e.open, i = ca(f), k = ha(f), j = ca(g), g = ha(g), e = e.end - f < Ha ? 0 : 1;
                return ["M", a + c * i, b + c * k, "A", c, c, 0, e, 1, a + c * j, b + c * g, h ? "M" : "L", a + d * j, b + d * g, "A", d, d, 0, e, 0, a + d * i, b + d * k, h ? "" : "Z"];
            }
        },
        clipRect: function(a, b, c, d) {
            var e = "highcharts-" + Kb++, f = this.createElement("clipPath").attr({ id: e }).add(this.defs),
                a = this.rect(a, b, c, d, 0).add(f);
            a.id = e;
            a.clipPath = f;
            return a;
        },
        color: function(a, b, c) {
            var d = this, e, f = /^rgba/, g, h, i, k, j, l, m, p = [];
            a && a.linearGradient ? g = "linearGradient" : a && a.radialGradient && (g = "radialGradient");
            if (g) {
                c = a[g];
                h = d.gradients;
                k = a.stops;
                b = b.radialReference;
                Ya(c) && (a[g] = c = { x1: c[0], y1: c[1], x2: c[2], y2: c[3], gradientUnits: "userSpaceOnUse" });
                g === "radialGradient" && b && !r(c.gradientUnits) && (c = u(c, { cx: b[0] - b[2] / 2 + c.cx * b[2], cy: b[1] - b[2] / 2 + c.cy * b[2], r: c.r * b[2], gradientUnits: "userSpaceOnUse" }));
                for (m in c)
                    m !==
                        "id" && p.push(m, c[m]);
                for (m in k) p.push(k[m]);
                p = p.join(",");
                h[p] ? a = h[p].id : (c.id = a = "highcharts-" + Kb++, h[p] = i = d.createElement(g).attr(c).add(d.defs), i.stops = [], q(k, function(a) {
                    f.test(a[1]) ? (e = ya(a[1]), j = e.get("rgb"), l = e.get("a")) : (j = a[1], l = 1);
                    a = d.createElement("stop").attr({ offset: a[0], "stop-color": j, "stop-opacity": l }).add(i);
                    i.stops.push(a);
                }));
                return "url(" + d.url + "#" + a + ")";
            } else return f.test(a) ? (e = ya(a), C(b, c + "-opacity", e.get("a")), e.get("rgb")) : (b.removeAttribute(c + "-opacity"), a);
        },
        text: function(a,
            b, c, d) {
            var e = K.chart.style, f = ia || !aa && this.forExport;
            if (d && !this.forExport) return this.html(a, b, c);
            b = t(o(b, 0));
            c = t(o(c, 0));
            a = this.createElement("text").attr({ x: b, y: c, text: a }).css({ fontFamily: e.fontFamily, fontSize: e.fontSize });
            f && a.css({ position: "absolute" });
            a.x = b;
            a.y = c;
            return a;
        },
        html: function(a, b, c) {
            var d = K.chart.style, e = this.createElement("span"), f = e.attrSetters, g = e.element, h = e.renderer;
            f.text = function(a) {
                a !== g.innerHTML && delete this.bBox;
                g.innerHTML = a;
                return !1;
            };
            f.x = f.y = f.align = function(a, b) {
                b ===
                    "align" && (b = "textAlign");
                e[b] = a;
                e.htmlUpdateTransform();
                return !1;
            };
            e.attr({ text: a, x: t(b), y: t(c) }).css({ position: "absolute", whiteSpace: "nowrap", fontFamily: d.fontFamily, fontSize: d.fontSize });
            e.css = e.htmlCss;
            if (h.isSVG)
                e.add = function(a) {
                    var b, c = h.box.parentNode, d = [];
                    if (this.parentGroup = a) {
                        if (b = a.div, !b) {
                            for (; a;) d.push(a), a = a.parentGroup;
                            q(d.reverse(), function(a) {
                                var d;
                                b = a.div = a.div || Y(Ta, { className: C(a.element, "class") }, { position: "absolute", left: (a.translateX || 0) + "px", top: (a.translateY || 0) + "px" }, b || c);
                                d = b.style;
                                x(a.attrSetters, { translateX: function(a) { d.left = a + "px"; }, translateY: function(a) { d.top = a + "px"; }, visibility: function(a, b) { d[b] = a; } });
                            });
                        }
                    } else b = c;
                    b.appendChild(g);
                    e.added = !0;
                    e.alignOnAdd && e.htmlUpdateTransform();
                    return e;
                };
            return e;
        },
        fontMetrics: function(a) {
            var a = B(a || 11), a = a < 24 ? a + 4 : t(a * 1.2), b = t(a * 0.8);
            return { h: a, b: b };
        },
        label: function(a, b, c, d, e, f, g, h, i) {

            function k() {
                var a, b;
                a = o.element.style;
                y = (db === void 0 || A === void 0 || n.styles.textAlign) && o.getBBox();
                n.width = (db || y.width || 0) + 2 * ja + v;
                n.height = (A ||
                    y.height || 0) + 2 * ja;
                z = ja + p.fontMetrics(a && a.fontSize).b;
                if (B) {
                    if (!w) a = t(-T * ja), b = h ? -z : 0, n.box = w = d ? p.symbol(d, a, b, n.width, n.height, Xa) : p.rect(a, b, n.width, n.height, 0, Xa[Yb]), w.add(n);
                    w.isImg || w.attr(u({ width: n.width, height: n.height }, Xa));
                    Xa = null;
                }
            }

            function j() {
                var a = n.styles, a = a && a.textAlign, b = v + ja * (1 - T), c;
                c = h ? 0 : z;
                if (r(db) && (a === "center" || a === "right")) b += { center: 0.5, right: 1 }[a] * (db - y.width);
                (b !== o.x || c !== o.y) && o.attr({ x: b, y: c });
                o.x = b;
                o.y = c;
            }

            function l(a, b) { w ? w.attr(a, b) : Xa[a] = b; }

            function m() {
                o.add(n);
                n.attr({
                    text: a,
                    x: b,
                    y: c
                });
                w && r(e) && n.attr({ anchorX: e, anchorY: f });
            }

            var p = this, n = p.g(i), o = p.text("", 0, 0, g).attr({ zIndex: 1 }), w, y, T = 0, ja = 3, v = 0, db, A, F, Aa, I = 0, Xa = {}, z, g = n.attrSetters, B;
            E(n, "add", m);
            g.width = function(a) {
                db = a;
                return !1;
            };
            g.height = function(a) {
                A = a;
                return !1;
            };
            g.padding = function(a) {
                r(a) && a !== ja && (ja = a, j());
                return !1;
            };
            g.paddingLeft = function(a) {
                r(a) && a !== v && (v = a, j());
                return !1;
            };
            g.align = function(a) {
                T = { left: 0, center: 0.5, right: 1 }[a];
                return !1;
            };
            g.text = function(a, b) {
                o.attr(b, a);
                k();
                j();
                return !1;
            };
            g[Yb] = function(a, b) {
                B = !0;
                I =
                    a % 2 / 2;
                l(b, a);
                return !1;
            };
            g.stroke = g.fill = g.r = function(a, b) {
                b === "fill" && (B = !0);
                l(b, a);
                return !1;
            };
            g.anchorX = function(a, b) {
                e = a;
                l(b, a + I - F);
                return !1;
            };
            g.anchorY = function(a, b) {
                f = a;
                l(b, a - Aa);
                return !1;
            };
            g.x = function(a) {
                n.x = a;
                a -= T * ((db || y.width) + ja);
                F = t(a);
                n.attr("translateX", F);
                return !1;
            };
            g.y = function(a) {
                Aa = n.y = t(a);
                n.attr("translateY", Aa);
                return !1;
            };
            var C = n.css;
            return x(n, {
                css: function(a) {
                    if (a) {
                        var b = {}, a = u(a);
                        q("fontSize,fontWeight,fontFamily,color,lineHeight,width,textDecoration,textShadow".split(","), function(c) {
                            a[c] !==
                                s && (b[c] = a[c], delete a[c]);
                        });
                        o.css(b);
                    }
                    return C.call(n, a);
                },
                getBBox: function() { return { width: y.width + 2 * ja, height: y.height + 2 * ja, x: y.x - ja, y: y.y - ja }; },
                shadow: function(a) {
                    w && w.shadow(a);
                    return n;
                },
                destroy: function() {
                    U(n, "add", m);
                    U(n.element, "mouseenter");
                    U(n.element, "mouseleave");
                    o && (o = o.destroy());
                    w && (w = w.destroy());
                    Fa.prototype.destroy.call(n);
                    n = p = k = j = l = m = null;
                }
            });
        }
    };
    cb = za;
    var kb, X;
    if (!aa && !ia)
        Highcharts.VMLElement = X = {
            init: function(a, b) {
                var c = ["<", b, ' filled="f" stroked="f"'], d = ["position: ", "absolute", ";"],
                    e = b === Ta;
                (b === "shape" || e) && d.push("left:0;top:0;width:1px;height:1px;");
                d.push("visibility: ", e ? "hidden" : "visible");
                c.push(' style="', d.join(""), '"/>');
                if (b) c = e || b === "span" || b === "img" ? c.join("") : a.prepVML(c), this.element = Y(c);
                this.renderer = a;
                this.attrSetters = {};
            },
            add: function(a) {
                var b = this.renderer, c = this.element, d = b.box, d = a ? a.element || a : d;
                a && a.inverted && b.invertChild(c, d);
                d.appendChild(c);
                this.added = !0;
                this.alignOnAdd && !this.deferUpdateTransform && this.updateTransform();
                L(this, "add");
                return this;
            },
            updateTransform: Fa.prototype.htmlUpdateTransform,
            setSpanRotation: function(a, b, c) { G(this.element, { filter: a ? ["progid:DXImageTransform.Microsoft.Matrix(M11=", c, ", M12=", -b, ", M21=", b, ", M22=", c, ", sizingMethod='auto expand')"].join("") : Z }); },
            pathToVML: function(a) {
                for (var b = a.length, c = [], d; b--;)
                    if (sa(a[b])) c[b] = t(a[b] * 10) - 5;
                    else if (a[b] === "Z") c[b] = "x";
                    else if (c[b] = a[b], a.isArc && (a[b] === "wa" || a[b] === "at")) d = a[b] === "wa" ? 1 : -1, c[b + 5] === c[b + 7] && (c[b + 7] -= d), c[b + 6] === c[b + 8] && (c[b + 8] -= d);
                return c.join(" ") || "x";
            },
            attr: function(a, b) {
                var c, d, e, f = this.element || {}, g = f.style, h = f.nodeName, i = this.renderer, k = this.symbolName, j, l = this.shadows, m, p = this.attrSetters, n = this;
                ka(a) && r(b) && (c = a, a = {}, a[c] = b);
                if (ka(a)) c = a, n = c === "strokeWidth" || c === "stroke-width" ? this.strokeweight : this[c];
                else
                    for (c in a)
                        if (d = a[c], m = !1, e = p[c] && p[c].call(this, d, c), e !== !1 && d !== null) {
                            e !== s && (d = e);
                            if (k && /^(x|y|r|start|end|width|height|innerR|anchorX|anchorY)/.test(c)) j || (this.symbolAttr(a), j = !0), m = !0;
                            else if (c === "d") {
                                d = d || [];
                                this.d = d.join(" ");
                                f.path =
                                    d = this.pathToVML(d);
                                if (l) for (e = l.length; e--;) l[e].path = l[e].cutOff ? this.cutOffPath(d, l[e].cutOff) : d;
                                m = !0;
                            } else if (c === "visibility") {
                                if (l) for (e = l.length; e--;) l[e].style[c] = d;
                                h === "DIV" && (d = d === "hidden" ? "-999em" : 0, tb || (g[c] = d ? "visible" : "hidden"), c = "top");
                                g[c] = d;
                                m = !0;
                            } else if (c === "zIndex") d && (g[c] = d), m = !0;
                            else if (xa(c, ["x", "y", "width", "height"]) !== -1) this[c] = d, c === "x" || c === "y" ? c = { x: "left", y: "top" }[c] : d = v(0, d), this.updateClipping ? (this[c] = d, this.updateClipping()) : g[c] = d, m = !0;
                            else if (c === "class" && h === "DIV")
                                f.className =
                                    d;
                            else if (c === "stroke") d = i.color(d, f, c), c = "strokecolor";
                            else if (c === "stroke-width" || c === "strokeWidth") f.stroked = d ? !0 : !1, c = "strokeweight", this[c] = d, sa(d) && (d += "px");
                            else if (c === "dashstyle") (f.getElementsByTagName("stroke")[0] || Y(i.prepVML(["<stroke/>"]), null, null, f))[c] = d || "solid", this.dashstyle = d, m = !0;
                            else if (c === "fill")
                                if (h === "SPAN") g.color = d;
                                else {
                                    if (h !== "IMG") f.filled = d !== Z ? !0 : !1, d = i.color(d, f, c, this), c = "fillcolor";
                                }
                            else if (c === "opacity") m = !0;
                            else if (h === "shape" && c === "rotation")
                                this[c] = f.style[c] =
                                    d, f.style.left = -t(ha(d * gb) + 1) + "px", f.style.top = t(ca(d * gb)) + "px";
                            else if (c === "translateX" || c === "translateY" || c === "rotation") this[c] = d, this.updateTransform(), m = !0;
                            else if (c === "text") this.bBox = null, f.innerHTML = d, m = !0;
                            m || (tb ? f[c] = d : C(f, c, d));
                        }
                return n;
            },
            clip: function(a) {
                var b = this, c;
                a ? (c = a.members, ma(c, b), c.push(b), b.destroyClip = function() { ma(c, b); }, a = a.getCSS(b)) : (b.destroyClip && b.destroyClip(), a = { clip: tb ? "inherit" : "rect(auto)" });
                return b.css(a);
            },
            css: Fa.prototype.htmlCss,
            safeRemoveChild: function(a) {
                a.parentNode &&
                    $a(a);
            },
            destroy: function() {
                this.destroyClip && this.destroyClip();
                return Fa.prototype.destroy.apply(this);
            },
            on: function(a, b) {
                this.element["on" + a] = function() {
                    var a = W.event;
                    a.target = a.srcElement;
                    b(a);
                };
                return this;
            },
            cutOffPath: function(a, b) {
                var c, a = a.split(/[ ,]/);
                c = a.length;
                if (c === 9 || c === 11) a[c - 4] = a[c - 2] = B(a[c - 2]) - 10 * b;
                return a.join(" ");
            },
            shadow: function(a, b, c) {
                var d = [], e, f = this.element, g = this.renderer, h, i = f.style, k, j = f.path, l, m, p, n;
                j && typeof j.value !== "string" && (j = "x");
                m = j;
                if (a) {
                    p = o(a.width, 3);
                    n = (a.opacity ||
                        0.15) / p;
                    for (e = 1; e <= 3; e++) {
                        l = p * 2 + 1 - 2 * e;
                        c && (m = this.cutOffPath(j.value, l + 0.5));
                        k = ['<shape isShadow="true" strokeweight="', l, '" filled="false" path="', m, '" coordsize="10 10" style="', f.style.cssText, '" />'];
                        h = Y(g.prepVML(k), null, { left: B(i.left) + o(a.offsetX, 1), top: B(i.top) + o(a.offsetY, 1) });
                        if (c) h.cutOff = l + 1;
                        k = ['<stroke color="', a.color || "black", '" opacity="', n * e, '"/>'];
                        Y(g.prepVML(k), null, null, h);
                        b ? b.element.appendChild(h) : f.parentNode.insertBefore(h, f);
                        d.push(h);
                    }
                    this.shadows = d;
                }
                return this;
            }
        }, X = ba(Fa, X),
        X = {
            Element: X,
            isIE8: wa.indexOf("MSIE 8.0") > -1,
            init: function(a, b, c) {
                var d, e;
                this.alignedObjects = [];
                d = this.createElement(Ta);
                e = d.element;
                e.style.position = "relative";
                a.appendChild(d.element);
                this.isVML = !0;
                this.box = e;
                this.boxWrapper = d;
                this.setSize(b, c, !1);
                if (!J.namespaces.hcv) {
                    J.namespaces.add("hcv", "urn:schemas-microsoft-com:vml");
                    try {
                        J.createStyleSheet().cssText = "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } ";
                    } catch(f) {
                        J.styleSheets[0].cssText +=
                            "hcv\\:fill, hcv\\:path, hcv\\:shape, hcv\\:stroke{ behavior:url(#default#VML); display: inline-block; } ";
                    }
                }
            },
            isHidden: function() { return !this.box.offsetWidth; },
            clipRect: function(a, b, c, d) {
                var e = this.createElement(), f = $(a);
                return x(e, {
                    members: [],
                    left: (f ? a.x : a) + 1,
                    top: (f ? a.y : b) + 1,
                    width: (f ? a.width : c) - 1,
                    height: (f ? a.height : d) - 1,
                    getCSS: function(a) {
                        var b = a.element, c = b.nodeName, a = a.inverted, d = this.top - (c === "shape" ? b.offsetTop : 0), e = this.left, b = e + this.width, f = d + this.height, d = {
                            clip: "rect(" + t(a ? e : d) + "px," + t(a ? f :
                                b) + "px," + t(a ? b : f) + "px," + t(a ? d : e) + "px)"
                        };
                        !a && tb && c === "DIV" && x(d, { width: b + "px", height: f + "px" });
                        return d;
                    },
                    updateClipping: function() { q(e.members, function(a) { a.css(e.getCSS(a)); }); }
                });
            },
            color: function(a, b, c, d) {
                var e = this, f, g = /^rgba/, h, i, k = Z;
                a && a.linearGradient ? i = "gradient" : a && a.radialGradient && (i = "pattern");
                if (i) {
                    var j, l, m = a.linearGradient || a.radialGradient, p, n, o, w, y, T = "", a = a.stops, v, s = [], t = function() {
                        h = ['<fill colors="' + s.join(",") + '" opacity="', o, '" o:opacity2="', n, '" type="', i, '" ', T, 'focus="100%" method="any" />'];
                        Y(e.prepVML(h), null, null, b);
                    };
                    p = a[0];
                    v = a[a.length - 1];
                    p[0] > 0 && a.unshift([0, p[1]]);
                    v[0] < 1 && a.push([1, v[1]]);
                    q(a, function(a, b) {
                        g.test(a[1]) ? (f = ya(a[1]), j = f.get("rgb"), l = f.get("a")) : (j = a[1], l = 1);
                        s.push(a[0] * 100 + "% " + j);
                        b ? (o = l, w = j) : (n = l, y = j);
                    });
                    if (c === "fill")
                        if (i === "gradient") c = m.x1 || m[0] || 0, a = m.y1 || m[1] || 0, p = m.x2 || m[2] || 0, m = m.y2 || m[3] || 0, T = 'angle="' + (90 - S.atan((m - a) / (p - c)) * 180 / Ha) + '"', t();
                        else {
                            var k = m.r, A = k * 2, F = k * 2, r = m.cx, I = m.cy, x = b.radialReference, u, k = function() {
                                x && (u = d.getBBox(), r += (x[0] - u.x) / u.width -
                                    0.5, I += (x[1] - u.y) / u.height - 0.5, A *= x[2] / u.width, F *= x[2] / u.height);
                                T = 'src="' + K.global.VMLRadialGradientURL + '" size="' + A + "," + F + '" origin="0.5,0.5" position="' + r + "," + I + '" color2="' + y + '" ';
                                t();
                            };
                            d.added ? k() : E(d, "add", k);
                            k = w;
                        }
                    else k = j;
                } else if (g.test(a) && b.tagName !== "IMG") f = ya(a), h = ["<", c, ' opacity="', f.get("a"), '"/>'], Y(this.prepVML(h), null, null, b), k = f.get("rgb");
                else {
                    k = b.getElementsByTagName(c);
                    if (k.length) k[0].opacity = 1, k[0].type = "solid";
                    k = a;
                }
                return k;
            },
            prepVML: function(a) {
                var b = this.isIE8, a = a.join("");
                b ?
                    (a = a.replace("/>", ' xmlns="urn:schemas-microsoft-com:vml" />'), a = a.indexOf('style="') === -1 ? a.replace("/>", ' style="display:inline-block;behavior:url(#default#VML);" />') : a.replace('style="', 'style="display:inline-block;behavior:url(#default#VML);')) : a = a.replace("<", "<hcv:");
                return a;
            },
            text: za.prototype.html,
            path: function(a) {
                var b = { coordsize: "10 10" };
                Ya(a) ? b.d = a : $(a) && x(b, a);
                return this.createElement("shape").attr(b);
            },
            circle: function(a, b, c) {
                var d = this.symbol("circle");
                if ($(a)) c = a.r, b = a.y, a = a.x;
                d.isCircle =
                    !0;
                d.r = c;
                return d.attr({ x: a, y: b });
            },
            g: function(a) {
                var b;
                a && (b = { className: "highcharts-" + a, "class": "highcharts-" + a });
                return this.createElement(Ta).attr(b);
            },
            image: function(a, b, c, d, e) {
                var f = this.createElement("img").attr({ src: a });
                arguments.length > 1 && f.attr({ x: b, y: c, width: d, height: e });
                return f;
            },
            rect: function(a, b, c, d, e, f) {
                var g = this.symbol("rect");
                g.r = $(a) ? a.r : e;
                return g.attr($(a) ? a : g.crisp(f, a, b, v(c, 0), v(d, 0)));
            },
            invertChild: function(a, b) {
                var c = b.style;
                G(a, { flip: "x", left: B(c.width) - 1, top: B(c.height) - 1, rotation: -90 });
            },
            symbols: {
                arc: function(a, b, c, d, e) {
                    var f = e.start, g = e.end, h = e.r || c || d, c = e.innerR, d = ca(f), i = ha(f), k = ca(g), j = ha(g);
                    if (g - f === 0) return ["x"];
                    f = ["wa", a - h, b - h, a + h, b + h, a + h * d, b + h * i, a + h * k, b + h * j];
                    e.open && !c && f.push("e", "M", a, b);
                    f.push("at", a - c, b - c, a + c, b + c, a + c * k, b + c * j, a + c * d, b + c * i, "x", "e");
                    f.isArc = !0;
                    return f;
                },
                circle: function(a, b, c, d, e) {
                    e && (c = d = 2 * e.r);
                    e && e.isCircle && (a -= c / 2, b -= d / 2);
                    return ["wa", a, b, a + c, b + d, a + c, b + d / 2, a + c, b + d / 2, "e"];
                },
                rect: function(a, b, c, d, e) {
                    var f = a + c, g = b + d, h;
                    !r(e) || !e.r ? f = za.prototype.symbols.square.apply(0,
                        arguments) : (h = z(e.r, c, d), f = ["M", a + h, b, "L", f - h, b, "wa", f - 2 * h, b, f, b + 2 * h, f - h, b, f, b + h, "L", f, g - h, "wa", f - 2 * h, g - 2 * h, f, g, f, g - h, f - h, g, "L", a + h, g, "wa", a, g - 2 * h, a + 2 * h, g, a + h, g, a, g - h, "L", a, b + h, "wa", a, b, a + 2 * h, b + 2 * h, a, b + h, a + h, b, "x", "e"]);
                    return f;
                }
            }
        }, Highcharts.VMLRenderer = kb = function() { this.init.apply(this, arguments); }, kb.prototype = u(za.prototype, X), cb = kb;
    za.prototype.measureSpanWidth = function(a, b) {
        var c = J.createElement("span"), d = J.createTextNode(a);
        c.appendChild(d);
        G(c, b);
        this.box.appendChild(c);
        return c.offsetWidth;
    };
    var Zb;
    if (ia)
        Highcharts.CanVGRenderer = X = function() { Ia = "http://www.w3.org/1999/xhtml"; }, X.prototype.symbols = {}, Zb = function() {

            function a() {
                var a = b.length, d;
                for (d = 0; d < a; d++) b[d]();
                b = [];
            }

            var b = [];
            return {
                push: function(c, d) {
                    b.length === 0 && cc(d, a);
                    b.push(c);
                }
            };
        }(), cb = X;
    bb.prototype = {
        addLabel: function() {
            var a = this.axis, b = a.options, c = a.chart, d = a.horiz, e = a.categories, f = a.names, g = this.pos, h = b.labels, i = a.tickPositions, d = d && e && !h.step && !h.staggerLines && !h.rotation && c.plotWidth / i.length || !d && (c.margin[3] || c.chartWidth *
                0.33), k = g === i[0], j = g === i[i.length - 1], l, f = e ? o(e[g], f[g], g) : g, e = this.label, m = i.info;
            a.isDatetimeAxis && m && (l = b.dateTimeLabelFormats[m.higherRanks[g] || m.unitName]);
            this.isFirst = k;
            this.isLast = j;
            b = a.labelFormatter.call({ axis: a, chart: c, isFirst: k, isLast: j, dateTimeLabelFormat: l, value: a.isLog ? na(la(f)) : f });
            g = d && { width: v(1, t(d - 2 * (h.padding || 10))) + "px" };
            g = x(g, h.style);
            if (r(e)) e && e.attr({ text: b }).css(g);
            else {
                l = { align: a.labelAlign };
                if (sa(h.rotation)) l.rotation = h.rotation;
                if (d && h.ellipsis) l._clipHeight = a.len / i.length;
                this.label = r(b) && h.enabled ? c.renderer.text(b, 0, 0, h.useHTML).attr(l).css(g).add(a.labelGroup) : null;
            }
        },
        getLabelSize: function() {
            var a = this.label, b = this.axis;
            return a ? (this.labelBBox = a.getBBox())[b.horiz ? "height" : "width"] : 0;
        },
        getLabelSides: function() {
            var a = this.axis, b = this.labelBBox.width, a = b * { left: 0, center: 0.5, right: 1 }[a.labelAlign] - a.options.labels.x;
            return [-a, b - a];
        },
        handleOverflow: function(a, b) {
            var c = !0, d = this.axis, e = d.chart, f = this.isFirst, g = this.isLast, h = b.x, i = d.reversed, k = d.tickPositions;
            if (f || g) {
                var j =
                        this.getLabelSides(), l = j[0], j = j[1], e = e.plotLeft, m = e + d.len, k = (d = d.ticks[k[a + (f ? 1 : -1)]]) && d.label.xy && d.label.xy.x + d.getLabelSides()[f ? 0 : 1];
                f && !i || g && i ? h + l < e && (h = e - l, d && h + j > k && (c = !1)) : h + j > m && (h = m - j, d && h + l < k && (c = !1));
                b.x = h;
            }
            return c;
        },
        getPosition: function(a, b, c, d) {
            var e = this.axis, f = e.chart, g = d && f.oldChartHeight || f.chartHeight;
            return {
                x: a ? e.translate(b + c, null, null, d) + e.transB : e.left + e.offset + (e.opposite ? (d && f.oldChartWidth || f.chartWidth) - e.right - e.left : 0),
                y: a ? g - e.bottom + e.offset - (e.opposite ? e.height : 0) :
                    g - e.translate(b + c, null, null, d) - e.transB
            };
        },
        getLabelPosition: function(a, b, c, d, e, f, g, h) {
            var i = this.axis, k = i.transA, j = i.reversed, l = i.staggerLines, m = i.chart.renderer.fontMetrics(e.style.fontSize).b, p = e.rotation, a = a + e.x - (f && d ? f * k * (j ? -1 : 1) : 0), b = b + e.y - (f && !d ? f * k * (j ? 1 : -1) : 0);
            p && i.side === 2 && (b -= m - m * ca(p * gb));
            !r(e.y) && !p && (b += m - c.getBBox().height / 2);
            l && (b += g / (h || 1) % l * (i.labelOffset / l));
            return { x: a, y: b };
        },
        getMarkPath: function(a, b, c, d, e, f) { return f.crispLine(["M", a, b, "L", a + (e ? 0 : -c), b + (e ? c : 0)], d); },
        render: function(a,
            b, c) {
            var d = this.axis, e = d.options, f = d.chart.renderer, g = d.horiz, h = this.type, i = this.label, k = this.pos, j = e.labels, l = this.gridLine, m = h ? h + "Grid" : "grid", p = h ? h + "Tick" : "tick", n = e[m + "LineWidth"], q = e[m + "LineColor"], w = e[m + "LineDashStyle"], y = e[p + "Length"], m = e[p + "Width"] || 0, T = e[p + "Color"], v = e[p + "Position"], p = this.mark, t = j.step, r = !0, A = d.tickmarkOffset, F = this.getPosition(g, k, A, b), u = F.x, F = F.y, x = g && u === d.pos + d.len || !g && F === d.pos ? -1 : 1, Xa = d.staggerLines;
            this.isActive = !0;
            if (n) {
                k = d.getPlotLinePath(k + A, n * x, b, !0);
                if (l ===
                    s) {
                    l = { stroke: q, "stroke-width": n };
                    if (w) l.dashstyle = w;
                    if (!h) l.zIndex = 1;
                    if (b) l.opacity = 0;
                    this.gridLine = l = n ? f.path(k).attr(l).add(d.gridGroup) : null;
                }
                if (!b && l && k) l[this.isNew ? "attr" : "animate"]({ d: k, opacity: c });
            }
            if (m && y) v === "inside" && (y = -y), d.opposite && (y = -y), b = this.getMarkPath(u, F, y, m * x, g, f), p ? p.animate({ d: b, opacity: c }) : this.mark = f.path(b).attr({ stroke: T, "stroke-width": m, opacity: c }).add(d.axisGroup);
            if (i && !isNaN(u))
                i.xy = F = this.getLabelPosition(u, F, i, g, j, A, a, t), this.isFirst && !this.isLast && !o(e.showFirstLabel,
                    1) || this.isLast && !this.isFirst && !o(e.showLastLabel, 1) ? r = !1 : !Xa && g && j.overflow === "justify" && !this.handleOverflow(a, F) && (r = !1), t && a % t && (r = !1), r && !isNaN(F.y) ? (F.opacity = c, i[this.isNew ? "attr" : "animate"](F), this.isNew = !1) : i.attr("y", -9999);
        },
        destroy: function() { Ea(this, this.axis); }
    };
    Gb.prototype = {
        render: function() {
            var a = this, b = a.axis, c = b.horiz, d = (b.pointRange || 0) / 2, e = a.options, f = e.label, g = a.label, h = e.width, i = e.to, k = e.from, j = r(k) && r(i), l = e.value, m = e.dashStyle, p = a.svgElem, n = [], q, w = e.color, y = e.zIndex, T = e.events,
                s = b.chart.renderer;
            b.isLog && (k = ta(k), i = ta(i), l = ta(l));
            if (h) {
                if (n = b.getPlotLinePath(l, h), d = { stroke: w, "stroke-width": h }, m) d.dashstyle = m;
            } else if (j) {
                if (k = v(k, b.min - d), i = z(i, b.max + d), n = b.getPlotBandPath(k, i, e), d = { fill: w }, e.borderWidth) d.stroke = e.borderColor, d["stroke-width"] = e.borderWidth;
            } else return;
            if (r(y)) d.zIndex = y;
            if (p)
                if (n) p.animate({ d: n }, null, p.onGetPath);
                else {
                    if (p.hide(), p.onGetPath = function() { p.show(); }, g) a.label = g = g.destroy();
                }
            else if (n && n.length && (a.svgElem = p = s.path(n).attr(d).add(), T))
                for (q in e =
                    function(b) { p.on(b, function(c) { T[b].apply(a, [c]); }); }, T) e(q);
            if (f && r(f.text) && n && n.length && b.width > 0 && b.height > 0) {
                f = u({ align: c && j && "center", x: c ? !j && 4 : 10, verticalAlign: !c && j && "middle", y: c ? j ? 16 : 10 : j ? 6 : -4, rotation: c && !j && 90 }, f);
                if (!g) a.label = g = s.text(f.text, 0, 0, f.useHTML).attr({ align: f.textAlign || f.align, rotation: f.rotation, zIndex: y }).css(f.style).add();
                b = [n[1], n[4], o(n[6], n[1])];
                n = [n[2], n[5], o(n[7], n[2])];
                c = Sa(b);
                j = Sa(n);
                g.align(f, !1, { x: c, y: j, width: va(b) - c, height: va(n) - j });
                g.show();
            } else g && g.hide();
            return a;
        },
        destroy: function() {
            ma(this.axis.plotLinesAndBands, this);
            delete this.axis;
            Ea(this);
        }
    };
    Ub.prototype = {
        destroy: function() { Ea(this, this.axis); },
        render: function(a) {
            var b = this.options, c = b.format, c = c ? Oa(c, this) : b.formatter.call(this);
            this.label ? this.label.attr({ text: c, visibility: "hidden" }) : this.label = this.axis.chart.renderer.text(c, 0, 0, b.useHTML).css(b.style).attr({ align: this.textAlign, rotation: b.rotation, visibility: "hidden" }).add(a);
        },
        setOffset: function(a, b) {
            var c = this.axis, d = c.chart, e = d.inverted, f = this.isNegative,
                g = c.translate(this.percent ? 100 : this.total, 0, 0, 0, 1), c = c.translate(0), c = M(g - c), h = d.xAxis[0].translate(this.x) + a, i = d.plotHeight, f = { x: e ? f ? g : g - c : h, y: e ? i - h - b : f ? i - g - c : i - g, width: e ? c : b, height: e ? b : c };
            if (e = this.label) e.align(this.alignOptions, null, f), f = e.alignAttr, e.attr({ visibility: this.options.crop === !1 || d.isInsidePlot(f.x, f.y) ? aa ? "inherit" : "visible" : "hidden" });
        }
    };
    oa.prototype = {
        defaultOptions: {
            dateTimeLabelFormats: {
                millisecond: "%H:%M:%S.%L",
                second: "%H:%M:%S",
                minute: "%H:%M",
                hour: "%H:%M",
                day: "%e. %b",
                week: "%e. %b",
                month: "%b '%y",
                year: "%Y"
            },
            endOnTick: !1,
            gridLineColor: "#C0C0C0",
            labels: D,
            lineColor: "#C0D0E0",
            lineWidth: 1,
            minPadding: 0.01,
            maxPadding: 0.01,
            minorGridLineColor: "#E0E0E0",
            minorGridLineWidth: 1,
            minorTickColor: "#A0A0A0",
            minorTickLength: 2,
            minorTickPosition: "outside",
            startOfWeek: 1,
            startOnTick: !1,
            tickColor: "#C0D0E0",
            tickLength: 5,
            tickmarkPlacement: "between",
            tickPixelInterval: 100,
            tickPosition: "outside",
            tickWidth: 1,
            title: { align: "middle", style: { color: "#4d759e", fontWeight: "bold" } },
            type: "linear"
        },
        defaultYAxisOptions: {
            endOnTick: !0,
            gridLineWidth: 1,
            tickPixelInterval: 72,
            showLastLabel: !0,
            labels: { x: -8, y: 3 },
            lineWidth: 0,
            maxPadding: 0.05,
            minPadding: 0.05,
            startOnTick: !0,
            tickWidth: 0,
            title: { rotation: 270, text: "Values" },
            stackLabels: { enabled: !1, formatter: function() { return Ca(this.total, -1); }, style: D.style }
        },
        defaultLeftAxisOptions: { labels: { x: -8, y: null }, title: { rotation: 270 } },
        defaultRightAxisOptions: { labels: { x: 8, y: null }, title: { rotation: 90 } },
        defaultBottomAxisOptions: { labels: { x: 0, y: 14 }, title: { rotation: 0 } },
        defaultTopAxisOptions: {
            labels: { x: 0, y: -5 },
            title: { rotation: 0 }
        },
        init: function(a, b) {
            var c = b.isX;
            this.horiz = a.inverted ? !c : c;
            this.xOrY = (this.isXAxis = c) ? "x" : "y";
            this.opposite = b.opposite;
            this.side = this.horiz ? this.opposite ? 0 : 2 : this.opposite ? 1 : 3;
            this.setOptions(b);
            var d = this.options, e = d.type;
            this.labelFormatter = d.labels.formatter || this.defaultLabelFormatter;
            this.userOptions = b;
            this.minPixelPadding = 0;
            this.chart = a;
            this.reversed = d.reversed;
            this.zoomEnabled = d.zoomEnabled !== !1;
            this.categories = d.categories || e === "category";
            this.names = [];
            this.isLog = e === "logarithmic";
            this.isDatetimeAxis = e === "datetime";
            this.isLinked = r(d.linkedTo);
            this.tickmarkOffset = this.categories && d.tickmarkPlacement === "between" ? 0.5 : 0;
            this.ticks = {};
            this.minorTicks = {};
            this.plotLinesAndBands = [];
            this.alternateBands = {};
            this.len = 0;
            this.minRange = this.userMinRange = d.minRange || d.maxZoom;
            this.range = d.range;
            this.offset = d.offset || 0;
            this.stacks = {};
            this.oldStacks = {};
            this.stackExtremes = {};
            this.min = this.max = null;
            var f, d = this.options.events;
            xa(this, a.axes) === -1 && (a.axes.push(this), a[c ? "xAxis" : "yAxis"].push(this));
            this.series = this.series || [];
            if (a.inverted && c && this.reversed === s) this.reversed = !0;
            this.removePlotLine = this.removePlotBand = this.removePlotBandOrLine;
            for (f in d) E(this, f, d[f]);
            if (this.isLog) this.val2lin = ta, this.lin2val = la;
        },
        setOptions: function(a) { this.options = u(this.defaultOptions, this.isXAxis ? {} : this.defaultYAxisOptions, [this.defaultTopAxisOptions, this.defaultRightAxisOptions, this.defaultBottomAxisOptions, this.defaultLeftAxisOptions][this.side], u(K[this.isXAxis ? "xAxis" : "yAxis"], a)); },
        update: function(a,
            b) {
            var c = this.chart, a = c.options[this.xOrY + "Axis"][this.options.index] = u(this.userOptions, a);
            this.destroy(!0);
            this._addedPlotLB = this.userMin = this.userMax = s;
            this.init(c, x(a, { events: s }));
            c.isDirtyBox = !0;
            o(b, !0) && c.redraw();
        },
        remove: function(a) {
            var b = this.chart, c = this.xOrY + "Axis";
            q(this.series, function(a) { a.remove(!1); });
            ma(b.axes, this);
            ma(b[c], this);
            b.options[c].splice(this.options.index, 1);
            q(b[c], function(a, b) { a.options.index = b; });
            this.destroy();
            b.isDirtyBox = !0;
            o(a, !0) && b.redraw();
        },
        defaultLabelFormatter: function() {
            var a =
                    this.axis, b = this.value, c = a.categories, d = this.dateTimeLabelFormat, e = K.lang.numericSymbols, f = e && e.length, g, h = a.options.labels.format, a = a.isLog ? b : a.tickInterval;
            if (h) g = Oa(h, this);
            else if (c) g = b;
            else if (d) g = qa(d, b);
            else if (f && a >= 1E3) for (; f-- && g === s;) c = Math.pow(1E3, f + 1), a >= c && e[f] !== null && (g = Ca(b / c, -1) + e[f]);
            g === s && (g = b >= 1E3 ? Ca(b, 0) : Ca(b, -1));
            return g;
        },
        getSeriesExtremes: function() {
            var a = this, b = a.chart;
            a.hasVisibleSeries = !1;
            a.dataMin = a.dataMax = null;
            a.stackExtremes = {};
            a.buildStacks();
            q(a.series, function(c) {
                if (c.visible ||
                    !b.options.chart.ignoreHiddenSeries) {
                    var d;
                    d = c.options.threshold;
                    var e;
                    a.hasVisibleSeries = !0;
                    a.isLog && d <= 0 && (d = null);
                    if (a.isXAxis) {
                        if (d = c.xData, d.length) a.dataMin = z(o(a.dataMin, d[0]), Sa(d)), a.dataMax = v(o(a.dataMax, d[0]), va(d));
                    } else {
                        c.getExtremes();
                        e = c.dataMax;
                        c = c.dataMin;
                        if (r(c) && r(e)) a.dataMin = z(o(a.dataMin, c), c), a.dataMax = v(o(a.dataMax, e), e);
                        if (r(d))
                            if (a.dataMin >= d) a.dataMin = d, a.ignoreMinPadding = !0;
                            else if (a.dataMax < d) a.dataMax = d, a.ignoreMaxPadding = !0;
                    }
                }
            });
        },
        translate: function(a, b, c, d, e, f) {
            var g =
                    this.len, h = 1, i = 0, k = d ? this.oldTransA : this.transA, d = d ? this.oldMin : this.min, j = this.minPixelPadding, e = (this.options.ordinal || this.isLog && e) && this.lin2val;
            if (!k) k = this.transA;
            c && (h *= -1, i = g);
            this.reversed && (h *= -1, i -= h * g);
            b ? (a = a * h + i, a -= j, a = a / k + d, e && (a = this.lin2val(a))) : (e && (a = this.val2lin(a)), f === "between" && (f = 0.5), a = h * (a - d) * k + i + h * j + (sa(f) ? k * f * this.pointRange : 0));
            return a;
        },
        toPixels: function(a, b) { return this.translate(a, !1, !this.horiz, null, !0) + (b ? 0 : this.pos); },
        toValue: function(a, b) {
            return this.translate(a -
                (b ? 0 : this.pos), !0, !this.horiz, null, !0);
        },
        getPlotLinePath: function(a, b, c, d) {
            var e = this.chart, f = this.left, g = this.top, h, i, k, a = this.translate(a, null, null, c), j = c && e.oldChartHeight || e.chartHeight, l = c && e.oldChartWidth || e.chartWidth, m;
            h = this.transB;
            c = i = t(a + h);
            h = k = t(j - a - h);
            if (isNaN(a)) m = !0;
            else if (this.horiz) {
                if (h = g, k = j - this.bottom, c < f || c > f + this.width) m = !0;
            } else if (c = f, i = l - this.right, h < g || h > g + this.height) m = !0;
            return m && !d ? null : e.renderer.crispLine(["M", c, h, "L", i, k], b || 0);
        },
        getPlotBandPath: function(a, b) {
            var c =
                    this.getPlotLinePath(b), d = this.getPlotLinePath(a);
            d && c ? d.push(c[4], c[5], c[1], c[2]) : d = null;
            return d;
        },
        getLinearTickPositions: function(a, b, c) {
            for (var d, b = na(O(b / a) * a), c = na(Ga(c / a) * a), e = []; b <= c;) {
                e.push(b);
                b = na(b + a);
                if (b === d) break;
                d = b;
            }
            return e;
        },
        getLogTickPositions: function(a, b, c, d) {
            var e = this.options, f = this.len, g = [];
            if (!d) this._minorAutoInterval = null;
            if (a >= 0.5) a = t(a), g = this.getLinearTickPositions(a, b, c);
            else if (a >= 0.08)
                for (var f = O(b), h, i, k, j, l, e = a > 0.3 ? [1, 2, 4] : a > 0.15 ? [1, 2, 4, 6, 8] : [1, 2, 3, 4, 5, 6, 7, 8, 9]; f <
                    c + 1 && !l; f++) {
                    i = e.length;
                    for (h = 0; h < i && !l; h++) k = ta(la(f) * e[h]), k > b && (!d || j <= c) && g.push(j), j > c && (l = !0), j = k;
                }
            else if (b = la(b), c = la(c), a = e[d ? "minorTickInterval" : "tickInterval"], a = o(a === "auto" ? null : a, this._minorAutoInterval, (c - b) * (e.tickPixelInterval / (d ? 5 : 1)) / ((d ? f / this.tickPositions.length : f) || 1)), a = zb(a, null, yb(a)), g = Ja(this.getLinearTickPositions(a, b, c), ta), !d) this._minorAutoInterval = a / 5;
            if (!d) this.tickInterval = a;
            return g;
        },
        getMinorTickPositions: function() {
            var a = this.options, b = this.tickPositions, c = this.minorTickInterval,
                d = [], e;
            if (this.isLog) {
                e = b.length;
                for (a = 1; a < e; a++) d = d.concat(this.getLogTickPositions(c, b[a - 1], b[a], !0));
            } else if (this.isDatetimeAxis && a.minorTickInterval === "auto") d = d.concat(fb(Ab(c), this.min, this.max, a.startOfWeek)), d[0] < this.min && d.shift();
            else for (b = this.min + (b[0] - this.min) % c; b <= this.max; b += c) d.push(b);
            return d;
        },
        adjustForMinRange: function() {
            var a = this.options, b = this.min, c = this.max, d, e = this.dataMax - this.dataMin >= this.minRange, f, g, h, i, k;
            if (this.isXAxis && this.minRange === s && !this.isLog)
                r(a.min) || r(a.max) ?
                    this.minRange = null : (q(this.series, function(a) {
                        i = a.xData;
                        for (g = k = a.xIncrement ? 1 : i.length - 1; g > 0; g--) if (h = i[g] - i[g - 1], f === s || h < f) f = h;
                    }), this.minRange = z(f * 5, this.dataMax - this.dataMin));
            if (c - b < this.minRange) {
                var j = this.minRange;
                d = (j - c + b) / 2;
                d = [b - d, o(a.min, b - d)];
                if (e) d[2] = this.dataMin;
                b = va(d);
                c = [b + j, o(a.max, b + j)];
                if (e) c[2] = this.dataMax;
                c = Sa(c);
                c - b < j && (d[0] = c - j, d[1] = o(a.min, c - j), b = va(d));
            }
            this.min = b;
            this.max = c;
        },
        setAxisTranslation: function(a) {
            var b = this.max - this.min, c = 0, d, e = 0, f = 0, g = this.linkedParent, h = this.transA;
            if (this.isXAxis)
                g ? (e = g.minPointOffset, f = g.pointRangePadding) : q(this.series, function(a) {
                    var g = a.pointRange, h = a.options.pointPlacement, l = a.closestPointRange;
                    g > b && (g = 0);
                    c = v(c, g);
                    e = v(e, ka(h) ? 0 : g / 2);
                    f = v(f, h === "on" ? 0 : g);
                    !a.noSharedTooltip && r(l) && (d = r(d) ? z(d, l) : l);
                }), g = this.ordinalSlope && d ? this.ordinalSlope / d : 1, this.minPointOffset = e *= g, this.pointRangePadding = f *= g, this.pointRange = z(c, b), this.closestPointRange = d;
            if (a) this.oldTransA = h;
            this.translationSlope = this.transA = h = this.len / (b + f || 1);
            this.transB = this.horiz ?
                this.left : this.bottom;
            this.minPixelPadding = h * e;
        },
        setTickPositions: function(a) {
            var b = this, c = b.chart, d = b.options, e = b.isLog, f = b.isDatetimeAxis, g = b.isXAxis, h = b.isLinked, i = b.options.tickPositioner, k = d.maxPadding, j = d.minPadding, l = d.tickInterval, m = d.minTickInterval, p = d.tickPixelInterval, n, ea = b.categories;
            h ? (b.linkedParent = c[g ? "xAxis" : "yAxis"][d.linkedTo], c = b.linkedParent.getExtremes(), b.min = o(c.min, c.dataMin), b.max = o(c.max, c.dataMax), d.type !== b.linkedParent.options.type && ra(11, 1)) : (b.min = o(b.userMin, d.min,
                b.dataMin), b.max = o(b.userMax, d.max, b.dataMax));
            if (e) !a && z(b.min, o(b.dataMin, b.min)) <= 0 && ra(10, 1), b.min = na(ta(b.min)), b.max = na(ta(b.max));
            if (b.range && (b.userMin = b.min = v(b.min, b.max - b.range), b.userMax = b.max, a)) b.range = null;
            b.beforePadding && b.beforePadding();
            b.adjustForMinRange();
            if (!ea && !b.usePercentage && !h && r(b.min) && r(b.max) && (c = b.max - b.min)) {
                if (!r(d.min) && !r(b.userMin) && j && (b.dataMin < 0 || !b.ignoreMinPadding)) b.min -= c * j;
                if (!r(d.max) && !r(b.userMax) && k && (b.dataMax > 0 || !b.ignoreMaxPadding)) b.max += c * k;
            }
            b.min ===
                b.max || b.min === void 0 || b.max === void 0 ? b.tickInterval = 1 : h && !l && p === b.linkedParent.options.tickPixelInterval ? b.tickInterval = b.linkedParent.tickInterval : (b.tickInterval = o(l, ea ? 1 : (b.max - b.min) * p / v(b.len, p)), !r(l) && b.len < p && !this.isRadial && (n = !0, b.tickInterval /= 4));
            g && !a && q(b.series, function(a) { a.processData(b.min !== b.oldMin || b.max !== b.oldMax); });
            b.setAxisTranslation(!0);
            b.beforeSetTickPositions && b.beforeSetTickPositions();
            if (b.postProcessTickInterval) b.tickInterval = b.postProcessTickInterval(b.tickInterval);
            if (b.pointRange) b.tickInterval = v(b.pointRange, b.tickInterval);
            if (!l && b.tickInterval < m) b.tickInterval = m;
            if (!f && !e && !l) b.tickInterval = zb(b.tickInterval, null, yb(b.tickInterval), d);
            b.minorTickInterval = d.minorTickInterval === "auto" && b.tickInterval ? b.tickInterval / 5 : d.minorTickInterval;
            b.tickPositions = a = d.tickPositions ? [].concat(d.tickPositions) : i && i.apply(b, [b.min, b.max]);
            if (!a)
                !b.ordinalPositions && (b.max - b.min) / b.tickInterval > v(2 * b.len, 200) && ra(19, !0), a = f ? (b.getNonLinearTimeTicks || fb)(Ab(b.tickInterval,
                    d.units), b.min, b.max, d.startOfWeek, b.ordinalPositions, b.closestPointRange, !0) : e ? b.getLogTickPositions(b.tickInterval, b.min, b.max) : b.getLinearTickPositions(b.tickInterval, b.min, b.max), n && a.splice(1, a.length - 2), b.tickPositions = a;
            if (!h) e = a[0], f = a[a.length - 1], h = b.minPointOffset || 0, d.startOnTick ? b.min = e : b.min - h > e && a.shift(), d.endOnTick ? b.max = f : b.max + h < f && a.pop(), a.length === 1 && (b.min -= 0.001, b.max += 0.001);
        },
        setMaxTicks: function() {
            var a = this.chart, b = a.maxTicks || {}, c = this.tickPositions, d = this._maxTicksKey =
                [this.xOrY, this.pos, this.len].join("-");
            if (!this.isLinked && !this.isDatetimeAxis && c && c.length > (b[d] || 0) && this.options.alignTicks !== !1) b[d] = c.length;
            a.maxTicks = b;
        },
        adjustTickAmount: function() {
            var a = this._maxTicksKey, b = this.tickPositions, c = this.chart.maxTicks;
            if (c && c[a] && !this.isDatetimeAxis && !this.categories && !this.isLinked && this.options.alignTicks !== !1) {
                var d = this.tickAmount, e = b.length;
                this.tickAmount = a = c[a];
                if (e < a) {
                    for (; b.length < a;) b.push(na(b[b.length - 1] + this.tickInterval));
                    this.transA *= (e - 1) / (a -
                        1);
                    this.max = b[b.length - 1];
                }
                if (r(d) && a !== d) this.isDirty = !0;
            }
        },
        setScale: function() {
            var a = this.stacks, b, c, d, e;
            this.oldMin = this.min;
            this.oldMax = this.max;
            this.oldAxisLength = this.len;
            this.setAxisSize();
            e = this.len !== this.oldAxisLength;
            q(this.series, function(a) { if (a.isDirtyData || a.isDirty || a.xAxis.isDirty) d = !0; });
            if (e || d || this.isLinked || this.forceRedraw || this.userMin !== this.oldUserMin || this.userMax !== this.oldUserMax) {
                if (!this.isXAxis) for (b in a) for (c in a[b]) a[b][c].total = null, a[b][c].cum = 0;
                this.forceRedraw =
                    !1;
                this.getSeriesExtremes();
                this.setTickPositions();
                this.oldUserMin = this.userMin;
                this.oldUserMax = this.userMax;
                if (!this.isDirty) this.isDirty = e || this.min !== this.oldMin || this.max !== this.oldMax;
            } else if (!this.isXAxis) {
                if (this.oldStacks) a = this.stacks = this.oldStacks;
                for (b in a) for (c in a[b]) a[b][c].cum = a[b][c].total;
            }
            this.setMaxTicks();
        },
        setExtremes: function(a, b, c, d, e) {
            var f = this, g = f.chart, c = o(c, !0), e = x(e, { min: a, max: b });
            L(f, "setExtremes", e, function() {
                f.userMin = a;
                f.userMax = b;
                f.eventArgs = e;
                f.isDirtyExtremes =
                    !0;
                c && g.redraw(d);
            });
        },
        zoom: function(a, b) {
            this.allowZoomOutside || (r(this.dataMin) && a <= this.dataMin && (a = s), r(this.dataMax) && b >= this.dataMax && (b = s));
            this.displayBtn = a !== s || b !== s;
            this.setExtremes(a, b, !1, s, { trigger: "zoom" });
            return !0;
        },
        setAxisSize: function() {
            var a = this.chart, b = this.options, c = b.offsetLeft || 0, d = b.offsetRight || 0, e = this.horiz, f, g;
            this.left = g = o(b.left, a.plotLeft + c);
            this.top = f = o(b.top, a.plotTop);
            this.width = c = o(b.width, a.plotWidth - c + d);
            this.height = b = o(b.height, a.plotHeight);
            this.bottom = a.chartHeight -
                b - f;
            this.right = a.chartWidth - c - g;
            this.len = v(e ? c : b, 0);
            this.pos = e ? g : f;
        },
        getExtremes: function() {
            var a = this.isLog;
            return { min: a ? na(la(this.min)) : this.min, max: a ? na(la(this.max)) : this.max, dataMin: this.dataMin, dataMax: this.dataMax, userMin: this.userMin, userMax: this.userMax };
        },
        getThreshold: function(a) {
            var b = this.isLog, c = b ? la(this.min) : this.min, b = b ? la(this.max) : this.max;
            c > a || a === null ? a = c : b < a && (a = b);
            return this.translate(a, 0, 1, 0, 1);
        },
        addPlotBand: function(a) { this.addPlotBandOrLine(a, "plotBands"); },
        addPlotLine: function(a) {
            this.addPlotBandOrLine(a,
                "plotLines");
        },
        addPlotBandOrLine: function(a, b) {
            var c = (new Gb(this, a)).render(), d = this.userOptions;
            c && (b && (d[b] = d[b] || [], d[b].push(a)), this.plotLinesAndBands.push(c));
            return c;
        },
        autoLabelAlign: function(a) {
            a = (o(a, 0) - this.side * 90 + 720) % 360;
            return a > 15 && a < 165 ? "right" : a > 195 && a < 345 ? "left" : "center";
        },
        getOffset: function() {
            var a = this, b = a.chart, c = b.renderer, d = a.options, e = a.tickPositions, f = a.ticks, g = a.horiz, h = a.side, i = b.inverted ? [1, 0, 3, 2][h] : h, k, j = 0, l, m = 0, p = d.title, n = d.labels, ea = 0, w = b.axisOffset, y = b.clipOffset,
                T = [-1, 1, 1, -1][h], t, u = 1, x = o(n.maxStaggerLines, 5), A, F, Aa, I;
            a.hasData = k = a.hasVisibleSeries || r(a.min) && r(a.max) && !!e;
            a.showAxis = b = k || o(d.showEmpty, !0);
            a.staggerLines = a.horiz && n.staggerLines;
            if (!a.axisGroup) a.gridGroup = c.g("grid").attr({ zIndex: d.gridZIndex || 1 }).add(), a.axisGroup = c.g("axis").attr({ zIndex: d.zIndex || 2 }).add(), a.labelGroup = c.g("axis-labels").attr({ zIndex: n.zIndex || 7 }).add();
            if (k || a.isLinked) {
                a.labelAlign = o(n.align || a.autoLabelAlign(n.rotation));
                q(e, function(b) {
                    f[b] ? f[b].addLabel() : f[b] = new bb(a,
                        b);
                });
                if (a.horiz && !a.staggerLines && x && !n.rotation) {
                    for (t = a.reversed ? [].concat(e).reverse() : e; u < x;) {
                        k = [];
                        A = !1;
                        for (n = 0; n < t.length; n++) F = t[n], Aa = (Aa = f[F].label && f[F].label.getBBox()) ? Aa.width : 0, I = n % u, Aa && (F = a.translate(F), k[I] !== s && F < k[I] && (A = !0), k[I] = F + Aa);
                        if (A) u++;
                        else break;
                    }
                    if (u > 1) a.staggerLines = u;
                }
                q(e, function(b) { if (h === 0 || h === 2 || { 1: "left", 3: "right" }[h] === a.labelAlign) ea = v(f[b].getLabelSize(), ea); });
                if (a.staggerLines) ea *= a.staggerLines, a.labelOffset = ea;
            } else for (t in f) f[t].destroy(), delete f[t];
            if (p &&
                p.text && p.enabled !== !1) {
                if (!a.axisTitle) a.axisTitle = c.text(p.text, 0, 0, p.useHTML).attr({ zIndex: 7, rotation: p.rotation || 0, align: p.textAlign || { low: "left", middle: "center", high: "right" }[p.align] }).css(p.style).add(a.axisGroup), a.axisTitle.isNew = !0;
                if (b) j = a.axisTitle.getBBox()[g ? "height" : "width"], m = o(p.margin, g ? 5 : 10), l = p.offset;
                a.axisTitle[b ? "show" : "hide"]();
            }
            a.offset = T * o(d.offset, w[h]);
            a.axisTitleMargin = o(l, ea + m + (h !== 2 && ea && T * d.labels[g ? "y" : "x"]));
            w[h] = v(w[h], a.axisTitleMargin + j + T * a.offset);
            y[i] = v(y[i],
                O(d.lineWidth / 2) * 2);
        },
        getLinePath: function(a) {
            var b = this.chart, c = this.opposite, d = this.offset, e = this.horiz, f = this.left + (c ? this.width : 0) + d, d = b.chartHeight - this.bottom - (c ? this.height : 0) + d;
            c && (a *= -1);
            return b.renderer.crispLine(["M", e ? this.left : f, e ? d : this.top, "L", e ? b.chartWidth - this.right : f, e ? d : b.chartHeight - this.bottom], a);
        },
        getTitlePosition: function() {
            var a = this.horiz, b = this.left, c = this.top, d = this.len, e = this.options.title, f = a ? b : c, g = this.opposite, h = this.offset, i = B(e.style.fontSize || 12), d = {
                low: f + (a ? 0 : d),
                middle: f + d / 2,
                high: f + (a ? d : 0)
            }[e.align], b = (a ? c + this.height : b) + (a ? 1 : -1) * (g ? -1 : 1) * this.axisTitleMargin + (this.side === 2 ? i : 0);
            return { x: a ? d : b + (g ? this.width : 0) + h + (e.x || 0), y: a ? b - (g ? this.height : 0) + h : d + (e.y || 0) };
        },
        render: function() {
            var a = this, b = a.chart, c = b.renderer, d = a.options, e = a.isLog, f = a.isLinked, g = a.tickPositions, h = a.axisTitle, i = a.stacks, k = a.ticks, j = a.minorTicks, l = a.alternateBands, m = d.stackLabels, p = d.alternateGridColor, n = a.tickmarkOffset, o = d.lineWidth, w, y = b.hasRendered && r(a.oldMin) && !isNaN(a.oldMin);
            w = a.hasData;
            var v = a.showAxis, t, u;
            q([k, j, l], function(a) { for (var b in a) a[b].isActive = !1; });
            if (w || f)
                if (a.minorTickInterval && !a.categories && q(a.getMinorTickPositions(), function(b) {
                    j[b] || (j[b] = new bb(a, b, "minor"));
                    y && j[b].isNew && j[b].render(null, !0);
                    j[b].render(null, !1, 1);
                }), g.length && (q(g.slice(1).concat([g[0]]), function(b, c) {
                    c = c === g.length - 1 ? 0 : c + 1;
                    if (!f || b >= a.min && b <= a.max) k[b] || (k[b] = new bb(a, b)), y && k[b].isNew && k[b].render(c, !0), k[b].render(c, !1, 1);
                }), n && a.min === 0 && (k[-1] || (k[-1] = new bb(a, -1, null, !0)), k[-1].render(-1))),
                p && q(g, function(b, c) { if (c % 2 === 0 && b < a.max) l[b] || (l[b] = new Gb(a)), t = b + n, u = g[c + 1] !== s ? g[c + 1] + n : a.max, l[b].options = { from: e ? la(t) : t, to: e ? la(u) : u, color: p }, l[b].render(), l[b].isActive = !0; }), !a._addedPlotLB) q((d.plotLines || []).concat(d.plotBands || []), function(b) { a.addPlotBandOrLine(b); }), a._addedPlotLB = !0;
            q([k, j, l], function(a) {
                var c, d, e = [], f = Ua ? Ua.duration || 500 : 0, g = function() { for (d = e.length; d--;) a[e[d]] && !a[e[d]].isActive && (a[e[d]].destroy(), delete a[e[d]]); };
                for (c in a)
                    if (!a[c].isActive)
                        a[c].render(c, !1, 0),
                        a[c].isActive = !1, e.push(c);
                a === l || !b.hasRendered || !f ? g() : f && setTimeout(g, f);
            });
            if (o) w = a.getLinePath(o), a.axisLine ? a.axisLine.animate({ d: w }) : a.axisLine = c.path(w).attr({ stroke: d.lineColor, "stroke-width": o, zIndex: 7 }).add(a.axisGroup), a.axisLine[v ? "show" : "hide"]();
            if (h && v) h[h.isNew ? "attr" : "animate"](a.getTitlePosition()), h.isNew = !1;
            if (m && m.enabled) {
                var x, A, d = a.stackTotalGroup;
                if (!d) a.stackTotalGroup = d = c.g("stack-labels").attr({ visibility: "visible", zIndex: 6 }).add();
                d.translate(b.plotLeft, b.plotTop);
                for (x in i)
                    for (A in c =
                        i[x], c) c[A].render(d);
            }
            a.isDirty = !1;
        },
        removePlotBandOrLine: function(a) {
            for (var b = this.plotLinesAndBands, c = this.options, d = this.userOptions, e = b.length; e--;) b[e].id === a && b[e].destroy();
            q([c.plotLines || [], d.plotLines || [], c.plotBands || [], d.plotBands || []], function(b) { for (e = b.length; e--;) b[e].id === a && ma(b, b[e]); });
        },
        setTitle: function(a, b) { this.update({ title: a }, b); },
        redraw: function() {
            var a = this.chart.pointer;
            a.reset && a.reset(!0);
            this.render();
            q(this.plotLinesAndBands, function(a) { a.render(); });
            q(this.series, function(a) {
                a.isDirty =
                    !0;
            });
        },
        buildStacks: function() {
            var a = this.series, b = a.length;
            if (!this.isXAxis) {
                for (; b--;) a[b].setStackedPoints();
                if (this.usePercentage) for (b = 0; b < a.length; b++) a[b].setPercentStacks();
            }
        },
        setCategories: function(a, b) { this.update({ categories: a }, b); },
        destroy: function(a) {
            var b = this, c = b.stacks, d, e = b.plotLinesAndBands;
            a || U(b);
            for (d in c) Ea(c[d]), c[d] = null;
            q([b.ticks, b.minorTicks, b.alternateBands], function(a) { Ea(a); });
            for (a = e.length; a--;) e[a].destroy();
            q("stackTotalGroup,axisLine,axisGroup,gridGroup,labelGroup,axisTitle".split(","),
                function(a) { b[a] && (b[a] = b[a].destroy()); });
        }
    };
    Hb.prototype = {
        init: function(a, b) {
            var c = b.borderWidth, d = b.style, e = B(d.padding);
            this.chart = a;
            this.options = b;
            this.crosshairs = [];
            this.now = { x: 0, y: 0 };
            this.isHidden = !0;
            this.label = a.renderer.label("", 0, 0, b.shape, null, null, b.useHTML, null, "tooltip").attr({ padding: e, fill: b.backgroundColor, "stroke-width": c, r: b.borderRadius, zIndex: 8 }).css(d).css({ padding: 0 }).add().attr({ y: -999 });
            ia || this.label.shadow(b.shadow);
            this.shared = b.shared;
        },
        destroy: function() {
            q(this.crosshairs,
                function(a) { a && a.destroy(); });
            if (this.label) this.label = this.label.destroy();
            clearTimeout(this.hideTimer);
            clearTimeout(this.tooltipTimeout);
        },
        move: function(a, b, c, d) {
            var e = this, f = e.now, g = e.options.animation !== !1 && !e.isHidden;
            x(f, { x: g ? (2 * f.x + a) / 3 : a, y: g ? (f.y + b) / 2 : b, anchorX: g ? (2 * f.anchorX + c) / 3 : c, anchorY: g ? (f.anchorY + d) / 2 : d });
            e.label.attr(f);
            if (g && (M(a - f.x) > 1 || M(b - f.y) > 1)) clearTimeout(this.tooltipTimeout), this.tooltipTimeout = setTimeout(function() { e && e.move(a, b, c, d); }, 32);
        },
        hide: function() {
            var a = this, b;
            clearTimeout(this.hideTimer);
            if (!this.isHidden)
                b = this.chart.hoverPoints, this.hideTimer = setTimeout(function() {
                    a.label.fadeOut();
                    a.isHidden = !0;
                }, o(this.options.hideDelay, 500)), b && q(b, function(a) { a.setState(); }), this.chart.hoverPoints = null;
        },
        hideCrosshairs: function() { q(this.crosshairs, function(a) { a && a.hide(); }); },
        getAnchor: function(a, b) {
            var c, d = this.chart, e = d.inverted, f = d.plotTop, g = 0, h = 0, i, a = fa(a);
            c = a[0].tooltipPos;
            this.followPointer && b && (b.chartX === s && (b = d.pointer.normalize(b)), c = [b.chartX - d.plotLeft, b.chartY - f]);
            c || (q(a, function(a) {
                i =
                    a.series.yAxis;
                g += a.plotX;
                h += (a.plotLow ? (a.plotLow + a.plotHigh) / 2 : a.plotY) + (!e && i ? i.top - f : 0);
            }), g /= a.length, h /= a.length, c = [e ? d.plotWidth - h : g, this.shared && !e && a.length > 1 && b ? b.chartY - f : e ? d.plotHeight - g : h]);
            return Ja(c, t);
        },
        getPosition: function(a, b, c) {
            var d = this.chart, e = d.plotLeft, f = d.plotTop, g = d.plotWidth, h = d.plotHeight, i = o(this.options.distance, 12), k = c.plotX, c = c.plotY, d = k + e + (d.inverted ? i : -a - i), j = c - b + f + 15, l;
            d < 7 && (d = e + v(k, 0) + i);
            d + a > e + g && (d -= d + a - (e + g), j = c - b + f - i, l = !0);
            j < f + 5 && (j = f + 5, l && c >= j && c <= j + b && (j = c +
                f + i));
            j + b > f + h && (j = v(f, f + h - b - i));
            return { x: d, y: j };
        },
        defaultFormatter: function(a) {
            var b = this.points || fa(this), c = b[0].series, d;
            d = [c.tooltipHeaderFormatter(b[0])];
            q(b, function(a) {
                c = a.series;
                d.push(c.tooltipFormatter && c.tooltipFormatter(a) || a.point.tooltipFormatter(c.tooltipOptions.pointFormat));
            });
            d.push(a.options.footerFormat || "");
            return d.join("");
        },
        refresh: function(a, b) {
            var c = this.chart, d = this.label, e = this.options, f, g, h = {}, i, k = [];
            i = e.formatter || this.defaultFormatter;
            var h = c.hoverPoints, j, l = e.crosshairs,
                m = this.shared;
            clearTimeout(this.hideTimer);
            this.followPointer = fa(a)[0].series.tooltipOptions.followPointer;
            g = this.getAnchor(a, b);
            f = g[0];
            g = g[1];
            m && (!a.series || !a.series.noSharedTooltip) ? (c.hoverPoints = a, h && q(h, function(a) { a.setState(); }), q(a, function(a) {
                a.setState("hover");
                k.push(a.getLabelConfig());
            }), h = { x: a[0].category, y: a[0].y }, h.points = k, a = a[0]) : h = a.getLabelConfig();
            i = i.call(h, this);
            h = a.series;
            i === !1 ? this.hide() : (this.isHidden && (jb(d), d.attr("opacity", 1).show()), d.attr({ text: i }), j = e.borderColor ||
                a.color || h.color || "#606060", d.attr({ stroke: j }), this.updatePosition({ plotX: f, plotY: g }), this.isHidden = !1);
            if (l) {
                l = fa(l);
                for (d = l.length; d--;)
                    if (m = a.series, e = m[d ? "yAxis" : "xAxis"], l[d] && e)
                        if (h = d ? o(a.stackY, a.y) : a.x, e.isLog && (h = ta(h)), d === 1 && m.modifyValue && (h = m.modifyValue(h)), e = e.getPlotLinePath(h, 1), this.crosshairs[d]) this.crosshairs[d].attr({ d: e, visibility: "visible" });
                        else {
                            h = { "stroke-width": l[d].width || 1, stroke: l[d].color || "#C0C0C0", zIndex: l[d].zIndex || 2 };
                            if (l[d].dashStyle) h.dashstyle = l[d].dashStyle;
                            this.crosshairs[d] = c.renderer.path(e).attr(h).add();
                        }
            }
            L(c, "tooltipRefresh", { text: i, x: f + c.plotLeft, y: g + c.plotTop, borderColor: j });
        },
        updatePosition: function(a) {
            var b = this.chart, c = this.label, c = (this.options.positioner || this.getPosition).call(this, c.width, c.height, a);
            this.move(t(c.x), t(c.y), a.plotX + b.plotLeft, a.plotY + b.plotTop);
        }
    };
    rb.prototype = {
        init: function(a, b) {
            var c = b.chart, d = c.events, e = ia ? "" : c.zoomType, c = a.inverted, f;
            this.options = b;
            this.chart = a;
            this.zoomX = f = /x/.test(e);
            this.zoomY = e = /y/.test(e);
            this.zoomHor =
                f && !c || e && c;
            this.zoomVert = e && !c || f && c;
            this.runChartClick = d && !!d.click;
            this.pinchDown = [];
            this.lastValidTouch = {};
            if (b.tooltip.enabled) a.tooltip = new Hb(a, b.tooltip);
            this.setDOMEvents();
        },
        normalize: function(a, b) {
            var c, d, a = a || W.event;
            if (!a.target) a.target = a.srcElement;
            a = ec(a);
            d = a.touches ? a.touches.item(0) : a;
            if (!b) this.chartPosition = b = dc(this.chart.container);
            d.pageX === s ? (c = v(a.x, a.clientX - b.left), d = a.y) : (c = d.pageX - b.left, d = d.pageY - b.top);
            return x(a, { chartX: t(c), chartY: t(d) });
        },
        getCoordinates: function(a) {
            var b =
                { xAxis: [], yAxis: [] };
            q(this.chart.axes, function(c) { b[c.isXAxis ? "xAxis" : "yAxis"].push({ axis: c, value: c.toValue(a[c.horiz ? "chartX" : "chartY"]) }); });
            return b;
        },
        getIndex: function(a) {
            var b = this.chart;
            return b.inverted ? b.plotHeight + b.plotTop - a.chartY : a.chartX - b.plotLeft;
        },
        runPointActions: function(a) {
            var b = this.chart, c = b.series, d = b.tooltip, e, f = b.hoverPoint, g = b.hoverSeries, h, i, k = b.chartWidth, j = this.getIndex(a);
            if (d && this.options.tooltip.shared && (!g || !g.noSharedTooltip)) {
                e = [];
                h = c.length;
                for (i = 0; i < h; i++)
                    if (c[i].visible &&
                        c[i].options.enableMouseTracking !== !1 && !c[i].noSharedTooltip && c[i].tooltipPoints.length && (b = c[i].tooltipPoints[j]) && b.series) b._dist = M(j - b.clientX), k = z(k, b._dist), e.push(b);
                for (h = e.length; h--;) e[h]._dist > k && e.splice(h, 1);
                if (e.length && e[0].clientX !== this.hoverX) d.refresh(e, a), this.hoverX = e[0].clientX;
            }
            if (g && g.tracker) {
                if ((b = g.tooltipPoints[j]) && b !== f) b.onMouseOver(a);
            } else d && d.followPointer && !d.isHidden && (a = d.getAnchor([{}], a), d.updatePosition({ plotX: a[0], plotY: a[1] }));
        },
        reset: function(a) {
            var b = this.chart,
                c = b.hoverSeries, d = b.hoverPoint, e = b.tooltip, b = e && e.shared ? b.hoverPoints : d;
            (a = a && e && b) && fa(b)[0].plotX === s && (a = !1);
            if (a) e.refresh(b);
            else {
                if (d) d.onMouseOut();
                if (c) c.onMouseOut();
                e && (e.hide(), e.hideCrosshairs());
                this.hoverX = null;
            }
        },
        scaleGroups: function(a, b) {
            var c = this.chart, d;
            q(c.series, function(e) {
                d = a || e.getPlotBox();
                e.xAxis && e.xAxis.zoomEnabled && (e.group.attr(d), e.markerGroup && (e.markerGroup.attr(d), e.markerGroup.clip(b ? c.clipRect : null)), e.dataLabelsGroup && e.dataLabelsGroup.attr(d));
            });
            c.clipRect.attr(b ||
                c.clipBox);
        },
        pinchTranslate: function(a, b, c, d, e, f, g, h) {
            a && this.pinchTranslateDirection(!0, c, d, e, f, g, h);
            b && this.pinchTranslateDirection(!1, c, d, e, f, g, h);
        },
        pinchTranslateDirection: function(a, b, c, d, e, f, g, h) {
            var i = this.chart, k = a ? "x" : "y", j = a ? "X" : "Y", l = "chart" + j, m = a ? "width" : "height", p = i["plot" + (a ? "Left" : "Top")], n, o, w = h || 1, q = i.inverted, v = i.bounds[a ? "h" : "v"], t = b.length === 1, s = b[0][l], r = c[0][l], A = !t && b[1][l], u = !t && c[1][l], x, c = function() {
                !t && M(s - A) > 20 && (w = h || M(r - u) / M(s - A));
                o = (p - r) / w + s;
                n = i["plot" + (a ? "Width" : "Height")] /
                    w;
            };
            c();
            b = o;
            b < v.min ? (b = v.min, x = !0) : b + n > v.max && (b = v.max - n, x = !0);
            x ? (r -= 0.8 * (r - g[k][0]), t || (u -= 0.8 * (u - g[k][1])), c()) : g[k] = [r, u];
            q || (f[k] = o - p, f[m] = n);
            f = q ? 1 / w : w;
            e[m] = n;
            e[k] = b;
            d[q ? a ? "scaleY" : "scaleX" : "scale" + j] = w;
            d["translate" + j] = f * p + (r - f * s);
        },
        pinch: function(a) {
            var b = this, c = b.chart, d = b.pinchDown, e = c.tooltip && c.tooltip.options.followTouchMove, f = a.touches, g = f.length, h = b.lastValidTouch, i = b.zoomHor || b.pinchHor, k = b.zoomVert || b.pinchVert, j = i || k, l = b.selectionMarker, m = {}, p = g === 1 && (b.inClass(a.target, "highcharts-tracker") &&
                c.runTrackerClick || c.runChartClick), n = {};
            (j || e) && !p && a.preventDefault();
            Ja(f, function(a) { return b.normalize(a); });
            if (a.type === "touchstart")
                q(f, function(a, b) { d[b] = { chartX: a.chartX, chartY: a.chartY }; }), h.x = [d[0].chartX, d[1] && d[1].chartX], h.y = [d[0].chartY, d[1] && d[1].chartY], q(c.axes, function(a) {
                    if (a.zoomEnabled) {
                        var b = c.bounds[a.horiz ? "h" : "v"], d = a.minPixelPadding, e = a.toPixels(a.dataMin), f = a.toPixels(a.dataMax), g = z(e, f), e = v(e, f);
                        b.min = z(a.pos, g - d);
                        b.max = v(a.pos + a.len, e + d);
                    }
                });
            else if (d.length) {
                if (!l)
                    b.selectionMarker =
                        l = x({ destroy: pa }, c.plotBox);
                b.pinchTranslate(i, k, d, f, m, l, n, h);
                b.hasPinched = j;
                b.scaleGroups(m, n);
                !j && e && g === 1 && this.runPointActions(b.normalize(a));
            }
        },
        dragStart: function(a) {
            var b = this.chart;
            b.mouseIsDown = a.type;
            b.cancelClick = !1;
            b.mouseDownX = this.mouseDownX = a.chartX;
            b.mouseDownY = this.mouseDownY = a.chartY;
        },
        drag: function(a) {
            var b = this.chart, c = b.options.chart, d = a.chartX, e = a.chartY, f = this.zoomHor, g = this.zoomVert, h = b.plotLeft, i = b.plotTop, k = b.plotWidth, j = b.plotHeight, l, m = this.mouseDownX, p = this.mouseDownY;
            d <
                h ? d = h : d > h + k && (d = h + k);
            e < i ? e = i : e > i + j && (e = i + j);
            this.hasDragged = Math.sqrt(Math.pow(m - d, 2) + Math.pow(p - e, 2));
            if (this.hasDragged > 10) {
                l = b.isInsidePlot(m - h, p - i);
                if (b.hasCartesianSeries && (this.zoomX || this.zoomY) && l && !this.selectionMarker) this.selectionMarker = b.renderer.rect(h, i, f ? 1 : k, g ? 1 : j, 0).attr({ fill: c.selectionMarkerFill || "rgba(69,114,167,0.25)", zIndex: 7 }).add();
                this.selectionMarker && f && (d -= m, this.selectionMarker.attr({ width: M(d), x: (d > 0 ? 0 : d) + m }));
                this.selectionMarker && g && (d = e - p, this.selectionMarker.attr({
                    height: M(d),
                    y: (d > 0 ? 0 : d) + p
                }));
                l && !this.selectionMarker && c.panning && b.pan(a, c.panning);
            }
        },
        drop: function(a) {
            var b = this.chart, c = this.hasPinched;
            if (this.selectionMarker) {
                var d = { xAxis: [], yAxis: [], originalEvent: a.originalEvent || a }, e = this.selectionMarker, f = e.x, g = e.y, h;
                if (this.hasDragged || c)
                    q(b.axes, function(a) {
                        if (a.zoomEnabled) {
                            var b = a.horiz, c = a.toValue(b ? f : g), b = a.toValue(b ? f + e.width : g + e.height);
                            !isNaN(c) && !isNaN(b) && (d[a.xOrY + "Axis"].push({ axis: a, min: z(c, b), max: v(c, b) }), h = !0);
                        }
                    }), h && L(b, "selection", d, function(a) {
                        b.zoom(x(a,
                            c ? { animation: !1 } : null));
                    });
                this.selectionMarker = this.selectionMarker.destroy();
                c && this.scaleGroups();
            }
            if (b) G(b.container, { cursor: b._cursor }), b.cancelClick = this.hasDragged > 10, b.mouseIsDown = this.hasDragged = this.hasPinched = !1, this.pinchDown = [];
        },
        onContainerMouseDown: function(a) {
            a = this.normalize(a);
            a.preventDefault && a.preventDefault();
            this.dragStart(a);
        },
        onDocumentMouseUp: function(a) { this.drop(a); },
        onDocumentMouseMove: function(a) {
            var b = this.chart, c = this.chartPosition, d = b.hoverSeries, a = this.normalize(a, c);
            c && d && !this.inClass(a.target, "highcharts-tracker") && !b.isInsidePlot(a.chartX - b.plotLeft, a.chartY - b.plotTop) && this.reset();
        },
        onContainerMouseLeave: function() {
            this.reset();
            this.chartPosition = null;
        },
        onContainerMouseMove: function(a) {
            var b = this.chart, a = this.normalize(a);
            a.returnValue = !1;
            b.mouseIsDown === "mousedown" && this.drag(a);
            (this.inClass(a.target, "highcharts-tracker") || b.isInsidePlot(a.chartX - b.plotLeft, a.chartY - b.plotTop)) && !b.openMenu && this.runPointActions(a);
        },
        inClass: function(a, b) {
            for (var c; a;) {
                if (c =
                    C(a, "class"))
                    if (c.indexOf(b) !== -1) return !0;
                    else if (c.indexOf("highcharts-container") !== -1) return !1;
                a = a.parentNode;
            }
        },
        onTrackerMouseOut: function(a) {
            var b = this.chart.hoverSeries;
            if (b && !b.options.stickyTracking && !this.inClass(a.toElement || a.relatedTarget, "highcharts-tooltip")) b.onMouseOut();
        },
        onContainerClick: function(a) {
            var b = this.chart, c = b.hoverPoint, d = b.plotLeft, e = b.plotTop, f = b.inverted, g, h, i, a = this.normalize(a);
            a.cancelBubble = !0;
            if (!b.cancelClick)
                c && this.inClass(a.target, "highcharts-tracker") ? (g = this.chartPosition,
                h = c.plotX, i = c.plotY, x(c, { pageX: g.left + d + (f ? b.plotWidth - i : h), pageY: g.top + e + (f ? b.plotHeight - h : i) }), L(c.series, "click", x(a, { point: c })), b.hoverPoint && c.firePointEvent("click", a)) : (x(a, this.getCoordinates(a)), b.isInsidePlot(a.chartX - d, a.chartY - e) && L(b, "click", a));
        },
        onContainerTouchStart: function(a) {
            var b = this.chart;
            a.touches.length === 1 ? (a = this.normalize(a), b.isInsidePlot(a.chartX - b.plotLeft, a.chartY - b.plotTop) ? (this.runPointActions(a), this.pinch(a)) : this.reset()) : a.touches.length === 2 && this.pinch(a);
        },
        onContainerTouchMove: function(a) {
            (a.touches.length ===
                1 || a.touches.length === 2) && this.pinch(a);
        },
        onDocumentTouchEnd: function(a) { this.drop(a); },
        setDOMEvents: function() {
            var a = this, b = a.chart.container, c;
            this._events = c = [[b, "onmousedown", "onContainerMouseDown"], [b, "onmousemove", "onContainerMouseMove"], [b, "onclick", "onContainerClick"], [b, "mouseleave", "onContainerMouseLeave"], [J, "mousemove", "onDocumentMouseMove"], [J, "mouseup", "onDocumentMouseUp"]];
            ib && c.push([b, "ontouchstart", "onContainerTouchStart"], [b, "ontouchmove", "onContainerTouchMove"], [J, "touchend", "onDocumentTouchEnd"]);
            q(c, function(b) {
                a["_" + b[2]] = function(c) { a[b[2]](c); };
                b[1].indexOf("on") === 0 ? b[0][b[1]] = a["_" + b[2]] : E(b[0], b[1], a["_" + b[2]]);
            });
        },
        destroy: function() {
            var a = this;
            q(a._events, function(b) { b[1].indexOf("on") === 0 ? b[0][b[1]] = null : U(b[0], b[1], a["_" + b[2]]); });
            delete a._events;
            clearInterval(a.tooltipTimeout);
        }
    };
    sb.prototype = {
        init: function(a, b) {
            var c = this, d = b.itemStyle, e = o(b.padding, 8), f = b.itemMarginTop || 0;
            this.options = b;
            if (b.enabled)
                c.baseline = B(d.fontSize) + 3 + f, c.itemStyle = d, c.itemHiddenStyle = u(d, b.itemHiddenStyle),
                c.itemMarginTop = f, c.padding = e, c.initialItemX = e, c.initialItemY = e - 5, c.maxItemWidth = 0, c.chart = a, c.itemHeight = 0, c.lastLineHeight = 0, c.render(), E(c.chart, "endResize", function() { c.positionCheckboxes(); });
        },
        colorizeItem: function(a, b) {
            var c = this.options, d = a.legendItem, e = a.legendLine, f = a.legendSymbol, g = this.itemHiddenStyle.color, c = b ? c.itemStyle.color : g, h = b ? a.color : g, g = a.options && a.options.marker, i = { stroke: h, fill: h }, k;
            d && d.css({ fill: c, color: c });
            e && e.attr({ stroke: h });
            if (f) {
                if (g && f.isMarker)
                    for (k in g = a.convertAttribs(g),
                    g) d = g[k], d !== s && (i[k] = d);
                f.attr(i);
            }
        },
        positionItem: function(a) {
            var b = this.options, c = b.symbolPadding, b = !b.rtl, d = a._legendItemPos, e = d[0], d = d[1], f = a.checkbox;
            a.legendGroup && a.legendGroup.translate(b ? e : this.legendWidth - e - 2 * c - 4, d);
            if (f) f.x = e, f.y = d;
        },
        destroyItem: function(a) {
            var b = a.checkbox;
            q(["legendItem", "legendLine", "legendSymbol", "legendGroup"], function(b) { a[b] && (a[b] = a[b].destroy()); });
            b && $a(a.checkbox);
        },
        destroy: function() {
            var a = this.group, b = this.box;
            if (b) this.box = b.destroy();
            if (a) this.group = a.destroy();
        },
        positionCheckboxes: function(a) {
            var b = this.group.alignAttr, c, d = this.clipHeight || this.legendHeight;
            if (b)
                c = b.translateY, q(this.allItems, function(e) {
                    var f = e.checkbox, g;
                    f && (g = c + f.y + (a || 0) + 3, G(f, { left: b.translateX + e.legendItemWidth + f.x - 20 + "px", top: g + "px", display: g > c - 6 && g < c + d - 6 ? "" : Z }));
                });
        },
        renderTitle: function() {
            var a = this.padding, b = this.options.title, c = 0;
            if (b.text) {
                if (!this.title) this.title = this.chart.renderer.label(b.text, a - 3, a - 4, null, null, null, null, null, "legend-title").attr({ zIndex: 1 }).css(b.style).add(this.group);
                a = this.title.getBBox();
                c = a.height;
                this.offsetWidth = a.width;
                this.contentGroup.attr({ translateY: c });
            }
            this.titleHeight = c;
        },
        renderItem: function(a) {
            var A;
            var b = this, c = b.chart, d = c.renderer, e = b.options, f = e.layout === "horizontal", g = e.symbolWidth, h = e.symbolPadding, i = b.itemStyle, k = b.itemHiddenStyle, j = b.padding, l = f ? o(e.itemDistance, 8) : 0, m = !e.rtl, p = e.width, n = e.itemMarginBottom || 0, q = b.itemMarginTop, w = b.initialItemX, y = a.legendItem, t = a.series || a, s = t.options, r = s.showCheckbox, x = e.useHTML;
            if (!y && (a.legendGroup = d.g("legend-item").attr({ zIndex: 1 }).add(b.scrollGroup),
            t.drawLegendSymbol(b, a), a.legendItem = y = d.text(e.labelFormat ? Oa(e.labelFormat, a) : e.labelFormatter.call(a), m ? g + h : -h, b.baseline, x).css(u(a.visible ? i : k)).attr({ align: m ? "left" : "right", zIndex: 2 }).add(a.legendGroup), (x ? y : a.legendGroup).on("mouseover", function() {
                a.setState("hover");
                y.css(b.options.itemHoverStyle);
            }).on("mouseout", function() {
                y.css(a.visible ? i : k);
                a.setState();
            }).on("click", function(b) {
                var c = function() { a.setVisible(); }, b = { browserEvent: b };
                a.firePointEvent ? a.firePointEvent("legendItemClick", b, c) :
                    L(a, "legendItemClick", b, c);
            }), b.colorizeItem(a, a.visible), s && r)) a.checkbox = Y("input", { type: "checkbox", checked: a.selected, defaultChecked: a.selected }, e.itemCheckboxStyle, c.container), E(a.checkbox, "click", function(b) { L(a, "checkboxClick", { checked: b.target.checked }, function() { a.select(); }); });
            d = y.getBBox();
            A = a.legendItemWidth = e.itemWidth || g + h + d.width + l + (r ? 20 : 0), e = A;
            b.itemHeight = g = d.height;
            if (f && b.itemX - w + e > (p || c.chartWidth - 2 * j - w)) b.itemX = w, b.itemY += q + b.lastLineHeight + n, b.lastLineHeight = 0;
            b.maxItemWidth = v(b.maxItemWidth,
                e);
            b.lastItemY = q + b.itemY + n;
            b.lastLineHeight = v(g, b.lastLineHeight);
            a._legendItemPos = [b.itemX, b.itemY];
            f ? b.itemX += e : (b.itemY += q + g + n, b.lastLineHeight = g);
            b.offsetWidth = p || v((f ? b.itemX - w - l : e) + j, b.offsetWidth);
        },
        render: function() {
            var a = this, b = a.chart, c = b.renderer, d = a.group, e, f, g, h, i = a.box, k = a.options, j = a.padding, l = k.borderWidth, m = k.backgroundColor;
            a.itemX = a.initialItemX;
            a.itemY = a.initialItemY;
            a.offsetWidth = 0;
            a.lastItemY = 0;
            if (!d)
                a.group = d = c.g("legend").attr({ zIndex: 7 }).add(), a.contentGroup = c.g().attr({ zIndex: 1 }).add(d),
                a.scrollGroup = c.g().add(a.contentGroup);
            a.renderTitle();
            e = [];
            q(b.series, function(a) {
                var b = a.options;
                if (o(b.showInLegend, b.linkedTo === s ? s : !1, !0)) e = e.concat(a.legendItems || (b.legendType === "point" ? a.data : a));
            });
            Sb(e, function(a, b) { return (a.options && a.options.legendIndex || 0) - (b.options && b.options.legendIndex || 0); });
            k.reversed && e.reverse();
            a.allItems = e;
            a.display = f = !!e.length;
            q(e, function(b) { a.renderItem(b); });
            g = k.width || a.offsetWidth;
            h = a.lastItemY + a.lastLineHeight + a.titleHeight;
            h = a.handleOverflow(h);
            if (l ||
                m) {
                g += j;
                h += j;
                if (i) {
                    if (g > 0 && h > 0) i[i.isNew ? "attr" : "animate"](i.crisp(null, null, null, g, h)), i.isNew = !1;
                } else a.box = i = c.rect(0, 0, g, h, k.borderRadius, l || 0).attr({ stroke: k.borderColor, "stroke-width": l || 0, fill: m || Z }).add(d).shadow(k.shadow), i.isNew = !0;
                i[f ? "show" : "hide"]();
            }
            a.legendWidth = g;
            a.legendHeight = h;
            q(e, function(b) { a.positionItem(b); });
            f && d.align(x({ width: g, height: h }, k), !0, "spacingBox");
            b.isResizing || this.positionCheckboxes();
        },
        handleOverflow: function(a) {
            var b = this, c = this.chart, d = c.renderer, e = this.options,
                f = e.y, f = c.spacingBox.height + (e.verticalAlign === "top" ? -f : f) - this.padding, g = e.maxHeight, h = this.clipRect, i = e.navigation, k = o(i.animation, !0), j = i.arrowSize || 12, l = this.nav;
            e.layout === "horizontal" && (f /= 2);
            g && (f = z(f, g));
            if (a > f && !e.useHTML) {
                this.clipHeight = c = f - 20 - this.titleHeight;
                this.pageCount = Ga(a / c);
                this.currentPage = o(this.currentPage, 1);
                this.fullHeight = a;
                if (!h) h = b.clipRect = d.clipRect(0, 0, 9999, 0), b.contentGroup.clip(h);
                h.attr({ height: c });
                if (!l)
                    this.nav = l = d.g().attr({ zIndex: 1 }).add(this.group), this.up = d.symbol("triangle",
                        0, 0, j, j).on("click", function() { b.scroll(-1, k); }).add(l), this.pager = d.text("", 15, 10).css(i.style).add(l), this.down = d.symbol("triangle-down", 0, 0, j, j).on("click", function() { b.scroll(1, k); }).add(l);
                b.scroll(0);
                a = f;
            } else if (l) h.attr({ height: c.chartHeight }), l.hide(), this.scrollGroup.attr({ translateY: 1 }), this.clipHeight = 0;
            return a;
        },
        scroll: function(a, b) {
            var c = this.pageCount, d = this.currentPage + a, e = this.clipHeight, f = this.options.navigation, g = f.activeColor, h = f.inactiveColor, f = this.pager, i = this.padding;
            d > c && (d =
                c);
            if (d > 0) b !== s && ab(b, this.chart), this.nav.attr({ translateX: i, translateY: e + 7 + this.titleHeight, visibility: "visible" }), this.up.attr({ fill: d === 1 ? h : g }).css({ cursor: d === 1 ? "default" : "pointer" }), f.attr({ text: d + "/" + this.pageCount }), this.down.attr({ x: 18 + this.pager.getBBox().width, fill: d === c ? h : g }).css({ cursor: d === c ? "default" : "pointer" }), e = -z(e * (d - 1), this.fullHeight - e + i) + 1, this.scrollGroup.animate({ translateY: e }), f.attr({ text: d + "/" + c }), this.currentPage = d, this.positionCheckboxes(e);
        }
    };
    /Trident\/7\.0/.test(wa) &&
        ga(sb.prototype, "positionItem", function(a, b) {
            var c = this, d = function() { a.call(c, b); };
            c.chart.renderer.forExport ? d() : setTimeout(d);
        });
    Va.prototype = {
        init: function(a, b) {
            var c, d = a.series;
            a.series = null;
            c = u(K, a);
            c.series = a.series = d;
            d = c.chart;
            this.margin = this.splashArray("margin", d);
            this.spacing = this.splashArray("spacing", d);
            var e = d.events;
            this.bounds = { h: {}, v: {} };
            this.callback = b;
            this.isResizing = 0;
            this.options = c;
            this.axes = [];
            this.series = [];
            this.hasCartesianSeries = d.showAxes;
            var f = this, g;
            f.index = Wa.length;
            Wa.push(f);
            d.reflow !== !1 && E(f, "load", function() { f.initReflow(); });
            if (e) for (g in e) E(f, g, e[g]);
            f.xAxis = [];
            f.yAxis = [];
            f.animation = ia ? !1 : o(d.animation, !0);
            f.pointCount = 0;
            f.counters = new Rb;
            f.firstRender();
        },
        initSeries: function(a) {
            var b = this.options.chart;
            (b = N[a.type || b.type || b.defaultSeriesType]) || ra(17, !0);
            b = new b;
            b.init(this, a);
            return b;
        },
        addSeries: function(a, b, c) {
            var d, e = this;
            a && (b = o(b, !0), L(e, "addSeries", { options: a }, function() {
                d = e.initSeries(a);
                e.isDirtyLegend = !0;
                e.linkSeries();
                b && e.redraw(c);
            }));
            return d;
        },
        addAxis: function(a,
            b, c, d) {
            var e = b ? "xAxis" : "yAxis", f = this.options;
            new oa(this, u(a, { index: this[e].length, isX: b }));
            f[e] = fa(f[e] || {});
            f[e].push(a);
            o(c, !0) && this.redraw(d);
        },
        isInsidePlot: function(a, b, c) {
            var d = c ? b : a, a = c ? a : b;
            return d >= 0 && d <= this.plotWidth && a >= 0 && a <= this.plotHeight;
        },
        adjustTickAmounts: function() {
            this.options.chart.alignTicks !== !1 && q(this.axes, function(a) { a.adjustTickAmount(); });
            this.maxTicks = null;
        },
        redraw: function(a) {
            var b = this.axes, c = this.series, d = this.pointer, e = this.legend, f = this.isDirtyLegend, g, h, i = this.isDirtyBox,
                k = c.length, j = k, l = this.renderer, m = l.isHidden(), p = [];
            ab(a, this);
            m && this.cloneRenderTo();
            for (this.layOutTitles(); j--;)
                if (a = c[j], a.options.stacking && (g = !0, a.isDirty)) {
                    h = !0;
                    break;
                }
            if (h) for (j = k; j--;) if (a = c[j], a.options.stacking) a.isDirty = !0;
            q(c, function(a) { a.isDirty && a.options.legendType === "point" && (f = !0); });
            if (f && e.options.enabled) e.render(), this.isDirtyLegend = !1;
            g && this.getStacks();
            if (this.hasCartesianSeries) {
                if (!this.isResizing) this.maxTicks = null, q(b, function(a) { a.setScale(); });
                this.adjustTickAmounts();
                this.getMargins();
                q(b, function(a) { a.isDirty && (i = !0); });
                q(b, function(a) {
                    if (a.isDirtyExtremes)
                        a.isDirtyExtremes = !1, p.push(function() {
                            L(a, "afterSetExtremes", x(a.eventArgs, a.getExtremes()));
                            delete a.eventArgs;
                        });
                    (i || g) && a.redraw();
                });
            }
            i && this.drawChartBox();
            q(c, function(a) { a.isDirty && a.visible && (!a.isCartesian || a.xAxis) && a.redraw(); });
            d && d.reset && d.reset(!0);
            l.draw();
            L(this, "redraw");
            m && this.cloneRenderTo(!0);
            q(p, function(a) { a.call(); });
        },
        showLoading: function(a) {
            var b = this.options, c = this.loadingDiv, d = b.loading;
            if (!c) this.loadingDiv = c = Y(Ta, { className: "highcharts-loading" }, x(d.style, { zIndex: 10, display: Z }), this.container), this.loadingSpan = Y("span", null, d.labelStyle, c);
            this.loadingSpan.innerHTML = a || b.lang.loading;
            if (!this.loadingShown) G(c, { opacity: 0, display: "", left: this.plotLeft + "px", top: this.plotTop + "px", width: this.plotWidth + "px", height: this.plotHeight + "px" }), Mb(c, { opacity: d.style.opacity }, { duration: d.showDuration || 0 }), this.loadingShown = !0;
        },
        hideLoading: function() {
            var a = this.options, b = this.loadingDiv;
            b && Mb(b,
                { opacity: 0 }, { duration: a.loading.hideDuration || 100, complete: function() { G(b, { display: Z }); } });
            this.loadingShown = !1;
        },
        get: function(a) {
            var b = this.axes, c = this.series, d, e;
            for (d = 0; d < b.length; d++) if (b[d].options.id === a) return b[d];
            for (d = 0; d < c.length; d++) if (c[d].options.id === a) return c[d];
            for (d = 0; d < c.length; d++) {
                e = c[d].points || [];
                for (b = 0; b < e.length; b++) if (e[b].id === a) return e[b];
            }
            return null;
        },
        getAxes: function() {
            var a = this, b = this.options, c = b.xAxis = fa(b.xAxis || {}), b = b.yAxis = fa(b.yAxis || {});
            q(c, function(a, b) {
                a.index =
                    b;
                a.isX = !0;
            });
            q(b, function(a, b) { a.index = b; });
            c = c.concat(b);
            q(c, function(b) { new oa(a, b); });
            a.adjustTickAmounts();
        },
        getSelectedPoints: function() {
            var a = [];
            q(this.series, function(b) { a = a.concat(Fb(b.points || [], function(a) { return a.selected; })); });
            return a;
        },
        getSelectedSeries: function() { return Fb(this.series, function(a) { return a.selected; }); },
        getStacks: function() {
            var a = this;
            q(a.yAxis, function(a) { if (a.stacks && a.hasVisibleSeries) a.oldStacks = a.stacks; });
            q(a.series, function(b) {
                if (b.options.stacking && (b.visible === !0 ||
                    a.options.chart.ignoreHiddenSeries === !1)) b.stackKey = b.type + o(b.options.stack, "");
            });
        },
        showResetZoom: function() {
            var a = this, b = K.lang, c = a.options.chart.resetZoomButton, d = c.theme, e = d.states, f = c.relativeTo === "chart" ? null : "plotBox";
            this.resetZoomButton = a.renderer.button(b.resetZoom, null, null, function() { a.zoomOut(); }, d, e && e.hover).attr({ align: c.position.align, title: b.resetZoomTitle }).add().align(c.position, !1, f);
        },
        zoomOut: function() {
            var a = this;
            L(a, "selection", { resetSelection: !0 }, function() { a.zoom(); });
        },
        zoom: function(a) {
            var b,
                c = this.pointer, d = !1, e;
            !a || a.resetSelection ? q(this.axes, function(a) { b = a.zoom(); }) : q(a.xAxis.concat(a.yAxis), function(a) {
                var e = a.axis, h = e.isXAxis;
                if (c[h ? "zoomX" : "zoomY"] || c[h ? "pinchX" : "pinchY"]) b = e.zoom(a.min, a.max), e.displayBtn && (d = !0);
            });
            e = this.resetZoomButton;
            if (d && !e) this.showResetZoom();
            else if (!d && $(e)) this.resetZoomButton = e.destroy();
            b && this.redraw(o(this.options.chart.animation, a && a.animation, this.pointCount < 100));
        },
        pan: function(a, b) {
            var c = this, d = c.hoverPoints, e;
            d && q(d, function(a) { a.setState(); });
            q(b === "xy" ? [1, 0] : [1], function(b) {
                var d = a[b ? "chartX" : "chartY"], h = c[b ? "xAxis" : "yAxis"][0], i = c[b ? "mouseDownX" : "mouseDownY"], k = (h.pointRange || 0) / 2, j = h.getExtremes(), l = h.toValue(i - d, !0) + k, i = h.toValue(i + c[b ? "plotWidth" : "plotHeight"] - d, !0) - k;
                h.series.length && l > z(j.dataMin, j.min) && i < v(j.dataMax, j.max) && (h.setExtremes(l, i, !1, !1, { trigger: "pan" }), e = !0);
                c[b ? "mouseDownX" : "mouseDownY"] = d;
            });
            e && c.redraw(!1);
            G(c.container, { cursor: "move" });
        },
        setTitle: function(a, b) {
            var f;
            var c = this, d = c.options, e;
            e = d.title = u(d.title,
                a);
            f = d.subtitle = u(d.subtitle, b), d = f;
            q([["title", a, e], ["subtitle", b, d]], function(a) {
                var b = a[0], d = c[b], e = a[1], a = a[2];
                d && e && (c[b] = d = d.destroy());
                a && a.text && !d && (c[b] = c.renderer.text(a.text, 0, 0, a.useHTML).attr({ align: a.align, "class": "highcharts-" + b, zIndex: a.zIndex || 4 }).css(a.style).add());
            });
            c.layOutTitles();
        },
        layOutTitles: function() {
            var a = 0, b = this.title, c = this.subtitle, d = this.options, e = d.title, d = d.subtitle, f = this.spacingBox.width - 44;
            if (b && (b.css({ width: (e.width || f) + "px" }).align(x({ y: 15 }, e), !1, "spacingBox"),
            !e.floating && !e.verticalAlign)) a = b.getBBox().height, a >= 18 && a <= 25 && (a = 15);
            c && (c.css({ width: (d.width || f) + "px" }).align(x({ y: a + e.margin }, d), !1, "spacingBox"), !d.floating && !d.verticalAlign && (a = Ga(a + c.getBBox().height)));
            this.titleOffset = a;
        },
        getChartSize: function() {
            var a = this.options.chart, b = this.renderToClone || this.renderTo;
            this.containerWidth = wb(b, "width");
            this.containerHeight = wb(b, "height");
            this.chartWidth = v(0, a.width || this.containerWidth || 600);
            this.chartHeight = v(0, o(a.height, this.containerHeight > 19 ? this.containerHeight :
                400));
        },
        cloneRenderTo: function(a) {
            var b = this.renderToClone, c = this.container;
            a ? b && (this.renderTo.appendChild(c), $a(b), delete this.renderToClone) : (c && c.parentNode === this.renderTo && this.renderTo.removeChild(c), this.renderToClone = b = this.renderTo.cloneNode(0), G(b, { position: "absolute", top: "-9999px", display: "block" }), J.body.appendChild(b), c && b.appendChild(c));
        },
        getContainer: function() {
            var a, b = this.options.chart, c, d, e;
            this.renderTo = a = b.renderTo;
            e = "highcharts-" + Kb++;
            if (ka(a)) this.renderTo = a = J.getElementById(a);
            a || ra(13, !0);
            c = B(C(a, "data-highcharts-chart"));
            !isNaN(c) && Wa[c] && Wa[c].destroy();
            C(a, "data-highcharts-chart", this.index);
            a.innerHTML = "";
            a.offsetWidth || this.cloneRenderTo();
            this.getChartSize();
            c = this.chartWidth;
            d = this.chartHeight;
            this.container = a = Y(Ta, { className: "highcharts-container" + (b.className ? " " + b.className : ""), id: e }, x({ position: "relative", overflow: "hidden", width: c + "px", height: d + "px", textAlign: "left", lineHeight: "normal", zIndex: 0, "-webkit-tap-highlight-color": "rgba(0,0,0,0)" }, b.style), this.renderToClone ||
                a);
            this._cursor = a.style.cursor;
            this.renderer = b.forExport ? new za(a, c, d, !0) : new cb(a, c, d);
            ia && this.renderer.create(this, a, c, d);
        },
        getMargins: function() {
            var a = this.spacing, b, c = this.legend, d = this.margin, e = this.options.legend, f = o(e.margin, 10), g = e.x, h = e.y, i = e.align, k = e.verticalAlign, j = this.titleOffset;
            this.resetMargins();
            b = this.axisOffset;
            if (j && !r(d[0])) this.plotTop = v(this.plotTop, j + this.options.title.margin + a[0]);
            if (c.display && !e.floating)
                if (i === "right") {
                    if (!r(d[1]))
                        this.marginRight = v(this.marginRight, c.legendWidth -
                            g + f + a[1]);
                } else if (i === "left") {
                    if (!r(d[3])) this.plotLeft = v(this.plotLeft, c.legendWidth + g + f + a[3]);
                } else if (k === "top") {
                    if (!r(d[0])) this.plotTop = v(this.plotTop, c.legendHeight + h + f + a[0]);
                } else if (k === "bottom" && !r(d[2])) this.marginBottom = v(this.marginBottom, c.legendHeight - h + f + a[2]);
            this.extraBottomMargin && (this.marginBottom += this.extraBottomMargin);
            this.extraTopMargin && (this.plotTop += this.extraTopMargin);
            this.hasCartesianSeries && q(this.axes, function(a) { a.getOffset(); });
            r(d[3]) || (this.plotLeft += b[3]);
            r(d[0]) ||
                (this.plotTop += b[0]);
            r(d[2]) || (this.marginBottom += b[2]);
            r(d[1]) || (this.marginRight += b[1]);
            this.setChartSize();
        },
        initReflow: function() {

            function a(a) {
                var g = c.width || wb(d, "width"), h = c.height || wb(d, "height"), a = a ? a.target : W;
                if (!b.hasUserSize && g && h && (a === W || a === J)) {
                    if (g !== b.containerWidth || h !== b.containerHeight) clearTimeout(e), b.reflowTimeout = e = setTimeout(function() { if (b.container) b.setSize(g, h, !1), b.hasUserSize = null; }, 100);
                    b.containerWidth = g;
                    b.containerHeight = h;
                }
            }

            var b = this, c = b.options.chart, d = b.renderTo,
                e;
            b.reflow = a;
            E(W, "resize", a);
            E(b, "destroy", function() { U(W, "resize", a); });
        },
        setSize: function(a, b, c) {
            var d = this, e, f, g;
            d.isResizing += 1;
            g = function() { d && L(d, "endResize", null, function() { d.isResizing -= 1; }); };
            ab(c, d);
            d.oldChartHeight = d.chartHeight;
            d.oldChartWidth = d.chartWidth;
            if (r(a)) d.chartWidth = e = v(0, t(a)), d.hasUserSize = !!e;
            if (r(b)) d.chartHeight = f = v(0, t(b));
            G(d.container, { width: e + "px", height: f + "px" });
            d.setChartSize(!0);
            d.renderer.setSize(e, f, c);
            d.maxTicks = null;
            q(d.axes, function(a) {
                a.isDirty = !0;
                a.setScale();
            });
            q(d.series, function(a) { a.isDirty = !0; });
            d.isDirtyLegend = !0;
            d.isDirtyBox = !0;
            d.getMargins();
            d.redraw(c);
            d.oldChartHeight = null;
            L(d, "resize");
            Ua === !1 ? g() : setTimeout(g, Ua && Ua.duration || 500);
        },
        setChartSize: function(a) {
            var b = this.inverted, c = this.renderer, d = this.chartWidth, e = this.chartHeight, f = this.options.chart, g = this.spacing, h = this.clipOffset, i, k, j, l;
            this.plotLeft = i = t(this.plotLeft);
            this.plotTop = k = t(this.plotTop);
            this.plotWidth = j = v(0, t(d - i - this.marginRight));
            this.plotHeight = l = v(0, t(e - k - this.marginBottom));
            this.plotSizeX = b ? l : j;
            this.plotSizeY = b ? j : l;
            this.plotBorderWidth = f.plotBorderWidth || 0;
            this.spacingBox = c.spacingBox = { x: g[3], y: g[0], width: d - g[3] - g[1], height: e - g[0] - g[2] };
            this.plotBox = c.plotBox = { x: i, y: k, width: j, height: l };
            d = 2 * O(this.plotBorderWidth / 2);
            b = Ga(v(d, h[3]) / 2);
            c = Ga(v(d, h[0]) / 2);
            this.clipBox = { x: b, y: c, width: O(this.plotSizeX - v(d, h[1]) / 2 - b), height: O(this.plotSizeY - v(d, h[2]) / 2 - c) };
            a || q(this.axes, function(a) {
                a.setAxisSize();
                a.setAxisTranslation();
            });
        },
        resetMargins: function() {
            var a = this.spacing, b = this.margin;
            this.plotTop = o(b[0], a[0]);
            this.marginRight = o(b[1], a[1]);
            this.marginBottom = o(b[2], a[2]);
            this.plotLeft = o(b[3], a[3]);
            this.axisOffset = [0, 0, 0, 0];
            this.clipOffset = [0, 0, 0, 0];
        },
        drawChartBox: function() {
            var a = this.options.chart, b = this.renderer, c = this.chartWidth, d = this.chartHeight, e = this.chartBackground, f = this.plotBackground, g = this.plotBorder, h = this.plotBGImage, i = a.borderWidth || 0, k = a.backgroundColor, j = a.plotBackgroundColor, l = a.plotBackgroundImage, m = a.plotBorderWidth || 0, p, n = this.plotLeft, o = this.plotTop, w = this.plotWidth,
                q = this.plotHeight, t = this.plotBox, v = this.clipRect, s = this.clipBox;
            p = i + (a.shadow ? 8 : 0);
            if (i || k)
                if (e) e.animate(e.crisp(null, null, null, c - p, d - p));
                else {
                    e = { fill: k || Z };
                    if (i) e.stroke = a.borderColor, e["stroke-width"] = i;
                    this.chartBackground = b.rect(p / 2, p / 2, c - p, d - p, a.borderRadius, i).attr(e).add().shadow(a.shadow);
                }
            if (j) f ? f.animate(t) : this.plotBackground = b.rect(n, o, w, q, 0).attr({ fill: j }).add().shadow(a.plotShadow);
            if (l) h ? h.animate(t) : this.plotBGImage = b.image(l, n, o, w, q).add();
            v ? v.animate({ width: s.width, height: s.height }) :
                this.clipRect = b.clipRect(s);
            if (m) g ? g.animate(g.crisp(null, n, o, w, q)) : this.plotBorder = b.rect(n, o, w, q, 0, -m).attr({ stroke: a.plotBorderColor, "stroke-width": m, zIndex: 1 }).add();
            this.isDirtyBox = !1;
        },
        propFromSeries: function() {
            var a = this, b = a.options.chart, c, d = a.options.series, e, f;
            q(["inverted", "angular", "polar"], function(g) {
                c = N[b.type || b.defaultSeriesType];
                f = a[g] || b[g] || c && c.prototype[g];
                for (e = d && d.length; !f && e--;) (c = N[d[e].type]) && c.prototype[g] && (f = !0);
                a[g] = f;
            });
        },
        linkSeries: function() {
            var a = this, b = a.series;
            q(b, function(a) { a.linkedSeries.length = 0; });
            q(b, function(b) {
                var d = b.options.linkedTo;
                if (ka(d) && (d = d === ":previous" ? a.series[b.index - 1] : a.get(d))) d.linkedSeries.push(b), b.linkedParent = d;
            });
        },
        render: function() {
            var a = this, b = a.axes, c = a.renderer, d = a.options, e = d.labels, f = d.credits, g;
            a.setTitle();
            a.legend = new sb(a, d.legend);
            a.getStacks();
            q(b, function(a) { a.setScale(); });
            a.getMargins();
            a.maxTicks = null;
            q(b, function(a) {
                a.setTickPositions(!0);
                a.setMaxTicks();
            });
            a.adjustTickAmounts();
            a.getMargins();
            a.drawChartBox();
            a.hasCartesianSeries && q(b, function(a) { a.render(); });
            if (!a.seriesGroup) a.seriesGroup = c.g("series-group").attr({ zIndex: 3 }).add();
            q(a.series, function(a) {
                a.translate();
                a.setTooltipPoints();
                a.render();
            });
            e.items && q(e.items, function(b) {
                var d = x(e.style, b.style), f = B(d.left) + a.plotLeft, g = B(d.top) + a.plotTop + 12;
                delete d.left;
                delete d.top;
                c.text(b.html, f, g).attr({ zIndex: 2 }).css(d).add();
            });
            if (f.enabled && !a.credits)
                g = f.href, a.credits = c.text(f.text, 0, 0).on("click", function() { if (g) location.href = g; }).attr({
                    align: f.position.align,
                    zIndex: 8
                }).css(f.style).add().align(f.position);
            a.hasRendered = !0;
        },
        destroy: function() {
            var a = this, b = a.axes, c = a.series, d = a.container, e, f = d && d.parentNode;
            L(a, "destroy");
            Wa[a.index] = s;
            a.renderTo.removeAttribute("data-highcharts-chart");
            U(a);
            for (e = b.length; e--;) b[e] = b[e].destroy();
            for (e = c.length; e--;) c[e] = c[e].destroy();
            q("title,subtitle,chartBackground,plotBackground,plotBGImage,plotBorder,seriesGroup,clipRect,credits,pointer,scroller,rangeSelector,legend,resetZoomButton,tooltip,renderer".split(","), function(b) {
                var c =
                    a[b];
                c && c.destroy && (a[b] = c.destroy());
            });
            if (d) d.innerHTML = "", U(d), f && $a(d);
            for (e in a) delete a[e];
        },
        isReadyToRender: function() {
            var a = this;
            return !aa && W == W.top && J.readyState !== "complete" || ia && !W.canvg ? (ia ? Zb.push(function() { a.firstRender(); }, a.options.global.canvasToolsURL) : J.attachEvent("onreadystatechange", function() {
                J.detachEvent("onreadystatechange", a.firstRender);
                J.readyState === "complete" && a.firstRender();
            }), !1) : !0;
        },
        firstRender: function() {
            var a = this, b = a.options, c = a.callback;
            if (a.isReadyToRender())
                a.getContainer(),
                L(a, "init"), a.resetMargins(), a.setChartSize(), a.propFromSeries(), a.getAxes(), q(b.series || [], function(b) { a.initSeries(b); }), a.linkSeries(), L(a, "beforeRender"), a.pointer = new rb(a, b), a.render(), a.renderer.draw(), c && c.apply(a, [a]), q(a.callbacks, function(b) { b.apply(a, [a]); }), a.cloneRenderTo(!0), L(a, "load");
        },
        splashArray: function(a, b) {
            var c = b[a], c = $(c) ? c : [c, c, c, c];
            return [o(b[a + "Top"], c[0]), o(b[a + "Right"], c[1]), o(b[a + "Bottom"], c[2]), o(b[a + "Left"], c[3])];
        }
    };
    Va.prototype.callbacks = [];
    var Ka = function() {
    };
    Ka.prototype =
    {
        init: function(a, b, c) {
            this.series = a;
            this.applyOptions(b, c);
            this.pointAttr = {};
            if (a.options.colorByPoint && (b = a.options.colors || a.chart.options.colors, this.color = this.color || b[a.colorCounter++], a.colorCounter === b.length)) a.colorCounter = 0;
            a.chart.pointCount++;
            return this;
        },
        applyOptions: function(a, b) {
            var c = this.series, d = c.pointValKey, a = Ka.prototype.optionsToObject.call(this, a);
            x(this, a);
            this.options = this.options ? x(this.options, a) : a;
            if (d) this.y = this[d];
            if (this.x === s && c) this.x = b === s ? c.autoIncrement() : b;
            return this;
        },
        optionsToObject: function(a) {
            var b = {}, c = this.series, d = c.pointArrayMap || ["y"], e = d.length, f = 0, g = 0;
            if (typeof a === "number" || a === null) b[d[0]] = a;
            else if (Ya(a)) {
                if (a.length > e) {
                    c = typeof a[0];
                    if (c === "string") b.name = a[0];
                    else if (c === "number") b.x = a[0];
                    f++;
                }
                for (; g < e;) b[d[g++]] = a[f++];
            } else if (typeof a === "object") {
                b = a;
                if (a.dataLabels) c._hasPointLabels = !0;
                if (a.marker) c._hasPointMarkers = !0;
            }
            return b;
        },
        destroy: function() {
            var a = this.series.chart, b = a.hoverPoints, c;
            a.pointCount--;
            if (b && (this.setState(), ma(b, this), !b.length))
                a.hoverPoints =
                    null;
            if (this === a.hoverPoint) this.onMouseOut();
            if (this.graphic || this.dataLabel) U(this), this.destroyElements();
            this.legendItem && a.legend.destroyItem(this);
            for (c in this) this[c] = null;
        },
        destroyElements: function() { for (var a = "graphic,dataLabel,dataLabelUpper,group,connector,shadowGroup".split(","), b, c = 6; c--;) b = a[c], this[b] && (this[b] = this[b].destroy()); },
        getLabelConfig: function() {
            return {
                x: this.category,
                y: this.y,
                key: this.name || this.category,
                series: this.series,
                point: this,
                percentage: this.percentage,
                total: this.total ||
                    this.stackTotal
            };
        },
        select: function(a, b) {
            var c = this, d = c.series, e = d.chart, a = o(a, !c.selected);
            c.firePointEvent(a ? "select" : "unselect", { accumulate: b }, function() {
                c.selected = c.options.selected = a;
                d.options.data[xa(c, d.data)] = c.options;
                c.setState(a && "select");
                b || q(e.getSelectedPoints(), function(a) { if (a.selected && a !== c) a.selected = a.options.selected = !1, d.options.data[xa(a, d.data)] = a.options, a.setState(""), a.firePointEvent("unselect"); });
            });
        },
        onMouseOver: function(a) {
            var b = this.series, c = b.chart, d = c.tooltip, e = c.hoverPoint;
            if (e && e !== this) e.onMouseOut();
            this.firePointEvent("mouseOver");
            d && (!d.shared || b.noSharedTooltip) && d.refresh(this, a);
            this.setState("hover");
            c.hoverPoint = this;
        },
        onMouseOut: function() {
            var a = this.series.chart, b = a.hoverPoints;
            if (!b || xa(this, b) === -1) this.firePointEvent("mouseOut"), this.setState(), a.hoverPoint = null;
        },
        tooltipFormatter: function(a) {
            var b = this.series, c = b.tooltipOptions, d = o(c.valueDecimals, ""), e = c.valuePrefix || "", f = c.valueSuffix || "";
            q(b.pointArrayMap || ["y"], function(b) {
                b = "{point." + b;
                if (e || f)
                    a =
                        a.replace(b + "}", e + b + "}" + f);
                a = a.replace(b + "}", b + ":,." + d + "f}");
            });
            return Oa(a, { point: this, series: this.series });
        },
        update: function(a, b, c) {
            var d = this, e = d.series, f = d.graphic, g, h = e.data, i = e.chart, k = e.options, b = o(b, !0);
            d.firePointEvent("update", { options: a }, function() {
                d.applyOptions(a);
                if ($(a) && (e.getAttribs(), f)) a && a.marker && a.marker.symbol ? d.graphic = f.destroy() : f.attr(d.pointAttr[d.state || ""]);
                g = xa(d, h);
                e.xData[g] = d.x;
                e.yData[g] = e.toYData ? e.toYData(d) : d.y;
                e.zData[g] = d.z;
                k.data[g] = d.options;
                e.isDirty = e.isDirtyData =
                    !0;
                if (!e.fixedBox && e.hasCartesianSeries) i.isDirtyBox = !0;
                k.legendType === "point" && i.legend.destroyItem(d);
                b && i.redraw(c);
            });
        },
        remove: function(a, b) {
            var c = this, d = c.series, e = d.points, f = d.chart, g, h = d.data;
            ab(b, f);
            a = o(a, !0);
            c.firePointEvent("remove", null, function() {
                g = xa(c, h);
                h.length === e.length && e.splice(g, 1);
                h.splice(g, 1);
                d.options.data.splice(g, 1);
                d.xData.splice(g, 1);
                d.yData.splice(g, 1);
                d.zData.splice(g, 1);
                c.destroy();
                d.isDirty = !0;
                d.isDirtyData = !0;
                a && f.redraw();
            });
        },
        firePointEvent: function(a, b, c) {
            var d = this,
                e = this.series.options;
            (e.point.events[a] || d.options && d.options.events && d.options.events[a]) && this.importEvents();
            a === "click" && e.allowPointSelect && (c = function(a) { d.select(null, a.ctrlKey || a.metaKey || a.shiftKey); });
            L(this, a, b, c);
        },
        importEvents: function() {
            if (!this.hasImportedEvents) {
                var a = u(this.series.options.point, this.options).events, b;
                this.events = a;
                for (b in a) E(this, b, a[b]);
                this.hasImportedEvents = !0;
            }
        },
        setState: function(a) {
            var b = this.plotX, c = this.plotY, d = this.series, e = d.options.states, f = Q[d.type].marker &&
                d.options.marker, g = f && !f.enabled, h = f && f.states[a], i = h && h.enabled === !1, k = d.stateMarkerGraphic, j = this.marker || {}, l = d.chart, m = this.pointAttr, a = a || "";
            if (!(a === this.state || this.selected && a !== "select" || e[a] && e[a].enabled === !1 || a && (i || g && !h.enabled) || a && j.states && j.states[a] && j.states[a].enabled === !1)) {
                if (this.graphic) e = f && this.graphic.symbolName && m[a].r, this.graphic.attr(u(m[a], e ? { x: b - e, y: c - e, width: 2 * e, height: 2 * e } : {}));
                else {
                    if (a && h)
                        e = h.radius, j = j.symbol || d.symbol, k && k.currentSymbol !== j && (k = k.destroy()),
                        k ? k.attr({ x: b - e, y: c - e }) : (d.stateMarkerGraphic = k = l.renderer.symbol(j, b - e, c - e, 2 * e, 2 * e).attr(m[a]).add(d.markerGroup), k.currentSymbol = j);
                    if (k) k[a && l.isInsidePlot(b, c) ? "show" : "hide"]();
                }
                this.state = a;
            }
        }
    };
    var V = function() {
    };
    V.prototype = {
        isCartesian: !0,
        type: "line",
        pointClass: Ka,
        sorted: !0,
        requireSorting: !0,
        pointAttrToOptions: { stroke: "lineColor", "stroke-width": "lineWidth", fill: "fillColor", r: "radius" },
        colorCounter: 0,
        init: function(a, b) {
            var c, d, e = a.series;
            this.chart = a;
            this.options = b = this.setOptions(b);
            this.linkedSeries =
                [];
            this.bindAxes();
            x(this, { name: b.name, state: "", pointAttr: {}, visible: b.visible !== !1, selected: b.selected === !0 });
            if (ia) b.animation = !1;
            d = b.events;
            for (c in d) E(this, c, d[c]);
            if (d && d.click || b.point && b.point.events && b.point.events.click || b.allowPointSelect) a.runTrackerClick = !0;
            this.getColor();
            this.getSymbol();
            this.setData(b.data, !1);
            if (this.isCartesian) a.hasCartesianSeries = !0;
            e.push(this);
            this._i = e.length - 1;
            Sb(e, function(a, b) { return o(a.options.index, a._i) - o(b.options.index, a._i); });
            q(e, function(a, b) {
                a.index =
                    b;
                a.name = a.name || "Series " + (b + 1);
            });
        },
        bindAxes: function() {
            var a = this, b = a.options, c = a.chart, d;
            a.isCartesian && q(["xAxis", "yAxis"], function(e) {
                q(c[e], function(c) {
                    d = c.options;
                    if (b[e] === d.index || b[e] !== s && b[e] === d.id || b[e] === s && d.index === 0) c.series.push(a), a[e] = c, c.isDirty = !0;
                });
                a[e] || ra(18, !0);
            });
        },
        autoIncrement: function() {
            var a = this.options, b = this.xIncrement, b = o(b, a.pointStart, 0);
            this.pointInterval = o(this.pointInterval, a.pointInterval, 1);
            this.xIncrement = b + this.pointInterval;
            return b;
        },
        getSegments: function() {
            var a =
                    -1, b = [], c, d = this.points, e = d.length;
            if (e)
                if (this.options.connectNulls) {
                    for (c = e; c--;) d[c].y === null && d.splice(c, 1);
                    d.length && (b = [d]);
                } else q(d, function(c, g) { c.y === null ? (g > a + 1 && b.push(d.slice(a + 1, g)), a = g) : g === e - 1 && b.push(d.slice(a + 1, g + 1)); });
            this.segments = b;
        },
        setOptions: function(a) {
            var b = this.chart.options, c = b.plotOptions, d = c[this.type];
            this.userOptions = a;
            a = u(d, c.series, a);
            this.tooltipOptions = u(b.tooltip, a.tooltip);
            d.marker === null && delete a.marker;
            return a;
        },
        getColor: function() {
            var a = this.options, b = this.userOptions,
                c = this.chart.options.colors, d = this.chart.counters, e;
            e = a.color || Q[this.type].color;
            if (!e && !a.colorByPoint) r(b._colorIndex) ? a = b._colorIndex : (b._colorIndex = d.color, a = d.color++), e = c[a];
            this.color = e;
            d.wrapColor(c.length);
        },
        getSymbol: function() {
            var a = this.userOptions, b = this.options.marker, c = this.chart, d = c.options.symbols, c = c.counters;
            this.symbol = b.symbol;
            if (!this.symbol) r(a._symbolIndex) ? a = a._symbolIndex : (a._symbolIndex = c.symbol, a = c.symbol++), this.symbol = d[a];
            if (/^url/.test(this.symbol)) b.radius = 0;
            c.wrapSymbol(d.length);
        },
        drawLegendSymbol: function(a) {
            var b = this.options, c = b.marker, d = a.options, e;
            e = d.symbolWidth;
            var f = this.chart.renderer, g = this.legendGroup, a = a.baseline - t(f.fontMetrics(d.itemStyle.fontSize).b * 0.3);
            if (b.lineWidth) {
                d = { "stroke-width": b.lineWidth };
                if (b.dashStyle) d.dashstyle = b.dashStyle;
                this.legendLine = f.path(["M", 0, a, "L", e, a]).attr(d).add(g);
            }
            if (c && c.enabled) b = c.radius, this.legendSymbol = e = f.symbol(this.symbol, e / 2 - b, a - b, 2 * b, 2 * b).add(g), e.isMarker = !0;
        },
        addPoint: function(a, b, c, d) {
            var e = this.options, f = this.data,
                g = this.graph, h = this.area, i = this.chart, k = this.xData, j = this.yData, l = this.zData, m = this.xAxis && this.xAxis.names, p = g && g.shift || 0, n = e.data, t;
            ab(d, i);
            c && q([g, h, this.graphNeg, this.areaNeg], function(a) { if (a) a.shift = p + 1; });
            if (h) h.isArea = !0;
            b = o(b, !0);
            d = { series: this };
            this.pointClass.prototype.applyOptions.apply(d, [a]);
            g = d.x;
            h = k.length;
            if (this.requireSorting && g < k[h - 1]) for (t = !0; h && k[h - 1] > g;) h--;
            k.splice(h, 0, g);
            j.splice(h, 0, this.toYData ? this.toYData(d) : d.y);
            l.splice(h, 0, d.z);
            if (m) m[g] = d.name;
            n.splice(h, 0, a);
            t && (this.data.splice(h,
                0, null), this.processData());
            e.legendType === "point" && this.generatePoints();
            c && (f[0] && f[0].remove ? f[0].remove(!1) : (f.shift(), k.shift(), j.shift(), l.shift(), n.shift()));
            this.isDirtyData = this.isDirty = !0;
            b && (this.getAttribs(), i.redraw());
        },
        setData: function(a, b) {
            var c = this.points, d = this.options, e = this.chart, f = null, g = this.xAxis, h = g && g.names, i;
            this.xIncrement = null;
            this.pointRange = g && g.categories ? 1 : d.pointRange;
            this.colorCounter = 0;
            var k = [], j = [], l = [], m = a ? a.length : [];
            i = o(d.turboThreshold, 1E3);
            var p = this.pointArrayMap,
                p = p && p.length, n = !!this.toYData;
            if (i && m > i) {
                for (i = 0; f === null && i < m;) f = a[i], i++;
                if (sa(f)) {
                    h = o(d.pointStart, 0);
                    d = o(d.pointInterval, 1);
                    for (i = 0; i < m; i++) k[i] = h, j[i] = a[i], h += d;
                    this.xIncrement = h;
                } else if (Ya(f))
                    if (p) for (i = 0; i < m; i++) d = a[i], k[i] = d[0], j[i] = d.slice(1, p + 1);
                    else for (i = 0; i < m; i++) d = a[i], k[i] = d[0], j[i] = d[1];
                else ra(12);
            } else for (i = 0; i < m; i++) if (a[i] !== s && (d = { series: this }, this.pointClass.prototype.applyOptions.apply(d, [a[i]]), k[i] = d.x, j[i] = n ? this.toYData(d) : d.y, l[i] = d.z, h && d.name)) h[d.x] = d.name;
            ka(j[0]) &&
                ra(14, !0);
            this.data = [];
            this.options.data = a;
            this.xData = k;
            this.yData = j;
            this.zData = l;
            for (i = c && c.length || 0; i--;) c[i] && c[i].destroy && c[i].destroy();
            if (g) g.minRange = g.userMinRange;
            this.isDirty = this.isDirtyData = e.isDirtyBox = !0;
            o(b, !0) && e.redraw(!1);
        },
        remove: function(a, b) {
            var c = this, d = c.chart, a = o(a, !0);
            if (!c.isRemoving)
                c.isRemoving = !0, L(c, "remove", null, function() {
                    c.destroy();
                    d.isDirtyLegend = d.isDirtyBox = !0;
                    d.linkSeries();
                    a && d.redraw(b);
                });
            c.isRemoving = !1;
        },
        processData: function(a) {
            var b = this.xData, c = this.yData,
                d = b.length, e;
            e = 0;
            var f, g, h = this.xAxis, i = this.options, k = i.cropThreshold, j = this.isCartesian;
            if (j && !this.isDirty && !h.isDirty && !this.yAxis.isDirty && !a) return !1;
            if (j && this.sorted && (!k || d > k || this.forceCrop))
                if (a = h.min, h = h.max, b[d - 1] < a || b[0] > h) b = [], c = [];
                else if (b[0] < a || b[d - 1] > h) e = this.cropData(this.xData, this.yData, a, h), b = e.xData, c = e.yData, e = e.start, f = !0;
            for (h = b.length - 1; h >= 0; h--) d = b[h] - b[h - 1], d > 0 && (g === s || d < g) ? g = d : d < 0 && this.requireSorting && ra(15);
            this.cropped = f;
            this.cropStart = e;
            this.processedXData = b;
            this.processedYData =
                c;
            if (i.pointRange === null) this.pointRange = g || 1;
            this.closestPointRange = g;
        },
        cropData: function(a, b, c, d) {
            var e = a.length, f = 0, g = e, h = o(this.cropShoulder, 1), i;
            for (i = 0; i < e; i++)
                if (a[i] >= c) {
                    f = v(0, i - h);
                    break;
                }
            for (; i < e; i++)
                if (a[i] > d) {
                    g = i + h;
                    break;
                }
            return { xData: a.slice(f, g), yData: b.slice(f, g), start: f, end: g };
        },
        generatePoints: function() {
            var a = this.options.data, b = this.data, c, d = this.processedXData, e = this.processedYData, f = this.pointClass, g = d.length, h = this.cropStart || 0, i, k = this.hasGroupedData, j, l = [], m;
            if (!b && !k)
                b = [], b.length =
                    a.length, b = this.data = b;
            for (m = 0; m < g; m++) i = h + m, k ? l[m] = (new f).init(this, [d[m]].concat(fa(e[m]))) : (b[i] ? j = b[i] : a[i] !== s && (b[i] = j = (new f).init(this, a[i], d[m])), l[m] = j);
            if (b && (g !== (c = b.length) || k)) for (m = 0; m < c; m++) if (m === h && !k && (m += g), b[m]) b[m].destroyElements(), b[m].plotX = s;
            this.data = b;
            this.points = l;
        },
        setStackedPoints: function() {
            if (this.options.stacking && !(this.visible !== !0 && this.chart.options.chart.ignoreHiddenSeries !== !1)) {
                var a = this.processedXData, b = this.processedYData, c = [], d = b.length, e = this.options,
                    f = e.threshold, g = e.stack, e = e.stacking, h = this.stackKey, i = "-" + h, k = this.negStacks, j = this.yAxis, l = j.stacks, m = j.oldStacks, p, n, o, q, y;
                for (o = 0; o < d; o++) {
                    q = a[o];
                    y = b[o];
                    n = (p = k && y < f) ? i : h;
                    l[n] || (l[n] = {});
                    if (!l[n][q]) m[n] && m[n][q] ? (l[n][q] = m[n][q], l[n][q].total = null) : l[n][q] = new Ub(j, j.options.stackLabels, p, q, g, e);
                    n = l[n][q];
                    n.points[this.index] = [n.cum || 0];
                    e === "percent" ? (p = p ? h : i, k && l[p] && l[p][q] ? (p = l[p][q], n.total = p.total = v(p.total, n.total) + M(y) || 0) : n.total += M(y) || 0) : n.total += y || 0;
                    n.cum = (n.cum || 0) + (y || 0);
                    n.points[this.index].push(n.cum);
                    c[o] = n.cum;
                }
                if (e === "percent") j.usePercentage = !0;
                this.stackedYData = c;
                j.oldStacks = {};
            }
        },
        setPercentStacks: function() {
            var a = this, b = a.stackKey, c = a.yAxis.stacks;
            q([b, "-" + b], function(b) {
                var d;
                for (var e = a.xData.length, f, g; e--;) if (f = a.xData[e], d = (g = c[b] && c[b][f]) && g.points[a.index], f = d) g = g.total ? 100 / g.total : 0, f[0] = na(f[0] * g), f[1] = na(f[1] * g), a.stackedYData[e] = f[1];
            });
        },
        getExtremes: function() {
            var a = this.yAxis, b = this.processedXData, c = this.stackedYData || this.processedYData, d = c.length, e = [], f = 0, g = this.xAxis.getExtremes(),
                h = g.min, g = g.max, i, k, j, l;
            for (l = 0; l < d; l++)
                if (k = b[l], j = c[l], i = j !== null && j !== s && (!a.isLog || j.length || j > 0), k = this.getExtremesFromAll || this.cropped || (b[l + 1] || k) >= h && (b[l - 1] || k) <= g, i && k)
                    if (i = j.length) for (; i--;) j[i] !== null && (e[f++] = j[i]);
                    else e[f++] = j;
            this.dataMin = o(void 0, Sa(e));
            this.dataMax = o(void 0, va(e));
        },
        translate: function() {
            this.processedXData || this.processData();
            this.generatePoints();
            for (var a = this.options, b = a.stacking, c = this.xAxis, d = c.categories, e = this.yAxis, f = this.points, g = f.length, h = !!this.modifyValue,
                i = a.pointPlacement, k = i === "between" || sa(i), j = a.threshold, a = 0; a < g; a++) {
                var l = f[a], m = l.x, p = l.y, n = l.low, q = e.stacks[(this.negStacks && p < j ? "-" : "") + this.stackKey];
                if (e.isLog && p <= 0) l.y = p = null;
                l.plotX = c.translate(m, 0, 0, 0, 1, i, this.type === "flags");
                if (b && this.visible && q && q[m]) q = q[m], p = q.points[this.index], n = p[0], p = p[1], n === 0 && (n = o(j, e.min)), e.isLog && n <= 0 && (n = null), l.total = l.stackTotal = q.total, l.percentage = b === "percent" && l.y / q.total * 100, l.stackY = p, q.setOffset(this.pointXOffset || 0, this.barW || 0);
                l.yBottom = r(n) ? e.translate(n,
                    0, 1, 0, 1) : null;
                h && (p = this.modifyValue(p, l));
                l.plotY = typeof p === "number" && p !== Infinity ? e.translate(p, 0, 1, 0, 1) : s;
                l.clientX = k ? c.translate(m, 0, 0, 0, 1) : l.plotX;
                l.negative = l.y < (j || 0);
                l.category = d && d[l.x] !== s ? d[l.x] : l.x;
            }
            this.getSegments();
        },
        setTooltipPoints: function(a) {
            var b = [], c, d, e = this.xAxis, f = e && e.getExtremes(), g = e ? e.tooltipLen || e.len : this.chart.plotSizeX, h, i, k = [];
            if (this.options.enableMouseTracking !== !1) {
                if (a) this.tooltipPoints = null;
                q(this.segments || this.points, function(a) { b = b.concat(a); });
                e && e.reversed &&
                    (b = b.reverse());
                this.orderTooltipPoints && this.orderTooltipPoints(b);
                a = b.length;
                for (i = 0; i < a; i++)
                    if (e = b[i], c = e.x, c >= f.min && c <= f.max) {
                        h = b[i + 1];
                        c = d === s ? 0 : d + 1;
                        for (d = b[i + 1] ? z(v(0, O((e.clientX + (h ? h.wrappedClientX || h.clientX : g)) / 2)), g) : g; c >= 0 && c <= d;) k[c++] = e;
                    }
                this.tooltipPoints = k;
            }
        },
        tooltipHeaderFormatter: function(a) {
            var b = this.tooltipOptions, c = b.xDateFormat, d = b.dateTimeLabelFormats, e = this.xAxis, f = e && e.options.type === "datetime", b = b.headerFormat, e = e && e.closestPointRange, g;
            if (f && !c)
                if (e)
                    for (g in H) {
                        if (H[g] >=
                            e) {
                            c = d[g];
                            break;
                        }
                    }
                else c = d.day;
            f && c && sa(a.key) && (b = b.replace("{point.key}", "{point.key:" + c + "}"));
            return Oa(b, { point: a, series: this });
        },
        onMouseOver: function() {
            var a = this.chart, b = a.hoverSeries;
            if (b && b !== this) b.onMouseOut();
            this.options.events.mouseOver && L(this, "mouseOver");
            this.setState("hover");
            a.hoverSeries = this;
        },
        onMouseOut: function() {
            var a = this.options, b = this.chart, c = b.tooltip, d = b.hoverPoint;
            if (d) d.onMouseOut();
            this && a.events.mouseOut && L(this, "mouseOut");
            c && !a.stickyTracking && (!c.shared || this.noSharedTooltip) &&
                c.hide();
            this.setState();
            b.hoverSeries = null;
        },
        animate: function(a) {
            var b = this, c = b.chart, d = c.renderer, e;
            e = b.options.animation;
            var f = c.clipBox, g = c.inverted, h;
            if (e && !$(e)) e = Q[b.type].animation;
            h = "_sharedClip" + e.duration + e.easing;
            if (a) a = c[h], e = c[h + "m"], a || (c[h] = a = d.clipRect(x(f, { width: 0 })), c[h + "m"] = e = d.clipRect(-99, g ? -c.plotLeft : -c.plotTop, 99, g ? c.chartWidth : c.chartHeight)), b.group.clip(a), b.markerGroup.clip(e), b.sharedClipKey = h;
            else {
                if (a = c[h])
                    a.animate({ width: c.plotSizeX }, e), c[h + "m"].animate({
                        width: c.plotSizeX +
                            99
                    }, e);
                b.animate = null;
                b.animationTimeout = setTimeout(function() { b.afterAnimate(); }, e.duration);
            }
        },
        afterAnimate: function() {
            var a = this.chart, b = this.sharedClipKey, c = this.group;
            c && this.options.clip !== !1 && (c.clip(a.clipRect), this.markerGroup.clip());
            setTimeout(function() { b && a[b] && (a[b] = a[b].destroy(), a[b + "m"] = a[b + "m"].destroy()); }, 100);
        },
        drawPoints: function() {
            var a, b = this.points, c = this.chart, d, e, f, g, h, i, k, j, l = this.options.marker, m, p = this.markerGroup;
            if (l.enabled || this._hasPointMarkers)
                for (f = b.length; f--;)
                    if (g =
                        b[f], d = O(g.plotX), e = g.plotY, j = g.graphic, i = g.marker || {}, a = l.enabled && i.enabled === s || i.enabled, m = c.isInsidePlot(t(d), e, c.inverted), a && e !== s && !isNaN(e) && g.y !== null)
                        if (a = g.pointAttr[g.selected ? "select" : ""], h = a.r, i = o(i.symbol, this.symbol), k = i.indexOf("url") === 0, j) j.attr({ visibility: m ? aa ? "inherit" : "visible" : "hidden" }).animate(x({ x: d - h, y: e - h }, j.symbolName ? { width: 2 * h, height: 2 * h } : {}));
                        else {
                            if (m && (h > 0 || k)) g.graphic = c.renderer.symbol(i, d - h, e - h, 2 * h, 2 * h).attr(a).add(p);
                        }
                    else if (j) g.graphic = j.destroy();
        },
        convertAttribs: function(a,
            b, c, d) {
            var e = this.pointAttrToOptions, f, g, h = {}, a = a || {}, b = b || {}, c = c || {}, d = d || {};
            for (f in e) g = e[f], h[f] = o(a[g], b[f], c[f], d[f]);
            return h;
        },
        getAttribs: function() {
            var a = this, b = a.options, c = Q[a.type].marker ? b.marker : b, d = c.states, e = d.hover, f, g = a.color, h = { stroke: g, fill: g }, i = a.points || [], k = [], j, l = a.pointAttrToOptions, m = b.negativeColor, p = c.lineColor, n;
            b.marker ? (e.radius = e.radius || c.radius + 2, e.lineWidth = e.lineWidth || c.lineWidth + 1) : e.color = e.color || ya(e.color || g).brighten(e.brightness).get();
            k[""] = a.convertAttribs(c,
                h);
            q(["hover", "select"], function(b) { k[b] = a.convertAttribs(d[b], k[""]); });
            a.pointAttr = k;
            for (g = i.length; g--;) {
                h = i[g];
                if ((c = h.options && h.options.marker || h.options) && c.enabled === !1) c.radius = 0;
                if (h.negative && m) h.color = h.fillColor = m;
                f = b.colorByPoint || h.color;
                if (h.options) for (n in l) r(c[l[n]]) && (f = !0);
                if (f) {
                    c = c || {};
                    j = [];
                    d = c.states || {};
                    f = d.hover = d.hover || {};
                    if (!b.marker) f.color = ya(f.color || h.color).brighten(f.brightness || e.brightness).get();
                    j[""] = a.convertAttribs(x({
                        color: h.color,
                        fillColor: h.color,
                        lineColor: p ===
                            null ? h.color : s
                    }, c), k[""]);
                    j.hover = a.convertAttribs(d.hover, k.hover, j[""]);
                    j.select = a.convertAttribs(d.select, k.select, j[""]);
                } else j = k;
                h.pointAttr = j;
            }
        },
        update: function(a, b) {
            var c = this.chart, d = this.type, e = N[d].prototype, f, a = u(this.userOptions, { animation: !1, index: this.index, pointStart: this.xData[0] }, { data: this.options.data }, a);
            this.remove(!1);
            for (f in e) e.hasOwnProperty(f) && (this[f] = s);
            x(this, N[a.type || d].prototype);
            this.init(c, a);
            o(b, !0) && c.redraw(!1);
        },
        destroy: function() {
            var a = this, b = a.chart, c = /AppleWebKit\/533/.test(wa),
                d, e, f = a.data || [], g, h, i;
            L(a, "destroy");
            U(a);
            q(["xAxis", "yAxis"], function(b) { if (i = a[b]) ma(i.series, a), i.isDirty = i.forceRedraw = !0, i.stacks = {}; });
            a.legendItem && a.chart.legend.destroyItem(a);
            for (e = f.length; e--;) (g = f[e]) && g.destroy && g.destroy();
            a.points = null;
            clearTimeout(a.animationTimeout);
            q("area,graph,dataLabelsGroup,group,markerGroup,tracker,graphNeg,areaNeg,posClip,negClip".split(","), function(b) { a[b] && (d = c && b === "group" ? "hide" : "destroy", a[b][d]()); });
            if (b.hoverSeries === a) b.hoverSeries = null;
            ma(b.series,
                a);
            for (h in a) delete a[h];
        },
        drawDataLabels: function() {
            var a = this, b = a.options, c = b.cursor, d = b.dataLabels, b = a.points, e, f, g, h;
            if (d.enabled || a._hasPointLabels)
                a.dlProcessOptions && a.dlProcessOptions(d), h = a.plotGroup("dataLabelsGroup", "data-labels", a.visible ? "visible" : "hidden", d.zIndex || 6), f = d, q(b, function(b) {
                    var k, j = b.dataLabel, l, m, p = b.connector, n = !0;
                    e = b.options && b.options.dataLabels;
                    k = o(e && e.enabled, f.enabled);
                    if (j && !k) b.dataLabel = j.destroy();
                    else if (k) {
                        d = u(f, e);
                        k = d.rotation;
                        l = b.getLabelConfig();
                        g = d.format ?
                            Oa(d.format, l) : d.formatter.call(l, d);
                        d.style.color = o(d.color, d.style.color, a.color, "black");
                        if (j)
                            if (r(g)) j.attr({ text: g }), n = !1;
                            else {
                                if (b.dataLabel = j = j.destroy(), p) b.connector = p.destroy();
                            }
                        else if (r(g)) {
                            j = { fill: d.backgroundColor, stroke: d.borderColor, "stroke-width": d.borderWidth, r: d.borderRadius || 0, rotation: k, padding: d.padding, zIndex: 1 };
                            for (m in j) j[m] === s && delete j[m];
                            j = b.dataLabel = a.chart.renderer[k ? "text" : "label"](g, 0, -999, null, null, null, d.useHTML).attr(j).css(x(d.style, c && { cursor: c })).add(h).shadow(d.shadow);
                        }
                        j &&
                            a.alignDataLabel(b, j, d, null, n);
                    }
                });
        },
        alignDataLabel: function(a, b, c, d, e) {
            var f = this.chart, g = f.inverted, h = o(a.plotX, -999), i = o(a.plotY, -999), k = b.getBBox();
            if (a = this.visible && f.isInsidePlot(a.plotX, a.plotY, g))
                d = x({ x: g ? f.plotWidth - i : h, y: t(g ? f.plotHeight - h : i), width: 0, height: 0 }, d), x(c, { width: k.width, height: k.height }), c.rotation ? (g = { align: c.align, x: d.x + c.x + d.width / 2, y: d.y + c.y + d.height / 2 }, b[e ? "attr" : "animate"](g)) : (b.align(c, null, d), g = b.alignAttr, o(c.overflow, "justify") === "justify" ? this.justifyDataLabel(b, c,
                    g, k, d, e) : o(c.crop, !0) && (a = f.isInsidePlot(g.x, g.y) && f.isInsidePlot(g.x + k.width, g.y + k.height)));
            a || b.attr({ y: -999 });
        },
        justifyDataLabel: function(a, b, c, d, e, f) {
            var g = this.chart, h = b.align, i = b.verticalAlign, k, j;
            k = c.x;
            if (k < 0) h === "right" ? b.align = "left" : b.x = -k, j = !0;
            k = c.x + d.width;
            if (k > g.plotWidth) h === "left" ? b.align = "right" : b.x = g.plotWidth - k, j = !0;
            k = c.y;
            if (k < 0) i === "bottom" ? b.verticalAlign = "top" : b.y = -k, j = !0;
            k = c.y + d.height;
            if (k > g.plotHeight) i === "top" ? b.verticalAlign = "bottom" : b.y = g.plotHeight - k, j = !0;
            if (j)
                a.placed =
                    !f, a.align(b, null, e);
        },
        getSegmentPath: function(a) {
            var b = this, c = [], d = b.options.step;
            q(a, function(e, f) {
                var g = e.plotX, h = e.plotY, i;
                b.getPointSpline ? c.push.apply(c, b.getPointSpline(a, e, f)) : (c.push(f ? "L" : "M"), d && f && (i = a[f - 1], d === "right" ? c.push(i.plotX, h) : d === "center" ? c.push((i.plotX + g) / 2, i.plotY, (i.plotX + g) / 2, h) : c.push(g, i.plotY)), c.push(e.plotX, e.plotY));
            });
            return c;
        },
        getGraphPath: function() {
            var a = this, b = [], c, d = [];
            q(a.segments, function(e) {
                c = a.getSegmentPath(e);
                e.length > 1 ? b = b.concat(c) : d.push(e[0]);
            });
            a.singlePoints =
                d;
            return a.graphPath = b;
        },
        drawGraph: function() {
            var a = this, b = this.options, c = [["graph", b.lineColor || this.color]], d = b.lineWidth, e = b.dashStyle, f = b.linecap !== "square", g = this.getGraphPath(), h = b.negativeColor;
            h && c.push(["graphNeg", h]);
            q(c, function(c, h) {
                var j = c[0], l = a[j];
                if (l) jb(l), l.animate({ d: g });
                else if (d && g.length) l = { stroke: c[1], "stroke-width": d, zIndex: 1 }, e ? l.dashstyle = e : f && (l["stroke-linecap"] = l["stroke-linejoin"] = "round"), a[j] = a.chart.renderer.path(g).attr(l).add(a.group).shadow(!h && b.shadow);
            });
        },
        clipNeg: function() {
            var a =
                    this.options, b = this.chart, c = b.renderer, d = a.negativeColor || a.negativeFillColor, e, f = this.graph, g = this.area, h = this.posClip, i = this.negClip;
            e = b.chartWidth;
            var k = b.chartHeight, j = v(e, k), l = this.yAxis;
            if (d && (f || g)) {
                d = t(l.toPixels(a.threshold || 0, !0));
                a = { x: 0, y: 0, width: j, height: d };
                j = { x: 0, y: d, width: j, height: j };
                if (b.inverted) a.height = j.y = b.plotWidth - d, c.isVML && (a = { x: b.plotWidth - d - b.plotLeft, y: 0, width: e, height: k }, j = { x: d + b.plotLeft - e, y: 0, width: b.plotLeft + d, height: e });
                l.reversed ? (b = j, e = a) : (b = a, e = j);
                h ? (h.animate(b),
                i.animate(e)) : (this.posClip = h = c.clipRect(b), this.negClip = i = c.clipRect(e), f && this.graphNeg && (f.clip(h), this.graphNeg.clip(i)), g && (g.clip(h), this.areaNeg.clip(i)));
            }
        },
        invertGroups: function() {

            function a() {
                var a = { width: b.yAxis.len, height: b.xAxis.len };
                q(["group", "markerGroup"], function(c) { b[c] && b[c].attr(a).invert(); });
            }

            var b = this, c = b.chart;
            if (b.xAxis) E(c, "resize", a), E(b, "destroy", function() { U(c, "resize", a); }), a(), b.invertGroups = a;
        },
        plotGroup: function(a, b, c, d, e) {
            var f = this[a], g = !f;
            g && (this[a] = f = this.chart.renderer.g(b).attr({
                visibility: c,
                zIndex: d || 0.1
            }).add(e));
            f[g ? "attr" : "animate"](this.getPlotBox());
            return f;
        },
        getPlotBox: function() { return { translateX: this.xAxis ? this.xAxis.left : this.chart.plotLeft, translateY: this.yAxis ? this.yAxis.top : this.chart.plotTop, scaleX: 1, scaleY: 1 }; },
        render: function() {
            var a = this.chart, b, c = this.options, d = c.animation && !!this.animate && a.renderer.isSVG, e = this.visible ? "visible" : "hidden", f = c.zIndex, g = this.hasRendered, h = a.seriesGroup;
            b = this.plotGroup("group", "series", e, f, h);
            this.markerGroup = this.plotGroup("markerGroup",
                "markers", e, f, h);
            d && this.animate(!0);
            this.getAttribs();
            b.inverted = this.isCartesian ? a.inverted : !1;
            this.drawGraph && (this.drawGraph(), this.clipNeg());
            this.drawDataLabels();
            this.drawPoints();
            this.options.enableMouseTracking !== !1 && this.drawTracker();
            a.inverted && this.invertGroups();
            c.clip !== !1 && !this.sharedClipKey && !g && b.clip(a.clipRect);
            d ? this.animate() : g || this.afterAnimate();
            this.isDirty = this.isDirtyData = !1;
            this.hasRendered = !0;
        },
        redraw: function() {
            var a = this.chart, b = this.isDirtyData, c = this.group, d = this.xAxis,
                e = this.yAxis;
            c && (a.inverted && c.attr({ width: a.plotWidth, height: a.plotHeight }), c.animate({ translateX: o(d && d.left, a.plotLeft), translateY: o(e && e.top, a.plotTop) }));
            this.translate();
            this.setTooltipPoints(!0);
            this.render();
            b && L(this, "updatedData");
        },
        setState: function(a) {
            var b = this.options, c = this.graph, d = this.graphNeg, e = b.states, b = b.lineWidth, a = a || "";
            if (this.state !== a) this.state = a, e[a] && e[a].enabled === !1 || (a && (b = e[a].lineWidth || b + 1), c && !c.dashstyle && (a = { "stroke-width": b }, c.attr(a), d && d.attr(a)));
        },
        setVisible: function(a,
            b) {
            var c = this, d = c.chart, e = c.legendItem, f, g = d.options.chart.ignoreHiddenSeries, h = c.visible;
            f = (c.visible = a = c.userOptions.visible = a === s ? !h : a) ? "show" : "hide";
            q(["group", "dataLabelsGroup", "markerGroup", "tracker"], function(a) { if (c[a]) c[a][f](); });
            if (d.hoverSeries === c) c.onMouseOut();
            e && d.legend.colorizeItem(c, a);
            c.isDirty = !0;
            c.options.stacking && q(d.series, function(a) { if (a.options.stacking && a.visible) a.isDirty = !0; });
            q(c.linkedSeries, function(b) { b.setVisible(a, !1); });
            if (g) d.isDirtyBox = !0;
            b !== !1 && d.redraw();
            L(c,
                f);
        },
        show: function() { this.setVisible(!0); },
        hide: function() { this.setVisible(!1); },
        select: function(a) {
            this.selected = a = a === s ? !this.selected : a;
            if (this.checkbox) this.checkbox.checked = a;
            L(this, a ? "select" : "unselect");
        },
        drawTracker: function() {
            var a = this, b = a.options, c = b.trackByArea, d = [].concat(c ? a.areaPath : a.graphPath), e = d.length, f = a.chart, g = f.pointer, h = f.renderer, i = f.options.tooltip.snap, k = a.tracker, j = b.cursor, l = j && { cursor: j }, j = a.singlePoints, m, p = function() { if (f.hoverSeries !== a) a.onMouseOver(); };
            if (e && !c)
                for (m =
                    e + 1; m--;) d[m] === "M" && d.splice(m + 1, 0, d[m + 1] - i, d[m + 2], "L"), (m && d[m] === "M" || m === e) && d.splice(m, 0, "L", d[m - 2] + i, d[m - 1]);
            for (m = 0; m < j.length; m++) e = j[m], d.push("M", e.plotX - i, e.plotY, "L", e.plotX + i, e.plotY);
            k ? k.attr({ d: d }) : (a.tracker = h.path(d).attr({ "stroke-linejoin": "round", visibility: a.visible ? "visible" : "hidden", stroke: Xb, fill: c ? Xb : Z, "stroke-width": b.lineWidth + (c ? 0 : 2 * i), zIndex: 2 }).add(a.group), q([a.tracker, a.markerGroup], function(a) {
                a.addClass("highcharts-tracker").on("mouseover", p).on("mouseout", function(a) { g.onTrackerMouseOut(a); }).css(l);
                if (ib) a.on("touchstart", p);
            }));
        }
    };
    D = ba(V);
    N.line = D;
    Q.area = u(P, { threshold: 0 });
    D = ba(V, {
        type: "area",
        getSegments: function() {
            var a = [], b = [], c = [], d = this.xAxis, e = this.yAxis, f = e.stacks[this.stackKey], g = {}, h, i, k = this.points, j = this.options.connectNulls, l, m, p;
            if (this.options.stacking && !this.cropped) {
                for (m = 0; m < k.length; m++) g[k[m].x] = k[m];
                for (p in f) f[p].total !== null && c.push(+p);
                c.sort(function(a, b) { return a - b; });
                q(c, function(a) {
                    if (!j || g[a] && g[a].y !== null)
                        g[a] ? b.push(g[a]) : (h = d.translate(a), l = f[a].percent ? f[a].total ?
                            f[a].cum * 100 / f[a].total : 0 : f[a].cum, i = e.toPixels(l, !0), b.push({ y: null, plotX: h, clientX: h, plotY: i, yBottom: i, onMouseOver: pa }));
                });
                b.length && a.push(b);
            } else V.prototype.getSegments.call(this), a = this.segments;
            this.segments = a;
        },
        getSegmentPath: function(a) {
            var b = V.prototype.getSegmentPath.call(this, a), c = [].concat(b), d, e = this.options;
            d = b.length;
            var f = this.yAxis.getThreshold(e.threshold), g;
            d === 3 && c.push("L", b[1], b[2]);
            if (e.stacking && !this.closedStacks)
                for (d = a.length - 1; d >= 0; d--)
                    g = o(a[d].yBottom, f), d < a.length - 1 &&
                        e.step && c.push(a[d + 1].plotX, g), c.push(a[d].plotX, g);
            else this.closeSegment(c, a, f);
            this.areaPath = this.areaPath.concat(c);
            return b;
        },
        closeSegment: function(a, b, c) { a.push("L", b[b.length - 1].plotX, c, "L", b[0].plotX, c); },
        drawGraph: function() {
            this.areaPath = [];
            V.prototype.drawGraph.apply(this);
            var a = this, b = this.areaPath, c = this.options, d = c.negativeColor, e = c.negativeFillColor, f = [["area", this.color, c.fillColor]];
            (d || e) && f.push(["areaNeg", d, e]);
            q(f, function(d) {
                var e = d[0], f = a[e];
                f ? f.animate({ d: b }) : a[e] = a.chart.renderer.path(b).attr({
                    fill: o(d[2],
                        ya(d[1]).setOpacity(o(c.fillOpacity, 0.75)).get()),
                    zIndex: 0
                }).add(a.group);
            });
        },
        drawLegendSymbol: function(a, b) { b.legendSymbol = this.chart.renderer.rect(0, a.baseline - 11, a.options.symbolWidth, 12, 2).attr({ zIndex: 3 }).add(b.legendGroup); }
    });
    N.area = D;
    Q.spline = u(P);
    X = ba(V, {
        type: "spline",
        getPointSpline: function(a, b, c) {
            var d = b.plotX, e = b.plotY, f = a[c - 1], g = a[c + 1], h, i, k, j;
            if (f && g) {
                a = f.plotY;
                k = g.plotX;
                var g = g.plotY, l;
                h = (1.5 * d + f.plotX) / 2.5;
                i = (1.5 * e + a) / 2.5;
                k = (1.5 * d + k) / 2.5;
                j = (1.5 * e + g) / 2.5;
                l = (j - i) * (k - d) / (k - h) + e - j;
                i += l;
                j += l;
                i > a && i > e ? (i = v(a, e), j = 2 * e - i) : i < a && i < e && (i = z(a, e), j = 2 * e - i);
                j > g && j > e ? (j = v(g, e), i = 2 * e - j) : j < g && j < e && (j = z(g, e), i = 2 * e - j);
                b.rightContX = k;
                b.rightContY = j;
            }
            c ? (b = ["C", f.rightContX || f.plotX, f.rightContY || f.plotY, h || d, i || e, d, e], f.rightContX = f.rightContY = null) : b = ["M", d, e];
            return b;
        }
    });
    N.spline = X;
    Q.areaspline = u(Q.area);
    var La = D.prototype;
    X = ba(X, { type: "areaspline", closedStacks: !0, getSegmentPath: La.getSegmentPath, closeSegment: La.closeSegment, drawGraph: La.drawGraph, drawLegendSymbol: La.drawLegendSymbol });
    N.areaspline =
        X;
    Q.column = u(P, { borderColor: "#FFFFFF", borderWidth: 1, borderRadius: 0, groupPadding: 0.2, marker: null, pointPadding: 0.1, minPointLength: 0, cropThreshold: 50, pointRange: null, states: { hover: { brightness: 0.1, shadow: !1 }, select: { color: "#C0C0C0", borderColor: "#000000", shadow: !1 } }, dataLabels: { align: null, verticalAlign: null, y: null }, stickyTracking: !1, threshold: 0 });
    X = ba(V, {
        type: "column",
        pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color", r: "borderRadius" },
        cropShoulder: 0,
        trackerGroups: ["group",
            "dataLabelsGroup"],
        negStacks: !0,
        init: function() {
            V.prototype.init.apply(this, arguments);
            var a = this, b = a.chart;
            b.hasRendered && q(b.series, function(b) { if (b.type === a.type) b.isDirty = !0; });
        },
        getColumnMetrics: function() {
            var a = this, b = a.options, c = a.xAxis, d = a.yAxis, e = c.reversed, f, g = {}, h, i = 0;
            b.grouping === !1 ? i = 1 : q(a.chart.series, function(b) {
                var c = b.options, e = b.yAxis;
                if (b.type === a.type && b.visible && d.len === e.len && d.pos === e.pos)
                    c.stacking ? (f = b.stackKey, g[f] === s && (g[f] = i++), h = g[f]) : c.grouping !== !1 && (h = i++), b.columnIndex =
                        h;
            });
            var c = z(M(c.transA) * (c.ordinalSlope || b.pointRange || c.closestPointRange || 1), c.len), k = c * b.groupPadding, j = (c - 2 * k) / i, l = b.pointWidth, b = r(l) ? (j - l) / 2 : j * b.pointPadding, l = o(l, j - 2 * b);
            return a.columnMetrics = { width: l, offset: b + (k + ((e ? i - (a.columnIndex || 0) : a.columnIndex) || 0) * j - c / 2) * (e ? -1 : 1) };
        },
        translate: function() {
            var a = this.chart, b = this.options, c = b.borderWidth, d = this.yAxis, e = this.translatedThreshold = d.getThreshold(b.threshold), f = o(b.minPointLength, 5), b = this.getColumnMetrics(), g = b.width, h = this.barW = Ga(v(g, 1 +
                2 * c)), i = this.pointXOffset = b.offset, k = -(c % 2 ? 0.5 : 0), j = c % 2 ? 0.5 : 1;
            a.renderer.isVML && a.inverted && (j += 1);
            V.prototype.translate.apply(this);
            q(this.points, function(a) {
                var b = o(a.yBottom, e), c = z(v(-999 - b, a.plotY), d.len + 999 + b), n = a.plotX + i, q = h, w = z(c, b), y, c = v(c, b) - w;
                M(c) < f && f && (c = f, w = t(M(w - e) > f ? b - f : e - (d.translate(a.y, 0, 1, 0, 1) <= e ? f : 0)));
                a.barX = n;
                a.pointWidth = g;
                b = M(n) < 0.5;
                q = t(n + q) + k;
                n = t(n) + k;
                q -= n;
                y = M(w) < 0.5;
                c = t(w + c) + j;
                w = t(w) + j;
                c -= w;
                b && (n += 1, q -= 1);
                y && (w -= 1, c += 1);
                a.shapeType = "rect";
                a.shapeArgs = { x: n, y: w, width: q, height: c };
            });
        },
        getSymbol: pa,
        drawLegendSymbol: D.prototype.drawLegendSymbol,
        drawGraph: pa,
        drawPoints: function() {
            var a = this, b = a.options, c = a.chart.renderer, d;
            q(a.points, function(e) {
                var f = e.plotY, g = e.graphic;
                if (f !== s && !isNaN(f) && e.y !== null) d = e.shapeArgs, g ? (jb(g), g.animate(u(d))) : e.graphic = c[e.shapeType](d).attr(e.pointAttr[e.selected ? "select" : ""]).add(a.group).shadow(b.shadow, null, b.stacking && !b.borderRadius);
                else if (g) e.graphic = g.destroy();
            });
        },
        drawTracker: function() {
            var a = this, b = a.chart, c = b.pointer, d = a.options.cursor,
                e = d && { cursor: d }, f = function(c) {
                    var d = c.target, e;
                    if (b.hoverSeries !== a) a.onMouseOver();
                    for (; d && !e;) e = d.point, d = d.parentNode;
                    if (e !== s && e !== b.hoverPoint) e.onMouseOver(c);
                };
            q(a.points, function(a) {
                if (a.graphic) a.graphic.element.point = a;
                if (a.dataLabel) a.dataLabel.element.point = a;
            });
            if (!a._hasTracking) q(a.trackerGroups, function(b) { if (a[b] && (a[b].addClass("highcharts-tracker").on("mouseover", f).on("mouseout", function(a) { c.onTrackerMouseOut(a); }).css(e), ib)) a[b].on("touchstart", f); }), a._hasTracking = !0;
        },
        alignDataLabel: function(a,
            b, c, d, e) {
            var f = this.chart, g = f.inverted, h = a.dlBox || a.shapeArgs, i = a.below || a.plotY > o(this.translatedThreshold, f.plotSizeY), k = o(c.inside, !!this.options.stacking);
            if (h && (d = u(h), g && (d = { x: f.plotWidth - d.y - d.height, y: f.plotHeight - d.x - d.width, width: d.height, height: d.width }), !k)) g ? (d.x += i ? 0 : d.width, d.width = 0) : (d.y += i ? d.height : 0, d.height = 0);
            c.align = o(c.align, !g || k ? "center" : i ? "right" : "left");
            c.verticalAlign = o(c.verticalAlign, g || k ? "middle" : i ? "top" : "bottom");
            V.prototype.alignDataLabel.call(this, a, b, c, d, e);
        },
        animate: function(a) {
            var b =
                    this.yAxis, c = this.options, d = this.chart.inverted, e = {};
            if (aa) a ? (e.scaleY = 0.001, a = z(b.pos + b.len, v(b.pos, b.toPixels(c.threshold))), d ? e.translateX = a - b.len : e.translateY = a, this.group.attr(e)) : (e.scaleY = 1, e[d ? "translateX" : "translateY"] = b.pos, this.group.animate(e, this.options.animation), this.animate = null);
        },
        remove: function() {
            var a = this, b = a.chart;
            b.hasRendered && q(b.series, function(b) { if (b.type === a.type) b.isDirty = !0; });
            V.prototype.remove.apply(a, arguments);
        }
    });
    N.column = X;
    Q.bar = u(Q.column);
    La = ba(X, { type: "bar", inverted: !0 });
    N.bar = La;
    Q.scatter = u(P, { lineWidth: 0, tooltip: { headerFormat: '<span style="font-size: 10px; color:{series.color}">{series.name}</span><br/>', pointFormat: "x: <b>{point.x}</b><br/>y: <b>{point.y}</b><br/>", followPointer: !0 }, stickyTracking: !1 });
    La = ba(V, { type: "scatter", sorted: !1, requireSorting: !1, noSharedTooltip: !0, trackerGroups: ["markerGroup"], takeOrdinalPosition: !1, drawTracker: X.prototype.drawTracker, setTooltipPoints: pa });
    N.scatter = La;
    Q.pie = u(P, {
        borderColor: "#FFFFFF",
        borderWidth: 1,
        center: [null, null],
        clip: !1,
        colorByPoint: !0,
        dataLabels: { distance: 30, enabled: !0, formatter: function() { return this.point.name; } },
        ignoreHiddenPoint: !0,
        legendType: "point",
        marker: null,
        size: null,
        showInLegend: !1,
        slicedOffset: 10,
        states: { hover: { brightness: 0.1, shadow: !1 } },
        stickyTracking: !1,
        tooltip: { followPointer: !0 }
    });
    P = {
        type: "pie",
        isCartesian: !1,
        pointClass: ba(Ka, {
            init: function() {
                Ka.prototype.init.apply(this, arguments);
                var a = this, b;
                if (a.y < 0) a.y = null;
                x(a, { visible: a.visible !== !1, name: o(a.name, "Slice") });
                b = function(b) { a.slice(b.type === "select"); };
                E(a, "select", b);
                E(a, "unselect", b);
                return a;
            },
            setVisible: function(a) {
                var b = this, c = b.series, d = c.chart, e;
                b.visible = b.options.visible = a = a === s ? !b.visible : a;
                c.options.data[xa(b, c.data)] = b.options;
                e = a ? "show" : "hide";
                q(["graphic", "dataLabel", "connector", "shadowGroup"], function(a) { if (b[a]) b[a][e](); });
                b.legendItem && d.legend.colorizeItem(b, a);
                if (!c.isDirty && c.options.ignoreHiddenPoint) c.isDirty = !0, d.redraw();
            },
            slice: function(a, b, c) {
                var d = this.series;
                ab(c, d.chart);
                o(b, !0);
                this.sliced = this.options.sliced = a = r(a) ?
                    a : !this.sliced;
                d.options.data[xa(this, d.data)] = this.options;
                a = a ? this.slicedTranslation : { translateX: 0, translateY: 0 };
                this.graphic.animate(a);
                this.shadowGroup && this.shadowGroup.animate(a);
            }
        }),
        requireSorting: !1,
        noSharedTooltip: !0,
        trackerGroups: ["group", "dataLabelsGroup"],
        pointAttrToOptions: { stroke: "borderColor", "stroke-width": "borderWidth", fill: "color" },
        getColor: pa,
        animate: function(a) {
            var b = this, c = b.points, d = b.startAngleRad;
            if (!a)
                q(c, function(a) {
                    var c = a.graphic, a = a.shapeArgs;
                    c && (c.attr({
                        r: b.center[3] /
                            2,
                        start: d,
                        end: d
                    }), c.animate({ r: a.r, start: a.start, end: a.end }, b.options.animation));
                }), b.animate = null;
        },
        setData: function(a, b) {
            V.prototype.setData.call(this, a, !1);
            this.processData();
            this.generatePoints();
            o(b, !0) && this.chart.redraw();
        },
        generatePoints: function() {
            var a, b = 0, c, d, e, f = this.options.ignoreHiddenPoint;
            V.prototype.generatePoints.call(this);
            c = this.points;
            d = c.length;
            for (a = 0; a < d; a++) e = c[a], b += f && !e.visible ? 0 : e.y;
            this.total = b;
            for (a = 0; a < d; a++) e = c[a], e.percentage = b > 0 ? e.y / b * 100 : 0, e.total = b;
        },
        getCenter: function() {
            var a =
                    this.options, b = this.chart, c = 2 * (a.slicedOffset || 0), d, e = b.plotWidth - 2 * c, f = b.plotHeight - 2 * c, b = a.center, a = [o(b[0], "50%"), o(b[1], "50%"), a.size || "100%", a.innerSize || 0], g = z(e, f), h;
            return Ja(a, function(a, b) {
                h = /%$/.test(a);
                d = b < 2 || b === 2 && h;
                return (h ? [e, f, g, g][b] * B(a) / 100 : a) + (d ? c : 0);
            });
        },
        translate: function(a) {
            this.generatePoints();
            var b = 0, c = this.options, d = c.slicedOffset, e = d + c.borderWidth, f, g, h, i = c.startAngle || 0, k = this.startAngleRad = Ha / 180 * (i - 90), i = (this.endAngleRad = Ha / 180 * ((c.endAngle || i + 360) - 90)) - k, j = this.points,
                l = c.dataLabels.distance, c = c.ignoreHiddenPoint, m, p = j.length, n;
            if (!a) this.center = a = this.getCenter();
            this.getX = function(b, c) {
                h = S.asin((b - a[1]) / (a[2] / 2 + l));
                return a[0] + (c ? -1 : 1) * ca(h) * (a[2] / 2 + l);
            };
            for (m = 0; m < p; m++) {
                n = j[m];
                f = k + b * i;
                if (!c || n.visible) b += n.percentage / 100;
                g = k + b * i;
                n.shapeType = "arc";
                n.shapeArgs = { x: a[0], y: a[1], r: a[2] / 2, innerR: a[3] / 2, start: t(f * 1E3) / 1E3, end: t(g * 1E3) / 1E3 };
                h = (g + f) / 2;
                h > 0.75 * i && (h -= 2 * Ha);
                n.slicedTranslation = { translateX: t(ca(h) * d), translateY: t(ha(h) * d) };
                f = ca(h) * a[2] / 2;
                g = ha(h) * a[2] / 2;
                n.tooltipPos =
                    [a[0] + f * 0.7, a[1] + g * 0.7];
                n.half = h < -Ha / 2 || h > Ha / 2 ? 1 : 0;
                n.angle = h;
                e = z(e, l / 2);
                n.labelPos = [a[0] + f + ca(h) * l, a[1] + g + ha(h) * l, a[0] + f + ca(h) * e, a[1] + g + ha(h) * e, a[0] + f, a[1] + g, l < 0 ? "center" : n.half ? "right" : "left", h];
            }
        },
        setTooltipPoints: pa,
        drawGraph: null,
        drawPoints: function() {
            var a = this, b = a.chart.renderer, c, d, e = a.options.shadow, f, g;
            if (e && !a.shadowGroup) a.shadowGroup = b.g("shadow").add(a.group);
            q(a.points, function(h) {
                d = h.graphic;
                g = h.shapeArgs;
                f = h.shadowGroup;
                if (e && !f) f = h.shadowGroup = b.g("shadow").add(a.shadowGroup);
                c = h.sliced ?
                    h.slicedTranslation : { translateX: 0, translateY: 0 };
                f && f.attr(c);
                d ? d.animate(x(g, c)) : h.graphic = d = b.arc(g).setRadialReference(a.center).attr(h.pointAttr[h.selected ? "select" : ""]).attr({ "stroke-linejoin": "round" }).attr(c).add(a.group).shadow(e, f);
                h.visible === !1 && h.setVisible(!1);
            });
        },
        sortByAngle: function(a, b) { a.sort(function(a, d) { return a.angle !== void 0 && (d.angle - a.angle) * b; }); },
        drawDataLabels: function() {
            var a = this, b = a.data, c, d = a.chart, e = a.options.dataLabels, f = o(e.connectorPadding, 10), g = o(e.connectorWidth, 1),
                h = d.plotWidth, d = d.plotHeight, i, k, j = o(e.softConnector, !0), l = e.distance, m = a.center, p = m[2] / 2, n = m[1], s = l > 0, w, y, r, u, x = [[], []], z, A, F, B, I, C = [0, 0, 0, 0], E = function(a, b) { return b.y - a.y; };
            if (a.visible && (e.enabled || a._hasPointLabels)) {
                V.prototype.drawDataLabels.apply(a);
                q(b, function(a) { a.dataLabel && x[a.half].push(a); });
                for (B = 0; !u && b[B];) u = b[B] && b[B].dataLabel && (b[B].dataLabel.getBBox().height || 21), B++;
                for (B = 2; B--;) {
                    var b = [], J = [], H = x[B], G = H.length, D;
                    a.sortByAngle(H, B - 0.5);
                    if (l > 0) {
                        for (I = n - p - l; I <= n + p + l; I += u) b.push(I);
                        y = b.length;
                        if (G > y) {
                            c = [].concat(H);
                            c.sort(E);
                            for (I = G; I--;) c[I].rank = I;
                            for (I = G; I--;) H[I].rank >= y && H.splice(I, 1);
                            G = H.length;
                        }
                        for (I = 0; I < G; I++) {
                            c = H[I];
                            r = c.labelPos;
                            c = 9999;
                            var L, K;
                            for (K = 0; K < y; K++) L = M(b[K] - r[1]), L < c && (c = L, D = K);
                            if (D < I && b[I] !== null) D = I;
                            else for (y < G - I + D && b[I] !== null && (D = y - G + I); b[D] === null;) D++;
                            J.push({ i: D, y: b[D] });
                            b[D] = null;
                        }
                        J.sort(E);
                    }
                    for (I = 0; I < G; I++) {
                        c = H[I];
                        r = c.labelPos;
                        w = c.dataLabel;
                        F = c.visible === !1 ? "hidden" : "visible";
                        c = r[1];
                        if (l > 0) {
                            if (y = J.pop(), D = y.i, A = y.y, c > A && b[D + 1] !== null || c < A && b[D - 1] !==
                                null) A = c;
                        } else A = c;
                        z = e.justify ? m[0] + (B ? -1 : 1) * (p + l) : a.getX(D === 0 || D === b.length - 1 ? c : A, B);
                        w._attr = { visibility: F, align: r[6] };
                        w._pos = { x: z + e.x + ({ left: f, right: -f }[r[6]] || 0), y: A + e.y - 10 };
                        w.connX = z;
                        w.connY = A;
                        if (this.options.size === null) y = w.width, z - y < f ? C[3] = v(t(y - z + f), C[3]) : z + y > h - f && (C[1] = v(t(z + y - h + f), C[1])), A - u / 2 < 0 ? C[0] = v(t(-A + u / 2), C[0]) : A + u / 2 > d && (C[2] = v(t(A + u / 2 - d), C[2]));
                    }
                }
                if (va(C) === 0 || this.verifyDataLabelOverflow(C))
                    this.placeDataLabels(), s && g && q(this.points, function(b) {
                        i = b.connector;
                        r = b.labelPos;
                        if ((w = b.dataLabel) &&
                            w._pos) F = w._attr.visibility, z = w.connX, A = w.connY, k = j ? ["M", z + (r[6] === "left" ? 5 : -5), A, "C", z, A, 2 * r[2] - r[4], 2 * r[3] - r[5], r[2], r[3], "L", r[4], r[5]] : ["M", z + (r[6] === "left" ? 5 : -5), A, "L", r[2], r[3], "L", r[4], r[5]], i ? (i.animate({ d: k }), i.attr("visibility", F)) : b.connector = i = a.chart.renderer.path(k).attr({ "stroke-width": g, stroke: e.connectorColor || b.color || "#606060", visibility: F }).add(a.group);
                        else if (i) b.connector = i.destroy();
                    });
            }
        },
        verifyDataLabelOverflow: function(a) {
            var b = this.center, c = this.options, d = c.center, e = c = c.minSize ||
                80, f;
            d[0] !== null ? e = v(b[2] - v(a[1], a[3]), c) : (e = v(b[2] - a[1] - a[3], c), b[0] += (a[3] - a[1]) / 2);
            d[1] !== null ? e = v(z(e, b[2] - v(a[0], a[2])), c) : (e = v(z(e, b[2] - a[0] - a[2]), c), b[1] += (a[0] - a[2]) / 2);
            e < b[2] ? (b[2] = e, this.translate(b), q(this.points, function(a) { if (a.dataLabel) a.dataLabel._pos = null; }), this.drawDataLabels()) : f = !0;
            return f;
        },
        placeDataLabels: function() {
            q(this.points, function(a) {
                var a = a.dataLabel, b;
                if (a) (b = a._pos) ? (a.attr(a._attr), a[a.moved ? "animate" : "attr"](b), a.moved = !0) : a && a.attr({ y: -999 });
            });
        },
        alignDataLabel: pa,
        drawTracker: X.prototype.drawTracker,
        drawLegendSymbol: D.prototype.drawLegendSymbol,
        getSymbol: pa
    };
    P = ba(V, P);
    N.pie = P;
    var R = V.prototype, ic = R.processData, jc = R.generatePoints, kc = R.destroy, lc = R.tooltipHeaderFormatter, mc = {
        approximation: "average",
        groupPixelWidth: 2,
        dateTimeLabelFormats: lb(mb, ["%A, %b %e, %H:%M:%S.%L", "%A, %b %e, %H:%M:%S.%L", "-%H:%M:%S.%L"], eb, ["%A, %b %e, %H:%M:%S", "%A, %b %e, %H:%M:%S", "-%H:%M:%S"], Za, ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M", "-%H:%M"], Da, ["%A, %b %e, %H:%M", "%A, %b %e, %H:%M",
            "-%H:%M"], da, ["%A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], Pa, ["Week from %A, %b %e, %Y", "%A, %b %e", "-%A, %b %e, %Y"], Qa, ["%B %Y", "%B", "-%B %Y"], ua, ["%Y", "%Y", "-%Y"])
    }, $b = { line: {}, spline: {}, area: {}, areaspline: {}, column: { approximation: "sum", groupPixelWidth: 10 }, arearange: { approximation: "range" }, areasplinerange: { approximation: "range" }, columnrange: { approximation: "range", groupPixelWidth: 10 }, candlestick: { approximation: "ohlc", groupPixelWidth: 10 }, ohlc: { approximation: "ohlc", groupPixelWidth: 5 } }, ac = [[mb, [1,
        2, 5, 10, 20, 25, 50, 100, 200, 500]], [eb, [1, 2, 5, 10, 15, 30]], [Za, [1, 2, 5, 10, 15, 30]], [Da, [1, 2, 3, 4, 6, 8, 12]], [da, [1]], [Pa, [1]], [Qa, [1, 3, 6]], [ua, null]], Ma = {
            sum: function(a) {
                var b = a.length, c;
                if (!b && a.hasNulls) c = null;
                else if (b) for (c = 0; b--;) c += a[b];
                return c;
            },
            average: function(a) {
                var b = a.length, a = Ma.sum(a);
                typeof a === "number" && b && (a /= b);
                return a;
            },
            open: function(a) { return a.length ? a[0] : a.hasNulls ? null : s; },
            high: function(a) { return a.length ? va(a) : a.hasNulls ? null : s; },
            low: function(a) { return a.length ? Sa(a) : a.hasNulls ? null : s; },
            close: function(a) { return a.length ? a[a.length - 1] : a.hasNulls ? null : s; },
            ohlc: function(a, b, c, d) {
                a = Ma.open(a);
                b = Ma.high(b);
                c = Ma.low(c);
                d = Ma.close(d);
                if (typeof a === "number" || typeof b === "number" || typeof c === "number" || typeof d === "number") return [a, b, c, d];
            },
            range: function(a, b) {
                a = Ma.low(a);
                b = Ma.high(b);
                if (typeof a === "number" || typeof b === "number") return [a, b];
            }
        };
    R.groupData = function(a, b, c, d) {
        var e = this.data, f = this.options.data, g = [], h = [], i = a.length, k, j, l = !!b, m = [[], [], [], []], d = typeof d === "function" ? d : Ma[d], p = this.pointArrayMap,
            n = p && p.length, o;
        for (o = 0; o <= i; o++) {
            for (; c[1] !== s && a[o] >= c[1] || o === i;) if (k = c.shift(), j = d.apply(0, m), j !== s && (g.push(k), h.push(j)), m[0] = [], m[1] = [], m[2] = [], m[3] = [], o === i) break;
            if (o === i) break;
            if (p) {
                k = this.cropStart + o;
                k = e && e[k] || this.pointClass.prototype.applyOptions.apply({ series: this }, [f[k]]);
                var q;
                for (j = 0; j < n; j++)
                    if (q = k[p[j]], typeof q === "number") m[j].push(q);
                    else if (q === null) m[j].hasNulls = !0;
            } else if (k = l ? b[o] : null, typeof k === "number") m[0].push(k);
            else if (k === null) m[0].hasNulls = !0;
        }
        return [g, h];
    };
    R.processData =
        function() {
            var a = this.chart, b = this.options, c = b.dataGrouping, d = c && o(c.enabled, a.options._stock), e;
            this.forceCrop = d;
            if (ic.apply(this, arguments) !== !1 && d) {
                this.destroyGroupedData();
                var f = this.processedXData, g = this.processedYData, h = a.plotSizeX, i = this.xAxis, k = this.groupPixelWidth = i.getGroupPixelWidth && i.getGroupPixelWidth(), a = this.pointRange;
                if (k) {
                    e = !0;
                    this.points = null;
                    var d = i.getExtremes(), a = d.min, d = d.max, j = i.getGroupIntervalFactor && i.getGroupIntervalFactor(a, d, f) || 1, h = k * (d - a) / h * j, i = (i.getNonLinearTimeTicks ||
                        fb)(Ab(h, c.units || ac), a, d, null, f, this.closestPointRange), g = R.groupData.apply(this, [f, g, i, c.approximation]), f = g[0], g = g[1];
                    if (c.smoothed) {
                        c = f.length - 1;
                        for (f[c] = d; c-- && c > 0;) f[c] += h / 2;
                        f[0] = a;
                    }
                    this.currentDataGrouping = i.info;
                    if (b.pointRange === null) this.pointRange = i.info.totalRange;
                    this.closestPointRange = i.info.totalRange;
                    this.processedXData = f;
                    this.processedYData = g;
                } else this.currentDataGrouping = null, this.pointRange = a;
                this.hasGroupedData = e;
            }
        };
    R.destroyGroupedData = function() {
        var a = this.groupedData;
        q(a || [],
            function(b, c) { b && (a[c] = b.destroy ? b.destroy() : null); });
        this.groupedData = null;
    };
    R.generatePoints = function() {
        jc.apply(this);
        this.destroyGroupedData();
        this.groupedData = this.hasGroupedData ? this.points : null;
    };
    R.tooltipHeaderFormatter = function(a) {
        var b = this.tooltipOptions, c = this.options.dataGrouping, d = b.xDateFormat, e, f = this.xAxis, g, h;
        if (f && f.options.type === "datetime" && c && sa(a.key)) {
            g = this.currentDataGrouping;
            c = c.dateTimeLabelFormats;
            if (g) f = c[g.unitName], g.count === 1 ? d = f[0] : (d = f[1], e = f[2]);
            else if (!d && c)
                for (h in H)
                    if (H[h] >=
                        f.closestPointRange) {
                        d = c[h][0];
                        break;
                    }
            d = qa(d, a.key);
            e && (d += qa(e, a.key + g.totalRange - 1));
            a = b.headerFormat.replace("{point.key}", d);
        } else a = lc.call(this, a);
        return a;
    };
    R.destroy = function() {
        for (var a = this.groupedData || [], b = a.length; b--;) a[b] && a[b].destroy();
        kc.apply(this);
    };
    ga(R, "setOptions", function(a, b) {
        var c = a.call(this, b), d = this.type, e = this.chart.options.plotOptions, f = Q[d].dataGrouping;
        if ($b[d]) f || (f = u(mc, $b[d])), c.dataGrouping = u(f, e.series && e.series.dataGrouping, e[d].dataGrouping, b.dataGrouping);
        if (this.chart.options._stock)
            this.requireSorting =
                !0;
        return c;
    });
    oa.prototype.getGroupPixelWidth = function() {
        var a = this.series, b = a.length, c, d = 0, e = !1, f;
        for (c = b; c--;) (f = a[c].options.dataGrouping) && (d = v(d, f.groupPixelWidth));
        for (c = b; c--;) if (f = a[c].options.dataGrouping) if (b = (a[c].processedXData || a[c].data).length, a[c].groupPixelWidth || b > this.chart.plotSizeX / d || b && f.forced) e = !0;
        return e ? d : 0;
    };
    Q.ohlc = u(Q.column, {
        lineWidth: 1,
        tooltip: { pointFormat: '<span style="color:{series.color};font-weight:bold">{series.name}</span><br/>Open: {point.open}<br/>High: {point.high}<br/>Low: {point.low}<br/>Close: {point.close}<br/>' },
        states: { hover: { lineWidth: 3 } },
        threshold: null
    });
    P = ba(N.column, {
        type: "ohlc",
        pointArrayMap: ["open", "high", "low", "close"],
        toYData: function(a) { return [a.open, a.high, a.low, a.close]; },
        pointValKey: "high",
        pointAttrToOptions: { stroke: "color", "stroke-width": "lineWidth" },
        upColorProp: "stroke",
        getAttribs: function() {
            N.column.prototype.getAttribs.apply(this, arguments);
            var a = this.options, b = a.states, a = a.upColor || this.color, c = u(this.pointAttr), d = this.upColorProp;
            c[""][d] = a;
            c.hover[d] = b.hover.upColor || a;
            c.select[d] = b.select.upColor ||
                a;
            q(this.points, function(a) { if (a.open < a.close) a.pointAttr = c; });
        },
        translate: function() {
            var a = this.yAxis;
            N.column.prototype.translate.apply(this);
            q(this.points, function(b) {
                if (b.open !== null) b.plotOpen = a.translate(b.open, 0, 1, 0, 1);
                if (b.close !== null) b.plotClose = a.translate(b.close, 0, 1, 0, 1);
            });
        },
        drawPoints: function() {
            var a = this, b = a.chart, c, d, e, f, g, h, i, k;
            q(a.points, function(j) {
                if (j.plotY !== s)
                    i = j.graphic, c = j.pointAttr[j.selected ? "selected" : ""], f = c["stroke-width"] % 2 / 2, k = t(j.plotX) + f, g = t(j.shapeArgs.width / 2), h =
                        ["M", k, t(j.yBottom), "L", k, t(j.plotY)], j.open !== null && (d = t(j.plotOpen) + f, h.push("M", k, d, "L", k - g, d)), j.close !== null && (e = t(j.plotClose) + f, h.push("M", k, e, "L", k + g, e)), i ? i.animate({ d: h }) : j.graphic = b.renderer.path(h).attr(c).add(a.group);
            });
        },
        animate: null
    });
    N.ohlc = P;
    Q.candlestick = u(Q.column, { lineColor: "black", lineWidth: 1, states: { hover: { lineWidth: 2 } }, tooltip: Q.ohlc.tooltip, threshold: null, upColor: "white" });
    P = ba(P, {
        type: "candlestick",
        pointAttrToOptions: { fill: "color", stroke: "lineColor", "stroke-width": "lineWidth" },
        upColorProp: "fill",
        getAttribs: function() {
            N.ohlc.prototype.getAttribs.apply(this, arguments);
            var a = this.options, b = a.states, c = a.upLineColor || a.lineColor, d = b.hover.upLineColor || c, e = b.select.upLineColor || c;
            q(this.points, function(a) { if (a.open < a.close) a.pointAttr[""].stroke = c, a.pointAttr.hover.stroke = d, a.pointAttr.select.stroke = e; });
        },
        drawPoints: function() {
            var a = this, b = a.chart, c, d, e, f, g, h, i, k, j, l, m, p;
            q(a.points, function(n) {
                l = n.graphic;
                if (n.plotY !== s)
                    c = n.pointAttr[n.selected ? "selected" : ""], k = c["stroke-width"] %
                        2 / 2, j = t(n.plotX) + k, d = n.plotOpen, e = n.plotClose, f = S.min(d, e), g = S.max(d, e), p = t(n.shapeArgs.width / 2), h = t(f) !== t(n.plotY), i = g !== n.yBottom, f = t(f) + k, g = t(g) + k, m = ["M", j - p, g, "L", j - p, f, "L", j + p, f, "L", j + p, g, "L", j - p, g, "M", j, f, "L", j, h ? t(n.plotY) : f, "M", j, g, "L", j, i ? t(n.yBottom) : g, "Z"], l ? l.animate({ d: m }) : n.graphic = b.renderer.path(m).attr(c).add(a.group).shadow(a.options.shadow);
            });
        }
    });
    N.candlestick = P;
    var xb = za.prototype.symbols;
    Q.flags = u(Q.column, {
        dataGrouping: null,
        fillColor: "white",
        lineWidth: 1,
        pointRange: 0,
        shape: "flag",
        stackDistance: 12,
        states: { hover: { lineColor: "black", fillColor: "#FCFFC5" } },
        style: { fontSize: "11px", fontWeight: "bold", textAlign: "center" },
        tooltip: { pointFormat: "{point.text}<br/>" },
        threshold: null,
        y: -30
    });
    N.flags = ba(N.column, {
        type: "flags",
        sorted: !1,
        noSharedTooltip: !0,
        takeOrdinalPosition: !1,
        forceCrop: !0,
        init: V.prototype.init,
        pointAttrToOptions: { fill: "fillColor", stroke: "color", "stroke-width": "lineWidth", r: "radius" },
        translate: function() {
            N.column.prototype.translate.apply(this);
            var a = this.chart, b = this.points,
                c = b.length - 1, d, e, f = this.options.onSeries, f = (d = f && a.get(f)) && d.options.step, g = d && d.points, h = g && g.length, i = this.xAxis, k = i.getExtremes(), j, l, m;
            if (d && d.visible && h) {
                l = g[h - 1].x;
                for (b.sort(function(a, b) { return a.x - b.x; }); h-- && b[c];)
                    if (d = b[c], j = g[h], j.x <= d.x && j.plotY !== s) {
                        if (d.x <= l) d.plotY = j.plotY, j.x < d.x && !f && (m = g[h + 1]) && m.plotY !== s && (d.plotY += (d.x - j.x) / (m.x - j.x) * (m.plotY - j.plotY));
                        c--;
                        h++;
                        if (c < 0) break;
                    }
            }
            q(b, function(c, d) {
                if (c.plotY === s)
                    c.x >= k.min && c.x <= k.max ? c.plotY = a.chartHeight - i.bottom - (i.opposite ? i.height :
                        0) + i.offset - a.plotTop : c.shapeArgs = {};
                if ((e = b[d - 1]) && e.plotX === c.plotX) {
                    if (e.stackIndex === s) e.stackIndex = 0;
                    c.stackIndex = e.stackIndex + 1;
                }
            });
        },
        drawPoints: function() {
            var a, b = this.points, c = this.chart.renderer, d, e, f = this.options, g = f.y, h, i, k, j, l = f.lineWidth % 2 / 2, m, p;
            for (i = b.length; i--;)
                if (k = b[i], d = k.plotX + l, a = k.stackIndex, h = k.options.shape || f.shape, e = k.plotY, e !== s && (e = k.plotY + g + l - (a !== s && a * f.stackDistance)), m = a ? s : k.plotX + l, p = a ? s : k.plotY, j = k.graphic, e !== s && d >= 0 && d < this.xAxis.len)
                    a = k.pointAttr[k.selected ? "select" :
                        ""], j ? j.attr({ x: d, y: e, r: a.r, anchorX: m, anchorY: p }) : j = k.graphic = c.label(k.options.title || f.title || "A", d, e, h, m, p, f.useHTML).css(u(f.style, k.style)).attr(a).attr({ align: h === "flag" ? "left" : "center", width: f.width, height: f.height }).add(this.group).shadow(f.shadow), h = j.box, h.getBBox(), k.tooltipPos = [d, e];
                else if (j) k.graphic = j.destroy();
        },
        drawTracker: function() {
            var a = this.points;
            N.column.prototype.drawTracker.apply(this);
            q(a, function(b) {
                var c = b.graphic;
                c && E(c.element, "mouseover", function() {
                    if (b.stackIndex > 0 &&
                        !b.raised) b._y = c.y, c.attr({ y: b._y - 8 }), b.raised = !0;
                    q(a, function(a) { if (a !== b && a.raised && a.graphic) a.graphic.attr({ y: a._y }), a.raised = !1; });
                });
            });
        },
        animate: pa
    });
    xb.flag = function(a, b, c, d, e) {
        var f = e && e.anchorX || a, e = e && e.anchorY || b;
        return ["M", f, e, "L", a, b + d, a, b, a + c, b, a + c, b + d, a, b + d, "M", f, e, "Z"];
    };
    q(["circle", "square"], function(a) {
        xb[a + "pin"] = function(b, c, d, e, f) {
            var g = f && f.anchorX, f = f && f.anchorY, b = xb[a](b, c, d, e);
            g && f && b.push("M", g, c > f ? c : c + e, "L", g, f);
            return b;
        };
    });
    cb === kb && q(["flag", "circlepin", "squarepin"], function(a) {
        kb.prototype.symbols[a] =
            xb[a];
    });
    P = lb("linearGradient", { x1: 0, y1: 0, x2: 0, y2: 1 }, "stops", [[0, "#FFF"], [1, "#CCC"]]);
    D = [].concat(ac);
    D[4] = [da, [1, 2, 3, 4]];
    D[5] = [Pa, [1, 2, 3]];
    x(K, {
        navigator: {
            handles: { backgroundColor: "#FFF", borderColor: "#666" },
            height: 40,
            margin: 10,
            maskFill: "rgba(255, 255, 255, 0.75)",
            outlineColor: "#444",
            outlineWidth: 1,
            series: {
                type: "areaspline",
                color: "#4572A7",
                compare: null,
                fillOpacity: 0.4,
                dataGrouping: { approximation: "average", enabled: !0, groupPixelWidth: 2, smoothed: !0, units: D },
                dataLabels: { enabled: !1, zIndex: 2 },
                id: "highcharts-navigator-series",
                lineColor: "#4572A7",
                lineWidth: 1,
                marker: { enabled: !1 },
                pointRange: 0,
                shadow: !1,
                threshold: null
            },
            xAxis: { tickWidth: 0, lineWidth: 0, gridLineWidth: 1, tickPixelInterval: 200, labels: { align: "left", x: 3, y: -4 } },
            yAxis: { gridLineWidth: 0, startOnTick: !1, endOnTick: !1, minPadding: 0.1, maxPadding: 0.1, labels: { enabled: !1 }, title: { text: null }, tickWidth: 0 }
        },
        scrollbar: {
            height: hb ? 20 : 14,
            barBackgroundColor: P,
            barBorderRadius: 2,
            barBorderWidth: 1,
            barBorderColor: "#666",
            buttonArrowColor: "#666",
            buttonBackgroundColor: P,
            buttonBorderColor: "#666",
            buttonBorderRadius: 2,
            buttonBorderWidth: 1,
            minWidth: 6,
            rifleColor: "#666",
            trackBackgroundColor: lb("linearGradient", { x1: 0, y1: 0, x2: 0, y2: 1 }, "stops", [[0, "#EEE"], [1, "#FFF"]]),
            trackBorderColor: "#CCC",
            trackBorderWidth: 1,
            liveRedraw: aa && !hb
        }
    });
    Ib.prototype = {
        drawHandle: function(a, b) {
            var c = this.chart, d = c.renderer, e = this.elementsToDestroy, f = this.handles, g = this.navigatorOptions.handles, g = { fill: g.backgroundColor, stroke: g.borderColor, "stroke-width": 1 }, h;
            this.rendered || (f[b] = d.g().css({ cursor: "e-resize" }).attr({
                zIndex: 4 -
                    b
            }).add(), h = d.rect(-4.5, 0, 9, 16, 3, 1).attr(g).add(f[b]), e.push(h), h = d.path(["M", -1.5, 4, "L", -1.5, 12, "M", 0.5, 4, "L", 0.5, 12]).attr(g).add(f[b]), e.push(h));
            f[b][c.isResizing ? "animate" : "attr"]({ translateX: this.scrollerLeft + this.scrollbarHeight + parseInt(a, 10), translateY: this.top + this.height / 2 - 8 });
        },
        drawScrollbarButton: function(a) {
            var b = this.chart.renderer, c = this.elementsToDestroy, d = this.scrollbarButtons, e = this.scrollbarHeight, f = this.scrollbarOptions, g;
            this.rendered || (d[a] = b.g().add(this.scrollbarGroup), g = b.rect(-0.5,
                -0.5, e + 1, e + 1, f.buttonBorderRadius, f.buttonBorderWidth).attr({ stroke: f.buttonBorderColor, "stroke-width": f.buttonBorderWidth, fill: f.buttonBackgroundColor }).add(d[a]), c.push(g), g = b.path(["M", e / 2 + (a ? -1 : 1), e / 2 - 3, "L", e / 2 + (a ? -1 : 1), e / 2 + 3, e / 2 + (a ? 2 : -2), e / 2]).attr({ fill: f.buttonArrowColor }).add(d[a]), c.push(g));
            a && d[a].attr({ translateX: this.scrollerWidth - e });
        },
        render: function(a, b, c, d) {
            var e = this.chart, f = e.renderer, g, h, i, k, j = this.scrollbarGroup, l = this.navigatorGroup, m = this.scrollbar, l = this.xAxis, p = this.scrollbarTrack,
                n = this.scrollbarHeight, q = this.scrollbarEnabled, w = this.navigatorOptions, y = this.scrollbarOptions, r = y.minWidth, s = this.height, u = this.top, x = this.navigatorEnabled, A = w.outlineWidth, F = A / 2, C = 0, I = this.outlineHeight, D = y.barBorderRadius, H = y.barBorderWidth, E = u + F, G;
            if (!isNaN(a)) {
                this.navigatorLeft = g = o(l.left, e.plotLeft + n);
                this.navigatorWidth = h = o(l.len, e.plotWidth - 2 * n);
                this.scrollerLeft = i = g - n;
                this.scrollerWidth = k = k = h + 2 * n;
                l.getExtremes && (G = this.getUnionExtremes(!0)) && (G.dataMin !== l.min || G.dataMax !== l.max) && l.setExtremes(G.dataMin,
                    G.dataMax, !0, !1);
                c = o(c, l.translate(a));
                d = o(d, l.translate(b));
                if (isNaN(c) || M(c) === Infinity) c = 0, d = k;
                this.zoomedMax = a = z(B(v(c, d)), h);
                this.zoomedMin = d = v(this.fixedWidth ? a - this.fixedWidth : B(z(c, d)), 0);
                this.range = c = a - d;
                if (!this.rendered) {
                    if (x) this.navigatorGroup = l = f.g("navigator").attr({ zIndex: 3 }).add(), this.leftShade = f.rect().attr({ fill: w.maskFill }).add(l), this.rightShade = f.rect().attr({ fill: w.maskFill }).add(l), this.outline = f.path().attr({ "stroke-width": A, stroke: w.outlineColor }).add(l);
                    if (q)
                        this.scrollbarGroup =
                            j = f.g("scrollbar").add(), m = y.trackBorderWidth, this.scrollbarTrack = p = f.rect().attr({ y: -m % 2 / 2, fill: y.trackBackgroundColor, stroke: y.trackBorderColor, "stroke-width": m, r: y.trackBorderRadius || 0, height: n }).add(j), this.scrollbar = m = f.rect().attr({ y: -H % 2 / 2, height: n, fill: y.barBackgroundColor, stroke: y.barBorderColor, "stroke-width": H, r: D }).add(j), this.scrollbarRifles = f.path().attr({ stroke: y.rifleColor, "stroke-width": 1 }).add(j);
                }
                e = e.isResizing ? "animate" : "attr";
                x && (this.leftShade[e]({ x: g, y: u, width: d, height: s }), this.rightShade[e]({
                    x: g +
                        a,
                    y: u,
                    width: h - a,
                    height: s
                }), this.outline[e]({ d: ["M", i, E, "L", g + d + F, E, g + d + F, E + I - n, "M", g + a - F, E + I - n, "L", g + a - F, E, i + k, E] }), this.drawHandle(d + F, 0), this.drawHandle(a + F, 1));
                if (q && j)
                    this.drawScrollbarButton(0), this.drawScrollbarButton(1), j[e]({ translateX: i, translateY: t(E + s) }), p[e]({ width: k }), g = n + d, h = c - H, h < r && (C = (r - h) / 2, h = r, g -= C), this.scrollbarPad = C, m[e]({ x: O(g) + H % 2 / 2, width: h }), r = n + d + c / 2 - 0.5, this.scrollbarRifles.attr({ visibility: c > 12 ? "visible" : "hidden" })[e]({
                        d: ["M", r - 3, n / 4, "L", r - 3, 2 * n / 3, "M", r, n / 4, "L", r, 2 * n / 3,
                            "M", r + 3, n / 4, "L", r + 3, 2 * n / 3]
                    });
                this.scrollbarPad = C;
                this.rendered = !0;
            }
        },
        addEvents: function() {
            var a = this.chart.container, b = this.mouseDownHandler, c = this.mouseMoveHandler, d = this.mouseUpHandler, e;
            e = [[a, "mousedown", b], [a, "mousemove", c], [document, "mouseup", d]];
            ib && e.push([a, "touchstart", b], [a, "touchmove", c], [document, "touchend", d]);
            q(e, function(a) { E.apply(null, a); });
            this._events = e;
        },
        removeEvents: function() {
            q(this._events, function(a) { U.apply(null, a); });
            this._events = s;
            this.navigatorEnabled && this.baseSeries && U(this.baseSeries,
                "updatedData", this.updatedDataHandler);
        },
        init: function() {
            var a = this, b = a.chart, c, d, e = a.scrollbarHeight, f = a.navigatorOptions, g = a.height, h = a.top, i, k, j = document.body.style, l, m = a.baseSeries;
            a.mouseDownHandler = function(d) {
                var d = b.pointer.normalize(d), e = a.zoomedMin, f = a.zoomedMax, h = a.top, k = a.scrollbarHeight, m = a.scrollerLeft, n = a.scrollerWidth, p = a.navigatorLeft, o = a.navigatorWidth, q = a.scrollbarPad, r = a.range, s = d.chartX, t = d.chartY, d = b.xAxis[0], u, x = hb ? 10 : 7;
                if (t > h && t < h + g + k)
                    if ((h = !a.scrollbarEnabled || t < h + g) && S.abs(s -
                        e - p) < x) a.grabbedLeft = !0, a.otherHandlePos = f, a.fixedExtreme = d.max, b.fixedRange = null;
                    else if (h && S.abs(s - f - p) < x) a.grabbedRight = !0, a.otherHandlePos = e, a.fixedExtreme = d.min, b.fixedRange = null;
                    else if (s > p + e - q && s < p + f + q) {
                        a.grabbedCenter = s;
                        a.fixedWidth = r;
                        if (b.renderer.isSVG) l = j.cursor, j.cursor = "ew-resize";
                        i = s - e;
                    } else if (s > m && s < m + n) {
                        f = h ? s - p - r / 2 : s < p ? e - v(z(10, r), 1) : s > m + n - k ? e + v(z(10, r), 1) : s < p + e ? e - r : f;
                        if (f < 0) f = 0;
                        else if (f + r >= o) f = o - r, u = c.dataMax;
                        if (f !== e)
                            a.fixedWidth = r, e = c.toFixedRange(f, f + r, null, u), d.setExtremes(e.min,
                                e.max, !0, !1, { trigger: "navigator" });
                    }
            };
            a.mouseMoveHandler = function(c) {
                var d = a.scrollbarHeight, e = a.navigatorLeft, f = a.navigatorWidth, g = a.scrollerLeft, h = a.scrollerWidth, j = a.range, l;
                if (c.pageX !== 0)
                    c = b.pointer.normalize(c), l = c.chartX, l < e ? l = e : l > g + h - d && (l = g + h - d), a.grabbedLeft ? (k = !0, a.render(0, 0, l - e, a.otherHandlePos)) : a.grabbedRight ? (k = !0, a.render(0, 0, a.otherHandlePos, l - e)) : a.grabbedCenter && (k = !0, l < i ? l = i : l > f + i - j && (l = f + i - j), a.render(0, 0, l - i, l - i + j)), k && a.scrollbarOptions.liveRedraw && setTimeout(function() { a.mouseUpHandler(c); },
                        0);
            };
            a.mouseUpHandler = function(d) {
                var e, f;
                if (k) {
                    if (a.zoomedMin === a.otherHandlePos) e = a.fixedExtreme;
                    else if (a.zoomedMax === a.otherHandlePos) f = a.fixedExtreme;
                    if (a.zoomedMax === a.navigatorWidth) f = c.dataMax;
                    e = c.toFixedRange(a.zoomedMin, a.zoomedMax, e, f);
                    b.xAxis[0].setExtremes(e.min, e.max, !0, !1, { trigger: "navigator", triggerOp: "navigator-drag", DOMEvent: d });
                }
                if (d.type !== "mousemove") a.grabbedLeft = a.grabbedRight = a.grabbedCenter = a.fixedWidth = a.fixedExtreme = a.otherHandlePos = k = i = null, j.cursor = l || "";
            };
            var p = b.xAxis.length,
                n = b.yAxis.length;
            b.extraBottomMargin = a.outlineHeight + f.margin;
            a.navigatorEnabled ? (a.xAxis = c = new oa(b, u({ ordinal: m && m.xAxis.options.ordinal }, f.xAxis, { id: "navigator-x-axis", isX: !0, type: "datetime", index: p, height: g, offset: 0, offsetLeft: e, offsetRight: -e, startOnTick: !1, endOnTick: !1, minPadding: 0, maxPadding: 0, zoomEnabled: !1 })), a.yAxis = d = new oa(b, u(f.yAxis, { id: "navigator-y-axis", alignTicks: !1, height: g, offset: 0, index: n, zoomEnabled: !1 })), m || f.series.data ? a.addBaseSeries() : b.series.length === 0 && ga(b, "redraw", function(c,
                d) {
                if (b.series.length > 0 && !a.series) a.setBaseSeries(), b.redraw = c;
                c.call(b, d);
            })) : a.xAxis = c = {
                translate: function(a, c) {
                    var d = b.xAxis[0].getExtremes(), f = b.plotWidth - 2 * e, g = d.dataMin, d = d.dataMax - g;
                    return c ? a * d / f + g : f * (a - g) / d;
                },
                toFixedRange: oa.prototype.toFixedRange
            };
            ga(b, "getMargins", function(b) {
                var e = this.legend, f = e.options;
                b.call(this);
                a.top = h = a.navigatorOptions.top || this.chartHeight - a.height - a.scrollbarHeight - this.spacing[2] - (f.verticalAlign === "bottom" && f.enabled && !f.floating ? e.legendHeight + o(f.margin, 10) :
                    0);
                if (c && d) c.options.top = d.options.top = h, c.setAxisSize(), d.setAxisSize();
            });
            a.addEvents();
        },
        getUnionExtremes: function(a) {
            var b = this.chart.xAxis[0], c = this.xAxis, d = c.options;
            if (!a || b.dataMin !== null) return { dataMin: o(d && d.min, (r(b.dataMin) && r(c.dataMin) ? z : o)(b.dataMin, c.dataMin)), dataMax: o(d && d.max, (r(b.dataMax) && r(c.dataMax) ? v : o)(b.dataMax, c.dataMax)) };
        },
        setBaseSeries: function(a) {
            var b = this.chart, a = a || b.options.navigator.baseSeries;
            this.series && this.series.remove();
            this.baseSeries = b.series[a] || typeof a ===
                "string" && b.get(a) || b.series[0];
            this.xAxis && this.addBaseSeries();
        },
        addBaseSeries: function() {
            var a = this.baseSeries, b = a ? a.options : {}, c = b.data, d = this.navigatorOptions.series, e;
            e = d.data;
            this.hasNavigatorData = !!e;
            b = u(b, d, { clip: !1, enableMouseTracking: !1, group: "nav", padXAxis: !1, xAxis: "navigator-x-axis", yAxis: "navigator-y-axis", name: "Navigator", showInLegend: !1, isInternal: !0, visible: !0 });
            b.data = e || c;
            this.series = this.chart.initSeries(b);
            if (a && this.navigatorOptions.adaptToUpdatedData !== !1)
                E(a, "updatedData", this.updatedDataHandler),
                a.userOptions.events = x(a.userOptions.event, { updatedData: this.updatedDataHandler });
        },
        updatedDataHandler: function() {
            var a = this.chart.scroller, b = a.baseSeries, c = b.xAxis, d = c.getExtremes(), e = d.min, f = d.max, g = d.dataMin, d = d.dataMax, h = f - e, i, k, j, l, m, p = a.series;
            i = p.xData;
            var n = !!c.setExtremes;
            k = f >= i[i.length - 1] - (this.closestPointRange || 0);
            i = e <= g;
            if (!a.hasNavigatorData) p.options.pointStart = b.xData[0], p.setData(b.options.data, !1), m = !0;
            i && (l = g, j = l + h);
            k && (j = d, i || (l = v(j - h, p.xData[0])));
            n && (i || k) ? isNaN(l) || c.setExtremes(l,
                j, !0, !1, { trigger: "updatedData" }) : (m && this.chart.redraw(!1), a.render(v(e, g), z(f, d)));
        },
        destroy: function() {
            this.removeEvents();
            q([this.xAxis, this.yAxis, this.leftShade, this.rightShade, this.outline, this.scrollbarTrack, this.scrollbarRifles, this.scrollbarGroup, this.scrollbar], function(a) { a && a.destroy && a.destroy(); });
            this.xAxis = this.yAxis = this.leftShade = this.rightShade = this.outline = this.scrollbarTrack = this.scrollbarRifles = this.scrollbarGroup = this.scrollbar = null;
            q([this.scrollbarButtons, this.handles, this.elementsToDestroy],
                function(a) { Ea(a); });
        }
    };
    Highcharts.Scroller = Ib;
    ga(oa.prototype, "zoom", function(a, b, c) {
        var d = this.chart, e = d.options, f = e.chart.zoomType, g = e.navigator, e = e.rangeSelector, h;
        if (this.isXAxis && (g && g.enabled || e && e.enabled))
            if (f === "x") d.resetZoomButton = "blocked";
            else if (f === "y") h = !1;
            else if (f === "xy") d = this.previousZoom, r(b) ? this.previousZoom = [this.min, this.max] : d && (b = d[0], c = d[1], delete this.previousZoom);
        return h !== s ? h : a.call(this, b, c);
    });
    ga(Va.prototype, "init", function(a, b, c) {
        E(this, "beforeRender", function() {
            var a =
                this.options;
            if (a.navigator.enabled || a.scrollbar.enabled) this.scroller = new Ib(this);
        });
        a.call(this, b, c);
    });
    x(K, { rangeSelector: { buttonTheme: { width: 28, height: 16, padding: 1, r: 0, stroke: "#68A", zIndex: 7 }, inputPosition: { align: "right" }, labelStyle: { color: "#666" } } });
    K.lang = u(K.lang, { rangeSelectorZoom: "Zoom", rangeSelectorFrom: "From", rangeSelectorTo: "To" });
    Jb.prototype = {
        clickButton: function(a, b, c) {
            var d = this, e = d.selected, f = d.chart, g = d.buttons, h = f.xAxis[0], i = f.scroller && f.scroller.getUnionExtremes() || h || {}, k = i.dataMin,
                j = i.dataMax, l, m = h && t(z(h.max, o(j, h.max))), p = new Date(m), n = b.type, r = b.count, i = b._range, w;
            if (!(k === null || j === null || a === d.selected)) {
                if (n === "month" || n === "year") l = { month: "Month", year: "FullYear" }[n], p["set" + l](p["get" + l]() - r), l = p.getTime(), k = o(k, Number.MIN_VALUE), isNaN(l) || l < k ? (l = k, m = z(l + i, j)) : i = m - l;
                else if (i) l = v(m - i, k), m = z(l + i, j);
                else if (n === "ytd")
                    if (h) {
                        if (j === s)
                            k = Number.MAX_VALUE, j = Number.MIN_VALUE, q(f.series, function(a) {
                                a = a.xData;
                                k = z(a[0], k);
                                j = v(a[a.length - 1], j);
                            }), c = !1;
                        m = new Date(j);
                        w = m.getFullYear();
                        l = w = v(k || 0, Date.UTC(w, 0, 1));
                        m = m.getTime();
                        m = z(j || m, m);
                    } else {
                        E(f, "beforeRender", function() { d.clickButton(a, b); });
                        return;
                    }
                else n === "all" && h && (l = k, m = j);
                g[e] && g[e].setState(0);
                g[a] && g[a].setState(2);
                f.fixedRange = i;
                h ? h.setExtremes(l, m, o(c, 1), 0, { trigger: "rangeSelectorButton", rangeSelectorButton: b }) : (c = f.options.xAxis, c[0] = u(c[0], { range: i, min: w }));
                d.selected = a;
            }
        },
        defaultButtons: [{ type: "month", count: 1, text: "1m" }, { type: "month", count: 3, text: "3m" }, { type: "month", count: 6, text: "6m" }, { type: "ytd", text: "YTD" }, {
            type: "year",
            count: 1,
            text: "1y"
        }, { type: "all", text: "All" }],
        init: function(a) {
            var b = this, c = a.options.rangeSelector, d = c.buttons || [].concat(b.defaultButtons), e = b.buttons = [], c = c.selected, f = b.blurInputs = function() {
                var a = b.minInput, c = b.maxInput;
                a && a.blur();
                c && c.blur();
            };
            b.chart = a;
            a.extraTopMargin = 25;
            b.buttonOptions = d;
            E(a.container, "mousedown", f);
            E(a, "resize", f);
            q(d, b.computeButtonRange);
            c !== s && d[c] && this.clickButton(c, d[c], !1);
            E(a, "load", function() {
                E(a.xAxis[0], "afterSetExtremes", function() {
                    if (a.fixedRange !== t(this.max -
                        this.min)) e[b.selected] && !a.renderer.forExport && e[b.selected].setState(0), b.selected = null;
                    b.updateButtonStates();
                });
            });
        },
        updateButtonStates: function() {
            var a = this, b = this.chart, c = b.xAxis[0], b = b.scroller && b.scroller.getUnionExtremes() || c, d = b.dataMin, e = b.dataMax, f = a.selected, g = a.buttons;
            q(a.buttonOptions, function(b, i) {
                var k = b._range, j = k > e - d, l = k < c.minRange, m = b.type === "all" && c.max - c.min >= e - d && g[i].state !== 2, p = b.type === "ytd" && qa("%Y", d) === qa("%Y", e);
                k === t(c.max - c.min) && i !== f ? (a.selected = i, g[i].setState(2)) :
                    j || l || m || p ? g[i].setState(3) : g[i].state === 3 && g[i].setState(0);
            });
        },
        computeButtonRange: function(a) {
            var b = a.type, c = a.count || 1, d = { millisecond: 1, second: 1E3, minute: 6E4, hour: 36E5, day: 864E5, week: 6048E5 };
            if (d[b]) a._range = d[b] * c;
            else if (b === "month" || b === "year") a._range = { month: 30, year: 365 }[b] * 864E5 * c;
        },
        setInputValue: function(a, b) {
            var c = this.chart.options.rangeSelector;
            if (r(b)) this[a + "Input"].HCTime = b;
            this[a + "Input"].value = qa(c.inputEditDateFormat || "%Y-%m-%d", this[a + "Input"].HCTime);
            this[a + "DateBox"].attr({
                text: qa(c.inputDateFormat ||
                    "%b %e, %Y", this[a + "Input"].HCTime)
            });
        },
        drawInput: function(a) {
            var b = this, c = b.chart, d = c.options.chart.style, e = c.renderer, f = c.options.rangeSelector, g = b.div, h = a === "min", i, k, j, l = this.inputGroup;
            this[a + "Label"] = k = e.label(K.lang[h ? "rangeSelectorFrom" : "rangeSelectorTo"], this.inputGroup.offset).attr({ padding: 1 }).css(u(d, f.labelStyle)).add(l);
            l.offset += k.width + 5;
            this[a + "DateBox"] = j = e.label("", l.offset).attr({
                padding: 1,
                width: f.inputBoxWidth || 90,
                height: f.inputBoxHeight || 16,
                stroke: f.inputBoxBorderColor || "silver",
                "stroke-width": 1
            }).css(u({ textAlign: "center" }, d, f.inputStyle)).on("click", function() { b[a + "Input"].focus(); }).add(l);
            l.offset += j.width + (h ? 10 : 0);
            this[a + "Input"] = i = Y("input", { name: a, className: "highcharts-range-selector", type: "text" }, x({ position: "absolute", border: 0, width: "1px", height: "1px", padding: 0, textAlign: "center", fontSize: d.fontSize, fontFamily: d.fontFamily, top: c.plotTop + "px" }, f.inputStyle), g);
            i.onfocus = function() {
                G(this, {
                    left: l.translateX + j.x + "px",
                    top: l.translateY + "px",
                    width: j.width - 2 + "px",
                    height: j.height -
                        2 + "px",
                    border: "2px solid silver"
                });
            };
            i.onblur = function() {
                G(this, { border: 0, width: "1px", height: "1px" });
                b.setInputValue(a);
            };
            i.onchange = function() {
                var a = i.value, d = (f.inputDateParser || Date.parse)(a), e = c.xAxis[0].getExtremes();
                isNaN(d) && (d = a.split("-"), d = Date.UTC(B(d[0]), B(d[1]) - 1, B(d[2])));
                if (!isNaN(d) && (K.global.useUTC || (d += (new Date).getTimezoneOffset() * 6E4), h && d >= e.dataMin && d <= b.maxInput.HCTime || !h && d <= e.dataMax && d >= b.minInput.HCTime)) c.xAxis[0].setExtremes(h ? d : e.min, h ? e.max : d, s, s, { trigger: "rangeSelectorInput" });
            };
        },
        render: function(a, b) {
            var c = this, d = c.chart, e = d.renderer, f = d.container, g = d.options, h = g.exporting && g.navigation && g.navigation.buttonOptions, i = g.rangeSelector, k = c.buttons, g = K.lang, j = c.div, j = c.inputGroup, l = i.buttonTheme, m = i.inputEnabled !== !1, p = l && l.states, n = d.plotLeft, o;
            if (!c.rendered && (c.zoomText = e.text(g.rangeSelectorZoom, n, d.plotTop - 10).css(i.labelStyle).add(), o = n + c.zoomText.getBBox().width + 5, q(c.buttonOptions, function(a, b) {
                k[b] = e.button(a.text, o, d.plotTop - 25, function() {
                    c.clickButton(b, a);
                    c.isActive =
                        !0;
                }, l, p && p.hover, p && p.select).css({ textAlign: "center" }).add();
                o += k[b].width + (i.buttonSpacing || 0);
                c.selected === b && k[b].setState(2);
            }), c.updateButtonStates(), m)) c.div = j = Y("div", null, { position: "relative", height: 0, zIndex: 1 }), f.parentNode.insertBefore(j, f), c.inputGroup = j = e.g("input-group").add(), j.offset = 0, c.drawInput("min"), c.drawInput("max");
            m && (f = d.plotTop - 35, j.align(x({ y: f, width: j.offset, x: h && f < (h.y || 0) + h.height - d.spacing[0] ? -40 : 0 }, i.inputPosition), !0, d.spacingBox), c.setInputValue("min", a), c.setInputValue("max",
                b));
            c.rendered = !0;
        },
        destroy: function() {
            var a = this.minInput, b = this.maxInput, c = this.chart, d = this.blurInputs, e;
            U(c.container, "mousedown", d);
            U(c, "resize", d);
            Ea(this.buttons);
            if (a) a.onfocus = a.onblur = a.onchange = null;
            if (b) b.onfocus = b.onblur = b.onchange = null;
            for (e in this) this[e] && e !== "chart" && (this[e].destroy ? this[e].destroy() : this[e].nodeType && $a(this[e])), this[e] = null;
        }
    };
    oa.prototype.toFixedRange = function(a, b, c, d) {
        var e = this.chart && this.chart.fixedRange, a = o(c, this.translate(a, !0)), b = o(d, this.translate(b,
            !0));
        e && (b - a) / e < 1.3 && (d ? a = b - e : b = a + e);
        return { min: a, max: b };
    };
    ga(Va.prototype, "init", function(a, b, c) {
        E(this, "init", function() { if (this.options.rangeSelector.enabled) this.rangeSelector = new Jb(this); });
        a.call(this, b, c);
    });
    Highcharts.RangeSelector = Jb;
    Va.prototype.callbacks.push(function(a) {

        function b() {
            f = a.xAxis[0].getExtremes();
            g.render(f.min, f.max);
        }

        function c() {
            f = a.xAxis[0].getExtremes();
            isNaN(f.min) || h.render(f.min, f.max);
        }

        function d(a) { a.triggerOp !== "navigator-drag" && g.render(a.min, a.max); }

        function e(a) {
            h.render(a.min,
                a.max);
        }

        var f, g = a.scroller, h = a.rangeSelector;
        g && (E(a.xAxis[0], "afterSetExtremes", d), ga(a, "drawChartBox", function(a) {
            var c = this.isDirtyBox;
            a.call(this);
            c && b();
        }), b());
        h && (E(a.xAxis[0], "afterSetExtremes", e), E(a, "resize", c), c());
        E(a, "destroy", function() {
            g && U(a.xAxis[0], "afterSetExtremes", d);
            h && (U(a, "resize", c), U(a.xAxis[0], "afterSetExtremes", e));
        });
    });
    Highcharts.StockChart = function(a, b) {
        var c = a.series, d, e = o(a.navigator && a.navigator.enabled, !0) ? { startOnTick: !1, endOnTick: !1 } : null, f = {
            marker: { enabled: !1, states: { hover: { radius: 5 } } },
            states: { hover: { lineWidth: 2 } }
        }, g = { shadow: !1, borderWidth: 0 };
        a.xAxis = Ja(fa(a.xAxis || {}), function(a) { return u({ minPadding: 0, maxPadding: 0, ordinal: !0, title: { text: null }, labels: { overflow: "justify" }, showLastLabel: !0 }, a, { type: "datetime", categories: null }, e); });
        a.yAxis = Ja(fa(a.yAxis || {}), function(a) {
            d = a.opposite;
            return u({ labels: { align: d ? "right" : "left", x: d ? -2 : 2, y: -2 }, showLastLabel: !1, title: { text: null } }, a);
        });
        a.series = null;
        a = u({
            chart: { panning: !0, pinchType: "x" },
            navigator: { enabled: !0 },
            scrollbar: { enabled: !0 },
            rangeSelector: { enabled: !0 },
            title: { text: null },
            tooltip: { shared: !0, crosshairs: !0 },
            legend: { enabled: !1 },
            plotOptions: { line: f, spline: f, area: f, areaspline: f, arearange: f, areasplinerange: f, column: g, columnrange: g, candlestick: g, ohlc: g }
        }, a, { _stock: !0, chart: { inverted: !1 } });
        a.series = c;
        return new Va(a, b);
    };
    ga(rb.prototype, "init", function(a, b, c) {
        var d = c.chart.pinchType || "";
        a.call(this, b, c);
        this.pinchX = this.pinchHor = d.indexOf("x") !== -1;
        this.pinchY = this.pinchVert = d.indexOf("y") !== -1;
    });
    var nc = R.init, oc = R.processData, pc = Ka.prototype.tooltipFormatter;
    R.init = function() {
        nc.apply(this, arguments);
        this.setCompare(this.options.compare);
    };
    R.setCompare = function(a) {
        this.modifyValue = a === "value" || a === "percent" ? function(b, c) {
            var d = this.compareValue, b = a === "value" ? b - d : b = 100 * (b / d) - 100;
            if (c) c.change = b;
            return b;
        } : null;
        if (this.chart.hasRendered) this.isDirty = !0;
    };
    R.processData = function() {
        var a = 0, b, c, d;
        oc.apply(this, arguments);
        if (this.xAxis && this.processedYData) {
            b = this.processedXData;
            c = this.processedYData;
            for (d = c.length; a < d; a++)
                if (typeof c[a] === "number" && b[a] >= this.xAxis.min) {
                    this.compareValue =
                        c[a];
                    break;
                }
        }
    };
    ga(R, "getExtremes", function(a) {
        a.call(this);
        if (this.modifyValue) this.dataMax = this.modifyValue(this.dataMax), this.dataMin = this.modifyValue(this.dataMin);
    });
    oa.prototype.setCompare = function(a, b) { this.isXAxis || (q(this.series, function(b) { b.setCompare(a); }), o(b, !0) && this.chart.redraw()); };
    Ka.prototype.tooltipFormatter = function(a) {
        a = a.replace("{point.change}", (this.change > 0 ? "+" : "") + Ca(this.change, o(this.series.tooltipOptions.changeDecimals, 2)));
        return pc.apply(this, [a]);
    };
    (function() {
        var a = R.init,
            b = R.getSegments;
        R.init = function() {
            var b, d;
            a.apply(this, arguments);
            b = this.chart;
            (d = this.xAxis) && d.options.ordinal && E(this, "updatedData", function() { delete d.ordinalIndex; });
            if (d && d.options.ordinal && !d.hasOrdinalExtension) {
                d.hasOrdinalExtension = !0;
                d.beforeSetTickPositions = function() {
                    var a, b = [], c = !1, e, k = this.getExtremes(), j = k.min, k = k.max, l;
                    if (this.options.ordinal) {
                        q(this.series, function(c, d) {
                            if (c.visible !== !1 && c.takeOrdinalPosition !== !1 && (b = b.concat(c.processedXData), a = b.length, b.sort(function(a, b) {
                                return a -
                                    b;
                            }), a)) for (d = a - 1; d--;) b[d] === b[d + 1] && b.splice(d, 1);
                        });
                        a = b.length;
                        if (a > 2) {
                            e = b[1] - b[0];
                            for (l = a - 1; l-- && !c;) b[l + 1] - b[l] !== e && (c = !0);
                            if (b[0] - j > e || k - b[b.length - 1] > e) c = !0;
                        }
                        c ? (this.ordinalPositions = b, c = d.val2lin(v(j, b[0]), !0), e = d.val2lin(z(k, b[b.length - 1]), !0), this.ordinalSlope = k = (k - j) / (e - c), this.ordinalOffset = j - c * k) : this.ordinalPositions = this.ordinalSlope = this.ordinalOffset = s;
                    }
                };
                d.val2lin = function(a, b) {
                    var c = this.ordinalPositions;
                    if (c) {
                        var d = c.length, e, j;
                        for (e = d; e--;)
                            if (c[e] === a) {
                                j = e;
                                break;
                            }
                        for (e = d - 1; e--;)
                            if (a >
                                c[e] || e === 0) {
                                c = (a - c[e]) / (c[e + 1] - c[e]);
                                j = e + c;
                                break;
                            }
                        return b ? j : this.ordinalSlope * (j || 0) + this.ordinalOffset;
                    } else return a;
                };
                d.lin2val = function(a, b) {
                    var c = this.ordinalPositions;
                    if (c) {
                        var d = this.ordinalSlope, e = this.ordinalOffset, j = c.length - 1, l, m;
                        if (b) a < 0 ? a = c[0] : a > j ? a = c[j] : (j = O(a), m = a - j);
                        else
                            for (; j--;)
                                if (l = d * j + e, a >= l) {
                                    d = d * (j + 1) + e;
                                    m = (a - l) / (d - l);
                                    break;
                                }
                        return m !== s && c[j] !== s ? c[j] + (m ? m * (c[j + 1] - c[j]) : 0) : a;
                    } else return a;
                };
                d.getExtendedPositions = function() {
                    var a = d.series[0].currentDataGrouping, e = d.ordinalIndex,
                        h = a ? a.count + a.unitName : "raw", i = d.getExtremes(), k, j;
                    if (!e) e = d.ordinalIndex = {};
                    if (!e[h])
                        k = { series: [], getExtremes: function() { return { min: i.dataMin, max: i.dataMax }; }, options: { ordinal: !0 } }, q(d.series, function(d) {
                            j = { xAxis: k, xData: d.xData, chart: b, destroyGroupedData: pa };
                            j.options = { dataGrouping: a ? { enabled: !0, forced: !0, approximation: "open", units: [[a.unitName, [a.count]]] } : { enabled: !1 } };
                            d.processData.apply(j);
                            k.series.push(j);
                        }), d.beforeSetTickPositions.apply(k), e[h] = k.ordinalPositions;
                    return e[h];
                };
                d.getGroupIntervalFactor =
                    function(a, b, c) {
                        for (var d = 0, e = c.length, j = []; d < e - 1; d++) j[d] = c[d + 1] - c[d];
                        j.sort(function(a, b) { return a - b; });
                        d = j[O(e / 2)];
                        a = v(a, c[0]);
                        b = z(b, c[e - 1]);
                        return e * d / (b - a);
                    };
                d.postProcessTickInterval = function(a) {
                    var b = this.ordinalSlope;
                    return b ? a / (b / d.closestPointRange) : a;
                };
                d.getNonLinearTimeTicks = function(a, b, c, e, k, j, l) {
                    var m = 0, p = 0, n, o = {}, q, t, v, u = [], x = -Number.MAX_VALUE, z = d.options.tickPixelInterval;
                    if (!k || k.length < 3 || b === s) return fb(a, b, c, e);
                    for (t = k.length; p < t; p++) {
                        v = p && k[p - 1] > c;
                        k[p] < b && (m = p);
                        if (p === t - 1 || k[p +
                            1] - k[p] > j * 5 || v) {
                            if (k[p] > x) {
                                for (n = fb(a, k[m], k[p], e); n.length && n[0] <= x;) n.shift();
                                n.length && (x = n[n.length - 1]);
                                u = u.concat(n);
                            }
                            m = p + 1;
                        }
                        if (v) break;
                    }
                    a = n.info;
                    if (l && a.unitRange <= H[Da]) {
                        p = u.length - 1;
                        for (m = 1; m < p; m++) (new Date(u[m]))[Ra]() !== (new Date(u[m - 1]))[Ra]() && (o[u[m]] = da, q = !0);
                        q && (o[u[0]] = da);
                        a.higherRanks = o;
                    }
                    u.info = a;
                    if (l && r(z)) {
                        var l = a = u.length, p = [], A;
                        for (q = []; l--;) m = d.translate(u[l]), A && (q[l] = A - m), p[l] = A = m;
                        q.sort();
                        q = q[O(q.length / 2)];
                        q < z * 0.6 && (q = null);
                        l = u[a - 1] > c ? a - 1 : a;
                        for (A = void 0; l--;)
                            m = p[l], c = A - m, A &&
                                c < z * 0.8 && (q === null || c < q * 0.8) ? (o[u[l]] && !o[u[l + 1]] ? (c = l + 1, A = m) : c = l, u.splice(c, 1)) : A = m;
                    }
                    return u;
                };
                var e = b.pan;
                b.pan = function(a) {
                    var d = b.xAxis[0], h = a.chartX, i = !1;
                    if (d.options.ordinal && d.series.length) {
                        var k = b.mouseDownX, j = d.getExtremes(), l = j.dataMax, m = j.min, p = j.max, n = b.hoverPoints, o = d.closestPointRange, k = (k - h) / (d.translationSlope * (d.ordinalSlope || o)), r = { ordinalPositions: d.getExtendedPositions() }, o = d.lin2val, s = d.val2lin, t;
                        if (r.ordinalPositions) {
                            if (M(k) > 1)
                                n && q(n, function(a) { a.setState(); }), k < 0 ? (n = r, t = d.ordinalPositions ?
                                    d : r) : (n = d.ordinalPositions ? d : r, t = r), r = t.ordinalPositions, l > r[r.length - 1] && r.push(l), k = d.toFixedRange(null, null, o.apply(n, [s.apply(n, [m, !0]) + k, !0]), o.apply(t, [s.apply(t, [p, !0]) + k, !0])), k.min >= z(j.dataMin, m) && k.max <= v(l, p) && d.setExtremes(k.min, k.max, !0, !1, { trigger: "pan" }), b.mouseDownX = h, G(b.container, { cursor: "move" });
                        } else i = !0;
                    } else i = !0;
                    i && e.apply(b, arguments);
                };
            }
        };
        R.getSegments = function() {
            var a, d = this.options.gapSize, e = this.xAxis;
            b.apply(this);
            if (e.options.ordinal && d)
                a = this.segments, q(a, function(b, g) {
                    for (var h =
                        b.length - 1; h--;) b[h + 1].x - b[h].x > e.closestPointRange * d && a.splice(g + 1, 0, b.splice(h + 1, b.length - h));
                });
        };
    })();
    x(Highcharts, {
        Axis: oa,
        Chart: Va,
        Color: ya,
        Legend: sb,
        Pointer: rb,
        Point: Ka,
        Tick: bb,
        Tooltip: Hb,
        Renderer: cb,
        Series: V,
        SVGElement: Fa,
        SVGRenderer: za,
        arrayMin: Sa,
        arrayMax: va,
        charts: Wa,
        dateFormat: qa,
        format: Oa,
        pathAnim: Lb,
        getOptions: function() { return K; },
        hasBidiBug: bc,
        isTouchDevice: hb,
        numberFormat: Ca,
        seriesTypes: N,
        setOptions: function(a) {
            K = u(K, a);
            Tb();
            return K;
        },
        addEvent: E,
        removeEvent: U,
        createElement: Y,
        discardElement: $a,
        css: G,
        each: q,
        extend: x,
        map: Ja,
        merge: u,
        pick: o,
        splat: fa,
        extendClass: ba,
        pInt: B,
        wrap: ga,
        svg: aa,
        canvas: ia,
        vml: !aa && !ia,
        product: "Highstock",
        version: "1.3.7"
    });
})();
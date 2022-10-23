! function (e, t) {
    "object" == typeof exports && "undefined" != typeof module ? t(exports) : "function" == typeof define && define.amd ? define(["exports"], t) : t((e = e || self).FullCalendar = {})
}(this, function (e) {
    "use strict";
    var o = {
        className: !0,
        colSpan: !0,
        rowSpan: !0
    },
        t = {
            "<tr": "tbody",
            "<td": "tr"
        };

    function a(e, t, n) {
        var r = document.createElement(e);
        if (t)
            for (var i in t) "style" === i ? g(r, t[i]) : o[i] ? r[i] = t[i] : r.setAttribute(i, t[i]);
        return "string" == typeof n ? r.innerHTML = n : null != n && w(r, n), r
    }

    function S(e) {
        e = e.trim();
        var t = document.createElement(n(e));
        return t.innerHTML = e, t.firstChild
    }

    function s(e) {
        return Array.prototype.slice.call(function (e) {
            e = e.trim();
            var t = document.createElement(n(e));
            return t.innerHTML = e, t.childNodes
        }(e))
    }

    function n(e) {
        return t[e.substr(0, 3)] || "div"
    }

    function w(e, t) {
        for (var n = l(t), r = 0; r < n.length; r++) e.appendChild(n[r])
    }

    function f(e, t) {
        for (var n = l(t), r = e.firstChild || null, i = 0; i < n.length; i++) e.insertBefore(n[i], r)
    }

    function l(e) {
        return "string" == typeof e ? s(e) : e instanceof Node ? [e] : Array.prototype.slice.call(e)
    }

    function r(e) {
        e.parentNode && e.parentNode.removeChild(e)
    }
    var i = Element.prototype.matches || Element.prototype.matchesSelector || Element.prototype.msMatchesSelector,
        u = Element.prototype.closest || function (e) {
            var t = this;
            if (!document.documentElement.contains(t)) return null;
            do {
                if (d(t, e)) return t;
                t = t.parentElement || t.parentNode
            } while (null !== t && 1 === t.nodeType);
            return null
        };

    function c(e, t) {
        return u.call(e, t)
    }

    function d(e, t) {
        return i.call(e, t)
    }

    function p(e, t) {
        for (var n = e instanceof HTMLElement ? [e] : e, r = [], i = 0; i < n.length; i++)
            for (var o = n[i].querySelectorAll(t), a = 0; a < o.length; a++) r.push(o[a]);
        return r
    }
    var h = /(top|left|right|bottom|width|height)$/i;

    function g(e, t) {
        for (var n in t) v(e, n, t[n])
    }

    function v(e, t, n) {
        null == n ? e.style[t] = "" : "number" == typeof n && h.test(t) ? e.style[t] = n + "px" : e.style[t] = n
    }

    function m(e, t) {
        var n = {
            left: Math.max(e.left, t.left),
            right: Math.min(e.right, t.right),
            top: Math.max(e.top, t.top),
            bottom: Math.min(e.bottom, t.bottom)
        };
        return n.left < n.right && n.top < n.bottom && n
    }
    var y = null;

    function b() {
        return null === y && (y = function () {
            var e = a("div", {
                style: {
                    position: "absolute",
                    top: -1e3,
                    left: 0,
                    border: 0,
                    padding: 0,
                    overflow: "scroll",
                    direction: "rtl"
                }
            }, "<div></div>");
            document.body.appendChild(e);
            var t = e.firstChild.getBoundingClientRect().left > e.getBoundingClientRect().left;
            return r(e), t
        }()), y
    }

    function D(e) {
        return e = Math.max(0, e), e = Math.round(e)
    }

    function T(e, t) {
        void 0 === t && (t = !1);
        var n = window.getComputedStyle(e),
            r = parseInt(n.borderLeftWidth, 10) || 0,
            i = parseInt(n.borderRightWidth, 10) || 0,
            o = parseInt(n.borderTopWidth, 10) || 0,
            a = parseInt(n.borderBottomWidth, 10) || 0,
            s = D(e.offsetWidth - e.clientWidth - r - i),
            l = {
                borderLeft: r,
                borderRight: i,
                borderTop: o,
                borderBottom: a,
                scrollbarBottom: D(e.offsetHeight - e.clientHeight - o - a),
                scrollbarLeft: 0,
                scrollbarRight: 0
            };
        return b() && "rtl" === n.direction ? l.scrollbarLeft = s : l.scrollbarRight = s, t && (l.paddingLeft = parseInt(n.paddingLeft, 10) || 0, l.paddingRight = parseInt(n.paddingRight, 10) || 0, l.paddingTop = parseInt(n.paddingTop, 10) || 0, l.paddingBottom = parseInt(n.paddingBottom, 10) || 0), l
    }

    function C(e, t) {
        void 0 === t && (t = !1);
        var n = E(e),
            r = T(e, t),
            i = {
                left: n.left + r.borderLeft + r.scrollbarLeft,
                right: n.right - r.borderRight - r.scrollbarRight,
                top: n.top + r.borderTop,
                bottom: n.bottom - r.borderBottom - r.scrollbarBottom
            };
        return t && (i.left += r.paddingLeft, i.right -= r.paddingRight, i.top += r.paddingTop, i.bottom -= r.paddingBottom), i
    }

    function E(e) {
        var t = e.getBoundingClientRect();
        return {
            left: t.left + window.pageXOffset,
            top: t.top + window.pageYOffset,
            right: t.right + window.pageXOffset,
            bottom: t.bottom + window.pageYOffset
        }
    }

    function _(e) {
        return e.getBoundingClientRect().height + x(e)
    }

    function x(e) {
        var t = window.getComputedStyle(e);
        return parseInt(t.marginTop, 10) + parseInt(t.marginBottom, 10)
    }

    function I(e) {
        for (var t = []; e instanceof HTMLElement;) {
            var n = window.getComputedStyle(e);
            if ("fixed" === n.position) break;
            /(auto|scroll)/.test(n.overflow + n.overflowY + n.overflowX) && t.push(e), e = e.parentNode
        }
        return t
    }

    function R(e) {
        e.preventDefault()
    }

    function k(e, t, n, r) {
        function i(e) {
            var t = c(e.target, n);
            t && r.call(t, e, t)
        }
        return e.addEventListener(t, i),
            function () {
                e.removeEventListener(t, i)
            }
    }
    var P = ["webkitTransitionEnd", "otransitionend", "oTransitionEnd", "msTransitionEnd", "transitionend"];
    var M = ["sun", "mon", "tue", "wed", "thu", "fri", "sat"];

    function O(e, t) {
        var n = W(e);
        return n[2] += t, V(n)
    }

    function H(e, t) {
        var n = W(e);
        return n[6] += t, V(n)
    }

    function A(e, t) {
        return (t.valueOf() - e.valueOf()) / 864e5
    }

    function N(e, t) {
        var n = z(e),
            r = z(t);
        return {
            years: 0,
            months: 0,
            days: Math.round(A(n, r)),
            milliseconds: t.valueOf() - r.valueOf() - (e.valueOf() - n.valueOf())
        }
    }

    function L(e, t) {
        var n = F(e, t);
        return null !== n && n % 7 == 0 ? n / 7 : null
    }

    function F(e, t) {
        return Z(e) === Z(t) ? Math.round(A(e, t)) : null
    }

    function z(e) {
        return V([e.getUTCFullYear(), e.getUTCMonth(), e.getUTCDate()])
    }

    function B(e, t, n, r) {
        var i = V([t, 0, 1 + function (e, t, n) {
            var r = 7 + t - n;
            return -(7 + V([e, 0, r]).getUTCDay() - t) % 7 + r - 1
        }(t, n, r)]),
            o = z(e),
            a = Math.round(A(i, o));
        return Math.floor(a / 7) + 1
    }

    function j(e) {
        return [e.getFullYear(), e.getMonth(), e.getDate(), e.getHours(), e.getMinutes(), e.getSeconds(), e.getMilliseconds()]
    }

    function U(e) {
        return new Date(e[0], e[1] || 0, null == e[2] ? 1 : e[2], e[3] || 0, e[4] || 0, e[5] || 0)
    }

    function W(e) {
        return [e.getUTCFullYear(), e.getUTCMonth(), e.getUTCDate(), e.getUTCHours(), e.getUTCMinutes(), e.getUTCSeconds(), e.getUTCMilliseconds()]
    }

    function V(e) {
        return 1 === e.length && (e = e.concat([0])), new Date(Date.UTC.apply(Date, e))
    }

    function G(e) {
        return !isNaN(e.valueOf())
    }

    function Z(e) {
        return 1e3 * e.getUTCHours() * 60 * 60 + 1e3 * e.getUTCMinutes() * 60 + 1e3 * e.getUTCSeconds() + e.getUTCMilliseconds()
    }
    var q = ["years", "months", "days", "milliseconds"],
        Y = /^(-?)(?:(\d+)\.)?(\d+):(\d\d)(?::(\d\d)(?:\.(\d\d\d))?)?/;

    function X(e, t) {
        var n;
        return "string" == typeof e ? function (e) {
            var t = Y.exec(e);
            if (t) {
                var n = t[1] ? -1 : 1;
                return {
                    years: 0,
                    months: 0,
                    days: n * (t[2] ? parseInt(t[2], 10) : 0),
                    milliseconds: n * (60 * (t[3] ? parseInt(t[3], 10) : 0) * 60 * 1e3 + 60 * (t[4] ? parseInt(t[4], 10) : 0) * 1e3 + 1e3 * (t[5] ? parseInt(t[5], 10) : 0) + (t[6] ? parseInt(t[6], 10) : 0))
                }
            }
            return null
        }(e) : "object" == typeof e && e ? J(e) : "number" == typeof e ? J(((n = {})[t || "milliseconds"] = e, n)) : null
    }

    function J(e) {
        return {
            years: e.years || e.year || 0,
            months: e.months || e.month || 0,
            days: (e.days || e.day || 0) + 7 * $(e),
            milliseconds: 60 * (e.hours || e.hour || 0) * 60 * 1e3 + 60 * (e.minutes || e.minute || 0) * 1e3 + 1e3 * (e.seconds || e.second || 0) + (e.milliseconds || e.millisecond || e.ms || 0)
        }
    }

    function $(e) {
        return e.weeks || e.week || 0
    }

    function Q(e, t) {
        return e.years === t.years && e.months === t.months && e.days === t.days && e.milliseconds === t.milliseconds
    }

    function K(e) {
        return ee(e) / 864e5
    }

    function ee(e) {
        return 31536e6 * e.years + 2592e6 * e.months + 864e5 * e.days + e.milliseconds
    }

    function te(e, t) {
        var n = e.milliseconds;
        if (n) {
            if (n % 1e3 != 0) return {
                unit: "millisecond",
                value: n
            };
            if (n % 6e4 != 0) return {
                unit: "second",
                value: n / 1e3
            };
            if (n % 36e5 != 0) return {
                unit: "minute",
                value: n / 6e4
            };
            if (n) return {
                unit: "hour",
                value: n / 36e5
            }
        }
        return e.days ? t || e.days % 7 != 0 ? {
            unit: "day",
            value: e.days
        } : {
                unit: "week",
                value: e.days / 7
            } : e.months ? {
                unit: "month",
                value: e.months
            } : e.years ? {
                unit: "year",
                value: e.years
            } : {
                        unit: "millisecond",
                        value: 0
                    }
    }

    function ne(e) {
        e.forEach(function (e) {
            e.style.height = ""
        })
    }

    function re(e) {
        var t, n, r = [],
            i = [];
        for ("string" == typeof e ? i = e.split(/\s*,\s*/) : "function" == typeof e ? i = [e] : Array.isArray(e) && (i = e), t = 0; t < i.length; t++) "string" == typeof (n = i[t]) ? r.push("-" === n.charAt(0) ? {
            field: n.substring(1),
            order: -1
        } : {
                field: n,
                order: 1
            }) : "function" == typeof n && r.push({
                func: n
            });
        return r
    }

    function ie(e, t, n) {
        var r, i;
        for (r = 0; r < n.length; r++)
            if (i = oe(e, t, n[r])) return i;
        return 0
    }

    function oe(e, t, n) {
        return n.func ? n.func(e, t) : ae(e[n.field], t[n.field]) * (n.order || 1)
    }

    function ae(e, t) {
        return e || t ? null == t ? -1 : null == e ? 1 : "string" == typeof e || "string" == typeof t ? String(e).localeCompare(String(t)) : e - t : 0
    }

    function se(e) {
        return e.charAt(0).toUpperCase() + e.slice(1)
    }

    function le(e, t) {
        var n = String(e);
        return "000".substr(0, t - n.length) + n
    }

    function ue(e) {
        return e % 1 == 0
    }

    function ce(e, t, n) {
        if ("function" == typeof e && (e = [e]), e) {
            var r = void 0,
                i = void 0;
            for (r = 0; r < e.length; r++) i = e[r].apply(t, n) || i;
            return i
        }
    }

    function de() {
        for (var e = [], t = 0; t < arguments.length; t++) e[t] = arguments[t];
        for (var n = 0; n < e.length; n++)
            if (void 0 !== e[n]) return e[n]
    }

    function fe(t, n) {
        var r, i, o, a, s, l = function () {
            var e = (new Date).valueOf() - a;
            e < n ? r = setTimeout(l, n - e) : (r = null, s = t.apply(o, i), o = i = null)
        };
        return function () {
            return o = this, i = arguments, a = (new Date).valueOf(), r = r || setTimeout(l, n), s
        }
    }

    function pe(e, t, n, r) {
        void 0 === n && (n = {});
        var i = {};
        for (var o in t) {
            var a = t[o];
            void 0 !== e[o] ? a === Function ? i[o] = "function" == typeof e[o] ? e[o] : null : i[o] = a ? a(e[o]) : e[o] : void 0 !== n[o] ? i[o] = n[o] : a === String ? i[o] = "" : a && a !== Number && a !== Boolean && a !== Function ? i[o] = a(null) : i[o] = null
        }
        if (r)
            for (var o in e) void 0 === t[o] && (r[o] = e[o]);
        return i
    }

    function he(e) {
        var t = Math.floor(A(e.start, e.end)) || 1,
            n = z(e.start);
        return {
            start: n,
            end: O(n, t)
        }
    }

    function ge(e, t) {
        void 0 === t && (t = X(0));
        var n = null,
            r = null;
        if (e.end) {
            r = z(e.end);
            var i = e.end.valueOf() - r.valueOf();
            i && i >= ee(t) && (r = O(r, 1))
        }
        return e.start && (n = z(e.start), r && r <= n && (r = O(n, 1))), {
            start: n,
            end: r
        }
    }

    function ve(e, t, n, r) {
        return "year" === r ? X(n.diffWholeYears(e, t), "year") : "month" === r ? X(n.diffWholeMonths(e, t), "month") : N(e, t)
    }
    var me = function (e, t) {
        return (me = Object.setPrototypeOf || {
            __proto__: []
        }
            instanceof Array && function (e, t) {
                e.__proto__ = t
            } || function (e, t) {
                for (var n in t) t.hasOwnProperty(n) && (e[n] = t[n])
            })(e, t)
    };

    function ye(e, t) {
        function n() {
            this.constructor = e
        }
        me(e, t), e.prototype = null === t ? Object.create(t) : (n.prototype = t.prototype, new n)
    }
    var be = function () {
        return (be = Object.assign || function (e) {
            for (var t, n = 1, r = arguments.length; n < r; n++)
                for (var i in t = arguments[n]) Object.prototype.hasOwnProperty.call(t, i) && (e[i] = t[i]);
            return e
        }).apply(this, arguments)
    };
    var Se = Object.prototype.hasOwnProperty;

    function we(e, t) {
        var n, r, i, o, a, s, l = {};
        if (t)
            for (n = 0; n < t.length; n++) {
                for (r = t[n], i = [], o = e.length - 1; 0 <= o; o--)
                    if ("object" == typeof (a = e[o][r]) && a) i.unshift(a);
                    else if (void 0 !== a) {
                        l[r] = a;
                        break
                    }
                i.length && (l[r] = we(i))
            }
        for (n = e.length - 1; 0 <= n; n--)
            for (r in s = e[n]) r in l || (l[r] = s[r]);
        return l
    }

    function De(e, t) {
        var n = {};
        for (var r in e) t(e[r], r) && (n[r] = e[r]);
        return n
    }

    function Te(e, t) {
        var n = {};
        for (var r in e) n[r] = t(e[r], r);
        return n
    }

    function Ce(e) {
        for (var t = {}, n = 0, r = e; n < r.length; n++) {
            t[r[n]] = !0
        }
        return t
    }

    function Ee(e) {
        var t = [];
        for (var n in e) t.push(e[n]);
        return t
    }

    function _e(e, t) {
        for (var n in e)
            if (Se.call(e, n) && !(n in t)) return !1;
        for (var n in t)
            if (Se.call(t, n) && e[n] !== t[n]) return !1;
        return !0
    }

    function xe(e, t, n, r) {
        for (var i = {
            defs: {},
            instances: {}
        }, o = 0, a = e; o < a.length; o++) {
            var s = Wt(a[o], t, n, r);
            s && Ie(s, i)
        }
        return i
    }

    function Ie(e, t) {
        return void 0 === t && (t = {
            defs: {},
            instances: {}
        }), t.defs[e.def.defId] = e.def, e.instance && (t.instances[e.instance.instanceId] = e.instance), t
    }

    function Re(e, t, n) {
        var r, i, o, a, s, l, u = n.dateEnv,
            c = e.defs,
            d = e.instances;
        for (var f in d = De(d, function (e) {
            return !c[e.defId].recurringDef
        }), c) {
            var p = c[f];
            if (p.recurringDef) {
                var h = p.recurringDef.duration;
                h = h || (p.allDay ? n.defaultAllDayEventDuration : n.defaultTimedEventDuration);
                for (var g = 0, v = (r = p, i = h, o = t, a = n.dateEnv, s = n.pluginSystem.hooks.recurringTypes, l = void 0, l = s[r.recurringDef.typeId].expand(r.recurringDef.typeData, {
                    start: a.subtract(o.start, i),
                    end: o.end
                }, a), r.allDay && (l = l.map(z)), l); g < v.length; g++) {
                    var m = v[g],
                        y = Gt(f, {
                            start: m,
                            end: u.add(m, h)
                        });
                    d[y.instanceId] = y
                }
            }
        }
        return {
            defs: c,
            instances: d
        }
    }

    function ke(e, t) {
        var n = e.instances[t];
        if (n) {
            var r = e.defs[n.defId],
                i = He(e, function (e) {
                    return function (e, t) {
                        return Boolean(e.groupId && e.groupId === t.groupId)
                    }(r, e)
                });
            return i.defs[r.defId] = r, i.instances[n.instanceId] = n, i
        }
        return {
            defs: {},
            instances: {}
        }
    }

    function Pe(e, t) {
        var n;
        if (t) {
            n = [];
            for (var r = 0, i = e; r < i.length; r++) {
                var o = i[r],
                    a = t(o);
                a ? n.push(a) : null == a && n.push(o)
            }
        } else n = e;
        return n
    }

    function Me() {
        return {
            defs: {},
            instances: {}
        }
    }

    function Oe(e, t) {
        return {
            defs: be({}, e.defs, t.defs),
            instances: be({}, e.instances, t.instances)
        }
    }

    function He(e, t) {
        var n = De(e.defs, t),
            r = De(e.instances, function (e) {
                return n[e.defId]
            });
        return {
            defs: n,
            instances: r
        }
    }

    function Ae(e, t) {
        var n, r, i = [],
            o = t.start;
        for (e.sort(Ne), n = 0; n < e.length; n++)(r = e[n]).start > o && i.push({
            start: o,
            end: r.start
        }), r.end > o && (o = r.end);
        return o < t.end && i.push({
            start: o,
            end: t.end
        }), i
    }

    function Ne(e, t) {
        return e.start.valueOf() - t.start.valueOf()
    }

    function Le(e, t) {
        var n = e.start,
            r = e.end,
            i = null;
        return null !== t.start && (n = null === n ? t.start : new Date(Math.max(n.valueOf(), t.start.valueOf()))), null != t.end && (r = null === r ? t.end : new Date(Math.min(r.valueOf(), t.end.valueOf()))), (null === n || null === r || n < r) && (i = {
            start: n,
            end: r
        }), i
    }

    function Fe(e, t) {
        return (null === e.start ? null : e.start.valueOf()) === (null === t.start ? null : t.start.valueOf()) && (null === e.end ? null : e.end.valueOf()) === (null === t.end ? null : t.end.valueOf())
    }

    function ze(e, t) {
        return (null === e.end || null === t.start || e.end > t.start) && (null === e.start || null === t.end || e.start < t.end)
    }

    function Be(e, t) {
        return (null === e.start || null !== t.start && t.start >= e.start) && (null === e.end || null !== t.end && t.end <= e.end)
    }

    function je(e, t) {
        return (null === e.start || t >= e.start) && (null === e.end || t < e.end)
    }

    function Ue(e, t) {
        var n, r = e.length;
        if (r !== t.length) return !1;
        for (n = 0; n < r; n++)
            if (e[n] !== t[n]) return !1;
        return !0
    }

    function We(e) {
        var t, n;
        return function () {
            return t && Ue(t, arguments) || (t = arguments, n = e.apply(this, arguments)), n
        }
    }

    function Ve(t, n) {
        var r = null;
        return function () {
            var e = t.apply(this, arguments);
            return null !== r && (r === e || n(r, e)) || (r = e), r
        }
    }
    var Ge = {
        week: 3,
        separator: 0,
        omitZeroMinute: 0,
        meridiem: 0,
        omitCommas: 0
    },
        Ze = {
            timeZoneName: 7,
            era: 6,
            year: 5,
            month: 4,
            day: 2,
            weekday: 2,
            hour: 1,
            minute: 1,
            second: 1
        },
        qe = /\s*([ap])\.?m\.?/i,
        Ye = /,/g,
        Xe = /\s+/g,
        Je = /\u200e/g,
        $e = /UTC|GMT/,
        Qe = (Ke.prototype.format = function (e, t) {
            return this.buildFormattingFunc(this.standardDateProps, this.extendedSettings, t)(e)
        }, Ke.prototype.formatRange = function (e, t, n) {
            var r = this.standardDateProps,
                i = this.extendedSettings,
                o = function (e, t, n) {
                    return n.getMarkerYear(e) === n.getMarkerYear(t) ? n.getMarkerMonth(e) === n.getMarkerMonth(t) ? n.getMarkerDay(e) === n.getMarkerDay(t) ? Z(e) === Z(t) ? 0 : 1 : 2 : 4 : 5
                }(e.marker, t.marker, n.calendarSystem);
            if (!o) return this.format(e, n);
            var a = o;
            !(1 < a) || "numeric" !== r.year && "2-digit" !== r.year || "numeric" !== r.month && "2-digit" !== r.month || "numeric" !== r.day && "2-digit" !== r.day || (a = 1);
            var s = this.format(e, n),
                l = this.format(t, n);
            if (s === l) return s;
            var u = et(function (e, t) {
                var n = {};
                for (var r in e) r in Ze && !(Ze[r] <= t) || (n[r] = e[r]);
                return n
            }(r, a), i, n),
                c = u(e),
                d = u(t),
                f = function (e, t, n, r) {
                    for (var i = 0; i < e.length;) {
                        var o = e.indexOf(t, i);
                        if (-1 === o) break;
                        var a = e.substr(0, o);
                        i = o + t.length;
                        for (var s = e.substr(i), l = 0; l < n.length;) {
                            var u = n.indexOf(r, l);
                            if (-1 === u) break;
                            var c = n.substr(0, u);
                            l = u + r.length;
                            var d = n.substr(l);
                            if (a === c && s === d) return {
                                before: a,
                                after: s
                            }
                        }
                    }
                    return null
                }(s, c, l, d),
                p = i.separator || "";
            return f ? f.before + c + p + d + f.after : s + p + l
        }, Ke.prototype.getLargestUnit = function () {
            switch (this.severity) {
                case 7:
                case 6:
                case 5:
                    return "year";
                case 4:
                    return "month";
                case 3:
                    return "week";
                default:
                    return "day"
            }
        }, Ke);

    function Ke(e) {
        var t = {},
            n = {},
            r = 0;
        for (var i in e) i in Ge ? (n[i] = e[i], r = Math.max(Ge[i], r)) : (t[i] = e[i], i in Ze && (r = Math.max(Ze[i], r)));
        this.standardDateProps = t, this.extendedSettings = n, this.severity = r, this.buildFormattingFunc = We(et)
    }

    function et(e, t, n) {
        var r = Object.keys(e).length;
        return 1 === r && "short" === e.timeZoneName ? function (e) {
            return at(e.timeZoneOffset)
        } : 0 === r && t.week ? function (e) {
            return function (e, t, n, r) {
                var i = [];
                "narrow" === r ? i.push(t) : "short" === r && i.push(t, " ");
                i.push(n.simpleNumberFormat.format(e)), n.options.isRtl && i.reverse();
                return i.join("")
            }(n.computeWeekNumber(e.marker), n.weekLabel, n.locale, t.week)
        } : function (n, r, i) {
            n = be({}, n), r = be({}, r),
                function (e, t) {
                    e.timeZoneName && (e.hour || (e.hour = "2-digit"), e.minute || (e.minute = "2-digit"));
                    "long" === e.timeZoneName && (e.timeZoneName = "short");
                    t.omitZeroMinute && (e.second || e.millisecond) && delete t.omitZeroMinute
                }(n, r), n.timeZone = "UTC";
            var o, a = new Intl.DateTimeFormat(i.locale.codes, n);
            if (r.omitZeroMinute) {
                var e = be({}, n);
                delete e.minute, o = new Intl.DateTimeFormat(i.locale.codes, e)
            }
            return function (e) {
                var t = e.marker;
                return function (e, t, n, r, i) {
                    e = e.replace(Je, ""), "short" === n.timeZoneName && (e = function (e, t) {
                        var n = !1;
                        e = e.replace($e, function () {
                            return n = !0, t
                        }), n || (e += " " + t);
                        return e
                    }(e, "UTC" === i.timeZone || null == t.timeZoneOffset ? "UTC" : at(t.timeZoneOffset)));
                    r.omitCommas && (e = e.replace(Ye, "").trim());
                    r.omitZeroMinute && (e = e.replace(":00", ""));
                    !1 === r.meridiem ? e = e.replace(qe, "").trim() : "narrow" === r.meridiem ? e = e.replace(qe, function (e, t) {
                        return t.toLocaleLowerCase()
                    }) : "short" === r.meridiem ? e = e.replace(qe, function (e, t) {
                        return t.toLocaleLowerCase() + "m"
                    }) : "lowercase" === r.meridiem && (e = e.replace(qe, function (e) {
                        return e.toLocaleLowerCase()
                    }));
                    return e = (e = e.replace(Xe, " ")).trim()
                }((o && !t.getUTCMinutes() ? o : a).format(t), e, n, r, i)
            }
        }(e, t, n)
    }
    var tt = (nt.prototype.format = function (e, t) {
        return t.cmdFormatter(this.cmdStr, st(e, null, t, this.separator))
    }, nt.prototype.formatRange = function (e, t, n) {
        return n.cmdFormatter(this.cmdStr, st(e, t, n, this.separator))
    }, nt);

    function nt(e, t) {
        this.cmdStr = e, this.separator = t
    }
    var rt = (it.prototype.format = function (e, t) {
        return this.func(st(e, null, t))
    }, it.prototype.formatRange = function (e, t, n) {
        return this.func(st(e, t, n))
    }, it);

    function it(e) {
        this.func = e
    }

    function ot(e, t) {
        return "object" == typeof e && e ? ("string" == typeof t && (e = be({
            separator: t
        }, e)), new Qe(e)) : "string" == typeof e ? new tt(e, t) : "function" == typeof e ? new rt(e) : void 0
    }

    function at(e, t) {
        void 0 === t && (t = !1);
        var n = e < 0 ? "-" : "+",
            r = Math.abs(e),
            i = Math.floor(r / 60),
            o = Math.round(r % 60);
        return t ? n + le(i, 2) + ":" + le(o, 2) : "GMT" + n + i + (o ? ":" + le(o, 2) : "")
    }

    function st(e, t, n, r) {
        var i = lt(e, n.calendarSystem);
        return {
            date: i,
            start: i,
            end: t ? lt(t, n.calendarSystem) : null,
            timeZone: n.timeZone,
            localeCodes: n.locale.codes,
            separator: r
        }
    }

    function lt(e, t) {
        var n = t.markerToArray(e.marker);
        return {
            marker: e.marker,
            timeZoneOffset: e.timeZoneOffset,
            array: n,
            year: n[0],
            month: n[1],
            day: n[2],
            hour: n[3],
            minute: n[4],
            second: n[5],
            millisecond: n[6]
        }
    }
    var ut = (ct.prototype.remove = function () {
        this.calendar.dispatch({
            type: "REMOVE_EVENT_SOURCE",
            sourceId: this.internalEventSource.sourceId
        })
    }, ct.prototype.refetch = function () {
        this.calendar.dispatch({
            type: "FETCH_EVENT_SOURCES",
            sourceIds: [this.internalEventSource.sourceId]
        })
    }, Object.defineProperty(ct.prototype, "id", {
        get: function () {
            return this.internalEventSource.publicId
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ct.prototype, "url", {
        get: function () {
            return this.internalEventSource.meta.url
        },
        enumerable: !0,
        configurable: !0
    }), ct);

    function ct(e, t) {
        this.calendar = e, this.internalEventSource = t
    }
    var dt = (ft.prototype.setProp = function (e, t) {
        var n, r;
        if (e in jt);
        else if (e in Bt) "function" == typeof Bt[e] && (t = Bt[e](t)), this.mutate({
            standardProps: (n = {}, n[e] = t, n)
        });
        else if (e in Ht) {
            var i = void 0;
            "function" == typeof Ht[e] && (t = Ht[e](t)), i = "color" === e ? {
                backgroundColor: t,
                borderColor: t
            } : "editable" === e ? {
                startEditable: t,
                durationEditable: t
            } : ((r = {})[e] = t, r), this.mutate({
                standardProps: {
                    ui: i
                }
            })
        }
    }, ft.prototype.setExtendedProp = function (e, t) {
        var n;
        this.mutate({
            extendedProps: (n = {}, n[e] = t, n)
        })
    }, ft.prototype.setStart = function (e, t) {
        void 0 === t && (t = {});
        var n = this._calendar.dateEnv,
            r = n.createMarker(e);
        if (r && this._instance) {
            var i = ve(this._instance.range.start, r, n, t.granularity);
            t.maintainDuration ? this.mutate({
                datesDelta: i
            }) : this.mutate({
                startDelta: i
            })
        }
    }, ft.prototype.setEnd = function (e, t) {
        void 0 === t && (t = {});
        var n, r = this._calendar.dateEnv;
        if ((null == e || (n = r.createMarker(e))) && this._instance)
            if (n) {
                var i = ve(this._instance.range.end, n, r, t.granularity);
                this.mutate({
                    endDelta: i
                })
            } else this.mutate({
                standardProps: {
                    hasEnd: !1
                }
            })
    }, ft.prototype.setDates = function (e, t, n) {
        void 0 === n && (n = {});
        var r, i = this._calendar.dateEnv,
            o = {
                allDay: n.allDay
            },
            a = i.createMarker(e);
        if (a && (null == t || (r = i.createMarker(t))) && this._instance) {
            var s = this._instance.range;
            !0 === n.allDay && (s = he(s));
            var l = ve(s.start, a, i, n.granularity);
            if (r) {
                var u = ve(s.end, r, i, n.granularity);
                Q(l, u) ? this.mutate({
                    datesDelta: l,
                    standardProps: o
                }) : this.mutate({
                    startDelta: l,
                    endDelta: u,
                    standardProps: o
                })
            } else o.hasEnd = !1, this.mutate({
                datesDelta: l,
                standardProps: o
            })
        }
    }, ft.prototype.moveStart = function (e) {
        var t = X(e);
        t && this.mutate({
            startDelta: t
        })
    }, ft.prototype.moveEnd = function (e) {
        var t = X(e);
        t && this.mutate({
            endDelta: t
        })
    }, ft.prototype.moveDates = function (e) {
        var t = X(e);
        t && this.mutate({
            datesDelta: t
        })
    }, ft.prototype.setAllDay = function (e, t) {
        void 0 === t && (t = {});
        var n = {
            allDay: e
        },
            r = t.maintainDuration;
        null == r && (r = this._calendar.opt("allDayMaintainDuration")), this._def.allDay !== e && (n.hasEnd = r), this.mutate({
            standardProps: n
        })
    }, ft.prototype.formatRange = function (e) {
        var t = this._calendar.dateEnv,
            n = this._instance,
            r = ot(e, this._calendar.opt("defaultRangeSeparator"));
        return this._def.hasEnd ? t.formatRange(n.range.start, n.range.end, r, {
            forcedStartTzo: n.forcedStartTzo,
            forcedEndTzo: n.forcedEndTzo
        }) : t.format(n.range.start, r, {
            forcedTzo: n.forcedStartTzo
        })
    }, ft.prototype.mutate = function (e) {
        var t = this._def,
            n = this._instance;
        if (n) {
            this._calendar.dispatch({
                type: "MUTATE_EVENTS",
                instanceId: n.instanceId,
                mutation: e,
                fromApi: !0
            });
            var r = this._calendar.state.eventStore;
            this._def = r.defs[t.defId], this._instance = r.instances[n.instanceId]
        }
    }, ft.prototype.remove = function () {
        this._calendar.dispatch({
            type: "REMOVE_EVENT_DEF",
            defId: this._def.defId
        })
    }, Object.defineProperty(ft.prototype, "source", {
        get: function () {
            var e = this._def.sourceId;
            return e ? new ut(this._calendar, this._calendar.state.eventSources[e]) : null
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "start", {
        get: function () {
            return this._instance ? this._calendar.dateEnv.toDate(this._instance.range.start) : null
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "end", {
        get: function () {
            return this._instance && this._def.hasEnd ? this._calendar.dateEnv.toDate(this._instance.range.end) : null
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "id", {
        get: function () {
            return this._def.publicId
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "groupId", {
        get: function () {
            return this._def.groupId
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "allDay", {
        get: function () {
            return this._def.allDay
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "title", {
        get: function () {
            return this._def.title
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "url", {
        get: function () {
            return this._def.url
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "rendering", {
        get: function () {
            return this._def.rendering
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "startEditable", {
        get: function () {
            return this._def.ui.startEditable
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "durationEditable", {
        get: function () {
            return this._def.ui.durationEditable
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "constraint", {
        get: function () {
            return this._def.ui.constraints[0] || null
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "overlap", {
        get: function () {
            return this._def.ui.overlap
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "allow", {
        get: function () {
            return this._def.ui.allows[0] || null
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "backgroundColor", {
        get: function () {
            return this._def.ui.backgroundColor
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "borderColor", {
        get: function () {
            return this._def.ui.borderColor
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "textColor", {
        get: function () {
            return this._def.ui.textColor
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "classNames", {
        get: function () {
            return this._def.ui.classNames
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ft.prototype, "extendedProps", {
        get: function () {
            return this._def.extendedProps
        },
        enumerable: !0,
        configurable: !0
    }), ft);

    function ft(e, t, n) {
        this._calendar = e, this._def = t, this._instance = n || null
    }

    function pt(e, t, n, r) {
        var i = {},
            o = {},
            a = {},
            s = [],
            l = [],
            u = vt(e.defs, t);
        for (var c in e.defs) {
            "inverse-background" === (S = e.defs[c]).rendering && (S.groupId ? (i[S.groupId] = [], a[S.groupId] || (a[S.groupId] = S)) : o[c] = [])
        }
        for (var d in e.instances) {
            var f = e.instances[d],
                p = u[(S = e.defs[f.defId]).defId],
                h = f.range,
                g = !S.allDay && r ? ge(h, r) : h,
                v = Le(g, n);
            v && ("inverse-background" === S.rendering ? S.groupId ? i[S.groupId].push(v) : o[f.defId].push(v) : ("background" === S.rendering ? s : l).push({
                def: S,
                ui: p,
                instance: f,
                range: v,
                isStart: g.start && g.start.valueOf() === v.start.valueOf(),
                isEnd: g.end && g.end.valueOf() === v.end.valueOf()
            }))
        }
        for (var m in i)
            for (var y = 0, b = Ae(i[m], n); y < b.length; y++) {
                var S, w = b[y];
                p = u[(S = a[m]).defId];
                s.push({
                    def: S,
                    ui: p,
                    instance: null,
                    range: w,
                    isStart: !1,
                    isEnd: !1
                })
            }
        for (var c in o)
            for (var D = 0, T = Ae(o[c], n); D < T.length; D++) {
                w = T[D];
                s.push({
                    def: e.defs[c],
                    ui: u[c],
                    instance: null,
                    range: w,
                    isStart: !1,
                    isEnd: !1
                })
            }
        return {
            bg: s,
            fg: l
        }
    }

    function ht(n, e, r) {
        n.hasPublicHandlers("eventRender") && (e = e.filter(function (e) {
            var t = n.publiclyTrigger("eventRender", [{
                event: new dt(n.calendar, e.eventRange.def, e.eventRange.instance),
                isMirror: r,
                isStart: e.isStart,
                isEnd: e.isEnd,
                el: e.el,
                view: n
            }]);
            return !1 !== t && (t && !0 !== t && (e.el = t), !0)
        }));
        for (var t = 0, i = e; t < i.length; t++) {
            var o = i[t];
            a = o.el, s = o, a.fcSeg = s
        }
        var a, s;
        return e
    }

    function gt(e) {
        return e.fcSeg || null
    }

    function vt(e, t) {
        return Te(e, function (e) {
            return mt(e, t)
        })
    }

    function mt(e, t) {
        var n = [];
        return t[""] && n.push(t[""]), t[e.defId] && n.push(t[e.defId]), n.push(e.ui), Ft(n)
    }

    function yt(e, t, n, r) {
        var i = vt(e.defs, t),
            o = {
                defs: {},
                instances: {}
            };
        for (var a in e.defs) {
            var s = e.defs[a];
            o.defs[a] = bt(s, i[a], n, r.pluginSystem.hooks.eventDefMutationAppliers, r)
        }
        for (var l in e.instances) {
            var u = e.instances[l];
            s = o.defs[u.defId];
            o.instances[l] = St(u, s, i[u.defId], n, r)
        }
        return o
    }

    function bt(e, t, n, r, i) {
        var o = n.standardProps || {};
        null == o.hasEnd && t.durationEditable && (n.startDelta || n.endDelta) && (o.hasEnd = !0);
        var a = be({}, e, o, {
            ui: be({}, e.ui, o.ui)
        });
        n.extendedProps && (a.extendedProps = be({}, a.extendedProps, n.extendedProps));
        for (var s = 0, l = r; s < l.length; s++) {
            (0, l[s])(a, n, i)
        }
        return !a.hasEnd && i.opt("forceEventDuration") && (a.hasEnd = !0), a
    }

    function St(e, t, n, r, i) {
        var o = i.dateEnv,
            a = r.standardProps && !0 === r.standardProps.allDay,
            s = r.standardProps && !1 === r.standardProps.hasEnd,
            l = be({}, e);
        return a && (l.range = he(l.range)), r.datesDelta && n.startEditable && (l.range = {
            start: o.add(l.range.start, r.datesDelta),
            end: o.add(l.range.end, r.datesDelta)
        }), r.startDelta && n.durationEditable && (l.range = {
            start: o.add(l.range.start, r.startDelta),
            end: l.range.end
        }), r.endDelta && n.durationEditable && (l.range = {
            start: l.range.start,
            end: o.add(l.range.end, r.endDelta)
        }), s && (l.range = {
            start: l.range.start,
            end: i.getDefaultEventEnd(t.allDay, l.range.start)
        }), t.allDay && (l.range = {
            start: z(l.range.start),
            end: z(l.range.end)
        }), l.range.end < l.range.start && (l.range.end = i.getDefaultEventEnd(t.allDay, l.range.start)), l
    }

    function wt(e, t, n, r, i) {
        switch (t.type) {
            case "RECEIVE_EVENTS":
                return function (e, t, n, r, i, o) {
                    if (t && n === t.latestFetchId) {
                        var a = xe(function (e, t, n) {
                            var r = n.opt("eventDataTransform"),
                                i = t ? t.eventDataTransform : null;
                            return i && (e = Pe(e, i)), r && (e = Pe(e, r)), e
                        }(i, t, o), t.sourceId, o);
                        return r && (a = Re(a, r, o)), Oe(Dt(e, t.sourceId), a)
                    }
                    return e
                }(e, n[t.sourceId], t.fetchId, t.fetchRange, t.rawEvents, i);
            case "ADD_EVENTS":
                return function (e, t, n, r) {
                    n && (t = Re(t, n, r));
                    return Oe(e, t)
                }(e, t.eventStore, r ? r.activeRange : null, i);
            case "MERGE_EVENTS":
                return Oe(e, t.eventStore);
            case "PREV":
            case "NEXT":
            case "SET_DATE":
            case "SET_VIEW_TYPE":
                return r ? Re(e, r.activeRange, i) : e;
            case "CHANGE_TIMEZONE":
                return function (e, n, r) {
                    var i = e.defs,
                        t = Te(e.instances, function (e) {
                            var t = i[e.defId];
                            return t.allDay || t.recurringDef ? e : be({}, e, {
                                range: {
                                    start: r.createMarker(n.toDate(e.range.start, e.forcedStartTzo)),
                                    end: r.createMarker(n.toDate(e.range.end, e.forcedEndTzo))
                                },
                                forcedStartTzo: r.canComputeOffset ? null : e.forcedStartTzo,
                                forcedEndTzo: r.canComputeOffset ? null : e.forcedEndTzo
                            })
                        });
                    return {
                        defs: i,
                        instances: t
                    }
                }(e, t.oldDateEnv, i.dateEnv);
            case "MUTATE_EVENTS":
                return function (e, t, n, r, i) {
                    var o = ke(e, t),
                        a = r ? {
                            "": {
                                startEditable: !0,
                                durationEditable: !0,
                                constraints: [],
                                overlap: null,
                                allows: [],
                                backgroundColor: "",
                                borderColor: "",
                                textColor: "",
                                classNames: []
                            }
                        } : i.eventUiBases;
                    return o = yt(o, a, n, i), Oe(e, o)
                }(e, t.instanceId, t.mutation, t.fromApi, i);
            case "REMOVE_EVENT_INSTANCES":
                return Tt(e, t.instances);
            case "REMOVE_EVENT_DEF":
                return He(e, function (e) {
                    return e.defId !== t.defId
                });
            case "REMOVE_EVENT_SOURCE":
                return Dt(e, t.sourceId);
            case "REMOVE_ALL_EVENT_SOURCES":
                return He(e, function (e) {
                    return !e.sourceId
                });
            case "REMOVE_ALL_EVENTS":
                return {
                    defs: {}, instances: {}
                };
            case "RESET_EVENTS":
                return {
                    defs: e.defs, instances: e.instances
                };
            default:
                return e
        }
    }

    function Dt(e, t) {
        return He(e, function (e) {
            return e.sourceId !== t
        })
    }

    function Tt(e, t) {
        return {
            defs: e.defs,
            instances: De(e.instances, function (e) {
                return !t[e.instanceId]
            })
        }
    }

    function Ct(e, t) {
        return Et({
            eventDrag: e
        }, t)
    }

    function Et(e, t) {
        var n = t.view,
            r = be({
                businessHours: n ? n.props.businessHours : {
                    defs: {},
                    instances: {}
                },
                dateSelection: "",
                eventStore: t.state.eventStore,
                eventUiBases: t.eventUiBases,
                eventSelection: "",
                eventDrag: null,
                eventResize: null
            }, e);
        return (t.pluginSystem.hooks.isPropsValid || _t)(r, t)
    }

    function _t(e, t, n, r) {
        return void 0 === n && (n = {}), !(e.eventDrag && ! function (e, t, n, r) {
            var i = e.eventDrag,
                o = i.mutatedEvents,
                a = o.defs,
                s = o.instances,
                l = vt(a, i.isEvent ? e.eventUiBases : {
                    "": t.selectionConfig
                });
            r && (l = Te(l, r));
            var u = Tt(e.eventStore, i.affectedEvents.instances),
                c = u.defs,
                d = u.instances,
                f = vt(c, e.eventUiBases);
            for (var p in s) {
                var h = s[p],
                    g = h.range,
                    v = l[h.defId],
                    m = a[h.defId];
                if (!xt(v.constraints, g, u, e.businessHours, t)) return !1;
                var y = t.opt("eventOverlap");
                for (var b in "function" != typeof y && (y = null), d) {
                    var S = d[b];
                    if (ze(g, S.range)) {
                        if (!1 === f[S.defId].overlap && i.isEvent) return !1;
                        if (!1 === v.overlap) return !1;
                        if (y && !y(new dt(t, c[S.defId], S), new dt(t, m, h))) return !1
                    }
                }
                for (var w = t.state.eventStore, D = 0, T = v.allows; D < T.length; D++) {
                    var C = T[D],
                        E = be({}, n, {
                            range: h.range,
                            allDay: m.allDay
                        }),
                        _ = w.defs[m.defId],
                        x = w.instances[p],
                        I = void 0;
                    if (I = _ ? new dt(t, _, x) : new dt(t, m), !C(t.buildDateSpanApi(E), I)) return !1
                }
            }
            return !0
        }(e, t, n, r)) && !(e.dateSelection && ! function (e, t, n, r) {
            var i = e.eventStore,
                o = i.defs,
                a = i.instances,
                s = e.dateSelection,
                l = s.range,
                u = t.selectionConfig;
            r && (u = r(u));
            if (!xt(u.constraints, l, i, e.businessHours, t)) return !1;
            var c = t.opt("selectOverlap");
            "function" != typeof c && (c = null);
            for (var d in a) {
                var f = a[d];
                if (ze(l, f.range)) {
                    if (!1 === u.overlap) return !1;
                    if (c && !c(new dt(t, o[f.defId], f))) return !1
                }
            }
            for (var p = 0, h = u.allows; p < h.length; p++) {
                var g = h[p],
                    v = be({}, n, s);
                if (!g(t.buildDateSpanApi(v), null)) return !1
            }
            return !0
        }(e, t, n, r))
    }

    function xt(e, t, n, r, i) {
        for (var o = 0, a = e; o < a.length; o++) {
            if (!kt(It(a[o], t, n, r, i), t)) return !1
        }
        return !0
    }

    function It(t, e, n, r, i) {
        return "businessHours" === t ? Rt(Re(r, e, i)) : "string" == typeof t ? Rt(He(n, function (e) {
            return e.groupId === t
        })) : "object" == typeof t && t ? Rt(Re(t, e, i)) : []
    }

    function Rt(e) {
        var t = e.instances,
            n = [];
        for (var r in t) n.push(t[r].range);
        return n
    }

    function kt(e, t) {
        for (var n = 0, r = e; n < r.length; n++) {
            if (Be(r[n], t)) return !0
        }
        return !1
    }

    function Pt(e) {
        return (e + "").replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/'/g, "&#039;").replace(/"/g, "&quot;").replace(/\n/g, "<br />")
    }

    function Mt(e) {
        var t = [];
        for (var n in e) {
            var r = e[n];
            null != r && "" !== r && t.push(n + ":" + r)
        }
        return t.join(";")
    }

    function Ot(e) {
        return Array.isArray(e) ? e : "string" == typeof e ? e.split(/\s+/) : []
    }
    var Ht = {
        editable: Boolean,
        startEditable: Boolean,
        durationEditable: Boolean,
        constraint: null,
        overlap: null,
        allow: null,
        className: Ot,
        classNames: Ot,
        color: String,
        backgroundColor: String,
        borderColor: String,
        textColor: String
    };

    function At(e, t, n) {
        var r = pe(e, Ht, {}, n),
            i = function (e, t) {
                return Array.isArray(e) ? xe(e, "", t, !0) : "object" == typeof e && e ? xe([e], "", t, !0) : null != e ? String(e) : null
            }(r.constraint, t);
        return {
            startEditable: null != r.startEditable ? r.startEditable : r.editable,
            durationEditable: null != r.durationEditable ? r.durationEditable : r.editable,
            constraints: null != i ? [i] : [],
            overlap: r.overlap,
            allows: null != r.allow ? [r.allow] : [],
            backgroundColor: r.backgroundColor || r.color,
            borderColor: r.borderColor || r.color,
            textColor: r.textColor,
            classNames: r.classNames.concat(r.className)
        }
    }

    function Nt(e, t, n, r) {
        var i = {},
            o = {};
        for (var a in Ht) {
            var s = e + se(a);
            i[a] = t[s], o[s] = !0
        }
        if ("event" === e && (i.editable = t.editable), r)
            for (var a in t) o[a] || (r[a] = t[a]);
        return At(i, n)
    }
    var Lt = {
        startEditable: null,
        durationEditable: null,
        constraints: [],
        overlap: null,
        allows: [],
        backgroundColor: "",
        borderColor: "",
        textColor: "",
        classNames: []
    };

    function Ft(e) {
        return e.reduce(zt, Lt)
    }

    function zt(e, t) {
        return {
            startEditable: null != t.startEditable ? t.startEditable : e.startEditable,
            durationEditable: null != t.durationEditable ? t.durationEditable : e.durationEditable,
            constraints: e.constraints.concat(t.constraints),
            overlap: "boolean" == typeof t.overlap ? t.overlap : e.overlap,
            allows: e.allows.concat(t.allows),
            backgroundColor: t.backgroundColor || e.backgroundColor,
            borderColor: t.borderColor || e.borderColor,
            textColor: t.textColor || e.textColor,
            classNames: e.classNames.concat(t.classNames)
        }
    }
    var Bt = {
        id: String,
        groupId: String,
        title: String,
        url: String,
        rendering: String,
        extendedProps: null
    },
        jt = {
            start: null,
            date: null,
            end: null,
            allDay: null
        },
        Ut = 0;

    function Wt(e, t, n, r) {
        var i = function (e, t) {
            var n = null;
            if (e) {
                var r = t.state.eventSources[e];
                n = r.allDayDefault
            }
            null == n && (n = t.opt("allDayDefault"));
            return n
        }(t, n),
            o = {},
            a = function (e, t, n, r, i) {
                for (var o = 0; o < r.length; o++) {
                    var a = {},
                        s = r[o].parse(e, a, n);
                    if (s) {
                        var l = a.allDay;
                        return delete a.allDay, null == l && null == (l = t) && null == (l = s.allDayGuess) && (l = !1), be(i, a), {
                            allDay: l,
                            duration: s.duration,
                            typeData: s.typeData,
                            typeId: o
                        }
                    }
                }
                return null
            }(e, i, n.dateEnv, n.pluginSystem.hooks.recurringTypes, o);
        if (a) return (s = Vt(o, t, a.allDay, Boolean(a.duration), n)).recurringDef = {
            typeId: a.typeId,
            typeData: a.typeData,
            duration: a.duration
        }, {
            def: s,
            instance: null
        };
        var s, l = {},
            u = function (e, t, n, r, i) {
                var o, a, s = function (e, t) {
                    var n = pe(e, jt, {}, t);
                    return n.start = null !== n.start ? n.start : n.date, delete n.date, n
                }(e, r),
                    l = s.allDay,
                    u = null,
                    c = !1,
                    d = null;
                if (o = n.dateEnv.createMarkerMeta(s.start)) u = o.marker;
                else if (!i) return null;
                null != s.end && (a = n.dateEnv.createMarkerMeta(s.end));
                null == l && (l = null != t ? t : (!o || o.isTimeUnspecified) && (!a || a.isTimeUnspecified));
                l && u && (u = z(u));
                a && (d = a.marker, l && (d = z(d)), u && d <= u && (d = null));
                d ? c = !0 : i || (c = n.opt("forceEventDuration") || !1, d = n.dateEnv.add(u, l ? n.defaultAllDayEventDuration : n.defaultTimedEventDuration));
                return {
                    allDay: l,
                    hasEnd: c,
                    range: {
                        start: u,
                        end: d
                    },
                    forcedStartTzo: o ? o.forcedTzo : null,
                    forcedEndTzo: a ? a.forcedTzo : null
                }
            }(e, i, n, l, r);
        return u ? {
            def: s = Vt(l, t, u.allDay, u.hasEnd, n),
            instance: Gt(s.defId, u.range, u.forcedStartTzo, u.forcedEndTzo)
        } : null
    }

    function Vt(e, t, n, r, i) {
        var o = {},
            a = function (e, t, n) {
                var r = {},
                    i = pe(e, Bt, {}, r),
                    o = At(r, t, n);
                return i.publicId = i.id, delete i.id, i.ui = o, i
            }(e, i, o);
        a.defId = String(Ut++), a.sourceId = t, a.allDay = n, a.hasEnd = r;
        for (var s = 0, l = i.pluginSystem.hooks.eventDefParsers; s < l.length; s++) {
            var u = {};
            (0, l[s])(a, o, u), o = u
        }
        return a.extendedProps = be(o, a.extendedProps || {}), Object.freeze(a.ui.classNames), Object.freeze(a.extendedProps), a
    }

    function Gt(e, t, n, r) {
        return {
            instanceId: String(Ut++),
            defId: e,
            range: t,
            forcedStartTzo: null == n ? null : n,
            forcedEndTzo: null == r ? null : r
        }
    }
    var Zt = {
        startTime: "09:00",
        endTime: "17:00",
        daysOfWeek: [1, 2, 3, 4, 5],
        rendering: "inverse-background",
        classNames: "fc-nonbusiness",
        groupId: "_businessHours"
    };

    function qt(e, t) {
        return xe(function (e) {
            var t;
            t = !0 === e ? [{}] : Array.isArray(e) ? e.filter(function (e) {
                return e.daysOfWeek
            }) : "object" == typeof e && e ? [e] : [];
            return t = t.map(function (e) {
                return be({}, Zt, e)
            })
        }(e), "", t)
    }

    function Yt(e, n, t) {
        void 0 === t && (t = []);
        var r, i, o = [];

        function a() {
            if (i) {
                for (var e = 0, t = o; e < t.length; e++) {
                    t[e].unrender()
                }
                n && n.apply(r, i), i = null
            }
        }

        function s() {
            i && Ue(i, arguments) || (a(), r = this, i = arguments, e.apply(this, arguments))
        }
        s.dependents = o, s.unrender = a;
        for (var l = 0, u = t; l < u.length; l++) {
            u[l].dependents.push(s)
        }
        return s
    }
    var Xt = {
        defs: {},
        instances: {}
    },
        Jt = ($t.prototype.splitProps = function (e) {
            var n = this,
                t = this.getKeyInfo(e),
                r = this.getKeysForEventDefs(e.eventStore),
                i = this.splitDateSelection(e.dateSelection),
                o = this.splitIndividualUi(e.eventUiBases, r),
                a = this.splitEventStore(e.eventStore, r),
                s = this.splitEventDrag(e.eventDrag),
                l = this.splitEventResize(e.eventResize),
                u = {};
            for (var c in this.eventUiBuilders = Te(t, function (e, t) {
                return n.eventUiBuilders[t] || We(Qt)
            }), t) {
                var d = t[c],
                    f = a[c] || Xt,
                    p = this.eventUiBuilders[c];
                u[c] = {
                    businessHours: d.businessHours || e.businessHours,
                    dateSelection: i[c] || null,
                    eventStore: f,
                    eventUiBases: p(e.eventUiBases[""], d.ui, o[c]),
                    eventSelection: f.instances[e.eventSelection] ? e.eventSelection : "",
                    eventDrag: s[c] || null,
                    eventResize: l[c] || null
                }
            }
            return u
        }, $t.prototype._splitDateSpan = function (e) {
            var t = {};
            if (e)
                for (var n = 0, r = this.getKeysForDateSpan(e); n < r.length; n++) t[r[n]] = e;
            return t
        }, $t.prototype._getKeysForEventDefs = function (e) {
            var t = this;
            return Te(e.defs, function (e) {
                return t.getKeysForEventDef(e)
            })
        }, $t.prototype._splitEventStore = function (e, t) {
            var n = e.defs,
                r = e.instances,
                i = {};
            for (var o in n)
                for (var a = 0, s = t[o]; a < s.length; a++) i[f = s[a]] || (i[f] = {
                    defs: {},
                    instances: {}
                }), i[f].defs[o] = n[o];
            for (var l in r)
                for (var u = r[l], c = 0, d = t[u.defId]; c < d.length; c++) {
                    var f;
                    i[f = d[c]] && (i[f].instances[l] = u)
                }
            return i
        }, $t.prototype._splitIndividualUi = function (e, t) {
            var n = {};
            for (var r in e)
                if (r)
                    for (var i = 0, o = t[r]; i < o.length; i++) {
                        var a = o[i];
                        n[a] || (n[a] = {}), n[a][r] = e[r]
                    }
            return n
        }, $t.prototype._splitInteraction = function (t) {
            var n = {};
            if (t) {
                var r = this._splitEventStore(t.affectedEvents, this._getKeysForEventDefs(t.affectedEvents)),
                    e = this._getKeysForEventDefs(t.mutatedEvents),
                    i = this._splitEventStore(t.mutatedEvents, e),
                    o = function (e) {
                        n[e] || (n[e] = {
                            affectedEvents: r[e] || Xt,
                            mutatedEvents: i[e] || Xt,
                            isEvent: t.isEvent,
                            origSeg: t.origSeg
                        })
                    };
                for (var a in r) o(a);
                for (var a in i) o(a)
            }
            return n
        }, $t);

    function $t() {
        this.getKeysForEventDefs = We(this._getKeysForEventDefs), this.splitDateSelection = We(this._splitDateSpan), this.splitEventStore = We(this._splitEventStore), this.splitIndividualUi = We(this._splitIndividualUi), this.splitEventDrag = We(this._splitInteraction), this.splitEventResize = We(this._splitInteraction), this.eventUiBuilders = {}
    }

    function Qt(e, t, n) {
        var r = [];
        e && r.push(e), t && r.push(t);
        var i = {
            "": Ft(r)
        };
        return n && be(i, n), i
    }

    function Kt(e, t, n, r) {
        var i, o, a, s, l = e.dateEnv;
        return t instanceof Date ? i = t : (i = t.date, o = t.type, a = t.forceOff), s = {
            date: l.formatIso(i, {
                omitTime: !0
            }),
            type: o || "day"
        }, "string" == typeof n && (r = n, n = null), n = n ? " " + function (e) {
            var t = [];
            for (var n in e) {
                var r = e[n];
                null != r && t.push(n + '="' + Pt(r) + '"')
            }
            return t.join(" ")
        }(n) : "", r = r || "", !a && e.opt("navLinks") ? "<a" + n + ' data-goto="' + Pt(JSON.stringify(s)) + '">' + r + "</a>" : "<span" + n + ">" + r + "</span>"
    }

    function en(e, t, n, r) {
        var i, o, a = n.calendar,
            s = n.view,
            l = n.theme,
            u = n.dateEnv,
            c = [];
        return je(t.activeRange, e) ? (c.push("fc-" + M[e.getUTCDay()]), s.opt("monthMode") && u.getMonth(e) !== u.getMonth(t.currentRange.start) && c.push("fc-other-month"), o = O(i = z(a.getNow()), 1), e < i ? c.push("fc-past") : o <= e ? c.push("fc-future") : (c.push("fc-today"), !0 !== r && c.push(l.getClass("today")))) : c.push("fc-disabled-day"), c
    }

    function tn(e, t, n) {
        function r() {
            o || (o = !0, t.apply(this, arguments))
        }

        function i() {
            o || (o = !0, n && n.apply(this, arguments))
        }
        var o = !1,
            a = e(r, i);
        a && "function" == typeof a.then && a.then(r, i)
    }
    var nn = (rn.mixInto = function (e) {
        this.mixIntoObj(e.prototype)
    }, rn.mixIntoObj = function (t) {
        var n = this;
        Object.getOwnPropertyNames(this.prototype).forEach(function (e) {
            t[e] || (t[e] = n.prototype[e])
        })
    }, rn.mixOver = function (t) {
        var n = this;
        Object.getOwnPropertyNames(this.prototype).forEach(function (e) {
            t.prototype[e] = n.prototype[e]
        })
    }, rn);

    function rn() { }
    var on, an = (ye(sn, on = nn), sn.prototype.on = function (e, t) {
        return ln(this._handlers || (this._handlers = {}), e, t), this
    }, sn.prototype.one = function (e, t) {
        return ln(this._oneHandlers || (this._oneHandlers = {}), e, t), this
    }, sn.prototype.off = function (e, t) {
        return this._handlers && un(this._handlers, e, t), this._oneHandlers && un(this._oneHandlers, e, t), this
    }, sn.prototype.trigger = function (e) {
        for (var t = [], n = 1; n < arguments.length; n++) t[n - 1] = arguments[n];
        return this.triggerWith(e, this, t), this
    }, sn.prototype.triggerWith = function (e, t, n) {
        return this._handlers && ce(this._handlers[e], t, n), this._oneHandlers && (ce(this._oneHandlers[e], t, n), delete this._oneHandlers[e]), this
    }, sn.prototype.hasHandlers = function (e) {
        return this._handlers && this._handlers[e] && this._handlers[e].length || this._oneHandlers && this._oneHandlers[e] && this._oneHandlers[e].length
    }, sn);

    function sn() {
        return null !== on && on.apply(this, arguments) || this
    }

    function ln(e, t, n) {
        (e[t] || (e[t] = [])).push(n)
    }

    function un(e, t, n) {
        n ? e[t] && (e[t] = e[t].filter(function (e) {
            return e !== n
        })) : delete e[t]
    }
    var cn = (dn.prototype.build = function () {
        var e = this.originEl,
            t = this.originClientRect = e.getBoundingClientRect();
        this.isHorizontal && this.buildElHorizontals(t.left), this.isVertical && this.buildElVerticals(t.top)
    }, dn.prototype.buildElHorizontals = function (e) {
        for (var t = [], n = [], r = 0, i = this.els; r < i.length; r++) {
            var o = i[r].getBoundingClientRect();
            t.push(o.left - e), n.push(o.right - e)
        }
        this.lefts = t, this.rights = n
    }, dn.prototype.buildElVerticals = function (e) {
        for (var t = [], n = [], r = 0, i = this.els; r < i.length; r++) {
            var o = i[r].getBoundingClientRect();
            t.push(o.top - e), n.push(o.bottom - e)
        }
        this.tops = t, this.bottoms = n
    }, dn.prototype.leftToIndex = function (e) {
        var t, n = this.lefts,
            r = this.rights,
            i = n.length;
        for (t = 0; t < i; t++)
            if (e >= n[t] && e < r[t]) return t
    }, dn.prototype.topToIndex = function (e) {
        var t, n = this.tops,
            r = this.bottoms,
            i = n.length;
        for (t = 0; t < i; t++)
            if (e >= n[t] && e < r[t]) return t
    }, dn.prototype.getWidth = function (e) {
        return this.rights[e] - this.lefts[e]
    }, dn.prototype.getHeight = function (e) {
        return this.bottoms[e] - this.tops[e]
    }, dn);

    function dn(e, t, n, r) {
        this.originEl = e, this.els = t, this.isHorizontal = n, this.isVertical = r
    }
    var fn = (pn.prototype.getMaxScrollTop = function () {
        return this.getScrollHeight() - this.getClientHeight()
    }, pn.prototype.getMaxScrollLeft = function () {
        return this.getScrollWidth() - this.getClientWidth()
    }, pn.prototype.canScrollVertically = function () {
        return 0 < this.getMaxScrollTop()
    }, pn.prototype.canScrollHorizontally = function () {
        return 0 < this.getMaxScrollLeft()
    }, pn.prototype.canScrollUp = function () {
        return 0 < this.getScrollTop()
    }, pn.prototype.canScrollDown = function () {
        return this.getScrollTop() < this.getMaxScrollTop()
    }, pn.prototype.canScrollLeft = function () {
        return 0 < this.getScrollLeft()
    }, pn.prototype.canScrollRight = function () {
        return this.getScrollLeft() < this.getMaxScrollLeft()
    }, pn);

    function pn() { }
    var hn, gn = (ye(vn, hn = fn), vn.prototype.getScrollTop = function () {
        return this.el.scrollTop
    }, vn.prototype.getScrollLeft = function () {
        return this.el.scrollLeft
    }, vn.prototype.setScrollTop = function (e) {
        this.el.scrollTop = e
    }, vn.prototype.setScrollLeft = function (e) {
        this.el.scrollLeft = e
    }, vn.prototype.getScrollWidth = function () {
        return this.el.scrollWidth
    }, vn.prototype.getScrollHeight = function () {
        return this.el.scrollHeight
    }, vn.prototype.getClientHeight = function () {
        return this.el.clientHeight
    }, vn.prototype.getClientWidth = function () {
        return this.el.clientWidth
    }, vn);

    function vn(e) {
        var t = hn.call(this) || this;
        return t.el = e, t
    }
    var mn, yn = (ye(bn, mn = fn), bn.prototype.getScrollTop = function () {
        return window.pageYOffset
    }, bn.prototype.getScrollLeft = function () {
        return window.pageXOffset
    }, bn.prototype.setScrollTop = function (e) {
        window.scroll(window.pageXOffset, e)
    }, bn.prototype.setScrollLeft = function (e) {
        window.scroll(e, window.pageYOffset)
    }, bn.prototype.getScrollWidth = function () {
        return document.documentElement.scrollWidth
    }, bn.prototype.getScrollHeight = function () {
        return document.documentElement.scrollHeight
    }, bn.prototype.getClientHeight = function () {
        return document.documentElement.clientHeight
    }, bn.prototype.getClientWidth = function () {
        return document.documentElement.clientWidth
    }, bn);

    function bn() {
        return null !== mn && mn.apply(this, arguments) || this
    }
    var Sn, wn = (ye(Dn, Sn = gn), Dn.prototype.clear = function () {
        this.setHeight("auto"), this.applyOverflow()
    }, Dn.prototype.destroy = function () {
        r(this.el)
    }, Dn.prototype.applyOverflow = function () {
        g(this.el, {
            overflowX: this.overflowX,
            overflowY: this.overflowY
        })
    }, Dn.prototype.lockOverflow = function (e) {
        var t = this.overflowX,
            n = this.overflowY;
        e = e || this.getScrollbarWidths(), "auto" === t && (t = e.bottom || this.canScrollHorizontally() ? "scroll" : "hidden"), "auto" === n && (n = e.left || e.right || this.canScrollVertically() ? "scroll" : "hidden"), g(this.el, {
            overflowX: t,
            overflowY: n
        })
    }, Dn.prototype.setHeight = function (e) {
        v(this.el, "height", e)
    }, Dn.prototype.getScrollbarWidths = function () {
        var e = T(this.el);
        return {
            left: e.scrollbarLeft,
            right: e.scrollbarRight,
            bottom: e.scrollbarBottom
        }
    }, Dn);

    function Dn(e, t) {
        var n = Sn.call(this, a("div", {
            className: "fc-scroller"
        })) || this;
        return n.overflowX = e, n.overflowY = t, n.applyOverflow(), n
    }
    var Tn = (Cn.prototype.processIconOverride = function () {
        this.iconOverrideOption && this.setIconOverride(this.calendarOptions[this.iconOverrideOption])
    }, Cn.prototype.setIconOverride = function (e) {
        var t, n;
        if ("object" == typeof e && e) {
            for (n in t = be({}, this.iconClasses), e) t[n] = this.applyIconOverridePrefix(e[n]);
            this.iconClasses = t
        } else !1 === e && (this.iconClasses = {})
    }, Cn.prototype.applyIconOverridePrefix = function (e) {
        var t = this.iconOverridePrefix;
        return t && 0 !== e.indexOf(t) && (e = t + e), e
    }, Cn.prototype.getClass = function (e) {
        return this.classes[e] || ""
    }, Cn.prototype.getIconClass = function (e) {
        var t = this.iconClasses[e];
        return t ? this.baseIconClass + " " + t : ""
    }, Cn.prototype.getCustomButtonIconClass = function (e) {
        var t;
        return this.iconOverrideCustomButtonOption && (t = e[this.iconOverrideCustomButtonOption]) ? this.baseIconClass + " " + this.applyIconOverridePrefix(t) : ""
    }, Cn);

    function Cn(e) {
        this.calendarOptions = e, this.processIconOverride()
    }
    Tn.prototype.classes = {}, Tn.prototype.iconClasses = {}, Tn.prototype.baseIconClass = "", Tn.prototype.iconOverridePrefix = "";
    var En = 0,
        _n = (xn.addEqualityFuncs = function (e) {
            this.prototype.equalityFuncs = be({}, this.prototype.equalityFuncs, e)
        }, xn.prototype.opt = function (e) {
            return this.context.options[e]
        }, xn.prototype.receiveProps = function (e) {
            var t = function (e, t, n) {
                var r = {},
                    i = !1;
                for (var o in t) o in e && (e[o] === t[o] || n[o] && n[o](e[o], t[o])) ? r[o] = e[o] : (r[o] = t[o], i = !0);
                for (var o in e)
                    if (!(o in t)) {
                        i = !0;
                        break
                    } return {
                        anyChanges: i,
                        comboProps: r
                    }
            }(this.props || {}, e, this.equalityFuncs),
                n = t.anyChanges,
                r = t.comboProps;
            this.props = r, n && this.render(r)
        }, xn.prototype.render = function (e) { }, xn.prototype.destroy = function () { }, xn);

    function xn(e, t) {
        t && (e.view = this), this.uid = String(En++), this.context = e, this.dateEnv = e.dateEnv, this.theme = e.theme, this.view = e.view, this.calendar = e.calendar, this.isRtl = "rtl" === this.opt("dir")
    }
    _n.prototype.equalityFuncs = {};
    var In, Rn = (ye(kn, In = _n), kn.prototype.destroy = function () {
        In.prototype.destroy.call(this), r(this.el)
    }, kn.prototype.buildPositionCaches = function () { }, kn.prototype.queryHit = function (e, t, n, r) {
        return null
    }, kn.prototype.isInteractionValid = function (e) {
        var t = this.calendar,
            n = this.props.dateProfile,
            r = e.mutatedEvents.instances;
        if (n)
            for (var i in r)
                if (!Be(n.validRange, r[i].range)) return !1;
        return Ct(e, t)
    }, kn.prototype.isDateSelectionValid = function (e) {
        var t = this.props.dateProfile;
        return !(t && !Be(t.validRange, e.range)) && function (e, t) {
            return Et({
                dateSelection: e
            }, t)
        }(e, this.calendar)
    }, kn.prototype.publiclyTrigger = function (e, t) {
        return this.calendar.publiclyTrigger(e, t)
    }, kn.prototype.publiclyTriggerAfterSizing = function (e, t) {
        return this.calendar.publiclyTriggerAfterSizing(e, t)
    }, kn.prototype.hasPublicHandlers = function (e) {
        return this.calendar.hasPublicHandlers(e)
    }, kn.prototype.triggerRenderedSegs = function (e, t) {
        var n = this.calendar;
        if (this.hasPublicHandlers("eventPositioned"))
            for (var r = 0, i = e; r < i.length; r++) {
                var o = i[r];
                this.publiclyTriggerAfterSizing("eventPositioned", [{
                    event: new dt(n, o.eventRange.def, o.eventRange.instance),
                    isMirror: t,
                    isStart: o.isStart,
                    isEnd: o.isEnd,
                    el: o.el,
                    view: this
                }])
            }
        n.state.loadingLevel || (n.afterSizingTriggers._eventsPositioned = [null])
    }, kn.prototype.triggerWillRemoveSegs = function (e, t) {
        for (var n = this.calendar, r = 0, i = e; r < i.length; r++) {
            var o = i[r];
            n.trigger("eventElRemove", o.el)
        }
        if (this.hasPublicHandlers("eventDestroy"))
            for (var a = 0, s = e; a < s.length; a++) o = s[a], this.publiclyTrigger("eventDestroy", [{
                event: new dt(n, o.eventRange.def, o.eventRange.instance),
                isMirror: t,
                el: o.el,
                view: this
            }])
    }, kn.prototype.isValidSegDownEl = function (e) {
        return !this.props.eventDrag && !this.props.eventResize && !c(e, ".fc-mirror") && (this.isPopover() || !this.isInPopover(e))
    }, kn.prototype.isValidDateDownEl = function (e) {
        var t = c(e, this.fgSegSelector);
        return (!t || t.classList.contains("fc-mirror")) && !c(e, ".fc-more") && !c(e, "a[data-goto]") && !this.isInPopover(e)
    }, kn.prototype.isPopover = function () {
        return this.el.classList.contains("fc-popover")
    }, kn.prototype.isInPopover = function (e) {
        return Boolean(c(e, ".fc-popover"))
    }, kn);

    function kn(e, t, n) {
        var r = In.call(this, e, n) || this;
        return r.el = t, r
    }
    Rn.prototype.fgSegSelector = ".fc-event-container > *", Rn.prototype.bgSegSelector = ".fc-bgevent:not(.fc-nonbusiness)";
    var Pn = 0;

    function Mn(e) {
        return {
            id: String(Pn++),
            deps: e.deps || [],
            reducers: e.reducers || [],
            eventDefParsers: e.eventDefParsers || [],
            isDraggableTransformers: e.isDraggableTransformers || [],
            eventDragMutationMassagers: e.eventDragMutationMassagers || [],
            eventDefMutationAppliers: e.eventDefMutationAppliers || [],
            dateSelectionTransformers: e.dateSelectionTransformers || [],
            datePointTransforms: e.datePointTransforms || [],
            dateSpanTransforms: e.dateSpanTransforms || [],
            views: e.views || {},
            viewPropsTransformers: e.viewPropsTransformers || [],
            isPropsValid: e.isPropsValid || null,
            externalDefTransforms: e.externalDefTransforms || [],
            eventResizeJoinTransforms: e.eventResizeJoinTransforms || [],
            viewContainerModifiers: e.viewContainerModifiers || [],
            eventDropTransformers: e.eventDropTransformers || [],
            componentInteractions: e.componentInteractions || [],
            calendarInteractions: e.calendarInteractions || [],
            themeClasses: e.themeClasses || {},
            eventSourceDefs: e.eventSourceDefs || [],
            cmdFormatter: e.cmdFormatter,
            recurringTypes: e.recurringTypes || [],
            namedTimeZonedImpl: e.namedTimeZonedImpl,
            defaultView: e.defaultView || "",
            elementDraggingImpl: e.elementDraggingImpl,
            optionChangeHandlers: e.optionChangeHandlers || {}
        }
    }
    var On = (Hn.prototype.add = function (e) {
        if (!this.addedHash[e.id]) {
            this.addedHash[e.id] = !0;
            for (var t = 0, n = e.deps; t < n.length; t++) {
                var r = n[t];
                this.add(r)
            }
            this.hooks = function (e, t) {
                return {
                    reducers: e.reducers.concat(t.reducers),
                    eventDefParsers: e.eventDefParsers.concat(t.eventDefParsers),
                    isDraggableTransformers: e.isDraggableTransformers.concat(t.isDraggableTransformers),
                    eventDragMutationMassagers: e.eventDragMutationMassagers.concat(t.eventDragMutationMassagers),
                    eventDefMutationAppliers: e.eventDefMutationAppliers.concat(t.eventDefMutationAppliers),
                    dateSelectionTransformers: e.dateSelectionTransformers.concat(t.dateSelectionTransformers),
                    datePointTransforms: e.datePointTransforms.concat(t.datePointTransforms),
                    dateSpanTransforms: e.dateSpanTransforms.concat(t.dateSpanTransforms),
                    views: be({}, e.views, t.views),
                    viewPropsTransformers: e.viewPropsTransformers.concat(t.viewPropsTransformers),
                    isPropsValid: t.isPropsValid || e.isPropsValid,
                    externalDefTransforms: e.externalDefTransforms.concat(t.externalDefTransforms),
                    eventResizeJoinTransforms: e.eventResizeJoinTransforms.concat(t.eventResizeJoinTransforms),
                    viewContainerModifiers: e.viewContainerModifiers.concat(t.viewContainerModifiers),
                    eventDropTransformers: e.eventDropTransformers.concat(t.eventDropTransformers),
                    calendarInteractions: e.calendarInteractions.concat(t.calendarInteractions),
                    componentInteractions: e.componentInteractions.concat(t.componentInteractions),
                    themeClasses: be({}, e.themeClasses, t.themeClasses),
                    eventSourceDefs: e.eventSourceDefs.concat(t.eventSourceDefs),
                    cmdFormatter: t.cmdFormatter || e.cmdFormatter,
                    recurringTypes: e.recurringTypes.concat(t.recurringTypes),
                    namedTimeZonedImpl: t.namedTimeZonedImpl || e.namedTimeZonedImpl,
                    defaultView: e.defaultView || t.defaultView,
                    elementDraggingImpl: e.elementDraggingImpl || t.elementDraggingImpl,
                    optionChangeHandlers: be({}, e.optionChangeHandlers, t.optionChangeHandlers)
                }
            }(this.hooks, e)
        }
    }, Hn);

    function Hn() {
        this.hooks = {
            reducers: [],
            eventDefParsers: [],
            isDraggableTransformers: [],
            eventDragMutationMassagers: [],
            eventDefMutationAppliers: [],
            dateSelectionTransformers: [],
            datePointTransforms: [],
            dateSpanTransforms: [],
            views: {},
            viewPropsTransformers: [],
            isPropsValid: null,
            externalDefTransforms: [],
            eventResizeJoinTransforms: [],
            viewContainerModifiers: [],
            eventDropTransformers: [],
            componentInteractions: [],
            calendarInteractions: [],
            themeClasses: {},
            eventSourceDefs: [],
            cmdFormatter: null,
            recurringTypes: [],
            namedTimeZonedImpl: null,
            defaultView: "",
            elementDraggingImpl: null,
            optionChangeHandlers: {}
        }, this.addedHash = {}
    }
    var An = Mn({
        eventSourceDefs: [{
            ignoreRange: !0,
            parseMeta: function (e) {
                return Array.isArray(e) ? e : Array.isArray(e.events) ? e.events : null
            },
            fetch: function (e, t) {
                t({
                    rawEvents: e.eventSource.meta
                })
            }
        }]
    }),
        Nn = Mn({
            eventSourceDefs: [{
                parseMeta: function (e) {
                    return "function" == typeof e ? e : "function" == typeof e.events ? e.events : null
                },
                fetch: function (e, t, n) {
                    var r = e.calendar.dateEnv;
                    tn(e.eventSource.meta.bind(null, {
                        start: r.toDate(e.range.start),
                        end: r.toDate(e.range.end),
                        startStr: r.formatIso(e.range.start),
                        endStr: r.formatIso(e.range.end),
                        timeZone: r.timeZone
                    }), function (e) {
                        t({
                            rawEvents: e
                        })
                    }, n)
                }
            }]
        });

    function Ln(e, t, n, r, i) {
        var o = null;
        "GET" === (e = e.toUpperCase()) ? t = function (e, t) {
            return e + (-1 === e.indexOf("?") ? "?" : "&") + Fn(t)
        }(t, n) : o = Fn(n);
        var a = new XMLHttpRequest;
        a.open(e, t, !0), "GET" !== e && a.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"), a.onload = function () {
            if (200 <= a.status && a.status < 400) try {
                var e = JSON.parse(a.responseText);
                r(e, a)
            } catch (e) {
                i("Failure parsing JSON", a)
            } else i("Request failed", a)
        }, a.onerror = function () {
            i("Request failed", a)
        }, a.send(o)
    }

    function Fn(e) {
        var t = [];
        for (var n in e) t.push(encodeURIComponent(n) + "=" + encodeURIComponent(e[n]));
        return t.join("&")
    }
    var zn = Mn({
        eventSourceDefs: [{
            parseMeta: function (e) {
                if ("string" == typeof e) e = {
                    url: e
                };
                else if (!e || "object" != typeof e || !e.url) return null;
                return {
                    url: e.url,
                    method: (e.method || "GET").toUpperCase(),
                    extraParams: e.extraParams,
                    startParam: e.startParam,
                    endParam: e.endParam,
                    timeZoneParam: e.timeZoneParam
                }
            },
            fetch: function (e, n, r) {
                var t = e.eventSource.meta,
                    i = function (e, t, n) {
                        var r, i, o, a, s = n.dateEnv,
                            l = {};
                        null == (r = e.startParam) && (r = n.opt("startParam"));
                        null == (i = e.endParam) && (i = n.opt("endParam"));
                        null == (o = e.timeZoneParam) && (o = n.opt("timeZoneParam"));
                        a = "function" == typeof e.extraParams ? e.extraParams() : e.extraParams || {};
                        be(l, a), l[r] = s.formatIso(t.start), l[i] = s.formatIso(t.end), "local" !== s.timeZone && (l[o] = s.timeZone);
                        return l
                    }(t, e.range, e.calendar);
                Ln(t.method, t.url, i, function (e, t) {
                    n({
                        rawEvents: e,
                        xhr: t
                    })
                }, function (e, t) {
                    r({
                        message: e,
                        xhr: t
                    })
                })
            }
        }]
    });
    var Bn = Mn({
        recurringTypes: [{
            parse: function (e, t, n) {
                var r = n.createMarker.bind(n),
                    i = pe(e, {
                        daysOfWeek: null,
                        startTime: X,
                        endTime: X,
                        startRecur: r,
                        endRecur: r
                    }, {}, t),
                    o = !1;
                for (var a in i)
                    if (null != i[a]) {
                        o = !0;
                        break
                    } if (o) {
                        var s = null;
                        return "duration" in t && (s = X(t.duration), delete t.duration), !s && i.startTime && i.endTime && (s = function (e, t) {
                            return {
                                years: e.years - t.years,
                                months: e.months - t.months,
                                days: e.days - t.days,
                                milliseconds: e.milliseconds - t.milliseconds
                            }
                        }(i.endTime, i.startTime)), {
                            allDayGuess: Boolean(!i.startTime && !i.endTime),
                            duration: s,
                            typeData: i
                        }
                    }
                return null
            },
            expand: function (e, t, n) {
                var r = Le(t, {
                    start: e.startRecur,
                    end: e.endRecur
                });
                return r ? function (e, t, n, r) {
                    var i = e ? Ce(e) : null,
                        o = z(n.start),
                        a = n.end,
                        s = [];
                    for (; o < a;) {
                        var l = void 0;
                        i && !i[o.getUTCDay()] || (l = t ? r.add(o, t) : o, s.push(l)), o = O(o, 1)
                    }
                    return s
                }(e.daysOfWeek, e.startTime, r, n) : []
            }
        }]
    });
    var jn = Mn({
        optionChangeHandlers: {
            events: function (e, t, n) {
                Un([e], t, n)
            },
            eventSources: Un,
            plugins: function (e, t) {
                t.addPluginInputs(e)
            }
        }
    });

    function Un(e, t, n) {
        for (var r = Ee(t.state.eventSources), i = [], o = 0, a = e; o < a.length; o++) {
            for (var s = a[o], l = !1, u = 0; u < r.length; u++)
                if (n(r[u]._raw, s)) {
                    r.splice(u, 1), l = !0;
                    break
                } l || i.push(s)
        }
        for (var c = 0, d = r; c < d.length; c++) {
            var f = d[c];
            t.dispatch({
                type: "REMOVE_EVENT_SOURCE",
                sourceId: f.sourceId
            })
        }
        for (var p = 0, h = i; p < h.length; p++) {
            var g = h[p];
            t.addEventSource(g)
        }
    }
    var Wn = {
        defaultRangeSeparator: " - ",
        titleRangeSeparator: " – ",
        defaultTimedEventDuration: "01:00:00",
        defaultAllDayEventDuration: {
            day: 1
        },
        forceEventDuration: !1,
        nextDayThreshold: "00:00:00",
        columnHeader: !0,
        defaultView: "",
        aspectRatio: 1.35,
        header: {
            left: "title",
            center: "",
            right: "today prev,next"
        },
        weekends: !0,
        weekNumbers: !1,
        weekNumberCalculation: "local",
        editable: !1,
        scrollTime: "06:00:00",
        minTime: "00:00:00",
        maxTime: "24:00:00",
        showNonCurrentDates: !0,
        lazyFetching: !0,
        startParam: "start",
        endParam: "end",
        timeZoneParam: "timeZone",
        timeZone: "local",
        locales: [],
        locale: "",
        timeGridEventMinHeight: 0,
        themeSystem: "standard",
        dragRevertDuration: 500,
        dragScroll: !0,
        allDayMaintainDuration: !1,
        unselectAuto: !0,
        dropAccept: "*",
        eventOrder: "start,-duration,allDay,title",
        eventLimit: !1,
        eventLimitClick: "popover",
        dayPopoverFormat: {
            month: "long",
            day: "numeric",
            year: "numeric"
        },
        handleWindowResize: !0,
        windowResizeDelay: 100,
        longPressDelay: 1e3,
        eventDragMinDistance: 5
    },
        Vn = {
            header: {
                left: "next,prev today",
                center: "",
                right: "title"
            },
            buttonIcons: {
                prev: "fc-icon-chevron-right",
                next: "fc-icon-chevron-left",
                prevYear: "fc-icon-chevrons-right",
                nextYear: "fc-icon-chevrons-left"
            }
        },
        Gn = ["header", "footer", "buttonText", "buttonIcons"];
    var Zn = [An, Nn, zn, Bn, jn];
    var qn = {
        code: "en",
        week: {
            dow: 0,
            doy: 4
        },
        dir: "ltr",
        buttonText: {
            prev: "prev",
            next: "next",
            prevYear: "prev year",
            nextYear: "next year",
            year: "year",
            today: "today",
            month: "month",
            week: "week",
            day: "day",
            list: "list"
        },
        weekLabel: "W",
        allDayText: "all-day",
        eventLimitText: "more",
        noEventsMessage: "No events to display"
    };

    function Yn(e) {
        for (var t = 0 < e.length ? e[0].code : "en", n = window.FullCalendarLocalesAll || [], r = window.FullCalendarLocales || {}, i = n.concat(Ee(r), e), o = {
            en: qn
        }, a = 0, s = i; a < s.length; a++) {
            var l = s[a];
            o[l.code] = l
        }
        return {
            map: o,
            defaultCode: t
        }
    }

    function Xn(e, t) {
        return "object" != typeof e || Array.isArray(e) ? function (e, t) {
            var n = [].concat(e || []),
                r = function (e, t) {
                    for (var n = 0; n < e.length; n++)
                        for (var r = e[n].toLocaleLowerCase().split("-"), i = r.length; 0 < i; i--) {
                            var o = r.slice(0, i).join("-");
                            if (t[o]) return t[o]
                        }
                    return null
                }(n, t) || qn;
            return Jn(e, n, r)
        }(e, t) : Jn(e.code, [e.code], e)
    }

    function Jn(e, t, n) {
        var r = we([qn, n], ["buttonText"]);
        delete r.code;
        var i = r.week;
        return delete r.week, {
            codeArg: e,
            codes: t,
            week: i,
            simpleNumberFormat: new Intl.NumberFormat(e),
            options: r
        }
    }
    var $n = (Qn.prototype.mutate = function (e, t, n) {
        var r = n ? this.dynamicOverrides : this.overrides;
        be(r, e);
        for (var i = 0, o = t; i < o.length; i++) delete r[o[i]];
        this.compute()
    }, Qn.prototype.compute = function () {
        var e = de(this.dynamicOverrides.locales, this.overrides.locales, Wn.locales),
            t = de(this.dynamicOverrides.locale, this.overrides.locale, Wn.locale),
            n = Yn(e),
            r = Xn(t || n.defaultCode, n.map).options,
            i = "rtl" === de(this.dynamicOverrides.dir, this.overrides.dir, r.dir) ? Vn : {};
        this.dirDefaults = i, this.localeDefaults = r, this.computed = function (e) {
            return we(e, Gn)
        }([Wn, i, r, this.overrides, this.dynamicOverrides])
    }, Qn);

    function Qn(e) {
        this.overrides = be({}, e), this.dynamicOverrides = {}, this.compute()
    }
    var Kn = {};
    var er, tr = (nr.prototype.getMarkerYear = function (e) {
        return e.getUTCFullYear()
    }, nr.prototype.getMarkerMonth = function (e) {
        return e.getUTCMonth()
    }, nr.prototype.getMarkerDay = function (e) {
        return e.getUTCDate()
    }, nr.prototype.arrayToMarker = function (e) {
        return V(e)
    }, nr.prototype.markerToArray = function (e) {
        return W(e)
    }, nr);

    function nr() { }
    er = tr, Kn["gregory"] = er;
    var rr = /^\s*(\d{4})(-(\d{2})(-(\d{2})([T ](\d{2}):(\d{2})(:(\d{2})(\.(\d+))?)?(Z|(([-+])(\d{2})(:?(\d{2}))?))?)?)?)?$/;

    function ir(e) {
        var t = rr.exec(e);
        if (t) {
            var n = new Date(Date.UTC(Number(t[1]), t[3] ? Number(t[3]) - 1 : 0, Number(t[5] || 1), Number(t[7] || 0), Number(t[8] || 0), Number(t[10] || 0), t[12] ? 1e3 * Number("0." + t[12]) : 0));
            if (G(n)) {
                var r = null;
                return t[13] && (r = ("-" === t[15] ? -1 : 1) * (60 * Number(t[16] || 0) + Number(t[18] || 0))), {
                    marker: n,
                    isTimeUnspecified: !t[6],
                    timeZoneOffset: r
                }
            }
        }
        return null
    }
    var or = (ar.prototype.createMarker = function (e) {
        var t = this.createMarkerMeta(e);
        return null === t ? null : t.marker
    }, ar.prototype.createNowMarker = function () {
        return this.canComputeOffset ? this.timestampToMarker((new Date).valueOf()) : V(j(new Date))
    }, ar.prototype.createMarkerMeta = function (e) {
        if ("string" == typeof e) return this.parse(e);
        var t = null;
        return "number" == typeof e ? t = this.timestampToMarker(e) : e instanceof Date ? (e = e.valueOf(), isNaN(e) || (t = this.timestampToMarker(e))) : Array.isArray(e) && (t = V(e)), null !== t && G(t) ? {
            marker: t,
            isTimeUnspecified: !1,
            forcedTzo: null
        } : null
    }, ar.prototype.parse = function (e) {
        var t = ir(e);
        if (null === t) return null;
        var n = t.marker,
            r = null;
        return null !== t.timeZoneOffset && (this.canComputeOffset ? n = this.timestampToMarker(n.valueOf() - 60 * t.timeZoneOffset * 1e3) : r = t.timeZoneOffset), {
            marker: n,
            isTimeUnspecified: t.isTimeUnspecified,
            forcedTzo: r
        }
    }, ar.prototype.getYear = function (e) {
        return this.calendarSystem.getMarkerYear(e)
    }, ar.prototype.getMonth = function (e) {
        return this.calendarSystem.getMarkerMonth(e)
    }, ar.prototype.add = function (e, t) {
        var n = this.calendarSystem.markerToArray(e);
        return n[0] += t.years, n[1] += t.months, n[2] += t.days, n[6] += t.milliseconds, this.calendarSystem.arrayToMarker(n)
    }, ar.prototype.subtract = function (e, t) {
        var n = this.calendarSystem.markerToArray(e);
        return n[0] -= t.years, n[1] -= t.months, n[2] -= t.days, n[6] -= t.milliseconds, this.calendarSystem.arrayToMarker(n)
    }, ar.prototype.addYears = function (e, t) {
        var n = this.calendarSystem.markerToArray(e);
        return n[0] += t, this.calendarSystem.arrayToMarker(n)
    }, ar.prototype.addMonths = function (e, t) {
        var n = this.calendarSystem.markerToArray(e);
        return n[1] += t, this.calendarSystem.arrayToMarker(n)
    }, ar.prototype.diffWholeYears = function (e, t) {
        var n = this.calendarSystem;
        return Z(e) === Z(t) && n.getMarkerDay(e) === n.getMarkerDay(t) && n.getMarkerMonth(e) === n.getMarkerMonth(t) ? n.getMarkerYear(t) - n.getMarkerYear(e) : null
    }, ar.prototype.diffWholeMonths = function (e, t) {
        var n = this.calendarSystem;
        return Z(e) === Z(t) && n.getMarkerDay(e) === n.getMarkerDay(t) ? n.getMarkerMonth(t) - n.getMarkerMonth(e) + 12 * (n.getMarkerYear(t) - n.getMarkerYear(e)) : null
    }, ar.prototype.greatestWholeUnit = function (e, t) {
        var n = this.diffWholeYears(e, t);
        return null !== n ? {
            unit: "year",
            value: n
        } : null !== (n = this.diffWholeMonths(e, t)) ? {
            unit: "month",
            value: n
        } : null !== (n = L(e, t)) ? {
            unit: "week",
            value: n
        } : null !== (n = F(e, t)) ? {
            unit: "day",
            value: n
        } : ue(n = function (e, t) {
            return (t.valueOf() - e.valueOf()) / 36e5
        }(e, t)) ? {
                                unit: "hour",
                                value: n
                            } : ue(n = function (e, t) {
                                return (t.valueOf() - e.valueOf()) / 6e4
                            }(e, t)) ? {
                                    unit: "minute",
                                    value: n
                                } : ue(n = function (e, t) {
                                    return (t.valueOf() - e.valueOf()) / 1e3
                                }(e, t)) ? {
                                        unit: "second",
                                        value: n
                                    } : {
                                        unit: "millisecond",
                                        value: t.valueOf() - e.valueOf()
                                    }
    }, ar.prototype.countDurationsBetween = function (e, t, n) {
        var r;
        return n.years && null !== (r = this.diffWholeYears(e, t)) ? r / function (e) {
            return K(e) / 365
        }(n) : n.months && null !== (r = this.diffWholeMonths(e, t)) ? r / function (e) {
            return K(e) / 30
        }(n) : n.days && null !== (r = F(e, t)) ? r / K(n) : (t.valueOf() - e.valueOf()) / ee(n)
    }, ar.prototype.startOf = function (e, t) {
        return "year" === t ? this.startOfYear(e) : "month" === t ? this.startOfMonth(e) : "week" === t ? this.startOfWeek(e) : "day" === t ? z(e) : "hour" === t ? function (e) {
            return V([e.getUTCFullYear(), e.getUTCMonth(), e.getUTCDate(), e.getUTCHours()])
        }(e) : "minute" === t ? function (e) {
            return V([e.getUTCFullYear(), e.getUTCMonth(), e.getUTCDate(), e.getUTCHours(), e.getUTCMinutes()])
        }(e) : "second" === t ? function (e) {
            return V([e.getUTCFullYear(), e.getUTCMonth(), e.getUTCDate(), e.getUTCHours(), e.getUTCMinutes(), e.getUTCSeconds()])
        }(e) : void 0
    }, ar.prototype.startOfYear = function (e) {
        return this.calendarSystem.arrayToMarker([this.calendarSystem.getMarkerYear(e)])
    }, ar.prototype.startOfMonth = function (e) {
        return this.calendarSystem.arrayToMarker([this.calendarSystem.getMarkerYear(e), this.calendarSystem.getMarkerMonth(e)])
    }, ar.prototype.startOfWeek = function (e) {
        return this.calendarSystem.arrayToMarker([this.calendarSystem.getMarkerYear(e), this.calendarSystem.getMarkerMonth(e), e.getUTCDate() - (e.getUTCDay() - this.weekDow + 7) % 7])
    }, ar.prototype.computeWeekNumber = function (e) {
        return this.weekNumberFunc ? this.weekNumberFunc(this.toDate(e)) : function (e, t, n) {
            var r = e.getUTCFullYear(),
                i = B(e, r, t, n);
            if (i < 1) return B(e, r - 1, t, n);
            var o = B(e, r + 1, t, n);
            return 1 <= o ? Math.min(i, o) : i
        }(e, this.weekDow, this.weekDoy)
    }, ar.prototype.format = function (e, t, n) {
        return void 0 === n && (n = {}), t.format({
            marker: e,
            timeZoneOffset: null != n.forcedTzo ? n.forcedTzo : this.offsetForMarker(e)
        }, this)
    }, ar.prototype.formatRange = function (e, t, n, r) {
        return void 0 === r && (r = {}), r.isEndExclusive && (t = H(t, -1)), n.formatRange({
            marker: e,
            timeZoneOffset: null != r.forcedStartTzo ? r.forcedStartTzo : this.offsetForMarker(e)
        }, {
            marker: t,
            timeZoneOffset: null != r.forcedEndTzo ? r.forcedEndTzo : this.offsetForMarker(t)
        }, this)
    }, ar.prototype.formatIso = function (e, t) {
        void 0 === t && (t = {});
        var n = null;
        return t.omitTimeZoneOffset || (n = null != t.forcedTzo ? t.forcedTzo : this.offsetForMarker(e)),
            function (e, t, n) {
                void 0 === n && (n = !1);
                var r = e.toISOString();
                return r = r.replace(".000", ""), n && (r = r.replace("T00:00:00Z", "")), 10 < r.length && (null == t ? r = r.replace("Z", "") : 0 !== t && (r = r.replace("Z", at(t, !0)))), r
            }(e, n, t.omitTime)
    }, ar.prototype.timestampToMarker = function (e) {
        return "local" === this.timeZone ? V(j(new Date(e))) : "UTC" !== this.timeZone && this.namedTimeZoneImpl ? V(this.namedTimeZoneImpl.timestampToArray(e)) : new Date(e)
    }, ar.prototype.offsetForMarker = function (e) {
        return "local" === this.timeZone ? -U(W(e)).getTimezoneOffset() : "UTC" === this.timeZone ? 0 : this.namedTimeZoneImpl ? this.namedTimeZoneImpl.offsetForArray(W(e)) : null
    }, ar.prototype.toDate = function (e, t) {
        return "local" === this.timeZone ? U(W(e)) : "UTC" === this.timeZone ? new Date(e.valueOf()) : this.namedTimeZoneImpl ? new Date(e.valueOf() - 1e3 * this.namedTimeZoneImpl.offsetForArray(W(e)) * 60) : new Date(e.valueOf() - (t || 0))
    }, ar);

    function ar(e) {
        var t = this.timeZone = e.timeZone,
            n = "local" !== t && "UTC" !== t;
        e.namedTimeZoneImpl && n && (this.namedTimeZoneImpl = new e.namedTimeZoneImpl(t)), this.canComputeOffset = Boolean(!n || this.namedTimeZoneImpl), this.calendarSystem = function (e) {
            return new Kn[e]
        }(e.calendarSystem), this.locale = e.locale, this.weekDow = e.locale.week.dow, this.weekDoy = e.locale.week.doy, "ISO" === e.weekNumberCalculation && (this.weekDow = 1, this.weekDoy = 4), "number" == typeof e.firstDay && (this.weekDow = e.firstDay), "function" == typeof e.weekNumberCalculation && (this.weekNumberFunc = e.weekNumberCalculation), this.weekLabel = null != e.weekLabel ? e.weekLabel : e.locale.options.weekLabel, this.cmdFormatter = e.cmdFormatter
    }
    var sr = {
        id: String,
        allDayDefault: Boolean,
        eventDataTransform: Function,
        success: Function,
        failure: Function
    },
        lr = 0;

    function ur(e, t) {
        return !t.pluginSystem.hooks.eventSourceDefs[e.sourceDefId].ignoreRange
    }

    function cr(e, t) {
        for (var n = t.pluginSystem.hooks.eventSourceDefs, r = n.length - 1; 0 <= r; r--) {
            var i = n[r].parseMeta(e);
            if (i) {
                var o = dr("object" == typeof e ? e : {}, i, r, t);
                return o._raw = e, o
            }
        }
        return null
    }

    function dr(e, t, n, r) {
        var i = {},
            o = pe(e, sr, {}, i),
            a = {},
            s = At(i, r, a);
        return o.isFetching = !1, o.latestFetchId = "", o.fetchRange = null, o.publicId = String(e.id || ""), o.sourceId = String(lr++), o.sourceDefId = n, o.meta = t, o.ui = s, o.extendedProps = a, o
    }

    function fr(e, t, n, r) {
        switch (t.type) {
            case "ADD_EVENT_SOURCES":
                return function (e, t, n, r) {
                    for (var i = {}, o = 0, a = t; o < a.length; o++) {
                        var s = a[o];
                        i[s.sourceId] = s
                    }
                    n && (i = hr(i, n, r));
                    return be({}, e, i)
                }(e, t.sources, n ? n.activeRange : null, r);
            case "REMOVE_EVENT_SOURCE":
                return function (e, t) {
                    return De(e, function (e) {
                        return e.sourceId !== t
                    })
                }(e, t.sourceId);
            case "PREV":
            case "NEXT":
            case "SET_DATE":
            case "SET_VIEW_TYPE":
                return n ? hr(e, n.activeRange, r) : e;
            case "FETCH_EVENT_SOURCES":
            case "CHANGE_TIMEZONE":
                return gr(e, t.sourceIds ? Ce(t.sourceIds) : function (e, t) {
                    return De(e, function (e) {
                        return ur(e, t)
                    })
                }(e, r), n ? n.activeRange : null, r);
            case "RECEIVE_EVENTS":
            case "RECEIVE_EVENT_ERROR":
                return function (e, t, n, r) {
                    var i, o = e[t];
                    if (o && n === o.latestFetchId) return be({}, e, ((i = {})[t] = be({}, o, {
                        isFetching: !1,
                        fetchRange: r
                    }), i));
                    return e
                }(e, t.sourceId, t.fetchId, t.fetchRange);
            case "REMOVE_ALL_EVENT_SOURCES":
                return {};
            default:
                return e
        }
    }
    var pr = 0;

    function hr(e, t, n) {
        return gr(e, De(e, function (e) {
            return function (e, t, n) {
                return ur(e, n) ? !n.opt("lazyFetching") || !e.fetchRange || t.start < e.fetchRange.start || t.end > e.fetchRange.end : !e.latestFetchId
            }(e, t, n)
        }), t, n)
    }

    function gr(e, t, n, r) {
        var i = {};
        for (var o in e) {
            var a = e[o];
            t[o] ? i[o] = vr(a, n, r) : i[o] = a
        }
        return i
    }

    function vr(o, a, s) {
        var e = s.pluginSystem.hooks.eventSourceDefs[o.sourceDefId],
            l = String(pr++);
        return e.fetch({
            eventSource: o,
            calendar: s,
            range: a
        }, function (e) {
            var t, n, r = e.rawEvents,
                i = s.opt("eventSourceSuccess");
            o.success && (n = o.success(r, e.xhr)), i && (t = i(r, e.xhr)), r = n || t || r, s.dispatch({
                type: "RECEIVE_EVENTS",
                sourceId: o.sourceId,
                fetchId: l,
                fetchRange: a,
                rawEvents: r
            })
        }, function (e) {
            var t = s.opt("eventSourceFailure");
            console.warn(e.message, e), o.failure && o.failure(e), t && t(e), s.dispatch({
                type: "RECEIVE_EVENT_ERROR",
                sourceId: o.sourceId,
                fetchId: l,
                fetchRange: a,
                error: e
            })
        }), be({}, o, {
            isFetching: !0,
            latestFetchId: l
        })
    }
    var mr = (yr.prototype.buildPrev = function (e, t) {
        var n = this.dateEnv,
            r = n.subtract(n.startOf(t, e.currentRangeUnit), e.dateIncrement);
        return this.build(r, -1)
    }, yr.prototype.buildNext = function (e, t) {
        var n = this.dateEnv,
            r = n.add(n.startOf(t, e.currentRangeUnit), e.dateIncrement);
        return this.build(r, 1)
    }, yr.prototype.build = function (e, t, n) {
        var r, i, o, a, s, l, u, c;
        return void 0 === n && (n = !1), r = this.buildValidRange(), r = this.trimHiddenDays(r), n && (e = function (e, t) {
            return null != t.start && e < t.start ? t.start : null != t.end && e >= t.end ? new Date(t.end.valueOf() - 1) : e
        }(e, r)), a = this.buildCurrentRangeInfo(e, t), s = /^(year|month|week|day)$/.test(a.unit), l = this.buildRenderRange(this.trimHiddenDays(a.range), a.unit, s), u = l = this.trimHiddenDays(l), this.options.showNonCurrentDates || (u = Le(u, a.range)), i = X(this.options.minTime), o = X(this.options.maxTime), u = Le(u = this.adjustActiveRange(u, i, o), r), c = ze(a.range, r), {
            validRange: r,
            currentRange: a.range,
            currentRangeUnit: a.unit,
            isRangeAllDay: s,
            activeRange: u,
            renderRange: l,
            minTime: i,
            maxTime: o,
            isValid: c,
            dateIncrement: this.buildDateIncrement(a.duration)
        }
    }, yr.prototype.buildValidRange = function () {
        return this.getRangeOption("validRange", this.calendar.getNow()) || {
            start: null,
            end: null
        }
    }, yr.prototype.buildCurrentRangeInfo = function (e, t) {
        var n, r = this.viewSpec,
            i = this.dateEnv,
            o = null,
            a = null,
            s = null;
        return r.duration ? (o = r.duration, a = r.durationUnit, s = this.buildRangeFromDuration(e, t, o, a)) : (n = this.options.dayCount) ? (a = "day", s = this.buildRangeFromDayCount(e, t, n)) : (s = this.buildCustomVisibleRange(e)) ? a = i.greatestWholeUnit(s.start, s.end).unit : (a = te(o = this.getFallbackDuration()).unit, s = this.buildRangeFromDuration(e, t, o, a)), {
            duration: o,
            unit: a,
            range: s
        }
    }, yr.prototype.getFallbackDuration = function () {
        return X({
            day: 1
        })
    }, yr.prototype.adjustActiveRange = function (e, t, n) {
        var r = this.dateEnv,
            i = e.start,
            o = e.end;
        return this.viewSpec.class.prototype.usesMinMaxTime && (K(t) < 0 && (i = z(i), i = r.add(i, t)), 1 < K(n) && (o = O(o = z(o), -1), o = r.add(o, n))), {
            start: i,
            end: o
        }
    }, yr.prototype.buildRangeFromDuration = function (e, t, n, r) {
        var i, o, a, s, l, u = this.dateEnv,
            c = this.options.dateAlignment;

        function d() {
            a = u.startOf(e, c), s = u.add(a, n), l = {
                start: a,
                end: s
            }
        }
        return c || (i = this.options.dateIncrement, c = i && ee(o = X(i)) < ee(n) ? te(o, !$(i)).unit : r), K(n) <= 1 && this.isHiddenDay(a) && (a = z(a = this.skipHiddenDays(a, t))), d(), this.trimHiddenDays(l) || (e = this.skipHiddenDays(e, t), d()), l
    }, yr.prototype.buildRangeFromDayCount = function (e, t, n) {
        var r, i = this.dateEnv,
            o = this.options.dateAlignment,
            a = 0,
            s = e;
        for (o && (s = i.startOf(s, o)), s = z(s), r = s = this.skipHiddenDays(s, t); r = O(r, 1), this.isHiddenDay(r) || a++, a < n;);
        return {
            start: s,
            end: r
        }
    }, yr.prototype.buildCustomVisibleRange = function (e) {
        var t = this.dateEnv,
            n = this.getRangeOption("visibleRange", t.toDate(e));
        return !n || null != n.start && null != n.end ? n : null
    }, yr.prototype.buildRenderRange = function (e, t, n) {
        return e
    }, yr.prototype.buildDateIncrement = function (e) {
        var t, n = this.options.dateIncrement;
        return n ? X(n) : (t = this.options.dateAlignment) ? X(1, t) : e || X({
            days: 1
        })
    }, yr.prototype.getRangeOption = function (e) {
        for (var t = [], n = 1; n < arguments.length; n++) t[n - 1] = arguments[n];
        var r = this.options[e];
        return "function" == typeof r && (r = r.apply(null, t)), r = (r = r && function (e, t) {
            var n = null,
                r = null;
            return e.start && (n = t.createMarker(e.start)), e.end && (r = t.createMarker(e.end)), n || r ? n && r && r < n ? null : {
                start: n,
                end: r
            } : null
        }(r, this.dateEnv)) && ge(r)
    }, yr.prototype.initHiddenDays = function () {
        var e, t = this.options.hiddenDays || [],
            n = [],
            r = 0;
        for (!1 === this.options.weekends && t.push(0, 6), e = 0; e < 7; e++)(n[e] = -1 !== t.indexOf(e)) || r++;
        if (!r) throw new Error("invalid hiddenDays");
        this.isHiddenDayHash = n
    }, yr.prototype.trimHiddenDays = function (e) {
        var t = e.start,
            n = e.end;
        return t = t && this.skipHiddenDays(t), n = n && this.skipHiddenDays(n, -1, !0), null == t || null == n || t < n ? {
            start: t,
            end: n
        } : null
    }, yr.prototype.isHiddenDay = function (e) {
        return e instanceof Date && (e = e.getUTCDay()), this.isHiddenDayHash[e]
    }, yr.prototype.skipHiddenDays = function (e, t, n) {
        for (void 0 === t && (t = 1), void 0 === n && (n = !1); this.isHiddenDayHash[(e.getUTCDay() + (n ? t : 0) + 7) % 7];) e = O(e, t);
        return e
    }, yr);

    function yr(e, t) {
        this.viewSpec = e, this.options = e.options, this.dateEnv = t.dateEnv, this.calendar = t, this.initHiddenDays()
    }

    function br(e, t, n) {
        for (var r = function (e, t) {
            switch (t.type) {
                case "SET_VIEW_TYPE":
                    return t.viewType;
                default:
                    return e
            }
        }(e.viewType, t), i = function (e, t, n, r, i) {
            var o;
            switch (t.type) {
                case "PREV":
                    o = i.dateProfileGenerators[r].buildPrev(e, n);
                    break;
                case "NEXT":
                    o = i.dateProfileGenerators[r].buildNext(e, n);
                    break;
                case "SET_DATE":
                    e.activeRange && je(e.currentRange, t.dateMarker) || (o = i.dateProfileGenerators[r].build(t.dateMarker, void 0, !0));
                    break;
                case "SET_VIEW_TYPE":
                    var a = i.dateProfileGenerators[r];
                    if (!a) throw new Error(r ? 'The FullCalendar view "' + r + '" does not exist. Make sure your plugins are loaded correctly.' : "No available FullCalendar view plugins.");
                    o = a.build(t.dateMarker || n, void 0, !0)
            }
            return !o || !o.isValid || e && function (e, t) {
                return Fe(e.validRange, t.validRange) && Fe(e.activeRange, t.activeRange) && Fe(e.renderRange, t.renderRange) && Q(e.minTime, t.minTime) && Q(e.maxTime, t.maxTime)
            }(e, o) ? e : o
        }(e.dateProfile, t, e.currentDate, r, n), o = fr(e.eventSources, t, i, n), a = be({}, e, {
            viewType: r,
            dateProfile: i,
            currentDate: function (e, t, n) {
                switch (t.type) {
                    case "PREV":
                    case "NEXT":
                        return je(n.currentRange, e) ? e : n.currentRange.start;
                    case "SET_DATE":
                    case "SET_VIEW_TYPE":
                        var r = t.dateMarker || e;
                        return n.activeRange && !je(n.activeRange, r) ? n.currentRange.start : r;
                    default:
                        return e
                }
            }(e.currentDate, t, i),
            eventSources: o,
            eventStore: wt(e.eventStore, t, o, i, n),
            dateSelection: function (e, t) {
                switch (t.type) {
                    case "SELECT_DATES":
                        return t.selection;
                    case "UNSELECT_DATES":
                        return null;
                    default:
                        return e
                }
            }(e.dateSelection, t),
            eventSelection: function (e, t) {
                switch (t.type) {
                    case "SELECT_EVENT":
                        return t.eventInstanceId;
                    case "UNSELECT_EVENT":
                        return "";
                    default:
                        return e
                }
            }(e.eventSelection, t),
            eventDrag: function (e, t) {
                switch (t.type) {
                    case "SET_EVENT_DRAG":
                        var n = t.state;
                        return {
                            affectedEvents: n.affectedEvents, mutatedEvents: n.mutatedEvents, isEvent: n.isEvent, origSeg: n.origSeg
                        };
                    case "UNSET_EVENT_DRAG":
                        return null;
                    default:
                        return e
                }
            }(e.eventDrag, t),
            eventResize: function (e, t) {
                switch (t.type) {
                    case "SET_EVENT_RESIZE":
                        var n = t.state;
                        return {
                            affectedEvents: n.affectedEvents, mutatedEvents: n.mutatedEvents, isEvent: n.isEvent, origSeg: n.origSeg
                        };
                    case "UNSET_EVENT_RESIZE":
                        return null;
                    default:
                        return e
                }
            }(e.eventResize, t),
            eventSourceLoadingLevel: Sr(o),
            loadingLevel: Sr(o)
        }), s = 0, l = n.pluginSystem.hooks.reducers; s < l.length; s++) {
            a = (0, l[s])(a, t, n)
        }
        return a
    }

    function Sr(e) {
        var t = 0;
        for (var n in e) e[n].isFetching && t++;
        return t
    }
    var wr = {
        start: null,
        end: null,
        allDay: Boolean
    };

    function Dr(e, t, n) {
        var r = function (e, t) {
            var n = {},
                r = pe(e, wr, {}, n),
                i = r.start ? t.createMarkerMeta(r.start) : null,
                o = r.end ? t.createMarkerMeta(r.end) : null,
                a = r.allDay;
            null == a && (a = i && i.isTimeUnspecified && (!o || o.isTimeUnspecified));
            return n.range = {
                start: i ? i.marker : null,
                end: o ? o.marker : null
            }, n.allDay = a, n
        }(e, t),
            i = r.range;
        if (!i.start) return null;
        if (!i.end) {
            if (null == n) return null;
            i.end = t.add(i.start, n)
        }
        return r
    }

    function Tr(e, t, n, r) {
        if (t[e]) return t[e];
        var i = function (e, t, n, r) {
            function i(e) {
                return o && null !== o[e] ? o[e] : a && null !== a[e] ? a[e] : null
            }
            var o = n[e],
                a = r[e],
                s = i("class"),
                l = i("superType");
            !l && s && (l = Cr(s, r) || Cr(s, n));
            var u = null;
            if (l) {
                if (l === e) throw new Error("Can't have a custom view type that references itself");
                u = Tr(l, t, n, r)
            } !s && u && (s = u.class);
            return s ? {
                type: e,
                class: s,
                defaults: be({}, u ? u.defaults : {}, o ? o.options : {}),
                overrides: be({}, u ? u.overrides : {}, a ? a.options : {})
            } : null
        }(e, t, n, r);
        return i && (t[e] = i), i
    }

    function Cr(e, t) {
        var n = Object.getPrototypeOf(e.prototype);
        for (var r in t) {
            var i = t[r];
            if (i.class && i.class.prototype === n) return r
        }
        return ""
    }

    function Er(e) {
        return Te(e, xr)
    }
    var _r = {
        type: String,
        class: null
    };

    function xr(e) {
        "function" == typeof e && (e = {
            class: e
        });
        var t = {},
            n = pe(e, _r, {}, t);
        return {
            superType: n.type,
            class: n.class,
            options: t
        }
    }

    function Ir(e, t) {
        var n = Er(e),
            r = Er(t.overrides.views);
        return Te(function (e, t) {
            var n, r = {};
            for (n in e) Tr(n, r, e, t);
            for (n in t) Tr(n, r, e, t);
            return r
        }(n, r), function (e) {
            return function (r, e, t) {
                var n = r.overrides.duration || r.defaults.duration || t.dynamicOverrides.duration || t.overrides.duration,
                    i = null,
                    o = "",
                    a = "",
                    s = {};
                if (n && (i = X(n))) {
                    var l = te(i, !$(n));
                    o = l.unit, 1 === l.value && (s = e[a = o] ? e[o].options : {})
                }

                function u(e) {
                    var t = e.buttonText || {},
                        n = r.defaults.buttonTextKey;
                    return null != n && null != t[n] ? t[n] : null != t[r.type] ? t[r.type] : null != t[a] ? t[a] : void 0
                }
                return {
                    type: r.type,
                    class: r.class,
                    duration: i,
                    durationUnit: o,
                    singleUnit: a,
                    options: be({}, Wn, r.defaults, t.dirDefaults, t.localeDefaults, t.overrides, s, r.overrides, t.dynamicOverrides),
                    buttonTextOverride: u(t.dynamicOverrides) || u(t.overrides) || r.overrides.buttonText,
                    buttonTextDefault: u(t.localeDefaults) || u(t.dirDefaults) || r.defaults.buttonText || u(Wn) || r.type
                }
            }(e, r, t)
        })
    }
    var Rr, kr = (ye(Pr, Rr = _n), Pr.prototype.destroy = function () {
        Rr.prototype.destroy.call(this), this._renderLayout.unrender(), r(this.el)
    }, Pr.prototype.render = function (e) {
        this._renderLayout(e.layout), this._updateTitle(e.title), this._updateActiveButton(e.activeButton), this._updateToday(e.isTodayEnabled), this._updatePrev(e.isPrevEnabled), this._updateNext(e.isNextEnabled)
    }, Pr.prototype.renderLayout = function (e) {
        var t = this.el;
        this.viewsWithButtons = [], w(t, this.renderSection("left", e.left)), w(t, this.renderSection("center", e.center)), w(t, this.renderSection("right", e.right))
    }, Pr.prototype.unrenderLayout = function () {
        this.el.innerHTML = ""
    }, Pr.prototype.renderSection = function (e, t) {
        var p = this,
            h = this.theme,
            g = this.calendar,
            n = g.optionsManager,
            v = g.viewSpecs,
            i = a("div", {
                className: "fc-" + e
            }),
            m = n.computed.customButtons || {},
            y = n.overrides.buttonText || {},
            b = n.computed.buttonText || {};
        return t && t.split(" ").forEach(function (e, t) {
            var n, d = [],
                f = !0;
            if (e.split(",").forEach(function (e, t) {
                var n, r, i, o, a, s, l, u, c;
                "title" === e ? (d.push(S("<h2>&nbsp;</h2>")), f = !1) : ((n = m[e]) ? (i = function (e) {
                    n.click && n.click.call(u, e)
                }, (o = h.getCustomButtonIconClass(n)) || (o = h.getIconClass(e)) || (a = n.text)) : (r = v[e]) ? (p.viewsWithButtons.push(e), i = function () {
                    g.changeView(e)
                }, (a = r.buttonTextOverride) || (o = h.getIconClass(e)) || (a = r.buttonTextDefault)) : g[e] && (i = function () {
                    g[e]()
                }, (a = y[e]) || (o = h.getIconClass(e)) || (a = b[e])), i && (l = ["fc-" + e + "-button", h.getClass("button")], a ? (s = Pt(a), c = "") : o && (s = "<span class='" + o + "'></span>", c = ' aria-label="' + e + '"'), (u = S('<button type="button" class="' + l.join(" ") + '"' + c + ">" + s + "</button>")).addEventListener("click", i), d.push(u)))
            }), 1 < d.length) {
                n = document.createElement("div");
                var r = h.getClass("buttonGroup");
                f && r && n.classList.add(r), w(n, d), i.appendChild(n)
            } else w(i, d)
        }), i
    }, Pr.prototype.updateToday = function (e) {
        this.toggleButtonEnabled("today", e)
    }, Pr.prototype.updatePrev = function (e) {
        this.toggleButtonEnabled("prev", e)
    }, Pr.prototype.updateNext = function (e) {
        this.toggleButtonEnabled("next", e)
    }, Pr.prototype.updateTitle = function (t) {
        p(this.el, "h2").forEach(function (e) {
            e.innerText = t
        })
    }, Pr.prototype.updateActiveButton = function (t) {
        var n = this.theme.getClass("buttonActive");
        p(this.el, "button").forEach(function (e) {
            t && e.classList.contains("fc-" + t + "-button") ? e.classList.add(n) : e.classList.remove(n)
        })
    }, Pr.prototype.toggleButtonEnabled = function (e, t) {
        p(this.el, ".fc-" + e + "-button").forEach(function (e) {
            e.disabled = !t
        })
    }, Pr);

    function Pr(e, t) {
        var n = Rr.call(this, e) || this;
        return n._renderLayout = Yt(n.renderLayout, n.unrenderLayout), n._updateTitle = Yt(n.updateTitle, null, [n._renderLayout]), n._updateActiveButton = Yt(n.updateActiveButton, null, [n._renderLayout]), n._updateToday = Yt(n.updateToday, null, [n._renderLayout]), n._updatePrev = Yt(n.updatePrev, null, [n._renderLayout]), n._updateNext = Yt(n.updateNext, null, [n._renderLayout]), n.el = a("div", {
            className: "fc-toolbar " + t
        }), n
    }
    var Mr, Or = (ye(Hr, Mr = _n), Hr.prototype.destroy = function () {
        this.header && this.header.destroy(), this.footer && this.footer.destroy(), this.view && this.view.destroy(), r(this.contentEl), this.toggleElClassNames(!1), Mr.prototype.destroy.call(this)
    }, Hr.prototype.toggleElClassNames = function (e) {
        var t = this.el.classList,
            n = "fc-" + this.opt("dir"),
            r = this.theme.getClass("widget");
        e ? (t.add("fc"), t.add(n), t.add(r)) : (t.remove("fc"), t.remove(n), t.remove(r))
    }, Hr.prototype.render = function (e) {
        this.freezeHeight();
        var t = this.computeTitle(e.dateProfile, e.viewSpec.options);
        this._renderToolbars(e.viewSpec, e.dateProfile, e.currentDate, e.dateProfileGenerator, t), this.renderView(e, t), this.updateSize(), this.thawHeight()
    }, Hr.prototype.renderToolbars = function (e, t, n, r, i) {
        var o = this.opt("header"),
            a = this.opt("footer"),
            s = this.calendar.getNow(),
            l = r.build(s),
            u = r.buildPrev(t, n),
            c = r.buildNext(t, n),
            d = {
                title: i,
                activeButton: e.type,
                isTodayEnabled: l.isValid && !je(t.currentRange, s),
                isPrevEnabled: u.isValid,
                isNextEnabled: c.isValid
            };
        o ? (this.header || (this.header = new kr(this.context, "fc-header-toolbar"), f(this.el, this.header.el)), this.header.receiveProps(be({
            layout: o
        }, d))) : this.header && (this.header.destroy(), this.header = null), a ? (this.footer || (this.footer = new kr(this.context, "fc-footer-toolbar"), w(this.el, this.footer.el)), this.footer.receiveProps(be({
            layout: a
        }, d))) : this.footer && (this.footer.destroy(), this.footer = null)
    }, Hr.prototype.renderView = function (e, t) {
        var n = this.view,
            r = e.viewSpec,
            i = e.dateProfileGenerator;
        n && n.viewSpec === r ? n.addScroll(n.queryScroll()) : (n && n.destroy(), n = this.view = new r.class({
            calendar: this.calendar, view: null, dateEnv: this.dateEnv, theme: this.theme, options: r.options
        }, r, i, this.contentEl)), n.title = t;
        for (var o = {
            dateProfile: e.dateProfile,
            businessHours: this.parseBusinessHours(r.options.businessHours),
            eventStore: e.eventStore,
            eventUiBases: e.eventUiBases,
            dateSelection: e.dateSelection,
            eventSelection: e.eventSelection,
            eventDrag: e.eventDrag,
            eventResize: e.eventResize
        }, a = 0, s = this.buildViewPropTransformers(this.calendar.pluginSystem.hooks.viewPropsTransformers); a < s.length; a++) {
            var l = s[a];
            be(o, l.transform(o, r, e, n))
        }
        n.receiveProps(o)
    }, Hr.prototype.updateSize = function (e) {
        void 0 === e && (e = !1);
        var t = this.view;
        e && t.addScroll(t.queryScroll()), !e && null != this.isHeightAuto || this.computeHeightVars(), t.updateSize(e, this.viewHeight, this.isHeightAuto), t.updateNowIndicator(), t.popScroll(e)
    }, Hr.prototype.computeHeightVars = function () {
        var e = this.calendar,
            t = e.opt("height"),
            n = e.opt("contentHeight");
        if (this.isHeightAuto = "auto" === t || "auto" === n, "number" == typeof n) this.viewHeight = n;
        else if ("function" == typeof n) this.viewHeight = n();
        else if ("number" == typeof t) this.viewHeight = t - this.queryToolbarsHeight();
        else if ("function" == typeof t) this.viewHeight = t() - this.queryToolbarsHeight();
        else if ("parent" === t) {
            var r = this.el.parentNode;
            this.viewHeight = r.getBoundingClientRect().height - this.queryToolbarsHeight()
        } else this.viewHeight = Math.round(this.contentEl.getBoundingClientRect().width / Math.max(e.opt("aspectRatio"), .5))
    }, Hr.prototype.queryToolbarsHeight = function () {
        var e = 0;
        return this.header && (e += _(this.header.el)), this.footer && (e += _(this.footer.el)), e
    }, Hr.prototype.freezeHeight = function () {
        g(this.el, {
            height: this.el.getBoundingClientRect().height,
            overflow: "hidden"
        })
    }, Hr.prototype.thawHeight = function () {
        g(this.el, {
            height: "",
            overflow: ""
        })
    }, Hr);

    function Hr(e, t) {
        var n = Mr.call(this, e) || this;
        n._renderToolbars = Yt(n.renderToolbars), n.buildViewPropTransformers = We(Nr), f(n.el = t, n.contentEl = a("div", {
            className: "fc-view-container"
        }));
        for (var r = n.calendar, i = 0, o = r.pluginSystem.hooks.viewContainerModifiers; i < o.length; i++) {
            (0, o[i])(n.contentEl, r)
        }
        return n.toggleElClassNames(!0), n.computeTitle = We(Ar), n.parseBusinessHours = We(function (e) {
            return qt(e, n.calendar)
        }), n
    }

    function Ar(e, t) {
        var n;
        return n = /^(year|month)$/.test(e.currentRangeUnit) ? e.currentRange : e.activeRange, this.dateEnv.formatRange(n.start, n.end, ot(t.titleFormat || function (e) {
            var t = e.currentRangeUnit; {
                if ("year" === t) return {
                    year: "numeric"
                };
                if ("month" === t) return {
                    year: "numeric",
                    month: "long"
                };
                var n = F(e.currentRange.start, e.currentRange.end);
                return null !== n && 1 < n ? {
                    year: "numeric",
                    month: "short",
                    day: "numeric"
                } : {
                        year: "numeric",
                        month: "long",
                        day: "numeric"
                    }
            }
        }(e), t.titleRangeSeparator), {
            isEndExclusive: e.isRangeAllDay
        })
    }

    function Nr(e) {
        return e.map(function (e) {
            return new e
        })
    }
    var Lr = (Fr.prototype.destroy = function () { }, Fr);

    function Fr(e) {
        this.component = e.component
    }
    var zr, Br = {},
        jr = (ye(Ur, zr = Lr), Ur);

    function Ur(e) {
        var a = zr.call(this, e) || this;
        a.handleSegClick = function (e, t) {
            var n = a.component,
                r = gt(t);
            if (r && n.isValidSegDownEl(e.target)) {
                var i = c(e.target, ".fc-has-url"),
                    o = i ? i.querySelector("a[href]").href : "";
                n.publiclyTrigger("eventClick", [{
                    el: t,
                    event: new dt(n.calendar, r.eventRange.def, r.eventRange.instance),
                    jsEvent: e,
                    view: n.view
                }]), o && !e.defaultPrevented && (window.location.href = o)
            }
        };
        var t = e.component;
        return a.destroy = k(t.el, "click", t.fgSegSelector + "," + t.bgSegSelector, a.handleSegClick), a
    }
    var Wr, Vr = (ye(Gr, Wr = Lr), Gr.prototype.destroy = function () {
        this.removeHoverListeners(), this.component.calendar.off("eventElRemove", this.handleEventElRemove)
    }, Gr.prototype.triggerEvent = function (e, t, n) {
        var r = this.component,
            i = gt(n);
        t && !r.isValidSegDownEl(t.target) || r.publiclyTrigger(e, [{
            el: n,
            event: new dt(this.component.calendar, i.eventRange.def, i.eventRange.instance),
            jsEvent: t,
            view: r.view
        }])
    }, Gr);

    function Gr(e) {
        var n = Wr.call(this, e) || this;
        n.handleEventElRemove = function (e) {
            e === n.currentSegEl && n.handleSegLeave(null, n.currentSegEl)
        }, n.handleSegEnter = function (e, t) {
            gt(t) && (t.classList.add("fc-allow-mouse-resize"), n.currentSegEl = t, n.triggerEvent("eventMouseEnter", e, t))
        }, n.handleSegLeave = function (e, t) {
            n.currentSegEl && (t.classList.remove("fc-allow-mouse-resize"), n.currentSegEl = null, n.triggerEvent("eventMouseLeave", e, t))
        };
        var t = e.component;
        return n.removeHoverListeners = function (e, t, r, i) {
            var o;
            return k(e, "mouseover", t, function (e, t) {
                if (t !== o) {
                    r(e, o = t);
                    var n = function (e) {
                        o = null, i(e, t), t.removeEventListener("mouseleave", n)
                    };
                    t.addEventListener("mouseleave", n)
                }
            })
        }(t.el, t.fgSegSelector + "," + t.bgSegSelector, n.handleSegEnter, n.handleSegLeave), t.calendar.on("eventElRemove", n.handleEventElRemove), n
    }
    var Zr, qr = (ye(Yr, Zr = Tn), Yr);

    function Yr() {
        return null !== Zr && Zr.apply(this, arguments) || this
    }
    qr.prototype.classes = {
        widget: "fc-unthemed",
        widgetHeader: "fc-widget-header",
        widgetContent: "fc-widget-content",
        buttonGroup: "fc-button-group",
        button: "fc-button fc-button-primary",
        buttonActive: "fc-button-active",
        popoverHeader: "fc-widget-header",
        popoverContent: "fc-widget-content",
        headerRow: "fc-widget-header",
        dayRow: "fc-widget-content",
        listView: "fc-widget-content"
    }, qr.prototype.baseIconClass = "fc-icon", qr.prototype.iconClasses = {
        close: "fc-icon-x",
        prev: "fc-icon-chevron-left",
        next: "fc-icon-chevron-right",
        prevYear: "fc-icon-chevrons-left",
        nextYear: "fc-icon-chevrons-right"
    }, qr.prototype.iconOverrideOption = "buttonIcons", qr.prototype.iconOverrideCustomButtonOption = "icon", qr.prototype.iconOverridePrefix = "fc-icon-";
    var Xr = (Jr.prototype.addPluginInputs = function (e) {
        for (var t = 0, n = function (e) {
            for (var t = [], n = 0, r = e; n < r.length; n++) {
                var i = r[n];
                if ("string" == typeof i) {
                    var o = "FullCalendar" + se(i);
                    window[o] ? t.push(window[o].default) : console.warn("Plugin file not loaded for " + i)
                } else t.push(i)
            }
            return Zn.concat(t)
        }(e); t < n.length; t++) {
            var r = n[t];
            this.pluginSystem.add(r)
        }
    }, Object.defineProperty(Jr.prototype, "view", {
        get: function () {
            return this.component ? this.component.view : null
        },
        enumerable: !0,
        configurable: !0
    }), Jr.prototype.render = function () {
        this.component ? this.requestRerender(!0) : (this.renderableEventStore = {
            defs: {},
            instances: {}
        }, this.bindHandlers(), this.executeRender())
    }, Jr.prototype.destroy = function () {
        if (this.component) {
            this.unbindHandlers(), this.component.destroy(), this.component = null;
            for (var e = 0, t = this.calendarInteractions; e < t.length; e++) t[e].destroy();
            this.publiclyTrigger("_destroyed")
        }
    }, Jr.prototype.bindHandlers = function () {
        var s = this;
        this.removeNavLinkListener = k(this.el, "click", "a[data-goto]", function (e, t) {
            var n = t.getAttribute("data-goto");
            n = n ? JSON.parse(n) : {};
            var r = s.dateEnv,
                i = r.createMarker(n.date),
                o = n.type,
                a = s.viewOpt("navLink" + se(o) + "Click");
            "function" == typeof a ? a(r.toDate(i), e) : ("string" == typeof a && (o = a), s.zoomTo(i, o))
        }), this.opt("handleWindowResize") && window.addEventListener("resize", this.windowResizeProxy = fe(this.windowResize.bind(this), this.opt("windowResizeDelay")))
    }, Jr.prototype.unbindHandlers = function () {
        this.removeNavLinkListener(), this.windowResizeProxy && (window.removeEventListener("resize", this.windowResizeProxy), this.windowResizeProxy = null)
    }, Jr.prototype.hydrate = function () {
        var e = this;
        this.state = this.buildInitialState();
        var t = this.opt("eventSources") || [],
            n = this.opt("events"),
            r = [];
        n && t.unshift(n);
        for (var i = 0, o = t; i < o.length; i++) {
            var a = cr(o[i], this);
            a && r.push(a)
        }
        this.batchRendering(function () {
            e.dispatch({
                type: "INIT"
            }), e.dispatch({
                type: "ADD_EVENT_SOURCES",
                sources: r
            }), e.dispatch({
                type: "SET_VIEW_TYPE",
                viewType: e.opt("defaultView") || e.pluginSystem.hooks.defaultView
            })
        })
    }, Jr.prototype.buildInitialState = function () {
        return {
            viewType: null,
            loadingLevel: 0,
            eventSourceLoadingLevel: 0,
            currentDate: this.getInitialDate(),
            dateProfile: null,
            eventSources: {},
            eventStore: {
                defs: {},
                instances: {}
            },
            dateSelection: null,
            eventSelection: "",
            eventDrag: null,
            eventResize: null
        }
    }, Jr.prototype.dispatch = function (e) {
        if (this.actionQueue.push(e), !this.isReducing) {
            this.isReducing = !0;
            for (var t = this.state; this.actionQueue.length;) this.state = this.reduce(this.state, this.actionQueue.shift(), this);
            var n = this.state;
            this.isReducing = !1, !t.loadingLevel && n.loadingLevel ? this.publiclyTrigger("loading", [!0]) : t.loadingLevel && !n.loadingLevel && this.publiclyTrigger("loading", [!1]);
            var r = this.component && this.component.view;
            (t.eventStore !== n.eventStore || this.needsFullRerender) && t.eventStore && (this.isEventsUpdated = !0), t.dateProfile === n.dateProfile && !this.needsFullRerender || (t.dateProfile && r && this.publiclyTrigger("datesDestroy", [{
                view: r,
                el: r.el
            }]), this.isDatesUpdated = !0), t.viewType === n.viewType && !this.needsFullRerender || (t.viewType && r && this.publiclyTrigger("viewSkeletonDestroy", [{
                view: r,
                el: r.el
            }]), this.isViewUpdated = !0), this.requestRerender()
        }
    }, Jr.prototype.reduce = function (e, t, n) {
        return br(e, t, n)
    }, Jr.prototype.requestRerender = function (e) {
        void 0 === e && (e = !1), this.needsRerender = !0, this.needsFullRerender = this.needsFullRerender || e, this.delayedRerender()
    }, Jr.prototype.tryRerender = function () {
        this.component && this.needsRerender && !this.renderingPauseDepth && !this.isRendering && this.executeRender()
    }, Jr.prototype.batchRendering = function (e) {
        this.renderingPauseDepth++, e(), this.renderingPauseDepth--, this.needsRerender && this.requestRerender()
    }, Jr.prototype.executeRender = function () {
        var e = this.needsFullRerender;
        this.needsRerender = !1, this.needsFullRerender = !1, this.isRendering = !0, this.renderComponent(e), this.isRendering = !1, this.needsRerender && this.delayedRerender()
    }, Jr.prototype.renderComponent = function (e) {
        var t = this.state,
            n = this.component,
            r = t.viewType,
            i = this.viewSpecs[r],
            o = e && n ? n.view.queryScroll() : null;
        if (!i) throw new Error('View type "' + r + '" is not valid');
        var a = this.renderableEventStore = t.eventSourceLoadingLevel && !this.opt("progressiveEventRendering") ? this.renderableEventStore : t.eventStore,
            s = this.buildEventUiSingleBase(i.options),
            l = this.buildEventUiBySource(t.eventSources),
            u = this.eventUiBases = this.buildEventUiBases(a.defs, s, l);
        !e && n || (n && (n.freezeHeight(), n.destroy()), n = this.component = new Or({
            calendar: this,
            view: null,
            dateEnv: this.dateEnv,
            theme: this.theme,
            options: this.optionsManager.computed
        }, this.el), this.isViewUpdated = !0, this.isDatesUpdated = !0, this.isEventsUpdated = !0), n.receiveProps(be({}, t, {
            viewSpec: i,
            dateProfile: t.dateProfile,
            dateProfileGenerator: this.dateProfileGenerators[r],
            eventStore: a,
            eventUiBases: u,
            dateSelection: t.dateSelection,
            eventSelection: t.eventSelection,
            eventDrag: t.eventDrag,
            eventResize: t.eventResize
        })), o && n.view.applyScroll(o, !1), this.isViewUpdated && (this.isViewUpdated = !1, this.publiclyTrigger("viewSkeletonRender", [{
            view: n.view,
            el: n.view.el
        }])), this.isDatesUpdated && (this.isDatesUpdated = !1, this.publiclyTrigger("datesRender", [{
            view: n.view,
            el: n.view.el
        }])), this.isEventsUpdated && (this.isEventsUpdated = !1), this.releaseAfterSizingTriggers()
    }, Jr.prototype.setOption = function (e, t) {
        var n;
        this.mutateOptions(((n = {})[e] = t, n), [], !0)
    }, Jr.prototype.getOption = function (e) {
        return this.optionsManager.computed[e]
    }, Jr.prototype.opt = function (e) {
        return this.optionsManager.computed[e]
    }, Jr.prototype.viewOpt = function (e) {
        return this.viewOpts()[e]
    }, Jr.prototype.viewOpts = function () {
        return this.viewSpecs[this.state.viewType].options
    }, Jr.prototype.mutateOptions = function (e, t, n, r) {
        var i = this,
            o = this.pluginSystem.hooks.optionChangeHandlers,
            a = {},
            s = {},
            l = this.dateEnv,
            u = !1,
            c = !1,
            d = Boolean(t.length);
        for (var f in e) o[f] ? s[f] = e[f] : a[f] = e[f];
        for (var p in a) /^(height|contentHeight|aspectRatio)$/.test(p) ? c = !0 : /^(defaultDate|defaultView)$/.test(p) || (d = !0, "timeZone" === p && (u = !0));
        this.optionsManager.mutate(a, t, n), d && (this.handleOptions(this.optionsManager.computed), this.needsFullRerender = !0), this.batchRendering(function () {
            if (d ? (u && i.dispatch({
                type: "CHANGE_TIMEZONE",
                oldDateEnv: l
            }), i.dispatch({
                type: "SET_VIEW_TYPE",
                viewType: i.state.viewType
            })) : c && i.updateSize(), r)
                for (var e in s) o[e](s[e], i, r)
        })
    }, Jr.prototype.handleOptions = function (e) {
        var t = this,
            n = this.pluginSystem.hooks;
        this.defaultAllDayEventDuration = X(e.defaultAllDayEventDuration), this.defaultTimedEventDuration = X(e.defaultTimedEventDuration), this.delayedRerender = this.buildDelayedRerender(e.rerenderDelay), this.theme = this.buildTheme(e);
        var r = this.parseRawLocales(e.locales);
        this.availableRawLocales = r.map;
        var i = this.buildLocale(e.locale || r.defaultCode, r.map);
        this.dateEnv = this.buildDateEnv(i, e.timeZone, n.namedTimeZonedImpl, e.firstDay, e.weekNumberCalculation, e.weekLabel, n.cmdFormatter), this.selectionConfig = this.buildSelectionConfig(e), this.viewSpecs = Ir(n.views, this.optionsManager), this.dateProfileGenerators = Te(this.viewSpecs, function (e) {
            return new e.class.prototype.dateProfileGeneratorClass(e, t)
        })
    }, Jr.prototype.getAvailableLocaleCodes = function () {
        return Object.keys(this.availableRawLocales)
    }, Jr.prototype._buildSelectionConfig = function (e) {
        return Nt("select", e, this)
    }, Jr.prototype._buildEventUiSingleBase = function (e) {
        return e.editable && (e = be({}, e, {
            eventEditable: !0
        })), Nt("event", e, this)
    }, Jr.prototype.hasPublicHandlers = function (e) {
        return this.hasHandlers(e) || this.opt(e)
    }, Jr.prototype.publiclyTrigger = function (e, t) {
        var n = this.opt(e);
        if (this.triggerWith(e, this, t), n) return n.apply(this, t)
    }, Jr.prototype.publiclyTriggerAfterSizing = function (e, t) {
        var n = this.afterSizingTriggers;
        (n[e] || (n[e] = [])).push(t)
    }, Jr.prototype.releaseAfterSizingTriggers = function () {
        var e = this.afterSizingTriggers;
        for (var t in e)
            for (var n = 0, r = e[t]; n < r.length; n++) {
                var i = r[n];
                this.publiclyTrigger(t, i)
            }
        this.afterSizingTriggers = {}
    }, Jr.prototype.isValidViewType = function (e) {
        return Boolean(this.viewSpecs[e])
    }, Jr.prototype.changeView = function (e, t) {
        var n = null;
        t && (t.start && t.end ? (this.optionsManager.mutate({
            visibleRange: t
        }, []), this.handleOptions(this.optionsManager.computed)) : n = this.dateEnv.createMarker(t)), this.unselect(), this.dispatch({
            type: "SET_VIEW_TYPE",
            viewType: e,
            dateMarker: n
        })
    }, Jr.prototype.zoomTo = function (e, t) {
        var n;
        t = t || "day", n = this.viewSpecs[t] || this.getUnitViewSpec(t), this.unselect(), n ? this.dispatch({
            type: "SET_VIEW_TYPE",
            viewType: n.type,
            dateMarker: e
        }) : this.dispatch({
            type: "SET_DATE",
            dateMarker: e
        })
    }, Jr.prototype.getUnitViewSpec = function (e) {
        var t, n, r = this.component,
            i = [];
        for (var o in r.header && i.push.apply(i, r.header.viewsWithButtons), r.footer && i.push.apply(i, r.footer.viewsWithButtons), this.viewSpecs) i.push(o);
        for (t = 0; t < i.length; t++)
            if ((n = this.viewSpecs[i[t]]) && n.singleUnit === e) return n
    }, Jr.prototype.getInitialDate = function () {
        var e = this.opt("defaultDate");
        return null != e ? this.dateEnv.createMarker(e) : this.getNow()
    }, Jr.prototype.prev = function () {
        this.unselect(), this.dispatch({
            type: "PREV"
        })
    }, Jr.prototype.next = function () {
        this.unselect(), this.dispatch({
            type: "NEXT"
        })
    }, Jr.prototype.prevYear = function () {
        this.unselect(), this.dispatch({
            type: "SET_DATE",
            dateMarker: this.dateEnv.addYears(this.state.currentDate, -1)
        })
    }, Jr.prototype.nextYear = function () {
        this.unselect(), this.dispatch({
            type: "SET_DATE",
            dateMarker: this.dateEnv.addYears(this.state.currentDate, 1)
        })
    }, Jr.prototype.today = function () {
        this.unselect(), this.dispatch({
            type: "SET_DATE",
            dateMarker: this.getNow()
        })
    }, Jr.prototype.gotoDate = function (e) {
        this.unselect(), this.dispatch({
            type: "SET_DATE",
            dateMarker: this.dateEnv.createMarker(e)
        })
    }, Jr.prototype.incrementDate = function (e) {
        var t = X(e);
        t && (this.unselect(), this.dispatch({
            type: "SET_DATE",
            dateMarker: this.dateEnv.add(this.state.currentDate, t)
        }))
    }, Jr.prototype.getDate = function () {
        return this.dateEnv.toDate(this.state.currentDate)
    }, Jr.prototype.formatDate = function (e, t) {
        var n = this.dateEnv;
        return n.format(n.createMarker(e), ot(t))
    }, Jr.prototype.formatRange = function (e, t, n) {
        var r = this.dateEnv;
        return r.formatRange(r.createMarker(e), r.createMarker(t), ot(n, this.opt("defaultRangeSeparator")), n)
    }, Jr.prototype.formatIso = function (e, t) {
        var n = this.dateEnv;
        return n.formatIso(n.createMarker(e), {
            omitTime: t
        })
    }, Jr.prototype.windowResize = function (e) {
        !this.isHandlingWindowResize && this.component && e.target === window && (this.isHandlingWindowResize = !0, this.updateSize(), this.publiclyTrigger("windowResize", [this.view]), this.isHandlingWindowResize = !1)
    }, Jr.prototype.updateSize = function () {
        this.component && this.component.updateSize(!0)
    }, Jr.prototype.registerInteractiveComponent = function (e, t) {
        var n = function (e, t) {
            return {
                component: e,
                el: t.el,
                useEventCenter: null == t.useEventCenter || t.useEventCenter
            }
        }(e, t),
            r = [jr, Vr].concat(this.pluginSystem.hooks.componentInteractions).map(function (e) {
                return new e(n)
            });
        this.interactionsStore[e.uid] = r, Br[e.uid] = n
    }, Jr.prototype.unregisterInteractiveComponent = function (e) {
        for (var t = 0, n = this.interactionsStore[e.uid]; t < n.length; t++) n[t].destroy();
        delete this.interactionsStore[e.uid], delete Br[e.uid]
    }, Jr.prototype.select = function (e, t) {
        var n = Dr(null == t ? null != e.start ? e : {
            start: e,
            end: null
        } : {
                start: e,
                end: t
            }, this.dateEnv, X({
                days: 1
            }));
        n && (this.dispatch({
            type: "SELECT_DATES",
            selection: n
        }), this.triggerDateSelect(n))
    }, Jr.prototype.unselect = function (e) {
        this.state.dateSelection && (this.dispatch({
            type: "UNSELECT_DATES"
        }), this.triggerDateUnselect(e))
    }, Jr.prototype.triggerDateSelect = function (e, t) {
        var n = be({}, this.buildDateSpanApi(e), {
            jsEvent: t ? t.origEvent : null,
            view: this.view
        });
        this.publiclyTrigger("select", [n])
    }, Jr.prototype.triggerDateUnselect = function (e) {
        this.publiclyTrigger("unselect", [{
            jsEvent: e ? e.origEvent : null,
            view: this.view
        }])
    }, Jr.prototype.triggerDateClick = function (e, t, n, r) {
        var i = be({}, this.buildDatePointApi(e), {
            dayEl: t,
            jsEvent: r,
            view: n
        });
        this.publiclyTrigger("dateClick", [i])
    }, Jr.prototype.buildDatePointApi = function (e) {
        for (var t = {}, n = 0, r = this.pluginSystem.hooks.datePointTransforms; n < r.length; n++) {
            var i = r[n];
            be(t, i(e, this))
        }
        return be(t, function (e, t) {
            return {
                date: t.toDate(e.range.start),
                dateStr: t.formatIso(e.range.start, {
                    omitTime: e.allDay
                }),
                allDay: e.allDay
            }
        }(e, this.dateEnv)), t
    }, Jr.prototype.buildDateSpanApi = function (e) {
        for (var t = {}, n = 0, r = this.pluginSystem.hooks.dateSpanTransforms; n < r.length; n++) {
            var i = r[n];
            be(t, i(e, this))
        }
        return be(t, function (e, t) {
            return {
                start: t.toDate(e.range.start),
                end: t.toDate(e.range.end),
                startStr: t.formatIso(e.range.start, {
                    omitTime: e.allDay
                }),
                endStr: t.formatIso(e.range.end, {
                    omitTime: e.allDay
                }),
                allDay: e.allDay
            }
        }(e, this.dateEnv)), t
    }, Jr.prototype.getNow = function () {
        var e = this.opt("now");
        return "function" == typeof e && (e = e()), null == e ? this.dateEnv.createNowMarker() : this.dateEnv.createMarker(e)
    }, Jr.prototype.getDefaultEventEnd = function (e, t) {
        var n = t;
        return n = e ? (n = z(n), this.dateEnv.add(n, this.defaultAllDayEventDuration)) : this.dateEnv.add(n, this.defaultTimedEventDuration)
    }, Jr.prototype.addEvent = function (e, t) {
        if (e instanceof dt) {
            var n = e._def,
                r = e._instance;
            return this.state.eventStore.defs[n.defId] || this.dispatch({
                type: "ADD_EVENTS",
                eventStore: Ie({
                    def: n,
                    instance: r
                })
            }), e
        }
        var i;
        if (t instanceof ut) i = t.internalEventSource.sourceId;
        else if (null != t) {
            var o = this.getEventSourceById(t);
            if (!o) return console.warn('Could not find an event source with ID "' + t + '"'), null;
            i = o.internalEventSource.sourceId
        }
        var a = Wt(e, i, this);
        return a ? (this.dispatch({
            type: "ADD_EVENTS",
            eventStore: Ie(a)
        }), new dt(this, a.def, a.def.recurringDef ? null : a.instance)) : null
    }, Jr.prototype.getEventById = function (e) {
        var t = this.state.eventStore,
            n = t.defs,
            r = t.instances;
        for (var i in e = String(e), n) {
            var o = n[i];
            if (o.publicId === e) {
                if (o.recurringDef) return new dt(this, o, null);
                for (var a in r) {
                    var s = r[a];
                    if (s.defId === o.defId) return new dt(this, o, s)
                }
            }
        }
        return null
    }, Jr.prototype.getEvents = function () {
        var e = this.state.eventStore,
            t = e.defs,
            n = e.instances,
            r = [];
        for (var i in n) {
            var o = n[i],
                a = t[o.defId];
            r.push(new dt(this, a, o))
        }
        return r
    }, Jr.prototype.removeAllEvents = function () {
        this.dispatch({
            type: "REMOVE_ALL_EVENTS"
        })
    }, Jr.prototype.rerenderEvents = function () {
        this.dispatch({
            type: "RESET_EVENTS"
        })
    }, Jr.prototype.getEventSources = function () {
        var e = this.state.eventSources,
            t = [];
        for (var n in e) t.push(new ut(this, e[n]));
        return t
    }, Jr.prototype.getEventSourceById = function (e) {
        var t = this.state.eventSources;
        for (var n in e = String(e), t)
            if (t[n].publicId === e) return new ut(this, t[n]);
        return null
    }, Jr.prototype.addEventSource = function (e) {
        if (e instanceof ut) return this.state.eventSources[e.internalEventSource.sourceId] || this.dispatch({
            type: "ADD_EVENT_SOURCES",
            sources: [e.internalEventSource]
        }), e;
        var t = cr(e, this);
        return t ? (this.dispatch({
            type: "ADD_EVENT_SOURCES",
            sources: [t]
        }), new ut(this, t)) : null
    }, Jr.prototype.removeAllEventSources = function () {
        this.dispatch({
            type: "REMOVE_ALL_EVENT_SOURCES"
        })
    }, Jr.prototype.refetchEvents = function () {
        this.dispatch({
            type: "FETCH_EVENT_SOURCES"
        })
    }, Jr.prototype.scrollToTime = function (e) {
        var t = X(e);
        t && this.component.view.scrollToDuration(t)
    }, Jr);

    function Jr(e, t) {
        var n = this;
        this.parseRawLocales = We(Yn), this.buildLocale = We(Xn), this.buildDateEnv = We($r), this.buildTheme = We(Qr), this.buildEventUiSingleBase = We(this._buildEventUiSingleBase), this.buildSelectionConfig = We(this._buildSelectionConfig), this.buildEventUiBySource = Ve(ei, _e), this.buildEventUiBases = We(ti), this.interactionsStore = {}, this.actionQueue = [], this.isReducing = !1, this.needsRerender = !1, this.needsFullRerender = !1, this.isRendering = !1, this.renderingPauseDepth = 0, this.buildDelayedRerender = We(Kr), this.afterSizingTriggers = {}, this.isViewUpdated = !1, this.isDatesUpdated = !1, this.isEventsUpdated = !1, this.el = e, this.optionsManager = new $n(t || {}), this.pluginSystem = new On, this.addPluginInputs(this.optionsManager.computed.plugins || []), this.handleOptions(this.optionsManager.computed), this.publiclyTrigger("_init"), this.hydrate(), this.calendarInteractions = this.pluginSystem.hooks.calendarInteractions.map(function (e) {
            return new e(n)
        })
    }

    function $r(e, t, n, r, i, o, a) {
        return new or({
            calendarSystem: "gregory",
            timeZone: t,
            namedTimeZoneImpl: n,
            locale: e,
            weekNumberCalculation: i,
            firstDay: r,
            weekLabel: o,
            cmdFormatter: a
        })
    }

    function Qr(e) {
        return new (this.pluginSystem.hooks.themeClasses[e.themeSystem] || qr)(e)
    }

    function Kr(e) {
        var t = this.tryRerender.bind(this);
        return null != e && (t = fe(t, e)), t
    }

    function ei(e) {
        return Te(e, function (e) {
            return e.ui
        })
    }

    function ti(e, t, n) {
        var r = {
            "": t
        };
        for (var i in e) {
            var o = e[i];
            o.sourceId && n[o.sourceId] && (r[i] = n[o.sourceId])
        }
        return r
    }
    an.mixInto(Xr);
    var ni, ri = (ye(ii, ni = Rn), ii.prototype.initialize = function () { }, Object.defineProperty(ii.prototype, "activeStart", {
        get: function () {
            return this.dateEnv.toDate(this.props.dateProfile.activeRange.start)
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ii.prototype, "activeEnd", {
        get: function () {
            return this.dateEnv.toDate(this.props.dateProfile.activeRange.end)
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ii.prototype, "currentStart", {
        get: function () {
            return this.dateEnv.toDate(this.props.dateProfile.currentRange.start)
        },
        enumerable: !0,
        configurable: !0
    }), Object.defineProperty(ii.prototype, "currentEnd", {
        get: function () {
            return this.dateEnv.toDate(this.props.dateProfile.currentRange.end)
        },
        enumerable: !0,
        configurable: !0
    }), ii.prototype.render = function (e) {
        this.renderDatesMem(e.dateProfile), this.renderBusinessHoursMem(e.businessHours), this.renderDateSelectionMem(e.dateSelection), this.renderEventsMem(e.eventStore), this.renderEventSelectionMem(e.eventSelection), this.renderEventDragMem(e.eventDrag), this.renderEventResizeMem(e.eventResize)
    }, ii.prototype.destroy = function () {
        ni.prototype.destroy.call(this), this.renderDatesMem.unrender()
    }, ii.prototype.updateSize = function (e, t, n) {
        var r = this.calendar;
        (e || r.isViewUpdated || r.isDatesUpdated || r.isEventsUpdated) && this.updateBaseSize(e, t, n)
    }, ii.prototype.updateBaseSize = function (e, t, n) { }, ii.prototype.renderDatesWrap = function (e) {
        this.renderDates(e), this.addScroll({
            duration: X(this.opt("scrollTime"))
        }), this.startNowIndicator(e)
    }, ii.prototype.unrenderDatesWrap = function () {
        this.stopNowIndicator(), this.unrenderDates()
    }, ii.prototype.renderDates = function (e) { }, ii.prototype.unrenderDates = function () { }, ii.prototype.renderBusinessHours = function (e) { }, ii.prototype.unrenderBusinessHours = function () { }, ii.prototype.renderDateSelectionWrap = function (e) {
        e && this.renderDateSelection(e)
    }, ii.prototype.unrenderDateSelectionWrap = function (e) {
        e && this.unrenderDateSelection(e)
    }, ii.prototype.renderDateSelection = function (e) { }, ii.prototype.unrenderDateSelection = function (e) { }, ii.prototype.renderEvents = function (e) { }, ii.prototype.unrenderEvents = function () { }, ii.prototype.sliceEvents = function (e, t) {
        var n = this.props;
        return pt(e, n.eventUiBases, n.dateProfile.activeRange, t ? this.nextDayThreshold : null).fg
    }, ii.prototype.computeEventDraggable = function (e, t) {
        for (var n = this.calendar.pluginSystem.hooks.isDraggableTransformers, r = t.startEditable, i = 0, o = n; i < o.length; i++) r = (0, o[i])(r, e, t, this);
        return r
    }, ii.prototype.computeEventStartResizable = function (e, t) {
        return t.durationEditable && this.opt("eventResizableFromStart")
    }, ii.prototype.computeEventEndResizable = function (e, t) {
        return t.durationEditable
    }, ii.prototype.renderEventSelectionWrap = function (e) {
        e && this.renderEventSelection(e)
    }, ii.prototype.unrenderEventSelectionWrap = function (e) {
        e && this.unrenderEventSelection(e)
    }, ii.prototype.renderEventSelection = function (e) { }, ii.prototype.unrenderEventSelection = function (e) { }, ii.prototype.renderEventDragWrap = function (e) {
        e && this.renderEventDrag(e)
    }, ii.prototype.unrenderEventDragWrap = function (e) {
        e && this.unrenderEventDrag(e)
    }, ii.prototype.renderEventDrag = function (e) { }, ii.prototype.unrenderEventDrag = function (e) { }, ii.prototype.renderEventResizeWrap = function (e) {
        e && this.renderEventResize(e)
    }, ii.prototype.unrenderEventResizeWrap = function (e) {
        e && this.unrenderEventResize(e)
    }, ii.prototype.renderEventResize = function (e) { }, ii.prototype.unrenderEventResize = function (e) { }, ii.prototype.startNowIndicator = function (e) {
        var t, n, r, i = this,
            o = this.dateEnv;
        this.opt("nowIndicator") && (t = this.getNowIndicatorUnit(e)) && (n = this.updateNowIndicator.bind(this), this.initialNowDate = this.calendar.getNow(), this.initialNowQueriedMs = (new Date).valueOf(), r = o.add(o.startOf(this.initialNowDate, t), X(1, t)).valueOf() - this.initialNowDate.valueOf(), this.nowIndicatorTimeoutID = setTimeout(function () {
            i.nowIndicatorTimeoutID = null, n(), r = "second" === t ? 1e3 : 6e4, i.nowIndicatorIntervalID = setInterval(n, r)
        }, r))
    }, ii.prototype.updateNowIndicator = function () {
        this.props.dateProfile && this.initialNowDate && (this.unrenderNowIndicator(), this.renderNowIndicator(H(this.initialNowDate, (new Date).valueOf() - this.initialNowQueriedMs)), this.isNowIndicatorRendered = !0)
    }, ii.prototype.stopNowIndicator = function () {
        this.isNowIndicatorRendered && (this.nowIndicatorTimeoutID && (clearTimeout(this.nowIndicatorTimeoutID), this.nowIndicatorTimeoutID = null), this.nowIndicatorIntervalID && (clearInterval(this.nowIndicatorIntervalID), this.nowIndicatorIntervalID = null), this.unrenderNowIndicator(), this.isNowIndicatorRendered = !1)
    }, ii.prototype.getNowIndicatorUnit = function (e) { }, ii.prototype.renderNowIndicator = function (e) { }, ii.prototype.unrenderNowIndicator = function () { }, ii.prototype.addScroll = function (e) {
        var t = this.queuedScroll || (this.queuedScroll = {});
        be(t, e)
    }, ii.prototype.popScroll = function (e) {
        this.applyQueuedScroll(e), this.queuedScroll = null
    }, ii.prototype.applyQueuedScroll = function (e) {
        this.applyScroll(this.queuedScroll || {}, e)
    }, ii.prototype.queryScroll = function () {
        var e = {};
        return this.props.dateProfile && be(e, this.queryDateScroll()), e
    }, ii.prototype.applyScroll = function (e, t) {
        var n = e.duration;
        null != n && (delete e.duration, this.props.dateProfile && be(e, this.computeDateScroll(n))), this.props.dateProfile && this.applyDateScroll(e)
    }, ii.prototype.computeDateScroll = function (e) {
        return {}
    }, ii.prototype.queryDateScroll = function () {
        return {}
    }, ii.prototype.applyDateScroll = function (e) { }, ii.prototype.scrollToDuration = function (e) {
        this.applyScroll({
            duration: e
        }, !1)
    }, ii);

    function ii(e, t, n, r) {
        var i = ni.call(this, e, a("div", {
            className: "fc-view fc-" + t.type + "-view"
        }), !0) || this;
        return i.renderDatesMem = Yt(i.renderDatesWrap, i.unrenderDatesWrap), i.renderBusinessHoursMem = Yt(i.renderBusinessHours, i.unrenderBusinessHours, [i.renderDatesMem]), i.renderDateSelectionMem = Yt(i.renderDateSelectionWrap, i.unrenderDateSelectionWrap, [i.renderDatesMem]), i.renderEventsMem = Yt(i.renderEvents, i.unrenderEvents, [i.renderDatesMem]), i.renderEventSelectionMem = Yt(i.renderEventSelectionWrap, i.unrenderEventSelectionWrap, [i.renderEventsMem]), i.renderEventDragMem = Yt(i.renderEventDragWrap, i.unrenderEventDragWrap, [i.renderDatesMem]), i.renderEventResizeMem = Yt(i.renderEventResizeWrap, i.unrenderEventResizeWrap, [i.renderDatesMem]), i.viewSpec = t, i.dateProfileGenerator = n, i.type = t.type, i.eventOrderSpecs = re(i.opt("eventOrder")), i.nextDayThreshold = X(i.opt("nextDayThreshold")), r.appendChild(i.el), i.initialize(), i
    }
    an.mixInto(ri), ri.prototype.usesMinMaxTime = !1, ri.prototype.dateProfileGeneratorClass = mr;
    var oi = (ai.prototype.renderSegs = function (e, t) {
        this.rangeUpdated(), e = this.renderSegEls(e, t), this.segs = e, this.attachSegs(e, t), this.isSizeDirty = !0, this.context.view.triggerRenderedSegs(this.segs, Boolean(t))
    }, ai.prototype.unrender = function (e, t) {
        this.context.view.triggerWillRemoveSegs(this.segs, Boolean(t)), this.detachSegs(this.segs), this.segs = []
    }, ai.prototype.rangeUpdated = function () {
        var e, t, n = this.context.options;
        this.eventTimeFormat = ot(n.eventTimeFormat || this.computeEventTimeFormat(), n.defaultRangeSeparator), null == (e = n.displayEventTime) && (e = this.computeDisplayEventTime()), null == (t = n.displayEventEnd) && (t = this.computeDisplayEventEnd()), this.displayEventTime = e, this.displayEventEnd = t
    }, ai.prototype.renderSegEls = function (r, e) {
        var t, n = "";
        if (r.length) {
            for (t = 0; t < r.length; t++) n += this.renderSegHtml(r[t], e);
            s(n).forEach(function (e, t) {
                var n = r[t];
                e && (n.el = e)
            }), r = ht(this.context.view, r, Boolean(e))
        }
        return r
    }, ai.prototype.getSegClasses = function (e, t, n, r) {
        var i = ["fc-event", e.isStart ? "fc-start" : "fc-not-start", e.isEnd ? "fc-end" : "fc-not-end"].concat(e.eventRange.ui.classNames);
        return t && i.push("fc-draggable"), n && i.push("fc-resizable"), r && (i.push("fc-mirror"), r.isDragging && i.push("fc-dragging"), r.isResizing && i.push("fc-resizing")), i
    }, ai.prototype.getTimeText = function (e, t, n) {
        var r = e.def,
            i = e.instance;
        return this._getTimeText(i.range.start, r.hasEnd ? i.range.end : null, r.allDay, t, n, i.forcedStartTzo, i.forcedEndTzo)
    }, ai.prototype._getTimeText = function (e, t, n, r, i, o, a) {
        var s = this.context.dateEnv;
        return null == r && (r = this.eventTimeFormat), null == i && (i = this.displayEventEnd), this.displayEventTime && !n ? i && t ? s.formatRange(e, t, r, {
            forcedStartTzo: o,
            forcedEndTzo: a
        }) : s.format(e, r, {
            forcedTzo: o
        }) : ""
    }, ai.prototype.computeEventTimeFormat = function () {
        return {
            hour: "numeric",
            minute: "2-digit",
            omitZeroMinute: !0
        }
    }, ai.prototype.computeDisplayEventTime = function () {
        return !0
    }, ai.prototype.computeDisplayEventEnd = function () {
        return !0
    }, ai.prototype.getSkinCss = function (e) {
        return {
            "background-color": e.backgroundColor,
            "border-color": e.borderColor,
            color: e.textColor
        }
    }, ai.prototype.sortEventSegs = function (e) {
        var n = this.context.view.eventOrderSpecs,
            t = e.map(si);
        return t.sort(function (e, t) {
            return ie(e, t, n)
        }), t.map(function (e) {
            return e._seg
        })
    }, ai.prototype.computeSizes = function (e) {
        (e || this.isSizeDirty) && this.computeSegSizes(this.segs)
    }, ai.prototype.assignSizes = function (e) {
        (e || this.isSizeDirty) && (this.assignSegSizes(this.segs), this.isSizeDirty = !1)
    }, ai.prototype.computeSegSizes = function (e) { }, ai.prototype.assignSegSizes = function (e) { }, ai.prototype.hideByHash = function (e) {
        if (e)
            for (var t = 0, n = this.segs; t < n.length; t++) {
                var r = n[t];
                e[r.eventRange.instance.instanceId] && (r.el.style.visibility = "hidden")
            }
    }, ai.prototype.showByHash = function (e) {
        if (e)
            for (var t = 0, n = this.segs; t < n.length; t++) {
                var r = n[t];
                e[r.eventRange.instance.instanceId] && (r.el.style.visibility = "")
            }
    }, ai.prototype.selectByInstanceId = function (e) {
        if (e)
            for (var t = 0, n = this.segs; t < n.length; t++) {
                var r = n[t],
                    i = r.eventRange.instance;
                i && i.instanceId === e && r.el && r.el.classList.add("fc-selected")
            }
    }, ai.prototype.unselectByInstanceId = function (e) {
        if (e)
            for (var t = 0, n = this.segs; t < n.length; t++) {
                var r = n[t];
                r.el && r.el.classList.remove("fc-selected")
            }
    }, ai);

    function ai(e) {
        this.segs = [], this.isSizeDirty = !1, this.context = e
    }

    function si(e) {
        var t = e.eventRange.def,
            n = e.eventRange.instance.range,
            r = n.start ? n.start.valueOf() : 0,
            i = n.end ? n.end.valueOf() : 0;
        return be({}, t.extendedProps, t, {
            id: t.publicId,
            start: r,
            end: i,
            duration: i - r,
            allDay: Number(t.allDay),
            _seg: e
        })
    }
    var li = (ui.prototype.getSegsByType = function (e) {
        return this.segsByType[e] || []
    }, ui.prototype.renderSegs = function (e, t) {
        var n, r = this.renderSegEls(e, t),
            i = this.attachSegs(e, r);
        i && (n = this.containerElsByType[e] || (this.containerElsByType[e] = [])).push.apply(n, i), this.segsByType[e] = r, "bgEvent" === e && this.context.view.triggerRenderedSegs(r, !1), this.dirtySizeFlags[e] = !0
    }, ui.prototype.unrender = function (e) {
        var t = this.segsByType[e];
        t && ("bgEvent" === e && this.context.view.triggerWillRemoveSegs(t, !1), this.detachSegs(e, t))
    }, ui.prototype.renderSegEls = function (e, r) {
        var t, n = this,
            i = "";
        if (r.length) {
            for (t = 0; t < r.length; t++) i += this.renderSegHtml(e, r[t]);
            s(i).forEach(function (e, t) {
                var n = r[t];
                e && (n.el = e)
            }), "bgEvent" === e && (r = ht(this.context.view, r, !1)), r = r.filter(function (e) {
                return d(e.el, n.fillSegTag)
            })
        }
        return r
    }, ui.prototype.renderSegHtml = function (e, t) {
        var n = null,
            r = [];
        return "highlight" !== e && "businessHours" !== e && (n = {
            "background-color": t.eventRange.ui.backgroundColor
        }), "highlight" !== e && (r = r.concat(t.eventRange.ui.classNames)), "businessHours" === e ? r.push("fc-bgevent") : r.push("fc-" + e.toLowerCase()), "<" + this.fillSegTag + (r.length ? ' class="' + r.join(" ") + '"' : "") + (n ? ' style="' + Mt(n) + '"' : "") + "></" + this.fillSegTag + ">"
    }, ui.prototype.detachSegs = function (e, t) {
        var n = this.containerElsByType[e];
        n && (n.forEach(r), delete this.containerElsByType[e])
    }, ui.prototype.computeSizes = function (e) {
        for (var t in this.segsByType) (e || this.dirtySizeFlags[t]) && this.computeSegSizes(this.segsByType[t])
    }, ui.prototype.assignSizes = function (e) {
        for (var t in this.segsByType) (e || this.dirtySizeFlags[t]) && this.assignSegSizes(this.segsByType[t]);
        this.dirtySizeFlags = {}
    }, ui.prototype.computeSegSizes = function (e) { }, ui.prototype.assignSegSizes = function (e) { }, ui);

    function ui(e) {
        this.fillSegTag = "div", this.dirtySizeFlags = {}, this.context = e, this.containerElsByType = {}, this.segsByType = {}
    }

    function ci(e) {
        this.timeZoneName = e
    }
    var di = (fi.prototype.destroy = function () { }, fi.prototype.setMirrorIsVisible = function (e) { }, fi.prototype.setMirrorNeedsRevert = function (e) { }, fi.prototype.setAutoScrollEnabled = function (e) { }, fi);

    function fi(e) {
        this.emitter = new an
    }

    function pi(e) {
        var t = Xn(e.locale || "en", Yn([]).map);
        return e = be({
            timeZone: Wn.timeZone,
            calendarSystem: "gregory"
        }, e, {
            locale: t
        }), new or(e)
    }
    var hi = {
        startTime: X,
        duration: X,
        create: Boolean,
        sourceId: String
    },
        gi = {
            create: !0
        };

    function vi(e, t) {
        return !e || 10 < t ? {
            weekday: "short"
        } : 1 < t ? {
            weekday: "short",
            month: "numeric",
            day: "numeric",
            omitCommas: !0
        } : {
                    weekday: "long"
                }
    }

    function mi(e, t, n, r, i, o, a, s) {
        var l, u = o.view,
            c = o.dateEnv,
            d = o.theme,
            f = o.options,
            p = je(t.activeRange, e),
            h = ["fc-day-header", d.getClass("widgetHeader")];
        return l = "function" == typeof f.columnHeaderHtml ? f.columnHeaderHtml(c.toDate(e)) : "function" == typeof f.columnHeaderText ? Pt(f.columnHeaderText(c.toDate(e))) : Pt(c.format(e, i)), n ? h = h.concat(en(e, t, o, !0)) : h.push("fc-" + M[e.getUTCDay()]), '<th class="' + h.join(" ") + '"' + (p && n ? ' data-date="' + c.formatIso(e, {
            omitTime: !0
        }) + '"' : "") + (1 < a ? ' colspan="' + a + '"' : "") + (s ? " " + s : "") + ">" + (p ? Kt(u, {
            date: e,
            forceOff: !n || 1 === r
        }, l) : l) + "</th>"
    }
    var yi, bi = (ye(Si, yi = _n), Si.prototype.destroy = function () {
        r(this.el)
    }, Si.prototype.render = function (e) {
        var t = e.dates,
            n = e.datesRepDistinctDays,
            r = [];
        e.renderIntroHtml && r.push(e.renderIntroHtml());
        for (var i = ot(this.opt("columnHeaderFormat") || vi(n, t.length)), o = 0, a = t; o < a.length; o++) {
            var s = a[o];
            r.push(mi(s, e.dateProfile, n, t.length, i, this.context))
        }
        this.isRtl && r.reverse(), this.thead.innerHTML = "<tr>" + r.join("") + "</tr>"
    }, Si);

    function Si(e, t) {
        var n = yi.call(this, e) || this;
        return t.innerHTML = "", t.appendChild(n.el = S('<div class="fc-row ' + n.theme.getClass("headerRow") + '"><table class="' + n.theme.getClass("tableGrid") + '"><thead></thead></table></div>')), n.thead = n.el.querySelector("thead"), n
    }
    var wi = (Di.prototype.sliceRange = function (e) {
        var t = this.getDateDayIndex(e.start),
            n = this.getDateDayIndex(O(e.end, -1)),
            r = Math.max(0, t),
            i = Math.min(this.cnt - 1, n);
        return (r = Math.ceil(r)) <= (i = Math.floor(i)) ? {
            firstIndex: r,
            lastIndex: i,
            isStart: t === r,
            isEnd: n === i
        } : null
    }, Di.prototype.getDateDayIndex = function (e) {
        var t = this.indices,
            n = Math.floor(A(this.dates[0], e));
        return n < 0 ? t[0] - 1 : n >= t.length ? t[t.length - 1] + 1 : t[n]
    }, Di);

    function Di(e, t) {
        for (var n = e.start, r = e.end, i = [], o = [], a = -1; n < r;) t.isHiddenDay(n) ? i.push(a + .5) : (a++, i.push(a), o.push(n)), n = O(n, 1);
        this.dates = o, this.indices = i, this.cnt = o.length
    }
    var Ti = (Ci.prototype.buildCells = function () {
        for (var e = [], t = 0; t < this.rowCnt; t++) {
            for (var n = [], r = 0; r < this.colCnt; r++) n.push(this.buildCell(t, r));
            e.push(n)
        }
        return e
    }, Ci.prototype.buildCell = function (e, t) {
        return {
            date: this.daySeries.dates[e * this.colCnt + t]
        }
    }, Ci.prototype.buildHeaderDates = function () {
        for (var e = [], t = 0; t < this.colCnt; t++) e.push(this.cells[0][t].date);
        return e
    }, Ci.prototype.sliceRange = function (e) {
        var t = this.colCnt,
            n = this.daySeries.sliceRange(e),
            r = [];
        if (n)
            for (var i = n.firstIndex, o = n.lastIndex, a = i; a <= o;) {
                var s = Math.floor(a / t),
                    l = Math.min((s + 1) * t, o + 1);
                r.push({
                    row: s,
                    firstCol: a % t,
                    lastCol: (l - 1) % t,
                    isStart: n.isStart && a === i,
                    isEnd: n.isEnd && l - 1 === o
                }), a = l
            }
        return r
    }, Ci);

    function Ci(e, t) {
        var n, r, i, o = e.dates;
        if (t) {
            for (r = o[0].getUTCDay(), n = 1; n < o.length && o[n].getUTCDay() !== r; n++);
            i = Math.ceil(o.length / n)
        } else i = 1, n = o.length;
        this.rowCnt = i, this.colCnt = n, this.daySeries = e, this.cells = this.buildCells(), this.headerDates = this.buildHeaderDates()
    }
    var Ei = (_i.prototype.sliceProps = function (e, t, n, r) {
        for (var i = [], o = 4; o < arguments.length; o++) i[o - 4] = arguments[o];
        var a = e.eventUiBases,
            s = this.sliceEventStore.apply(this, [e.eventStore, a, t, n, r].concat(i));
        return {
            dateSelectionSegs: this.sliceDateSelection.apply(this, [e.dateSelection, a, r].concat(i)),
            businessHourSegs: this.sliceBusinessHours.apply(this, [e.businessHours, t, n, r].concat(i)),
            fgEventSegs: s.fg,
            bgEventSegs: s.bg,
            eventDrag: this.sliceEventDrag.apply(this, [e.eventDrag, a, t, n, r].concat(i)),
            eventResize: this.sliceEventResize.apply(this, [e.eventResize, a, t, n, r].concat(i)),
            eventSelection: e.eventSelection
        }
    }, _i.prototype.sliceNowDate = function (e, t) {
        for (var n = [], r = 2; r < arguments.length; r++) n[r - 2] = arguments[r];
        return this._sliceDateSpan.apply(this, [{
            range: {
                start: e,
                end: H(e, 1)
            },
            allDay: !1
        }, {}, t].concat(n))
    }, _i.prototype._sliceBusinessHours = function (e, t, n, r) {
        for (var i = [], o = 4; o < arguments.length; o++) i[o - 4] = arguments[o];
        return e ? this._sliceEventStore.apply(this, [Re(e, xi(t, Boolean(n)), r.calendar), {}, t, n, r].concat(i)).bg : []
    }, _i.prototype._sliceEventStore = function (e, t, n, r, i) {
        for (var o = [], a = 5; a < arguments.length; a++) o[a - 5] = arguments[a];
        if (e) {
            var s = pt(e, t, xi(n, Boolean(r)), r);
            return {
                bg: this.sliceEventRanges(s.bg, i, o),
                fg: this.sliceEventRanges(s.fg, i, o)
            }
        }
        return {
            bg: [],
            fg: []
        }
    }, _i.prototype._sliceInteraction = function (e, t, n, r, i) {
        for (var o = [], a = 5; a < arguments.length; a++) o[a - 5] = arguments[a];
        if (!e) return null;
        var s = pt(e.mutatedEvents, t, xi(n, Boolean(r)), r);
        return {
            segs: this.sliceEventRanges(s.fg, i, o),
            affectedInstances: e.affectedEvents.instances,
            isEvent: e.isEvent,
            sourceSeg: e.origSeg
        }
    }, _i.prototype._sliceDateSpan = function (e, t, n) {
        for (var r = [], i = 3; i < arguments.length; i++) r[i - 3] = arguments[i];
        if (!e) return [];
        for (var o = function (e, t, n) {
            var r = Vt({
                editable: !1
            }, "", e.allDay, !0, n);
            return {
                def: r,
                ui: mt(r, t),
                instance: Gt(r.defId, e.range),
                range: e.range,
                isStart: !0,
                isEnd: !0
            }
        }(e, t, n.calendar), a = this.sliceRange.apply(this, [e.range].concat(r)), s = 0, l = a; s < l.length; s++) {
            var u = l[s];
            u.component = n, u.eventRange = o
        }
        return a
    }, _i.prototype.sliceEventRanges = function (e, t, n) {
        for (var r = [], i = 0, o = e; i < o.length; i++) {
            var a = o[i];
            r.push.apply(r, this.sliceEventRange(a, t, n))
        }
        return r
    }, _i.prototype.sliceEventRange = function (e, t, n) {
        for (var r = this.sliceRange.apply(this, [e.range].concat(n)), i = 0, o = r; i < o.length; i++) {
            var a = o[i];
            a.component = t, a.eventRange = e, a.isStart = e.isStart && a.isStart, a.isEnd = e.isEnd && a.isEnd
        }
        return r
    }, _i);

    function _i() {
        this.sliceBusinessHours = We(this._sliceBusinessHours), this.sliceDateSelection = We(this._sliceDateSpan), this.sliceEventStore = We(this._sliceEventStore), this.sliceEventDrag = We(this._sliceInteraction), this.sliceEventResize = We(this._sliceInteraction)
    }

    function xi(e, t) {
        var n = e.activeRange;
        return t ? n : {
            start: H(n.start, e.minTime.milliseconds),
            end: H(n.end, e.maxTime.milliseconds - 864e5)
        }
    }
    e.Calendar = Xr, e.Component = _n, e.DateComponent = Rn, e.DateEnv = or, e.DateProfileGenerator = mr, e.DayHeader = bi, e.DaySeries = wi, e.DayTable = Ti, e.ElementDragging = di, e.ElementScrollController = gn, e.EmitterMixin = an, e.EventApi = dt, e.FgEventRenderer = oi, e.FillRenderer = li, e.Interaction = Lr, e.Mixin = nn, e.NamedTimeZoneImpl = ci, e.PositionCache = cn, e.ScrollComponent = wn, e.ScrollController = fn, e.Slicer = Ei, e.Splitter = Jt, e.Theme = Tn, e.View = ri, e.WindowScrollController = yn, e.addDays = O, e.addDurations = function (e, t) {
        return {
            years: e.years + t.years,
            months: e.months + t.months,
            days: e.days + t.days,
            milliseconds: e.milliseconds + t.milliseconds
        }
    }, e.addMs = H, e.addWeeks = function (e, t) {
        var n = W(e);
        return n[2] += 7 * t, V(n)
    }, e.allowContextMenu = function (e) {
        e.removeEventListener("contextmenu", R)
    }, e.allowSelection = function (e) {
        e.classList.remove("fc-unselectable"), e.removeEventListener("selectstart", R)
    }, e.appendToElement = w, e.applyAll = ce, e.applyMutationToEventStore = yt, e.applyStyle = g, e.applyStyleProp = v, e.asRoughMinutes = function (e) {
        return ee(e) / 6e4
    }, e.asRoughMs = ee, e.asRoughSeconds = function (e) {
        return ee(e) / 1e3
    }, e.buildGotoAnchorHtml = Kt, e.buildSegCompareObj = si, e.capitaliseFirstLetter = se, e.combineEventUis = Ft, e.compareByFieldSpec = oe, e.compareByFieldSpecs = ie, e.compareNumbers = function (e, t) {
        return e - t
    }, e.compensateScroll = function (e, t) {
        t.left && g(e, {
            borderLeftWidth: 1,
            marginLeft: t.left - 1
        }), t.right && g(e, {
            borderRightWidth: 1,
            marginRight: t.right - 1
        })
    }, e.computeClippingRect = function (e) {
        return I(e).map(function (e) {
            return C(e)
        }).concat({
            left: window.pageXOffset,
            right: window.pageXOffset + document.documentElement.clientWidth,
            top: window.pageYOffset,
            bottom: window.pageYOffset + document.documentElement.clientHeight
        }).reduce(function (e, t) {
            return m(e, t) || t
        })
    }, e.computeEdges = T, e.computeFallbackHeaderFormat = vi, e.computeHeightAndMargins = _, e.computeInnerRect = C, e.computeRect = E, e.computeVisibleDayRange = ge, e.config = {}, e.constrainPoint = function (e, t) {
        return {
            left: Math.min(Math.max(e.left, t.left), t.right),
            top: Math.min(Math.max(e.top, t.top), t.bottom)
        }
    }, e.createDuration = X, e.createElement = a, e.createEmptyEventStore = Me, e.createEventInstance = Gt, e.createFormatter = ot, e.createPlugin = Mn, e.cssToStr = Mt, e.debounce = fe, e.diffDates = ve, e.diffDayAndTime = N, e.diffDays = A, e.diffPoints = function (e, t) {
        return {
            left: e.left - t.left,
            top: e.top - t.top
        }
    }, e.diffWeeks = function (e, t) {
        return A(e, t) / 7
    }, e.diffWholeDays = F, e.diffWholeWeeks = L, e.disableCursor = function () {
        document.body.classList.add("fc-not-allowed")
    }, e.distributeHeight = function (o, e, t) {
        var a = Math.floor(e / o.length),
            s = Math.floor(e - a * (o.length - 1)),
            l = [],
            u = [],
            c = [],
            d = 0;
        ne(o), o.forEach(function (e, t) {
            var n = t === o.length - 1 ? s : a,
                r = e.getBoundingClientRect().height,
                i = r + x(e);
            i < n ? (l.push(e), u.push(i), c.push(r)) : d += i
        }), t && (e -= d, a = Math.floor(e / l.length), s = Math.floor(e - a * (l.length - 1))), l.forEach(function (e, t) {
            var n = t === l.length - 1 ? s : a,
                r = u[t],
                i = n - (r - c[t]);
            r < n && (e.style.height = i + "px")
        })
    }, e.elementClosest = c, e.elementMatches = d, e.enableCursor = function () {
        document.body.classList.remove("fc-not-allowed")
    }, e.eventTupleToStore = Ie, e.filterEventStoreDefs = He, e.filterHash = De, e.findChildren = function (e, t) {
        for (var n = e instanceof HTMLElement ? [e] : e, r = [], i = 0; i < n.length; i++)
            for (var o = n[i].children, a = 0; a < o.length; a++) {
                var s = o[a];
                t && !d(s, t) || r.push(s)
            }
        return r
    }, e.findElements = p, e.flexibleCompare = ae, e.forceClassName = function (e, t, n) {
        n ? e.classList.add(t) : e.classList.remove(t)
    }, e.formatDate = function (e, t) {
        void 0 === t && (t = {});
        var n = pi(t),
            r = ot(t),
            i = n.createMarkerMeta(e);
        return i ? n.format(i.marker, r, {
            forcedTzo: i.forcedTzo
        }) : ""
    }, e.formatIsoTimeString = function (e) {
        return le(e.getUTCHours(), 2) + ":" + le(e.getUTCMinutes(), 2) + ":" + le(e.getUTCSeconds(), 2)
    }, e.formatRange = function (e, t, n) {
        var r = pi("object" == typeof n && n ? n : {}),
            i = ot(n, Wn.defaultRangeSeparator),
            o = r.createMarkerMeta(e),
            a = r.createMarkerMeta(t);
        return o && a ? r.formatRange(o.marker, a.marker, i, {
            forcedStartTzo: o.forcedTzo,
            forcedEndTzo: a.forcedTzo,
            isEndExclusive: n.isEndExclusive
        }) : ""
    }, e.getAllDayHtml = function (e) {
        return e.opt("allDayHtml") || Pt(e.opt("allDayText"))
    }, e.getClippingParents = I, e.getDayClasses = en, e.getElSeg = gt, e.getRectCenter = function (e) {
        return {
            left: (e.left + e.right) / 2,
            top: (e.top + e.bottom) / 2
        }
    }, e.getRelevantEvents = ke, e.globalDefaults = Wn, e.greatestDurationDenominator = te, e.hasBgRendering = function (e) {
        return "background" === e.rendering || "inverse-background" === e.rendering
    }, e.htmlEscape = Pt, e.htmlToElement = S, e.insertAfterElement = function (e, t) {
        for (var n = l(t), r = e.nextSibling || null, i = 0; i < n.length; i++) e.parentNode.insertBefore(n[i], r)
    }, e.interactionSettingsStore = Br, e.interactionSettingsToStore = function (e) {
        var t;
        return (t = {})[e.component.uid] = e, t
    }, e.intersectRanges = Le, e.intersectRects = m, e.isArraysEqual = Ue, e.isDateSpansEqual = function (e, t) {
        return Fe(e.range, t.range) && e.allDay === t.allDay && function (e, t) {
            for (var n in t)
                if ("range" !== n && "allDay" !== n && e[n] !== t[n]) return !1;
            for (var n in e)
                if (!(n in t)) return !1;
            return !0
        }(e, t)
    }, e.isInt = ue, e.isInteractionValid = Ct, e.isMultiDayRange = function (e) {
        var t = ge(e);
        return 1 < A(t.start, t.end)
    }, e.isPropsEqual = _e, e.isPropsValid = _t, e.isSingleDay = function (e) {
        return 0 === e.years && 0 === e.months && 1 === e.days && 0 === e.milliseconds
    }, e.isValidDate = G, e.listenBySelector = k, e.mapHash = Te, e.matchCellWidths = function (e) {
        var r = 0;
        return e.forEach(function (e) {
            var t = e.firstChild;
            if (t instanceof HTMLElement) {
                var n = t.getBoundingClientRect().width;
                r < n && (r = n)
            }
        }), r++, e.forEach(function (e) {
            e.style.width = r + "px"
        }), r
    }, e.memoize = We, e.memoizeOutput = Ve, e.memoizeRendering = Yt, e.mergeEventStores = Oe, e.multiplyDuration = function (e, t) {
        return {
            years: e.years * t,
            months: e.months * t,
            days: e.days * t,
            milliseconds: e.milliseconds * t
        }
    }, e.padStart = le, e.parseBusinessHours = qt, e.parseDragMeta = function (e) {
        var t = {},
            n = pe(e, hi, gi, t);
        return n.leftoverProps = t, n
    }, e.parseEventDef = Vt, e.parseFieldSpecs = re, e.parseMarker = ir, e.pointInsideRect = function (e, t) {
        return e.left >= t.left && e.left < t.right && e.top >= t.top && e.top < t.bottom
    }, e.prependToElement = f, e.preventContextMenu = function (e) {
        e.addEventListener("contextmenu", R)
    }, e.preventDefault = R, e.preventSelection = function (e) {
        e.classList.add("fc-unselectable"), e.addEventListener("selectstart", R)
    }, e.processScopedUiProps = Nt, e.rangeContainsMarker = je, e.rangeContainsRange = Be, e.rangesEqual = Fe, e.rangesIntersect = ze, e.refineProps = pe, e.removeElement = r, e.removeExact = function (e, t) {
        for (var n = 0, r = 0; r < e.length;) e[r] === t ? (e.splice(r, 1), n++) : r++;
        return n
    }, e.renderDateCell = mi, e.requestJson = Ln, e.sliceEventStore = pt, e.startOfDay = z, e.subtractInnerElHeight = function (e, t) {
        var n = {
            position: "relative",
            left: -1
        };
        g(e, n), g(t, n);
        var r = e.getBoundingClientRect().height - t.getBoundingClientRect().height,
            i = {
                position: "",
                left: ""
            };
        return g(e, i), g(t, i), r
    }, e.translateRect = function (e, t, n) {
        return {
            left: e.left + t,
            right: e.right + t,
            top: e.top + n,
            bottom: e.bottom + n
        }
    }, e.uncompensateScroll = function (e) {
        g(e, {
            marginLeft: "",
            marginRight: "",
            borderLeftWidth: "",
            borderRightWidth: ""
        })
    }, e.undistributeHeight = ne, e.unpromisify = tn, e.version = "4.3.1", e.whenTransitionDone = function (t, n) {
        var r = function (e) {
            n(e), P.forEach(function (e) {
                t.removeEventListener(e, r)
            })
        };
        P.forEach(function (e) {
            t.addEventListener(e, r)
        })
    }, e.wholeDivideDurations = function (e, t) {
        for (var n = null, r = 0; r < q.length; r++) {
            var i = q[r];
            if (t[i]) {
                var o = e[i] / t[i];
                if (!ue(o) || null !== n && n !== o) return null;
                n = o
            } else if (e[i]) return null
        }
        return n
    }, Object.defineProperty(e, "__esModule", {
        value: !0
    })
}),
    function (e, t) {
        "object" == typeof exports && "undefined" != typeof module ? t(exports, require("@fullcalendar/core")) : "function" == typeof define && define.amd ? define(["exports", "@fullcalendar/core"], t) : t((e = e || self).FullCalendarDayGrid = {}, e.FullCalendar)
    }(this, function (e, _) {
        "use strict";
        var r = function (e, t) {
            return (r = Object.setPrototypeOf || {
                __proto__: []
            }
                instanceof Array && function (e, t) {
                    e.__proto__ = t
                } || function (e, t) {
                    for (var n in t) t.hasOwnProperty(n) && (e[n] = t[n])
                })(e, t)
        };

        function t(e, t) {
            function n() {
                this.constructor = e
            }
            r(e, t), e.prototype = null === t ? Object.create(t) : (n.prototype = t.prototype, new n)
        }
        var u, d = function () {
            return (d = Object.assign || function (e) {
                for (var t, n = 1, r = arguments.length; n < r; n++)
                    for (var i in t = arguments[n]) Object.prototype.hasOwnProperty.call(t, i) && (e[i] = t[i]);
                return e
            }).apply(this, arguments)
        },
            n = (t(i, u = _.DateProfileGenerator), i.prototype.buildRenderRange = function (e, t, n) {
                var r, i = this.dateEnv,
                    o = u.prototype.buildRenderRange.call(this, e, t, n),
                    a = o.start,
                    s = o.end;
                if (/^(year|month)$/.test(t) && (a = i.startOfWeek(a), (r = i.startOfWeek(s)).valueOf() !== s.valueOf() && (s = _.addWeeks(r, 1))), this.options.monthMode && this.options.fixedWeekCount) {
                    var l = Math.ceil(_.diffWeeks(a, s));
                    s = _.addWeeks(s, 6 - l)
                }
                return {
                    start: a,
                    end: s
                }
            }, i);

        function i() {
            return null !== u && u.apply(this, arguments) || this
        }
        var f = (o.prototype.show = function () {
            this.isHidden && (this.el || this.render(), this.el.style.display = "", this.position(), this.isHidden = !1, this.trigger("show"))
        }, o.prototype.hide = function () {
            this.isHidden || (this.el.style.display = "none", this.isHidden = !0, this.trigger("hide"))
        }, o.prototype.render = function () {
            var t = this,
                e = this.options,
                n = this.el = _.createElement("div", {
                    className: "fc-popover " + (e.className || ""),
                    style: {
                        top: "0",
                        left: "0"
                    }
                });
            "function" == typeof e.content && e.content(n), e.parentEl.appendChild(n), _.listenBySelector(n, "click", ".fc-close", function (e) {
                t.hide()
            }), e.autoHide && document.addEventListener("mousedown", this.documentMousedown)
        }, o.prototype.destroy = function () {
            this.hide(), this.el && (_.removeElement(this.el), this.el = null), document.removeEventListener("mousedown", this.documentMousedown)
        }, o.prototype.position = function () {
            var e, t, n = this.options,
                r = this.el,
                i = r.getBoundingClientRect(),
                o = _.computeRect(r.offsetParent),
                a = _.computeClippingRect(n.parentEl);
            e = n.top || 0, t = void 0 !== n.left ? n.left : void 0 !== n.right ? n.right - i.width : 0, e = Math.min(e, a.bottom - i.height - this.margin), e = Math.max(e, a.top + this.margin), t = Math.min(t, a.right - i.width - this.margin), t = Math.max(t, a.left + this.margin), _.applyStyle(r, {
                top: e - o.top,
                left: t - o.left
            })
        }, o.prototype.trigger = function (e) {
            this.options[e] && this.options[e].apply(this, Array.prototype.slice.call(arguments, 1))
        }, o);

        function o(e) {
            var t = this;
            this.isHidden = !0, this.margin = 10, this.documentMousedown = function (e) {
                t.el && !t.el.contains(e.target) && t.hide()
            }, this.options = e
        }
        var a, s = (t(l, a = _.FgEventRenderer), l.prototype.renderSegHtml = function (e, t) {
            var n, r, i = this.context,
                o = i.view,
                a = i.options,
                s = e.eventRange,
                l = s.def,
                u = s.ui,
                c = l.allDay,
                d = o.computeEventDraggable(l, u),
                f = c && e.isStart && o.computeEventStartResizable(l, u),
                p = c && e.isEnd && o.computeEventEndResizable(l, u),
                h = this.getSegClasses(e, d, f || p, t),
                g = _.cssToStr(this.getSkinCss(u)),
                v = "";
            return h.unshift("fc-day-grid-event", "fc-h-event"), e.isStart && (n = this.getTimeText(s)) && (v = '<span class="fc-time">' + _.htmlEscape(n) + "</span>"), r = '<span class="fc-title">' + (_.htmlEscape(l.title || "") || "&nbsp;") + "</span>", '<a class="' + h.join(" ") + '"' + (l.url ? ' href="' + _.htmlEscape(l.url) + '"' : "") + (g ? ' style="' + g + '"' : "") + '><div class="fc-content">' + ("rtl" === a.dir ? r + " " + v : v + " " + r) + "</div>" + (f ? '<div class="fc-resizer fc-start-resizer"></div>' : "") + (p ? '<div class="fc-resizer fc-end-resizer"></div>' : "") + "</a>"
        }, l.prototype.computeEventTimeFormat = function () {
            return {
                hour: "numeric",
                minute: "2-digit",
                omitZeroMinute: !0,
                meridiem: "narrow"
            }
        }, l.prototype.computeDisplayEventEnd = function () {
            return !1
        }, l);

        function l() {
            return null !== a && a.apply(this, arguments) || this
        }
        var c, p = (t(h, c = s), h.prototype.attachSegs = function (e, t) {
            var n = this.rowStructs = this.renderSegRows(e);
            this.dayGrid.rowEls.forEach(function (e, t) {
                e.querySelector(".fc-content-skeleton > table").appendChild(n[t].tbodyEl)
            }), t || this.dayGrid.removeSegPopover()
        }, h.prototype.detachSegs = function () {
            for (var e, t = this.rowStructs || []; e = t.pop();) _.removeElement(e.tbodyEl);
            this.rowStructs = null
        }, h.prototype.renderSegRows = function (e) {
            var t, n, r = [];
            for (t = this.groupSegRows(e), n = 0; n < t.length; n++) r.push(this.renderSegRow(n, t[n]));
            return r
        }, h.prototype.renderSegRow = function (e, t) {
            var n, r, i, o, a, s, l, u = this.dayGrid,
                c = u.colCnt,
                d = u.isRtl,
                f = this.buildSegLevels(t),
                p = Math.max(1, f.length),
                h = document.createElement("tbody"),
                g = [],
                v = [],
                m = [];

            function y(e) {
                for (; i < e;)(l = (m[n - 1] || [])[i]) ? l.rowSpan = (l.rowSpan || 1) + 1 : (l = document.createElement("td"), o.appendChild(l)), v[n][i] = l, m[n][i] = l, i++
            }
            for (n = 0; n < p; n++) {
                if (r = f[n], i = 0, o = document.createElement("tr"), g.push([]), v.push([]), m.push([]), r)
                    for (a = 0; a < r.length; a++) {
                        s = r[a];
                        var b = d ? c - 1 - s.lastCol : s.firstCol,
                            S = d ? c - 1 - s.firstCol : s.lastCol;
                        for (y(b), l = _.createElement("td", {
                            className: "fc-event-container"
                        }, s.el), b !== S ? l.colSpan = S - b + 1 : m[n][i] = l; i <= S;) v[n][i] = l, g[n][i] = s, i++;
                        o.appendChild(l)
                    }
                y(c);
                var w = u.renderProps.renderIntroHtml();
                w && (u.isRtl ? _.appendToElement(o, w) : _.prependToElement(o, w)), h.appendChild(o)
            }
            return {
                row: e,
                tbodyEl: h,
                cellMatrix: v,
                segMatrix: g,
                segLevels: f,
                segs: t
            }
        }, h.prototype.buildSegLevels = function (e) {
            var t, n, r, i = this.dayGrid,
                o = i.isRtl,
                a = i.colCnt,
                s = [];
            for (e = this.sortEventSegs(e), t = 0; t < e.length; t++) {
                for (n = e[t], r = 0; r < s.length && g(n, s[r]); r++);
                n.level = r, n.leftCol = o ? a - 1 - n.lastCol : n.firstCol, n.rightCol = o ? a - 1 - n.firstCol : n.lastCol, (s[r] || (s[r] = [])).push(n)
            }
            for (r = 0; r < s.length; r++) s[r].sort(v);
            return s
        }, h.prototype.groupSegRows = function (e) {
            var t, n = [];
            for (t = 0; t < this.dayGrid.rowCnt; t++) n.push([]);
            for (t = 0; t < e.length; t++) n[e[t].row].push(e[t]);
            return n
        }, h.prototype.computeDisplayEventEnd = function () {
            return 1 === this.dayGrid.colCnt
        }, h);

        function h(e) {
            var t = c.call(this, e.context) || this;
            return t.dayGrid = e, t
        }

        function g(e, t) {
            var n, r;
            for (n = 0; n < t.length; n++)
                if ((r = t[n]).firstCol <= e.lastCol && r.lastCol >= e.firstCol) return !0;
            return !1
        }

        function v(e, t) {
            return e.leftCol - t.leftCol
        }
        var m, y = (t(b, m = p), b.prototype.attachSegs = function (e, t) {
            var i = t.sourceSeg,
                o = this.rowStructs = this.renderSegRows(e);
            this.dayGrid.rowEls.forEach(function (e, t) {
                var n, r = _.htmlToElement('<div class="fc-mirror-skeleton"><table></table></div>');
                n = (i && i.row === t ? i.el : e.querySelector(".fc-content-skeleton tbody") || e.querySelector(".fc-content-skeleton table")).getBoundingClientRect().top - e.getBoundingClientRect().top, r.style.top = n + "px", r.querySelector("table").appendChild(o[t].tbodyEl), e.appendChild(r)
            })
        }, b);

        function b() {
            return null !== m && m.apply(this, arguments) || this
        }
        var S, w = '<td style="pointer-events:none"></td>',
            D = (t(T, S = _.FillRenderer), T.prototype.renderSegs = function (e, t) {
                "bgEvent" === e && (t = t.filter(function (e) {
                    return e.eventRange.def.allDay
                })), S.prototype.renderSegs.call(this, e, t)
            }, T.prototype.attachSegs = function (e, t) {
                var n, r, i, o = [];
                for (n = 0; n < t.length; n++) r = t[n], i = this.renderFillRow(e, r), this.dayGrid.rowEls[r.row].appendChild(i), o.push(i);
                return o
            }, T.prototype.renderFillRow = function (e, t) {
                var n, r, i, o = this.dayGrid,
                    a = o.colCnt,
                    s = o.isRtl,
                    l = s ? a - 1 - t.lastCol : t.firstCol,
                    u = (s ? a - 1 - t.firstCol : t.lastCol) + 1;
                n = "businessHours" === e ? "bgevent" : e.toLowerCase(), i = (r = _.htmlToElement('<div class="fc-' + n + '-skeleton"><table><tr></tr></table></div>')).getElementsByTagName("tr")[0], 0 < l && _.appendToElement(i, new Array(l + 1).join(w)), t.el.colSpan = u - l, i.appendChild(t.el), u < a && _.appendToElement(i, new Array(a - u + 1).join(w));
                var c = o.renderProps.renderIntroHtml();
                return c && (o.isRtl ? _.appendToElement(i, c) : _.prependToElement(i, c)), r
            }, T);

        function T(e) {
            var t = S.call(this, e.context) || this;
            return t.fillSegTag = "td", t.dayGrid = e, t
        }
        var C, E = (t(x, C = _.DateComponent), x.prototype.render = function (e) {
            this.renderFrame(e.date), this.renderFgEvents(e.fgSegs), this.renderEventSelection(e.eventSelection), this.renderEventDrag(e.eventDragInstances), this.renderEventResize(e.eventResizeInstances)
        }, x.prototype.destroy = function () {
            C.prototype.destroy.call(this), this.renderFrame.unrender(), this.calendar.unregisterInteractiveComponent(this)
        }, x.prototype._renderFrame = function (e) {
            var t = this.theme,
                n = this.dateEnv.format(e, _.createFormatter(this.opt("dayPopoverFormat")));
            this.el.innerHTML = '<div class="fc-header ' + t.getClass("popoverHeader") + '"><span class="fc-title">' + _.htmlEscape(n) + '</span><span class="fc-close ' + t.getIconClass("close") + '"></span></div><div class="fc-body ' + t.getClass("popoverContent") + '"><div class="fc-event-container"></div></div>', this.segContainerEl = this.el.querySelector(".fc-event-container")
        }, x.prototype.queryHit = function (e, t, n, r) {
            var i = this.props.date;
            if (e < n && t < r) return {
                component: this,
                dateSpan: {
                    allDay: !0,
                    range: {
                        start: i,
                        end: _.addDays(i, 1)
                    }
                },
                dayEl: this.el,
                rect: {
                    left: 0,
                    top: 0,
                    right: n,
                    bottom: r
                },
                layer: 1
            }
        }, x);

        function x(e, t) {
            var n = C.call(this, e, t) || this,
                r = n.eventRenderer = new R(n),
                i = n.renderFrame = _.memoizeRendering(n._renderFrame);
            return n.renderFgEvents = _.memoizeRendering(r.renderSegs.bind(r), r.unrender.bind(r), [i]), n.renderEventSelection = _.memoizeRendering(r.selectByInstanceId.bind(r), r.unselectByInstanceId.bind(r), [n.renderFgEvents]), n.renderEventDrag = _.memoizeRendering(r.hideByHash.bind(r), r.showByHash.bind(r), [i]), n.renderEventResize = _.memoizeRendering(r.hideByHash.bind(r), r.showByHash.bind(r), [i]), e.calendar.registerInteractiveComponent(n, {
                el: n.el,
                useEventCenter: !1
            }), n
        }
        var I, R = (t(k, I = s), k.prototype.attachSegs = function (e) {
            for (var t = 0, n = e; t < n.length; t++) {
                var r = n[t];
                this.dayTile.segContainerEl.appendChild(r.el)
            }
        }, k.prototype.detachSegs = function (e) {
            for (var t = 0, n = e; t < n.length; t++) {
                var r = n[t];
                _.removeElement(r.el)
            }
        }, k);

        function k(e) {
            var t = I.call(this, e.context) || this;
            return t.dayTile = e, t
        }
        var P = (M.prototype.renderHtml = function (e) {
            var t = [];
            e.renderIntroHtml && t.push(e.renderIntroHtml());
            for (var n = 0, r = e.cells; n < r.length; n++) {
                var i = r[n];
                t.push(O(i.date, e.dateProfile, this.context, i.htmlAttrs))
            }
            return e.cells.length || t.push('<td class="fc-day ' + this.context.theme.getClass("widgetContent") + '"></td>'), "rtl" === this.context.options.dir && t.reverse(), "<tr>" + t.join("") + "</tr>"
        }, M);

        function M(e) {
            this.context = e
        }

        function O(e, t, n, r) {
            var i = n.dateEnv,
                o = n.theme,
                a = _.rangeContainsMarker(t.activeRange, e),
                s = _.getDayClasses(e, t, n);
            return s.unshift("fc-day", o.getClass("widgetContent")), '<td class="' + s.join(" ") + '"' + (a ? ' data-date="' + i.formatIso(e, {
                omitTime: !0
            }) + '"' : "") + (r ? " " + r : "") + "></td>"
        }
        var H, A = _.createFormatter({
            day: "numeric"
        }),
            N = _.createFormatter({
                week: "numeric"
            }),
            L = (t(F, H = _.DateComponent), F.prototype.render = function (e) {
                var t = e.cells;
                this.rowCnt = t.length, this.colCnt = t[0].length, this.renderCells(t, e.isRigid), this.renderBusinessHours(e.businessHourSegs), this.renderDateSelection(e.dateSelectionSegs), this.renderBgEvents(e.bgEventSegs), this.renderFgEvents(e.fgEventSegs), this.renderEventSelection(e.eventSelection), this.renderEventDrag(e.eventDrag), this.renderEventResize(e.eventResize), this.segPopoverTile && this.updateSegPopoverTile()
            }, F.prototype.destroy = function () {
                H.prototype.destroy.call(this), this.renderCells.unrender()
            }, F.prototype.getCellRange = function (e, t) {
                var n = this.props.cells[e][t].date;
                return {
                    start: n,
                    end: _.addDays(n, 1)
                }
            }, F.prototype.updateSegPopoverTile = function (e, t) {
                var n = this.props;
                this.segPopoverTile.receiveProps({
                    date: e || this.segPopoverTile.props.date,
                    fgSegs: t || this.segPopoverTile.props.fgSegs,
                    eventSelection: n.eventSelection,
                    eventDragInstances: n.eventDrag ? n.eventDrag.affectedInstances : null,
                    eventResizeInstances: n.eventResize ? n.eventResize.affectedInstances : null
                })
            }, F.prototype._renderCells = function (e, t) {
                var n, r, i = this.view,
                    o = this.dateEnv,
                    a = this.rowCnt,
                    s = this.colCnt,
                    l = "";
                for (n = 0; n < a; n++) l += this.renderDayRowHtml(n, t);
                for (this.el.innerHTML = l, this.rowEls = _.findElements(this.el, ".fc-row"), this.cellEls = _.findElements(this.el, ".fc-day, .fc-disabled-day"), this.isRtl && this.cellEls.reverse(), this.rowPositions = new _.PositionCache(this.el, this.rowEls, !1, !0), this.colPositions = new _.PositionCache(this.el, this.cellEls.slice(0, s), !0, !1), n = 0; n < a; n++)
                    for (r = 0; r < s; r++) this.publiclyTrigger("dayRender", [{
                        date: o.toDate(e[n][r].date),
                        el: this.getCellEl(n, r),
                        view: i
                    }]);
                this.isCellSizesDirty = !0
            }, F.prototype._unrenderCells = function () {
                this.removeSegPopover()
            }, F.prototype.renderDayRowHtml = function (e, t) {
                var n = this.theme,
                    r = ["fc-row", "fc-week", n.getClass("dayRow")];
                t && r.push("fc-rigid");
                var i = new P(this.context);
                return '<div class="' + r.join(" ") + '"><div class="fc-bg"><table class="' + n.getClass("tableGrid") + '">' + i.renderHtml({
                    cells: this.props.cells[e],
                    dateProfile: this.props.dateProfile,
                    renderIntroHtml: this.renderProps.renderBgIntroHtml
                }) + '</table></div><div class="fc-content-skeleton"><table>' + (this.getIsNumbersVisible() ? "<thead>" + this.renderNumberTrHtml(e) + "</thead>" : "") + "</table></div></div>"
            }, F.prototype.getIsNumbersVisible = function () {
                return this.getIsDayNumbersVisible() || this.renderProps.cellWeekNumbersVisible || this.renderProps.colWeekNumbersVisible
            }, F.prototype.getIsDayNumbersVisible = function () {
                return 1 < this.rowCnt
            }, F.prototype.renderNumberTrHtml = function (e) {
                var t = this.renderProps.renderNumberIntroHtml(e, this);
                return "<tr>" + (this.isRtl ? "" : t) + this.renderNumberCellsHtml(e) + (this.isRtl ? t : "") + "</tr>"
            }, F.prototype.renderNumberCellsHtml = function (e) {
                var t, n, r = [];
                for (t = 0; t < this.colCnt; t++) n = this.props.cells[e][t].date, r.push(this.renderNumberCellHtml(n));
                return this.isRtl && r.reverse(), r.join("")
            }, F.prototype.renderNumberCellHtml = function (e) {
                var t, n, r = this.view,
                    i = this.dateEnv,
                    o = "",
                    a = _.rangeContainsMarker(this.props.dateProfile.activeRange, e),
                    s = this.getIsDayNumbersVisible() && a;
                return s || this.renderProps.cellWeekNumbersVisible ? ((t = _.getDayClasses(e, this.props.dateProfile, this.context)).unshift("fc-day-top"), this.renderProps.cellWeekNumbersVisible && (n = i.weekDow), o += '<td class="' + t.join(" ") + '"' + (a ? ' data-date="' + i.formatIso(e, {
                    omitTime: !0
                }) + '"' : "") + ">", this.renderProps.cellWeekNumbersVisible && e.getUTCDay() === n && (o += _.buildGotoAnchorHtml(r, {
                    date: e,
                    type: "week"
                }, {
                    class: "fc-week-number"
                }, i.format(e, N))), s && (o += _.buildGotoAnchorHtml(r, e, {
                    class: "fc-day-number"
                }, i.format(e, A))), o += "</td>") : "<td></td>"
            }, F.prototype.updateSize = function (e) {
                var t = this.fillRenderer,
                    n = this.eventRenderer,
                    r = this.mirrorRenderer;
                (e || this.isCellSizesDirty || this.view.calendar.isEventsUpdated) && (this.buildPositionCaches(), this.isCellSizesDirty = !1), t.computeSizes(e), n.computeSizes(e), r.computeSizes(e), t.assignSizes(e), n.assignSizes(e), r.assignSizes(e)
            }, F.prototype.buildPositionCaches = function () {
                this.buildColPositions(), this.buildRowPositions()
            }, F.prototype.buildColPositions = function () {
                this.colPositions.build()
            }, F.prototype.buildRowPositions = function () {
                this.rowPositions.build(), this.rowPositions.bottoms[this.rowCnt - 1] += this.bottomCoordPadding
            }, F.prototype.positionToHit = function (e, t) {
                var n = this.colPositions,
                    r = this.rowPositions,
                    i = n.leftToIndex(e),
                    o = r.topToIndex(t);
                if (null != o && null != i) return {
                    row: o,
                    col: i,
                    dateSpan: {
                        range: this.getCellRange(o, i),
                        allDay: !0
                    },
                    dayEl: this.getCellEl(o, i),
                    relativeRect: {
                        left: n.lefts[i],
                        right: n.rights[i],
                        top: r.tops[o],
                        bottom: r.bottoms[o]
                    }
                }
            }, F.prototype.getCellEl = function (e, t) {
                return this.cellEls[e * this.colCnt + t]
            }, F.prototype._renderEventDrag = function (e) {
                e && (this.eventRenderer.hideByHash(e.affectedInstances), this.fillRenderer.renderSegs("highlight", e.segs))
            }, F.prototype._unrenderEventDrag = function (e) {
                e && (this.eventRenderer.showByHash(e.affectedInstances), this.fillRenderer.unrender("highlight"))
            }, F.prototype._renderEventResize = function (e) {
                e && (this.eventRenderer.hideByHash(e.affectedInstances), this.fillRenderer.renderSegs("highlight", e.segs), this.mirrorRenderer.renderSegs(e.segs, {
                    isResizing: !0,
                    sourceSeg: e.sourceSeg
                }))
            }, F.prototype._unrenderEventResize = function (e) {
                e && (this.eventRenderer.showByHash(e.affectedInstances), this.fillRenderer.unrender("highlight"), this.mirrorRenderer.unrender(e.segs, {
                    isResizing: !0,
                    sourceSeg: e.sourceSeg
                }))
            }, F.prototype.removeSegPopover = function () {
                this.segPopover && this.segPopover.hide()
            }, F.prototype.limitRows = function (e) {
                var t, n, r = this.eventRenderer.rowStructs || [];
                for (t = 0; t < r.length; t++) this.unlimitRow(t), !1 !== (n = !!e && ("number" == typeof e ? e : this.computeRowLevelLimit(t))) && this.limitRow(t, n)
            }, F.prototype.computeRowLevelLimit = function (e) {
                var t, n, r = this.rowEls[e].getBoundingClientRect().bottom,
                    i = _.findChildren(this.eventRenderer.rowStructs[e].tbodyEl);
                for (t = 0; t < i.length; t++)
                    if ((n = i[t]).classList.remove("fc-limited"), n.getBoundingClientRect().bottom > r) return t;
                return !1
            }, F.prototype.limitRow = function (t, n) {
                function e(e) {
                    for (; T < e;)(l = y.getCellSegs(t, T, n)).length && (d = i[n - 1][T], m = y.renderMoreLink(t, T, l), v = _.createElement("div", null, m), d.appendChild(v), D.push(v)), T++
                }
                var r, i, o, a, s, l, u, c, d, f, p, h, g, v, m, y = this,
                    b = this.colCnt,
                    S = this.isRtl,
                    w = this.eventRenderer.rowStructs[t],
                    D = [],
                    T = 0;
                if (n && n < w.segLevels.length) {
                    for (r = w.segLevels[n - 1], i = w.cellMatrix, (o = _.findChildren(w.tbodyEl).slice(n)).forEach(function (e) {
                        e.classList.add("fc-limited")
                    }), a = 0; a < r.length; a++) {
                        s = r[a];
                        var C = S ? b - 1 - s.lastCol : s.firstCol,
                            E = S ? b - 1 - s.firstCol : s.lastCol;
                        for (e(C), c = [], u = 0; T <= E;) l = this.getCellSegs(t, T, n), c.push(l), u += l.length, T++;
                        if (u) {
                            for (f = (d = i[n - 1][C]).rowSpan || 1, p = [], h = 0; h < c.length; h++) g = _.createElement("td", {
                                className: "fc-more-cell",
                                rowSpan: f
                            }), l = c[h], m = this.renderMoreLink(t, C + h, [s].concat(l)), v = _.createElement("div", null, m), g.appendChild(v), p.push(g), D.push(g);
                            d.classList.add("fc-limited"), _.insertAfterElement(d, p), o.push(d)
                        }
                    }
                    e(this.colCnt), w.moreEls = D, w.limitedEls = o
                }
            }, F.prototype.unlimitRow = function (e) {
                var t = this.eventRenderer.rowStructs[e];
                t.moreEls && (t.moreEls.forEach(_.removeElement), t.moreEls = null), t.limitedEls && (t.limitedEls.forEach(function (e) {
                    e.classList.remove("fc-limited")
                }), t.limitedEls = null)
            }, F.prototype.renderMoreLink = function (u, c, d) {
                var f = this,
                    p = this.view,
                    h = this.dateEnv,
                    e = _.createElement("a", {
                        className: "fc-more"
                    });
                return e.innerText = this.getMoreLinkText(d.length), e.addEventListener("click", function (e) {
                    var t = f.opt("eventLimitClick"),
                        n = f.isRtl ? f.colCnt - c - 1 : c,
                        r = f.props.cells[u][n].date,
                        i = e.currentTarget,
                        o = f.getCellEl(u, c),
                        a = f.getCellSegs(u, c),
                        s = f.resliceDaySegs(a, r),
                        l = f.resliceDaySegs(d, r);
                    "function" == typeof t && (t = f.publiclyTrigger("eventLimitClick", [{
                        date: h.toDate(r),
                        allDay: !0,
                        dayEl: o,
                        moreEl: i,
                        segs: s,
                        hiddenSegs: l,
                        jsEvent: e,
                        view: p
                    }])), "popover" === t ? f.showSegPopover(u, c, i, s) : "string" == typeof t && p.calendar.zoomTo(r, t)
                }), e
            }, F.prototype.showSegPopover = function (t, e, n, r) {
                var i, o, a = this,
                    s = this.calendar,
                    l = this.view,
                    u = this.theme,
                    c = this.isRtl ? this.colCnt - e - 1 : e,
                    d = n.parentNode;
                i = 1 === this.rowCnt ? l.el : this.rowEls[t], o = {
                    className: "fc-more-popover " + u.getClass("popover"),
                    parentEl: l.el,
                    top: _.computeRect(i).top,
                    autoHide: !0,
                    content: function (e) {
                        a.segPopoverTile = new E(a.context, e), a.updateSegPopoverTile(a.props.cells[t][c].date, r)
                    },
                    hide: function () {
                        a.segPopoverTile.destroy(), a.segPopoverTile = null, a.segPopover.destroy(), a.segPopover = null
                    }
                }, this.isRtl ? o.right = _.computeRect(d).right + 1 : o.left = _.computeRect(d).left - 1, this.segPopover = new f(o), this.segPopover.show(), s.releaseAfterSizingTriggers()
            }, F.prototype.resliceDaySegs = function (e, t) {
                for (var n = t, r = {
                    start: n,
                    end: _.addDays(n, 1)
                }, i = [], o = 0, a = e; o < a.length; o++) {
                    var s = a[o],
                        l = s.eventRange,
                        u = l.range,
                        c = _.intersectRanges(u, r);
                    c && i.push(d({}, s, {
                        eventRange: {
                            def: l.def,
                            ui: d({}, l.ui, {
                                durationEditable: !1
                            }),
                            instance: l.instance,
                            range: c
                        },
                        isStart: s.isStart && c.start.valueOf() === u.start.valueOf(),
                        isEnd: s.isEnd && c.end.valueOf() === u.end.valueOf()
                    }))
                }
                return i
            }, F.prototype.getMoreLinkText = function (e) {
                var t = this.opt("eventLimitText");
                return "function" == typeof t ? t(e) : "+" + e + " " + t
            }, F.prototype.getCellSegs = function (e, t, n) {
                for (var r, i = this.eventRenderer.rowStructs[e].segMatrix, o = n || 0, a = []; o < i.length;)(r = i[o][t]) && a.push(r), o++;
                return a
            }, F);

        function F(e, t, n) {
            var r = H.call(this, e, t) || this;
            r.bottomCoordPadding = 0, r.isCellSizesDirty = !1;
            var i = r.eventRenderer = new p(r),
                o = r.fillRenderer = new D(r);
            r.mirrorRenderer = new y(r);
            var a = r.renderCells = _.memoizeRendering(r._renderCells, r._unrenderCells);
            return r.renderBusinessHours = _.memoizeRendering(o.renderSegs.bind(o, "businessHours"), o.unrender.bind(o, "businessHours"), [a]), r.renderDateSelection = _.memoizeRendering(o.renderSegs.bind(o, "highlight"), o.unrender.bind(o, "highlight"), [a]), r.renderBgEvents = _.memoizeRendering(o.renderSegs.bind(o, "bgEvent"), o.unrender.bind(o, "bgEvent"), [a]), r.renderFgEvents = _.memoizeRendering(i.renderSegs.bind(i), i.unrender.bind(i), [a]), r.renderEventSelection = _.memoizeRendering(i.selectByInstanceId.bind(i), i.unselectByInstanceId.bind(i), [r.renderFgEvents]), r.renderEventDrag = _.memoizeRendering(r._renderEventDrag, r._unrenderEventDrag, [a]), r.renderEventResize = _.memoizeRendering(r._renderEventResize, r._unrenderEventResize, [a]), r.renderProps = n, r
        }
        var z, B = _.createFormatter({
            week: "numeric"
        }),
            j = (t(U, z = _.View), U.prototype.destroy = function () {
                z.prototype.destroy.call(this), this.dayGrid.destroy(), this.scroller.destroy()
            }, U.prototype.renderSkeletonHtml = function () {
                var e = this.theme;
                return '<table class="' + e.getClass("tableGrid") + '">' + (this.opt("columnHeader") ? '<thead class="fc-head"><tr><td class="fc-head-container ' + e.getClass("widgetHeader") + '">&nbsp;</td></tr></thead>' : "") + '<tbody class="fc-body"><tr><td class="' + e.getClass("widgetContent") + '"></td></tr></tbody></table>'
            }, U.prototype.weekNumberStyleAttr = function () {
                return null != this.weekNumberWidth ? 'style="width:' + this.weekNumberWidth + 'px"' : ""
            }, U.prototype.hasRigidRows = function () {
                var e = this.opt("eventLimit");
                return e && "number" != typeof e
            }, U.prototype.updateSize = function (e, t, n) {
                z.prototype.updateSize.call(this, e, t, n), this.dayGrid.updateSize(e)
            }, U.prototype.updateBaseSize = function (e, t, n) {
                var r, i, o = this.dayGrid,
                    a = this.opt("eventLimit"),
                    s = this.header ? this.header.el : null;
                o.rowEls ? (this.colWeekNumbersVisible && (this.weekNumberWidth = _.matchCellWidths(_.findElements(this.el, ".fc-week-number"))), this.scroller.clear(), s && _.uncompensateScroll(s), o.removeSegPopover(), a && "number" == typeof a && o.limitRows(a), r = this.computeScrollerHeight(t), this.setGridHeight(r, n), a && "number" != typeof a && o.limitRows(a), n || (this.scroller.setHeight(r), ((i = this.scroller.getScrollbarWidths()).left || i.right) && (s && _.compensateScroll(s, i), r = this.computeScrollerHeight(t), this.scroller.setHeight(r)), this.scroller.lockOverflow(i))) : n || (r = this.computeScrollerHeight(t), this.scroller.setHeight(r))
            }, U.prototype.computeScrollerHeight = function (e) {
                return e - _.subtractInnerElHeight(this.el, this.scroller.el)
            }, U.prototype.setGridHeight = function (e, t) {
                this.opt("monthMode") ? (t && (e *= this.dayGrid.rowCnt / 6), _.distributeHeight(this.dayGrid.rowEls, e, !t)) : t ? _.undistributeHeight(this.dayGrid.rowEls) : _.distributeHeight(this.dayGrid.rowEls, e, !0)
            }, U.prototype.computeDateScroll = function (e) {
                return {
                    top: 0
                }
            }, U.prototype.queryDateScroll = function () {
                return {
                    top: this.scroller.getScrollTop()
                }
            }, U.prototype.applyDateScroll = function (e) {
                void 0 !== e.top && this.scroller.setScrollTop(e.top)
            }, U);

        function U(e, t, n, r) {
            var i = z.call(this, e, t, n, r) || this;
            i.renderHeadIntroHtml = function () {
                var e = i.theme;
                return i.colWeekNumbersVisible ? '<th class="fc-week-number ' + e.getClass("widgetHeader") + '" ' + i.weekNumberStyleAttr() + "><span>" + _.htmlEscape(i.opt("weekLabel")) + "</span></th>" : ""
            }, i.renderDayGridNumberIntroHtml = function (e, t) {
                var n = i.dateEnv,
                    r = t.props.cells[e][0].date;
                return i.colWeekNumbersVisible ? '<td class="fc-week-number" ' + i.weekNumberStyleAttr() + ">" + _.buildGotoAnchorHtml(i, {
                    date: r,
                    type: "week",
                    forceOff: 1 === t.colCnt
                }, n.format(r, B)) + "</td>" : ""
            }, i.renderDayGridBgIntroHtml = function () {
                var e = i.theme;
                return i.colWeekNumbersVisible ? '<td class="fc-week-number ' + e.getClass("widgetContent") + '" ' + i.weekNumberStyleAttr() + "></td>" : ""
            }, i.renderDayGridIntroHtml = function () {
                return i.colWeekNumbersVisible ? '<td class="fc-week-number" ' + i.weekNumberStyleAttr() + "></td>" : ""
            }, i.el.classList.add("fc-dayGrid-view"), i.el.innerHTML = i.renderSkeletonHtml(), i.scroller = new _.ScrollComponent("hidden", "auto");
            var o = i.scroller.el;
            i.el.querySelector(".fc-body > tr > td").appendChild(o), o.classList.add("fc-day-grid-container");
            var a, s = _.createElement("div", {
                className: "fc-day-grid"
            });
            return o.appendChild(s), i.opt("weekNumbers") ? i.opt("weekNumbersWithinDays") ? (a = !0, i.colWeekNumbersVisible = !1) : (a = !1, i.colWeekNumbersVisible = !0) : a = i.colWeekNumbersVisible = !1, i.dayGrid = new L(i.context, s, {
                renderNumberIntroHtml: i.renderDayGridNumberIntroHtml,
                renderBgIntroHtml: i.renderDayGridBgIntroHtml,
                renderIntroHtml: i.renderDayGridIntroHtml,
                colWeekNumbersVisible: i.colWeekNumbersVisible,
                cellWeekNumbersVisible: a
            }), i
        }
        j.prototype.dateProfileGeneratorClass = n;
        var W, V = (t(G, W = _.DateComponent), G.prototype.destroy = function () {
            W.prototype.destroy.call(this), this.calendar.unregisterInteractiveComponent(this)
        }, G.prototype.render = function (e) {
            var t = this.dayGrid,
                n = e.dateProfile,
                r = e.dayTable;
            t.receiveProps(d({}, this.slicer.sliceProps(e, n, e.nextDayThreshold, t, r), {
                dateProfile: n,
                cells: r.cells,
                isRigid: e.isRigid
            }))
        }, G.prototype.buildPositionCaches = function () {
            this.dayGrid.buildPositionCaches()
        }, G.prototype.queryHit = function (e, t) {
            var n = this.dayGrid.positionToHit(e, t);
            if (n) return {
                component: this.dayGrid,
                dateSpan: n.dateSpan,
                dayEl: n.dayEl,
                rect: {
                    left: n.relativeRect.left,
                    right: n.relativeRect.right,
                    top: n.relativeRect.top,
                    bottom: n.relativeRect.bottom
                },
                layer: 0
            }
        }, G);

        function G(e, t) {
            var n = W.call(this, e, t.el) || this;
            return n.slicer = new q, n.dayGrid = t, e.calendar.registerInteractiveComponent(n, {
                el: n.dayGrid.el
            }), n
        }
        var Z, q = (t(Y, Z = _.Slicer), Y.prototype.sliceRange = function (e, t) {
            return t.sliceRange(e)
        }, Y);

        function Y() {
            return null !== Z && Z.apply(this, arguments) || this
        }
        var X, J = (t($, X = j), $.prototype.destroy = function () {
            X.prototype.destroy.call(this), this.header && this.header.destroy(), this.simpleDayGrid.destroy()
        }, $.prototype.render = function (e) {
            X.prototype.render.call(this, e);
            var t = this.props.dateProfile,
                n = this.dayTable = this.buildDayTable(t, this.dateProfileGenerator);
            this.header && this.header.receiveProps({
                dateProfile: t,
                dates: n.headerDates,
                datesRepDistinctDays: 1 === n.rowCnt,
                renderIntroHtml: this.renderHeadIntroHtml
            }), this.simpleDayGrid.receiveProps({
                dateProfile: t,
                dayTable: n,
                businessHours: e.businessHours,
                dateSelection: e.dateSelection,
                eventStore: e.eventStore,
                eventUiBases: e.eventUiBases,
                eventSelection: e.eventSelection,
                eventDrag: e.eventDrag,
                eventResize: e.eventResize,
                isRigid: this.hasRigidRows(),
                nextDayThreshold: this.nextDayThreshold
            })
        }, $);

        function $(e, t, n, r) {
            var i = X.call(this, e, t, n, r) || this;
            return i.buildDayTable = _.memoize(Q), i.opt("columnHeader") && (i.header = new _.DayHeader(i.context, i.el.querySelector(".fc-head-container"))), i.simpleDayGrid = new V(i.context, i.dayGrid), i
        }

        function Q(e, t) {
            var n = new _.DaySeries(e.renderRange, t);
            return new _.DayTable(n, /year|month|week/.test(e.currentRangeUnit))
        }
        var K = _.createPlugin({
            defaultView: "dayGridMonth",
            views: {
                dayGrid: J,
                dayGridDay: {
                    type: "dayGrid",
                    duration: {
                        days: 1
                    }
                },
                dayGridWeek: {
                    type: "dayGrid",
                    duration: {
                        weeks: 1
                    }
                },
                dayGridMonth: {
                    type: "dayGrid",
                    duration: {
                        months: 1
                    },
                    monthMode: !0,
                    fixedWeekCount: !0
                }
            }
        });
        e.AbstractDayGridView = j, e.DayBgRow = P, e.DayGrid = L, e.DayGridSlicer = q, e.DayGridView = J, e.SimpleDayGrid = V, e.buildBasicDayTable = Q, e.default = K, Object.defineProperty(e, "__esModule", {
            value: !0
        })
    }),
    function (e) {
        "function" == typeof define && define.amd ? define(["jquery"], e) : "object" == typeof exports ? e(require("jquery")) : e(window.jQuery || window.Zepto)
    }(function (c) {
        function e() { }

        function d(e, t) {
            g.ev.on("mfp" + e + S, t)
        }

        function f(e, t, n, r) {
            var i = document.createElement("div");
            return i.className = "mfp-" + e, n && (i.innerHTML = n), r ? t && t.appendChild(i) : (i = c(i), t && i.appendTo(t)), i
        }

        function p(e, t) {
            g.ev.triggerHandler("mfp" + e, t), g.st.callbacks && (e = e.charAt(0).toLowerCase() + e.slice(1), g.st.callbacks[e] && g.st.callbacks[e].apply(g, c.isArray(t) ? t : [t]))
        }

        function h(e) {
            return e === t && g.currTemplate.closeBtn || (g.currTemplate.closeBtn = c(g.st.closeMarkup.replace("%title%", g.st.tClose)), t = e), g.currTemplate.closeBtn
        }

        function o() {
            c.magnificPopup.instance || ((g = new e).init(), c.magnificPopup.instance = g)
        }
        var g, r, v, i, m, t, l = "Close",
            u = "BeforeClose",
            y = "MarkupParse",
            b = "Open",
            S = ".mfp",
            w = "mfp-ready",
            n = "mfp-removing",
            a = "mfp-prevent-close",
            s = !!window.jQuery,
            D = c(window);
        e.prototype = {
            constructor: e,
            init: function () {
                var e = navigator.appVersion;
                g.isLowIE = g.isIE8 = document.all && !document.addEventListener, g.isAndroid = /android/gi.test(e), g.isIOS = /iphone|ipad|ipod/gi.test(e), g.supportsTransition = function () {
                    var e = document.createElement("p").style,
                        t = ["ms", "O", "Moz", "Webkit"];
                    if (void 0 !== e.transition) return !0;
                    for (; t.length;)
                        if (t.pop() + "Transition" in e) return !0;
                    return !1
                }(), g.probablyMobile = g.isAndroid || g.isIOS || /(Opera Mini)|Kindle|webOS|BlackBerry|(Opera Mobi)|(Windows Phone)|IEMobile/i.test(navigator.userAgent), v = c(document), g.popupsCache = {}
            },
            open: function (e) {
                var t;
                if (!1 === e.isObj) {
                    g.items = e.items.toArray(), g.index = 0;
                    var n, r = e.items;
                    for (t = 0; t < r.length; t++)
                        if ((n = r[t]).parsed && (n = n.el[0]), n === e.el[0]) {
                            g.index = t;
                            break
                        }
                } else g.items = c.isArray(e.items) ? e.items : [e.items], g.index = e.index || 0;
                if (!g.isOpen) {
                    g.types = [], m = "", e.mainEl && e.mainEl.length ? g.ev = e.mainEl.eq(0) : g.ev = v, e.key ? (g.popupsCache[e.key] || (g.popupsCache[e.key] = {}), g.currTemplate = g.popupsCache[e.key]) : g.currTemplate = {}, g.st = c.extend(!0, {}, c.magnificPopup.defaults, e), g.fixedContentPos = "auto" === g.st.fixedContentPos ? !g.probablyMobile : g.st.fixedContentPos, g.st.modal && (g.st.closeOnContentClick = !1, g.st.closeOnBgClick = !1, g.st.showCloseBtn = !1, g.st.enableEscapeKey = !1), g.bgOverlay || (g.bgOverlay = f("bg").on("click" + S, function () {
                        g.close()
                    }), g.wrap = f("wrap").attr("tabindex", -1).on("click" + S, function (e) {
                        g._checkIfClose(e.target) && g.close()
                    }), g.container = f("container", g.wrap)), g.contentContainer = f("content"), g.st.preloader && (g.preloader = f("preloader", g.container, g.st.tLoading));
                    var i = c.magnificPopup.modules;
                    for (t = 0; t < i.length; t++) {
                        var o = i[t];
                        o = o.charAt(0).toUpperCase() + o.slice(1), g["init" + o].call(g)
                    }
                    p("BeforeOpen"), g.st.showCloseBtn && (g.st.closeBtnInside ? (d(y, function (e, t, n, r) {
                        n.close_replaceWith = h(r.type)
                    }), m += " mfp-close-btn-in") : g.wrap.append(h())), g.st.alignTop && (m += " mfp-align-top"), g.fixedContentPos ? g.wrap.css({
                        overflow: g.st.overflowY,
                        overflowX: "hidden",
                        overflowY: g.st.overflowY
                    }) : g.wrap.css({
                        top: D.scrollTop(),
                        position: "absolute"
                    }), !1 !== g.st.fixedBgPos && ("auto" !== g.st.fixedBgPos || g.fixedContentPos) || g.bgOverlay.css({
                        height: v.height(),
                        position: "absolute"
                    }), g.st.enableEscapeKey && v.on("keyup" + S, function (e) {
                        27 === e.keyCode && g.close()
                    }), D.on("resize" + S, function () {
                        g.updateSize()
                    }), g.st.closeOnContentClick || (m += " mfp-auto-cursor"), m && g.wrap.addClass(m);
                    var a = g.wH = D.height(),
                        s = {};
                    if (g.fixedContentPos && g._hasScrollBar(a)) {
                        var l = g._getScrollbarSize();
                        l && (s.marginRight = l)
                    }
                    g.fixedContentPos && (g.isIE7 ? c("body, html").css("overflow", "hidden") : s.overflow = "hidden");
                    var u = g.st.mainClass;
                    return g.isIE7 && (u += " mfp-ie7"), u && g._addClassToMFP(u), g.updateItemHTML(), p("BuildControls"), c("html").css(s), g.bgOverlay.add(g.wrap).prependTo(g.st.prependTo || c(document.body)), g._lastFocusedEl = document.activeElement, setTimeout(function () {
                        g.content ? (g._addClassToMFP(w), g._setFocus()) : g.bgOverlay.addClass(w), v.on("focusin" + S, g._onFocusIn)
                    }, 16), g.isOpen = !0, g.updateSize(a), p(b), e
                }
                g.updateItemHTML()
            },
            close: function () {
                g.isOpen && (p(u), g.isOpen = !1, g.st.removalDelay && !g.isLowIE && g.supportsTransition ? (g._addClassToMFP(n), setTimeout(function () {
                    g._close()
                }, g.st.removalDelay)) : g._close())
            },
            _close: function () {
                p(l);
                var e = n + " " + w + " ";
                if (g.bgOverlay.detach(), g.wrap.detach(), g.container.empty(), g.st.mainClass && (e += g.st.mainClass + " "), g._removeClassFromMFP(e), g.fixedContentPos) {
                    var t = {
                        marginRight: ""
                    };
                    g.isIE7 ? c("body, html").css("overflow", "") : t.overflow = "", c("html").css(t)
                }
                v.off("keyup.mfp focusin" + S), g.ev.off(S), g.wrap.attr("class", "mfp-wrap").removeAttr("style"), g.bgOverlay.attr("class", "mfp-bg"), g.container.attr("class", "mfp-container"), !g.st.showCloseBtn || g.st.closeBtnInside && !0 !== g.currTemplate[g.currItem.type] || g.currTemplate.closeBtn && g.currTemplate.closeBtn.detach(), g.st.autoFocusLast && g._lastFocusedEl && c(g._lastFocusedEl).focus(), g.currItem = null, g.content = null, g.currTemplate = null, g.prevHeight = 0, p("AfterClose")
            },
            updateSize: function (e) {
                if (g.isIOS) {
                    var t = document.documentElement.clientWidth / window.innerWidth,
                        n = window.innerHeight * t;
                    g.wrap.css("height", n), g.wH = n
                } else g.wH = e || D.height();
                g.fixedContentPos || g.wrap.css("height", g.wH), p("Resize")
            },
            updateItemHTML: function () {
                var e = g.items[g.index];
                g.contentContainer.detach(), g.content && g.content.detach(), e.parsed || (e = g.parseEl(g.index));
                var t = e.type;
                if (p("BeforeChange", [g.currItem ? g.currItem.type : "", t]), g.currItem = e, !g.currTemplate[t]) {
                    var n = !!g.st[t] && g.st[t].markup;
                    p("FirstMarkupParse", n), g.currTemplate[t] = !n || c(n)
                }
                i && i !== e.type && g.container.removeClass("mfp-" + i + "-holder");
                var r = g["get" + t.charAt(0).toUpperCase() + t.slice(1)](e, g.currTemplate[t]);
                g.appendContent(r, t), e.preloaded = !0, p("Change", e), i = e.type, g.container.prepend(g.contentContainer), p("AfterChange")
            },
            appendContent: function (e, t) {
                (g.content = e) ? g.st.showCloseBtn && g.st.closeBtnInside && !0 === g.currTemplate[t] ? g.content.find(".mfp-close").length || g.content.append(h()) : g.content = e : g.content = "", p("BeforeAppend"), g.container.addClass("mfp-" + t + "-holder"), g.contentContainer.append(g.content)
            },
            parseEl: function (e) {
                var t, n = g.items[e];
                if ((n = n.tagName ? {
                    el: c(n)
                } : (t = n.type, {
                    data: n,
                    src: n.src
                })).el) {
                    for (var r = g.types, i = 0; i < r.length; i++)
                        if (n.el.hasClass("mfp-" + r[i])) {
                            t = r[i];
                            break
                        } n.src = n.el.attr("data-mfp-src"), n.src || (n.src = n.el.attr("href"))
                }
                return n.type = t || g.st.type || "inline", n.index = e, n.parsed = !0, g.items[e] = n, p("ElementParse", n), g.items[e]
            },
            addGroup: function (t, n) {
                function e(e) {
                    e.mfpEl = this, g._openClick(e, t, n)
                }
                var r = "click.magnificPopup";
                (n = n || {}).mainEl = t, n.items ? (n.isObj = !0, t.off(r).on(r, e)) : (n.isObj = !1, n.delegate ? t.off(r).on(r, n.delegate, e) : (n.items = t).off(r).on(r, e))
            },
            _openClick: function (e, t, n) {
                if ((void 0 !== n.midClick ? n.midClick : c.magnificPopup.defaults.midClick) || !(2 === e.which || e.ctrlKey || e.metaKey || e.altKey || e.shiftKey)) {
                    var r = void 0 !== n.disableOn ? n.disableOn : c.magnificPopup.defaults.disableOn;
                    if (r)
                        if (c.isFunction(r)) {
                            if (!r.call(g)) return !0
                        } else if (D.width() < r) return !0;
                    e.type && (e.preventDefault(), g.isOpen && e.stopPropagation()), n.el = c(e.mfpEl), n.delegate && (n.items = t.find(n.delegate)), g.open(n)
                }
            },
            updateStatus: function (e, t) {
                if (g.preloader) {
                    r !== e && g.container.removeClass("mfp-s-" + r), t || "loading" !== e || (t = g.st.tLoading);
                    var n = {
                        status: e,
                        text: t
                    };
                    p("UpdateStatus", n), e = n.status, t = n.text, g.preloader.html(t), g.preloader.find("a").on("click", function (e) {
                        e.stopImmediatePropagation()
                    }), g.container.addClass("mfp-s-" + e), r = e
                }
            },
            _checkIfClose: function (e) {
                if (!c(e).hasClass(a)) {
                    var t = g.st.closeOnContentClick,
                        n = g.st.closeOnBgClick;
                    if (t && n) return !0;
                    if (!g.content || c(e).hasClass("mfp-close") || g.preloader && e === g.preloader[0]) return !0;
                    if (e === g.content[0] || c.contains(g.content[0], e)) {
                        if (t) return !0
                    } else if (n && c.contains(document, e)) return !0;
                    return !1
                }
            },
            _addClassToMFP: function (e) {
                g.bgOverlay.addClass(e), g.wrap.addClass(e)
            },
            _removeClassFromMFP: function (e) {
                this.bgOverlay.removeClass(e), g.wrap.removeClass(e)
            },
            _hasScrollBar: function (e) {
                return (g.isIE7 ? v.height() : document.body.scrollHeight) > (e || D.height())
            },
            _setFocus: function () {
                (g.st.focus ? g.content.find(g.st.focus).eq(0) : g.wrap).focus()
            },
            _onFocusIn: function (e) {
                if (e.target !== g.wrap[0] && !c.contains(g.wrap[0], e.target)) return g._setFocus(), !1
            },
            _parseMarkup: function (i, e, t) {
                var o;
                t.data && (e = c.extend(t.data, e)), p(y, [i, e, t]), c.each(e, function (e, t) {
                    if (void 0 === t || !1 === t) return !0;
                    if (1 < (o = e.split("_")).length) {
                        var n = i.find(S + "-" + o[0]);
                        if (0 < n.length) {
                            var r = o[1];
                            "replaceWith" === r ? n[0] !== t[0] && n.replaceWith(t) : "img" === r ? n.is("img") ? n.attr("src", t) : n.replaceWith(c("<img>").attr("src", t).attr("class", n.attr("class"))) : n.attr(o[1], t)
                        }
                    } else i.find(S + "-" + e).html(t)
                })
            },
            _getScrollbarSize: function () {
                if (void 0 === g.scrollbarSize) {
                    var e = document.createElement("div");
                    e.style.cssText = "width: 99px; height: 99px; overflow: scroll; position: absolute; top: -9999px;", document.body.appendChild(e), g.scrollbarSize = e.offsetWidth - e.clientWidth, document.body.removeChild(e)
                }
                return g.scrollbarSize
            }
        }, c.magnificPopup = {
            instance: null,
            proto: e.prototype,
            modules: [],
            open: function (e, t) {
                return o(), (e = e ? c.extend(!0, {}, e) : {}).isObj = !0, e.index = t || 0, this.instance.open(e)
            },
            close: function () {
                return c.magnificPopup.instance && c.magnificPopup.instance.close()
            },
            registerModule: function (e, t) {
                t.options && (c.magnificPopup.defaults[e] = t.options), c.extend(this.proto, t.proto), this.modules.push(e)
            },
            defaults: {
                disableOn: 0,
                key: null,
                midClick: !1,
                mainClass: "",
                preloader: !0,
                focus: "",
                closeOnContentClick: !1,
                closeOnBgClick: !0,
                closeBtnInside: !0,
                showCloseBtn: !0,
                enableEscapeKey: !0,
                modal: !1,
                alignTop: !1,
                removalDelay: 0,
                prependTo: null,
                fixedContentPos: "auto",
                fixedBgPos: "auto",
                overflowY: "auto",
                closeMarkup: '<button title="%title%" type="button" class="mfp-close">&#215;</button>',
                tClose: "Close (Esc)",
                tLoading: "Loading...",
                autoFocusLast: !0
            }
        }, c.fn.magnificPopup = function (e) {
            o();
            var t = c(this);
            if ("string" == typeof e)
                if ("open" === e) {
                    var n, r = s ? t.data("magnificPopup") : t[0].magnificPopup,
                        i = parseInt(arguments[1], 10) || 0;
                    n = r.items ? r.items[i] : (n = t, r.delegate && (n = n.find(r.delegate)), n.eq(i)), g._openClick({
                        mfpEl: n
                    }, t, r)
                } else g.isOpen && g[e].apply(g, Array.prototype.slice.call(arguments, 1));
            else e = c.extend(!0, {}, e), s ? t.data("magnificPopup", e) : t[0].magnificPopup = e, g.addGroup(t, e);
            return t
        };

        function T() {
            _ && (E.after(_.addClass(C)).detach(), _ = null)
        }
        var C, E, _, x = "inline";
        c.magnificPopup.registerModule(x, {
            options: {
                hiddenClass: "hide",
                markup: "",
                tNotFound: "Content not found"
            },
            proto: {
                initInline: function () {
                    g.types.push(x), d(l + "." + x, function () {
                        T()
                    })
                },
                getInline: function (e, t) {
                    if (T(), e.src) {
                        var n = g.st.inline,
                            r = c(e.src);
                        if (r.length) {
                            var i = r[0].parentNode;
                            i && i.tagName && (E || (C = n.hiddenClass, E = f(C), C = "mfp-" + C), _ = r.after(E).detach().removeClass(C)), g.updateStatus("ready")
                        } else g.updateStatus("error", n.tNotFound), r = c("<div>");
                        return e.inlineElement = r
                    }
                    return g.updateStatus("ready"), g._parseMarkup(t, {}, e), t
                }
            }
        });

        function I() {
            k && c(document.body).removeClass(k)
        }

        function R() {
            I(), g.req && g.req.abort()
        }
        var k, P = "ajax";
        c.magnificPopup.registerModule(P, {
            options: {
                settings: null,
                cursor: "mfp-ajax-cur",
                tError: '<a href="%url%">The content</a> could not be loaded.'
            },
            proto: {
                initAjax: function () {
                    g.types.push(P), k = g.st.ajax.cursor, d(l + "." + P, R), d("BeforeChange." + P, R)
                },
                getAjax: function (i) {
                    k && c(document.body).addClass(k), g.updateStatus("loading");
                    var e = c.extend({
                        url: i.src,
                        success: function (e, t, n) {
                            var r = {
                                data: e,
                                xhr: n
                            };
                            p("ParseAjax", r), g.appendContent(c(r.data), P), i.finished = !0, I(), g._setFocus(), setTimeout(function () {
                                g.wrap.addClass(w)
                            }, 16), g.updateStatus("ready"), p("AjaxContentAdded")
                        },
                        error: function () {
                            I(), i.finished = i.loadError = !0, g.updateStatus("error", g.st.ajax.tError.replace("%url%", i.src))
                        }
                    }, g.st.ajax.settings);
                    return g.req = c.ajax(e), ""
                }
            }
        });
        var M;
        c.magnificPopup.registerModule("image", {
            options: {
                markup: '<div class="mfp-figure"><div class="mfp-close"></div><figure><div class="mfp-img"></div><figcaption><div class="mfp-bottom-bar"><div class="mfp-title"></div><div class="mfp-counter"></div></div></figcaption></figure></div>',
                cursor: "mfp-zoom-out-cur",
                titleSrc: "title",
                verticalFit: !0,
                tError: '<a href="%url%">The image</a> could not be loaded.'
            },
            proto: {
                initImage: function () {
                    var e = g.st.image,
                        t = ".image";
                    g.types.push("image"), d(b + t, function () {
                        "image" === g.currItem.type && e.cursor && c(document.body).addClass(e.cursor)
                    }), d(l + t, function () {
                        e.cursor && c(document.body).removeClass(e.cursor), D.off("resize" + S)
                    }), d("Resize" + t, g.resizeImage), g.isLowIE && d("AfterChange", g.resizeImage)
                },
                resizeImage: function () {
                    var e = g.currItem;
                    if (e && e.img && g.st.image.verticalFit) {
                        var t = 0;
                        g.isLowIE && (t = parseInt(e.img.css("padding-top"), 10) + parseInt(e.img.css("padding-bottom"), 10)), e.img.css("max-height", g.wH - t)
                    }
                },
                _onImageHasSize: function (e) {
                    e.img && (e.hasSize = !0, M && clearInterval(M), e.isCheckingImgSize = !1, p("ImageHasSize", e), e.imgHidden && (g.content && g.content.removeClass("mfp-loading"), e.imgHidden = !1))
                },
                findImageSize: function (t) {
                    var n = 0,
                        r = t.img[0],
                        i = function (e) {
                            M && clearInterval(M), M = setInterval(function () {
                                0 < r.naturalWidth ? g._onImageHasSize(t) : (200 < n && clearInterval(M), 3 === ++n ? i(10) : 40 === n ? i(50) : 100 === n && i(500))
                            }, e)
                        };
                    i(1)
                },
                getImage: function (e, t) {
                    var n = 0,
                        r = function () {
                            e && (e.img[0].complete ? (e.img.off(".mfploader"), e === g.currItem && (g._onImageHasSize(e), g.updateStatus("ready")), e.hasSize = !0, e.loaded = !0, p("ImageLoadComplete")) : ++n < 200 ? setTimeout(r, 100) : i())
                        },
                        i = function () {
                            e && (e.img.off(".mfploader"), e === g.currItem && (g._onImageHasSize(e), g.updateStatus("error", o.tError.replace("%url%", e.src))), e.hasSize = !0, e.loaded = !0, e.loadError = !0)
                        },
                        o = g.st.image,
                        a = t.find(".mfp-img");
                    if (a.length) {
                        var s = document.createElement("img");
                        s.className = "mfp-img", e.el && e.el.find("img").length && (s.alt = e.el.find("img").attr("alt")), e.img = c(s).on("load.mfploader", r).on("error.mfploader", i), s.src = e.src, a.is("img") && (e.img = e.img.clone()), 0 < (s = e.img[0]).naturalWidth ? e.hasSize = !0 : s.width || (e.hasSize = !1)
                    }
                    return g._parseMarkup(t, {
                        title: function (e) {
                            if (e.data && void 0 !== e.data.title) return e.data.title;
                            var t = g.st.image.titleSrc;
                            if (t) {
                                if (c.isFunction(t)) return t.call(g, e);
                                if (e.el) return e.el.attr(t) || ""
                            }
                            return ""
                        }(e),
                        img_replaceWith: e.img
                    }, e), g.resizeImage(), e.hasSize ? (M && clearInterval(M), e.loadError ? (t.addClass("mfp-loading"), g.updateStatus("error", o.tError.replace("%url%", e.src))) : (t.removeClass("mfp-loading"), g.updateStatus("ready"))) : (g.updateStatus("loading"), e.loading = !0, e.hasSize || (e.imgHidden = !0, t.addClass("mfp-loading"), g.findImageSize(e))), t
                }
            }
        });
        var O;
        c.magnificPopup.registerModule("zoom", {
            options: {
                enabled: !1,
                easing: "ease-in-out",
                duration: 300,
                opener: function (e) {
                    return e.is("img") ? e : e.find("img")
                }
            },
            proto: {
                initZoom: function () {
                    var e, o = g.st.zoom,
                        t = ".zoom";
                    if (o.enabled && g.supportsTransition) {
                        function n(e) {
                            var t = e.clone().removeAttr("style").removeAttr("class").addClass("mfp-animated-image"),
                                n = "all " + o.duration / 1e3 + "s " + o.easing,
                                r = {
                                    position: "fixed",
                                    zIndex: 9999,
                                    left: 0,
                                    top: 0,
                                    "-webkit-backface-visibility": "hidden"
                                },
                                i = "transition";
                            return r["-webkit-" + i] = r["-moz-" + i] = r["-o-" + i] = r[i] = n, t.css(r), t
                        }

                        function r() {
                            g.content.css("visibility", "visible")
                        }
                        var i, a, s = o.duration;
                        d("BuildControls" + t, function () {
                            if (g._allowZoom()) {
                                if (clearTimeout(i), g.content.css("visibility", "hidden"), !(e = g._getItemToZoom())) return void r();
                                (a = n(e)).css(g._getOffset()), g.wrap.append(a), i = setTimeout(function () {
                                    a.css(g._getOffset(!0)), i = setTimeout(function () {
                                        r(), setTimeout(function () {
                                            a.remove(), e = a = null, p("ZoomAnimationEnded")
                                        }, 16)
                                    }, s)
                                }, 16)
                            }
                        }), d(u + t, function () {
                            if (g._allowZoom()) {
                                if (clearTimeout(i), g.st.removalDelay = s, !e) {
                                    if (!(e = g._getItemToZoom())) return;
                                    a = n(e)
                                }
                                a.css(g._getOffset(!0)), g.wrap.append(a), g.content.css("visibility", "hidden"), setTimeout(function () {
                                    a.css(g._getOffset())
                                }, 16)
                            }
                        }), d(l + t, function () {
                            g._allowZoom() && (r(), a && a.remove(), e = null)
                        })
                    }
                },
                _allowZoom: function () {
                    return "image" === g.currItem.type
                },
                _getItemToZoom: function () {
                    return !!g.currItem.hasSize && g.currItem.img
                },
                _getOffset: function (e) {
                    var t, n = (t = e ? g.currItem.img : g.st.zoom.opener(g.currItem.el || g.currItem)).offset(),
                        r = parseInt(t.css("padding-top"), 10),
                        i = parseInt(t.css("padding-bottom"), 10);
                    n.top -= c(window).scrollTop() - r;
                    var o = {
                        width: t.width(),
                        height: (s ? t.innerHeight() : t[0].offsetHeight) - i - r
                    };
                    return void 0 === O && (O = void 0 !== document.createElement("p").style.MozTransform), O ? o["-moz-transform"] = o.transform = "translate(" + n.left + "px," + n.top + "px)" : (o.left = n.left, o.top = n.top), o
                }
            }
        });

        function H(e) {
            if (g.currTemplate[A]) {
                var t = g.currTemplate[A].find("iframe");
                t.length && (e || (t[0].src = "//about:blank"), g.isIE8 && t.css("display", e ? "block" : "none"))
            }
        }
        var A = "iframe";
        c.magnificPopup.registerModule(A, {
            options: {
                markup: '<div class="mfp-iframe-scaler"><div class="mfp-close"></div><iframe class="mfp-iframe" src="//about:blank" frameborder="0" allowfullscreen></iframe></div>',
                srcAction: "iframe_src",
                patterns: {
                    youtube: {
                        index: "youtube.com",
                        id: "v=",
                        src: "//www.youtube.com/embed/%id%?autoplay=1"
                    },
                    vimeo: {
                        index: "vimeo.com/",
                        id: "/",
                        src: "//player.vimeo.com/video/%id%?autoplay=1"
                    },
                    gmaps: {
                        index: "//maps.google.",
                        src: "%id%&output=embed"
                    }
                }
            },
            proto: {
                initIframe: function () {
                    g.types.push(A), d("BeforeChange", function (e, t, n) {
                        t !== n && (t === A ? H() : n === A && H(!0))
                    }), d(l + "." + A, function () {
                        H()
                    })
                },
                getIframe: function (e, t) {
                    var n = e.src,
                        r = g.st.iframe;
                    c.each(r.patterns, function () {
                        if (-1 < n.indexOf(this.index)) return this.id && (n = "string" == typeof this.id ? n.substr(n.lastIndexOf(this.id) + this.id.length, n.length) : this.id.call(this, n)), n = this.src.replace("%id%", n), !1
                    });
                    var i = {};
                    return r.srcAction && (i[r.srcAction] = n), g._parseMarkup(t, i, e), g.updateStatus("ready"), t
                }
            }
        });

        function N(e) {
            var t = g.items.length;
            return t - 1 < e ? e - t : e < 0 ? t + e : e
        }

        function L(e, t, n) {
            return e.replace(/%curr%/gi, t + 1).replace(/%total%/gi, n)
        }
        c.magnificPopup.registerModule("gallery", {
            options: {
                enabled: !1,
                arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"></button>',
                preload: [0, 2],
                navigateByImgClick: !0,
                arrows: !0,
                tPrev: "Previous (Left arrow key)",
                tNext: "Next (Right arrow key)",
                tCounter: "%curr% of %total%"
            },
            proto: {
                initGallery: function () {
                    var o = g.st.gallery,
                        e = ".mfp-gallery";
                    if (g.direction = !0, !o || !o.enabled) return !1;
                    m += " mfp-gallery", d(b + e, function () {
                        o.navigateByImgClick && g.wrap.on("click" + e, ".mfp-img", function () {
                            if (1 < g.items.length) return g.next(), !1
                        }), v.on("keydown" + e, function (e) {
                            37 === e.keyCode ? g.prev() : 39 === e.keyCode && g.next()
                        })
                    }), d("UpdateStatus" + e, function (e, t) {
                        t.text && (t.text = L(t.text, g.currItem.index, g.items.length))
                    }), d(y + e, function (e, t, n, r) {
                        var i = g.items.length;
                        n.counter = 1 < i ? L(o.tCounter, r.index, i) : ""
                    }), d("BuildControls" + e, function () {
                        if (1 < g.items.length && o.arrows && !g.arrowLeft) {
                            var e = o.arrowMarkup,
                                t = g.arrowLeft = c(e.replace(/%title%/gi, o.tPrev).replace(/%dir%/gi, "left")).addClass(a),
                                n = g.arrowRight = c(e.replace(/%title%/gi, o.tNext).replace(/%dir%/gi, "right")).addClass(a);
                            t.click(function () {
                                g.prev()
                            }), n.click(function () {
                                g.next()
                            }), g.container.append(t.add(n))
                        }
                    }), d("Change" + e, function () {
                        g._preloadTimeout && clearTimeout(g._preloadTimeout), g._preloadTimeout = setTimeout(function () {
                            g.preloadNearbyImages(), g._preloadTimeout = null
                        }, 16)
                    }), d(l + e, function () {
                        v.off(e), g.wrap.off("click" + e), g.arrowRight = g.arrowLeft = null
                    })
                },
                next: function () {
                    g.direction = !0, g.index = N(g.index + 1), g.updateItemHTML()
                },
                prev: function () {
                    g.direction = !1, g.index = N(g.index - 1), g.updateItemHTML()
                },
                goTo: function (e) {
                    g.direction = e >= g.index, g.index = e, g.updateItemHTML()
                },
                preloadNearbyImages: function () {
                    var e, t = g.st.gallery.preload,
                        n = Math.min(t[0], g.items.length),
                        r = Math.min(t[1], g.items.length);
                    for (e = 1; e <= (g.direction ? r : n); e++) g._preloadItem(g.index + e);
                    for (e = 1; e <= (g.direction ? n : r); e++) g._preloadItem(g.index - e)
                },
                _preloadItem: function (e) {
                    if (e = N(e), !g.items[e].preloaded) {
                        var t = g.items[e];
                        t.parsed || (t = g.parseEl(e)), p("LazyLoad", t), "image" === t.type && (t.img = c('<img class="mfp-img" />').on("load.mfploader", function () {
                            t.hasSize = !0
                        }).on("error.mfploader", function () {
                            t.hasSize = !0, t.loadError = !0, p("LazyLoadError", t)
                        }).attr("src", t.src)), t.preloaded = !0
                    }
                }
            }
        });
        var F = "retina";
        c.magnificPopup.registerModule(F, {
            options: {
                replaceSrc: function (e) {
                    return e.src.replace(/\.\w+$/, function (e) {
                        return "@2x" + e
                    })
                },
                ratio: 1
            },
            proto: {
                initRetina: function () {
                    if (1 < window.devicePixelRatio) {
                        var n = g.st.retina,
                            r = n.ratio;
                        1 < (r = isNaN(r) ? r() : r) && (d("ImageHasSize." + F, function (e, t) {
                            t.img.css({
                                "max-width": t.img[0].naturalWidth / r,
                                width: "100%"
                            })
                        }), d("ElementParse." + F, function (e, t) {
                            t.src = n.replaceSrc(t, r)
                        }))
                    }
                }
            }
        }), o()
    }),
    function (i) {
        var t = {
            init: function (e) {
                var r = i.extend({
                    onHideAlert: null
                }, e);
                return this.each(function (e, t) {
                    var n = i(t);
                    n.on("click", ".button--hide-alert", function (e) {
                        e.preventDefault(), i.isFunction(r.onHideAlert) && r.onHideAlert(n), n.remove()
                    })
                })
            },
            destroy: function () {
                return this.each(function (e, t) {
                    i(t).off("click", ".button--hide-alert")
                })
            }
        };
        i.fn.BINUS_Alert_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void i.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, i(document).ready(function () {
            i(".C--alert.type--1.-autoload").BINUS_Alert_One()
        })
    }(jQuery);
var $jscomp = $jscomp || {};
$jscomp.scope = {}, $jscomp.findInternal = function (e, t, n) {
    e instanceof String && (e = String(e));
    for (var r = e.length, i = 0; i < r; i++) {
        var o = e[i];
        if (t.call(n, o, i, e)) return {
            i: i,
            v: o
        }
    }
    return {
        i: -1,
        v: void 0
    }
}, $jscomp.ASSUME_ES5 = !1, $jscomp.ASSUME_NO_NATIVE_MAP = !1, $jscomp.ASSUME_NO_NATIVE_SET = !1, $jscomp.SIMPLE_FROUND_POLYFILL = !1, $jscomp.defineProperty = $jscomp.ASSUME_ES5 || "function" == typeof Object.defineProperties ? Object.defineProperty : function (e, t, n) {
    e != Array.prototype && e != Object.prototype && (e[t] = n.value)
}, $jscomp.getGlobal = function (e) {
    return "undefined" != typeof window && window === e ? e : "undefined" != typeof global && null != global ? global : e
}, $jscomp.global = $jscomp.getGlobal(this), $jscomp.polyfill = function (e, t, n, r) {
    if (t) {
        for (n = $jscomp.global, e = e.split("."), r = 0; r < e.length - 1; r++) {
            var i = e[r];
            i in n || (n[i] = {}), n = n[i]
        } (t = t(r = n[e = e[e.length - 1]])) != r && null != t && $jscomp.defineProperty(n, e, {
            configurable: !0,
            writable: !0,
            value: t
        })
    }
}, $jscomp.polyfill("Array.prototype.find", function (e) {
    return e || function (e, t) {
        return $jscomp.findInternal(this, e, t).v
    }
}, "es6", "es3"),
    function (n) {
        "function" == typeof define && define.amd ? define(["jquery"], function (e) {
            return n(e, window, document)
        }) : "object" == typeof exports ? module.exports = function (e, t) {
            return e = e || window, t = t || ("undefined" != typeof window ? require("jquery") : require("jquery")(e)), n(t, e, e.document)
        } : n(jQuery, window, document)
    }(function (M, m, b, O) {
        function a(n) {
            var r, i, o = {};
            M.each(n, function (e, t) {
                (r = e.match(/^([^A-Z]+?)([A-Z])/)) && -1 !== "a aa ai ao as b fn i m o s ".indexOf(r[1] + " ") && (i = e.replace(r[0], r[2].toLowerCase()), o[i] = e, "o" === r[1] && a(n[e]))
            }), n._hungarianMap = o
        }

        function S(n, r, i) {
            var o;
            n._hungarianMap || a(n), M.each(r, function (e, t) {
                (o = n._hungarianMap[e]) === O || !i && r[o] !== O || ("o" === o.charAt(0) ? (r[o] || (r[o] = {}), M.extend(!0, r[o], r[e]), S(n[o], r[o], i)) : r[o] = r[e])
            })
        }

        function w(e) {
            var t = Ke.defaults.oLanguage,
                n = t.sDecimal;
            if (n && je(n), e) {
                var r = e.sZeroRecords;
                !e.sEmptyTable && r && "No data available in table" === t.sEmptyTable && Me(e, e, "sZeroRecords", "sEmptyTable"), !e.sLoadingRecords && r && "Loading..." === t.sLoadingRecords && Me(e, e, "sZeroRecords", "sLoadingRecords"), e.sInfoThousands && (e.sThousands = e.sInfoThousands), (e = e.sDecimal) && n !== e && je(e)
            }
        }

        function D(e) {
            if (lt(e, "ordering", "bSort"), lt(e, "orderMulti", "bSortMulti"), lt(e, "orderClasses", "bSortClasses"), lt(e, "orderCellsTop", "bSortCellsTop"), lt(e, "order", "aaSorting"), lt(e, "orderFixed", "aaSortingFixed"), lt(e, "paging", "bPaginate"), lt(e, "pagingType", "sPaginationType"), lt(e, "pageLength", "iDisplayLength"), lt(e, "searching", "bFilter"), "boolean" == typeof e.sScrollX && (e.sScrollX = e.sScrollX ? "100%" : ""), "boolean" == typeof e.scrollX && (e.scrollX = e.scrollX ? "100%" : ""), e = e.aoSearchCols)
                for (var t = 0, n = e.length; t < n; t++) e[t] && S(Ke.models.oSearch, e[t])
        }

        function T(e) {
            lt(e, "orderable", "bSortable"), lt(e, "orderData", "aDataSort"), lt(e, "orderSequence", "asSorting"), lt(e, "orderDataType", "sortDataType");
            var t = e.aDataSort;
            "number" != typeof t || M.isArray(t) || (e.aDataSort = [t])
        }

        function C(e) {
            if (!Ke.__browser) {
                var t = {};
                Ke.__browser = t;
                var n = M("<div/>").css({
                    position: "fixed",
                    top: 0,
                    left: -1 * M(m).scrollLeft(),
                    height: 1,
                    width: 1,
                    overflow: "hidden"
                }).append(M("<div/>").css({
                    position: "absolute",
                    top: 1,
                    left: 1,
                    width: 100,
                    overflow: "scroll"
                }).append(M("<div/>").css({
                    width: "100%",
                    height: 10
                }))).appendTo("body"),
                    r = n.children(),
                    i = r.children();
                t.barWidth = r[0].offsetWidth - r[0].clientWidth, t.bScrollOversize = 100 === i[0].offsetWidth && 100 !== r[0].clientWidth, t.bScrollbarLeft = 1 !== Math.round(i.offset().left), t.bBounding = !!n[0].getBoundingClientRect().width, n.remove()
            }
            M.extend(e.oBrowser, Ke.__browser), e.oScroll.iBarWidth = Ke.__browser.barWidth
        }

        function n(e, t, n, r, i, o) {
            var a = !1;
            if (n !== O) {
                var s = n;
                a = !0
            }
            for (; r !== i;) e.hasOwnProperty(r) && (s = a ? t(s, e[r], r, e) : e[r], a = !0, r += o);
            return s
        }

        function E(e, t) {
            var n = Ke.defaults.column,
                r = e.aoColumns.length;
            n = M.extend({}, Ke.models.oColumn, n, {
                nTh: t || b.createElement("th"),
                sTitle: n.sTitle ? n.sTitle : t ? t.innerHTML : "",
                aDataSort: n.aDataSort ? n.aDataSort : [r],
                mData: n.mData ? n.mData : r,
                idx: r
            }), e.aoColumns.push(n), (n = e.aoPreSearchCols)[r] = M.extend({}, Ke.models.oSearch, n[r]), _(e, r, M(t).data())
        }

        function _(e, t, n) {
            t = e.aoColumns[t];
            var r = e.oClasses,
                i = M(t.nTh);
            if (!t.sWidthOrig) {
                t.sWidthOrig = i.attr("width") || null;
                var o = (i.attr("style") || "").match(/width:\s*(\d+[pxem%]+)/);
                o && (t.sWidthOrig = o[1])
            }
            n !== O && null !== n && (T(n), S(Ke.defaults.column, n, !0), n.mDataProp === O || n.mData || (n.mData = n.mDataProp), n.sType && (t._sManualType = n.sType), n.className && !n.sClass && (n.sClass = n.className), n.sClass && i.addClass(n.sClass), M.extend(t, n), Me(t, n, "sWidth", "sWidthOrig"), n.iDataSort !== O && (t.aDataSort = [n.iDataSort]), Me(t, n, "aDataSort"));
            var a = t.mData,
                s = P(a),
                l = t.mRender ? P(t.mRender) : null;
            n = function (e) {
                return "string" == typeof e && -1 !== e.indexOf("@")
            }, t._bAttrSrc = M.isPlainObject(a) && (n(a.sort) || n(a.type) || n(a.filter)), t._setter = null, t.fnGetData = function (e, t, n) {
                var r = s(e, t, O, n);
                return l && t ? l(r, t, e, n) : r
            }, t.fnSetData = function (e, t, n) {
                return h(a)(e, t, n)
            }, "number" != typeof a && (e._rowReadObject = !0), e.oFeatures.bSort || (t.bSortable = !1, i.addClass(r.sSortableNone)), e = -1 !== M.inArray("asc", t.asSorting), n = -1 !== M.inArray("desc", t.asSorting), t.bSortable && (e || n) ? e && !n ? (t.sSortingClass = r.sSortableAsc, t.sSortingClassJUI = r.sSortJUIAscAllowed) : !e && n ? (t.sSortingClass = r.sSortableDesc, t.sSortingClassJUI = r.sSortJUIDescAllowed) : (t.sSortingClass = r.sSortable, t.sSortingClassJUI = r.sSortJUI) : (t.sSortingClass = r.sSortableNone, t.sSortingClassJUI = "")
        }

        function H(e) {
            if (!1 !== e.oFeatures.bAutoWidth) {
                var t = e.aoColumns;
                ve(e);
                for (var n = 0, r = t.length; n < r; n++) t[n].nTh.style.width = t[n].sWidth
            }
            "" === (t = e.oScroll).sY && "" === t.sX || he(e), Ne(e, null, "column-sizing", [e])
        }

        function A(e, t) {
            return "number" == typeof (e = x(e, "bVisible"))[t] ? e[t] : null
        }

        function u(e, t) {
            return e = x(e, "bVisible"), -1 !== (t = M.inArray(t, e)) ? t : null
        }

        function y(e) {
            var n = 0;
            return M.each(e.aoColumns, function (e, t) {
                t.bVisible && "none" !== M(t.nTh).css("display") && n++
            }), n
        }

        function x(e, n) {
            var r = [];
            return M.map(e.aoColumns, function (e, t) {
                e[n] && r.push(t)
            }), r
        }

        function s(e) {
            var t, n, r, i = e.aoColumns,
                o = e.aoData,
                a = Ke.ext.type.detect,
                s = 0;
            for (t = i.length; s < t; s++) {
                var l = i[s],
                    u = [];
                if (!l.sType && l._sManualType) l.sType = l._sManualType;
                else if (!l.sType) {
                    var c = 0;
                    for (n = a.length; c < n; c++) {
                        var d = 0;
                        for (r = o.length; d < r; d++) {
                            u[d] === O && (u[d] = g(e, d, s, "type"));
                            var f = a[c](u[d], e);
                            if (!f && c !== a.length - 1) break;
                            if ("html" === f) break
                        }
                        if (f) {
                            l.sType = f;
                            break
                        }
                    }
                    l.sType || (l.sType = "string")
                }
            }
        }

        function I(e, t, n, r) {
            var i, o, a, s = e.aoColumns;
            if (t)
                for (i = t.length - 1; 0 <= i; i--) {
                    var l = t[i],
                        u = l.targets !== O ? l.targets : l.aTargets;
                    M.isArray(u) || (u = [u]);
                    var c = 0;
                    for (o = u.length; c < o; c++)
                        if ("number" == typeof u[c] && 0 <= u[c]) {
                            for (; s.length <= u[c];) E(e);
                            r(u[c], l)
                        } else if ("number" == typeof u[c] && u[c] < 0) r(s.length + u[c], l);
                        else if ("string" == typeof u[c]) {
                            var d = 0;
                            for (a = s.length; d < a; d++) "_all" != u[c] && !M(s[d].nTh).hasClass(u[c]) || r(d, l)
                        }
                }
            if (n)
                for (i = 0, e = n.length; i < e; i++) r(i, n[i])
        }

        function R(e, t, n, r) {
            var i = e.aoData.length,
                o = M.extend(!0, {}, Ke.models.oRow, {
                    src: n ? "dom" : "data",
                    idx: i
                });
            o._aData = t, e.aoData.push(o);
            for (var a = e.aoColumns, s = 0, l = a.length; s < l; s++) a[s].sType = null;
            return e.aiDisplayMaster.push(i), (t = e.rowIdFn(t)) !== O && (e.aIds[t] = o), !n && e.oFeatures.bDeferRender || N(e, i, n, r), i
        }

        function k(n, e) {
            var r;
            return e instanceof M || (e = M(e)), e.map(function (e, t) {
                return r = f(n, t), R(n, r.data, t, r.cells)
            })
        }

        function g(e, t, n, r) {
            var i = e.iDraw,
                o = e.aoColumns[n],
                a = e.aoData[t]._aData,
                s = o.sDefaultContent,
                l = o.fnGetData(a, r, {
                    settings: e,
                    row: t,
                    col: n
                });
            if (l === O) return e.iDrawError != i && null === s && (Pe(e, 0, "Requested unknown parameter " + ("function" == typeof o.mData ? "{function}" : "'" + o.mData + "'") + " for row " + t + ", column " + n, 4), e.iDrawError = i), s;
            if (l !== a && null !== l || null === s || r === O) {
                if ("function" == typeof l) return l.call(a)
            } else l = s;
            return null === l && "display" == r ? "" : l
        }

        function r(e, t, n, r) {
            e.aoColumns[n].fnSetData(e.aoData[t]._aData, r, {
                settings: e,
                row: t,
                col: n
            })
        }

        function c(e) {
            return M.map(e.match(/(\\.|[^\.])+/g) || [""], function (e) {
                return e.replace(/\\\./g, ".")
            })
        }

        function P(i) {
            if (M.isPlainObject(i)) {
                var o = {};
                return M.each(i, function (e, t) {
                    t && (o[e] = P(t))
                }),
                    function (e, t, n, r) {
                        var i = o[t] || o._;
                        return i !== O ? i(e, t, n, r) : e
                    }
            }
            if (null === i) return function (e) {
                return e
            };
            if ("function" == typeof i) return function (e, t, n, r) {
                return i(e, t, n, r)
            };
            if ("string" != typeof i || -1 === i.indexOf(".") && -1 === i.indexOf("[") && -1 === i.indexOf("(")) return function (e, t) {
                return e[i]
            };
            var s = function (e, t, n) {
                if ("" !== n)
                    for (var r = c(n), i = 0, o = r.length; i < o; i++) {
                        n = r[i].match(ut);
                        var a = r[i].match(ct);
                        if (n) {
                            if (r[i] = r[i].replace(ut, ""), "" !== r[i] && (e = e[r[i]]), a = [], r.splice(0, i + 1), r = r.join("."), M.isArray(e))
                                for (i = 0, o = e.length; i < o; i++) a.push(s(e[i], t, r));
                            e = "" === (e = n[0].substring(1, n[0].length - 1)) ? a : a.join(e);
                            break
                        }
                        if (a) r[i] = r[i].replace(ct, ""), e = e[r[i]]();
                        else {
                            if (null === e || e[r[i]] === O) return O;
                            e = e[r[i]]
                        }
                    }
                return e
            };
            return function (e, t) {
                return s(e, t, i)
            }
        }

        function h(r) {
            if (M.isPlainObject(r)) return h(r._);
            if (null === r) return function () { };
            if ("function" == typeof r) return function (e, t, n) {
                r(e, "set", t, n)
            };
            if ("string" != typeof r || -1 === r.indexOf(".") && -1 === r.indexOf("[") && -1 === r.indexOf("(")) return function (e, t) {
                e[r] = t
            };
            var l = function (e, t, n) {
                for (var r, i, o = (n = c(n))[n.length - 1], a = 0, s = n.length - 1; a < s; a++) {
                    if (r = n[a].match(ut), i = n[a].match(ct), r) {
                        if (n[a] = n[a].replace(ut, ""), e[n[a]] = [], (o = n.slice()).splice(0, a + 1), r = o.join("."), M.isArray(t))
                            for (i = 0, s = t.length; i < s; i++) l(o = {}, t[i], r), e[n[a]].push(o);
                        else e[n[a]] = t;
                        return
                    }
                    i && (n[a] = n[a].replace(ct, ""), e = e[n[a]](t)), null !== e[n[a]] && e[n[a]] !== O || (e[n[a]] = {}), e = e[n[a]]
                }
                o.match(ct) ? e[o.replace(ct, "")](t) : e[o.replace(ut, "")] = t
            };
            return function (e, t) {
                return l(e, t, r)
            }
        }

        function v(e) {
            return at(e.aoData, "_aData")
        }

        function l(e) {
            e.aoData.length = 0, e.aiDisplayMaster.length = 0, e.aiDisplay.length = 0, e.aIds = {}
        }

        function d(e, t, n) {
            for (var r = -1, i = 0, o = e.length; i < o; i++) e[i] == t ? r = i : e[i] > t && e[i]--; - 1 != r && n === O && e.splice(r, 1)
        }

        function i(n, r, e, t) {
            var i, o = n.aoData[r],
                a = function (e, t) {
                    for (; e.childNodes.length;) e.removeChild(e.firstChild);
                    e.innerHTML = g(n, r, t, "display")
                };
            if ("dom" !== e && (e && "auto" !== e || "dom" !== o.src)) {
                var s = o.anCells;
                if (s)
                    if (t !== O) a(s[t], t);
                    else
                        for (e = 0, i = s.length; e < i; e++) a(s[e], e)
            } else o._aData = f(n, o, t, t === O ? O : o._aData).data;
            if (o._aSortData = null, o._aFilterData = null, a = n.aoColumns, t !== O) a[t].sType = null;
            else {
                for (e = 0, i = a.length; e < i; e++) a[e].sType = null;
                p(n, o)
            }
        }

        function f(e, t, n, r) {
            var i, o, a = [],
                s = t.firstChild,
                l = 0,
                u = e.aoColumns,
                c = e._rowReadObject;
            r = r !== O ? r : c ? {} : [];

            function d(e, t) {
                if ("string" == typeof e) {
                    var n = e.indexOf("@"); - 1 !== n && (n = e.substring(n + 1), h(e)(r, t.getAttribute(n)))
                }
            }

            function f(e) {
                n !== O && n !== l || (i = u[l], o = M.trim(e.innerHTML), i && i._bAttrSrc ? (h(i.mData._)(r, o), d(i.mData.sort, e), d(i.mData.type, e), d(i.mData.filter, e)) : c ? (i._setter || (i._setter = h(i.mData)), i._setter(r, o)) : r[l] = o), l++
            }
            if (s)
                for (; s;) {
                    var p = s.nodeName.toUpperCase();
                    "TD" != p && "TH" != p || (f(s), a.push(s)), s = s.nextSibling
                } else
                for (s = 0, p = (a = t.anCells).length; s < p; s++) f(a[s]);
            return (t = t.firstChild ? t : t.nTr) && (t = t.getAttribute("id")) && h(e.rowId)(r, t), {
                data: r,
                cells: a
            }
        }

        function N(e, t, n, r) {
            var i, o, a = e.aoData[t],
                s = a._aData,
                l = [];
            if (null === a.nTr) {
                var u = n || b.createElement("tr");
                a.nTr = u, a.anCells = l, u._DT_RowIndex = t, p(e, a);
                var c = 0;
                for (i = e.aoColumns.length; c < i; c++) {
                    var d = e.aoColumns[c],
                        f = (o = !n) ? b.createElement(d.sCellType) : r[c];
                    f._DT_CellIndex = {
                        row: t,
                        column: c
                    }, l.push(f), !o && (n && !d.mRender && d.mData === c || M.isPlainObject(d.mData) && d.mData._ === c + ".display") || (f.innerHTML = g(e, t, c, "display")), d.sClass && (f.className += " " + d.sClass), d.bVisible && !n ? u.appendChild(f) : !d.bVisible && n && f.parentNode.removeChild(f), d.fnCreatedCell && d.fnCreatedCell.call(e.oInstance, f, g(e, t, c), s, t, c)
                }
                Ne(e, "aoRowCreatedCallback", null, [u, s, t, l])
            }
            a.nTr.setAttribute("role", "row")
        }

        function p(e, t) {
            var n = t.nTr,
                r = t._aData;
            n && ((e = e.rowIdFn(r)) && (n.id = e), r.DT_RowClass && (e = r.DT_RowClass.split(" "), t.__rowc = t.__rowc ? st(t.__rowc.concat(e)) : e, M(n).removeClass(t.__rowc.join(" ")).addClass(r.DT_RowClass)), r.DT_RowAttr && M(n).attr(r.DT_RowAttr), r.DT_RowData && M(n).data(r.DT_RowData))
        }

        function L(e) {
            var t, n, r = e.nTHead,
                i = e.nTFoot,
                o = 0 === M("th, td", r).length,
                a = e.oClasses,
                s = e.aoColumns;
            o && (n = M("<tr/>").appendTo(r));
            var l = 0;
            for (t = s.length; l < t; l++) {
                var u = s[l],
                    c = M(u.nTh).addClass(u.sClass);
                o && c.appendTo(n), e.oFeatures.bSort && (c.addClass(u.sSortingClass), !1 !== u.bSortable && (c.attr("tabindex", e.iTabIndex).attr("aria-controls", e.sTableId), Ee(e, u.nTh, l))), u.sTitle != c[0].innerHTML && c.html(u.sTitle), Fe(e, "header")(e, c, u, a)
            }
            if (o && U(e.aoHeader, r), M(r).find(">tr").attr("role", "row"), M(r).find(">tr>th, >tr>td").addClass(a.sHeaderTH), M(i).find(">tr>th, >tr>td").addClass(a.sFooterTH), null !== i)
                for (l = 0, t = (e = e.aoFooter[0]).length; l < t; l++)(u = s[l]).nTf = e[l].cell, u.sClass && M(u.nTf).addClass(u.sClass)
        }

        function F(e, t, n) {
            var r, i, o = [],
                a = [],
                s = e.aoColumns.length;
            if (t) {
                n === O && (n = !1);
                var l = 0;
                for (r = t.length; l < r; l++) {
                    for (o[l] = t[l].slice(), o[l].nTr = t[l].nTr, i = s - 1; 0 <= i; i--) e.aoColumns[i].bVisible || n || o[l].splice(i, 1);
                    a.push([])
                }
                for (l = 0, r = o.length; l < r; l++) {
                    if (e = o[l].nTr)
                        for (; i = e.firstChild;) e.removeChild(i);
                    for (i = 0, t = o[l].length; i < t; i++) {
                        var u = s = 1;
                        if (a[l][i] === O) {
                            for (e.appendChild(o[l][i].cell), a[l][i] = 1; o[l + s] !== O && o[l][i].cell == o[l + s][i].cell;) a[l + s][i] = 1, s++;
                            for (; o[l][i + u] !== O && o[l][i].cell == o[l][i + u].cell;) {
                                for (n = 0; n < s; n++) a[l + n][i + u] = 1;
                                u++
                            }
                            M(o[l][i].cell).attr("rowspan", s).attr("colspan", u)
                        }
                    }
                }
            }
        }

        function z(e) {
            var t = Ne(e, "aoPreDrawCallback", "preDraw", [e]);
            if (-1 !== M.inArray(!1, t)) fe(e, !1);
            else {
                t = [];
                var n = 0,
                    r = e.asStripeClasses,
                    i = r.length,
                    o = e.oLanguage,
                    a = e.iInitDisplayStart,
                    s = "ssp" == ze(e),
                    l = e.aiDisplay;
                e.bDrawing = !0, a !== O && -1 !== a && (e._iDisplayStart = s ? a : a >= e.fnRecordsDisplay() ? 0 : a, e.iInitDisplayStart = -1), a = e._iDisplayStart;
                var u = e.fnDisplayEnd();
                if (e.bDeferLoading) e.bDeferLoading = !1, e.iDraw++, fe(e, !1);
                else if (s) {
                    if (!e.bDestroying && !G(e)) return
                } else e.iDraw++;
                if (0 !== l.length)
                    for (o = s ? e.aoData.length : u, s = s ? 0 : a; s < o; s++) {
                        var c = l[s],
                            d = e.aoData[c];
                        null === d.nTr && N(e, c);
                        var f = d.nTr;
                        if (0 !== i) {
                            var p = r[n % i];
                            d._sRowStripe != p && (M(f).removeClass(d._sRowStripe).addClass(p), d._sRowStripe = p)
                        }
                        Ne(e, "aoRowCallback", null, [f, d._aData, n, s, c]), t.push(f), n++
                    } else n = o.sZeroRecords, 1 == e.iDraw && "ajax" == ze(e) ? n = o.sLoadingRecords : o.sEmptyTable && 0 === e.fnRecordsTotal() && (n = o.sEmptyTable), t[0] = M("<tr/>", {
                        class: i ? r[0] : ""
                    }).append(M("<td />", {
                        valign: "top",
                        colSpan: y(e),
                        class: e.oClasses.sRowEmpty
                    }).html(n))[0];
                Ne(e, "aoHeaderCallback", "header", [M(e.nTHead).children("tr")[0], v(e), a, u, l]), Ne(e, "aoFooterCallback", "footer", [M(e.nTFoot).children("tr")[0], v(e), a, u, l]), (r = M(e.nTBody)).children().detach(), r.append(M(t)), Ne(e, "aoDrawCallback", "draw", [e]), e.bSorted = !1, e.bFiltered = !1, e.bDrawing = !1
            }
        }

        function B(e, t) {
            var n = e.oFeatures,
                r = n.bFilter;
            n.bSort && De(e), r ? Y(e, e.oPreviousSearch) : e.aiDisplay = e.aiDisplayMaster.slice(), !0 !== t && (e._iDisplayStart = 0), e._drawHold = t, z(e), e._drawHold = !1
        }

        function j(e) {
            var t = e.oClasses,
                n = M(e.nTable);
            n = M("<div/>").insertBefore(n);
            var r = e.oFeatures,
                i = M("<div/>", {
                    id: e.sTableId + "_wrapper",
                    class: t.sWrapper + (e.nTFoot ? "" : " " + t.sNoFooter)
                });
            e.nHolding = n[0], e.nTableWrapper = i[0], e.nTableReinsertBefore = e.nTable.nextSibling;
            for (var o, a, s, l, u, c, d = e.sDom.split(""), f = 0; f < d.length; f++) {
                if (o = null, "<" == (a = d[f])) {
                    if (s = M("<div/>")[0], "'" == (l = d[f + 1]) || '"' == l) {
                        for (u = "", c = 2; d[f + c] != l;) u += d[f + c], c++;
                        "H" == u ? u = t.sJUIHeader : "F" == u && (u = t.sJUIFooter), -1 != u.indexOf(".") ? (l = u.split("."), s.id = l[0].substr(1, l[0].length - 1), s.className = l[1]) : "#" == u.charAt(0) ? s.id = u.substr(1, u.length - 1) : s.className = u, f += c
                    }
                    i.append(s), i = M(s)
                } else if (">" == a) i = i.parent();
                else if ("l" == a && r.bPaginate && r.bLengthChange) o = le(e);
                else if ("f" == a && r.bFilter) o = q(e);
                else if ("r" == a && r.bProcessing) o = de(e);
                else if ("t" == a) o = pe(e);
                else if ("i" == a && r.bInfo) o = ne(e);
                else if ("p" == a && r.bPaginate) o = ue(e);
                else if (0 !== Ke.ext.feature.length)
                    for (c = 0, l = (s = Ke.ext.feature).length; c < l; c++)
                        if (a == s[c].cFeature) {
                            o = s[c].fnInit(e);
                            break
                        } o && ((s = e.aanFeatures)[a] || (s[a] = []), s[a].push(o), i.append(o))
            }
            n.replaceWith(i), e.nHolding = null
        }

        function U(e, t) {
            var n, r, i;
            t = M(t).children("tr"), e.splice(0, e.length);
            var o = 0;
            for (i = t.length; o < i; o++) e.push([]);
            for (o = 0, i = t.length; o < i; o++) {
                var a = t[o];
                for (n = a.firstChild; n;) {
                    if ("TD" == n.nodeName.toUpperCase() || "TH" == n.nodeName.toUpperCase()) {
                        var s = 1 * n.getAttribute("colspan"),
                            l = 1 * n.getAttribute("rowspan");
                        s = s && 0 !== s && 1 !== s ? s : 1, l = l && 0 !== l && 1 !== l ? l : 1;
                        var u = 0;
                        for (r = e[o]; r[u];) u++;
                        var c = u,
                            d = 1 === s;
                        for (r = 0; r < s; r++)
                            for (u = 0; u < l; u++) e[o + u][c + r] = {
                                cell: n,
                                unique: d
                            }, e[o + u].nTr = a
                    }
                    n = n.nextSibling
                }
            }
        }

        function W(e, t, n) {
            var r = [];
            n || (n = e.aoHeader, t && U(n = [], t)), t = 0;
            for (var i = n.length; t < i; t++)
                for (var o = 0, a = n[t].length; o < a; o++) !n[t][o].unique || r[o] && e.bSortCellsTop || (r[o] = n[t][o].cell);
            return r
        }

        function V(r, e, t) {
            if (Ne(r, "aoServerParams", "serverParams", [e]), e && M.isArray(e)) {
                var n = {},
                    i = /(.*?)\[\]$/;
                M.each(e, function (e, t) {
                    (e = t.name.match(i)) ? (e = e[0], n[e] || (n[e] = []), n[e].push(t.value)) : n[t.name] = t.value
                }), e = n
            }

            function o(e) {
                Ne(r, null, "xhr", [r, e, r.jqXHR]), t(e)
            }
            var a = r.ajax,
                s = r.oInstance;
            if (M.isPlainObject(a) && a.data) {
                var l = a.data,
                    u = "function" == typeof l ? l(e, r) : l;
                e = "function" == typeof l && u ? u : M.extend(!0, e, u), delete a.data
            }
            u = {
                data: e,
                success: function (e) {
                    var t = e.error || e.sError;
                    t && Pe(r, 0, t), r.json = e, o(e)
                },
                dataType: "json",
                cache: !1,
                type: r.sServerMethod,
                error: function (e, t, n) {
                    n = Ne(r, null, "xhr", [r, null, r.jqXHR]), -1 === M.inArray(!0, n) && ("parsererror" == t ? Pe(r, 0, "Invalid JSON response", 1) : 4 === e.readyState && Pe(r, 0, "Ajax error", 7)), fe(r, !1)
                }
            }, r.oAjaxData = e, Ne(r, null, "preXhr", [r, e]), r.fnServerData ? r.fnServerData.call(s, r.sAjaxSource, M.map(e, function (e, t) {
                return {
                    name: t,
                    value: e
                }
            }), o, r) : r.sAjaxSource || "string" == typeof a ? r.jqXHR = M.ajax(M.extend(u, {
                url: a || r.sAjaxSource
            })) : "function" == typeof a ? r.jqXHR = a.call(s, e, o, r) : (r.jqXHR = M.ajax(M.extend(u, a)), a.data = l)
        }

        function G(t) {
            return !t.bAjaxDataGet || (t.iDraw++, fe(t, !0), V(t, e(t), function (e) {
                o(t, e)
            }), !1)
        }

        function e(e) {
            function n(e, t) {
                s.push({
                    name: e,
                    value: t
                })
            }
            var t = e.aoColumns,
                r = t.length,
                i = e.oFeatures,
                o = e.oPreviousSearch,
                a = e.aoPreSearchCols,
                s = [],
                l = we(e),
                u = e._iDisplayStart,
                c = !1 !== i.bPaginate ? e._iDisplayLength : -1;
            n("sEcho", e.iDraw), n("iColumns", r), n("sColumns", at(t, "sName").join(",")), n("iDisplayStart", u), n("iDisplayLength", c);
            var d = {
                draw: e.iDraw,
                columns: [],
                order: [],
                start: u,
                length: c,
                search: {
                    value: o.sSearch,
                    regex: o.bRegex
                }
            };
            for (u = 0; u < r; u++) {
                var f = t[u],
                    p = a[u];
                c = "function" == typeof f.mData ? "function" : f.mData, d.columns.push({
                    data: c,
                    name: f.sName,
                    searchable: f.bSearchable,
                    orderable: f.bSortable,
                    search: {
                        value: p.sSearch,
                        regex: p.bRegex
                    }
                }), n("mDataProp_" + u, c), i.bFilter && (n("sSearch_" + u, p.sSearch), n("bRegex_" + u, p.bRegex), n("bSearchable_" + u, f.bSearchable)), i.bSort && n("bSortable_" + u, f.bSortable)
            }
            return i.bFilter && (n("sSearch", o.sSearch), n("bRegex", o.bRegex)), i.bSort && (M.each(l, function (e, t) {
                d.order.push({
                    column: t.col,
                    dir: t.dir
                }), n("iSortCol_" + e, t.col), n("sSortDir_" + e, t.dir)
            }), n("iSortingCols", l.length)), null === (t = Ke.ext.legacy.ajax) ? e.sAjaxSource ? s : d : t ? s : d
        }

        function o(e, n) {
            var t = function (e, t) {
                return n[e] !== O ? n[e] : n[t]
            },
                r = Z(e, n),
                i = t("sEcho", "draw"),
                o = t("iTotalRecords", "recordsTotal");
            if (t = t("iTotalDisplayRecords", "recordsFiltered"), i) {
                if (1 * i < e.iDraw) return;
                e.iDraw = 1 * i
            }
            for (l(e), e._iRecordsTotal = parseInt(o, 10), e._iRecordsDisplay = parseInt(t, 10), i = 0, o = r.length; i < o; i++) R(e, r[i]);
            e.aiDisplay = e.aiDisplayMaster.slice(), e.bAjaxDataGet = !1, z(e), e._bInitComplete || ae(e, n), e.bAjaxDataGet = !0, fe(e, !1)
        }

        function Z(e, t) {
            return "data" === (e = M.isPlainObject(e.ajax) && e.ajax.dataSrc !== O ? e.ajax.dataSrc : e.sAjaxDataProp) ? t.aaData || t[e] : "" !== e ? P(e)(t) : t
        }

        function q(n) {
            var e = n.oClasses,
                t = n.sTableId,
                r = n.oLanguage,
                i = n.oPreviousSearch,
                o = n.aanFeatures,
                a = '<input type="search" class="' + e.sFilterInput + '"/>',
                s = r.sSearch;
            s = s.match(/_INPUT_/) ? s.replace("_INPUT_", a) : s + a, e = M("<div/>", {
                id: o.f ? null : t + "_filter",
                class: e.sFilter
            }).append(M("<label/>").append(s)), o = function () {
                var e = this.value ? this.value : "";
                e != i.sSearch && (Y(n, {
                    sSearch: e,
                    bRegex: i.bRegex,
                    bSmart: i.bSmart,
                    bCaseInsensitive: i.bCaseInsensitive
                }), n._iDisplayStart = 0, z(n))
            }, a = null !== n.searchDelay ? n.searchDelay : "ssp" === ze(n) ? 400 : 0;
            var l = M("input", e).val(i.sSearch).attr("placeholder", r.sSearchPlaceholder).on("keyup.DT search.DT input.DT paste.DT cut.DT", a ? gt(o, a) : o).on("keypress.DT", function (e) {
                if (13 == e.keyCode) return !1
            }).attr("aria-controls", t);
            return M(n.nTable).on("search.dt.DT", function (e, t) {
                if (n === t) try {
                    l[0] !== b.activeElement && l.val(i.sSearch)
                } catch (e) { }
            }), e[0]
        }

        function Y(e, t, n) {
            function r(e) {
                o.sSearch = e.sSearch, o.bRegex = e.bRegex, o.bSmart = e.bSmart, o.bCaseInsensitive = e.bCaseInsensitive
            }

            function i(e) {
                return e.bEscapeRegex !== O ? !e.bEscapeRegex : e.bRegex
            }
            var o = e.oPreviousSearch,
                a = e.aoPreSearchCols;
            if (s(e), "ssp" != ze(e)) {
                for ($(e, t.sSearch, n, i(t), t.bSmart, t.bCaseInsensitive), r(t), t = 0; t < a.length; t++) J(e, a[t].sSearch, t, i(a[t]), a[t].bSmart, a[t].bCaseInsensitive);
                X(e)
            } else r(t);
            e.bFiltered = !0, Ne(e, null, "search", [e])
        }

        function X(e) {
            for (var t, n, r = Ke.ext.search, i = e.aiDisplay, o = 0, a = r.length; o < a; o++) {
                for (var s = [], l = 0, u = i.length; l < u; l++) n = i[l], t = e.aoData[n], r[o](e, t._aFilterData, n, t._aData, l) && s.push(n);
                i.length = 0, M.merge(i, s)
            }
        }

        function J(e, t, n, r, i, o) {
            if ("" !== t) {
                var a = [],
                    s = e.aiDisplay;
                for (r = Q(t, r, i, o), i = 0; i < s.length; i++) t = e.aoData[s[i]]._aFilterData[n], r.test(t) && a.push(s[i]);
                e.aiDisplay = a
            }
        }

        function $(e, t, n, r, i, o) {
            i = Q(t, r, i, o);
            var a = e.oPreviousSearch.sSearch,
                s = e.aiDisplayMaster;
            o = [], 0 !== Ke.ext.search.length && (n = !0);
            var l = K(e);
            if (t.length <= 0) e.aiDisplay = s.slice();
            else {
                for ((l || n || r || a.length > t.length || 0 !== t.indexOf(a) || e.bSorted) && (e.aiDisplay = s.slice()), t = e.aiDisplay, n = 0; n < t.length; n++) i.test(e.aoData[t[n]]._sFilterRow) && o.push(t[n]);
                e.aiDisplay = o
            }
        }

        function Q(e, t, n, r) {
            return e = t ? e : dt(e), n && (e = "^(?=.*?" + M.map(e.match(/"[^"]+"|[^ ]+/g) || [""], function (e) {
                if ('"' === e.charAt(0)) {
                    var t = e.match(/^"(.*)"$/);
                    e = t ? t[1] : e
                }
                return e.replace('"', "")
            }).join(")(?=.*?") + ").*$"), new RegExp(e, r ? "i" : "")
        }

        function K(e) {
            var t, n, r = e.aoColumns,
                i = Ke.ext.type.search,
                o = !1,
                a = 0;
            for (t = e.aoData.length; a < t; a++) {
                var s = e.aoData[a];
                if (!s._aFilterData) {
                    var l = [],
                        u = 0;
                    for (n = r.length; u < n; u++) {
                        if ((o = r[u]).bSearchable) {
                            var c = g(e, a, u, "filter");
                            i[o.sType] && (c = i[o.sType](c)), null === c && (c = ""), "string" != typeof c && c.toString && (c = c.toString())
                        } else c = "";
                        c.indexOf && -1 !== c.indexOf("&") && (ft.innerHTML = c, c = pt ? ft.textContent : ft.innerText), c.replace && (c = c.replace(/[\r\n\u2028]/g, "")), l.push(c)
                    }
                    s._aFilterData = l, s._sFilterRow = l.join("  "), o = !0
                }
            }
            return o
        }

        function ee(e) {
            return {
                search: e.sSearch,
                smart: e.bSmart,
                regex: e.bRegex,
                caseInsensitive: e.bCaseInsensitive
            }
        }

        function te(e) {
            return {
                sSearch: e.search,
                bSmart: e.smart,
                bRegex: e.regex,
                bCaseInsensitive: e.caseInsensitive
            }
        }

        function ne(e) {
            var t = e.sTableId,
                n = e.aanFeatures.i,
                r = M("<div/>", {
                    class: e.oClasses.sInfo,
                    id: n ? null : t + "_info"
                });
            return n || (e.aoDrawCallback.push({
                fn: re,
                sName: "information"
            }), r.attr("role", "status").attr("aria-live", "polite"), M(e.nTable).attr("aria-describedby", t + "_info")), r[0]
        }

        function re(e) {
            var t = e.aanFeatures.i;
            if (0 !== t.length) {
                var n = e.oLanguage,
                    r = e._iDisplayStart + 1,
                    i = e.fnDisplayEnd(),
                    o = e.fnRecordsTotal(),
                    a = e.fnRecordsDisplay(),
                    s = a ? n.sInfo : n.sInfoEmpty;
                a !== o && (s += " " + n.sInfoFiltered), s = ie(e, s += n.sInfoPostFix), null !== (n = n.fnInfoCallback) && (s = n.call(e.oInstance, e, r, i, o, a, s)), M(t).html(s)
            }
        }

        function ie(e, t) {
            var n = e.fnFormatNumber,
                r = e._iDisplayStart + 1,
                i = e._iDisplayLength,
                o = e.fnRecordsDisplay(),
                a = -1 === i;
            return t.replace(/_START_/g, n.call(e, r)).replace(/_END_/g, n.call(e, e.fnDisplayEnd())).replace(/_MAX_/g, n.call(e, e.fnRecordsTotal())).replace(/_TOTAL_/g, n.call(e, o)).replace(/_PAGE_/g, n.call(e, a ? 1 : Math.ceil(r / i))).replace(/_PAGES_/g, n.call(e, a ? 1 : Math.ceil(o / i)))
        }

        function oe(n) {
            var r = n.iInitDisplayStart,
                e = n.aoColumns,
                t = n.oFeatures,
                i = n.bDeferLoading;
            if (n.bInitialised) {
                j(n), L(n), F(n, n.aoHeader), F(n, n.aoFooter), fe(n, !0), t.bAutoWidth && ve(n);
                var o = 0;
                for (t = e.length; o < t; o++) {
                    var a = e[o];
                    a.sWidth && (a.nTh.style.width = Se(a.sWidth))
                }
                Ne(n, null, "preInit", [n]), B(n), "ssp" == (e = ze(n)) && !i || ("ajax" == e ? V(n, [], function (e) {
                    var t = Z(n, e);
                    for (o = 0; o < t.length; o++) R(n, t[o]);
                    n.iInitDisplayStart = r, B(n), fe(n, !1), ae(n, e)
                }) : (fe(n, !1), ae(n)))
            } else setTimeout(function () {
                oe(n)
            }, 200)
        }

        function ae(e, t) {
            e._bInitComplete = !0, (t || e.oInit.aaData) && H(e), Ne(e, null, "plugin-init", [e, t]), Ne(e, "aoInitComplete", "init", [e, t])
        }

        function se(e, t) {
            t = parseInt(t, 10), e._iDisplayLength = t, Le(e), Ne(e, null, "length", [e, t])
        }

        function le(r) {
            var e = r.oClasses,
                t = r.sTableId,
                n = r.aLengthMenu,
                i = M.isArray(n[0]),
                o = i ? n[0] : n;
            n = i ? n[1] : n, i = M("<select/>", {
                name: t + "_length",
                "aria-controls": t,
                class: e.sLengthSelect
            });
            for (var a = 0, s = o.length; a < s; a++) i[0][a] = new Option("number" == typeof n[a] ? r.fnFormatNumber(n[a]) : n[a], o[a]);
            var l = M("<div><label/></div>").addClass(e.sLength);
            return r.aanFeatures.l || (l[0].id = t + "_length"), l.children().append(r.oLanguage.sLengthMenu.replace("_MENU_", i[0].outerHTML)), M("select", l).val(r._iDisplayLength).on("change.DT", function (e) {
                se(r, M(this).val()), z(r)
            }), M(r.nTable).on("length.dt.DT", function (e, t, n) {
                r === t && M("select", l).val(n)
            }), l[0]
        }

        function ue(e) {
            function a(e) {
                z(e)
            }
            var t = e.sPaginationType,
                s = Ke.ext.pager[t],
                l = "function" == typeof s;
            t = M("<div/>").addClass(e.oClasses.sPaging + t)[0];
            var u = e.aanFeatures;
            return l || s.fnInit(e, t, a), u.p || (t.id = e.sTableId + "_paginate", e.aoDrawCallback.push({
                fn: function (e) {
                    if (l) {
                        var t, n = e._iDisplayStart,
                            r = e._iDisplayLength,
                            i = e.fnRecordsDisplay(),
                            o = -1 === r;
                        for (n = o ? 0 : Math.ceil(n / r), r = o ? 1 : Math.ceil(i / r), i = s(n, r), o = 0, t = u.p.length; o < t; o++) Fe(e, "pageButton")(e, u.p[o], o, i, n, r)
                    } else s.fnUpdate(e, a)
                },
                sName: "pagination"
            })), t
        }

        function ce(e, t, n) {
            var r = e._iDisplayStart,
                i = e._iDisplayLength,
                o = e.fnRecordsDisplay();
            return 0 === o || -1 === i ? r = 0 : "number" == typeof t ? o < (r = t * i) && (r = 0) : "first" == t ? r = 0 : "previous" == t ? (r = 0 <= i ? r - i : 0) < 0 && (r = 0) : "next" == t ? r + i < o && (r += i) : "last" == t ? r = Math.floor((o - 1) / i) * i : Pe(e, 0, "Unknown paging action: " + t, 5), t = e._iDisplayStart !== r, e._iDisplayStart = r, t && (Ne(e, null, "page", [e]), n && z(e)), t
        }

        function de(e) {
            return M("<div/>", {
                id: e.aanFeatures.r ? null : e.sTableId + "_processing",
                class: e.oClasses.sProcessing
            }).html(e.oLanguage.sProcessing).insertBefore(e.nTable)[0]
        }

        function fe(e, t) {
            e.oFeatures.bProcessing && M(e.aanFeatures.r).css("display", t ? "block" : "none"), Ne(e, null, "processing", [e, t])
        }

        function pe(e) {
            var t = M(e.nTable);
            t.attr("role", "grid");
            var n = e.oScroll;
            if ("" === n.sX && "" === n.sY) return e.nTable;
            var r = n.sX,
                i = n.sY,
                o = e.oClasses,
                a = t.children("caption"),
                s = a.length ? a[0]._captionSide : null,
                l = M(t[0].cloneNode(!1)),
                u = M(t[0].cloneNode(!1)),
                c = t.children("tfoot");
            c.length || (c = null), l = M("<div/>", {
                class: o.sScrollWrapper
            }).append(M("<div/>", {
                class: o.sScrollHead
            }).css({
                overflow: "hidden",
                position: "relative",
                border: 0,
                width: r ? r ? Se(r) : null : "100%"
            }).append(M("<div/>", {
                class: o.sScrollHeadInner
            }).css({
                "box-sizing": "content-box",
                width: n.sXInner || "100%"
            }).append(l.removeAttr("id").css("margin-left", 0).append("top" === s ? a : null).append(t.children("thead"))))).append(M("<div/>", {
                class: o.sScrollBody
            }).css({
                position: "relative",
                overflow: "auto",
                width: r ? Se(r) : null
            }).append(t)), c && l.append(M("<div/>", {
                class: o.sScrollFoot
            }).css({
                overflow: "hidden",
                border: 0,
                width: r ? r ? Se(r) : null : "100%"
            }).append(M("<div/>", {
                class: o.sScrollFootInner
            }).append(u.removeAttr("id").css("margin-left", 0).append("bottom" === s ? a : null).append(t.children("tfoot")))));
            var d = (t = l.children())[0];
            o = t[1];
            var f = c ? t[2] : null;
            return r && M(o).on("scroll.DT", function (e) {
                e = this.scrollLeft, d.scrollLeft = e, c && (f.scrollLeft = e)
            }), M(o).css(i && n.bCollapse ? "max-height" : "height", i), e.nScrollHead = d, e.nScrollBody = o, e.nScrollFoot = f, e.aoDrawCallback.push({
                fn: he,
                sName: "scrolling"
            }), l[0]
        }

        function he(n) {
            var e = n.oScroll,
                t = e.sX,
                r = e.sXInner,
                i = e.sY;
            e = e.iBarWidth;
            var o = M(n.nScrollHead),
                a = o[0].style,
                s = o.children("div"),
                l = s[0].style,
                u = s.children("table");
            s = n.nScrollBody;

            function c(e) {
                (e = e.style).paddingTop = "0", e.paddingBottom = "0", e.borderTopWidth = "0", e.borderBottomWidth = "0", e.height = 0
            }
            var d, f = M(s),
                p = s.style,
                h = M(n.nScrollFoot).children("div"),
                g = h.children("table"),
                v = M(n.nTHead),
                m = M(n.nTable),
                y = m[0],
                b = y.style,
                S = n.nTFoot ? M(n.nTFoot) : null,
                w = n.oBrowser,
                D = w.bScrollOversize,
                T = at(n.aoColumns, "nTh"),
                C = [],
                E = [],
                _ = [],
                x = [],
                I = s.scrollHeight > s.clientHeight;
            if (n.scrollBarVis !== I && n.scrollBarVis !== O) n.scrollBarVis = I, H(n);
            else {
                if (n.scrollBarVis = I, m.children("thead, tfoot").remove(), S) {
                    var R = S.clone().prependTo(m),
                        k = S.find("tr");
                    R = R.find("tr")
                }
                var P = v.clone().prependTo(m);
                v = v.find("tr"), I = P.find("tr"), P.find("th, td").removeAttr("tabindex"), t || (p.width = "100%", o[0].style.width = "100%"), M.each(W(n, P), function (e, t) {
                    d = A(n, e), t.style.width = n.aoColumns[d].sWidth
                }), S && ge(function (e) {
                    e.style.width = ""
                }, R), o = m.outerWidth(), "" === t ? (b.width = "100%", D && (m.find("tbody").height() > s.offsetHeight || "scroll" == f.css("overflow-y")) && (b.width = Se(m.outerWidth() - e)), o = m.outerWidth()) : "" !== r && (b.width = Se(r), o = m.outerWidth()), ge(c, I), ge(function (e) {
                    _.push(e.innerHTML), C.push(Se(M(e).css("width")))
                }, I), ge(function (e, t) {
                    -1 !== M.inArray(e, T) && (e.style.width = C[t])
                }, v), M(I).height(0), S && (ge(c, R), ge(function (e) {
                    x.push(e.innerHTML), E.push(Se(M(e).css("width")))
                }, R), ge(function (e, t) {
                    e.style.width = E[t]
                }, k), M(R).height(0)), ge(function (e, t) {
                    e.innerHTML = '<div class="dataTables_sizing">' + _[t] + "</div>", e.childNodes[0].style.height = "0", e.childNodes[0].style.overflow = "hidden", e.style.width = C[t]
                }, I), S && ge(function (e, t) {
                    e.innerHTML = '<div class="dataTables_sizing">' + x[t] + "</div>", e.childNodes[0].style.height = "0", e.childNodes[0].style.overflow = "hidden", e.style.width = E[t]
                }, R), m.outerWidth() < o ? (k = s.scrollHeight > s.offsetHeight || "scroll" == f.css("overflow-y") ? o + e : o, D && (s.scrollHeight > s.offsetHeight || "scroll" == f.css("overflow-y")) && (b.width = Se(k - e)), "" !== t && "" === r || Pe(n, 1, "Possible column misalignment", 6)) : k = "100%", p.width = Se(k), a.width = Se(k), S && (n.nScrollFoot.style.width = Se(k)), !i && D && (p.height = Se(y.offsetHeight + e)), t = m.outerWidth(), u[0].style.width = Se(t), l.width = Se(t), r = m.height() > s.clientHeight || "scroll" == f.css("overflow-y"), l[i = "padding" + (w.bScrollbarLeft ? "Left" : "Right")] = r ? e + "px" : "0px", S && (g[0].style.width = Se(t), h[0].style.width = Se(t), h[0].style[i] = r ? e + "px" : "0px"), m.children("colgroup").insertBefore(m.children("thead")), f.trigger("scroll"), !n.bSorted && !n.bFiltered || n._drawHold || (s.scrollTop = 0)
            }
        }

        function ge(e, t, n) {
            for (var r, i, o = 0, a = 0, s = t.length; a < s;) {
                for (r = t[a].firstChild, i = n ? n[a].firstChild : null; r;) 1 === r.nodeType && (n ? e(r, i, o) : e(r, o), o++), r = r.nextSibling, i = n ? i.nextSibling : null;
                a++
            }
        }

        function ve(e) {
            var t, n = e.nTable,
                r = e.aoColumns,
                i = e.oScroll,
                o = i.sY,
                a = i.sX,
                s = i.sXInner,
                l = r.length,
                u = x(e, "bVisible"),
                c = M("th", e.nTHead),
                d = n.getAttribute("width"),
                f = n.parentNode,
                p = !1,
                h = e.oBrowser;
            for (i = h.bScrollOversize, (t = n.style.width) && -1 !== t.indexOf("%") && (d = t), t = 0; t < u.length; t++) {
                var g = r[u[t]];
                null !== g.sWidth && (g.sWidth = me(g.sWidthOrig, f), p = !0)
            }
            if (i || !p && !a && !o && l == y(e) && l == c.length)
                for (t = 0; t < l; t++) null !== (u = A(e, t)) && (r[u].sWidth = Se(c.eq(t).width()));
            else {
                (l = M(n).clone().css("visibility", "hidden").removeAttr("id")).find("tbody tr").remove();
                var v = M("<tr/>").appendTo(l.find("tbody"));
                for (l.find("thead, tfoot").remove(), l.append(M(e.nTHead).clone()).append(M(e.nTFoot).clone()), l.find("tfoot th, tfoot td").css("width", ""), c = W(e, l.find("thead")[0]), t = 0; t < u.length; t++) g = r[u[t]], c[t].style.width = null !== g.sWidthOrig && "" !== g.sWidthOrig ? Se(g.sWidthOrig) : "", g.sWidthOrig && a && M(c[t]).append(M("<div/>").css({
                    width: g.sWidthOrig,
                    margin: 0,
                    padding: 0,
                    border: 0,
                    height: 1
                }));
                if (e.aoData.length)
                    for (t = 0; t < u.length; t++) g = r[p = u[t]], M(ye(e, p)).clone(!1).append(g.sContentPadding).appendTo(v);
                for (M("[name]", l).removeAttr("name"), g = M("<div/>").css(a || o ? {
                    position: "absolute",
                    top: 0,
                    left: 0,
                    height: 1,
                    right: 0,
                    overflow: "hidden"
                } : {}).append(l).appendTo(f), a && s ? l.width(s) : a ? (l.css("width", "auto"), l.removeAttr("width"), l.width() < f.clientWidth && d && l.width(f.clientWidth)) : o ? l.width(f.clientWidth) : d && l.width(d), t = o = 0; t < u.length; t++) s = (f = M(c[t])).outerWidth() - f.width(), o += f = h.bBounding ? Math.ceil(c[t].getBoundingClientRect().width) : f.outerWidth(), r[u[t]].sWidth = Se(f - s);
                n.style.width = Se(o), g.remove()
            }
            d && (n.style.width = Se(d)), !d && !a || e._reszEvt || (n = function () {
                M(m).on("resize.DT-" + e.sInstance, gt(function () {
                    H(e)
                }))
            }, i ? setTimeout(n, 1e3) : n(), e._reszEvt = !0)
        }

        function me(e, t) {
            return e ? (t = (e = M("<div/>").css("width", Se(e)).appendTo(t || b.body))[0].offsetWidth, e.remove(), t) : 0
        }

        function ye(e, t) {
            var n = be(e, t);
            if (n < 0) return null;
            var r = e.aoData[n];
            return r.nTr ? r.anCells[t] : M("<td/>").html(g(e, n, t, "display"))[0]
        }

        function be(e, t) {
            for (var n, r = -1, i = -1, o = 0, a = e.aoData.length; o < a; o++)(n = (n = (n = g(e, o, t, "display") + "").replace(ht, "")).replace(/&nbsp;/g, " ")).length > r && (r = n.length, i = o);
            return i
        }

        function Se(e) {
            return null === e ? "0px" : "number" == typeof e ? e < 0 ? "0px" : e + "px" : e.match(/\d$/) ? e + "px" : e
        }

        function we(e) {
            var t = [],
                n = e.aoColumns,
                r = e.aaSortingFixed,
                i = M.isPlainObject(r),
                o = [],
                a = function (e) {
                    e.length && !M.isArray(e[0]) ? o.push(e) : M.merge(o, e)
                };
            for (M.isArray(r) && a(r), i && r.pre && a(r.pre), a(e.aaSorting), i && r.post && a(r.post), e = 0; e < o.length; e++) {
                var s = o[e][0];
                for (r = 0, i = (a = n[s].aDataSort).length; r < i; r++) {
                    var l = a[r],
                        u = n[l].sType || "string";
                    o[e]._idx === O && (o[e]._idx = M.inArray(o[e][1], n[l].asSorting)), t.push({
                        src: s,
                        col: l,
                        dir: o[e][1],
                        index: o[e]._idx,
                        type: u,
                        formatter: Ke.ext.type.order[u + "-pre"]
                    })
                }
            }
            return t
        }

        function De(e) {
            var t, u = [],
                c = Ke.ext.type.order,
                d = e.aoData,
                n = 0,
                r = e.aiDisplayMaster;
            s(e);
            var f = we(e),
                i = 0;
            for (t = f.length; i < t; i++) {
                var o = f[i];
                o.formatter && n++, xe(e, o.col)
            }
            if ("ssp" != ze(e) && 0 !== f.length) {
                for (i = 0, t = r.length; i < t; i++) u[r[i]] = i;
                n === f.length ? r.sort(function (e, t) {
                    var n, r = f.length,
                        i = d[e]._aSortData,
                        o = d[t]._aSortData;
                    for (n = 0; n < r; n++) {
                        var a = f[n],
                            s = i[a.col],
                            l = o[a.col];
                        if (0 !== (s = s < l ? -1 : l < s ? 1 : 0)) return "asc" === a.dir ? s : -s
                    }
                    return (s = u[e]) < (l = u[t]) ? -1 : l < s ? 1 : 0
                }) : r.sort(function (e, t) {
                    var n, r = f.length,
                        i = d[e]._aSortData,
                        o = d[t]._aSortData;
                    for (n = 0; n < r; n++) {
                        var a = f[n],
                            s = i[a.col],
                            l = o[a.col];
                        if (0 !== (s = (a = c[a.type + "-" + a.dir] || c["string-" + a.dir])(s, l))) return s
                    }
                    return (s = u[e]) < (l = u[t]) ? -1 : l < s ? 1 : 0
                })
            }
            e.bSorted = !0
        }

        function Te(e) {
            var t = e.aoColumns,
                n = we(e);
            e = e.oLanguage.oAria;
            for (var r = 0, i = t.length; r < i; r++) {
                var o = t[r],
                    a = o.asSorting,
                    s = o.sTitle.replace(/<.*?>/g, ""),
                    l = o.nTh;
                l.removeAttribute("aria-sort"), o.bSortable && (s += "asc" === (o = 0 < n.length && n[0].col == r ? (l.setAttribute("aria-sort", "asc" == n[0].dir ? "ascending" : "descending"), a[n[0].index + 1] || a[0]) : a[0]) ? e.sSortAscending : e.sSortDescending), l.setAttribute("aria-label", s)
            }
        }

        function Ce(e, t, n, r) {
            function i(e, t) {
                var n = e._idx;
                return n === O && (n = M.inArray(e[1], a)), n + 1 < a.length ? n + 1 : t ? null : 0
            }
            var o = e.aaSorting,
                a = e.aoColumns[t].asSorting;
            "number" == typeof o[0] && (o = e.aaSorting = [o]), n && e.oFeatures.bSortMulti ? -1 !== (n = M.inArray(t, at(o, "0"))) ? (null === (t = i(o[n], !0)) && 1 === o.length && (t = 0), null === t ? o.splice(n, 1) : (o[n][1] = a[t], o[n]._idx = t)) : (o.push([t, a[0], 0]), o[o.length - 1]._idx = 0) : o.length && o[0][0] == t ? (t = i(o[0]), o.length = 1, o[0][1] = a[t], o[0]._idx = t) : (o.length = 0, o.push([t, a[0]]), o[0]._idx = 0), B(e), "function" == typeof r && r(e)
        }

        function Ee(t, e, n, r) {
            var i = t.aoColumns[n];
            He(e, {}, function (e) {
                !1 !== i.bSortable && (t.oFeatures.bProcessing ? (fe(t, !0), setTimeout(function () {
                    Ce(t, n, e.shiftKey, r), "ssp" !== ze(t) && fe(t, !1)
                }, 0)) : Ce(t, n, e.shiftKey, r))
            })
        }

        function _e(e) {
            var t, n = e.aLastSort,
                r = e.oClasses.sSortColumn,
                i = we(e),
                o = e.oFeatures;
            if (o.bSort && o.bSortClasses) {
                for (o = 0, t = n.length; o < t; o++) {
                    var a = n[o].src;
                    M(at(e.aoData, "anCells", a)).removeClass(r + (o < 2 ? o + 1 : 3))
                }
                for (o = 0, t = i.length; o < t; o++) a = i[o].src, M(at(e.aoData, "anCells", a)).addClass(r + (o < 2 ? o + 1 : 3))
            }
            e.aLastSort = i
        }

        function xe(e, t) {
            var n, r = e.aoColumns[t],
                i = Ke.ext.order[r.sSortDataType];
            i && (n = i.call(e.oInstance, e, t, u(e, t)));
            for (var o, a = Ke.ext.type.order[r.sType + "-pre"], s = 0, l = e.aoData.length; s < l; s++)(r = e.aoData[s])._aSortData || (r._aSortData = []), r._aSortData[t] && !i || (o = i ? n[s] : g(e, s, t, "sort"), r._aSortData[t] = a ? a(o) : o)
        }

        function Ie(n) {
            if (n.oFeatures.bStateSave && !n.bDestroying) {
                var e = {
                    time: +new Date,
                    start: n._iDisplayStart,
                    length: n._iDisplayLength,
                    order: M.extend(!0, [], n.aaSorting),
                    search: ee(n.oPreviousSearch),
                    columns: M.map(n.aoColumns, function (e, t) {
                        return {
                            visible: e.bVisible,
                            search: ee(n.aoPreSearchCols[t])
                        }
                    })
                };
                Ne(n, "aoStateSaveParams", "stateSaveParams", [n, e]), n.oSavedState = e, n.fnStateSaveCallback.call(n.oInstance, n, e)
            }
        }

        function Re(n, e, r) {
            var i, o, a = n.aoColumns;
            if (e = function (e) {
                if (e && e.time) {
                    var t = Ne(n, "aoStateLoadParams", "stateLoadParams", [n, e]);
                    if (-1 === M.inArray(!1, t) && !(0 < (t = n.iStateDuration) && e.time < +new Date - 1e3 * t || e.columns && a.length !== e.columns.length)) {
                        if (n.oLoadedState = M.extend(!0, {}, e), e.start !== O && (n._iDisplayStart = e.start, n.iInitDisplayStart = e.start), e.length !== O && (n._iDisplayLength = e.length), e.order !== O && (n.aaSorting = [], M.each(e.order, function (e, t) {
                            n.aaSorting.push(t[0] >= a.length ? [0, t[1]] : t)
                        })), e.search !== O && M.extend(n.oPreviousSearch, te(e.search)), e.columns)
                            for (i = 0, o = e.columns.length; i < o; i++)(t = e.columns[i]).visible !== O && (a[i].bVisible = t.visible), t.search !== O && M.extend(n.aoPreSearchCols[i], te(t.search));
                        Ne(n, "aoStateLoaded", "stateLoaded", [n, e])
                    }
                }
                r()
            }, n.oFeatures.bStateSave) {
                var t = n.fnStateLoadCallback.call(n.oInstance, n, e);
                t !== O && e(t)
            } else r()
        }

        function ke(e) {
            var t = Ke.settings;
            return -1 !== (e = M.inArray(e, at(t, "nTable"))) ? t[e] : null
        }

        function Pe(e, t, n, r) {
            if (n = "DataTables warning: " + (e ? "table id=" + e.sTableId + " - " : "") + n, r && (n += ". For more information about this error, please see http://datatables.net/tn/" + r), t) m.console && console.log && console.log(n);
            else if (t = (t = Ke.ext).sErrMode || t.errMode, e && Ne(e, null, "error", [e, r, n]), "alert" == t) alert(n);
            else {
                if ("throw" == t) throw Error(n);
                "function" == typeof t && t(e, r, n)
            }
        }

        function Me(n, r, e, t) {
            M.isArray(e) ? M.each(e, function (e, t) {
                M.isArray(t) ? Me(n, r, t[0], t[1]) : Me(n, r, t)
            }) : (t === O && (t = e), r[e] !== O && (n[t] = r[e]))
        }

        function Oe(e, t, n) {
            var r;
            for (r in t)
                if (t.hasOwnProperty(r)) {
                    var i = t[r];
                    M.isPlainObject(i) ? (M.isPlainObject(e[r]) || (e[r] = {}), M.extend(!0, e[r], i)) : n && "data" !== r && "aaData" !== r && M.isArray(i) ? e[r] = i.slice() : e[r] = i
                } return e
        }

        function He(t, e, n) {
            M(t).on("click.DT", e, function (e) {
                M(t).blur(), n(e)
            }).on("keypress.DT", e, function (e) {
                13 === e.which && (e.preventDefault(), n(e))
            }).on("selectstart.DT", function () {
                return !1
            })
        }

        function Ae(e, t, n, r) {
            n && e[t].push({
                fn: n,
                sName: r
            })
        }

        function Ne(n, e, t, r) {
            var i = [];
            return e && (i = M.map(n[e].slice().reverse(), function (e, t) {
                return e.fn.apply(n.oInstance, r)
            })), null !== t && (e = M.Event(t + ".dt"), M(n.nTable).trigger(e, r), i.push(e.result)), i
        }

        function Le(e) {
            var t = e._iDisplayStart,
                n = e.fnDisplayEnd(),
                r = e._iDisplayLength;
            n <= t && (t = n - r), t -= t % r, (-1 === r || t < 0) && (t = 0), e._iDisplayStart = t
        }

        function Fe(e, t) {
            e = e.renderer;
            var n = Ke.ext.renderer[t];
            return M.isPlainObject(e) && e[t] ? n[e[t]] || n._ : "string" == typeof e && n[e] || n._
        }

        function ze(e) {
            return e.oFeatures.bServerSide ? "ssp" : e.ajax || e.sAjaxSource ? "ajax" : "dom"
        }

        function Be(e, t) {
            var n = Rt.numbers_length,
                r = Math.floor(n / 2);
            return t <= n ? e = Ye(0, t) : e <= r ? ((e = Ye(0, n - 2)).push("ellipsis"), e.push(t - 1)) : (t - 1 - r <= e ? e = Ye(t - (n - 2), t) : ((e = Ye(e - r + 2, e + r - 1)).push("ellipsis"), e.push(t - 1)), e.splice(0, 0, "ellipsis"), e.splice(0, 0, 0)), e.DT_el = "span", e
        }

        function je(n) {
            M.each({
                num: function (e) {
                    return kt(e, n)
                },
                "num-fmt": function (e) {
                    return kt(e, n, ot)
                },
                "html-num": function (e) {
                    return kt(e, n, nt)
                },
                "html-num-fmt": function (e) {
                    return kt(e, n, nt, ot)
                }
            }, function (e, t) {
                Je.type.order[e + n + "-pre"] = t, e.match(/^html\-/) && (Je.type.search[e + n] = Je.type.search.html)
            })
        }

        function t(t) {
            return function () {
                var e = [ke(this[Ke.ext.iApiIndex])].concat(Array.prototype.slice.call(arguments));
                return Ke.ext.internal[t].apply(this, e)
            }
        }

        function Ue(e) {
            return !e || !0 === e || "-" === e
        }

        function We(e) {
            var t = parseInt(e, 10);
            return !isNaN(t) && isFinite(e) ? t : null
        }

        function Ve(e, t) {
            return et[t] || (et[t] = new RegExp(dt(t), "g")), "string" == typeof e && "." !== t ? e.replace(/\./g, "").replace(et[t], ".") : e
        }

        function Ge(e, t, n) {
            var r = "string" == typeof e;
            return !!Ue(e) || (t && r && (e = Ve(e, t)), n && r && (e = e.replace(ot, "")), !isNaN(parseFloat(e)) && isFinite(e))
        }

        function Ze(e, t, n) {
            return !!Ue(e) || ((Ue(e) || "string" == typeof e) && !!Ge(e.replace(nt, ""), t, n) || null)
        }

        function qe(e, t, n, r) {
            var i = [],
                o = 0,
                a = t.length;
            if (r !== O)
                for (; o < a; o++) e[t[o]][n] && i.push(e[t[o]][n][r]);
            else
                for (; o < a; o++) i.push(e[t[o]][n]);
            return i
        }

        function Ye(e, t) {
            var n = [];
            if (t === O) {
                t = 0;
                var r = e
            } else r = t, t = e;
            for (e = t; e < r; e++) n.push(e);
            return n
        }

        function Xe(e) {
            for (var t = [], n = 0, r = e.length; n < r; n++) e[n] && t.push(e[n]);
            return t
        }
        var Je, $e, Qe, Ke = function (v) {
            this.$ = function (e, t) {
                return this.api(!0).$(e, t)
            }, this._ = function (e, t) {
                return this.api(!0).rows(e, t).data()
            }, this.api = function (e) {
                return new yt(e ? ke(this[Je.iApiIndex]) : this)
            }, this.fnAddData = function (e, t) {
                var n = this.api(!0);
                return e = M.isArray(e) && (M.isArray(e[0]) || M.isPlainObject(e[0])) ? n.rows.add(e) : n.row.add(e), t !== O && !t || n.draw(), e.flatten().toArray()
            }, this.fnAdjustColumnSizing = function (e) {
                var t = this.api(!0).columns.adjust(),
                    n = t.settings()[0],
                    r = n.oScroll;
                e === O || e ? t.draw(!1) : "" === r.sX && "" === r.sY || he(n)
            }, this.fnClearTable = function (e) {
                var t = this.api(!0).clear();
                e !== O && !e || t.draw()
            }, this.fnClose = function (e) {
                this.api(!0).row(e).child.hide()
            }, this.fnDeleteRow = function (e, t, n) {
                var r = this.api(!0),
                    i = (e = r.rows(e)).settings()[0],
                    o = i.aoData[e[0][0]];
                return e.remove(), t && t.call(this, i, o), n !== O && !n || r.draw(), o
            }, this.fnDestroy = function (e) {
                this.api(!0).destroy(e)
            }, this.fnDraw = function (e) {
                this.api(!0).draw(e)
            }, this.fnFilter = function (e, t, n, r, i, o) {
                i = this.api(!0), null === t || t === O ? i.search(e, n, r, o) : i.column(t).search(e, n, r, o), i.draw()
            }, this.fnGetData = function (e, t) {
                var n = this.api(!0);
                if (e === O) return n.data().toArray();
                var r = e.nodeName ? e.nodeName.toLowerCase() : "";
                return t !== O || "td" == r || "th" == r ? n.cell(e, t).data() : n.row(e).data() || null
            }, this.fnGetNodes = function (e) {
                var t = this.api(!0);
                return e !== O ? t.row(e).node() : t.rows().nodes().flatten().toArray()
            }, this.fnGetPosition = function (e) {
                var t = this.api(!0),
                    n = e.nodeName.toUpperCase();
                return "TR" == n ? t.row(e).index() : "TD" == n || "TH" == n ? [(e = t.cell(e).index()).row, e.columnVisible, e.column] : null
            }, this.fnIsOpen = function (e) {
                return this.api(!0).row(e).child.isShown()
            }, this.fnOpen = function (e, t, n) {
                return this.api(!0).row(e).child(t, n).show().child()[0]
            }, this.fnPageChange = function (e, t) {
                e = this.api(!0).page(e), t !== O && !t || e.draw(!1)
            }, this.fnSetColumnVis = function (e, t, n) {
                e = this.api(!0).column(e).visible(t), n !== O && !n || e.columns.adjust().draw()
            }, this.fnSettings = function () {
                return ke(this[Je.iApiIndex])
            }, this.fnSort = function (e) {
                this.api(!0).order(e).draw()
            }, this.fnSortListener = function (e, t, n) {
                this.api(!0).order.listener(e, t, n)
            }, this.fnUpdate = function (e, t, n, r, i) {
                var o = this.api(!0);
                return n === O || null === n ? o.row(t).data(e) : o.cell(t, n).data(e), i !== O && !i || o.columns.adjust(), r !== O && !r || o.draw(), 0
            }, this.fnVersionCheck = Je.fnVersionCheck;
            var m = this,
                y = v === O,
                b = this.length;
            for (var e in y && (v = {}), this.oApi = this.internal = Je.internal, Ke.ext.internal) e && (this[e] = t(e));
            return this.each(function () {
                var n, e = {},
                    r = 1 < b ? Oe(e, v, !0) : v,
                    i = 0;
                e = this.getAttribute("id");
                var o = !1,
                    t = Ke.defaults,
                    a = M(this);
                if ("table" != this.nodeName.toLowerCase()) Pe(null, 0, "Non-table node initialisation (" + this.nodeName + ")", 2);
                else {
                    D(t), T(t.column), S(t, t, !0), S(t.column, t.column, !0), S(t, M.extend(r, a.data()), !0);
                    var s = Ke.settings;
                    for (i = 0, n = s.length; i < n; i++) {
                        var l = s[i];
                        if (l.nTable == this || l.nTHead && l.nTHead.parentNode == this || l.nTFoot && l.nTFoot.parentNode == this) {
                            var u = r.bRetrieve !== O ? r.bRetrieve : t.bRetrieve;
                            if (y || u) return l.oInstance;
                            if (r.bDestroy !== O ? r.bDestroy : t.bDestroy) {
                                l.oInstance.fnDestroy();
                                break
                            }
                            return void Pe(l, 0, "Cannot reinitialise DataTable", 3)
                        }
                        if (l.sTableId == this.id) {
                            s.splice(i, 1);
                            break
                        }
                    }
                    null !== e && "" !== e || (this.id = e = "DataTables_Table_" + Ke.ext._unique++);
                    var c = M.extend(!0, {}, Ke.models.oSettings, {
                        sDestroyWidth: a[0].style.width,
                        sInstance: e,
                        sTableId: e
                    });
                    c.nTable = this, c.oApi = m.internal, c.oInit = r, s.push(c), c.oInstance = 1 === m.length ? m : a.dataTable(), D(r), w(r.oLanguage), r.aLengthMenu && !r.iDisplayLength && (r.iDisplayLength = M.isArray(r.aLengthMenu[0]) ? r.aLengthMenu[0][0] : r.aLengthMenu[0]), r = Oe(M.extend(!0, {}, t), r), Me(c.oFeatures, r, "bPaginate bLengthChange bFilter bSort bSortMulti bInfo bProcessing bAutoWidth bSortClasses bServerSide bDeferRender".split(" ")), Me(c, r, ["asStripeClasses", "ajax", "fnServerData", "fnFormatNumber", "sServerMethod", "aaSorting", "aaSortingFixed", "aLengthMenu", "sPaginationType", "sAjaxSource", "sAjaxDataProp", "iStateDuration", "sDom", "bSortCellsTop", "iTabIndex", "fnStateLoadCallback", "fnStateSaveCallback", "renderer", "searchDelay", "rowId", ["iCookieDuration", "iStateDuration"],
                        ["oSearch", "oPreviousSearch"],
                        ["aoSearchCols", "aoPreSearchCols"],
                        ["iDisplayLength", "_iDisplayLength"]
                    ]), Me(c.oScroll, r, [
                        ["sScrollX", "sX"],
                        ["sScrollXInner", "sXInner"],
                        ["sScrollY", "sY"],
                        ["bScrollCollapse", "bCollapse"]
                    ]), Me(c.oLanguage, r, "fnInfoCallback"), Ae(c, "aoDrawCallback", r.fnDrawCallback, "user"), Ae(c, "aoServerParams", r.fnServerParams, "user"), Ae(c, "aoStateSaveParams", r.fnStateSaveParams, "user"), Ae(c, "aoStateLoadParams", r.fnStateLoadParams, "user"), Ae(c, "aoStateLoaded", r.fnStateLoaded, "user"), Ae(c, "aoRowCallback", r.fnRowCallback, "user"), Ae(c, "aoRowCreatedCallback", r.fnCreatedRow, "user"), Ae(c, "aoHeaderCallback", r.fnHeaderCallback, "user"), Ae(c, "aoFooterCallback", r.fnFooterCallback, "user"), Ae(c, "aoInitComplete", r.fnInitComplete, "user"), Ae(c, "aoPreDrawCallback", r.fnPreDrawCallback, "user"), c.rowIdFn = P(r.rowId), C(c);
                    var d = c.oClasses;
                    M.extend(d, Ke.ext.classes, r.oClasses), a.addClass(d.sTable), c.iInitDisplayStart === O && (c.iInitDisplayStart = r.iDisplayStart, c._iDisplayStart = r.iDisplayStart), null !== r.iDeferLoading && (c.bDeferLoading = !0, e = M.isArray(r.iDeferLoading), c._iRecordsDisplay = e ? r.iDeferLoading[0] : r.iDeferLoading, c._iRecordsTotal = e ? r.iDeferLoading[1] : r.iDeferLoading);
                    var f = c.oLanguage;
                    M.extend(!0, f, r.oLanguage), f.sUrl && (M.ajax({
                        dataType: "json",
                        url: f.sUrl,
                        success: function (e) {
                            w(e), S(t.oLanguage, e), M.extend(!0, f, e), oe(c)
                        },
                        error: function () {
                            oe(c)
                        }
                    }), o = !0), null === r.asStripeClasses && (c.asStripeClasses = [d.sStripeOdd, d.sStripeEven]), e = c.asStripeClasses;
                    var p = a.children("tbody").find("tr").eq(0);
                    if (-1 !== M.inArray(!0, M.map(e, function (e, t) {
                        return p.hasClass(e)
                    })) && (M("tbody tr", this).removeClass(e.join(" ")), c.asDestroyStripes = e.slice()), e = [], 0 !== (s = this.getElementsByTagName("thead")).length && (U(c.aoHeader, s[0]), e = W(c)), null === r.aoColumns)
                        for (s = [], i = 0, n = e.length; i < n; i++) s.push(null);
                    else s = r.aoColumns;
                    for (i = 0, n = s.length; i < n; i++) E(c, e ? e[i] : null);
                    if (I(c, r.aoColumnDefs, s, function (e, t) {
                        _(c, e, t)
                    }), p.length) {
                        function h(e, t) {
                            return null !== e.getAttribute("data-" + t) ? t : null
                        }
                        M(p[0]).children("th, td").each(function (e, t) {
                            var n = c.aoColumns[e];
                            if (n.mData === e) {
                                var r = h(t, "sort") || h(t, "order");
                                t = h(t, "filter") || h(t, "search"), null === r && null === t || (n.mData = {
                                    _: e + ".display",
                                    sort: null !== r ? e + ".@data-" + r : O,
                                    type: null !== r ? e + ".@data-" + r : O,
                                    filter: null !== t ? e + ".@data-" + t : O
                                }, _(c, e))
                            }
                        })
                    }
                    var g = c.oFeatures;
                    e = function () {
                        if (r.aaSorting === O) {
                            var e = c.aaSorting;
                            for (i = 0, n = e.length; i < n; i++) e[i][1] = c.aoColumns[i].asSorting[0]
                        }
                        _e(c), g.bSort && Ae(c, "aoDrawCallback", function () {
                            if (c.bSorted) {
                                var e = we(c),
                                    n = {};
                                M.each(e, function (e, t) {
                                    n[t.src] = t.dir
                                }), Ne(c, null, "order", [c, e, n]), Te(c)
                            }
                        }), Ae(c, "aoDrawCallback", function () {
                            (c.bSorted || "ssp" === ze(c) || g.bDeferRender) && _e(c)
                        }, "sc"), e = a.children("caption").each(function () {
                            this._captionSide = M(this).css("caption-side")
                        });
                        var t = a.children("thead");
                        if (0 === t.length && (t = M("<thead/>").appendTo(a)), c.nTHead = t[0], 0 === (t = a.children("tbody")).length && (t = M("<tbody/>").appendTo(a)), c.nTBody = t[0], 0 === (t = a.children("tfoot")).length && 0 < e.length && ("" !== c.oScroll.sX || "" !== c.oScroll.sY) && (t = M("<tfoot/>").appendTo(a)), 0 === t.length || 0 === t.children().length ? a.addClass(d.sNoFooter) : 0 < t.length && (c.nTFoot = t[0], U(c.aoFooter, c.nTFoot)), r.aaData)
                            for (i = 0; i < r.aaData.length; i++) R(c, r.aaData[i]);
                        else !c.bDeferLoading && "dom" != ze(c) || k(c, M(c.nTBody).children("tr"));
                        c.aiDisplay = c.aiDisplayMaster.slice(), !(c.bInitialised = !0) === o && oe(c)
                    }, r.bStateSave ? (g.bStateSave = !0, Ae(c, "aoDrawCallback", Ie, "state_save"), Re(c, r, e)) : e()
                }
            }), m = null, this
        },
            et = {},
            tt = /[\r\n\u2028]/g,
            nt = /<.*?>/g,
            rt = /^\d{2,4}[\.\/\-]\d{1,2}[\.\/\-]\d{1,2}([T ]{1}\d{1,2}[:\.]\d{2}([\.:]\d{2})?)?$/,
            it = /(\/|\.|\*|\+|\?|\||\(|\)|\[|\]|\{|\}|\\|\$|\^|\-)/g,
            ot = /[',$£€¥%\u2009\u202F\u20BD\u20a9\u20BArfkɃΞ]/gi,
            at = function (e, t, n) {
                var r = [],
                    i = 0,
                    o = e.length;
                if (n !== O)
                    for (; i < o; i++) e[i] && e[i][t] && r.push(e[i][t][n]);
                else
                    for (; i < o; i++) e[i] && r.push(e[i][t]);
                return r
            },
            st = function (e) {
                e: {
                    if (!(e.length < 2))
                        for (var t = e.slice().sort(), n = t[0], r = 1, i = t.length; r < i; r++) {
                            if (t[r] === n) {
                                t = !1;
                                break e
                            }
                            n = t[r]
                        }
                    t = !0
                }
                if (t) return e.slice(); t = [],
                    i = e.length;
                var o, a = 0; r = 0; e: for (; r < i; r++) {
                    for (n = e[r], o = 0; o < a; o++)
                        if (t[o] === n) continue e;
                    t.push(n), a++
                }
                return t
            };
        Ke.util = {
            throttle: function (r, e) {
                var i, o, a = e !== O ? e : 200;
                return function () {
                    var e = this,
                        t = +new Date,
                        n = arguments;
                    i && t < i + a ? (clearTimeout(o), o = setTimeout(function () {
                        i = O, r.apply(e, n)
                    }, a)) : (i = t, r.apply(e, n))
                }
            },
            escapeRegex: function (e) {
                return e.replace(it, "\\$1")
            }
        };
        var lt = function (e, t, n) {
            e[t] !== O && (e[n] = e[t])
        },
            ut = /\[.*?\]$/,
            ct = /\(\)$/,
            dt = Ke.util.escapeRegex,
            ft = M("<div>")[0],
            pt = ft.textContent !== O,
            ht = /<.*?>/g,
            gt = Ke.util.throttle,
            vt = [],
            mt = Array.prototype,
            yt = function (e, t) {
                if (!(this instanceof yt)) return new yt(e, t);

                function n(e) {
                    (e = function (e) {
                        var t, n = Ke.settings,
                            r = M.map(n, function (e, t) {
                                return e.nTable
                            });
                        if (!e) return [];
                        if (e.nTable && e.oApi) return [e];
                        if (e.nodeName && "table" === e.nodeName.toLowerCase()) {
                            var i = M.inArray(e, r);
                            return -1 !== i ? [n[i]] : null
                        }
                        return e && "function" == typeof e.settings ? e.settings().toArray() : ("string" == typeof e ? t = M(e) : e instanceof M && (t = e), t ? t.map(function (e) {
                            return -1 !== (i = M.inArray(this, r)) ? n[i] : null
                        }).toArray() : void 0)
                    }(e)) && r.push.apply(r, e)
                }
                var r = [];
                if (M.isArray(e))
                    for (var i = 0, o = e.length; i < o; i++) n(e[i]);
                else n(e);
                this.context = st(r), t && M.merge(this, t), this.selector = {
                    rows: null,
                    cols: null,
                    opts: null
                }, yt.extend(this, this, vt)
            };
        Ke.Api = yt, M.extend(yt.prototype, {
            any: function () {
                return 0 !== this.count()
            },
            concat: mt.concat,
            context: [],
            count: function () {
                return this.flatten().length
            },
            each: function (e) {
                for (var t = 0, n = this.length; t < n; t++) e.call(this, this[t], t, this);
                return this
            },
            eq: function (e) {
                var t = this.context;
                return t.length > e ? new yt(t[e], this[e]) : null
            },
            filter: function (e) {
                var t = [];
                if (mt.filter) t = mt.filter.call(this, e, this);
                else
                    for (var n = 0, r = this.length; n < r; n++) e.call(this, this[n], n, this) && t.push(this[n]);
                return new yt(this.context, t)
            },
            flatten: function () {
                var e = [];
                return new yt(this.context, e.concat.apply(e, this.toArray()))
            },
            join: mt.join,
            indexOf: mt.indexOf || function (e, t) {
                t = t || 0;
                for (var n = this.length; t < n; t++)
                    if (this[t] === e) return t;
                return -1
            },
            iterator: function (e, t, n, r) {
                var i, o, a, s = [],
                    l = this.context,
                    u = this.selector;
                "string" == typeof e && (r = n, n = t, t = e, e = !1);
                var c = 0;
                for (i = l.length; c < i; c++) {
                    var d = new yt(l[c]);
                    if ("table" === t) {
                        var f = n.call(d, l[c], c);
                        f !== O && s.push(f)
                    } else if ("columns" === t || "rows" === t) (f = n.call(d, l[c], this[c], c)) !== O && s.push(f);
                    else if ("column" === t || "column-rows" === t || "row" === t || "cell" === t) {
                        var p = this[c];
                        "column-rows" === t && (a = Tt(l[c], u.opts));
                        var h = 0;
                        for (o = p.length; h < o; h++) f = p[h], (f = "cell" === t ? n.call(d, l[c], f.row, f.column, c, h) : n.call(d, l[c], f, c, h, a)) !== O && s.push(f)
                    }
                }
                return s.length || r ? ((t = (e = new yt(l, e ? s.concat.apply([], s) : s)).selector).rows = u.rows, t.cols = u.cols, t.opts = u.opts, e) : this
            },
            lastIndexOf: mt.lastIndexOf || function (e, t) {
                return this.indexOf.apply(this.toArray.reverse(), arguments)
            },
            length: 0,
            map: function (e) {
                var t = [];
                if (mt.map) t = mt.map.call(this, e, this);
                else
                    for (var n = 0, r = this.length; n < r; n++) t.push(e.call(this, this[n], n));
                return new yt(this.context, t)
            },
            pluck: function (t) {
                return this.map(function (e) {
                    return e[t]
                })
            },
            pop: mt.pop,
            push: mt.push,
            reduce: mt.reduce || function (e, t) {
                return n(this, e, t, 0, this.length, 1)
            },
            reduceRight: mt.reduceRight || function (e, t) {
                return n(this, e, t, this.length - 1, -1, -1)
            },
            reverse: mt.reverse,
            selector: null,
            shift: mt.shift,
            slice: function () {
                return new yt(this.context, this)
            },
            sort: mt.sort,
            splice: mt.splice,
            toArray: function () {
                return mt.slice.call(this)
            },
            to$: function () {
                return M(this)
            },
            toJQuery: function () {
                return M(this)
            },
            unique: function () {
                return new yt(this.context, st(this))
            },
            unshift: mt.unshift
        }), yt.extend = function (e, t, n) {
            if (n.length && t && (t instanceof yt || t.__dt_wrapper)) {
                function r(t, n, r) {
                    return function () {
                        var e = n.apply(t, arguments);
                        return yt.extend(e, e, r.methodExt), e
                    }
                }
                var i, o = 0;
                for (i = n.length; o < i; o++) {
                    var a = n[o];
                    t[a.name] = "function" === a.type ? r(e, a.val, a) : "object" === a.type ? {} : a.val, t[a.name].__dt_wrapper = !0, yt.extend(e, t[a.name], a.propExt)
                }
            }
        }, yt.register = $e = function (e, t) {
            if (M.isArray(e))
                for (var n = 0, r = e.length; n < r; n++) yt.register(e[n], t);
            else {
                r = e.split(".");
                var i, o = vt;
                for (e = 0, n = r.length; e < n; e++) {
                    var a = (i = -1 !== r[e].indexOf("()")) ? r[e].replace("()", "") : r[e];
                    e: {
                        for (var s = 0, l = o.length; s < l; s++)
                            if (o[s].name === a) {
                                s = o[s];
                                break e
                            } s = null
                    }
                    s || (s = {
                        name: a,
                        val: {},
                        methodExt: [],
                        propExt: [],
                        type: "object"
                    }, o.push(s)), e === n - 1 ? (s.val = t, s.type = "function" == typeof t ? "function" : M.isPlainObject(t) ? "object" : "other") : o = i ? s.methodExt : s.propExt
                }
            }
        }, yt.registerPlural = Qe = function (e, t, n) {
            yt.register(e, n), yt.register(t, function () {
                var e = n.apply(this, arguments);
                return e === this ? this : e instanceof yt ? e.length ? M.isArray(e[0]) ? new yt(e.context, e[0]) : e[0] : O : e
            })
        };
        $e("tables()", function (e) {
            return e ? new yt(function (e, t) {
                if ("number" == typeof e) return [t[e]];
                var n = M.map(t, function (e, t) {
                    return e.nTable
                });
                return M(n).filter(e).map(function (e) {
                    return e = M.inArray(this, n), t[e]
                }).toArray()
            }(e, this.context)) : this
        }), $e("table()", function (e) {
            var t = (e = this.tables(e)).context;
            return t.length ? new yt(t[0]) : e
        }), Qe("tables().nodes()", "table().node()", function () {
            return this.iterator("table", function (e) {
                return e.nTable
            }, 1)
        }), Qe("tables().body()", "table().body()", function () {
            return this.iterator("table", function (e) {
                return e.nTBody
            }, 1)
        }), Qe("tables().header()", "table().header()", function () {
            return this.iterator("table", function (e) {
                return e.nTHead
            }, 1)
        }), Qe("tables().footer()", "table().footer()", function () {
            return this.iterator("table", function (e) {
                return e.nTFoot
            }, 1)
        }), Qe("tables().containers()", "table().container()", function () {
            return this.iterator("table", function (e) {
                return e.nTableWrapper
            }, 1)
        }), $e("draw()", function (t) {
            return this.iterator("table", function (e) {
                "page" === t ? z(e) : ("string" == typeof t && (t = "full-hold" !== t), B(e, !1 === t))
            })
        }), $e("page()", function (t) {
            return t === O ? this.page.info().page : this.iterator("table", function (e) {
                ce(e, t)
            })
        }), $e("page.info()", function (e) {
            if (0 === this.context.length) return O;
            var t = (e = this.context[0])._iDisplayStart,
                n = e.oFeatures.bPaginate ? e._iDisplayLength : -1,
                r = e.fnRecordsDisplay(),
                i = -1 === n;
            return {
                page: i ? 0 : Math.floor(t / n),
                pages: i ? 1 : Math.ceil(r / n),
                start: t,
                end: e.fnDisplayEnd(),
                length: n,
                recordsTotal: e.fnRecordsTotal(),
                recordsDisplay: r,
                serverSide: "ssp" === ze(e)
            }
        }), $e("page.len()", function (t) {
            return t === O ? 0 !== this.context.length ? this.context[0]._iDisplayLength : O : this.iterator("table", function (e) {
                se(e, t)
            })
        });

        function bt(r, i, e) {
            if (e) {
                var t = new yt(r);
                t.one("draw", function () {
                    e(t.ajax.json())
                })
            }
            if ("ssp" == ze(r)) B(r, i);
            else {
                fe(r, !0);
                var n = r.jqXHR;
                n && 4 !== n.readyState && n.abort(), V(r, [], function (e) {
                    l(r);
                    for (var t = 0, n = (e = Z(r, e)).length; t < n; t++) R(r, e[t]);
                    B(r, i), fe(r, !1)
                })
            }
        }
        $e("ajax.json()", function () {
            var e = this.context;
            if (0 < e.length) return e[0].json
        }), $e("ajax.params()", function () {
            var e = this.context;
            if (0 < e.length) return e[0].oAjaxData
        }), $e("ajax.reload()", function (t, n) {
            return this.iterator("table", function (e) {
                bt(e, !1 === n, t)
            })
        }), $e("ajax.url()", function (t) {
            var e = this.context;
            return t === O ? 0 === e.length ? O : (e = e[0]).ajax ? M.isPlainObject(e.ajax) ? e.ajax.url : e.ajax : e.sAjaxSource : this.iterator("table", function (e) {
                M.isPlainObject(e.ajax) ? e.ajax.url = t : e.ajax = t
            })
        }), $e("ajax.url().load()", function (t, n) {
            return this.iterator("table", function (e) {
                bt(e, !1 === n, t)
            })
        });

        function St(e, t, n, r, i) {
            var o, a, s, l = [],
                u = typeof t;
            for (t && "string" !== u && "function" !== u && t.length !== O || (t = [t]), u = 0, a = t.length; u < a; u++) {
                var c = t[u] && t[u].split && !t[u].match(/[\[\(:]/) ? t[u].split(",") : [t[u]],
                    d = 0;
                for (s = c.length; d < s; d++)(o = n("string" == typeof c[d] ? M.trim(c[d]) : c[d])) && o.length && (l = l.concat(o))
            }
            if ((e = Je.selector[e]).length)
                for (u = 0, a = e.length; u < a; u++) l = e[u](r, i, l);
            return st(l)
        }

        function wt(e) {
            return (e = e || {}).filter && e.search === O && (e.search = e.filter), M.extend({
                search: "none",
                order: "current",
                page: "all"
            }, e)
        }

        function Dt(e) {
            for (var t = 0, n = e.length; t < n; t++)
                if (0 < e[t].length) return e[0] = e[t], e[0].length = 1, e.length = 1, e.context = [e.context[t]], e;
            return e.length = 0, e
        }
        var Tt = function (e, t) {
            var n = [],
                r = e.aiDisplay,
                i = e.aiDisplayMaster,
                o = t.search,
                a = t.order;
            if (t = t.page, "ssp" == ze(e)) return "removed" === o ? [] : Ye(0, i.length);
            if ("current" == t)
                for (a = e._iDisplayStart, e = e.fnDisplayEnd(); a < e; a++) n.push(r[a]);
            else if ("current" == a || "applied" == a) {
                if ("none" == o) n = i.slice();
                else if ("applied" == o) n = r.slice();
                else if ("removed" == o) {
                    var s = {};
                    for (a = 0, e = r.length; a < e; a++) s[r[a]] = null;
                    n = M.map(i, function (e) {
                        return s.hasOwnProperty(e) ? null : e
                    })
                }
            } else if ("index" == a || "original" == a)
                for (a = 0, e = e.aoData.length; a < e; a++) "none" == o ? n.push(a) : (-1 === (i = M.inArray(a, r)) && "removed" == o || 0 <= i && "applied" == o) && n.push(a);
            return n
        };
        $e("rows()", function (t, n) {
            t === O ? t = "" : M.isPlainObject(t) && (n = t, t = ""), n = wt(n);
            var e = this.iterator("table", function (e) {
                return function (i, e, o) {
                    var a;
                    return St("row", e, function (n) {
                        var e = We(n),
                            r = i.aoData;
                        if (null !== e && !o) return [e];
                        if (a = a || Tt(i, o), null !== e && -1 !== M.inArray(e, a)) return [e];
                        if (null === n || n === O || "" === n) return a;
                        if ("function" == typeof n) return M.map(a, function (e) {
                            var t = r[e];
                            return n(e, t._aData, t.nTr) ? e : null
                        });
                        if (n.nodeName) {
                            e = n._DT_RowIndex;
                            var t = n._DT_CellIndex;
                            return e !== O ? r[e] && r[e].nTr === n ? [e] : [] : t ? r[t.row] && r[t.row].nTr === n.parentNode ? [t.row] : [] : (e = M(n).closest("*[data-dt-row]")).length ? [e.data("dt-row")] : []
                        }
                        return "string" == typeof n && "#" === n.charAt(0) && (e = i.aIds[n.replace(/^#/, "")]) !== O ? [e.idx] : (e = Xe(qe(i.aoData, a, "nTr")), M(e).filter(n).map(function () {
                            return this._DT_RowIndex
                        }).toArray())
                    }, i, o)
                }(e, t, n)
            }, 1);
            return e.selector.rows = t, e.selector.opts = n, e
        }), $e("rows().nodes()", function () {
            return this.iterator("row", function (e, t) {
                return e.aoData[t].nTr || O
            }, 1)
        }), $e("rows().data()", function () {
            return this.iterator(!0, "rows", function (e, t) {
                return qe(e.aoData, t, "_aData")
            }, 1)
        }), Qe("rows().cache()", "row().cache()", function (n) {
            return this.iterator("row", function (e, t) {
                return e = e.aoData[t], "search" === n ? e._aFilterData : e._aSortData
            }, 1)
        }), Qe("rows().invalidate()", "row().invalidate()", function (n) {
            return this.iterator("row", function (e, t) {
                i(e, t, n)
            })
        }), Qe("rows().indexes()", "row().index()", function () {
            return this.iterator("row", function (e, t) {
                return t
            }, 1)
        }), Qe("rows().ids()", "row().id()", function (e) {
            for (var t = [], n = this.context, r = 0, i = n.length; r < i; r++)
                for (var o = 0, a = this[r].length; o < a; o++) {
                    var s = n[r].rowIdFn(n[r].aoData[this[r][o]]._aData);
                    t.push((!0 === e ? "#" : "") + s)
                }
            return new yt(n, t)
        }), Qe("rows().remove()", "row().remove()", function () {
            var c = this;
            return this.iterator("row", function (e, t, n) {
                var r, i, o = e.aoData,
                    a = o[t];
                o.splice(t, 1);
                var s = 0;
                for (r = o.length; s < r; s++) {
                    var l = o[s],
                        u = l.anCells;
                    if (null !== l.nTr && (l.nTr._DT_RowIndex = s), null !== u)
                        for (l = 0, i = u.length; l < i; l++) u[l]._DT_CellIndex.row = s
                }
                d(e.aiDisplayMaster, t), d(e.aiDisplay, t), d(c[n], t, !1), 0 < e._iRecordsDisplay && e._iRecordsDisplay--, Le(e), (t = e.rowIdFn(a._aData)) !== O && delete e.aIds[t]
            }), this.iterator("table", function (e) {
                for (var t = 0, n = e.aoData.length; t < n; t++) e.aoData[t].idx = t
            }), this
        }), $e("rows.add()", function (o) {
            var e = this.iterator("table", function (e) {
                var t, n = [],
                    r = 0;
                for (t = o.length; r < t; r++) {
                    var i = o[r];
                    i.nodeName && "TR" === i.nodeName.toUpperCase() ? n.push(k(e, i)[0]) : n.push(R(e, i))
                }
                return n
            }, 1),
                t = this.rows(-1);
            return t.pop(), M.merge(t, e), t
        }), $e("row()", function (e, t) {
            return Dt(this.rows(e, t))
        }), $e("row().data()", function (e) {
            var t = this.context;
            if (e === O) return t.length && this.length ? t[0].aoData[this[0]]._aData : O;
            var n = t[0].aoData[this[0]];
            return n._aData = e, M.isArray(e) && n.nTr.id && h(t[0].rowId)(e, n.nTr.id), i(t[0], this[0], "data"), this
        }), $e("row().node()", function () {
            var e = this.context;
            return e.length && this.length && e[0].aoData[this[0]].nTr || null
        }), $e("row.add()", function (t) {
            t instanceof M && t.length && (t = t[0]);
            var e = this.iterator("table", function (e) {
                return t.nodeName && "TR" === t.nodeName.toUpperCase() ? k(e, t)[0] : R(e, t)
            });
            return this.row(e[0])
        });

        function Ct(e, t) {
            var n = e.context;
            n.length && (e = n[0].aoData[t !== O ? t : e[0]]) && e._details && (e._details.remove(), e._detailsShow = O, e._details = O)
        }

        function Et(e, t) {
            var n = e.context;
            n.length && e.length && ((e = n[0].aoData[e[0]])._details && ((e._detailsShow = t) ? e._details.insertAfter(e.nTr) : e._details.detach(), _t(n[0])))
        }
        var _t = function (i) {
            var n = new yt(i),
                o = i.aoData;
            n.off("draw.dt.DT_details column-visibility.dt.DT_details destroy.dt.DT_details"), 0 < at(o, "_details").length && (n.on("draw.dt.DT_details", function (e, t) {
                i === t && n.rows({
                    page: "current"
                }).eq(0).each(function (e) {
                    (e = o[e])._detailsShow && e._details.insertAfter(e.nTr)
                })
            }), n.on("column-visibility.dt.DT_details", function (e, t, n, r) {
                if (i === t)
                    for (t = y(t), n = 0, r = o.length; n < r; n++)(e = o[n])._details && e._details.children("td[colspan]").attr("colspan", t)
            }), n.on("destroy.dt.DT_details", function (e, t) {
                if (i === t)
                    for (e = 0, t = o.length; e < t; e++) o[e]._details && Ct(n, e)
            }))
        };
        $e("row().child()", function (e, t) {
            var n = this.context;
            return e === O ? n.length && this.length ? n[0].aoData[this[0]]._details : O : (!0 === e ? this.child.show() : !1 === e ? Ct(this) : n.length && this.length && function (i, e, t, n) {
                var o = [],
                    a = function (e, t) {
                        if (M.isArray(e) || e instanceof M)
                            for (var n = 0, r = e.length; n < r; n++) a(e[n], t);
                        else e.nodeName && "tr" === e.nodeName.toLowerCase() ? o.push(e) : (n = M("<tr><td/></tr>").addClass(t), M("td", n).addClass(t).html(e)[0].colSpan = y(i), o.push(n[0]))
                    };
                a(t, n), e._details && e._details.detach(), e._details = M(o), e._detailsShow && e._details.insertAfter(e.nTr)
            }(n[0], n[0].aoData[this[0]], e, t), this)
        }), $e(["row().child.show()", "row().child().show()"], function (e) {
            return Et(this, !0), this
        }), $e(["row().child.hide()", "row().child().hide()"], function () {
            return Et(this, !1), this
        }), $e(["row().child.remove()", "row().child().remove()"], function () {
            return Ct(this), this
        }), $e("row().child.isShown()", function () {
            var e = this.context;
            return e.length && this.length && e[0].aoData[this[0]]._detailsShow || !1
        });

        function xt(e, t, n, r, i) {
            n = [], r = 0;
            for (var o = i.length; r < o; r++) n.push(g(e, i[r], t));
            return n
        }
        var It = /^([^:]+):(name|visIdx|visible)$/;
        $e("columns()", function (t, n) {
            t === O ? t = "" : M.isPlainObject(t) && (n = t, t = ""), n = wt(n);
            var e = this.iterator("table", function (e) {
                return function (o, e, a) {
                    var s = o.aoColumns,
                        l = at(s, "sName"),
                        u = at(s, "nTh");
                    return St("column", e, function (n) {
                        var e = We(n);
                        if ("" === n) return Ye(s.length);
                        if (null !== e) return [0 <= e ? e : s.length + e];
                        if ("function" == typeof n) {
                            var r = Tt(o, a);
                            return M.map(s, function (e, t) {
                                return n(t, xt(o, t, 0, 0, r), u[t]) ? t : null
                            })
                        }
                        var i = "string" == typeof n ? n.match(It) : "";
                        if (i) switch (i[2]) {
                            case "visIdx":
                            case "visible":
                                if ((e = parseInt(i[1], 10)) < 0) {
                                    var t = M.map(s, function (e, t) {
                                        return e.bVisible ? t : null
                                    });
                                    return [t[t.length + e]]
                                }
                                return [A(o, e)];
                            case "name":
                                return M.map(l, function (e, t) {
                                    return e === i[1] ? t : null
                                });
                            default:
                                return []
                        }
                        return n.nodeName && n._DT_CellIndex ? [n._DT_CellIndex.column] : (e = M(u).filter(n).map(function () {
                            return M.inArray(this, u)
                        }).toArray()).length || !n.nodeName ? e : (e = M(n).closest("*[data-dt-column]")).length ? [e.data("dt-column")] : []
                    }, o, a)
                }(e, t, n)
            }, 1);
            return e.selector.cols = t, e.selector.opts = n, e
        }), Qe("columns().header()", "column().header()", function (e, t) {
            return this.iterator("column", function (e, t) {
                return e.aoColumns[t].nTh
            }, 1)
        }), Qe("columns().footer()", "column().footer()", function (e, t) {
            return this.iterator("column", function (e, t) {
                return e.aoColumns[t].nTf
            }, 1)
        }), Qe("columns().data()", "column().data()", function () {
            return this.iterator("column-rows", xt, 1)
        }), Qe("columns().dataSrc()", "column().dataSrc()", function () {
            return this.iterator("column", function (e, t) {
                return e.aoColumns[t].mData
            }, 1)
        }), Qe("columns().cache()", "column().cache()", function (o) {
            return this.iterator("column-rows", function (e, t, n, r, i) {
                return qe(e.aoData, i, "search" === o ? "_aFilterData" : "_aSortData", t)
            }, 1)
        }), Qe("columns().nodes()", "column().nodes()", function () {
            return this.iterator("column-rows", function (e, t, n, r, i) {
                return qe(e.aoData, i, "anCells", t)
            }, 1)
        }), Qe("columns().visible()", "column().visible()", function (l, n) {
            var t = this,
                e = this.iterator("column", function (e, t) {
                    if (l === O) return e.aoColumns[t].bVisible;
                    var n, r = e.aoColumns,
                        i = r[t],
                        o = e.aoData;
                    if (l !== O && i.bVisible !== l) {
                        if (l) {
                            var a = M.inArray(!0, at(r, "bVisible"), t + 1);
                            for (r = 0, n = o.length; r < n; r++) {
                                var s = o[r].nTr;
                                e = o[r].anCells, s && s.insertBefore(e[t], e[a] || null)
                            }
                        } else M(at(e.aoData, "anCells", t)).detach();
                        i.bVisible = l
                    }
                });
            return l !== O && this.iterator("table", function (e) {
                F(e, e.aoHeader), F(e, e.aoFooter), e.aiDisplay.length || M(e.nTBody).find("td[colspan]").attr("colspan", y(e)), Ie(e), t.iterator("column", function (e, t) {
                    Ne(e, null, "column-visibility", [e, t, l, n])
                }), n !== O && !n || t.columns.adjust()
            }), e
        }), Qe("columns().indexes()", "column().index()", function (n) {
            return this.iterator("column", function (e, t) {
                return "visible" === n ? u(e, t) : t
            }, 1)
        }), $e("columns.adjust()", function () {
            return this.iterator("table", function (e) {
                H(e)
            }, 1)
        }), $e("column.index()", function (e, t) {
            if (0 !== this.context.length) {
                var n = this.context[0];
                if ("fromVisible" === e || "toData" === e) return A(n, t);
                if ("fromData" === e || "toVisible" === e) return u(n, t)
            }
        }), $e("column()", function (e, t) {
            return Dt(this.columns(e, t))
        });
        $e("cells()", function (t, e, n) {
            if (M.isPlainObject(t) && (t.row === O ? (n = t, t = null) : (n = e, e = null)), M.isPlainObject(e) && (n = e, e = null), null === e || e === O) return this.iterator("table", function (e) {
                return function (n, e, t) {
                    var r, i, o, a, s, l, u, c = n.aoData,
                        d = Tt(n, t),
                        f = Xe(qe(c, d, "anCells")),
                        p = M([].concat.apply([], f)),
                        h = n.aoColumns.length;
                    return St("cell", e, function (e) {
                        var t = "function" == typeof e;
                        if (null === e || e === O || t) {
                            for (i = [], o = 0, a = d.length; o < a; o++)
                                for (r = d[o], s = 0; s < h; s++) l = {
                                    row: r,
                                    column: s
                                }, t ? (u = c[r], e(l, g(n, r, s), u.anCells ? u.anCells[s] : null) && i.push(l)) : i.push(l);
                            return i
                        }
                        return M.isPlainObject(e) ? e.column !== O && e.row !== O && -1 !== M.inArray(e.row, d) ? [e] : [] : (t = p.filter(e).map(function (e, t) {
                            return {
                                row: t._DT_CellIndex.row,
                                column: t._DT_CellIndex.column
                            }
                        }).toArray()).length || !e.nodeName ? t : (u = M(e).closest("*[data-dt-row]")).length ? [{
                            row: u.data("dt-row"),
                            column: u.data("dt-column")
                        }] : []
                    }, n, t)
                }(e, t, wt(n))
            });
            var r, i, o, a, s = n ? {
                page: n.page,
                order: n.order,
                search: n.search
            } : {},
                l = this.columns(e, s),
                u = this.rows(t, s);
            return s = this.iterator("table", function (e, t) {
                for (e = [], r = 0, i = u[t].length; r < i; r++)
                    for (o = 0, a = l[t].length; o < a; o++) e.push({
                        row: u[t][r],
                        column: l[t][o]
                    });
                return e
            }, 1), s = n && n.selected ? this.cells(s, n) : s, M.extend(s.selector, {
                cols: e,
                rows: t,
                opts: n
            }), s
        }), Qe("cells().nodes()", "cell().node()", function () {
            return this.iterator("cell", function (e, t, n) {
                return (e = e.aoData[t]) && e.anCells ? e.anCells[n] : O
            }, 1)
        }), $e("cells().data()", function () {
            return this.iterator("cell", function (e, t, n) {
                return g(e, t, n)
            }, 1)
        }), Qe("cells().cache()", "cell().cache()", function (r) {
            return r = "search" === r ? "_aFilterData" : "_aSortData", this.iterator("cell", function (e, t, n) {
                return e.aoData[t][r][n]
            }, 1)
        }), Qe("cells().render()", "cell().render()", function (r) {
            return this.iterator("cell", function (e, t, n) {
                return g(e, t, n, r)
            }, 1)
        }), Qe("cells().indexes()", "cell().index()", function () {
            return this.iterator("cell", function (e, t, n) {
                return {
                    row: t,
                    column: n,
                    columnVisible: u(e, n)
                }
            }, 1)
        }), Qe("cells().invalidate()", "cell().invalidate()", function (r) {
            return this.iterator("cell", function (e, t, n) {
                i(e, t, r, n)
            })
        }), $e("cell()", function (e, t, n) {
            return Dt(this.cells(e, t, n))
        }), $e("cell().data()", function (e) {
            var t = this.context,
                n = this[0];
            return e === O ? t.length && n.length ? g(t[0], n[0].row, n[0].column) : O : (r(t[0], n[0].row, n[0].column, e), i(t[0], n[0].row, "data", n[0].column), this)
        }), $e("order()", function (t, e) {
            var n = this.context;
            return t === O ? 0 !== n.length ? n[0].aaSorting : O : ("number" == typeof t ? t = [
                [t, e]
            ] : t.length && !M.isArray(t[0]) && (t = Array.prototype.slice.call(arguments)), this.iterator("table", function (e) {
                e.aaSorting = t.slice()
            }))
        }), $e("order.listener()", function (t, n, r) {
            return this.iterator("table", function (e) {
                Ee(e, t, n, r)
            })
        }), $e("order.fixed()", function (t) {
            if (t) return this.iterator("table", function (e) {
                e.aaSortingFixed = M.extend(!0, {}, t)
            });
            var e = this.context;
            return e = e.length ? e[0].aaSortingFixed : O, M.isArray(e) ? {
                pre: e
            } : e
        }), $e(["columns().order()", "column().order()"], function (r) {
            var i = this;
            return this.iterator("table", function (e, t) {
                var n = [];
                M.each(i[t], function (e, t) {
                    n.push([t, r])
                }), e.aaSorting = n
            })
        }), $e("search()", function (t, n, r, i) {
            var e = this.context;
            return t === O ? 0 !== e.length ? e[0].oPreviousSearch.sSearch : O : this.iterator("table", function (e) {
                e.oFeatures.bFilter && Y(e, M.extend({}, e.oPreviousSearch, {
                    sSearch: t + "",
                    bRegex: null !== n && n,
                    bSmart: null === r || r,
                    bCaseInsensitive: null === i || i
                }), 1)
            })
        }), Qe("columns().search()", "column().search()", function (r, i, o, a) {
            return this.iterator("column", function (e, t) {
                var n = e.aoPreSearchCols;
                if (r === O) return n[t].sSearch;
                e.oFeatures.bFilter && (M.extend(n[t], {
                    sSearch: r + "",
                    bRegex: null !== i && i,
                    bSmart: null === o || o,
                    bCaseInsensitive: null === a || a
                }), Y(e, e.oPreviousSearch, 1))
            })
        }), $e("state()", function () {
            return this.context.length ? this.context[0].oSavedState : null
        }), $e("state.clear()", function () {
            return this.iterator("table", function (e) {
                e.fnStateSaveCallback.call(e.oInstance, e, {})
            })
        }), $e("state.loaded()", function () {
            return this.context.length ? this.context[0].oLoadedState : null
        }), $e("state.save()", function () {
            return this.iterator("table", function (e) {
                Ie(e)
            })
        }), Ke.versionCheck = Ke.fnVersionCheck = function (e) {
            for (var t, n, r = Ke.version.split("."), i = 0, o = (e = e.split(".")).length; i < o; i++)
                if ((t = parseInt(r[i], 10) || 0) !== (n = parseInt(e[i], 10) || 0)) return n < t;
            return !0
        }, Ke.isDataTable = Ke.fnIsDataTable = function (e) {
            var r = M(e).get(0),
                i = !1;
            return e instanceof Ke.Api || (M.each(Ke.settings, function (e, t) {
                e = t.nScrollHead ? M("table", t.nScrollHead)[0] : null;
                var n = t.nScrollFoot ? M("table", t.nScrollFoot)[0] : null;
                t.nTable !== r && e !== r && n !== r || (i = !0)
            }), i)
        }, Ke.tables = Ke.fnTables = function (t) {
            var e = !1;
            M.isPlainObject(t) && (e = t.api, t = t.visible);
            var n = M.map(Ke.settings, function (e) {
                if (!t || t && M(e.nTable).is(":visible")) return e.nTable
            });
            return e ? new yt(n) : n
        }, Ke.camelToHungarian = S, $e("$()", function (e, t) {
            return t = this.rows(t).nodes(), t = M(t), M([].concat(t.filter(e).toArray(), t.find(e).toArray()))
        }), M.each(["on", "one", "off"], function (e, n) {
            $e(n + "()", function () {
                var e = Array.prototype.slice.call(arguments);
                e[0] = M.map(e[0].split(/\s/), function (e) {
                    return e.match(/\.dt\b/) ? e : e + ".dt"
                }).join(" ");
                var t = M(this.tables().nodes());
                return t[n].apply(t, e), this
            })
        }), $e("clear()", function () {
            return this.iterator("table", function (e) {
                l(e)
            })
        }), $e("settings()", function () {
            return new yt(this.context, this.context)
        }), $e("init()", function () {
            var e = this.context;
            return e.length ? e[0].oInit : null
        }), $e("data()", function () {
            return this.iterator("table", function (e) {
                return at(e.aoData, "_aData")
            }).flatten()
        }), $e("destroy()", function (d) {
            return d = d || !1, this.iterator("table", function (t) {
                var e = t.nTableWrapper.parentNode,
                    n = t.oClasses,
                    r = t.nTable,
                    i = t.nTBody,
                    o = t.nTHead,
                    a = t.nTFoot,
                    s = M(r);
                i = M(i);
                var l, u = M(t.nTableWrapper),
                    c = M.map(t.aoData, function (e) {
                        return e.nTr
                    });
                t.bDestroying = !0, Ne(t, "aoDestroyCallback", "destroy", [t]), d || new yt(t).columns().visible(!0), u.off(".DT").find(":not(tbody *)").off(".DT"), M(m).off(".DT-" + t.sInstance), r != o.parentNode && (s.children("thead").detach(), s.append(o)), a && r != a.parentNode && (s.children("tfoot").detach(), s.append(a)), t.aaSorting = [], t.aaSortingFixed = [], _e(t), M(c).removeClass(t.asStripeClasses.join(" ")), M("th, td", o).removeClass(n.sSortable + " " + n.sSortableAsc + " " + n.sSortableDesc + " " + n.sSortableNone), i.children().detach(), i.append(c), s[o = d ? "remove" : "detach"](), u[o](), !d && e && (e.insertBefore(r, t.nTableReinsertBefore), s.css("width", t.sDestroyWidth).removeClass(n.sTable), (l = t.asDestroyStripes.length) && i.children().each(function (e) {
                    M(this).addClass(t.asDestroyStripes[e % l])
                })), -1 !== (e = M.inArray(t, Ke.settings)) && Ke.settings.splice(e, 1)
            })
        }), M.each(["column", "row", "cell"], function (e, l) {
            $e(l + "s().every()", function (o) {
                var a = this.selector.opts,
                    s = this;
                return this.iterator(l, function (e, t, n, r, i) {
                    o.call(s[l](t, "cell" === l ? n : a, "cell" === l ? a : O), t, n, r, i)
                })
            })
        }), $e("i18n()", function (e, t, n) {
            var r = this.context[0];
            return (e = P(e)(r.oLanguage)) === O && (e = t), n !== O && M.isPlainObject(e) && (e = e[n] !== O ? e[n] : e._), e.replace("%d", n)
        }), Ke.version = "1.10.20", Ke.settings = [], Ke.models = {}, Ke.models.oSearch = {
            bCaseInsensitive: !0,
            sSearch: "",
            bRegex: !1,
            bSmart: !0
        }, Ke.models.oRow = {
            nTr: null,
            anCells: null,
            _aData: [],
            _aSortData: null,
            _aFilterData: null,
            _sFilterRow: null,
            _sRowStripe: "",
            src: null,
            idx: -1
        }, Ke.models.oColumn = {
            idx: null,
            aDataSort: null,
            asSorting: null,
            bSearchable: null,
            bSortable: null,
            bVisible: null,
            _sManualType: null,
            _bAttrSrc: !1,
            fnCreatedCell: null,
            fnGetData: null,
            fnSetData: null,
            mData: null,
            mRender: null,
            nTh: null,
            nTf: null,
            sClass: null,
            sContentPadding: null,
            sDefaultContent: null,
            sName: null,
            sSortDataType: "std",
            sSortingClass: null,
            sSortingClassJUI: null,
            sTitle: null,
            sType: null,
            sWidth: null,
            sWidthOrig: null
        }, Ke.defaults = {
            aaData: null,
            aaSorting: [
                [0, "asc"]
            ],
            aaSortingFixed: [],
            ajax: null,
            aLengthMenu: [10, 25, 50, 100],
            aoColumns: null,
            aoColumnDefs: null,
            aoSearchCols: [],
            asStripeClasses: null,
            bAutoWidth: !0,
            bDeferRender: !1,
            bDestroy: !1,
            bFilter: !0,
            bInfo: !0,
            bLengthChange: !0,
            bPaginate: !0,
            bProcessing: !1,
            bRetrieve: !1,
            bScrollCollapse: !1,
            bServerSide: !1,
            bSort: !0,
            bSortMulti: !0,
            bSortCellsTop: !1,
            bSortClasses: !0,
            bStateSave: !1,
            fnCreatedRow: null,
            fnDrawCallback: null,
            fnFooterCallback: null,
            fnFormatNumber: function (e) {
                return e.toString().replace(/\B(?=(\d{3})+(?!\d))/g, this.oLanguage.sThousands)
            },
            fnHeaderCallback: null,
            fnInfoCallback: null,
            fnInitComplete: null,
            fnPreDrawCallback: null,
            fnRowCallback: null,
            fnServerData: null,
            fnServerParams: null,
            fnStateLoadCallback: function (e) {
                try {
                    return JSON.parse((-1 === e.iStateDuration ? sessionStorage : localStorage).getItem("DataTables_" + e.sInstance + "_" + location.pathname))
                } catch (e) { }
            },
            fnStateLoadParams: null,
            fnStateLoaded: null,
            fnStateSaveCallback: function (e, t) {
                try {
                    (-1 === e.iStateDuration ? sessionStorage : localStorage).setItem("DataTables_" + e.sInstance + "_" + location.pathname, JSON.stringify(t))
                } catch (e) { }
            },
            fnStateSaveParams: null,
            iStateDuration: 7200,
            iDeferLoading: null,
            iDisplayLength: 10,
            iDisplayStart: 0,
            iTabIndex: 0,
            oClasses: {},
            oLanguage: {
                oAria: {
                    sSortAscending: ": activate to sort column ascending",
                    sSortDescending: ": activate to sort column descending"
                },
                oPaginate: {
                    sFirst: "First",
                    sLast: "Last",
                    sNext: "Next",
                    sPrevious: "Previous"
                },
                sEmptyTable: "No data available in table",
                sInfo: "Showing _START_ to _END_ of _TOTAL_ entries",
                sInfoEmpty: "Showing 0 to 0 of 0 entries",
                sInfoFiltered: "(filtered from _MAX_ total entries)",
                sInfoPostFix: "",
                sDecimal: "",
                sThousands: ",",
                sLengthMenu: "Show _MENU_ entries",
                sLoadingRecords: "Loading...",
                sProcessing: "Processing...",
                sSearch: "Search:",
                sSearchPlaceholder: "",
                sUrl: "",
                sZeroRecords: "No matching records found"
            },
            oSearch: M.extend({}, Ke.models.oSearch),
            sAjaxDataProp: "data",
            sAjaxSource: null,
            sDom: "lfrtip",
            searchDelay: null,
            sPaginationType: "simple_numbers",
            sScrollX: "",
            sScrollXInner: "",
            sScrollY: "",
            sServerMethod: "GET",
            renderer: null,
            rowId: "DT_RowId"
        }, a(Ke.defaults), Ke.defaults.column = {
            aDataSort: null,
            iDataSort: -1,
            asSorting: ["asc", "desc"],
            bSearchable: !0,
            bSortable: !0,
            bVisible: !0,
            fnCreatedCell: null,
            mData: null,
            mRender: null,
            sCellType: "td",
            sClass: "",
            sContentPadding: "",
            sDefaultContent: null,
            sName: "",
            sSortDataType: "std",
            sTitle: null,
            sType: null,
            sWidth: null
        }, a(Ke.defaults.column), Ke.models.oSettings = {
            oFeatures: {
                bAutoWidth: null,
                bDeferRender: null,
                bFilter: null,
                bInfo: null,
                bLengthChange: null,
                bPaginate: null,
                bProcessing: null,
                bServerSide: null,
                bSort: null,
                bSortMulti: null,
                bSortClasses: null,
                bStateSave: null
            },
            oScroll: {
                bCollapse: null,
                iBarWidth: 0,
                sX: null,
                sXInner: null,
                sY: null
            },
            oLanguage: {
                fnInfoCallback: null
            },
            oBrowser: {
                bScrollOversize: !1,
                bScrollbarLeft: !1,
                bBounding: !1,
                barWidth: 0
            },
            ajax: null,
            aanFeatures: [],
            aoData: [],
            aiDisplay: [],
            aiDisplayMaster: [],
            aIds: {},
            aoColumns: [],
            aoHeader: [],
            aoFooter: [],
            oPreviousSearch: {},
            aoPreSearchCols: [],
            aaSorting: null,
            aaSortingFixed: [],
            asStripeClasses: null,
            asDestroyStripes: [],
            sDestroyWidth: 0,
            aoRowCallback: [],
            aoHeaderCallback: [],
            aoFooterCallback: [],
            aoDrawCallback: [],
            aoRowCreatedCallback: [],
            aoPreDrawCallback: [],
            aoInitComplete: [],
            aoStateSaveParams: [],
            aoStateLoadParams: [],
            aoStateLoaded: [],
            sTableId: "",
            nTable: null,
            nTHead: null,
            nTFoot: null,
            nTBody: null,
            nTableWrapper: null,
            bDeferLoading: !1,
            bInitialised: !1,
            aoOpenRows: [],
            sDom: null,
            searchDelay: null,
            sPaginationType: "two_button",
            iStateDuration: 0,
            aoStateSave: [],
            aoStateLoad: [],
            oSavedState: null,
            oLoadedState: null,
            sAjaxSource: null,
            sAjaxDataProp: null,
            bAjaxDataGet: !0,
            jqXHR: null,
            json: O,
            oAjaxData: O,
            fnServerData: null,
            aoServerParams: [],
            sServerMethod: null,
            fnFormatNumber: null,
            aLengthMenu: null,
            iDraw: 0,
            bDrawing: !1,
            iDrawError: -1,
            _iDisplayLength: 10,
            _iDisplayStart: 0,
            _iRecordsTotal: 0,
            _iRecordsDisplay: 0,
            oClasses: {},
            bFiltered: !1,
            bSorted: !1,
            bSortCellsTop: null,
            oInit: null,
            aoDestroyCallback: [],
            fnRecordsTotal: function () {
                return "ssp" == ze(this) ? 1 * this._iRecordsTotal : this.aiDisplayMaster.length
            },
            fnRecordsDisplay: function () {
                return "ssp" == ze(this) ? 1 * this._iRecordsDisplay : this.aiDisplay.length
            },
            fnDisplayEnd: function () {
                var e = this._iDisplayLength,
                    t = this._iDisplayStart,
                    n = t + e,
                    r = this.aiDisplay.length,
                    i = this.oFeatures,
                    o = i.bPaginate;
                return i.bServerSide ? !1 === o || -1 === e ? t + r : Math.min(t + e, this._iRecordsDisplay) : !o || r < n || -1 === e ? r : n
            },
            oInstance: null,
            sInstance: null,
            iTabIndex: 0,
            nScrollHead: null,
            nScrollFoot: null,
            aLastSort: [],
            oPlugins: {},
            rowIdFn: null,
            rowId: null
        }, Ke.ext = Je = {
            buttons: {},
            classes: {},
            build: "dt/dt-1.10.20",
            errMode: "alert",
            feature: [],
            search: [],
            selector: {
                cell: [],
                column: [],
                row: []
            },
            internal: {},
            legacy: {
                ajax: null
            },
            pager: {},
            renderer: {
                pageButton: {},
                header: {}
            },
            order: {},
            type: {
                detect: [],
                search: {},
                order: {}
            },
            _unique: 0,
            fnVersionCheck: Ke.fnVersionCheck,
            iApiIndex: 0,
            oJUIClasses: {},
            sVersion: Ke.version
        }, M.extend(Je, {
            afnFiltering: Je.search,
            aTypes: Je.type.detect,
            ofnSearch: Je.type.search,
            oSort: Je.type.order,
            afnSortData: Je.order,
            aoFeatures: Je.feature,
            oApi: Je.internal,
            oStdClasses: Je.classes,
            oPagination: Je.pager
        }), M.extend(Ke.ext.classes, {
            sTable: "dataTable",
            sNoFooter: "no-footer",
            sPageButton: "paginate_button",
            sPageButtonActive: "current",
            sPageButtonDisabled: "disabled",
            sStripeOdd: "odd",
            sStripeEven: "even",
            sRowEmpty: "dataTables_empty",
            sWrapper: "dataTables_wrapper",
            sFilter: "dataTables_filter",
            sInfo: "dataTables_info",
            sPaging: "dataTables_paginate paging_",
            sLength: "dataTables_length",
            sProcessing: "dataTables_processing",
            sSortAsc: "sorting_asc",
            sSortDesc: "sorting_desc",
            sSortable: "sorting",
            sSortableAsc: "sorting_asc_disabled",
            sSortableDesc: "sorting_desc_disabled",
            sSortableNone: "sorting_disabled",
            sSortColumn: "sorting_",
            sFilterInput: "",
            sLengthSelect: "",
            sScrollWrapper: "dataTables_scroll",
            sScrollHead: "dataTables_scrollHead",
            sScrollHeadInner: "dataTables_scrollHeadInner",
            sScrollBody: "dataTables_scrollBody",
            sScrollFoot: "dataTables_scrollFoot",
            sScrollFootInner: "dataTables_scrollFootInner",
            sHeaderTH: "",
            sFooterTH: "",
            sSortJUIAsc: "",
            sSortJUIDesc: "",
            sSortJUI: "",
            sSortJUIAscAllowed: "",
            sSortJUIDescAllowed: "",
            sSortJUIWrapper: "",
            sSortIcon: "",
            sJUIHeader: "",
            sJUIFooter: ""
        });
        var Rt = Ke.ext.pager;
        M.extend(Rt, {
            simple: function (e, t) {
                return ["previous", "next"]
            },
            full: function (e, t) {
                return ["first", "previous", "next", "last"]
            },
            numbers: function (e, t) {
                return [Be(e, t)]
            },
            simple_numbers: function (e, t) {
                return ["previous", Be(e, t), "next"]
            },
            full_numbers: function (e, t) {
                return ["first", "previous", Be(e, t), "next", "last"]
            },
            first_last_numbers: function (e, t) {
                return ["first", Be(e, t), "last"]
            },
            _numbers: Be,
            numbers_length: 7
        }), M.extend(!0, Ke.ext.renderer, {
            pageButton: {
                _: function (l, e, u, t, c, d) {
                    var f, p, h = l.oClasses,
                        g = l.oLanguage.oPaginate,
                        v = l.oLanguage.oAria.paginate || {},
                        m = 0,
                        y = function (e, t) {
                            function n(e) {
                                ce(l, e.data.action, !0)
                            }
                            var r, i = h.sPageButtonDisabled,
                                o = 0;
                            for (r = t.length; o < r; o++) {
                                var a = t[o];
                                if (M.isArray(a)) {
                                    var s = M("<" + (a.DT_el || "div") + "/>").appendTo(e);
                                    y(s, a)
                                } else {
                                    switch (f = null, p = a, s = l.iTabIndex, a) {
                                        case "ellipsis":
                                            e.append('<span class="ellipsis">&#x2026;</span>');
                                            break;
                                        case "first":
                                            f = g.sFirst, 0 === c && (s = -1, p += " " + i);
                                            break;
                                        case "previous":
                                            f = g.sPrevious, 0 === c && (s = -1, p += " " + i);
                                            break;
                                        case "next":
                                            f = g.sNext, c === d - 1 && (s = -1, p += " " + i);
                                            break;
                                        case "last":
                                            f = g.sLast, c === d - 1 && (s = -1, p += " " + i);
                                            break;
                                        default:
                                            f = a + 1, p = c === a ? h.sPageButtonActive : ""
                                    }
                                    null !== f && (He(s = M("<a>", {
                                        class: h.sPageButton + " " + p,
                                        "aria-controls": l.sTableId,
                                        "aria-label": v[a],
                                        "data-dt-idx": m,
                                        tabindex: s,
                                        id: 0 === u && "string" == typeof a ? l.sTableId + "_" + a : null
                                    }).html(f).appendTo(e), {
                                        action: a
                                    }, n), m++)
                                }
                            }
                        };
                    try {
                        var n = M(e).find(b.activeElement).data("dt-idx")
                    } catch (e) { }
                    y(M(e).empty(), t), n !== O && M(e).find("[data-dt-idx=" + n + "]").focus()
                }
            }
        }), M.extend(Ke.ext.type.detect, [function (e, t) {
            return t = t.oLanguage.sDecimal, Ge(e, t) ? "num" + t : null
        }, function (e, t) {
            return (!e || e instanceof Date || rt.test(e)) && (null !== (t = Date.parse(e)) && !isNaN(t) || Ue(e)) ? "date" : null
        }, function (e, t) {
            return t = t.oLanguage.sDecimal, Ge(e, t, !0) ? "num-fmt" + t : null
        }, function (e, t) {
            return t = t.oLanguage.sDecimal, Ze(e, t) ? "html-num" + t : null
        }, function (e, t) {
            return t = t.oLanguage.sDecimal, Ze(e, t, !0) ? "html-num-fmt" + t : null
        }, function (e, t) {
            return Ue(e) || "string" == typeof e && -1 !== e.indexOf("<") ? "html" : null
        }]), M.extend(Ke.ext.type.search, {
            html: function (e) {
                return Ue(e) ? e : "string" == typeof e ? e.replace(tt, " ").replace(nt, "") : ""
            },
            string: function (e) {
                return Ue(e) ? e : "string" == typeof e ? e.replace(tt, " ") : e
            }
        });
        var kt = function (e, t, n, r) {
            return 0 === e || e && "-" !== e ? (t && (e = Ve(e, t)), e.replace && (n && (e = e.replace(n, "")), r && (e = e.replace(r, ""))), 1 * e) : -1 / 0
        };
        M.extend(Je.type.order, {
            "date-pre": function (e) {
                return e = Date.parse(e), isNaN(e) ? -1 / 0 : e
            },
            "html-pre": function (e) {
                return Ue(e) ? "" : e.replace ? e.replace(/<.*?>/g, "").toLowerCase() : e + ""
            },
            "string-pre": function (e) {
                return Ue(e) ? "" : "string" == typeof e ? e.toLowerCase() : e.toString ? e.toString() : ""
            },
            "string-asc": function (e, t) {
                return e < t ? -1 : t < e ? 1 : 0
            },
            "string-desc": function (e, t) {
                return e < t ? 1 : t < e ? -1 : 0
            }
        }), je(""), M.extend(!0, Ke.ext.renderer, {
            header: {
                _: function (i, o, a, s) {
                    M(i.nTable).on("order.dt.DT", function (e, t, n, r) {
                        i === t && (e = a.idx, o.removeClass(a.sSortingClass + " " + s.sSortAsc + " " + s.sSortDesc).addClass("asc" == r[e] ? s.sSortAsc : "desc" == r[e] ? s.sSortDesc : a.sSortingClass))
                    })
                },
                jqueryui: function (i, o, a, s) {
                    M("<div/>").addClass(s.sSortJUIWrapper).append(o.contents()).append(M("<span/>").addClass(s.sSortIcon + " " + a.sSortingClassJUI)).appendTo(o), M(i.nTable).on("order.dt.DT", function (e, t, n, r) {
                        i === t && (e = a.idx, o.removeClass(s.sSortAsc + " " + s.sSortDesc).addClass("asc" == r[e] ? s.sSortAsc : "desc" == r[e] ? s.sSortDesc : a.sSortingClass), o.find("span." + s.sSortIcon).removeClass(s.sSortJUIAsc + " " + s.sSortJUIDesc + " " + s.sSortJUI + " " + s.sSortJUIAscAllowed + " " + s.sSortJUIDescAllowed).addClass("asc" == r[e] ? s.sSortJUIAsc : "desc" == r[e] ? s.sSortJUIDesc : a.sSortingClassJUI))
                    })
                }
            }
        });

        function Pt(e) {
            return "string" == typeof e ? e.replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;") : e
        }
        return Ke.render = {
            number: function (r, i, o, a, s) {
                return {
                    display: function (e) {
                        if ("number" != typeof e && "string" != typeof e) return e;
                        var t = e < 0 ? "-" : "",
                            n = parseFloat(e);
                        return isNaN(n) ? Pt(e) : (n = n.toFixed(o), e = Math.abs(n), n = parseInt(e, 10), e = o ? i + (e - n).toFixed(o).substring(2) : "", t + (a || "") + n.toString().replace(/\B(?=(\d{3})+(?!\d))/g, r) + e + (s || ""))
                    }
                }
            },
            text: function () {
                return {
                    display: Pt,
                    filter: Pt
                }
            }
        }, M.extend(Ke.ext.internal, {
            _fnExternApiFunc: t,
            _fnBuildAjax: V,
            _fnAjaxUpdate: G,
            _fnAjaxParameters: e,
            _fnAjaxUpdateDraw: o,
            _fnAjaxDataSrc: Z,
            _fnAddColumn: E,
            _fnColumnOptions: _,
            _fnAdjustColumnSizing: H,
            _fnVisibleToColumnIndex: A,
            _fnColumnIndexToVisible: u,
            _fnVisbleColumns: y,
            _fnGetColumns: x,
            _fnColumnTypes: s,
            _fnApplyColumnDefs: I,
            _fnHungarianMap: a,
            _fnCamelToHungarian: S,
            _fnLanguageCompat: w,
            _fnBrowserDetect: C,
            _fnAddData: R,
            _fnAddTr: k,
            _fnNodeToDataIndex: function (e, t) {
                return t._DT_RowIndex !== O ? t._DT_RowIndex : null
            },
            _fnNodeToColumnIndex: function (e, t, n) {
                return M.inArray(n, e.aoData[t].anCells)
            },
            _fnGetCellData: g,
            _fnSetCellData: r,
            _fnSplitObjNotation: c,
            _fnGetObjectDataFn: P,
            _fnSetObjectDataFn: h,
            _fnGetDataMaster: v,
            _fnClearTable: l,
            _fnDeleteIndex: d,
            _fnInvalidate: i,
            _fnGetRowElements: f,
            _fnCreateTr: N,
            _fnBuildHead: L,
            _fnDrawHead: F,
            _fnDraw: z,
            _fnReDraw: B,
            _fnAddOptionsHtml: j,
            _fnDetectHeader: U,
            _fnGetUniqueThs: W,
            _fnFeatureHtmlFilter: q,
            _fnFilterComplete: Y,
            _fnFilterCustom: X,
            _fnFilterColumn: J,
            _fnFilter: $,
            _fnFilterCreateSearch: Q,
            _fnEscapeRegex: dt,
            _fnFilterData: K,
            _fnFeatureHtmlInfo: ne,
            _fnUpdateInfo: re,
            _fnInfoMacros: ie,
            _fnInitialise: oe,
            _fnInitComplete: ae,
            _fnLengthChange: se,
            _fnFeatureHtmlLength: le,
            _fnFeatureHtmlPaginate: ue,
            _fnPageChange: ce,
            _fnFeatureHtmlProcessing: de,
            _fnProcessingDisplay: fe,
            _fnFeatureHtmlTable: pe,
            _fnScrollDraw: he,
            _fnApplyToChildren: ge,
            _fnCalculateColumnWidths: ve,
            _fnThrottle: gt,
            _fnConvertToWidth: me,
            _fnGetWidestNode: ye,
            _fnGetMaxLenString: be,
            _fnStringToCss: Se,
            _fnSortFlatten: we,
            _fnSort: De,
            _fnSortAria: Te,
            _fnSortListener: Ce,
            _fnSortAttachListener: Ee,
            _fnSortingClasses: _e,
            _fnSortData: xe,
            _fnSaveState: Ie,
            _fnLoadState: Re,
            _fnSettingsFromNode: ke,
            _fnLog: Pe,
            _fnMap: Me,
            _fnBindAction: He,
            _fnCallbackReg: Ae,
            _fnCallbackFire: Ne,
            _fnLengthOverflow: Le,
            _fnRenderer: Fe,
            _fnDataSource: ze,
            _fnRowAttributes: p,
            _fnExtend: Oe,
            _fnCalculateEnd: function () { }
        }), ((M.fn.dataTable = Ke).$ = M).fn.dataTableSettings = Ke.settings, M.fn.dataTableExt = Ke.ext, M.fn.DataTable = function (e) {
            return M(this).dataTable(e).api()
        }, M.each(Ke, function (e, t) {
            M.fn.DataTable[e] = t
        }), M.fn.dataTable
    }),
    function (e) {
        e(document).ready(function () {
            e(".C--datatables.type--1.-autoload").find(".datatables").DataTable()
        })
    }(jQuery),
    function (o) {
        var a = {
            init: function (r, i) {
                r.on("click", ".checkbox__component", function (e) {
                    var t = o(this),
                        n = t.parents(".checkbox__item");
                    n.hasClass("-is-disabled") ? e.preventDefault() : (n.hasClass("-is-selected") ? a.reset.item(n) : n.addClass("-is-selected"), o.isFunction(i.onClickItem) && i.onClickItem(r, t))
                })
            },
            reset: {
                items: function (e) {
                    e.find(".checkbox__item").removeClass("-is-selected"), e.find(".checkbox__component").removeAttr("checked").prop("checked", !1)
                },
                item: function (e) {
                    e.removeClass("-is-selected"), e.find(".checkbox__component").removeAttr("checked").prop("checked", !1)
                }
            }
        },
            t = {
                init: function (e) {
                    var r = o.extend({
                        onClickItem: null
                    }, e);
                    return this.each(function (e, t) {
                        var n = o(t);
                        a.init(n, r)
                    })
                },
                reset: function () {
                    return this.each(function (e, t) {
                        var n = o(t);
                        a.reset.items(n)
                    })
                },
                destroy: function () {
                    return this.each(function (e, t) {
                        var n = o(t);
                        a.reset.items(n), n.off("click", ".checkbox__item")
                    })
                }
            };
        o.fn.BINUS_Checkbox_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void o.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, o(document).ready(function () {
            o(".C--checkbox.type--1.-autoload").BINUS_Checkbox_One()
        })
    }(jQuery),
    function (o) {
        var t = {
            init: function (e) {
                var i = o.extend({
                    onChangeItem: null
                }, e);
                return this.each(function (e, t) {
                    var n = o(t),
                        r = n.find(".combobox__component option:selected");
                    n.append('<span class="combobox__label">' + r.text() + '</span>                    <span class="combobox__dropdown">                        <span class="U--table -full-height">                            <span class="table__cell -vertical-align--middle">                                <i class="material-icons">keyboard_arrow_down</i>                            </span>                        </span>                    </span>'), n.find(".combobox__component").focusin(function () {
                        n.addClass("-is-open")
                    }).blur(function () {
                        n.removeClass("-is-open")
                    }), n.on("change", ".combobox__component", function (e) {
                        var t = n.find(".combobox__component option:selected");
                        n.find(".combobox__label").text(t.text()), n.removeClass("-is-open"), o.isFunction(i.onChangeItem) && i.onChangeItem(n, t)
                    })
                })
            },
            reset: function () {
                return this.each(function (e, t) {
                    var n = o(t);
                    n.removeClass("-is-open"), n.find(".combobox__component").val("").find("option").removeAttr("selected");
                    var r = n.find(".combobox__component option").first().attr("selected", "selected");
                    n.find(".combobox__label").html(r.text())
                })
            },
            destroy: function () {
                return this.each(function (e, t) {
                    var n = o(t);
                    n.removeClass("-is-open"), n.find(".combobox__component").val("").find("option").removeAttr("selected"), n.off("change focusin blur", ".combobox__component"), n.find(".combobox__label").remove(), n.find(".combobox__dropdown").remove()
                })
            }
        };
        o.fn.BINUS_Combobox_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void o.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, o(document).ready(function () {
            o(".C--combobox.type--1.-autoload").BINUS_Combobox_One()
        })
    }(jQuery),
    function (o) {
        var a = function (e) {
            return "<input " + (e.isDisabled ? 'readonly="readonly"' : "") + ' type="' + e.inputType + '" class="passbox__input ' + e.class + '" name="' + e.name + '" id="' + e.id + '" value="' + e.value + '" placeholder="' + e.placeholder + '">'
        },
            s = function () {
                return '<span class="passbox__key">                    <span class="U--table -full-height">                        <span class="table__cell -vertical-align--middle U--text-align--center">                            <i class="visibility__on material-icons">visibility</i>                            <i class="visibility__off material-icons">visibility_off</i>                        </span>                    </span>                </span>'
            },
            l = {
                toggle: function (e, t) {
                    e.find(".passbox__input").remove(), e.append(a(t))
                }
            },
            t = {
                init: function (e) {
                    var i = o.extend({
                        isDisabled: !1,
                        onShow: null,
                        onHide: null
                    }, e);
                    return this.each(function (e, t) {
                        var n = o(t),
                            r = {
                                id: n.attr("data-id"),
                                name: n.attr("data-name"),
                                class: n.attr("data-class"),
                                value: n.attr("data-value"),
                                placeholder: void 0 === n.attr("data-placeholder") ? "" : n.attr("data-placeholder"),
                                inputType: "password",
                                isDisabled: i.isDisabled
                            };
                        i.isDisabled && n.addClass("-is-disabled"), n.append(s() + a(r)), n.on("click", ".passbox__key", function (e) {
                            e.preventDefault(), r.value = n.find(".passbox__input").val(), n.hasClass("-is-show") ? (n.removeClass("-is-show"), r.inputType = "password", o.isFunction(i.onHide) && i.onHide(n, l, r)) : (n.addClass("-is-show"), r.inputType = "text", o.isFunction(i.onShow) && i.onShow(n, l, r)), l.toggle(n, r)
                        })
                    })
                },
                reset: function () {
                    return this.each(function (e, t) {
                        o(t).removeClass("-is-show").find("passbox__input").val("")
                    })
                },
                destroy: function () {
                    return this.each(function (e, t) {
                        var n = o(t);
                        n.off("click", ".passbox__key"), n.html("")
                    })
                },
                enabled: function () {
                    return this.each(function (e, t) {
                        o(t).removeClass("-is-disabled").find(".passbox__input").removeAttr("readonly")
                    })
                },
                disabled: function () {
                    return this.each(function (e, t) {
                        o(t).addClass("-is-disabled").find(".passbox__input").attr("readonly", "readonly")
                    })
                }
            };
        o.fn.BINUS_Passbox_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void o.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, o(document).ready(function () {
            o(".C--passbox.type--1.-autoload").BINUS_Passbox_One()
        })
    }(jQuery),
    function (o) {
        var a = function (e) {
            e.find(".radio-button__item").removeClass("-is-selected"), e.find(".radio-button__component").removeAttr("checked").prop("checked", !1)
        },
            t = {
                init: function (e) {
                    var i = o.extend({
                        onClickItem: null
                    }, e);
                    return this.each(function (e, t) {
                        var r = o(t);
                        r.on("click", ".radio-button__component", function (e) {
                            var t = o(this),
                                n = t.parents(".radio-button__item");
                            n.hasClass("-is-disabled") ? e.preventDefault() : (a(r), n.addClass("-is-selected"), o.isFunction(i.onClickItem) && i.onClickItem(r, t))
                        })
                    })
                },
                reset: function () {
                    return this.each(function (e, t) {
                        var n = o(t);
                        a(n)
                    })
                },
                destroy: function () {
                    return this.each(function (e, t) {
                        var n = o(t);
                        a(n), n.off("click", ".radio-button__component")
                    })
                }
            };
        o.fn.BINUS_RadioButton_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void o.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, o(document).ready(function () {
            o(".C--radio-button.type--1.-autoload").BINUS_RadioButton_One()
        })
    }(jQuery),
    function (a) {
        var s = function (e) {
            e.find(".uploadbox__label").text("No file chosen."), e.find(".uploadbox__component").val(""), e.find(".uploadbox__remove").remove()
        },
            l = {
                textbox: function (e) {
                    return '<input type="file" class="uploadbox__component ' + e.class + '" name="' + e.name + '" id="' + e.id + '">'
                },
                label: function (e) {
                    return '<span class="uploadbox__label">' + e + "</span>"
                },
                toggleButton: function () {
                    return '<span class="uploadbox__indicator">                    <span class="U--table -full-height">                        <span class="table__cell -vertical-align--middle">                            <i class="material-icons">attach_file</i>                        </span>                    </span>                </span>'
                },
                removeButton: function () {
                    return '<span class="uploadbox__remove">                    <span class="U--table -full-height">                        <span class="table__cell -vertical-align--middle">                            <i class="material-icons">close</i>                        </span>                    </span>                </span>'
                }
            },
            t = {
                init: function (e) {
                    var o = a.extend({
                        onChangeFile: null
                    }, e);
                    return this.each(function (e, t) {
                        var r = a(t),
                            i = {
                                id: r.attr("data-id"),
                                name: r.attr("data-name"),
                                class: r.attr("data-class"),
                                label: "No file chosen."
                            };
                        r.append(l.label(i.label) + l.toggleButton() + l.textbox(i)), r.on("change", ".uploadbox__component", function (e) {
                            var t = a(this);
                            if (0 != (e.target || e.srcElement).value.length) {
                                var n = 0 == t[0].files.length ? i.label : t[0].files[0].name;
                                r.find(".uploadbox__label").text(n), r.append(l.removeButton()), r.on("click", ".uploadbox__remove", function (e) {
                                    e.preventDefault(), s(r)
                                }), a.isFunction(o.onChangeFile) && o.onChangeFile(r, t)
                            } else r.find(".uploadbox__label").text(i.label)
                        })
                    })
                },
                reset: function () {
                    return this.each(function (e, t) {
                        var n = a(t);
                        s(n)
                    })
                },
                destroy: function () {
                    return this.each(function (e, t) {
                        var n = a(t);
                        s(n), n.off("click", ".uploadbox__component"), n.html("")
                    })
                }
            };
        a.fn.BINUS_Uploadbox_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void a.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, a(document).ready(function () {
            a(".C--uploadbox.type--1.-autoload").BINUS_Uploadbox_One()
        })
    }(jQuery),
    function (o) {
        var t = {
            init: function (e) {
                var i = o.extend({
                    onClickItem: null
                }, e);
                return this.each(function (e, t) {
                    o(t).on("click", ".list__item a", function (e) {
                        e.preventDefault();
                        var t = o(this),
                            n = t.parents(".C--tab.type--1").first(),
                            r = t.attr("href");
                        n.find("> .tab__body > .body__item").removeClass("-current-item"), o(r).addClass("-current-item"), n.find("> .tab__header .list__item").removeClass("-current-item"), t.parent().addClass("-current-item"), o.isFunction(i.onClickItem) && i.onClickItem(t)
                    })
                })
            },
            destroy: function () {
                return this.each(function (e, t) {
                    o(t).off("click", ".list__item a")
                })
            }
        };
        o.fn.BINUS_Tab_One = function (e) {
            return t[e] ? t[e].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof e && e ? void o.error("Method " + e + " does not exist.") : t.init.apply(this, arguments)
        }, o(document).ready(function () {
            o(".C--tab.type--1.-autoload").BINUS_Tab_One()
        })
    }(jQuery),
    function (n) {
        var r = n(".M--navigation.type--1");
        n(document).ready(function () {
            r.on("click", ".button--toggle-dropdown", function (e) {
                e.preventDefault();
                var t = n(this).parent();
                t.hasClass("-is-show") ? t.removeClass("-is-show") : (r.find(".navigation__dropdown-wrapper").removeClass("-is-show"), t.addClass("-is-show"))
            })
        }), n(document).on("mouseup", function (e) {
            var t = r.find(".navigation__dropdown-wrapper");
            t.is(e.target) || 0 !== t.has(e.target).length || t.removeClass("-is-show")
        })
    }(jQuery),
    function (e) {
        var t = e(".M--wrapper.type--1");
        e(document).ready(function () {
            t.on("click", ".button--toggle-sidebar", function (e) {
                e.preventDefault(), t.find(".wrapper__sidebar").addClass("-is-show")
            }).on("click", ".sidebar__overlay", function (e) {
                e.preventDefault(), t.find(".wrapper__sidebar").removeClass("-is-show")
            })
        })
    }(jQuery);
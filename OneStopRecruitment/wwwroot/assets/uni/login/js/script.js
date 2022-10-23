! function (t) {
    var o = function (a) {
        return "<input " + (a.isDisabled ? 'readonly="readonly"' : "") + ' type="' + a.inputType + '" class="passbox__input ' + a.class + '" name="' + a.name + '" id="' + a.id + '" value="' + a.value + '" placeholder="' + a.placeholder + '">'
    },
        l = function () {
            return '<span class="passbox__key"><span class="U--table -full-height"><span class="table__cell -vertical-align--middle U--text-align--center"><i class="visibility__on material-icons">visibility</i>                            <i class="visibility__off material-icons">visibility_off</i>                        </span>                    </span>                </span>'
        },
        d = {
            toggle: function (a, s) {
                a.find(".passbox__input").remove(), a.append(o(s))
            }
        },
        s = {
            init: function (a) {
                var n = t.extend({
                    isDisabled: !1,
                    onShow: null,
                    onHide: null
                }, a);
                return this.each(function (a, s) {
                    var i = t(s),
                        e = {
                            id: i.attr("data-id"),
                            name: i.attr("data-name"),
                            class: i.attr("data-class"),
                            value: i.attr("data-value"),
                            placeholder: void 0 === i.attr("data-placeholder") ? "" : i.attr("data-placeholder"),
                            inputType: "password",
                            isDisabled: n.isDisabled
                        };
                    n.isDisabled && i.addClass("-is-disabled"), i.append(l() + o(e)), i.on("click", ".passbox__key", function (a) {
                        a.preventDefault(), e.value = i.find(".passbox__input").val(), i.hasClass("-is-show") ? (i.removeClass("-is-show"), e.inputType = "password", t.isFunction(n.onHide) && n.onHide(i, d, e)) : (i.addClass("-is-show"), e.inputType = "text", t.isFunction(n.onShow) && n.onShow(i, d, e)), d.toggle(i, e)
                    })
                })
            },
            reset: function () {
                return this.each(function (a, s) {
                    t(s).removeClass("-is-show").find("passbox__input").val("")
                })
            },
            destroy: function () {
                return this.each(function (a, s) {
                    var i = t(s);
                    i.off("click", ".passbox__key"), i.html("")
                })
            },
            enabled: function () {
                return this.each(function (a, s) {
                    t(s).removeClass("-is-disabled").find(".passbox__input").removeAttr("readonly")
                })
            },
            disabled: function () {
                return this.each(function (a, s) {
                    t(s).addClass("-is-disabled").find(".passbox__input").attr("readonly", "readonly")
                })
            }
        };
    t.fn.BINUS_Passbox_One = function (a) {
        return s[a] ? s[a].apply(this, Array.prototype.slice.call(arguments, 1)) : "object" != typeof a && a ? void t.error("Method " + a + " does not exist.") : s.init.apply(this, arguments)
    }, t(document).ready(function () {
        t(".C--passbox.type--1.-autoload").BINUS_Passbox_One()
    })
}(jQuery);
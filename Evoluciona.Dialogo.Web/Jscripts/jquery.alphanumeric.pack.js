if (!Array.indexOf) {
    Array.prototype.indexOf = function(obj) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == obj) {
                return i;
            }
        }
        return -1;
    };
}

(function(a) {
    a.fn.decimalMask = function(e) {
        var c = { separator: ",", decSize: 4, intSize: 8 };
        c = a.extend(c, e);
        var d;
        if ([",", "."].indexOf(c.separator.substring(0, 1)) == -1) {
            d = ",";
        } else {
            d = c.separator.substring(0, 1);
        }
        var g = c.intSize;
        var b = c.decSize;
        var h = function() {
            var m = false;
            for (var k = 1, j = arguments.length; k < j; k++) {
                if (arguments[0] === arguments[k]) {
                    m = true;
                    break;
                }
            }
            return m;
        };
        a(this).each(function() {
            a(this).attr("maxlength", (g + b + 1));
            var j = a(this).val().replace(".", d);
            if (j.indexOf(d) === 0) {
                j = "0" + value;
            }
            a(this).val(j);
        });
        var i = function(m) {
            var n = a(this).val();
            var p = "";
            var q = n.indexOf(d) > -1 ? true : false;
            for (var k = 0, j = n.length; k < j; k++) {
                if (n.substring(k, k + 1).match("[0-9," + d + "]")) {
                    if (n.substring(k, k + 1) === d && p.indexOf(d) === -1) {
                        p = p + n.substring(k, k + 1);
                    }
                    if (n.substring(k, k + 1) !== d) {
                        p = p + n.substring(k, k + 1);
                    }
                }
            }
            var o = p.split(d);
            if (o[0].length > g) {
                if (o[1] === undefined && !q) {
                    q = true;
                    o[1] = o[0].substring(g, o[0].length);
                }
                o[0] = o[0].substring(0, g);
            }
            if (o[1] !== undefined && o[1].length > b) {
                o[1] = o[1].substring(0, b);
            }
            a(this).val((o[0] === undefined ? "" : o[0]) + (q ? d : "") + (o[1] === undefined ? "" : o[1]));
        };
        var f = function(l) {
            var j = l.keyCode;
            if (l.shiftKey || l.ctrlKey || l.altKey) {
                return true;
            }
            if (h(j, 35, 36, 9, 13)) {
                return true;
            }
            if (h(j, 37, 38, 39, 40)) {
                return true;
            }
            if (j === 46) {
                var m = a(this).val();
                if (a(this).caret().start === a(this).val().indexOf(d) && a(this).caret().start == a(this).caret().end && m.length > 1 && m.indexOf(d) > 0 && m.indexOf(d) + 1 < m.length) {
                    return false;
                }
                if (m.indexOf(d) >= a(this).caret().start && m.indexOf(d) < a(this).caret().end) {
                    a(this).val(m.substring(0, a(this).caret().start) + d + m.substring(a(this).caret().end, m.length));
                    if (a(this).val().length === 1) {
                        a(this).val("");
                    }
                    return false;
                }
                return true;
            }
            if (j === 8) {
                var m = a(this).val();
                if (a(this).caret().start === m.indexOf(d) + 1 && a(this).caret().start === a(this).caret().end && m.length > 1 && m.indexOf(d) > 0 && m.indexOf(d) + 1 < m.length) {
                    return false;
                }
                if (m.indexOf(d) >= a(this).caret().start && m.indexOf(d) < a(this).caret().end) {
                    a(this).val(m.substring(0, a(this).caret().start) + d + m.substring(a(this).caret().end, m.length));
                    if (a(this).val().length === 1) {
                        a(this).val("");
                    }
                    return false;
                }
                return true;
            }
            if (h(j, 110, 188)) {
                if (d === ".") {
                    return false;
                }
                if (d === "," && a(this).val().indexOf(",") !== -1) {
                    return false;
                }
                return true;
            }
            if (h(j, 190, 194)) {
                if (d === ",") {
                    return false;
                }
                if (d === "." && a(this).val().indexOf(".") !== -1) {
                    return false;
                }
                return true;
            }
            if ((j >= 96 && j <= 105) || (j >= 48 && j <= 57)) {
                if (a(this).val().length > (g + b + 1)) {
                    return false;
                }
                var o = a(this).val();
                var n = o.split(d);
                if (h(j, 48, 96) && a(this).caret().start === 0 && o.length > 0) {
                    return false;
                }
                if (a(this).caret().start === a(this).caret().end) {
                    if (a(this).caret().start > o.indexOf(d) && o.indexOf(d) > -1 && n[1].length >= b) {
                        return false;
                    }
                    if ((a(this).caret().start <= o.indexOf(d) || o.indexOf(d) === -1) && n[0].length >= g) {
                        return true;
                    }
                } else {
                    if (a(this).caret().end - a(this).caret().start === o.length) {
                        return true;
                    }
                    if (a(this).caret().start <= o.indexOf(d) && a(this).caret().end > o.indexOf(d)) {
                        return false;
                    }
                }
                return true;
            }
            return false;
        };
        a(this).bind("input", i);
        a(this).bind("keydown", f);
    };
})(jQuery);

eval(function(p, a, c, k, e, d) {
    e = function(c) { return (c < a ? "" : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)); };
    if (!''.replace(/^/, String)) {
        while (c--) {
            d[e(c)] = k[c] || e(c);
        }
        k = [function(e) { return d[e]; } ];
        e = function() { return '\\w+'; };
        c = 1;
    }
    ;
    while (c--) {
        if (k[c]) {
            p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]);
        }
    }
    return p;
} ('(2($){$.c.f=2(p){p=$.d({g:"!@#$%^&*()+=[]\\\\\\\';,/{}|\\":<>?~`.- ",4:"",9:""},p);7 3.b(2(){5(p.G)p.4+="Q";5(p.w)p.4+="n";s=p.9.z(\'\');x(i=0;i<s.y;i++)5(p.g.h(s[i])!=-1)s[i]="\\\\"+s[i];p.9=s.O(\'|\');6 l=N M(p.9,\'E\');6 a=p.g+p.4;a=a.H(l,\'\');$(3).J(2(e){5(!e.r)k=o.q(e.K);L k=o.q(e.r);5(a.h(k)!=-1)e.j();5(e.u&&k==\'v\')e.j()});$(3).B(\'D\',2(){7 F})})};$.c.I=2(p){6 8="n";8+=8.P();p=$.d({4:8},p);7 3.b(2(){$(3).f(p)})};$.c.t=2(p){6 m="A";p=$.d({4:m},p);7 3.b(2(){$(3).f(p)})}})(C);', 53, 53, '||function|this|nchars|if|var|return|az|allow|ch|each|fn|extend||alphanumeric|ichars|indexOf||preventDefault||reg|nm|abcdefghijklmnopqrstuvwxyz|String||fromCharCode|charCode||alpha|ctrlKey||allcaps|for|length|split|1234567890|bind|jQuery|contextmenu|gi|false|nocaps|replace|numeric|keypress|which|else|RegExp|new|join|toUpperCase|ABCDEFGHIJKLMNOPQRSTUVWXYZ'.split('|'), 0, {}));

var thresholdcolors = [['20%', 'darkred'], ['10%', 'red']];
//[chars_left_in_pct, CSS color to apply to output]
var uncheckedkeycodes = /(8)|(13)|(16)|(17)|(18)/;
//keycodes that are not checked, even when limit has been reached.

thresholdcolors.sort(function(a, b) { return parseInt(a[0]) - parseInt(b[0]); });
//sort thresholdcolors by percentage, ascending

function setformfieldsize($fields, optsize, optoutputdiv) {
    var $ = jQuery;
    $fields.each(function(i) {
        var $field = $(this);
        $field.data('maxsize', optsize || parseInt($field.attr('data-maxsize'))); //max character limit
        var statusdivid = optoutputdiv || $field.attr('data-output'); //id of DIV to output status
        $field.data('$statusdiv', $('#' + statusdivid).length == 1 ? $('#' + statusdivid) : null);
        $field.unbind('keypress.restrict').bind('keypress.restrict', function(e) {
            setformfieldsize.restrict($field, e);
        });
        $field.unbind('keyup.show').bind('keyup.show', function(e) {
            setformfieldsize.showlimit($field);
        });
        setformfieldsize.showlimit($field); //show status to start
    });
}

setformfieldsize.restrict = function($field, e) {
    var keyunicode = e.charCode || e.keyCode;
    if (!uncheckedkeycodes.test(keyunicode)) {
        if ($field.val().length >= $field.data('maxsize')) { //if characters entered exceed allowed
            if (e.preventDefault)
                e.preventDefault();
            return false;
        }
    }
};
setformfieldsize.showlimit = function($field) {
    if ($field.val().length > $field.data('maxsize')) {
        var trimmedtext = $field.val().substring(0, $field.data('maxsize'));
        $field.val(trimmedtext);
    }
    if ($field.data('$statusdiv')) {
        $field.data('$statusdiv').css('color', '').html($field.val().length);
        var pctremaining = ($field.data('maxsize') - $field.val().length) / $field.data('maxsize') * 100; //calculate chars remaining in terms of percentage
        for (var i = 0; i < thresholdcolors.length; i++) {
            if (pctremaining <= parseInt(thresholdcolors[i][0])) {
                $field.data('$statusdiv').css('color', thresholdcolors[i][1]);
                break;
            }
        }
    }
};
jQuery(document).ready(function($) { //fire on DOM ready
    var $targetfields = $("input[data-maxsize], textarea[data-maxsize]"); //get INPUTs and TEXTAREAs on page with "data-maxsize" attr defined
    setformfieldsize($targetfields);
});
/*
*
* Copyright (c) 2010 C. F., Wong (<a href="http://cloudgen.w0ng.hk">Cloudgen Examplet Store</a>)
* Licensed under the MIT License:
* http://www.opensource.org/licenses/mit-license.php
*
*/
eval(function($, len, createRange, duplicate) {
    $.fn.caret = function(options, opt2) {
        var start, end, t = this[0], browser = $.browser.msie;
        if (typeof options === "object" && typeof options.start === "number" && typeof options.end === "number") {
            start = options.start;
            end = options.end;
        } else if (typeof options === "number" && typeof opt2 === "number") {
            start = options;
            end = opt2;
        } else if (typeof options === "string") {
            if ((start = t.value.indexOf(options)) > -1) end = start + options[len];
            else start = null;
        } else if (Object.prototype.toString.call(options) === "[object RegExp]") {
            var re = options.exec(t.value);
            if (re != null) {
                start = re.index;
                end = start + re[0][len];
            }
        }
        if (typeof start != "undefined") {
            if (browser) {
                var selRange = this[0].createTextRange();
                selRange.collapse(true);
                selRange.moveStart('character', start);
                selRange.moveEnd('character', end - start);
                selRange.select();
            } else {
                this[0].selectionStart = start;
                this[0].selectionEnd = end;
            }
            this[0].focus();
            return this;
        } else {
            if (browser) {
                var selection = document.selection;
                if (this[0].tagName.toLowerCase() != "textarea") {
                    var val = this.val(),
                        range = selection[createRange]()[duplicate]();
                    range.moveEnd("character", val[len]);
                    var s = (range.text == "" ? val[len] : val.lastIndexOf(range.text));
                    range = selection[createRange]()[duplicate]();
                    range.moveStart("character", -val[len]);
                    var e = range.text[len];
                } else {
                    var range = selection[createRange](),
                        stored_range = range[duplicate]();
                    stored_range.moveToElementText(this[0]);
                    stored_range.setEndPoint('EndToEnd', range);
                    var s = stored_range.text[len] - range.text[len],
                        e = s + range.text[len];
                }
            } else {
                var s = t.selectionStart,
                    e = t.selectionEnd;
            }
            var te = t.value.substring(s, e);
            return {
                start: s,
                end: e,
                text: te,
                replace: function(st) {
                    return t.value.substring(0, s) + st + t.value.substring(e, t.value[len]);
                }
            };
        }
    };
})(jQuery, "length", "createRange", "duplicate");
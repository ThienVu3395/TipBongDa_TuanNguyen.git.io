$(document).ready(function() {
    if ("opera" == i() || "safari" == i()) {
        var e = $(".container img"),
            a = e.length,
            t = 0;
        e.one("load", function() {
            ++t == a && (grayscale($(".container img")), $(".container img").hover(function() {
                grayscale.reset($(this))
            }, function() {
                grayscale($(this))
            }))
        }).each(function() {
            this.complete && $(this).trigger("load")
        })
    }
    if (r() >= 10) {
        $(".container img").each(function() {
            var e = $(this);
            e.css({
                position: "absolute"
            }).wrap("<div class='img_wrapper' style='display: inline-block'>").clone().addClass("img_grayscale").css({
                position: "absolute",
                "z-index": "5",
                opacity: "0"
            }).insertBefore(e).queue(function() {
                var e = $(this);
                e.parent().css({
                    width: this.width,
                    height: this.height
                }), e.dequeue()
            }), this.src = function(e) {
                var a = document.createElement("canvas"),
                    t = a.getContext("2d"),
                    i = new Image;
                i.src = e, a.width = i.width, a.height = i.height, t.drawImage(i, 0, 0);
                for (var r = t.getImageData(0, 0, a.width, a.height), o = 0; o < r.height; o++)
                    for (var s = 0; s < r.width; s++) {
                        var n = 4 * o * r.width + 4 * s,
                            c = (r.data[n] + r.data[n + 1] + r.data[n + 2]) / 3;
                        r.data[n] = c, r.data[n + 1] = c, r.data[n + 2] = c
                    }
                return t.putImageData(r, 0, 0, 0, 0, r.width, r.height), a.toDataURL()
            }(this.src)
        }), $(".container img").hover(function() {
            $(this).parent().find("img:first").stop().animate({
                opacity: 1
            }, 200)
        }, function() {
            $(".img_grayscale").stop().animate({
                opacity: 0
            }, 200)
        })
    }

    function i() {
        var e = navigator.userAgent.toLowerCase();
        // return $.browser.chrome = /chrome/.test(e), $.browser.safari = /webkit/.test(e), $.browser.opera = /opera/.test(e), $.browser.msie = /msie/.test(e) && !/opera/.test(e), $.browser.mozilla = /mozilla/.test(e) && !/(compatible|webkit)/.test(e) || /firefox/.test(e), $.browser.chrome ? "chrome" : $.browser.mozilla ? "mozilla" : $.browser.opera ? "opera" : $.browser.safari ? "safari" : $.browser.msie ? "ie" : void 0
    }

    function r() {
        var e = -1;
        if ("Microsoft Internet Explorer" == navigator.appName) {
            var a = navigator.userAgent;
            null != new RegExp("MSIE ([0-9]{1,}[.0-9]{0,})").exec(a) && (e = parseFloat(RegExp.$1))
        } else if ("Netscape" == navigator.appName) {
            a = navigator.userAgent;
            null != new RegExp("Trident/.*rv:([0-9]{1,}[.0-9]{0,})").exec(a) && (e = parseFloat(RegExp.$1))
        }
        return e
    }
    "mozilla" == i() ? $("body").addClass("mozilla") : "ie" == i() ? $("body").addClass("ie") : "opera" == i() ? $("body").addClass("opera") : "safari" == i() ? $("body").addClass("safari") : "chrome" == i() && $("body").addClass("chrome"), r() >= 10 && $("body").addClass("ie11")
});

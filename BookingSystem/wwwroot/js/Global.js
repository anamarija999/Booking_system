window.eventInitialize = "Initialize";


// Initialize on jquery html ($.html)
var jqueryHtml = window.jQuery.fn.html;
window.jQuery.fn.html = function () {

    var newHtml = jqueryHtml.apply(this, arguments);
    if (arguments.length) {
        triggerInitialize(newHtml);
    }

    return newHtml;
};

// Adds $.postJSON and $.putJSON to ease the process of sending json
jQuery.each(["post", "put"], function (i, method) {
    jQuery[`${method}JSON`] = function (url, data, callback) {
        if (jQuery.isFunction(data)) {
            callback = data;
            data = undefined;
        }

        return jQuery.ajax({
            url: url,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: callback
        });
    };
});

window.triggerInitialize = function (parent) {
    if (!parent) {
        parent = $("body");
    }

    $(document).trigger(eventInitialize, parent);
};

// Extension of jQuery.attr() to retreive all attributes of an object.
(function (old) {
    $.fn.attr = function () {
        if (arguments.length === 0) {
            if (this.length === 0) {
                return null;
            }

            var obj = {};
            $.each(this[0].attributes, function () {
                if (this.specified) {
                    obj[this.name] = this.value;
                }
            });
            return obj;
        }

        return old.apply(this, arguments);
    };
})($.fn.attr);

var getToast = function (title, icon, message, delay = 5000) {

    var autoHide = delay > 0;

    if (!delay)
        delay = 3000;

    return $("<div>")
        .addClass("toast")
        .attr("role", "alert") //status
        .attr("aria-live", "assertive") //polite
        .attr("aria-atomic", "true")
        .attr("data-delay", delay)
        .attr("data-autohide", autoHide)
        .append($("<div>")
            .addClass("toast-header d-flex justify-content-between")
            .append($("<strong>")
                .text(title)
                .prepend($("<i>")
                    .addClass(icon)))
            .append($("<button>")
                .addClass("ml-2 mb-1 close")
                .attr("data-dismiss", "toast")
                .attr("aria-label", "Close")
                .append($("<span>")
                    .attr("aria-hidden", "true")
                    .html("&times;"))))
        .append($("<div>")
            .addClass("toast-body")
            .text(message))
        .appendTo($("#toast-wrapper"))
        .toast("show");
}

// Displays a success toast with the given message and delay. If the delay is negative, the toast will not hide automatically.
window.toastSuccess = function (message) {
    var $toast = getToast("Success", "fas fa-check-circle mr-2", message);
    $toast.addClass("toast-success");
};

// Displays an error toast with the given message and delay. If the delay is negative, the toast will not hide automatically.
window.toastError = function (message) {
    var $toast = getToast("Error", "fas fa-exclamation-circle mr-2", message);
    $toast.addClass("toast-error");
};

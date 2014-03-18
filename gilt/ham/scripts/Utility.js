var GILTY = GILTY || {};

GILTY.Toast =
{
    DisplayCookieAsToast: function DisplayCookieAsToast(CookieName, BgColor, FgColor, TimeoutMs)
    {
        TimeoutMs = typeof TimeoutMs !== 'undefined' ? TimeoutMs : 3000;

        var message = $.cookie(CookieName);
        if (message)
        {
            $.Notify({
                shadow: true,
                position: 'top-right',
                style: { background: BgColor, color: FgColor },
                content: message,
                timeout: TimeoutMs
            });
        }
        //use the { path } option or it will not delete
        $.removeCookie(CookieName, { path: '/' });
    },
    DisplayError: function DisplayError()
    {
        GILTY.Toast.DisplayCookieAsToast("Error", '#1BA1E2', 'white');
    },
    DisplayMessage: function DisplayMessage()
    {
        GILTY.Toast.DisplayCookieAsToast("Message", '#1BA1E2', 'white');
    },
    DisplayToast: function DisplayToast()
    {
        GILTY.Toast.DisplayMessage();
        GILTY.Toast.DisplayError();
    }
};

GILTY.Uri =
{
    Table: $.url().segment(1),
    Action: $.url().segment(2),
    Id: $.url().segment(3),
};

ko.bindingHandlers.changedCss = {
    init: function (element, valueAccessor, allBindings)
    {
        var original, dirty, data, cssClass, binding;

        data = allBindings().value;
        original = ko.utils.unwrapObservable(data);
        dirty = ko.computed({
            read: function ()
            {
                return ko.utils.unwrapObservable(data) !== original;
            },
            disposeWhenNodeIsRemoved: element
        });

        cssClass = ko.utils.unwrapObservable(valueAccessor());
        binding = { css: {} };
        binding.css[cssClass] = dirty;

        ko.applyBindingsToNode(element, binding);
    }
};
//control visibility, give element focus, and select the contents (in order)
ko.bindingHandlers.visibleAndSelect = {
    update: function (element, valueAccessor)
    {
        ko.bindingHandlers.visible.update(element, valueAccessor);
        if (valueAccessor())
        {
            setTimeout(function ()
            {
                $(element).find("input").focus().select();
            }, 0); //new tasks are not in DOM yet
        }
    }
};

$(document).ready(function ()
{
    $("#carousel1").carousel({
        auto: true,
        height: 300,
        period: 3000,
        duration: 500,
        stop: true,
        controls: true,
        effect: 'slowdown',
        markers: {
            show: false
        }
    });

    GILTY.Toast.DisplayToast();

    //google analytics tracking
    (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
    (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
    m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
    })(window,document,'script','//www.google-analytics.com/analytics.js','ga');

    ga('create', 'UA-48564739-1', 'giltly.com');
    ga('send', 'pageview');
   
});
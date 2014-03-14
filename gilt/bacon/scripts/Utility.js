var GILTY = GILTY || {};

GILTY.RowHighLighter =
{    
    AddTableRowHighlight: function AddTableRowHighlight(Data, Event)
    {
        //add the severity colors
        $(Event.target).parents("table").find("tr > td:nth-child(3) > div").each(function (index)
        {
            $(this).addClass('priority-' + $(this).text());
        });

        $(Event.target).parents("table").find(".rowhighlight").removeClass('rowhighlight');
        $(Event.target).parent("tr").addClass('rowhighlight');
    },
    ClickRow : function ClickRow(Data, Event)
    {
        try
        {
            var rowIndex = $(Event.target).parent("tr")[0].rowIndex - 1;
            $(Event.target).parents("table").find(".rowselected").removeClass('rowselected');
            $(Event.target).parent("tr").addClass('rowselected');
        }
        catch (e)
        {
        }
    }
};

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

GILTY.HomeTiles =
{
    HomeTiles: function HomeTiles()
    {
        //display the arc map on start
        GILTY.D3.ArcMap([], 'WorldMapSvg');

        var spinnerEventIp = new Spinner({}).spin(document.getElementById('EventUniqueIp'));
        $.when($.ajax({ type: "GET", url: "/Event/ByIp/0/8" })).then(function (data, textStatus, jqXHR)
        {
            var transforms = {
                "tag": "tr", "children": [
                    { "tag": "td", "html": "${Ip}" },
                    { "tag": "td", "html": "${EventCount}" }
                ]
            };
            spinnerEventIp.stop();
            $('#EventUniqueIp').json2html(data.Rows, transforms);
        });

        var spinnerEventCountry = new Spinner({}).spin(document.getElementById('EventUniqueCountry'));
        $.when($.ajax({ type: "GET", url: "/Event/ByCountry/0/8" })).then(function (data, textStatus, jqXHR)
        {
            var transforms = {
                "tag": "tr", "children": [
                    { "tag": "td", "html": "${CountryCode}" },
                    { "tag": "td", "html": "${CountryCount}" }
                ]
            };
            spinnerEventCountry.stop();
            $('#EventUniqueCountry').json2html(data.Rows, transforms);
        });

        var spinnerEventDestinationPort = new Spinner({}).spin(document.getElementById('EventUniqueDestinationPort'));
        $.when($.ajax({ type: "GET", url: "/Event/ByDestinationPort/0/8" })).then(function (data, textStatus, jqXHR)
        {
            var transforms = {
                "tag": "tr", "children": [
                    { "tag": "td", "html": "${Port}" },
                    { "tag": "td", "html": "${PortCount}" }
                ]
            };
            spinnerEventDestinationPort.stop();
            $('#EventUniqueDestinationPort').json2html(data.Rows, transforms);
        });

        var spinnerEventSourcePort = new Spinner({}).spin(document.getElementById('EventUniqueSourcePort'));
        $.when($.ajax({ type: "GET", url: "/Event/BySourcePort/0/8" })).then(function (data, textStatus, jqXHR)
        {
            var transforms = {
                "tag": "tr", "children": [
                    { "tag": "td", "html": "${Port}" },
                    { "tag": "td", "html": "${PortCount}" }
                ]
            };
            spinnerEventSourcePort.stop();
            $('#EventUniqueSourcePort').json2html(data.Rows, transforms);
        });
        
        var spinnerEventTotal = new Spinner({}).spin(document.getElementById('EventCountTotal'));
        $.when($.ajax({ type: "GET", url: "/Event/PreviousCount/52560000/52560000" })).then(function (data, textStatus, jqXHR)
        {
            spinnerEventTotal.stop();
            $("#EventCountTotal").html(data);
        });

        var spinnerEventToday = new Spinner({}).spin(document.getElementById('EventCountToday'));
        $.when($.ajax({ type: "GET", url: "/Event/PreviousCount/1440/1440" })).then(function (data, textStatus, jqXHR)
        {
            spinnerEventToday.stop();
            $("#EventCountToday").html(data);
        });

        var spinnerEventYesterday = new Spinner({}).spin(document.getElementById('EventCountYesterday'));
        $.when($.ajax({ type: "GET", url: "/Event/PreviousCount/2880/1440" })).then(function (data, textStatus, jqXHR)
        {
            spinnerEventToday.stop();
            $("#EventCountYesterday").html(data);
        });

        var spinnerEventMap = new Spinner({}).spin(document.getElementById('WorldMapSvg'));
        $.when($.ajax({ type: "GET", url: "/Event/GetGeoData" })).then(function (data, textStatus, jqXHR)
        {
            var arcPaths = Enumerable.From(data.GeoData)
                .Select(function (x)
                {
                    return {
                        origin: { latitude: x.GeoSource.Latitude, longitude: x.GeoSource.Longitude },
                        destination: { latitude: x.GeoDestination.Latitude, longitude: x.GeoDestination.Longitude }
                    }
                })
                .ToArray();
            //display the arc map on start
            GILTY.D3.ArcMap(arcPaths, 'WorldMapSvg');
            spinnerEventMap.stop();
        });
    }
};

GILTY.Uri =
{
    Table: $.url().segment(1),
    Action: $.url().segment(2),
    Id: $.url().segment(3),
};

GILTY.EventComment =
{
    CreateEventComment: function (DivId, EventId)
    {        
        $("#" + DivId).dialog(
            {
                title: EventId,
                draggable: false,
                modal:true,
                open: function (event, ui)
                {
                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                }
            }).position({
            my: "center",
            at: "center",
            of: window
            });
    },
    SubmitEventComment: function (DivId, ViewModel)
    {
        var eventComment = $("#" + DivId + " form input.EventComment").val();
        var eventId = $("#" + DivId + " form input[name='Id']").val();
        var data = {
            Comment: eventComment,
            Id: eventId
        };
        //prevent sending the form more than once        
        event.preventDefault();

        //close the comment window
        GILTY.EventComment.CloseEventComment(DivId);
        var spinnerEventComment = new Spinner({}).spin(document.getElementsByTagName('body')[0]);

        //note:
        //  JSON.stringify()
        //  contextType : 'application/json; charset=utf-8'
        //  dataType: 'json',
        //
        // are all required by Nancy for Model Binding
        //  https://github.com/NancyFx/Nancy/wiki/Model-binding
        //        
        $.ajax({
            type: "POST",
            url: "/EventComment/Create",
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: false,
            complete: function (data, textStatus, jqXHR)
            {
                spinnerEventComment.stop();
                //show the toast message
                GILTY.Toast.DisplayToast();
                //refresh the data;
                ViewModel.navigate();
            }
        });
    },
    CloseEventComment: function (DivId)
    {
        $('#' + DivId).dialog().dialog('close');
    }
};

GILTY.EventTimeline =
{
    EventTimeline: function EventTimeline(DivId)
    {
        var spinnerLoad = new Spinner({}).spin(document.getElementsByTagName('body')[0]);
        $.when($.ajax({ type: "GET", url: "/Event/TimelineData" })).then(function (data, textStatus, jqXHR)
        {
            //create a timeline event source
            var eventSource = new Timeline.DefaultEventSource();
            //get the event data from the JSON response
            var eventData = JSON.parse(data.EventData);
            //load the events into the event source
            eventSource.loadJSON(eventData, '.');

            //parse the BandInfo response from dataserver
            var bandInfos = [];
            var bandLength = data.BandInfo.length;
            for (var i = 0; i < bandLength; i++)
            {
                var bandData = data.BandInfo[i];
                var bandI = JSON.parse(bandData);
                bandI.eventSource = eventSource;
                bandInfos[i] = Timeline.createBandInfo(bandI);
                //synchronize all of the event bands with the the top band
                bandInfos[i].highlight = true;
                //if you sync 0 with 0 then none of the bands sync up
                if (0 !== i)
                {
                    bandInfos[i].syncWith = 0;
                }
            }
            //create the timeline
            Timeline.create(document.getElementById(DivId), bandInfos, Timeline.HORIZONTAL);
            spinnerLoad.stop();
        });
    }
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
    GILTY.Toast.DisplayToast();

    $('input[type="radio"].SavedSearch').click(function ()
    {
        var savedSearchName = $(this).parent().parent().parent().parent().find("div.span5").html();
        //refresh the page after the use has selected a new option
        $.ajax({
            type: "POST",            
            url: "/Users/Search/SetActive",
            async: false,
            data: 
            {
                ActiveSearchName: savedSearchName 
            },
            success: function(data) 
            {
                location.reload();
            },
            error: function ()
            {
                alert("Error");
            }
        });
    });
});
function pageViewModel(DataUrl, TargetId, SelectedPage, PageSize)
{
    var self = this;
    self.dataUrl = ko.observable(DataUrl);
    self.targetId = ko.observable(TargetId);
    self.selectedPageDisplaySize = ko.observable(10);
    self.pagedViewModel = ko.observable();
    self.selectedPageSize = ko.observable(PageSize || 10);
    self.availablePageSize = ko.observableArray([1, 5, 10, 50, 100]);
    self.selectedPage = ko.observable(SelectedPage);
    self.totalPages = ko.observable();
    self.selectedRow = ko.observable();
    
    self.columnName = ko.observable("Id");
    self.sortOrder = ko.observable("ASC");
    self.sortOrderClass = ko.observable("icon-arrow-up");

    self.setSelectedRow = function (row)
    {
        self.selectedRow(row);
    };

    self.setOrder = function (data, event, colName)
    {        
        if (self.columnName() === colName)
        {
            self.switchSortOrder();
        }
        else
        {
            self.columnName(colName);
            self.sortOrder("ASC");
        }
        self.setTHClass(event);
        self.navigate();
    };
    self.setTHClass = function (event)
    {
        //remove the existing sort icons
        $(event.currentTarget).parent().parent().parent().find("th > i").remove();
        $(event.currentTarget).parent().find("span." + self.columnName()).prepend('<i class="' + self.sortOrderClass() + '"></i>');
    };
    self.switchSortOrder = function ()
    {
        if (self.sortOrder() === "ASC")
        {            
            self.sortOrder("DESC");
            self.sortOrderClass("icon-arrow-down");
        }
        else
        {
            self.sortOrder("ASC");
            self.sortOrderClass("icon-arrow-up");
        }
    };

    $("#pager").on("click", ".pageIndex", function (event)
    {
        self.selectedPage(parseInt($(this).text()));
    });
    $("#pager").on("click", ".first", function (event)
    {
        self.selectedPage(1);
    });
    $("#pager").on("click", ".last", function (event)
    {
        self.selectedPage(parseInt(self.totalPages()));
    });
    $("#pager").on("click", ".next", function (event)
    {
        var nextPage = parseInt(self.selectedPage() + 1);
        if (nextPage >= parseInt(self.totalPages()))
        {
            nextPage = parseInt(self.totalPages());
        }
        self.selectedPage(nextPage);
    });
    $("#pager").on("click", ".prev", function (event)
    {
        var prevPage = parseInt(self.selectedPage() - 1);
        if (prevPage <= 0)
        {
            prevPage = 1;
        }
        self.selectedPage(prevPage);
    });


    // This function will be used to get the data from our WebApi. The requested page and page size are passed
    // as a parameter. The result will be stored in the pagedViewModel property. This will cause that the subscribe event
    // for the pagedViewModel will be fired ==> the page links will be created.
    self.navigate = function ()
    {
        var spinnerLoad = new Spinner({}).spin(document.getElementsByTagName('body')[0]);
        var url = self.dataUrl() + (self.selectedPage() - 1) + "/" + self.selectedPageSize() + "?SortName=" + self.columnName() + "&SortDirection=" + self.sortOrder();
        //if the user requested only one item then don't worry about paging, sorting etc        
        if (0 == self.selectedPage())
        {
            url = self.dataUrl();
        }
        $.get(url, self.pagedViewModel)
            .always(function ()
            {
                spinnerLoad.stop();
            });

    };

    // Function that will subscribe to all the needed events.
    self.subscribeToEvents = function ()
    {
        // This event will fire when selectedPageSize is changed.
        self.selectedPageSize.subscribe(function (newValue)
        {
            self.selectedPage(1);
            self.navigate();
        });

        // This event will be fired when the selectedPage is changed.
        self.selectedPage.subscribe(function (newValue)
        {
            self.navigate();
        });

        // This event will fire when a new value is defined for the pagedViewModel.
        // It will create the page links below the grid.
        self.pagedViewModel.subscribe(function (newValue)
        {
            self.totalPages(newValue.NumberPages);

            var selectedPage = parseInt(self.selectedPage());
            var totalPages = parseInt(self.totalPages());
            var selectedPageDisplaySize = parseInt(self.selectedPageDisplaySize());
            var remainingPageCount = parseInt((selectedPage + selectedPageDisplaySize > numberOfPages) ? (selectedPageDisplaySize - selectedPage) : selectedPageDisplaySize);
            var finalPage = parseInt((selectedPage + remainingPageCount));
            if (finalPage > totalPages)
            {
                finalPage = totalPages;
            }
            var numberOfPages = newValue.NumberPages;

            var pager = $("#pager");
            // clear the pager
            pager.html("");

            var pagerList = $("<ul>");
            pagerList.append($('<li class="first"><a><i class="icon-first-2"></i></a></li>'));
            pagerList.append($('<li class="prev"><a><i class="icon-previous"></i></a></li>'));

            for (var i = selectedPage ; i <= finalPage; i++)
            {
                var active = (i == selectedPage ? 'active' : '');                
                pagerList.append($('<li class="' + active + '"><a class="pageIndex">' + i + '</a></li>'));
            }

            pagerList.append($('<li class="next"><a><i class="icon-next"></i></a></li>'));
            pagerList.append($('<li class="last"><a><i class="icon-last-2"></i></a></li>'));
            //show the pager if there is more than one item
            if (self.selectedPage() > 0)
            {
                pager.append(pagerList);
            }

            $(document).ready(function ()
            {
                setTimeout(function ()
                {
                    $('table.eventlist tbody tr.parent td').on("click", "i", function ()
                    {
                        var idOfParent = $(this).parents('tr').attr('Id');
                        $('table.eventlist tbody tr.child-' + idOfParent).toggle('slow');
                    });
                    if (0 == self.selectedPage())
                    {
                        //expand the row
                        $('table.eventlist tbody tr td:eq(0) i').click();
                        //highlight the row
                        $('table.eventlist tbody tr td:gt(0)').click();
                    }
                }, 100);
            });
        }, this);
    };

    // This function will be used to kick start everything.
    self.bind = function ()
    {
        self.subscribeToEvents();
        self.navigate();
        ko.applyBindings(self, document.getElementById(self.targetId()));
    }
};

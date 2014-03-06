function SideBySideViewModel(DataUrl, TargetId, DropCallBack, SubmitForm)
{
    var self = this;
    self.DropCallback = DropCallBack;
    self.SubmitForm = SubmitForm;
    self.DataUrl = ko.observable(DataUrl);
    self.TargetId = ko.observable(TargetId);
    self.SideBySideViewModel = ko.observable();

    self.unAssignedItems = ko.observableArray([]);
    self.unAssignedItems.id = "unassigned";

    self.assignedItems = ko.observableArray([]);
    self.assignedItems.id = "assigned";

    self.selectedTask = ko.observable();
    self.clearTask = function (data, event)
    {
        if (data === self.selectedTask())
        {
            self.selectedTask(null);
        }

        if (data.name() === "")
        {
            self.unAssignedItems.remove(data);
            self.assignedItems.remove(data);
        }
    };

    self.isItemSelected = function (task)
    {
        return task === self.selectedTask();
    };

    // This function will be used to get the data from our WebApi. The requested page and page size are passed
    // as a parameter. The result will be stored in the pagedViewModel property. This will cause that the subscribe event
    // for the pagedViewModel will be fired ==> the page links will be created.
    self.Navigate = function ()
    {        
        $.get(self.DataUrl(), self.SideBySideViewModel);
    };

    // Function that will subscribe to all the needed events.
    self.SubscribeToEvents = function ()
    {
        // This event will fire when a new value is defined for the searchFormViewModel.
        // It will create the page links below the grid.
        self.SideBySideViewModel.subscribe(function (newValue)
        {            
            self.unAssignedItems(newValue.UnAssigned);
            self.assignedItems(newValue.Assigned);
        }, this);
    };

    // This function will be used to kick start everything.
    self.bind = function ()
    {
        self.SubscribeToEvents();
        self.Navigate();
        ko.applyBindings(self, document.getElementById(self.TargetId()));
    }
};

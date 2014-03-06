function searchFormViewModel(DataUrl, TargetId)
{
    var self = this;
    self.DataUrl = ko.observable(DataUrl);
    self.TargetId = ko.observable(TargetId);
    self.SearchFormViewModel = ko.observable();
    self.Id = ko.observable();
    self.Name = ko.observable();
    self.PacketTypes = ko.observableArray([" ", "ICMP", "TCP","UDP"]);
    self.Signatures = ko.observableArray();
    self.SignatureClassifications = ko.observableArray();
    self.DestinationPorts = ko.observableArray();
    self.SourcePorts = ko.observableArray();
    self.SourceIps = ko.observableArray();
    self.DestinationIps = ko.observableArray();

    // This function will be used to get the data from our WebApi. The requested page and page size are passed
    // as a parameter. The result will be stored in the pagedViewModel property. This will cause that the subscribe event
    // for the pagedViewModel will be fired ==> the page links will be created.
    self.Navigate = function ()
    {        
        $.get(self.DataUrl(), self.SearchFormViewModel);
    };

    // Function that will subscribe to all the needed events.
    self.SubscribeToEvents = function ()
    {
        // This event will fire when a new value is defined for the searchFormViewModel.
        // It will create the page links below the grid.
        self.SearchFormViewModel.subscribe(function (newValue)
        {
            self.Signatures(newValue.Signatures);
            self.SignatureClassifications(newValue.SignatureClassifications);
            self.SourcePorts(newValue.SourcePorts);
            self.DestinationPorts(newValue.DestinationPorts);
            self.SourceIps(newValue.SourceIps);
            self.DestinationIps(newValue.DestinationIps);

            //Setup the Default Values
            self.Id(newValue.SelectedId);
            self.Name(newValue.SelectedName);

            //use the setValue function to set the initial values of the select boxes            
            $('select[name="SourceIpString"]').combobox('setValue', newValue.SelectedSourceIp);
            $('select[name="DestinationIpString"]').combobox('setValue', newValue.SelectedDestinationIp);

            $('select[name="PacketType"]').combobox('setValue', newValue.SelectedPacketType);

            $('select[name="SourcePort"]').combobox('setValue', newValue.SelectedSourcePort);
            $('select[name="DestinationPort"]').combobox('setValue', newValue.SelectedDestinationPort);

            $('select[name="StartSearch"]').combobox('setValue', newValue.SelectedStartSearch);
            $('select[name="EndSearch"]').combobox('setValue', newValue.SelectedEndSearch);

            $('select[name="Signature"]').combobox('setValue', newValue.SelectedSignature);
            $('select[name="SignatureClassification"]').combobox('setValue', newValue.SelectedSignatureClassification);

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

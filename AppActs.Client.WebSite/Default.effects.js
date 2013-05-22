var debug = false;

$(document).ready(function () {
    Main.init({
        applicationId: applicationId,
        api: AppActs.Client.WebSite.WebService.Tile.GetTile
    });
    Main.load();
});

var Main = {

    applicationId: null,
    api: null,
    dateRange: null,
    dateRangeCompare: null,
    customCalendarsExist: false,
    submitDateClicked: false,

    init: function (settings) {
        Main.applicationId = settings.applicationId;
        Main.api = settings.api;
        Main.defaultCalendar();
    },

    defaultCalendar: function() {
        var dateRangeSelectedPredefined = $.session.get("dateRangeSelectedPredefined");
        var dateRangeSelectedCustom = $.session.get("dateRangeSelectedCustom");

        if(dateRangeSelectedPredefined != null || dateRangeSelectedCustom != null) {
            if(dateRangeSelectedPredefined != null) {
                $(ddlDate).val(dateRangeSelectedPredefined);
                $(ddlDate).msDropDown({ showIcon: false });
                Main[dateRangeSelectedPredefined]();
            } else if(dateRangeSelectedCustom != null) {
                $(ddlDate).val("dateChange");
                $(ddlDate).msDropDown({ showIcon: false });
                Main.dateRange = new DateRange(new Date(parseInt($.session.get("dateRangeSelectedCustomStart"))), new Date(parseInt($.session.get("dateRangeSelectedCustomEnd"))));
                Main.dateRangeCompare = new DateRange(new Date(parseInt($.session.get("dateRangeSelectedCustomStartCompare"))), new Date(parseInt($.session.get("dateRangeSelectedCustomEndCompare"))));
            }
        } else {
            $(ddlDate).val("dateChanged1Week");
            $(ddlDate).msDropDown({ showIcon: false });
            Main.dateRange = DateRangeUtil.get1Week(),
            Main.dateRangeCompare = DateRangeUtil.get2Weeks(DateRangeUtil.get1Week().Start)
        }
    },

    load: function () {
        Main.unbindEvents();
        Main.render();
        Main.bindEvents();
    },

    bindEvents: function () {
        $(ddlApplications).change(Main.applicationChange);
        $(ddlDate).change(Main.dateRangeSelected);
        $("#custom #dateChange").click(Main.dateChange);
        $("#custom #dateChangeApply").click(Main.dateChangeApply);
        $("#customDateRangeSelection").click(Main.datePickerClicked);
        $("#dateChangeApply").click(Main.datePickerSubmitClicked);
        $("body").click(Main.hideDateSelector);
    },

    unbindEvents: function () {
        $(ddlApplications).unbind("change");
        $(ddlDate).unbind("change");
        $("#custom #dateChange").unbind("click");
        $("#custom #dateChangeApply").unbind("click");
        $("#customDateRangeSelection").unbind("click");
        $("#dateChangeApply").unbind("click");
        $("body").unbind("click");
    },

    applicationChange: function () {
        Main.applicationId = $(ddlApplications).val();
        Main.load();
    },

    dateRangeSelected: function() {
        var funcName = $(ddlDate).val();
        Main[funcName]();
        if(funcName != "dateChange") {
            $.session.set("dateRangeSelectedPredefined", funcName);
            $.session.delete("dateRangeSelectedCustom");
            $.session.delete("dateRangeSelectedCustomStart"); 
            $.session.delete("dateRangeSelectedCustomEnd");
            $.session.delete("dateRangeSelectedCustomStartCompare");
            $.session.delete("dateRangeSelectedCustomEndCompare");
            Main.load();
        }
    },

    dateChange: function(e) {
        $("#customDateRangeSelection").toggleClass("hide");

        if(!Main.customCalendarsExist) {

            $("#customCalendarFrom").DatePicker({
                flat: true,
                date: DateRangeUtil.resetDate(Main.dateRange.Start),
                current: DateRangeUtil.resetDate(Main.dateRange.Start),
                format: 'd-m-Y',
                calendars: 1
            });

            $("#customCalendarTo").DatePicker({
                flat: true,
                date: DateRangeUtil.resetDate(Main.dateRange.End),
                current: DateRangeUtil.resetDate(Main.dateRange.End),
                format: 'd-m-Y',
                calendars: 1
            });

            Main.customCalendarsExist = true;
        } else{
            $("#customCalendarFrom").DatePickerClear();
            $("#customCalendarFrom").DatePickerSetDate(DateRangeUtil.resetDate(Main.dateRange.Start), true);

            $("#customCalendarTo").DatePickerClear();
            $("#customCalendarTo").DatePickerSetDate(DateRangeUtil.resetDate(Main.dateRange.End), true);
        }

        submitDateClicked = false;
        if(e != null) {
            e.stopPropagation();
        }
    },

    datePickerSubmitClicked: function(e) {
        submitDateClicked = true;
    },

    datePickerClicked: function(e) {
        if(!submitDateClicked) {
            e.stopPropagation();
        }
    },

    hideDateSelector: function() {
        if ($("#customDateRangeSelection").is(':visible')) {
            $("#customDateRangeSelection").toggleClass("hide");
            Main.defaultCalendar();
        }
    },

    dateChangeApply: function(e) {
         if($("#customCalendarTo").DatePickerGetDate().getTime() > new Date().getTime())
         {
            alert("To date can't be in the future");
            e.stopPropagation();
         }
         else if($("#customCalendarFrom").DatePickerGetDate().getTime() > 
            $("#customCalendarTo").DatePickerGetDate().getTime())
        {
            alert("From date can't be bigger then To date");
            e.stopPropagation();
        }
        else
        {
            Main.dateRange.Start = $("#customCalendarFrom").DatePickerGetDate();
            Main.dateRange.End = $("#customCalendarTo").DatePickerGetDate();

            var numberOfDays = dayDiff(Main.dateRange.Start, Main.dateRange.End);

            Main.dateRangeCompare.End = new Date(Main.dateRange.Start.getTime() - (1 * 1000 * 60 * 60 * 24));
            Main.dateRangeCompare.Start = new Date(Main.dateRangeCompare.End.getTime() - (numberOfDays * 1000 * 60 * 60 * 24))

            $("#customDateRangeSelection").toggleClass("hide");

            $(ddlDate).val("dateChange");
            $(ddlDate).msDropDown({ showIcon: false });

            $.session.delete("dateRangeSelectedPredefined");
            $.session.set("dateRangeSelectedCustom", true);
            $.session.set("dateRangeSelectedCustomStart", Main.dateRange.Start.getTime()); 
            $.session.set("dateRangeSelectedCustomEnd", Main.dateRange.End.getTime());
            $.session.set("dateRangeSelectedCustomStartCompare", Main.dateRangeCompare.Start.getTime());
            $.session.set("dateRangeSelectedCustomEndCompare", Main.dateRangeCompare.End.getTime())

            Main.load();
        }
    },

    dateChangedToday : function() {
        Main.dateRange = DateRangeUtil.getToday();
        Main.dateRangeCompare = DateRangeUtil.getYesterday(Main.dateRange.Start);
    },

    dateChangedYesterday : function() {
        Main.dateRange = DateRangeUtil.getYesterday();
        Main.dateRangeCompare = DateRangeUtil.get1Week(Main.dateRange.Start);
    },

    dateChanged1Week : function() {
        Main.dateRange = DateRangeUtil.get1Week();
        Main.dateRangeCompare = DateRangeUtil.get2Weeks(Main.dateRange.Start);
    },

    dateChanged2Weeks : function() {
        Main.dateRange = DateRangeUtil.get2Weeks();
        Main.dateRangeCompare = DateRangeUtil.get1Month(Main.dateRange.Start);
    },

    dateChanged1Month : function() {
        Main.dateRange = DateRangeUtil.get1Month();
        Main.dateRangeCompare = DateRangeUtil.get2Months(Main.dateRange.Start);
    },

    dateChanged2Months : function() {
        Main.dateRange = DateRangeUtil.get2Months();
        Main.dateRangeCompare = DateRangeUtil.get3Months(Main.dateRange.Start);
    },

    dateChanged3Months : function() {
        Main.dateRange = DateRangeUtil.get3Months();
        Main.dateRangeCompare = DateRangeUtil.get6Months(Main.dateRange.Start);
    },

    dateChanged6Months : function() {
        Main.dateRange = DateRangeUtil.get6Months();
        Main.dateRangeCompare = DateRangeUtil.get1Year(Main.dateRange.Start);
    },

    dateChanged1Year : function() {
        Main.dateRange = DateRangeUtil.get1Year();
        Main.dateRangeCompare = DateRangeUtil.get2Years(Main.dateRange.Start);
    },

    setDate: function() {
        $('#dateRange').html(
                $.datepicker.formatDate('dd/mm/yy', Main.dateRange.Start) 
                + ' <br/> - ' + $.datepicker.formatDate('dd/mm/yy', Main.dateRange.End));
    },

    tileClick: function (tile) {
    
        var lastSelected = TileManager.getTileLastSelected();
        if(lastSelected != null && lastSelected != undefined) {
            $("#tileStage #tiles").find("#" + lastSelected.info.Id).toggleClass("selected");
        }

        $("#tileStage #tiles").find("#" + tile.info.Id).toggleClass("selected");

        ReportManager.dispose();
        ReportManager.init({
            reportId: tile.info.reportId,
            applicationId: tile.applicationId,
            dateRange: Main.dateRange,
            dateRangeCompare: Main.dateRangeCompare,
            chartButton: $("#viewSwitchAndParams #views #chart"),
            tableButton: $("#viewSwitchAndParams #views #table"),
            graph: $("#stage #graph"),
            table: $("#stage #data")
        });
        ReportManager.load(Main.reportLoading, Main.reportLoaded);
    },

    tilesLoaded: function() {
        $("#loadingOverlay").show();

        var tileToLoad = null; 
        
        if(TileManager.hasTiles()) {
            $("#tileStage #noData").hide();
            tileToLoad = TileManager.getFirstTileReportReady();
        } else {
            $("#tileStage #noData").show();
        }

        if(TileManager.hasTiles() && tileToLoad != null) {
            $("#report #stage #NoData").hide();
            
            ReportManager.init({
                reportId: tileToLoad.info.reportId,
                applicationId: Main.applicationId,
                dateRange: Main.dateRange,
                dateRangeCompare: Main.dateRangeCompare,
                chartButton: $("#viewSwitchAndParams #views #chart"),
                tableButton: $("#viewSwitchAndParams #views #table"),
                graph: $("#stage #graph"),
                table: $("#stage #data")
            });
            ReportManager.load(Main.reportLoading, Main.reportLoaded);

        } else {
            $("#report #stage #NoData").show();
            $("#loadingOverlay").hide();
        }
    },

    reportLoading: function() {
        $("#loadingOverlay").show();
    },

    reportLoaded: function() {
         $("#loadingOverlay").hide();
    },

    tileLoading: function() {
        $("#loadingOverlay").show();
    },

    render: function () {

        Main.setDate();

        TileManager.dispose();
        ReportManager.dispose();

        TileManager.init({
            applicationId: Main.applicationId,
            dateRange: Main.dateRange,
            dateRangeCompare: Main.dateRangeCompare,
            api: Main.api,
            stage: $("#tileStage #tiles"),
            tileLibrary: $("#tileLibrary div")
        });
        TileManager.load(Main.tileLoading, Main.tilesLoaded, Main.tileClick);
    },
};

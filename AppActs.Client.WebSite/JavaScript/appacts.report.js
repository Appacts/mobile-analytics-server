var ReportManager = {
    reportId: null,
    applicationId: null,
    report: null,
    dateRange: null,
    dateRangeCompare: null,
    chartButton: null,
    tableButton: null,
    graph: null,
    table: null,

    init: function (settings) {
        ReportManager.reportId = settings.reportId;
        ReportManager.applicationId = settings.applicationId;
        ReportManager.dateRange = settings.dateRange;
        ReportManager.dateRangeCompare = settings.dateRangeCompare;
        ReportManager.chartButton = settings.chartButton;
        ReportManager.tableButton = settings.tableButton;
        ReportManager.graph = settings.graph;
        ReportManager.table = settings.table

        ReportManager.bindEvents();
    },

    bindEvents: function () {
        ReportManager.chartButton.click(ReportManager.changeViewChart);
        ReportManager.tableButton.click(ReportManager.changeViewTable);
    },

    changeViewChart: function () {
        if (ReportManager.chartButton.hasClass("switchOff")) {
            ReportManager.tableButton.removeClass();
            ReportManager.tableButton.addClass("switchOff");
            ReportManager.chartButton.removeClass();
            ReportManager.chartButton.addClass("switchOn");

            ReportManager.graph.removeClass();
            ReportManager.table.removeClass();
            ReportManager.table.addClass("hide");
        }
    },

    changeViewTable: function () {
        if (ReportManager.tableButton.hasClass("switchOff")) {
            ReportManager.tableButton.removeClass();
            ReportManager.tableButton.addClass("switchOn");
            ReportManager.chartButton.removeClass();
            ReportManager.chartButton.addClass("switchOff");

            ReportManager.table.removeClass();
            ReportManager.graph.removeClass();
            ReportManager.graph.addClass("hide");
        }
    },

    unbindEvents: function () {
        $(ReportManager.chartButton).unbind("click");
        $(ReportManager.tableButton).unbind("click");
    },

    load: function (loadingFunc, loadedFunc) {
        ReportManager.report = new Report(
            ReportManager.reportId,
            ReportManager.applicationId,
            ReportManager.dateRange,
            ReportManager.dateRangeCompare
        );
        ReportManager.report.addHandlerReportLoading(loadingFunc);
        ReportManager.report.addHandlerReportLoaded(loadedFunc);
        ReportManager.report.load();
        ReportManager.render();
    },

    render: function () {

    },

    dispose: function () {

        if (ReportManager.chartButton != null && ReportManager.tableButton != null) {
            ReportManager.chartButton.removeClass();
            ReportManager.chartButton.addClass("switchOn");
            ReportManager.tableButton.removeClass();
            ReportManager.tableButton.addClass("switchOff");
        }

        if (ReportManager.report != null) {
            ReportManager.report.dispose();
            ReportManager.report = null;
        }
        ReportManager.unbindEvents();
    }
};




function Report(reportId, applicationId, dateRange, dateRangeCompare) {

    var self = this;
    self.reportId = reportId;
    self.applicationId = applicationId;
    self.compareApplication = false;
    self.comparePlatform = false;
    self.compareVersion = false;
    self.info = null;
    self.normalData = null;
    self.compareApplicationsData = null;
    self.comparePlatformsData = null;
    self.compareVersionsData = null;
    self.reportName = null;
    self.chart = null;
    self.lastReportFunc = null;
    self.dateRange = dateRange;
    self.dateRangeCompare = dateRangeCompare;
    self.reportLoading = function() { };
    self.reportLoaded = function() { };

    self.load = function () {
        self.loading();
        self.normalLoad(function (result) {
            if (result != null) {
                self.loaded();
                self.render(result);
                self.loadGraph(self.normalData.data);
                self.bindEvents();
                self.lastReportFunc = self.normalShow;
            }
        });
    };

    self.bindEvents = function () {
        $("#report #options #normal").click(self.normalClick);
        $("#report #options #compareApplications").click(self.compareApplicationsClick);
        $("#report #options #comparePlatforms").click(self.comparePlatformsClick);
        $("#report #options #compareVersions").click(self.compareVersionsClick);
    };

    self.unbindEvents = function () {
        $("#report #options #normal").unbind("click");
        $("#report #options #compareApplications").unbind("click");
        $("#report #options #comparePlatforms").unbind("click");
        $("#report #options #compareVersions").unbind("click");
    };


    self.render = function (result) {

        self.reportName = result.Info.Name.toUpperCase();
        self.compareApplication = result.Info.CompareApplicationGuid;
        self.comparePlatform = result.Info.ComparePlatformGuid;
        self.compareVersion = result.Info.CompareVersionGuid;

        $("#viewSwitchAndParams").show();
        $("#stage #name #title").text(result.Info.Name);
        $("#stage #name #desc").text('(' + result.Data.YLabel + ' vs ' + result.Data.XLabel + ')');

        $("#report #options #normal").show();

        var compareApplicationElement = $("#report #options #compareApplications");
        self.compareApplication != null ? compareApplicationElement.show() : compareApplicationElement.hide();

        var comparePlatformsElement = $("#report #options #comparePlatforms");
        self.comparePlatform != null ? comparePlatformsElement.show() : comparePlatformsElement.hide();

        var ccompareVersionsElement = $("#report #options #compareVersions");
        self.compareVersion != null ? ccompareVersionsElement.show() : ccompareVersionsElement.hide();

        $("#stage #graph").removeClass("hide");
    };


    self.normalClick = function () {
        self.normalShow(null);
    };

    self.normalShow = function (callback) {
        $("#report #options").find(".select").removeClass("select");
        $("#report #options #normal").addClass("select");
        self.hideAll();

        if (self.normalData.startDate != self.dateRange.Start ||
            self.normalData.endDate != self.dateRange.End) {
            self.loading();
            self.normalLoad(function (result) {
                if (result != null) {
                    self.loaded();
                    self.normalData.show();
                    self.loadGraph(self.normalData.data);

                    if (callback != null) {
                        callback(result);
                    }
                }
            });
        } else {
            self.normalData.show();
            self.loadGraph(self.normalData.data);

            if (callback != null) {
                callback(self.normalData.data);
            }
        }

        self.lastReportFunc = self.normalShow;
    }

    self.normalLoad = function (callback) {
        if (self.normalData != null) {
            self.normalData.dispose();
        }
        self.normalData = new ReportDataNormal
            (
                self.reportId,
                self.applicationId,
                self.dateRange.Start,
                self.dateRange.End,
                $("#report #data #normal #table"),
                $("#report #data #normal #template").html(),
                $("#report #data #normal #paging")
            );

        self.normalData.load(function (result) {
            callback(result)
        });
    };

    self.compareApplicationsClick = function () {
        self.compareApplicationsShow(function(result) {
            if(result.NotEnoughData) {
                self.hideAll();
                $("#report #options").find(".select").removeClass("select");
                $("#report #options #normal").addClass("select");
                $("#report #options #compareApplications").hide();
                self.normalData.show();
            }
        });
    };

    self.compareApplicationsShow = function (callback) {

        $("#report #options").find(".select").removeClass("select");
        $("#report #options #compareApplications").addClass("select");
        self.hideAll();

        if (self.compareApplicationsData != null &&
            self.dateRange.Start.getTime() == self.compareApplicationsData.startDate.getTime() &&
             self.dateRange.End.getTime() == self.compareApplicationsData.endDate.getTime()) {

            self.compareApplicationsData.show();
            self.loadGraph(self.compareApplicationsData.data);
            self.compareApplicationsData.renderOptions();
            self.compareApplicationsData.unbindEvents();
            self.compareApplicationsData.bindEvents();

            if (callback != null) {
                callback(self.compareApplicationsData.data);
            }

        } else {
            self.loading();
            if (self.compareApplicationsData == null) {
                self.compareApplicationsLoad(true, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            } else {
                self.compareApplicationsLoad(false, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            }
        }

        self.lastReportFunc = self.compareApplicationsShow;
    }

    self.compareApplicationsLoad = function (initialLoad, callback) {
        if (initialLoad) {
            self.compareApplicationsData = new ReportDataCompare
                (
                    self.compareApplication,
                    self.applicationId,
                    self.dateRange.Start,
                    self.dateRange.End,
                    $("#report #data #compareApplications #table"),
                    $("#report #data #compareApplications #templateTriple").html(),
                    $("#report #data #compareApplications #templateDouble").html(),
                    $("#report #data #compareApplications #templateSingle").html(),
                    $("#report #data #compareApplications #paging"),
                    $("#optionsViewSwitchAndParams #params"),
                    $("#report #data #compareApplications #templateSelectors").html(),
                    AppActs.Client.WebSite.WebService.Report.GetGraphWithApplicationCompare,
                    AppActs.Client.WebSite.WebService.Report.GetGraphApplications
                );
            self.compareApplicationsData.addHandlerSelectionsChange(self.onSelectionsChanged);
            self.compareApplicationsData.addHandlerSelectionsChanging(self.onSelectionsChanging);
        }

        if (initialLoad) {
            self.compareApplicationsData.load(callback);
        } else {
            self.compareApplicationsData.reload(self.startDate, self.endDate, callback);
        }
    };

    self.comparePlatformsClick = function () {
        self.comparePlatformsShow(function(result) {
            if(result.NotEnoughData) {
                self.hideAll();
                $("#report #options").find(".select").removeClass("select");
                $("#report #options #normal").addClass("select");
                $("#report #options #comparePlatforms").hide();
                self.normalData.show();
            }
        });
    };

    self.comparePlatformsShow = function (callback) {
        $("#report #options").find(".select").removeClass("select");
        $("#report #options #comparePlatforms").addClass("select");
        self.hideAll();

        if (self.comparePlatformsData != null &&
            self.comparePlatformsData.startDate.getTime() == self.dateRange.Start.getTime()
            && self.comparePlatformsData.endDate.getTime() == self.dateRange.End.getTime()) {

            self.comparePlatformsData.show();
            self.loadGraph(self.comparePlatformsData.data);
            self.comparePlatformsData.renderOptions();
            self.comparePlatformsData.unbindEvents();
            self.comparePlatformsData.bindEvents();

            if (callback != null) {
                callback(self.comparePlatformsData.data);
            }

        } else {
            self.loading();
            if (self.comparePlatformsData == null) {
                self.comparePlatformsLoad(true, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            } else {
                self.comparePlatformsLoad(false, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            }
        }

        self.lastReportFunc = self.comparePlatformsShow;
    }

    self.comparePlatformsLoad = function (initialLoad, callback) {

        if (initialLoad) {
            self.comparePlatformsData = new ReportDataCompare
            (
                self.comparePlatform,
                self.applicationId,
                self.dateRange.Start,
                self.dateRange.End,
                $("#report #data #comparePlatforms #table"),
                $("#report #data #comparePlatforms #templateTriple").html(),
                $("#report #data #comparePlatforms #templateDouble").html(),
                $("#report #data #comparePlatforms #templateSingle").html(),
                $("#report #data #comparePlatforms #paging"),
                $("#optionsViewSwitchAndParams #params"),
                $("#report #data #comparePlatforms #templateSelectors").html(),
                AppActs.Client.WebSite.WebService.Report.GetGraphWithPlatformCompare,
                AppActs.Client.WebSite.WebService.Report.GetGraphPlatform
            );
            self.comparePlatformsData.addHandlerSelectionsChange(self.onSelectionsChanged);
            self.comparePlatformsData.addHandlerSelectionsChanging(self.onSelectionsChanging);
        }

        if (initialLoad) {
            self.comparePlatformsData.load(callback);
        } else {
            self.comparePlatformsData.reload(self.startDate, self.endDate, callback);
        }
    };

    self.compareVersionsClick = function () {
        self.compareVersionsShow(function(result) {
            if(result.NotEnoughData) {
                self.hideAll();
                $("#report #options").find(".select").removeClass("select");
                $("#report #options #normal").addClass("select");
                $("#report #options #compareVersions").hide();
                self.normalData.show();
            }
        });
    };

    self.compareVersionsShow = function (callback) {
        $("#report #options").find(".select").removeClass("select");
        $("#report #options #compareVersions").addClass("select");
        self.hideAll();

        if (self.compareVersionsData != null
            && self.compareVersionsData.startDate.getTime() == self.dateRange.Start.getTime()
            && self.compareVersionsData.endDate.getTime() == self.dateRange.End.getTime()) {

            self.compareVersionsData.show();
            self.loadGraph(self.compareVersionsData.data);
            self.compareVersionsData.renderOptions();
            self.compareVersionsData.unbindEvents();
            self.compareVersionsData.bindEvents();

            if (callback != null) {
                callback(self.compareVersionsData.data);
            }

        } else {
            self.loading();
            if (self.compareVersionsData == null) {
                self.compareVersionsLoad(true, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            } else {
                self.compareVersionsLoad(false, function (result) {
                    self.dataLoaded(result);
                    if (callback != null) {
                        callback(result);
                    }
                });
            }
        }
        self.lastReportFunc = self.compareVersionsShow;
    }

    self.compareVersionsLoad = function (initialLoad, callback) {

        if (initialLoad) {
            self.compareVersionsData = new ReportDataCompare
            (
                self.compareVersion,
                self.applicationId,
                self.dateRange.Start,
                self.dateRange.End,
                $("#report #data #compareVersions #table"),
                $("#report #data #compareVersions #templateTriple").html(),
                $("#report #data #compareVersions #templateDouble").html(),
                $("#report #data #compareVersions #templateSingle").html(),
                $("#report #data #compareVersions #paging"),
                $("#optionsViewSwitchAndParams #params"),
                $("#report #data #compareVersions #templateSelectors").html(),
                AppActs.Client.WebSite.WebService.Report.GetGraphWithVersionsCompare,
                AppActs.Client.WebSite.WebService.Report.GetGraphVersions
            );
            self.compareVersionsData.addHandlerSelectionsChange(self.onSelectionsChanged);
            self.compareVersionsData.addHandlerSelectionsChanging(self.onSelectionsChanging);
        }

        if (initialLoad) {
            self.compareVersionsData.load(callback);
        } else {
            self.compareVersionsData.reload(self.startDate, self.endDate, callback);
        }
    };

    self.onSelectionsChanging = function() {
        self.loading();
    }

    self.onSelectionsChanged = function (result) {
        self.dataLoaded(result);
    };

    self.addHandlerReportLoading = function(func) {
        self.reportLoading = func;
    };

    self.addHandlerReportLoaded = function(func){
        self.reportLoaded = func;
    };

    self.dataLoaded = function (result) {
        if (result != null) {
            self.loadGraph(result);
            self.loaded();
        }
    };

    self.loadGraph = function (data) {
        if (data != null) {
            if (self.chart != null) {
                self.chart.dispose();
                self.chart = null;
            }

            var detailOptions = {
                    detailGuid: data.Info != undefined ? data.Info.DetailGuid : null,
                    applicationId: self.applicationId,
                    startDate: self.dateRange.Start,
                    endDate: self.dateRange.End
                }

            self.chart = new Chart($("#stage #graph"),
                data.Data.Series,
                data.Data.XType,
                data.Data.XLabel,
                data.Data.YLabel,
                data.Data.YYLabel,
                data.Data.ChartType,
                data.Data.Ticker,
                data.Data.YY,
                detailOptions);
            self.chart.load();
        }
    };

    self.reset = function () {
        $("#stage #name #title").text('');
        $("#stage #name #desc").text('');
        $("#report #options #compareApplications").hide();
        $("#report #options #comparePlatforms").hide();
        $("#report #options #compareVersions").hide();
        $("#report #options #normal").hide();
        $("#viewSwitchAndParams").hide();
        $("#stage > #graph").addClass("hide");
        $("#stage > #data").addClass("hide");
    };

    self.loading = function () {
        self.reportLoading();
    };

    self.loaded = function () {
        self.reportLoaded();
    };

    self.hideAll = function () {
        if (self.normalData != null) {
            self.normalData.hide();
        }

        if (self.compareVersionsData != null) {
            self.compareVersionsData.hide();
        }

        if (self.compareApplicationsData != null) {
            self.compareApplicationsData.hide();
        }

        if (self.comparePlatformsData != null) {
            self.comparePlatformsData.hide();
        }
    };

    self.dispose = function () {
        self.unbindEvents();

        if (self.normalData != null) {
            self.normalData.dispose();
            self.normalData = null;
        }

        if (self.compareApplicationsData != null) {
            self.compareApplicationsData.dispose();
            self.compareApplicationsData = null;
        }

        if (self.comparePlatformsData != null) {
            self.comparePlatformsData.dispose();
            self.comparePlatformsData = null;
        }

        if (self.compareVersionsData != null) {
            self.compareVersionsData.dispose();
            self.compareVersionsData = null;
        }

        if (self.chart != null) {
            self.chart.dispose();
            self.chart = null;
        }

        self.reset();
        
        $("#report #options").find(".select").removeClass("select");
        $("#report #options #normal").addClass("select");

        //reset graph
        $("#stage > #graph").attr("class", "normal");
    };
}



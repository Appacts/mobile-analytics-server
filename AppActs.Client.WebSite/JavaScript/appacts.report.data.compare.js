
function ReportDataCompare(reportId, applicationId, startDate, endDate, tableElement,
    templateTriple, templateDouble, templateSingle, pagingElement, selectorsElement, selectorsTemplate, apiInitial, api) {

    var self = this;
    self.reportId = reportId;
    self.applicationId = applicationId;
    self.startDate = startDate;
    self.endDate = endDate;
    self.data = null;
    self.rowsPerPage = 16;
    self.tableElement = tableElement;
    self.templateTriple = templateTriple;
    self.templateDouble = templateDouble;
    self.templateSingle = templateSingle;
    self.pagingElement = pagingElement;
    self.pages = 0;
    self.currentPage = 0;
    self.totalRows = 0;
    self.selectorsElement = selectorsElement;
    self.selectorsTemplate = selectorsTemplate;
    self.Options = null;
    self.Selected = null;
    self.apiInitial = apiInitial;
    self.api = api;
    self.callback = null;
    self.onSelectionsChanging = null;
    self.onSelectionsChange = null;

    self.load = function (callback) {
        self.apiInitial(
            self.reportId,
            self.applicationId,
            self.startDate,
            self.endDate,
            self.requestLoaded,
            self.requestErrored
        );

        self.callback = callback;
    };

    self.requestLoaded = function (result) {
        if (!result.NotEnoughData) {
            self.data = result;
            self.Options = result.Options;

            self.Selected = [];
            for (var i = 0; i < result.Selected.length; i++) {
                self.Selected[i] = result.Selected[i].toString();
            }

            if (self.callback != null) {
                self.callback(result);
                self.callback = null;
            }
            self.render();
        } else {
            if (self.callback != null) {
                self.callback(result);
                self.callback = null;
            }
        }
    };


    self.reload = function (startDate, endDate, callback) {
        self.startDate = startDate;
        self.endDate = endDate;
        self.api(
            self.reportId,
            self.applicationId,
            self.Selected,
            self.startDate,
            self.endDate,
            self.requestReLoaded,
            self.requestErrored
        );
        self.callback = callback;
    };

    self.reloadSelections = function (callback) {
        self.api(
            self.reportId,
            self.applicationId,
            self.Selected,
            self.startDate,
            self.endDate,
            self.requestReLoaded,
            self.requestErrored
        );
        self.callback = callback;
    }

    self.requestReLoaded = function (result) {
        self.data = result;
        if (self.callback != null) {
            self.callback(result);
            self.callback = null;
        }
        self.render();
    };

    self.bindEvents = function () {
        self.pagingElement.find("#prev").click(self.prevClick);
        self.pagingElement.find("#next").click(self.nextClick);
        self.selectorsElement.find("input").bind("change", self.selectorsChange);
    };

    self.unbindEvents = function () {
        self.pagingElement.find("#prev").unbind("click");
        self.pagingElement.find("#next").unbind("click");
        self.selectorsElement.find("input").unbind("change");
    };

    self.requestErrored = function (result) {
        if (self.callback != null) {
            self.callback(null);
        }
    };

    self.render = function () {

        if (self.data.Tabular.Data != null &&
             self.data.Tabular.Data.length > 0) {

            self.pages = 0;
            self.currentPage = 0;
            self.totalRows = 0;

            //sorting paging out
            self.totalRows = self.data.Tabular.Data.length;
            self.pages = Math.ceil((self.totalRows / self.rowsPerPage));

            var tableData = self.data.Tabular.Data.slice(0, self.rowsPerPage);

            self.renderTable(tableData);

            self.tableElement.find("tbody > tr").each(function (index, value) {
                $(value).find("td:even").addClass("alter");
            });

            self.tableElement.parent().show();
            self.pagingElement.find("#current").text(self.currentPage + 1);
            self.pagingElement.find("#total").text(self.pages);

            self.pagingElement.find("#prev").hide();

            if (self.pages > 1) {
                self.pagingElement.find("#next").show();
            } else {
                self.pagingElement.find("#next").hide();
            }

            self.renderOptions();

            self.unbindEvents();
            self.bindEvents();
        } else {
            //no data was received
        }
    };

    self.renderOptions = function () {
        //hanlding options
        if (self.Options != null && self.Selected != null) {
            self.selectorsElement.html(Mustache.to_html(self.selectorsTemplate, { Options: self.Options }));
            self.selectorsElement.find("input").each(function (index, value) {
                $.each(self.Selected, function (index, val) {
                    if (value.value == val.toString()) {
                        $(value).attr('checked', 'checked');
                        return;
                    }
                });

            });

            self.selectorsElement.show();
        }        
    };

    self.renderTable = function (tableData) {
        if (self.Selected.length == 3) {
            self.tableElement.html(Mustache.to_html(self.templateTriple, {
                header0: self.data.Tabular.Header0,
                header1: self.data.Tabular.Header1,
                header2: self.data.Tabular.Header2,
                xLabel: self.data.Data.XLabel,
                yLabel: self.data.Data.YLabel,
                data: tableData
            }));
        } else if (self.Selected.length == 2) {
            self.tableElement.html(Mustache.to_html(self.templateDouble, {
                header0: self.data.Tabular.Header0,
                header1: self.data.Tabular.Header1,
                xLabel: self.data.Data.XLabel,
                yLabel: self.data.Data.YLabel,
                data: tableData
            }));
        } else if (self.Selected.length == 1) {
            self.tableElement.html(Mustache.to_html(self.templateSingle, {
                header0: self.data.Tabular.Header0,
                xLabel: self.data.Data.XLabel,
                yLabel: self.data.Data.YLabel,
                data: tableData
            }));
        }
    };

    self.addHandlerSelectionsChange = function (func) {
        self.onSelectionsChange = func;
    }

    self.addHandlerSelectionsChanging = function (func) {
        self.onSelectionsChanging = func;
    }

    self.selectorsChange = function (event) {

        if ($(event.target).prop("checked")) {
            if (self.Selected.length == 3) {
                $(event.target).removeAttr('checked');
                self.showTooManyOptionsSelected();
            } else if (self.Selected.length < 3) {
                self.Selected.push($(event.target).val());
                self.onSelectionsChanging();
                self.reloadSelections(function (result) {
                    self.onSelectionsChange(result);
                });
                self.selectorsElement.find("#message").fadeOut();
            }
        } else if (self.Selected.length != 1) {
            var index = self.Selected.indexOf($(event.target).val())
            self.Selected.splice(index, 1);
            self.onSelectionsChanging();
            self.reloadSelections(function (result) {
                self.onSelectionsChange(result);
            });
            self.selectorsElement.find("#message").fadeOut();
        } else {
            $(event.target).attr('checked', 'checked');
            self.showTooLittleOptionsSelected();
        }
    };

    self.showTooManyOptionsSelected = function () {
        self.selectorsElement.find("#message").fadeIn();
    };

    self.showTooLittleOptionsSelected = function () {
        self.selectorsElement.find("#message").fadeIn();
    };

    self.nextClick = function () {
        self.currentPage++;

        var tableData = self.data.Tabular.Data;
        var tableData = tableData.slice(self.rowsPerPage * self.currentPage,
            (self.currentPage + 1) * self.rowsPerPage);

        self.renderTable(tableData);

        self.tableElement.find("tbody > tr").each(function (index, value) {
            $(value).find("td:even").addClass("alter");
        });

        self.pagingElement.find("#current").text(self.currentPage + 1);
        self.pagingElement.find("#total").text(self.pages);

        self.pagingElement.find("#prev").show();

        if ((self.currentPage + 1) < self.pages) {
            self.pagingElement.find("#next").show();
        } else {
            self.pagingElement.find("#next").hide();
        }
    };

    self.prevClick = function () {
        self.currentPage--;

        var tableData = self.data.Tabular.Data;
        tableData = tableData.slice(self.rowsPerPage * self.currentPage,
            (self.currentPage + 1) * self.rowsPerPage);

        self.renderTable(tableData);

        self.tableElement.find("tbody > tr").each(function (index, value) {
            $(value).find("td:even").addClass("alter");
        });

        self.pagingElement.find("#current").text(self.currentPage + 1);
        self.pagingElement.find("#total").text(self.pages);

        self.pagingElement.find("#next").show();

        if (self.currentPage != 0) {
            self.pagingElement.find("#prev").show();
        } else {
            self.pagingElement.find("#prev").hide();
        }
    };

    self.hide = function () {
        self.tableElement.parent().hide();
        self.selectorsElement.hide();
    };

    self.show = function () {
        self.tableElement.parent().show();
        self.selectorsElement.show();
    };

    self.dispose = function () {
        self.unbindEvents();
        self.pagingElement.find("#next").hide();
        self.pagingElement.find("#prev").hide();
        self.pagingElement.find("#current").text(0);
        self.pagingElement.find("#total").text(0);
        self.tableElement.html('');
        self.hide();
        self.selectorsElement.html('');
        self.data = null;
        self.callback = null;
    };
}


function ReportDataNormal(reportId, applicationId, startDate, endDate, tableElement,
    template, pagingElement) {

    var self = this;
    self.reportId = reportId;
    self.applicationId = applicationId;
    self.startDate = startDate;
    self.endDate = endDate;
    self.data = null;
    self.rowsPerPage = 16;
    self.tableElement = tableElement;
    self.template = template;
    self.pagingElement = pagingElement;
    self.pages = 0;
    self.currentPage = 0;
    self.totalRows = 0;
    self.callback = null;
    self.reportDetail = null;

    self.load = function (callback) {
        AppActs.Client.WebSite.WebService.Report.GetGraphWithInfo(
            self.reportId,
            self.applicationId,
            self.startDate,
            self.endDate,
            self.requestLoaded,
            self.requestErrored
        )
        self.callback = callback;
    };

    self.requestLoaded = function (result) {
        self.data = result;
        if (self.callback != null) {
            self.callback(result);
        }
        self.render();
        self.bindEvents();
    };

    self.requestErrored = function (result) {
        if (self.callback != null) {
            self.callback(null);
        }
    };

    self.bindEvents = function () {
        self.pagingElement.find("#prev").click(self.prevClick);
        self.pagingElement.find("#next").click(self.nextClick);

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").click(self.cellClick);
        }

        $(window).click(self.windowClick);
    };

    self.unbindEvents = function () {
        self.pagingElement.find("#prev").unbind("click");
        self.pagingElement.find("#next").unbind("click");

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").unbind("click");
        }

        $(window).unbind("click", self.windowClick);
    };

    self.windowClick = function () {
        if (self.reportDetail != null) {
            self.reportDetail.dispose();
            self.reportDetail = null;
        }
    };

    self.render = function () {

        if (self.data.Tabular.Axis != null &&
            self.data.Tabular.Axis.length > 0) {

            if (self.data.Data.XType == "System.DateTime") {
                var dates = getDates(self.startDate, self.endDate);
                var dic = [];

                for (var z = 0; z < self.data.Tabular.Axis.length; z++) {
                    self.data.Tabular.Axis[z].X = new Date
                    (
                        self.data.Tabular.Axis[z].X.getUTCFullYear(),
                        self.data.Tabular.Axis[z].X.getUTCMonth(),
                        self.data.Tabular.Axis[z].X.getUTCDate()
                    );

                    dic[self.data.Tabular.Axis[z].X.getTime()] = self.data.Tabular.Axis[z];
                }

                for (var z = 0; z < dates.length; z++) {
                    var tick = dates[z].getTime();
                    if (dic[tick] != undefined) {
                        var row = dic[tick];
                        row.X = $.datepicker.formatDate('dd/mm/yy', row.X);
                        self.data.Tabular.Axis[z] = dic[tick];
                    } else {
                        self.data.Tabular.Axis[z] = {
                            X: $.datepicker.formatDate('dd/mm/yy', dates[z]),
                            Y: 0
                        };
                    }
                }

                dic = null;
                dates = null;
            }

            var fixedPoint = 0;

            if (self.data.Data.XType == "System.Decimal") {
                fixedPoint = 2
            }

            for (var z = 0; z < self.data.Tabular.Axis.length; z++) {
                self.data.Tabular.Axis[z].Y = self.data.Tabular.Axis[z].Y.toFixed(fixedPoint);
            }

            self.totalRows = self.data.Tabular.Axis.length;
            self.pages = Math.ceil((self.totalRows / self.rowsPerPage));

            var tableData = self.data.Tabular.Axis.slice(0, self.rowsPerPage);
            self.tableElement.html(Mustache.to_html(self.template, {
                xLabel: self.data.Data.XLabel,
                yLabel: self.data.Data.YLabel,
                axis: tableData
            }));

            self.tableElement.find("tbody > tr > td:even").addClass("alter");

            self.tableElement.parent().show();
            self.pagingElement.find("#current").text(self.currentPage + 1);
            self.pagingElement.find("#total").text(self.pages);

            self.pagingElement.find("#prev").hide();

            if (self.pages > 1) {
                self.pagingElement.find("#next").show();
            } else {
                self.pagingElement.find("#next").hide();
            }

            if (self.data.Info.DetailGuid != null) {
                self.tableElement.find("tbody > tr > td").addClass("canBeSelected");
            }

        } else {
            //no data was received
        }
    };

    self.cellClick = function (e) {

        if (self.reportDetail != null) {
            self.reportDetail.dispose();
            self.reportDetail = null;
        }

        var index = self.tableElement.find("tbody > tr > td").index(this);
        var detailId = $(self.tableElement.find("thead > tr > td")[index]).text();

        self.reportDetail =
            new ReportDetail(
                self.data.Info.DetailGuid,
                self.applicationId,
                self.startDate,
                self.endDate,
                detailId,
                { y: $(this).offset().top, x: $(this).offset().left }
            );
        var cell = $(this);
        cell.addClass("selected");
        var funcDispose = function () {
            cell.removeClass("selected");
        };
        self.reportDetail.load(function (result) {
            //loaded
        }, funcDispose);

        e.stopPropagation();
    };


    self.nextClick = function () {
        self.currentPage++;

        var tableData = self.data.Tabular.Axis;
        var tableData = tableData.slice(self.rowsPerPage * self.currentPage,
            (self.currentPage + 1) * self.rowsPerPage);

        var html = Mustache.to_html(self.template, {
            xLabel: self.data.Data.XLabel,
            yLabel: self.data.Data.YLabel,
            axis: tableData
        });

        self.tableElement.html(html);
        self.tableElement.find("tbody > tr > td:even").addClass("alter");

        self.pagingElement.find("#current").text(self.currentPage + 1);
        self.pagingElement.find("#total").text(self.pages);

        self.pagingElement.find("#prev").show();

        if ((self.currentPage + 1) < self.pages) {
            self.pagingElement.find("#next").show();
        } else {
            self.pagingElement.find("#next").hide();
        }

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").addClass("canBeSelected");
        }

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").click(self.cellClick);
        }
    };

    self.prevClick = function () {
        self.currentPage--;

        var tableData = self.data.Tabular.Axis;
        tableData = tableData.slice(self.rowsPerPage * self.currentPage, (self.currentPage + 1) * self.rowsPerPage);

        var html = Mustache.to_html(self.template, {
            xLabel: self.data.Data.XLabel,
            yLabel: self.data.Data.YLabel,
            axis: tableData
        });

        self.tableElement.html(html);
        self.tableElement.find("tbody > tr > td:even").addClass("alter");
        self.pagingElement.find("#current").text(self.currentPage + 1);
        self.pagingElement.find("#total").text(self.pages);

        self.pagingElement.find("#next").show();

        if (self.currentPage != 0) {
            self.pagingElement.find("#prev").show();
        } else {
            self.pagingElement.find("#prev").hide();
        }

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").addClass("canBeSelected");
        }

        if (self.data.Info.DetailGuid != null) {
            self.tableElement.find("tbody > tr > td").click(self.cellClick);
        }
    };

    self.hide = function () {
        self.tableElement.parent().hide();
    };

    self.show = function () {
        self.tableElement.parent().show();
    };

    self.dispose = function () {
        self.unbindEvents();

        self.pagingElement.find("#next").hide();
        self.pagingElement.find("#prev").hide();
        self.pagingElement.find("#current").text(0);
        self.pagingElement.find("#total").text(0);

        if (self.reportDetail != null) {
            self.reportDetail.dispose();
            self.reportDetail = null;
        }

        self.tableElement.html('');
        self.hide();
        self.data = null;
    };
}
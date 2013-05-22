
function ReportDetail(reportDetailId, applicationId, 
    startDate, endDate, detailId, position) {

    var self = this;
    self.reportDetailId = reportDetailId;
    self.applicationId = applicationId;
    self.startDate = startDate;
    self.endDate = endDate;
    self.detailId = detailId;
    self.callback = function (result) { };
    self.callbackDispose = function () { };
    self.data = null;
    self.report = null;
    self.position = position;

    self.load = function (callback, callbackDispose) {
        self.preRender();

        AppActs.Client.WebSite.WebService.Report.GetDetail(
            self.reportDetailId,
            self.applicationId,
            self.startDate,
            self.endDate,
            self.detailId,
            self.requestLoaded,
            self.requestErrored
        )
        self.callback = callback;
        self.callbackDispose = callbackDispose;
    };

    self.requestLoaded = function (result) {
        self.data = result;

        //extend functionality of the type
        self.data.formatDate = function () {
            return function (num, render) {
                var date = new Date(parseInt(render(num)));
                return $.datepicker.formatDate('dd/mm/yy', date);
            }
        }

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
        $(self.report).click(self.reportClick)
    };

    self.unbindEvents = function () {
        $(self.report).unbind("click");
    };

    self.reportClick = function (e) {
        e.stopPropagation();
    };

    self.preRender = function () {
        var positionStage = $("#report #stage").offset();
        self.report = $("#stage #detailReport");
        self.report.css({ top: self.position.y + 45 });
        self.report.find(".point").css({ left: (self.position.x - positionStage.left) - 10 });
        self.report.find(".loading").show();
        self.report.show();
    };

    self.render = function () {
        var detailReport = $("#detailLibrary").find(("#" + self.reportDetailId));
        var template = detailReport.find("#template").html();
        var html = Mustache.to_html(template, self.data);
        self.report.find("#loading").hide();
        self.report.find(".detailReportTable").append(html);
    };

    self.hide = function () {
        self.report.hide();
    };

    self.show = function () {
        self.report.show();    
    };

    self.dispose = function () {
        self.callbackDispose();
        self.callbackDispose = null;
        self.unbindEvents();
        self.report.find(".detailReportTable table").remove();
        self.report.find(".loading").show();
        self.report.hide();
        self.report = null;
        self.cell = null;
        self.callback = null;
        self.data = null;
        self.element = null;
    };
}
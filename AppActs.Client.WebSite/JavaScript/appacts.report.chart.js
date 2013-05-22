
function Chart(element, data, xType, xLabel,
    yLabel, yyLabel, chartType, ticker, yy, detailOptions) {

    var self = this;
    self.element = element;
    self.data = data;
    self.xType = xType;
    self.xLabel = xLabel;
    self.yLabel = yLabel;
    self.yyLabel = yyLabel;
    self.chartType = chartType;
    self.ticker = ticker;
    self.yy = yy;
    self.previousPoint = null;
    self.chartColorPallet = ['#F94B06', '#32539A', '#C21554', '#ED8135', '#7F317A', '#FFFFC0CB', '#FFC0C0C0',
    '#FFFFFF00', '#FFA52A2A', '#FF00FF00', '#FF8B0000'];
    self.options = null;
    self.stopPropogation = false;
    
    if(detailOptions != null) {
        self.detailGuid = detailOptions.detailGuid;
        self.applicationId = detailOptions.applicationId;
        self.startDate = detailOptions.startDate;
        self.endDate = detailOptions.endDate;
    }

    self.load = function () {
        if (self.data.length == 0) {
            self.empty();
            return;
        }

        self.options = {
            yaxis: { min: 0 },
            grid: { borderWidth: 0, hoverable: true, clickable: true },
            colors: self.chartColorPallet
        };

        if (self.chartType == 'Line') {
            self.options.series = { lines: { show: true, fill: true }, points: { show: true} };
        } else if (self.chartType == 'Bar') {
            self.options.series = {  lines: { show: false, steps: true }, bars: { show: true, align: 'center'} };
        } else if (self.chartType == 'Pie') {
            self.options.series = {
                pie: {
                    show: true,
                    radius: 1,
                    label: {
                        show: true,
                        radius: 3 / 4,
                        formatter: function (label, series) {
                            if (series.percent > 2) {
                                return '<div style="font-size:15px;font-family:\'Oswald\', Arial;text-align:center;padding:2px;color:white;background:black;">' +
                                    label.toUpperCase() + ' - ' + Math.round(series.percent) + '%</div>';
                            }
                            return '';
                        },
                        background: {
                            opacity: 0.9,
                            color: '#000'
                        }
                    }
                }
            };
        }

        if (self.xType == "System.DateTime") {
            self.options.xaxis = { mode: "time" };
        } else {
            self.options.xaxis = { ticks: self.ticker };
        }

        if (self.data.length > 1 && self.options.series.pie == undefined) {
            self.options.legend = { show: true };
        } else {
            self.options.legend = { show: false };
        }

        self.render();
    };

    self.render = function () {

        if (self.xType == "System.DateTime") {
            var dates = getDates(self.startDate, self.endDate);
            for (var i = 0; i < self.data.length; i++) {

                var dic = [];

                for (var z = 0; z < self.data[i].data.length; z++) {
                    self.data[i].data[z][0] = new Date
                    (
                        self.data[i].data[z][0].getUTCFullYear(),
                        self.data[i].data[z][0].getUTCMonth(),
                        self.data[i].data[z][0].getUTCDate()
                    );

                    dic[self.data[i].data[z][0].getTime()] = self.data[i].data[z];
                }

                for (var z = 0; z < dates.length; z++) {
                    var tick = dates[z].getTime();
                    if (dic[tick] != undefined) {
                        self.data[i].data[z] = dic[tick];
                    } else {
                        self.data[i].data[z] = [dates[z], 0];
                    }
                }

                dic = null;
            }
            dates = null;
        }

        $.plot(self.element, self.data, self.options);

        if (self.xType == "System.TimeSpan") {
            $("div.xAxis div.tickLabel").addClass("Horiz");
            if ($.browser.msie) {
                $("div.xAxis div.tickLabel").css("margin-top", "10px");
            } else {
                $("div.xAxis div.tickLabel").css("margin-top", "30px");
            }
        }

        self.unbindEvents();
        self.bindEvents();
    };

    self.bindEvents = function () {
        if (self.chartType != 'Pie') {
            self.element.bind("plothover", self.hover);
        }

        if (self.detailGuid != null) {
            self.element.bind("plotclick", self.click);
            $(window).click(self.windowClick);
        }
    };

    self.unbindEvents = function () {
        if (self.chartType != 'Pie') {
            self.element.unbind("plothover", self.hover);
        }

        if (self.detailGuid != null) {
            self.element.unbind("plotclick", self.click);
            $(window).unbind("click", self.windowClick);
        }
    };

    self.windowClick = function () {
        if (!self.stopPropogation) {
            if (self.reportDetail != null) {
                self.reportDetail.dispose();
                self.reportDetail = null;
            }
        } else {
            self.stopPropogation = false;
        }
    };

    self.empty = function () {
        //data is empty
    };

    self.click = function (event, pos, item) {
        if (item) {

            if (self.reportDetail != null) {
                self.reportDetail.dispose();
                self.reportDetail = null;
            }

            self.previousPoint = item.dataIndex;
            var x, y;
            x = item.datapoint[0];
            y = item.datapoint[1];

            if (self.xType == "System.DateTime" && self.chartType != 'Pie') {
                var date = new Date();
                date.setTime(x);
                x = date.format("dd/MM/yyyy");
            } else if (self.chartType == 'Pie') {
                x = item.series.label;
            } else if(self.xType == "System.String") {
                x = self.ticker[x][1];
            }

            self.reportDetail = new ReportDetail(
                self.detailGuid,
                self.applicationId,
                self.startDate,
                self.endDate,
                x,
                {
                    y: item.pageY != null ? item.pageY - 30 : pos.pageY,
                    x: item.pageX != null ? item.pageX - 25 : pos.pageX
                }
            );



            self.reportDetail.load(function (result) {
                $("#dataTooltip").remove();
            }, function () {
                //disposed
            });

            self.stopPropogation = true;
        }
    };

    self.hover = function (event, pos, item) {
        if (item) {
            if (self.previousPoint != item.dataIndex) {

                self.previousPoint = item.dataIndex;

                $("#dataTooltip").remove();

                var x, y;
                x = item.datapoint[0];
                y = item.datapoint[1];


                if (self.xType == "System.DateTime") {
                    var date = new Date();
                    date.setTime(x);
                    x = date.format("dd/MM/yyyy");
                }
                else {
                    x = self.ticker[x][1];
                }

                var content = "";
                if (item.series.label != undefined && item.series.label.length > 0
                    && self.data.length > 1) {
                    content += "<strong>" + item.series.label + "</strong><br/>";
                }

                content += self.xLabel + ': <strong>' + x + "</strong><br/>";

                if (self.yyLabel != null && self.yyLabel.length > 0) {
                    content += self.yyLabel + ': <strong>' + self.yy[item.series.label][item.dataIndex] + "</strong><br/>";
                } else {
                    content += self.yLabel + ': <strong>' + y + "</strong><br/>";
                }

                if (self.detailGuid != null) {
                    content += "Click for detail<br/>";
                }

                self.showTooltip(item.pageX, item.pageY, content);

                if (self.detailGuid != null) {
                    document.body.style.cursor = 'pointer';
                }

            }
        }
        else {
            $("#dataTooltip").remove();
            self.previousPoint = null;
            if (self.detailGuid != null) {
                document.body.style.cursor = 'default';
            }
        }
    };

    self.showTooltip = function (x, y, contents) {
        $('<div id="dataTooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 5,
            padding: '2px',
            'background': 'black',
            opacity: 0.90,
            "z-index": 99999999,
            "font-family": "'Oswald', Arial",
            "color": "white"
        }).appendTo("body").fadeIn(200);
    };

    self.dispose = function () {
        self.unbindEvents();
        self.options = null;
        self.element.html('');
    };
}
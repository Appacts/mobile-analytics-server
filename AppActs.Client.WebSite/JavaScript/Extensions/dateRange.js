function getDates(startDate, stopDate) {
    var dateArray = new Array();
    var currentDate = new Date(startDate.getTime());
    while (currentDate <= stopDate) {
        dateArray.push(new Date(currentDate));
        currentDate.setDate(currentDate.getDate() + 1);
    }
    return dateArray;
}

function dayDiff(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24)
}

var DateRangeUtil = {

    getToday: function () {
        return new DateRange(this.resetDate(new Date()), new Date());
    },

    getYesterday: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setDate(startDate.getDate() - 1);
        return new DateRange(this.resetDate(startDate), endDate);
    },

    get1Week: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setDate(startDate.getDate() - 7);
        return new DateRange(this.resetDate(startDate), endDate);
    },

    get2Weeks: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setDate(startDate.getDate() - 14);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get1Month: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 1);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get2Months: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 2);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get3Months: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 3);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get6Months: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 6);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get1Year: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 12);
        return new DateRange(this.resetDate(startDate), endDate);
    },


    get2Years: function (endDate) {
        if (endDate == undefined)
            endDate = new Date();

        var startDate = new Date();
        startDate.setMonth(startDate.getMonth() - 24);
        return new DateRange(this.resetDate(startDate), endDate);
    },

    resetDate: function (date) {
        return new Date(date.setHours(0, 0, 0, 0));
    }
};

function DateRange(start, end) {
    var self = this;
    self.Start = start;
    self.End = end;
    return self;
}
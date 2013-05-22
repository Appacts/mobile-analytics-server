var TileManager = {

    applicationId: null,
    dateRange: null,
    dateRangeCompare: null,
    api: null,

    tiles: new Array(),
    isDisposing: false,
    tileClickFunc: function () { },
    stage: null,
    tilesLoaded: 0,
    tilesTotal: 0,
    tilesLoadedFunc: function () { },
    tilesLoadingFunc: function () { },
    tileLibrary: null,
    tileLastSelected: null,

    init: function (settings) {
        this.applicationId = settings.applicationId;
        this.dateRange = settings.dateRange;
        this.dateRangeCompare = settings.dateRangeCompare;
        this.api = settings.api;
        this.stage = settings.stage;
        this.tileLibrary = settings.tileLibrary;
        this.isDisposing = false;
    },

    load: function (tilesLoading, tilesLoaded, tileClick) {
        this.tilesLoadedFunc = tilesLoaded;
        this.tilesLoadingFunc = tilesLoading;
        this.tileClickFunc = tileClick;

        this.tilesLoadingFunc();

        this.tilesTotal = $(this.tileLibrary).length;

        $(this.tileLibrary).each(function (key, value) {
            if (!TileManager.isDisposing) {
                var info = new TileInfo(value);
                var tile = new Tile(TileManager.applicationId, TileManager.api, TileManager.dateRange, TileManager.dateRangeCompare, info);

                tile.load(function (tile) {
                    TileManager.addTile(tile);
                });
            }
        });

    },

    addTile: function (tile) {
        this.tilesLoaded++;
        if (tile != null) {
            this.tiles.push(tile);

            this.stage.html(this.stage.html() + tile.content);
            this.stage.find('#' + tile.info.Id).fadeIn("slow");

            if (tile.info.reportId != null) {
                tile.addHandlerClicked(function (tileSelected) {
                    TileManager.tileClickFunc(tileSelected)
                    TileManager.tileLastSelected = tileSelected;
                });
            } else {
                this.stage.find('#' + tile.info.Id).addClass("noAction");
            }
        }

        if (this.tilesLoaded == this.tilesTotal) {
            this.bindEvents();
            this.tilesLoadedFunc();
        }
    },

    getTiles: function () {
        return this.tiles;
    },

    getFirstTileReportReady: function () {
        for (var i = 0; i < this.tiles.length; i++) {
            if (this.tiles[i].info.reportId != null) {
                return this.tiles[i];
            }
        }
    },

    hasTiles: function () {
        return this.tiles.length > 0;
    },

    getTileLastSelected: function () {
        return TileManager.tileLastSelected;
    },

    bindEvents: function () {
        for (var i = 0; i < this.tiles.length; i++) {
            this.stage.find("#" + this.tiles[i].info.Id).on("click", this.tiles[i], this.tiles[i].click);
        }
    },

    unbindEvents: function () {
        for (var i = 0; i < this.tiles.length; i++) {
            this.stage.find("#" + this.tiles[i].info.Id).off("click", this.tiles[i], this.tiles[i].click);
        }
    },

    dispose: function () {
        this.isDisposing = true;
        this.unbindEvents();
        if (this.stage != null)
            this.stage.html('');
        for (var i = 0; i < this.tiles.length; i++) {
            this.tiles[i].dispose();
            this.tiles[i] = null;
        }
        this.tiles = new Array();
        this.tilesLoaded = 0;
        this.tilesLoading = 0;
        this.tilesLoadedFunc = function () { };
        this.tilesLoadingFunc = function () { };
        this.tileLibrary = null;
        this.tileLastSelected = null;
    }
};


function Tile(applicationId, api, dateRange, dateRangeCompare, info) {

    var self = this;
    self.applicationId = applicationId;
    self.api = api;
    self.callback = function () { };
    self.dateRange = dateRange;
    self.dateRangeCompare = dateRangeCompare;
    self.isDisposing = false;
    self.tileClick = null;
    self.info = info;
    self.content = null;

    self.load = function (callback) {
//if (debug && console.time != undefined) console.time(self.info.Id);

        self.callback = callback;

        if (!self.isDisposing) {
            self.api(self.info.tileId,
            self.applicationId,
            self.dateRange.Start,
            self.dateRange.End,
            self.dateRangeCompare.Start,
            self.dateRangeCompare.End,
            self.render,
            self.error);
        }
    };

    self.error = function (result) {

    };

    self.addHandlerClicked = function (func) {
        self.tileClick = func;
    };

    self.click = function (event) {
        if (self.tileClick != null) {
            self.tileClick(event.data);
        }
    };

    self.render = function (result) {
        //if (debug && console.timeEnd != undefined) console.timeEnd(self.info.Id);

        if (!self.isDisposing) {
            if (result != null) {
                result.HasValueTwo = result.ValueTwo != undefined && result.ValueTwo != null;
                result.HasValueThree = result.ValueThree != undefined && result.ValueThree != null;
                self.content = Mustache.to_html(self.info.template, result);
                self.callback(self);
            } else {
                self.callback(null);
            }
        }
    };

    self.dispose = function () {
        self.isDisposing = true;
        self.callback = null;
        self.info = null;
        self.content = null;
    };
}

function TileInfo(html) {
    this.tileId = $(html).find('#tileId').val();
    this.reportId = $(html).find('#reportId').val();
    this.template = $(html).find('#template').html();
    this.Id = $(html).attr('id');
}
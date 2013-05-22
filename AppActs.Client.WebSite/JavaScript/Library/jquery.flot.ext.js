function showTooltip(x, y, contents) {
    $('<div id="dataTooltip">' + contents + '</div>').css({
        position: 'absolute',
        display: 'none',
        top: y + 5,
        left: x + 5,
        border: '1px solid #fdd',
        padding: '2px',
        'background-color': '#FFA500',
        opacity: 0.80,
        "z-index": 99999999
    }).appendTo("body").fadeIn(200);
}

var chartColorPallet = ['#FFFFA500', '#FF006400', '#FF800080', 'blue', '#FFA9A9A9', '#FFFFC0CB', '#FFC0C0C0', '#FFFFFF00', '#FFA52A2A', '#FF00FF00', '#FF8B0000'];
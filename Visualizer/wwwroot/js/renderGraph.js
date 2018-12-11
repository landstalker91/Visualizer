var nodes = null;
var edges = null;
var network = null;
var directionInput = document.getElementById("direction");

function destroy() {
    if (network !== null) {
        network.destroy();
        network = null;
    }
}

function draw(data) {
    destroy();

    var connectionCount = [];

    // create a network
    var container = document.getElementById('mynetwork');

    var options = {
        edges: {
            smooth: {
                type: 'cubicBezier',
                forceDirection: (directionInput.value == "UD" || directionInput.value == "DU") ? 'vertical' : 'horizontal',
                roundness: 0.4
            }
        },

        layout: {
            hierarchical: {
                direction: directionInput.value
            }
        },
        physics: false
    };
    network = new vis.Network(container, data, options);

    // add event listeners
    network.on('select', function (params) {
        document.getElementById('selection').innerHTML = 'Selection: ' + params.nodes;
    });


    network.on("click", function (params) {
        params.event = "[original event]" + test;
        //alert('test:' + JSON.stringify(params
        //alert('table:' + document.getElementById('t'));

        if (params.nodes == []) {
            document.getElementById('t').style.display = 'none';
        } else {
            document.getElementById('t').style.display = 'block';
        }

        document.getElementById('eventSpan').innerHTML = '<h2>Click event:</h2>' + JSON.stringify(params, null, 4);

    });
    network.on("doubleClick", function (params) {
        params.event = "[original event]";
        document.getElementById('eventSpan').innerHTML = '<h2>doubleClick event:</h2>' + JSON.stringify(params, null, 4);
    });
    network.on("oncontext", function (params) {
        params.event = "[original event]";
        document.getElementById('eventSpan').innerHTML = '<h2>oncontext (right click) event:</h2>' + JSON.stringify(params, null, 4);
    });
    network.on("dragStart", function (params) {
        // There's no point in displaying this event on screen, it gets immediately overwritten
        params.event = "[original event]";
        console.log('dragStart Event:', params);
        console.log('dragStart event, getNodeAt returns: ' + this.getNodeAt(params.pointer.DOM));
    });
    network.on("dragging", function (params) {
        params.event = "[original event]";
        document.getElementById('eventSpan').innerHTML = '<h2>dragging event:</h2>' + JSON.stringify(params, null, 4);
    });
    network.on("dragEnd", function (params) {
        params.event = "[original event]";
        document.getElementById('eventSpan').innerHTML = '<h2>dragEnd event:</h2>' + JSON.stringify(params, null, 4);
        console.log('dragEnd Event:', params);
        console.log('dragEnd event, getNodeAt returns: ' + this.getNodeAt(params.pointer.DOM));
    });
    network.on("zoom", function (params) {
        //var test;
        //debugger;
        console.log('zoom Event:', params);
        document.getElementById('eventSpan').innerHTML = '<h2>zoom event:</h2>' + JSON.stringify(params, null, 4);
    });
    network.on("showPopup", function (params) {
        alert(params);
        //var myCell = document.getElementById('thiselem');//указываем элемент в который вставляем данные
        //myCell.innerHTML = params.nodes[0];

        document.getElementById('eventSpan').innerHTML = '<h2>showPopup event: </h2>' + JSON.stringify(params, null, 4);
    });
    network.on("hidePopup", function () {
        console.log('hidePopup Event');
    });
    network.on("select", function (params) {
        console.log('select Event:', params);
    });
    network.on("selectNode", function (params) {
        console.log('selectNode Event:', params);
    });
    network.on("selectEdge", function (params) {
        console.log('selectEdge Event:', params);
    });
    network.on("deselectNode", function (params) {
        console.log('deselectNode Event:', params);
    });
    network.on("deselectEdge", function (params) {
        console.log('deselectEdge Event:', params);
    });
    network.on("hoverNode", function (params) {
        console.log('hoverNode Event:', params);
    });
    network.on("hoverEdge", function (params) {
        console.log('hoverEdge Event:', params);
    });
    network.on("blurNode", function (params) {
        console.log('blurNode Event:', params);
    });
    network.on("blurEdge", function (params) {
        console.log('blurEdge Event:', params);
    });
}


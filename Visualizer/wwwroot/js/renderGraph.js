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

function draw() {
    destroy();
    nodes = [];
    edges = [];
    var connectionCount = [];
    var DIR = '/images/';

    // randomly create some nodes and edges
    for (var i = 0; i < 15; i++) {
        nodes.push({ id: i, label: String(i), image: DIR + 'Network-Pipe-icon.png', shape: 'image' });
    }

    edges.push({ from: 0, to: 1, value: 0 });
    edges.push({ from: 0, to: 6 });
    edges.push({ from: 0, to: 13 });
    edges.push({ from: 0, to: 11 });
    edges.push({ from: 1, to: 2 });
    edges.push({ from: 2, to: 3 });
    edges.push({ from: 2, to: 4 });
    edges.push({ from: 3, to: 5 });
    edges.push({ from: 1, to: 10 });
    edges.push({ from: 1, to: 7 });
    edges.push({ from: 2, to: 8 });
    edges.push({ from: 2, to: 9 });
    edges.push({ from: 3, to: 14 });
    edges.push({ from: 1, to: 12 });
    nodes[0]["level"] = 0;
    nodes[1]["level"] = 1;
    nodes[2]["level"] = 3;
    nodes[3]["level"] = 4;
    nodes[4]["level"] = 4;
    nodes[5]["level"] = 5;
    nodes[6]["level"] = 1;
    nodes[7]["level"] = 2;
    nodes[8]["level"] = 4;
    nodes[9]["level"] = 4;
    nodes[10]["level"] = 2;
    nodes[11]["level"] = 1;
    nodes[12]["level"] = 2;
    nodes[13]["level"] = 1;
    nodes[14]["level"] = 5;


    // create a network
    var container = document.getElementById('mynetwork');
    var data = {
        nodes: nodes,
        edges: edges
    };

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
        params.event = "[original event]";
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


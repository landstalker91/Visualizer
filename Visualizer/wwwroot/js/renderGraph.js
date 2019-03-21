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

function ajaxGetNode(params) {
    var retData = null;

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Home/ajaxGetNode/',
        data: {
            ID: params.nodes
        },
        success: function (data) {
            // обработка полученных данных
            // data.returnCode
            // data.message
            retData = data;
        },
        error: function (error) {
            alert('Error');
        },
        dataType: 'json'
    });
    return retData;
}

function ajaxGetLink(params) {
    var retData = null;

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Home/ajaxGetLink/',
        data: {
            ID: params.edges
        },
        success: function (data) {
            // обработка полученных данных
            // data.returnCode
            // data.message
            retData = data;
        },
        error: function (error) {
            alert('Error');
        },
        dataType: 'json'
    });
    return retData;
}

function draw(data) {
    destroy();

    // create a network
    var container = document.getElementById('mynetwork');

    var options = {
        edges: {
            smooth: {
                type: 'cubicBezier',
                forceDirection: (directionInput.value == "UD" || directionInput.value == "DU") ? 'vertical' : 'horizontal',
                roundness: 0.3
            }
        },
        layout: {
            hierarchical: {
                direction: directionInput.value,
                sortMethod: "directed",
                nodeSpacing: 250,
                levelSeparation: 250,
                enabled: true
            }
        },
        physics: false
    };
    network = new vis.Network(container, data, options);

    // add event listeners
    network.on('select', function (params) {
        var data = ajaxGetNode(params);
        var description =
            '<h2>КЕ: ' + data.id + '</h2><br>' +
            '<b>Название:</b> ' + data.name + '<br>' +
            '<b>Категория:</b> ' + data.category + '<br>'
        $('#node_description').html(description);

        var data = ajaxGetLink(params);
        var description =
            '<h2>Связь: ' + data.id + '</h2><br>' +
            '<b>Вес:</b> ' + data.weight + '<br>' +
            '<b>Тип:</b> ' + data.type + '<br>'
        $('#link_description').html(description);
    });
    network.on("doubleClick", function (params) {
        //params.event = "[original event]";
        document.location.href = document.location.origin + document.location.pathname.substring(0, document.location.pathname.lastIndexOf('/') + 1) + params.nodes;
        //document.getElementById('eventSpan').innerHTML = '<h2>doubleClick event:</h2>' + JSON.stringify(params, null, 4);
    });
    
    network.on("click", function (params) {
        if (params.nodes.length == 0) {
            document.getElementById('node_description').style.display = 'none';
        } else {
            document.getElementById('node_description').style.display = 'block';
        }

        if (params.edges.length != 1) {
            document.getElementById('link_description').style.display = 'none';
        } else {
            document.getElementById('link_description').style.display = 'block';
        }

        if (params.edges.length == 0 || params.edges.length == 0) {
            document.getElementById('ig_infopanel').style.display = 'none';
        } else {
            document.getElementById('ig_infopanel').style.display = 'block';
        }
        //document.getElementById('eventSpan').innerHTML = '<h2>Click event:</h2>' + JSON.stringify(params, null, 4);
    });
    network.on('showPopup', function (params) {
        //document.getElementById('eventSpan').innerHTML = '<h2>showPopup event: </h2>' + JSON.stringify(params, null, 4);
    });
}
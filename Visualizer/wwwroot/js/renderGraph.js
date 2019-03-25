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
        nodes: {
            margin: { left: 50 },
            shape: 'circularImage',
            widthConstraint: { maximum: 80 }
        },
        edges: {
            smooth: {
                type: 'cubicBezier',
                forceDirection: (directionInput.value == "UD" || directionInput.value == "DU") ? 'vertical' : 'horizontal',
                roundness: 0.3
            },
            arrows: {
                from: {
                    enabled: true,
                    type: 'arrow'
                }
            },
            scaling: { max: 5, min: 1 },
            font: { vadjust: 20, strokeWidth: 5 }
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
            '<font face="Calibri"><h2>' + data.modelLongName + '</h2><br>' +
            '<b>Название:</b> ' + data.name + '<br>' +
            '<b>ID</b>: ' + data.id + '<br>' +
            (data.modelShortName ? ('<b>Модель (краткое):</b> ' + data.modelShortName + '<br>') : "") + 
            (data.subCategory ? ('<b>Подкатегория:</b> ' + data.subCategory + '<br>') : "") +
            (data.category ? ('<b>Категория:</b> ' + data.category + '<br>') : "") +
            (data.status ? ('<b>Статус:</b> ' + data.status + '<br>') : "") +
            (data.cost != 0 ? ('<b>Стоимость:</b> ' + data.cost + '<br>') : "") +
            (data.location ? ('<b>Местоположение:</b> ' + data.location + '<br></font>') : "");
            $('#node_description').html(description);

        var data = ajaxGetLink(params);
        var description =
            '<h2>Связь: ' + data.id + '</h2><br>' +
            '<b>Вес:</b> ' + data.weight + '<br>' +
            '<b>Тип:</b> ' + data.type + '<br>'
        $('#link_description').html(description);
    });

    network.on("doubleClick", function (params) {
        if (params.nodes.length != 0) {
            document.location.href = document.location.origin + document.location.pathname.substring(0, document.location.pathname.lastIndexOf('/') + 1) + params.nodes;
        }
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

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

function ajaxGetNode(id) {
    var retData = null;

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Home/ajaxGetNode/',
        data: {
            ID: id
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

function ajaxGetLink(id) {
    var retData = null;

    $.ajax({
        type: 'POST',
        async: false,
        url: '/Home/ajaxGetLink/',
        data: {
            ID: id
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
            widthConstraint: { maximum: 80 },
            shadow: {
                enabled: true
            }
        },
        edges: {
            smooth: {
                type: 'continuous',
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
        var linkInfo = '';
        var link = {};
        var description = '';
  
        if (params.nodes.length != 0) {

            var node = ajaxGetNode(params.nodes[0]);
            description =

                '<div class="infoHeader">' + node.modelLongName + '<br></div>' +
                '<div class="inp-1">' +
                '<b>Ид:</b> <a href="' + document.location.origin + document.location.pathname.substring(0, document.location.pathname.lastIndexOf('/') + 1) + node.id + '" class="renderLink">' + node.id + '</a>' + '<br>' +
                '<b>Название:</b> ' + node.name + '<br>' +
                (node.modelShortName ? ('<b>Модель (краткое):</b> ' + node.modelShortName + '<br>') : "") +
                (node.subCategory ? ('<b>Подкатегория:</b> ' + node.subCategory + '<br>') : "") +
                (node.category ? ('<b>Категория:</b> ' + node.category + '<br>') : "") +
                (node.status ? ('<b>Статус:</b> ' + node.status + '<br>') : "") +
                (node.cost != 0 ? ('<b>Стоимость:</b> ' + node.cost + '<br>') : "") +
                (node.location ? ('<b>Местоположение:</b> ' + node.location + '<br></font>') : "") +
                '</div><br/>';

            $('#node_description').html(description);
        } else {
            $('#node_description').html('');
        }

        if (params.edges.length != 0) {
            if (typeof params.edges == 'object') {
                description = '<div class="infoHeader">Связи<br></div>';
                params.edges.forEach(function (element) {
                    link = ajaxGetLink(element);
                    if (link.clientId == params.nodes[0]) {
                        linkInfo = "Связь с ресурсом " + ajaxGetNode(link.resourceId).modelShortName;
                    } else if (link.resourceId == params.nodes[0]) {
                        linkInfo = "Связь с клиентом " + ajaxGetNode(link.clientId).modelShortName;
                    } else {
                        linkInfo = "Связь между " + ajaxGetNode(link.resourceId).modelShortName + " и " + ajaxGetNode(link.clientId).modelShortName;
                    }

                    description +=
                        '<input class="hideLink" id="hd-' + link.id + '" type="checkbox">' +
                        '<label class="lab-1" for="hd-' + link.id + '">' +
                        '<br><b><u><font face="Calibri"><font size="4">' + linkInfo + '</font></b></u><br>' +
                        '</label>' +
                        '<div class="inp-1">' +
                        '<b>Вес:</b> ' + link.weight + '%<br>' +
                        '<b>Тип:</b> ' + link.type + '<font color="' + link.color + '"> •</font>  <br></font>' +
                        '</div>';
                });
                $('#link_description').html(description);
            } else {
                $('#link_description').html("");
            }
        } else {
            $('#link_description').html("");
        }

        if (node.childs.length != 0) {
            description = '<div class="infoHeader">Состав<br></div>';
            node.childs.forEach(function (element) {

                description +=
                    '<input class="hideChild" id="hd-' + element.id + '" type="checkbox">' +
                    '<label class="lab-1" for="hd-' + element.id + '">' +
                    '<br><b><u><font face="Calibri"><font size="4">' + (element.name ? element.name : element.modelLongName) + '</font></b></u><br>' +
                    '</label>' +
                    '<div class="inp-1">' +
                    '<b>Ид:</b> <a href="' + document.location.origin + document.location.pathname.substring(0, document.location.pathname.lastIndexOf('/') + 1) + element.id + '" class="renderLink">' + element.id + '</a><br>' +
                    (element.modelShortName ? ('<b>Модель (краткое):</b> ' + element.modelShortName + '<br>') : "") +
                    (element.category ? ('<b>Категория:</b> ' + element.category + '<br>') : "") +
                    '</div>';
            });
            $('#childs_description').html(description);
        } else {
            $('#childs_description').html("");
        } 
    });

    network.on("doubleClick", function (params) {
        if (params.nodes.length != 0) {
            document.location.href = document.location.origin + document.location.pathname.substring(0, document.location.pathname.lastIndexOf('/') + 1) + params.nodes;
        }
    });

    network.on("click", function (params) {
        if (params.nodes.length == 0) {
            document.getElementById('node_description').style.display = 'none';
            document.getElementById('childs_description').style.display = 'none';
        } else {
            document.getElementById('node_description').style.display = 'block';
            document.getElementById('childs_description').style.display = 'block';
        }

        if (params.edges.length == 0) {
            document.getElementById('link_description').style.display = 'none';
        } else {
            document.getElementById('link_description').style.display = 'block';
        }

        if (params.nodes.length == 0 && params.edges.length == 0) {
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
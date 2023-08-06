var tableItems = null;
var sortColumns = [];

$(function () {
    tableItems = $("#tableItems").DataTable({
        //"order": [[1, 'asc']],
        colReorder: true,
        "columns": [
            {
                "data": "id",
                render: function (data, type, row) {
                    let dataId = 'data-id="' + data + '"';
                    let btnEdit = '<button id="btnEdit" class="btn btn-sm btn-outline-info" '
                        + dataId + '><i class="fa-solid fa-pen-to-square"></i></button>';

                    let btnDelete = '<button id="btnDelete" class="btn btn-sm btn-outline-danger" '
                        + dataId + '><i class="fa-solid fa-trash"></i></button>';
                    return btnEdit + " " + btnDelete;
                }
            },
            {
                "data": "orderDate",
                render: function (data, type, row) {
                    return data.split("T")[0]
                }
            },
            { "data": "region" },
            { "data": "city" },
            { "data": "category" },
            { "data": "product" },
            { "data": "quantity", render: $.fn.dataTable.render.number(',', '.', 0, '') },
            { "data": "unitPrice", render: $.fn.dataTable.render.number(',', '.', 2, '') },
            { "data": "totalPrice", render: $.fn.dataTable.render.number(',', '.', 2, '') }
        ]
    });

    initializeData();

    $(document).on('click', '#btnSearch', function (e) {
        e.preventDefault()
        setWaitingSearchButton("btnSearch");
        setTimeout(function () {
            searchData();
            setNormalSearchButton("btnSearch");
        }, 500);
    }).on('click', '#btnDelete', function () {
        if (confirm("do you want delete item?")) {
            let id = $(this).data("id");
            let row = $(this).parents('tr');
            $.ajax({
                "url": hostApi + 'api/foodsales?id=' + id,
                "method": "DELETE",
                "timeout": 0,
                success: function (response) {
                    hideError();
                    tableItems.row(row).remove().draw();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    showError(jqXHR.responseJSON.errors.join(','));
                }
            });
        }
    }).on('click', '#btnCreateNew', function () {
        //Show create form
        $("#contentCreateOrEdit").removeClass("d-none");
        $("#searchTableContent").addClass("d-none");
    }).on('click', '#btnEdit', function () {
        //Show edit form
        let id = $(this).data("id");
        let row = $(this).parents("tr");
        let dataRow = tableItems.row(row).data();
        $("#hfId").val(id);
        $("#OrderDate").val(dataRow.orderDate.split('T')[0]);
        $("#Region").val(dataRow.region);
        $("#City").val(dataRow.city);
        $("#Category").val(dataRow.category);
        $("#Product").val(dataRow.product);
        $("#Quantity").val(dataRow.quantity);
        $("#UnitPrice").val(dataRow.unitPrice);

        $("#contentCreateOrEdit").removeClass("d-none");
        $("#searchTableContent").addClass("d-none");

    }).on('click', '#btnBack', function () {
        //Clear data from controls
        $("#OrderDate").val($("#hfCurrentDate").val());
        $("#hfId").val("");
        $("#Region").val("");
        $("#City").val("");
        $("#Category").val("");
        $("#Product").val("");
        $("#Quantity").val("0");
        $("#UnitPrice").val("0");

        //Hide create/edit form
        $("#contentCreateOrEdit").addClass("d-none");
        $("#searchTableContent").removeClass("d-none");
    }).on('submit', '#formCreateOrEdit', function (e) {
        e.preventDefault();

        let id = $("#hfId").val();
        let isNew = id === "";
        let method = isNew ? "POST" : "PUT";

        var data = isNew ? {
            "orderDate": $("#OrderDate").val(),
            "region": $("#Region").val(),
            "city": $("#City").val(),
            "category": $("#Category").val(),
            "product": $("#Product").val(),
            "quantity": parseInt($("#Quantity").val()),
            "unitPrice": parseFloat($("#UnitPrice").val())
        } : {
            "id": id,
            "orderDate": $("#OrderDate").val(),
            "region": $("#Region").val(),
            "city": $("#City").val(),
            "category": $("#Category").val(),
            "product": $("#Product").val(),
            "quantity": parseInt($("#Quantity").val()),
            "unitPrice": parseFloat($("#UnitPrice").val())
        };

        ajaxRequest('api/foodsales', method, data, function (result) {
            if (result.isSuccess) {
                $("#btnSearch").click();
                $("#btnBack").click();
                hideErrorSave();
            }
            else {
                showErrorSave(result.errors.join(','))
            }
        });
    })
})

function initializeData() {
    setWaitingSearchButton("btnSearch");
    initializeSearchControl();
    setTimeout(function () {
        searchData(true);
        setNormalSearchButton("btnSearch");
    }, 500);
}

function initializeSearchControl() {
    $.get(hostApi + 'api/foodsales/sort', function (result) {
        $("#SortColumn").empty()
        $.each(result.result.sortColumns, function (index, row) {
            $("#SortColumn").append("<option value='" + row + "'>" + row + "</option>")
            if (sortColumns.indexOf(row) < 0) {
                sortColumns.push(row);
            }
        })
    });
}

function searchData(isInit = false) {
    let data = {
        "limit": parseInt($("#RowLimit").val()),
        "pageNumber": 1,
        "orderDate": {
            "fromDate": isInit ? null : getValueFromControl("SearchFromOrderDate"),
            "toDate": isInit ? null : getValueFromControl("SearchToOrderDate")
        },
        "region": getValueFromControl("SearchRegion"),
        "city": getValueFromControl("SearchCity"),
        "category": getValueFromControl("SearchCategory"),
        "product": getValueFromControl("SearchProduct"),
        "quantity": {
            "min": parseInt(getValueFromControl("SearchQuantityMin")),
            "max": parseInt(getValueFromControl("SearchQuantityMax"))
        },
        "unitPrice": {
            "min": parseFloat(getValueFromControl("SearchUnitPriceMin")),
            "max": parseFloat(getValueFromControl("SearchUnitPriceMax"))
        },
        "totalPrice": {
            "min": parseFloat(getValueFromControl("SearchTotalPriceMin")),
            "max": parseFloat(getValueFromControl("SearchTotalPriceMax"))
        },
        "sortColumnName": $("#SortColumn").val(),
        "sortType": $("#SortType").val()
    };
    ajaxRequest('api/foodsales/all', 'POST', data, function (result) {
        let column = $("#SortColumn").val();
        let sortType = $("#SortType").val().toLowerCase();
        let index = sortColumns.indexOf(column);
        tableItems.rows().remove();
        tableItems.rows
            .add(result.result)
            .order([(index < 0 ? 1 : index + 1), sortType]).draw();
    });
}

function ajaxRequest(path, method, data, callback) {
    $.ajax({
        url: hostApi + path,
        type: method,
        dataType: 'json',
        timeout: 0,
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(data),
        success: function (response) {
            callback(response)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            callback(jqXHR.responseJSON)
        }
    });
}

function convertToDate(date) {
    if (date === "" || date === null || date === undefined) {
        return null;
    }

    let year = date.split('/')[2]
    let month = date.split('/')[1]
    let day = date.split('/')[0]
    return year + '-' + month + '-' + day
}

function getValueFromControl(id) {
    let value = $("#" + id).val();
    return value === "" || value === undefined ? null : value;
}

function setWaitingSearchButton(id) {
    $('#' + id).empty();
    $('#' + id).append("<i class='fa-solid fa-circle-notch fa-spin'></i> Search");
    $('#' + id).attr("disabled", "disabled");
}

function setNormalSearchButton(id) {
    $('#' + id).empty();
    $('#' + id).append("<i class='fa-solid fa-magnifying-glass'></i> Search");
    $('#' + id).removeAttr("disabled");
}

function showError(message) {
    $("#alertErrorSearch").removeClass("d-none");
    $("#errorMessageSearch").empty().html(message);
}
function hideError() {
    $("#alertErrorSearch").addClass("d-none");
}

function showErrorSave(message) {
    $("#alertErrorSave").removeClass("d-none");
    $("#errorMessageSave").empty().html(message);
}

function hideErrorSave() {
    $("#alertErrorSave").addClass("d-none");
}
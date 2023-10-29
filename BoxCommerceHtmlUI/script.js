$(document).ready(function () {
    

    $('#create-order-spinner').removeClass('d-none');

    $.ajax({
        url: 'https://localhost:7072/Item/getItems',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            if (data.success) {
                var itemsTable = $('#items-table');

                data.items.forEach(function (item) {
                    var row = '<tr>' +
                        '<td>' + item.itemID + '</td>' +
                        '<td>' + item.name + '</td>' +
                        '<td>' + item.description + '</td>' +
                        '<td>' + item.itemType + '</td>' +
                        '<td>' + item.price + '</td>' +
                        '<td>' + item.stockQuantity + '</td>' +
                        '</tr>';

                    itemsTable.append(row);
                });
            }
            $('#table-spinner').addClass('d-none');
        },
        error: function (error) {
            $('#table-spinner').addClass('d-none');
            console.error('Error fetching data:', error);
        }
    });
});


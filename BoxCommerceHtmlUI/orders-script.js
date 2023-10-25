function populateCustomerDropdown() {
    $.get("https://localhost:7072/Customer/getCustomers", function (data) {
        if (data.success) {
            var dropdown = $("#customer-dropdown");
            dropdown.empty();
            $.each(data.customers, function (index, customer) {
                dropdown.append($("<option></option>")
                    .attr("value", customer.customerID)
                    .text(customer.name));
            });
            $('#table-spinner').addClass('d-none');
        }
    });
}

function populateOrderTable() {
    $('#table-spinner').removeClass('d-none'); 
    $.get("https://localhost:7072/Order/getOrders/0")
    .done(function (data) {
        if (data.success) {
            var orderTableBody = $("#order-table-body");
            orderTableBody.empty();
                $.each(data.orders, function (index, order) {
                    // Creating edit button 
                    var editButton = $("<button>")
                    .text("Edit")
                    .addClass("btn btn-primary")
                    .on("click", function () {
                        
                        $("#OrderId").val(order.orderID);
                        $("#CustomerId").val(order.customerID);
                        
                        populateOrderItemsTable(order.orderID);
                        
                        $("#itemModal").modal("show");
                    });

                    var actionColumn = $("<td>").append(editButton);

                    var formattedTotalPrice = parseFloat(order.totalPrice).toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD"
                    });

                orderTableBody.append(
                    `<tr>
                        <td>${order.orderID}</td>
                        <td>${order.customerID}</td>
                        <td>${order.orderDate}</td>
                        <td>${order.status}</td>
                        <td>${formattedTotalPrice}</td>
                        <td></td>
                    </tr>`
                );
                orderTableBody.find('tr:last td:last').append(actionColumn);
            });
        }
    })
    .always(function () {
        $('#table-spinner').addClass('d-none'); 
    });

}

function createOrder(customerId) {
    var orderData = {
        customerId: customerId,
        orderDate: new Date().toISOString()
    };

    $.ajax({
        type: "POST",
        url: "https://localhost:7072/Order/createOrder",
        contentType: "application/json",
        data: JSON.stringify(orderData),
        success: function (response) {
            if (response.success) {
                alert("Order created successfully.");
                $("#OrderId").val(response.orderId);
                $("#CustomerId").val(response.customerId);
                populateOrderTable(); 
                $('#itemModal').modal('show');
                $('#create-order-spinner').addClass('d-none');
            }
        },
        error: function (error) {
            alert("Failed to create the order.");
            $('#create-order-spinner').addClass('d-none');
        }
    });
}

function fillItemDropdown() {
    $.get("https://localhost:7072/Item/getItems", function (data) {
        if (data.success) {
            var items = data.items;

            var itemDropdown = $("#itemDropdown");

            itemDropdown.empty();

            itemDropdown.append($('<option>', {
                value: "",
                text: "Please select an item"
            }));

            items.forEach(function (item) {
                var option = $('<option>', {
                    value: item.itemID, 
                    text: item.name
                });
                option.data('price', item.price);
                itemDropdown.append(option);
            });
        }
    });
}

// Button to add item to order 

$("#addItemBtn").on("click", function () {
    $('#add-item-order-spinner').removeClass('d-none');
    var selectedItemId = $("#itemDropdown").val();
    var orderId = $("#OrderId").val();
    var quantity = parseInt($("#quantity").val());
    var selectedPrice = parseFloat($("#itemDropdown option:selected").data('price')).toFixed(2);

    $.ajax({
        type: "POST",
        url: "https://localhost:7072/Order/addItemOrder",
        contentType: "application/json",
        data: JSON.stringify({
            orderId: orderId,
            itemId: selectedItemId,
            quantity: quantity,
            price: selectedPrice
        }),
        success: function (response) {
            
            if (response.success) {
                
                alert(response.message);
                populateOrderItemsTable();
            }
            else
            {
                $('#add-item-order-spinner').addClass('d-none');
                alert(response.message);
            }
            
        },
        error: function (error) {
            alert(error);
            $('#add-item-order-spinner').addClass('d-none');
        }
    });
});


// Order Items Table

function populateOrderItemsTable() {
    $('#add-item-order-spinner').removeClass('d-none');
    var orderId = $("#OrderId").val();
    $.get("https://localhost:7072/Order/getOrderItems/"+ orderId)
    .done(function (data) {
        if (data.success) {
            var orderItemTableBody = $("#item-table-body");
            orderItemTableBody.empty();
                $.each(data.orderItems, function (index, orderItem) {

                    var formattedItemPrice = parseFloat(orderItem.price).toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD"
                    });

                    orderItemTableBody.append(
                    `<tr>
                        <td>${orderItem.itemID}</td>
                        <td>Name</td>
                        <td>Item Type</td>
                        <td>${orderItem.quantity}</td>
                        <td>${formattedItemPrice}</td>
                    </tr>`
                );
            });

        }

    })
    .always(function () {
        $('#add-item-order-spinner').addClass('d-none');
    });

}


// this is just to clean the modal wen close 

$("#itemModal").on('hidden.bs.modal', function () {
    $("#OrderId").val('');
    $("#CustomerId").val('');

    $("#OrderItems tbody").empty();
    populateOrderTable();
});

//Confirm Order Button and close modal then update Orders Table
$("#confirmOrderBtn").on("click", function () {
    $('#add-item-order-spinner').removeClass('d-none');
    var orderId = $("#OrderId").val();

    $.ajax({
        type: "POST",
        url: "https://localhost:7072/Order/confirmOrder",
        contentType: "application/json",
        data: JSON.stringify({
            orderId: orderId
        }),
        success: function (response) {
            
            if (response.success) {
                
                alert(response.statusMessage);
                $('#add-item-order-spinner').addClass('d-none');
                $("#itemModal").modal("hide");
            }
            else
            {
                $('#add-item-order-spinner').addClass('d-none');
                alert(response.message);
            }
            
        },
        error: function (error) {
            alert(error);
            $('#add-item-order-spinner').addClass('d-none');
        }
    });
});
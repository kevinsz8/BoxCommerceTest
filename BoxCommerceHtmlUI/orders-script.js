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
    $.get("https://localhost:7072/Order/getOrders/0")
    .done(function (data) {
        if (data.success) {
            var orderTableBody = $("#order-table-body");
            orderTableBody.empty();
                $.each(data.orders, function (index, order) {
                orderTableBody.append(
                    `<tr>
                        <td>${order.orderID}</td>
                        <td>${order.customerID}</td>
                        <td>${order.orderDate}</td>
                        <td>${order.status}</td>
                        <td>${order.totalPrice}</td>
                    </tr>`
                );
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
    var orderId = $("#OrderId").val();
    $.get("https://localhost:7072/Order/getOrderItems/"+ orderId)
    .done(function (data) {
        if (data.success) {
            var orderItemTableBody = $("#item-table-body");
            orderItemTableBody.empty();
                $.each(data.orderItems, function (index, orderItem) {
                    orderItemTableBody.append(
                    `<tr>
                        <td>${orderItem.itemID}</td>
                        <td>Name</td>
                        <td>Item Type</td>
                        <td>${orderItem.quantity}</td>
                        <td>${orderItem.price}</td>
                    </tr>`
                );
            });

        }

    })
    .always(function () {
        $('#add-item-order-spinner').addClass('d-none');
    });

}


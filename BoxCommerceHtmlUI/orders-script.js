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
                itemDropdown.append($('<option>', {
                    value: item.itemID,
                    text: item.name
                }));
            });
        }
    });
}
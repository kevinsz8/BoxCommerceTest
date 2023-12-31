function populateCustomerInformation() {
    var customerId = $("#CustomerId").val();
    $.get("https://localhost:7072/Customer/getCustomerById/"+ customerId, function (data) {
        if (data.success) {
            $("#customerNameCard").text(data.name);
            $("#customerEmailCard").text(data.email);
            $("#customerPhoneCard").text(data.phone);
            $('#table-spinner').addClass('d-none');
        }
    });
}

function populateOrderTable() {
    $('#table-spinner').removeClass('d-none'); 
    var customerId = $("#CustomerId").val();
    $.get("https://localhost:7072/Order/customer/" + customerId)
    .done(function (data) {
        if (data.success) {
            var orderTableBody = $("#order-table-body");
            orderTableBody.empty();
                $.each(data.orders, function (index, order) {
                    // Creating edit button 
                    var editButton = $("<button>")
                    .addClass("btn")
                    .addClass(order.status === "New" ? "btn-primary" : "btn-info")
                    .text(order.status === "New" ? "Edit" : "View")
                    .on("click", function () {
                        
                        $("#OrderId").val(order.orderID);
                        $("#CustomerId").val(order.customerID);
                        $("#OrderStatus").val(order.status);

                        if (order.status !== "New") {
                            $("#itemDropdown").prop("disabled", true);
                            $("#quantity").prop("disabled", true);
                            $("#addItemBtn").prop("disabled", true);
                            $("#confirmOrderBtn").prop("disabled", true);
                        }
                        
                        populateOrderItemsTable(order.orderID);
                        
                        $("#itemModal").modal("show");
                    });

                    var cancelButton = $("<button title='Cancel Order'>")
                    .text("Cancel Order")
                    .addClass("btn btn-danger delete-item")
                    .data("orderId", order.orderID)
                    .prop("hidden", order.status !== "New" && order.status !== "ReadyToPickUp" && order.status !== "Canceled" ? false : true)
                    .on("click", function () {
                        var confirmCancel = confirm("Are you sure you want to cancel this order?");

                        if (confirmCancel) 
                        {
                            CancelOrder(order.orderID);
                        }
                    });
                    

                    var actionColumn = $("<p>").append(editButton);

                    var formattedTotalPrice = parseFloat(order.totalPrice).toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD"
                    });

                    var hiddenItemIdColumn = `<td class="d-none">${order.orderID}</td>`;

                    orderTableBody.append(
                    `<tr>
                        ${hiddenItemIdColumn}
                        <td>${index + 1}</td>
                        <td>${order.customerID}</td>
                        <td>${order.orderDate}</td>
                        <td>${order.status}</td>
                        <td>${formattedTotalPrice}</td>
                        <td></td>
                    </tr>`
                );
                orderTableBody.find('tr:last td:last').append(actionColumn);
                orderTableBody.find('tr:last td:last').append(cancelButton);
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

function CancelOrder(orderId) {
    $('#table-spinner').removeClass('d-none'); 
    var orderData = {
        orderId: orderId
    };

    $.ajax({
        type: "POST",
        url: "https://localhost:7072/Order/cancelOrder",
        contentType: "application/json",
        data: JSON.stringify(orderData),
        success: function (response) {
            if (response.success) {
                alert("Order cancel successfully.");
                $("#OrderId").val(response.orderId);
                $("#CustomerId").val(response.customerId);
                populateOrderTable(); 
                
            }
            else
            {
                alert(response.statusMessage + " " + response.errorMessage);
            }

            $('#table-spinner').addClass('d-none');
        },
        error: function (error) {
            alert("Failed to create the order.");
            $('#table-spinner').addClass('d-none');
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

    if (!selectedItemId) {
        alert("Please select a valid item from the dropdown.");
        $('#add-item-order-spinner').addClass('d-none');
        return false;
    }

    if (isNaN(quantity) || quantity < 1){
        alert("Please enter a valid quantity greater than 0.");
        $('#add-item-order-spinner').addClass('d-none');
        return false;
    }

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
                
                alert(response.message + ' ' + response.statusMessage);
                populateOrderItemsTable();
            }
            else
            {
                $('#add-item-order-spinner').addClass('d-none');
                alert(response.errorMessage);
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
    var orderStatus = $("#OrderStatus").val();
    $.get("https://localhost:7072/Order/getOrderItems/"+ orderId)
    .done(function (data) {
        if (data.success) {
            var orderItemTableBody = $("#item-table-body");
            orderItemTableBody.empty();
                $.each(data.orderItems, function (index, orderItem) {

                    var deleteButton = $("<button title='Delete item'>")
                    .html("<i class='fas fa-times'></i>")
                    .addClass("btn btn-danger delete-item")
                    .data("itemId", orderItem.itemID)
                    .data("quantity", orderItem.quantity);
                    deleteButton.attr("id", "btnDelete"+orderItem.orderID);

                    if (orderStatus !== "New") {
                        deleteButton.attr("disabled", "disabled");
                    }

                    var actionColumn = $("<p>").append(deleteButton);

                    //var subTotal = parseFloat(orderItem.price) * parseFloat(orderItem.quantity);

                    var formattedAmountPrice = parseFloat(orderItem.price * orderItem.quantity).toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD"
                    });

                    var formattedItemPrice = parseFloat(orderItem.price).toLocaleString("en-US", {
                        style: "currency",
                        currency: "USD"
                    });

                    var hiddenItemIdColumn = `<td class="d-none">${orderItem.itemID}</td>`;

                    orderItemTableBody.append(
                    `<tr>
                        ${hiddenItemIdColumn}
                        <td>${index + 1}</td>
                        <td>${orderItem.name}</td>
                        <td>${orderItem.itemType}</td>
                        <td>${orderItem.quantity}</td>
                        <td>${formattedItemPrice}</td>
                        <td>${formattedAmountPrice}</td>
                        <td></td>
                    </tr>`
                );
                orderItemTableBody.find('tr:last td:last').append(actionColumn);
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

// delete items button 

$("#item-table-body").on("click", ".delete-item", function () {
    
    var itemId = $(this).data("itemId");
    var quantity = $(this).data("quantity");
    var orderId = $("#OrderId").val();

    var confirmDelete = confirm("Are you sure you want to delete this item from the order?");

    if (confirmDelete) 
    {
        $('#add-item-order-spinner').removeClass('d-none');
        $.ajax({
            type: "DELETE", 
            url: "https://localhost:7072/Order/" + orderId + "/" + itemId + "/" + quantity, 
            success: function (response) {
                if (response.success) {
                    alert(response.statusMessage);
                    populateOrderItemsTable();
                } else {
                    $('#add-item-order-spinner').addClass('d-none');
                    alert(response.errorMessage);
                }
            },
            error: function (error) {
                alert(error);
                $('#add-item-order-spinner').addClass('d-none');
            }
        });
    }
});




$(document).ready(function () {
const urlParams = new URLSearchParams(window.location.search);
        const customerId = urlParams.get("CustomerId");

        if (!customerId || customerId.trim() === "") {
            window.location.href = "login.html";
        } else {
            $("#CustomerId").val(customerId);
        }

        $('#create-order-spinner').addClass('d-none');
        $('#add-item-order-spinner').addClass('d-none');
        populateCustomerInformation();


        populateOrderTable();

        fillItemDropdown();
        $("#create-order-btn").on("click", function () {
            $('#create-order-spinner').removeClass('d-none');
            var selectedCustomerId = $("#CustomerId").val();
            createOrder(selectedCustomerId);
        });
    });
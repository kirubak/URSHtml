$(document).ready(function () {
    var priceDetails = {
        itemSesame: {
            prodName: 'Sesame',
            qty: 0,
            pricePerProduct: 100,
            price: 0
        },
        itemPeanut: {
            prodName: 'Peanut',
            qty: 0,
            pricePerProduct: 99,
            price: 0
        },
        itemCoconut: {
            prodName: 'Coconut',
            qty: 0,
            pricePerProduct: 88,
            price: 0
        },
        itemCastor: {
            prodName: 'Castor',
            qty: 0,
            pricePerProduct: 11,
            price: 0
        }
    };

    var dataCheckFun = function (e, details) {

        if (details == 'inputQtyChange') {
            var choosedProductQty = (e.target.value != '') ? e.target.value : 0, choosedCalculateProductPrice = (e.target.value * priceDetails[e.target.id].pricePerProduct),
    			totalQty = 0, totalPrice = 0;
            priceDetails[e.target.id].qty = choosedProductQty;
            priceDetails[e.target.id].price = choosedCalculateProductPrice;

            $('#output' + e.target.id + '').text(choosedCalculateProductPrice);
            $('#' + e.target.id + '').val(choosedProductQty);

            Object.keys(priceDetails).forEach(function (items) {
                totalQty += Number(priceDetails[items].qty);
                totalPrice += Number(priceDetails[items].price);
            });

            $('#toatalProdQty').text(totalQty);
            $('#toatalProdPrice').text(totalPrice);

        }

        if (details == 'proceedChange') {
            var itemUncheckCount = 0;
            Object.keys(priceDetails).forEach(function (items) {
                if (Number(priceDetails[items].qty) == 0) {
                    itemUncheckCount++;
                }
            });
            if (itemUncheckCount == Object.keys(priceDetails).length) {
                $("#appPriceErr").remove();
                $(".app-price-table-wrap").append("<p id='appPriceErr' class='app-price-err'>Choose a product from above list</p>");
            } else {

                $('#orderProductsDetails').val(JSON.stringify(priceDetails));
                $('#pickOrderModal').modal('toggle');
                $('#oderDetailsModal').modal('hide');
                $('#pickOrderModal').modal('show');
                $("#appPriceErr").remove();
            }
        }
    };

    $('.productValues').change(function (ev) {
        dataCheckFun(ev, 'inputQtyChange');
    });

    $('#orderProceed').click(function (ev) {
        dataCheckFun('', 'proceedChange');
    });

});
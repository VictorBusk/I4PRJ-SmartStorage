﻿$(document)
    .ready(function () {

        // apply changes if modelstate fails
        if ($('#FromInventoryId').val() !== null) {
            setDropDownLists();
        };

        // add eventlistener for 
        $('#FromInventoryId')
            .on('change', setDropDownLists);

        function setDropDownLists() {
            // ajax get products of inventory
            $.get('/api/Inventories/GetOtherInventories/' + $('#FromInventoryId').val(),
                function (data) {
                    // populate with options
                    var inventoriesWithoutFromInventorySelect = $('#ToInventoryId');

                    // remove selected from select
                    inventoriesWithoutFromInventorySelect.empty();

                    /*if (!data || data.length === 0) {
                        productsOfInventorySelect.append(new Option('Please choose', undefined));
                        return; }*/

                    for (var i = 0; i < data.length; i++) {
                        inventoriesWithoutFromInventorySelect.append(new Option(data[i].Name, data[i].InventoryId));
                    }
                });

            // ajax get products of inventory
            $.get('/api/Products/GetProductsOfInventory/' + $('#FromInventoryId').val(),
                function (data) {
                    // populate with options
                    var productsOfInventorySelect = $('#ProductId');

                    // remove selected from select
                    productsOfInventorySelect.empty();

                    /*if (!data || data.length === 0) {
                        productsOfInventorySelect.append(new Option('Please choose', undefined));
                        return; }*/

                    for (var i = 0; i < data.length; i++) {
                        productsOfInventorySelect.append(new Option(data[i].Name, data[i].ProductId));
                    }
                });
        };
    });
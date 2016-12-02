﻿$(document)
    .ready(function () {
        var table = $("#status")
            .DataTable({
                ajax: {
                    url: "/api/status/GetStatus/" + document.location.pathname.split('/')[3],
                    dataSrc: ""
                },
                dom: '<"html5buttons"B>lTfgitp',
                "buttons": [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            { extend: 'copy' },
                            { extend: 'csv' },
                            { extend: 'excel', title: 'Optællingsrapport' },
                            { extend: 'pdf', title: 'Optællingsrapport' },
                            {
                                extend: 'print',
                                customize: function (win) {
                                    $(win.document.body).addClass('white-bg');
                                    $(win.document.body).css('font-size', '10px');

                                    $(win.document.body)
                                        .find('table')
                                        .addClass('compact')
                                        .css('font-size', 'inherit');
                                }
                            }
                        ]
                    }
                ],
                "columnDefs": [
                    { "width": "20%", "targets": 0 },
                    { "width": "20%", "targets": 1 },
                    { "width": "20%", "targets": 2 },
                    { "width": "20%", "targets": 3 },
                    { "width": "20%", "targets": 4 }
                ],
                columns: [
                    {
                        data: "product.name"
                    },
                    {
                        data: "product.category.name"
                    },
                    {
                        data: "expQuantity"
                    },
                    {
                        data: "curQuantity"
                    },
                    {
                        data: "difference"
                    }
                ]
            });
    });
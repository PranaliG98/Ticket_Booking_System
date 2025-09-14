$(document).ready(function () {
    $("#seatTable").dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/SeatDetails/GetSeatDetails",
            "type": "Post",
            "datatype":"json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
       
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "seatNumber", "name": "SeatNumber", "autoWidth": true },
            { "data": "busNumber", "name": "BusNumber", "autoWidth": true },
            {"data": "seatStatus", "name": "SeatStatus", "autoWidth": true}
        ]

    });
})
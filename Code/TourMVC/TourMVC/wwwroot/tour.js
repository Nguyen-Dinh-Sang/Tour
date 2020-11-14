$("#changeSalary").change(function () {
    var id = $("#changeSalary").val();
    //alert(id);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        headers: {
            "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        contentType: 'application/json; charset=utf-8',
        url: '/GiaTourHienTais/Jquery',
        data: {
            id: id
        },
        
        success: function (result) {
            var html = '<select asp-for="GiaId" name="GiaId" class="form-control">';
            $.each(result, function () {
                var item = this;
                html += '<option value="' + item.giaId + '"> ' + item.giaTuNgay + ' </option>';
            })
            html += '</select>';
            $("#result").html(html);
        }
    });
        
});
function loadResult()
{
    var id = $("#changeSalary").val();
    //alert(id);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        headers: {
            "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        contentType: 'application/json; charset=utf-8',
        url: '/GiaTourHienTais/Jquery',
        data: {
            id: id
        },

        success: function (result) {
            var html = '<select asp-for="GiaId" name="GiaId" class="form-control">';
            $.each(result, function () {
                var item = this;
                html += '<option value="' + item.giaId + '"> ' + item.giaTuNgay + ' </option>';
            })
            html += '</select>';
            $("#result").html(html);
        }
    });
}
$("#changeDoanTourId").change(function () {
    var id = $("#changeDoanTourId").val();
    $.ajax({
        type: 'GET',
        dataType: 'json',
        headers: {
            "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        contentType: 'application/json; charset=utf-8',
        url: '/TourDoans/getGiaTour',
        data: {
            id: id
        },

        success: function (result) {
            var html = "";
            $.each(result, function () {
                var item = this;
                html += item.giaSoTien;
            })
           
            $("#changeDoanGiaTour").val(html);
        }
    });

});
function loadchangeDoanGiaTour() {
    var id = $("#changeDoanTourId").val();
    $.ajax({
        type: 'GET',
        dataType: 'json',
        headers: {
            "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        contentType: 'application/json; charset=utf-8',
        url: '/TourDoans/getGiaTour',
        data: {
            id: id
        },

        success: function (result) {
            var html = "";
            $.each(result, function () {
                var item = this;
                html += item.giaSoTien;
            })

            $("#changeDoanGiaTour").val(html);
        }
    });
}
window.onload = function(){
    loadResult();
    loadchangeDoanGiaTour();
};
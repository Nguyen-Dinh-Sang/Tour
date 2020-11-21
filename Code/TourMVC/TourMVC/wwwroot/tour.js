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
                var spl = item.giaTuNgay.split('T');
                var date = new Date(spl[0]);
                html += '<option value="' + item.giaId + '"> ' + parseInt(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() + ' </option>';
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
                var spl = item.giaTuNgay.split('T');
                var date = new Date(spl[0]);
                html += '<option value="' + item.giaId + '"> ' + parseInt(date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear() + ' </option>';
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
function batnolai() {
    var ngayve = document.getElementById("DoanNgayVe");
    var ngaydi = document.getElementById("DoanNgayDi");
    if (ngaydi.value > ngayve.value) {
        alert("Ngày về không được nhỏ hơn ngày đi.");
        document.getElementById("DoanNgayVe").value = "";
    }

}
function batngaydilai() {
    var ngayve = document.getElementById("DoanNgayVe");
    var ngaydi = document.getElementById("DoanNgayDi");
    if (ngayve.value !="") {
        if (ngaydi.value > ngayve.value) {
            alert("Ngày về không được nhỏ hơn ngày đi.");
            document.getElementById("DoanNgayDi").value = "";
        }
    }

}
function batgiadenngaylai() {
    var ngayve = document.getElementById("GiaDenNgay");
    var ngaydi = document.getElementById("GiaTuNgay");
    if (ngaydi.value > ngayve.value) {
        alert("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
        document.getElementById("GiaDenNgay").value = "";
    }

}
function batgiatungaylai() {
    var ngayve = document.getElementById("GiaDenNgay");
    var ngaydi = document.getElementById("GiaTuNgay");
    if (ngayve.value != "") {
        if (ngaydi.value > ngayve.value) {
            alert("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");
            document.getElementById("GiaTuNgay").value = "";
        }
    }

}
function hienthi_ds_khach_hang() {
  
    var tmp = $("#btn_ds_khach_hang").val();
    if (tmp == "Xem danh sách") {
     
        document.getElementById("ds_khach_hang").style.display = 'block';
        document.getElementById("btn_ds_khach_hang").value = "Ẩn danh sách";
        
    }
    else {
     
        document.getElementById("ds_khach_hang").style.display = 'none';
        document.getElementById("btn_ds_khach_hang").value = "Xem danh sách";
        
    }
}
function hienthi_ds_nhan_vien() {

    var tmp = $("#btn_ds_nhan_vien").val();
    if (tmp == "Xem danh sách") {

        document.getElementById("ds_nhan_vien").style.display = 'block';
        document.getElementById("btn_ds_nhan_vien").value = "Ẩn danh sách";

    }
    else {

        document.getElementById("ds_nhan_vien").style.display = 'none';
        document.getElementById("btn_ds_nhan_vien").value = "Xem danh sách";

    }
}
function hienthi_ds_chi_phi() {

    var tmp = $("#btn_ds_chi_phi").val();
    if (tmp == "Xem danh sách") {

        document.getElementById("ds_chi_phi").style.display = 'block';
        document.getElementById("btn_ds_chi_phi").value = "Ẩn danh sách";

    }
    else {

        document.getElementById("ds_chi_phi").style.display = 'none';
        document.getElementById("btn_ds_chi_phi").value = "Xem danh sách";

    }
}
function hienthi_ds_dia_diem() {

    var tmp = $("#btn_ds_dia_diem").val();
    if (tmp == "Xem danh sách") {

        document.getElementById("ds_dia_diem").style.display = 'block';
        document.getElementById("btn_ds_dia_diem").value = "Ẩn danh sách";

    }
    else {

        document.getElementById("ds_dia_diem").style.display = 'none';
        document.getElementById("btn_ds_dia_diem").value = "Xem danh sách";

    }
}
function hienthi_ds_doan_da_di() {

    var tmp = $("#btn_ds_doan_da_di").val();
    if (tmp == "Xem danh sách") {

        document.getElementById("ds_doan_da_di").style.display = 'block';
        document.getElementById("btn_ds_doan_da_di").value = "Ẩn danh sách";

    }
    else {

        document.getElementById("ds_doan_da_di").style.display = 'none';
        document.getElementById("btn_ds_doan_da_di").value = "Xem danh sách";

    }
}
window.onload = function(){
    loadResult();
    loadchangeDoanGiaTour();
};
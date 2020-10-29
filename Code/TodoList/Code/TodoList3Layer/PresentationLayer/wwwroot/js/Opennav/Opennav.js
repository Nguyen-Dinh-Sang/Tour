
function openNav() {
   
    document.getElementById("mySidenav").style.width = "100%";
    
}

function closeNav() {
        document.getElementById("mySidenav").style.width = "0";
}
function openNavWork() {
    document.getElementById("mySidenavWork").style.width = "100%";
}

function closeNavWork() {
    document.getElementById("mySidenavWork").style.width = "0";
}
function openNavWorkEmployee() {
    document.getElementById("mySidenavWorkEmployee").style.width = "100%";
}

function closeNavWorkEmployee() {
    document.getElementById("mySidenavWorkEmployee").style.width = "0";
}
function openNavWorkListEmployee() {
    document.getElementById("mySidenavWorkListEmployee").style.width = "100%";
}

function closeNavWorkListEmployee() {
    document.getElementById("mySidenavWorkListEmployee").style.width = "0";
}
function btncomment(id_comment,comment) {
    
   /*var id_comment = document.getElementById("edit_comment").value;
   var comment = document.getElementById("hidden_edit_comment").value;*/
    document.getElementById("add_cmt_idcmt").value = id_comment;
    document.getElementById("Add_Comment").value = comment;
    document.getElementById("Add_Comment").focus();
    document.getElementById("close_edit_comment").innerHTML = '<button id="btn_close_edit_comment" onclick="huy_edit_comment()" value="0"> Hủy</button>';
}
function huy_edit_comment() {
    document.getElementById("add_cmt_idcmt").value = "0";
    document.getElementById("Add_Comment").value = "";
    document.getElementById("close_edit_comment").innerHTML = " ";
}
function thaydoingay() {
    var datecreated = document.getElementById("Ed_Editdatecrated").value;
    document.getElementById("Edit_DateCreatedWorkList").value = datecreated;
}
$(document).ready(
    function () {
        $("#datepicker").datepicker({
            dateFormat: 'dd/mm/yy',
            changeMonth: true, //Tùy chọn này cho phép người dùng chọn tháng
            changeYear: true //Tùy chọn này cho phép người dùng lựa chọn từ phạm vi năm
        });
    }
);


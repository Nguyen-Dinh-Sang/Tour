using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Providers.Entities;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages
{
    public class EmployeeModel : PageModel
    {
        public string id = null;
        public string loai = null;
        private string loaisession, idsession = null;
        IEmployeeService employeeService = new EmployeeService();
        IWorkListService workListService = new WorkListService();
        IWorkService workService = new WorkService();
        ICommentService commentService = new CommentService();
        public void OnGet()
        {
            if (Request.QueryString.HasValue == true)
            {
                var get = Request.QueryString.Value.Split('=');
                id = get[1];
                loai = get[0];
                if (loai != null && id != null)
                {
                    HttpContext.Session.SetString("loai", loai + "");
                    HttpContext.Session.SetString("id", id + "");
                    if (loai.Equals("?id"))
                    {
                        HttpContext.Session.SetString("id_employee", id + "");
                    }
                    if (loai.Equals("?danhsachcongviec"))
                    {
                        HttpContext.Session.SetString("id_worklist", id + "");
                    }
                    if (loai.Equals("?congviec"))
                    {
                        HttpContext.Session.SetString("id_work", id + "");
                    }
                }

            }


        }
        public void OnPost()
        {
            if (String.Compare(Request.Form["submit_logout"], "Đăng xuất") == 0)
            {
                logout();
            }
            if (String.Compare(Request.Form["danhsachcongviec_delete"], "Xóa") == 0)
            {
                deleteWorkList();
            }
            if ( String.Compare(Request.Form["Edit_WorkList"], "Sửa danh sách công việc") == 0)
            {
                edit_WorkList();
            }
            if ( String.Compare(Request.Form["Add_WorkList"], "Thêm danh sách") == 0)
            {
                add_WorkList();
            }
            if (String.Compare(Request.Form["submit_add_1_employee"], "Thêm") == 0)
            {
                add_1_Employee();
            }
            if (String.Compare(Request.Form["submit_delete_1_employee"], "Xóa") == 0)
            {
                delete_1_Employee();
            }
            if (String.Compare(Request.Form["submit_Delete_work_employee"], "Xóa") == 0)
            {
                delete_1_EmployeeWork();
            }
            if ( String.Compare(Request.Form["Add_Work"], "Thêm công việc") == 0)
            {
                add_work();
            }
            if ( String.Compare(Request.Form["congviec_delete"], "Xóa công việc") == 0)
            {
                deleteWork();
            }
            if ( String.Compare(Request.Form["Edit_Work"], "Sửa công việc") == 0)
            {
                edit_work();
            }
            if ( String.Compare(Request.Form["submit_add_work_employee"], "Thêm") == 0)
            {
                add_1_EmployeeWork();
            }
            if ( String.Compare(Request.Form["submit_comment"], "Gửi") == 0)
            {
                add_Comment();
            }
            
                if ( String.Compare(Request.Form["delete_comment"], "Xóa") == 0)
            {
                delete_Comment();
            }
            if (String.Compare(Request.Form["submit_editstatus"], "Cần làm") == 0)
            {
                edit_StatusWork();

            }

            if (String.Compare(Request.Form["submit_editstatus"], "Đang làm") == 0)
            {
                Console.WriteLine("chay toi day 2");
                edit_StatusWork();

            }
            if (String.Compare(Request.Form["submit_editstatus"], "Đã làm") == 0)
            {
                Console.WriteLine("chay toi day 3");
                edit_StatusWork();

            }
            if (String.Compare(Request.Form["submit_editstatus"], "Trễ hạn") == 0)
            {
                Console.WriteLine("chay toi day 4");
                edit_StatusWork();

            }
        }
        public void logout()
        {
            HttpContext.Session.SetString("fullname", "");
            HttpContext.Session.SetString("idrole", "");
            HttpContext.Session.SetString("idemployee", "");
            Response.Redirect("/login");
        }
        public void delete_Comment()
        {
            
                int id_comment = int.Parse(Request.Form["delete_cmt_idcmt"]);
            commentService.remove(id_comment);
            Response.Redirect("/employee?congviec=" + getid_work());
        }
        public void add_Comment()
        {
            CommentDTO commentDTO = new CommentDTO();
            commentDTO.IdEmployee = int.Parse(Request.Form["id_cmt_employee"]);
            commentDTO.IdWork = int.Parse(Request.Form["add_cmt_idwork"]);
            commentDTO.Id = int.Parse(Request.Form["add_cmt_idcmt"]);
            commentDTO.CommentContent = Request.Form["Add_Comment"];
            if (commentDTO.CommentContent != "")
            {
                commentService.save(commentDTO);
            }    
            
            Response.Redirect("/employee?congviec=" + commentDTO.IdWork);
        }
        public void edit_StatusWork()
        {
           
            int IdWorkStatus = int.Parse(Request.Form["editstatus_id"]);
            int IdWork = int.Parse(Request.Form["editstatus_idwork"]);
            int id_employee = int.Parse(getid_employee());
            workService.editStatus(IdWork,IdWorkStatus);
            Response.Redirect("/employee?congviec=" + IdWork);
            
        }
       
        public void add_work()
        {
            WorkDTO workDTO = new WorkDTO();
            workDTO.Id = 0;
            workDTO.IdWorkList = int.Parse(Request.Form["addwork_id_worklist"]);
            workDTO.WorkName = Request.Form["Add_WorkName"];
            workDTO.WorkContent = Request.Form["Add_WorkContent"];
            workDTO.StartDate = DateTime.Parse(Request.Form["Add_WorkStartDate"]);
            workDTO.EndDate = DateTime.Parse(Request.Form["Add_WorkEndDate"]);
            workDTO.IdWorkStatus = 1;
            int id_employee = int.Parse(Request.Form["addwork_id_employee"]);
            workService.save(workDTO, id_employee);
            Response.Redirect("/employee?danhsachcongviec=" + workDTO.IdWorkList);
        }
        public void edit_work()
        {
            WorkDTO workDTO = new WorkDTO();
            workDTO.Id = int.Parse(Request.Form["editwork_id_work"]);
            workDTO.IdWorkList = int.Parse(Request.Form["id_Edit_WorkList"]);
            workDTO.WorkName = Request.Form["Edit_WorkName"];
            workDTO.WorkContent = Request.Form["Edit_WorkContent"];
            workDTO.StartDate = DateTime.Parse(Request.Form["Edit_WorkStartDate"]);
            workDTO.EndDate = DateTime.Parse(Request.Form["Edit_WorkEndDate"]);
            workDTO.IdWorkStatus = int.Parse(Request.Form["id_Edit_WorkStatus"]);
            int id_employee = int.Parse(Request.Form["editwork_id_employee"]);
            workService.save(workDTO, id_employee);
            Response.Redirect("/employee?congviec=" + workDTO.Id);
        }
        public void edit_WorkList()
        {
            WorkListDTO workListDTO = new WorkListDTO();
            workListDTO.WorkListName = Request.Form["Edit_WorkListName"];
            workListDTO.IdWorkListStatus = int.Parse(Request.Form["Edit_WorkListStatus"]);
       
            workListDTO.Id = int.Parse(Request.Form["edit_id_worklist"]);
            int edit_id_employee = int.Parse(Request.Form["edit_id_employee"]);
            workListService.save(workListDTO, edit_id_employee);
            Response.Redirect("/employee?id=" + edit_id_employee);
        }
        public void add_WorkList()
        {
            WorkListDTO workListDTO = new WorkListDTO();
            workListDTO.WorkListName = Request.Form["Add_WorkListName"];
            workListDTO.IdWorkListStatus = int.Parse(Request.Form["Add_WorkListStatus"]);
            workListDTO.Id = 0;
            int add_id_employee = int.Parse(Request.Form["add_id_employee"]);
            workListService.save(workListDTO, add_id_employee);
            Response.Redirect("/employee?id=" + add_id_employee);
        }
        public void add_1_Employee()
        {
            int id_add_1_employee = int.Parse(Request.Form["id_add_1_employee"]);
            int id_add_1_worklist = int.Parse(Request.Form["id_add_1_worklist"]);
            workListService.addEmployee(id_add_1_worklist, id_add_1_employee);
            Response.Redirect("/employee?danhsachcongviec=" + id_add_1_worklist);
        }
        public void add_1_EmployeeWork()
        {
            int id_add_1_employee = int.Parse(Request.Form["id_add_work_employee"]);
            int id_add_1_work = int.Parse(Request.Form["id_add_work"]);
            workService.addEmployee(id_add_1_employee, id_add_1_work);
            Response.Redirect("/employee?congviec=" + id_add_1_work);
        }
        public void delete_1_EmployeeWork()
        {
            int id_delete_1_employee = int.Parse(Request.Form["id_delete_work_employee"]);
            int id_delete_1_work = int.Parse(Request.Form["id_delete_work"]);
            workService.removeEmployee(id_delete_1_employee, id_delete_1_work);
            Response.Redirect("/employee?congviec=" + id_delete_1_work);
        }
        public void delete_1_Employee()
        {
            int id_delete_1_employee = int.Parse(Request.Form["id_delete_1_employee"]);
            int id_delete_1_worklist = int.Parse(Request.Form["id_delete_1_worklist"]);
            workListService.removeEmployee(id_delete_1_employee,id_delete_1_worklist);
            Response.Redirect("/employee?danhsachcongviec=" + id_delete_1_worklist);
        }
        public void deleteWorkList()
        {
            workListService.remove(int.Parse(Request.Form["id_delete"]));
            Response.Redirect("/employee" + HttpContext.Session.GetString("loai") + "=" + HttpContext.Session.GetString("id"));
        }
        public void deleteWork()
        {
            workService.remove(int.Parse(Request.Form["id_delete_work"]));
            Response.Redirect("/employee?danhsachcongviec=" + getid_worklist());
        }
        public string getSessionLoai()
        {
            return HttpContext.Session.GetString("loai");
        }
        public string getSessionId()
        {
            return HttpContext.Session.GetString("id");
        }
        public string getid_employee()
        {
            return HttpContext.Session.GetString("id_employee");
        }
        public string getid_worklist()
        {
            return HttpContext.Session.GetString("id_worklist");
        }
        public string getid_work()
        {
            return HttpContext.Session.GetString("id_work");
        }
        public string getSessionFullname()
        {if(HttpContext.Session.GetString("fullname") != "")
            {
                return HttpContext.Session.GetString("fullname");
            }
            else { return ""; }
            
        }
        public string getSessionIdrole()
        {
            return HttpContext.Session.GetString("idrole");
        }
        public string getSessionIdEmployee()
        {
            return HttpContext.Session.GetString("idemployee");
        }
        public IEnumerable<WorkListDTO> getAllByIdEmployee()
        {
            return workListService.getAllByIdEmployee(int.Parse(HttpContext.Session.GetString("id")));
        }
        public IEnumerable<WorkListDTO> getPublicEmployee()
        {
            return workListService.getPublicByIdEmployee(int.Parse(HttpContext.Session.GetString("id")));
        }
        public IEnumerable<EmployeeDTO> getAllEmployee()
        {
            return employeeService.getAll();
        }
        public IEnumerable<WorkDTO> getAllByIdWorkList(string id)
        {
            return workService.getAllByIdWorkList(int.Parse(id));
        }
        public IEnumerable<EmployeeDTO> getNotInWorkList(string id)
        {
            return employeeService.getNotInWorkList(int.Parse(id));
        }
        public IEnumerable<EmployeeDTO> getNotInWork(string id)
        {
            return employeeService.getNotInWork(int.Parse(id));
        }
        public WorkListDTO getById(int id)
        {
            return workListService.getById(id);
        }
        public WorkDTO getWorkById(int id)
        {
            return workService.getById(id);
        }
        public IEnumerable<EmployeeDTO> getEmployeeByIdWorkList(string id)
        {
            return employeeService.getByIdWorkList(int.Parse(id));
        }
        public IEnumerable<EmployeeDTO> getEmployeeByIdWork(string id)
        {
            return employeeService.getByIdWork(int.Parse(id));
        }

        public IEnumerable<CommentDTO> getAllCommentByIdWork(string id)
        {
            return commentService.getAllByIdWork(int.Parse(id));
        }
        public EmployeeDTO getEmployeeById(string id)
        {
            return employeeService.getById(int.Parse(id));
        }
    }
}

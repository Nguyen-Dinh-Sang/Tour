using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages
{
    public class LoginModel : PageModel
    {
        IEmployeeService employeeService = new EmployeeService();
        string login;
        public void OnGet()
        {
        }
        public void OnPost()
        {   
            if(Request.Form["login_submit"].Equals("Sign In"))
            {
                signIn();
               
            }
            else
            {
                signUp();
            }
           
        }

        

        public void signIn()
        {

            var user = Request.Form["login_username"];
            var pass = Request.Form["login_password"];
            var employee = employeeService.login(user, pass);
            if (employee != null)
            {
                HttpContext.Session.SetString("fullname", employee.FullName + "");
                HttpContext.Session.SetString("idrole", employee.IdRole + "");
                HttpContext.Session.SetString("idemployee", employee.Id + "");
                if (employee.IdRole == 1)
                {
                    Response.Redirect("/employee?id="+employee.Id);
                }
                else
                {
                    if (employee.IdRole == 2)
                    {
                        
                        Response.Redirect("/employee?id=" + employee.Id);
                    }
                    else
                    {

                    }
                }
            }

        }
        public void signUp()
        {
            if (Request.Form["registration_submit"].Equals("Sign Up") && Request.Form["registration_password"].Equals(Request.Form["registration_passwordRepeat"]))
            {
                EmployeeDTO employeeDTO = new EmployeeDTO();
                employeeDTO.Id = 0;
                employeeDTO.Email = Request.Form["registration_email"];
                employeeDTO.FullName = Request.Form["registration_fullname"];
                employeeDTO.Password = Request.Form["registration_password"];
                if (employeeService.checkEmailExists(employeeDTO.Email) == true)
                {
                    Console.WriteLine("Email đã tồn tại");
                }
                else
                {if (!string.IsNullOrEmpty(employeeDTO.Email) && !string.IsNullOrEmpty(employeeDTO.FullName) && !string.IsNullOrEmpty(employeeDTO.Password))
                    { employeeService.save(employeeDTO);
                     
                    }

                }


            }
        }
      
    }
    
}
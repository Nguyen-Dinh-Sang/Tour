using DataAccessLayer.Context;
using DataAccessLayer.Entitys;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace DataAccessLayer.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private TodoListDBContext todoListDBContext = TodoListDBContext.getInstance();

        public Work addEmployee(int idEmployee, int idWork)
        {
            WorkEmployee workEmployee = new WorkEmployee();
            workEmployee.IdEmployee = idEmployee;
            workEmployee.IdWork = idWork;
            todoListDBContext.WorkEmployee.Add(workEmployee);
            todoListDBContext.SaveChanges();
            var result = todoListDBContext.Work.Find(idWork);
            var workList = todoListDBContext.WorkList.Find(result.IdWorkList);


            var workListEmployeeExist = from wle in todoListDBContext.WorkListEmployee
                                        where wle.IdWorkList == result.IdWorkList && wle.IdEmployee == idEmployee
                                        select wle;
            if (workListEmployeeExist.Count() > 0)
            {
                return result;
            }

            WorkListEmployee workListEmployee = new WorkListEmployee();
            workListEmployee.IdWorkList = workList.Id;
            workListEmployee.IdEmployee = idEmployee;

            todoListDBContext.WorkListEmployee.Add(workListEmployee);
            todoListDBContext.SaveChanges();
            return result;
        }

        public Work editStatus(int idWork, int idStatus)
        {
            var result = todoListDBContext.Work.Find(idWork);
            result.IdWorkStatus = idStatus;
            todoListDBContext.SaveChanges();
            return todoListDBContext.Work.Find(idWork);
        }

        public IEnumerable<Work> getAllByIdWorkList(int id)
        {
            var result = from w in todoListDBContext.Work
                         where w.IdWorkList == id
                         select w;
            return result;
        }

        public Work getById(int id)
        {
            return todoListDBContext.Work.Find(id);
        }

        public bool remove(int id)
        {
            var results = todoListDBContext.Work.Find(id);
            todoListDBContext.Work.Remove(results);
            todoListDBContext.SaveChanges();
            if (todoListDBContext.Work.Find(id) == null)
            {
                return true;
            }
            return false;
        }

        public bool removeEmployee(int idEmployee, int idWork)
        {
            var result = from we in todoListDBContext.WorkEmployee
                         where we.IdEmployee == idEmployee && we.IdWork == idWork
                         select we;
            todoListDBContext.WorkEmployee.Remove(result.First());
            todoListDBContext.SaveChanges();
            /* if (todoListDBContext.WorkEmployee.Find(result.First().Id) == null)
             {
                 return true;
             }
             return false;*/
            return true;
        }

        public Work save(Work work, int idEmployee)
        {
            if (work.Id == 0)
            {
                var result = todoListDBContext.Work.Add(work);
                todoListDBContext.SaveChanges();
                WorkEmployee workEmployee = new WorkEmployee();
                workEmployee.IdEmployee = idEmployee;
                workEmployee.IdWork = result.Entity.Id;
                todoListDBContext.WorkEmployee.Add(workEmployee);
                todoListDBContext.SaveChanges();
                return result.Entity;
            } else
            {
                var result = todoListDBContext.Work.Find(work.Id);
                result.IdWorkStatus = work.IdWorkStatus;
                result.WorkContent = work.WorkContent;
                result.WorkName = work.WorkName;
                result.StartDate = work.StartDate;
                result.EndDate = work.EndDate;
                todoListDBContext.SaveChanges();
                return todoListDBContext.Work.Find(work.Id);
            }
        }
    }
}

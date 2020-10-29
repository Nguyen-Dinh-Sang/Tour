using DataAccessLayer.Context;
using DataAccessLayer.Entitys;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace DataAccessLayer.Repositories
{
    public class WorkListRepository : IWorkListRepository
    {
        private TodoListDBContext todoListDBContext = TodoListDBContext.getInstance();

        public WorkList addEmployee(int idWorkList, int idEmployee)
        {
            WorkListEmployee workListEmployee = new WorkListEmployee();
            workListEmployee.IdWorkList = idWorkList;
            workListEmployee.IdEmployee = idEmployee;
            var result = todoListDBContext.WorkListEmployee.Add(workListEmployee);
            todoListDBContext.SaveChanges();
            return todoListDBContext.WorkList.Find(idWorkList);
        }

        public IEnumerable<WorkList> getAllByIdEmployee(int id)
        {
            var result = from wl in todoListDBContext.WorkList
                         join wle in todoListDBContext.WorkListEmployee on wl.Id equals wle.IdWorkList
                         where wle.IdEmployee == id
                         select wl;
            return result;
        }

        public WorkList getById(int id)
        {
            return todoListDBContext.WorkList.Find(id);
        }

        public IEnumerable<WorkList> getPublicByIdEmployee(int id)
        {
            var result = from wl in todoListDBContext.WorkList
                         join wle in todoListDBContext.WorkListEmployee on wl.Id equals wle.IdWorkList
                         where wle.IdEmployee == id && wl.IdWorkListStatus == 1
                         select wl;
            return result;
        }

        public bool remove(int id)
        {
            var results = todoListDBContext.WorkList.Find(id);
            todoListDBContext.WorkList.Remove(results);
            todoListDBContext.SaveChanges();
            if (todoListDBContext.WorkList.Find(id) == null)
            {
                return true;
            }
            return false;
        }

        public bool removeEmployee(int idEmployee, int idworkList)
        {
            var result = from wle in todoListDBContext.WorkListEmployee
                                        where wle.IdEmployee == idEmployee && wle.IdWorkList == idworkList
                                        select wle;
            todoListDBContext.WorkListEmployee.Remove(result.First());
            todoListDBContext.SaveChanges();

            var workEmployees = from w in todoListDBContext.Work
                         join we in todoListDBContext.WorkEmployee on w.Id equals we.IdWork
                         where we.IdEmployee == idEmployee && w.IdWorkList == idworkList
                         select we;

            foreach (var workEmployee in workEmployees)
            {
                todoListDBContext.WorkEmployee.Remove(workEmployee);
                
            }
            todoListDBContext.SaveChanges();
            /*if (todoListDBContext.WorkListEmployee.Find(result.First().Id) == null)
            {
                return true;
            }
            return false;*/
            return true;
        }

        public WorkList save(WorkList workList, int idEmployee)
        {
            if (workList.Id == 0)
            {
                var result = todoListDBContext.WorkList.Add(workList);
                todoListDBContext.SaveChanges();
                WorkListEmployee workListEmployee = new WorkListEmployee();
                workListEmployee.IdEmployee = idEmployee;
                workListEmployee.IdWorkList = result.Entity.Id;
                todoListDBContext.WorkListEmployee.Add(workListEmployee);
                todoListDBContext.SaveChanges();
                return result.Entity;
            }
            else
            {
                WorkList findResult = todoListDBContext.WorkList.Find(workList.Id);
                findResult.WorkListName = workList.WorkListName;
                findResult.IdWorkListStatus = workList.IdWorkListStatus;
                todoListDBContext.SaveChanges();
                return todoListDBContext.WorkList.Find(workList.Id);
            }
        }
    }
}

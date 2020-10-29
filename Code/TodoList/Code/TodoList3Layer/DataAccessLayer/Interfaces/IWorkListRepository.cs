using DataAccessLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IWorkListRepository
    {
        IEnumerable<WorkList> getAllByIdEmployee(int id);

        IEnumerable<WorkList> getPublicByIdEmployee(int id);

        WorkList save(WorkList workList, int idEmployee);

        WorkList addEmployee(int idWorkList, int idEmployee);

        bool remove(int id);

        WorkList getById(int id);

        bool removeEmployee(int idEmployee, int idworkList);
    }
}

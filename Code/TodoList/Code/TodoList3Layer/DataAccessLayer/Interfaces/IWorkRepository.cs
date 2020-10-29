using DataAccessLayer.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IWorkRepository
    {
        bool remove(int id);

        Work save(Work work, int idEmployee);

        Work getById(int id);

        IEnumerable<Work> getAllByIdWorkList(int id);

        Work addEmployee(int idEmployee, int idWork);

        Work editStatus(int idWork, int idStatus);

        bool removeEmployee(int idEmployee, int idWork);
    }
}

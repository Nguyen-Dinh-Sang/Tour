using BusinessLogicLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWorkService
    {
        bool remove(int id);

        WorkDTO save(WorkDTO work, int idEmployee);

        WorkDTO getById(int id);

        IEnumerable<WorkDTO> getAllByIdWorkList(int id);

        WorkDTO addEmployee(int idEmployee, int idWork);

        WorkDTO editStatus(int idWork, int idStatus);

        bool removeEmployee(int idEmployee, int idWork);
    }
}

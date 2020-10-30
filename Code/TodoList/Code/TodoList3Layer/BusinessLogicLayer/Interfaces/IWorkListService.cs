using BusinessLogicLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IWorkListService
    {
        IEnumerable<WorkListDTO> getAllByIdEmployee(int id);

        IEnumerable<WorkListDTO> getPublicByIdEmployee(int id);

        WorkListDTO save(WorkListDTO workList, int idEmployee);

        WorkListDTO addEmployee(int idWorkList, int idEmployee);

        bool remove(int id);

        WorkListDTO getById(int id);

        bool removeEmployee(int idEmployee, int idworkList);
    }
}

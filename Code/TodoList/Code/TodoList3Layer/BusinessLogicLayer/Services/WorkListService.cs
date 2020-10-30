using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mapping;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Entitys;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public class WorkListService : IWorkListService
    {
        private IWorkListRepository workListRepository = new WorkListRepository();
        private IMapper mapper = new MappingConfig().config();
        public WorkListDTO addEmployee(int idWorkList, int idEmployee)
        {
            return mapper.Map<WorkList, WorkListDTO>(workListRepository.addEmployee(idWorkList, idEmployee));
        }

        public IEnumerable<WorkListDTO> getAllByIdEmployee(int id)
        {
            return mapper.Map<IEnumerable<WorkList>, IEnumerable<WorkListDTO>>(workListRepository.getAllByIdEmployee(id));
        }

        public WorkListDTO getById(int id)
        {
            return mapper.Map<WorkList, WorkListDTO>(workListRepository.getById(id));
        }

        public IEnumerable<WorkListDTO> getPublicByIdEmployee(int id)
        {
            return mapper.Map<IEnumerable<WorkList>, IEnumerable<WorkListDTO>>(workListRepository.getPublicByIdEmployee(id));
        }

        public bool remove(int id)
        {
            return workListRepository.remove(id);
        }

        public bool removeEmployee(int idEmployee, int idworkList)
        {
            return workListRepository.removeEmployee(idEmployee, idworkList);
        }

        public WorkListDTO save(WorkListDTO workList, int idEmployee)
        {
            return mapper.Map<WorkList, WorkListDTO>(workListRepository.save(mapper.Map<WorkListDTO, WorkList>(workList), idEmployee));
        }
    }
}

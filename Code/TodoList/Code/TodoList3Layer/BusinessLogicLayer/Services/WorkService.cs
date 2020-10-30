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
    public class WorkService : IWorkService
    {
        private IWorkRepository workRepository = new WorkRepository();
        private IMapper mapper = new MappingConfig().config();

        public WorkDTO addEmployee(int idEmployee, int idWork)
        {
            return mapper.Map<Work, WorkDTO>(workRepository.addEmployee(idEmployee, idWork));
        }

        public WorkDTO editStatus(int idWork, int idStatus)
        {
            return mapper.Map<Work, WorkDTO>(workRepository.editStatus(idWork, idStatus));
        }

        public IEnumerable<WorkDTO> getAllByIdWorkList(int id)
        {
            return mapper.Map<IEnumerable<Work>, IEnumerable<WorkDTO>>(workRepository.getAllByIdWorkList(id));
        }

        public WorkDTO getById(int id)
        {
            return mapper.Map<Work, WorkDTO>(workRepository.getById(id));
        }

        public bool remove(int id)
        {
            return workRepository.remove(id);
        }

        public bool removeEmployee(int idEmployee, int idWork)
        {
            return workRepository.removeEmployee(idEmployee, idWork);
        }

        public WorkDTO save(WorkDTO work, int idEmployee)
        {
            return mapper.Map<Work, WorkDTO>(workRepository.save(mapper.Map<WorkDTO, Work>(work), idEmployee));
        }
    }
}

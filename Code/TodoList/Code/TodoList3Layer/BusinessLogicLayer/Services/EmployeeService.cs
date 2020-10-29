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
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository employeeRepository = new EmployeeRepository();
        private IMapper mapper = new MappingConfig().config();

        public bool checkEmailExists(string email)
        {
            return employeeRepository.checkEmailExists(email);
        }

        public IEnumerable<EmployeeDTO> getAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employeeRepository.getAll());
        }

        public EmployeeDTO getById(int id)
        {
            return mapper.Map<Employee, EmployeeDTO>(employeeRepository.getById(id));
        }

        public IEnumerable<EmployeeDTO> getByIdWork(int id)
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employeeRepository.getByIdWork(id));
        }

        public IEnumerable<EmployeeDTO> getByIdWorkList(int id)
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employeeRepository.getByIdWorkList(id));
        }

        public IEnumerable<EmployeeDTO> getNotInWork(int id)
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employeeRepository.getNotInWork(id));
        }

        public IEnumerable<EmployeeDTO> getNotInWorkList(int id)
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(employeeRepository.getNotInWorkList(id));
        }

        public EmployeeDTO login(string email, string password)
        {
            return mapper.Map<Employee, EmployeeDTO>(employeeRepository.login(email, password));
        }

        public EmployeeDTO save(EmployeeDTO employee)
        {
            return mapper.Map<Employee, EmployeeDTO>(employeeRepository.save(mapper.Map<EmployeeDTO, Employee>(employee)));
        }
    }
}

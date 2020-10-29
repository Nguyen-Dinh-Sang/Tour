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
    public class CommentService : ICommentService
    {
        private ICommentRepository commentRepository = new CommentRepository();
        private IMapper mapper = new MappingConfig().config();
        public IEnumerable<CommentDTO> getAllByIdWork(int id)
        {
            return mapper.Map<IEnumerable<Comment>, IEnumerable<CommentDTO>>(commentRepository.getAllByIdWork(id));
        }

        public CommentDTO getById(int id)
        {
            return mapper.Map<Comment, CommentDTO>(commentRepository.getById(id));
        }

        public bool remove(int id)
        {
            return commentRepository.remove(id);
        }

        public CommentDTO save(CommentDTO comment)
        {
            return mapper.Map<Comment, CommentDTO>(commentRepository.save(mapper.Map<CommentDTO, Comment>(comment)));
        }
    }
}

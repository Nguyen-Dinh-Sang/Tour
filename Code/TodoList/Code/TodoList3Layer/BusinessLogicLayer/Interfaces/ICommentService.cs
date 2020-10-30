using BusinessLogicLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICommentService
    {
        bool remove(int id);

        CommentDTO save(CommentDTO comment);

        CommentDTO getById(int id);

        IEnumerable<CommentDTO> getAllByIdWork(int id);
    }
}

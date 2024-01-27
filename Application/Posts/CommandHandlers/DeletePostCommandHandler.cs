using Application.Abstractions;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.CommandHandlers
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePost,Unit>
    {
        public readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        public async Task<Unit> Handle(DeletePost request, CancellationToken cancellationToken)
        {
             await _postRepository.DeletePost(request.Id);
            return Unit.Value;
        }
    }
}

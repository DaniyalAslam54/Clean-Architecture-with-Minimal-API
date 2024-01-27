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
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePost, Post>
    {
        public readonly IPostRepository _postRepository;

        public UpdatePostCommandHandler(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            return await _postRepository.UpdatePost(request.PostContent,request.Id);
        }
    }
}

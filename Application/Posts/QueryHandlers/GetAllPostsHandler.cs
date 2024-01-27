using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.QueryHandlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPosts, IReadOnlyCollection<Post>>
    {
        public readonly IPostRepository _postRepository;

        public GetAllPostsHandler(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }
        public async Task<IReadOnlyCollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            return await _postRepository.GetAllPosts();
        }
    }
}

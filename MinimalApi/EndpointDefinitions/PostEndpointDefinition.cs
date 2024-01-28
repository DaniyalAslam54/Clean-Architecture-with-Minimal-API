using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;
using MinimalApi.Filters;

namespace MinimalApi.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var posts = app.MapGroup("api/posts");
                

            posts.MapGet("/{id:int}", GetPostById).WithName("GetPostById");

            posts.MapPost("/", CreatePost)
                .AddEndpointFilter<PostValidationFilter>();

            posts.MapGet("/", GetPosts).WithName("GetPosts");

            posts.MapPut("/{id:int}", UpdatePost).WithName("UpdatePost")
                .AddEndpointFilter<PostValidationFilter>();

            posts.MapDelete("/{id:int}", DeletePost).WithName("DeletePost");

        }

        private async Task<IResult> GetPostById(int id, IMediator mediator)
        {
            var getPost = new GetPostById() { PostId = id };
            var post = await mediator.Send(getPost);
            return TypedResults.Ok(post);
        }

        private async Task<IResult> CreatePost(Post post, IMediator mediator)
        {
            var createPost = new CreatePost() { PostContent = post.Content };
            var createdPost = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostById", new
            {
                createdPost.Id
            }, createdPost);
        }

        private async Task<IResult> GetPosts(IMediator mediator)
        {
            var getPosts = new GetAllPosts();
            var post = await mediator.Send(getPosts);
            return TypedResults.Ok(post);
        }

        private async Task<IResult> UpdatePost(int id, Post post, IMediator mediator)
        {
            var updatePost = new UpdatePost() { Id = id, PostContent = post.Content };
            var updatedPost = await mediator.Send(updatePost);
            return TypedResults.Ok(updatedPost);
        }

        private async Task<IResult> DeletePost(int id, IMediator mediator)
        {
            var deletePost = new DeletePost() { Id = id };
            var deletedPost = await mediator.Send(deletePost);
            return TypedResults.NoContent();
        }
    }
}

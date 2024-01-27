using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class PostEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {

            app.MapGet("api/post/{id:int}", async (int id, IMediator mediator) =>
            {
                var getPost = new GetPostById() { PostId = id };
                var post = await mediator.Send(getPost);
                return Results.Ok(post);
            }).WithName("GetPostById");

            app.MapPost("api/posts", async (Post post, IMediator mediator) =>
            {
                var createPost = new CreatePost() { PostContent = post.Content };
                var createdPost = await mediator.Send(createPost);
                return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
            });

            app.MapGet("api/posts", async (IMediator mediator) =>
            {
                var getPosts = new GetAllPosts();
                var post = await mediator.Send(getPosts);
                return Results.Ok(post);
            }).WithName("GetPosts");

            app.MapPut("api/posts/{id:int}", async (int id, Post post, IMediator mediator) =>
            {
                var updatePost = new UpdatePost() { Id = id, PostContent = post.Content };
                var updatedPost = await mediator.Send(updatePost);
                return Results.Ok(updatedPost);
            }).WithName("UpdatePost");

            app.MapDelete("api/post/{id:int}", async (int id, IMediator mediator) =>
            {
                var deletePost = new DeletePost() { Id = id };
                var deletedPost = await mediator.Send(deletePost);
                return Results.NoContent();
            }).WithName("DeletePost");

        }
    }
}

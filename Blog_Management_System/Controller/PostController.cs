using BLL.DTOs;
using BLL.Services;
using Blog_Management_System.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog_Management_System.Controller
{
    public class PostController : ApiController
    {
        [HttpGet]
        [Route("api/posts")]
        public HttpResponseMessage Get()
        {
            try
            {
                var data = PostService.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/posts/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = PostService.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Logged]
        [HttpGet]
        [Route("api/posts/{id}/comments")]
        public HttpResponseMessage GetPostWithComments(int id)
        {
            try
            {
                var data = PostService.GetWithComments(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Create Blog
        
        [HttpPost]
        [Route("api/posts")]
        public HttpResponseMessage Create(PostDTO post)
        {
            try
            {
                var service = new PostService();
                var result = service.Create(post);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.Created, "Blog created successfully.");
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create blog.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Update Blog
        
        [HttpPut]
        [Route("api/posts")]
        public HttpResponseMessage Update(PostDTO post)
        {
            try
            {
                var service = new PostService();
                var result = service.Update(post);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Blog updated successfully.");
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to update blog.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Delete Blog
        
        [HttpDelete]
        [Route("api/posts/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var service = new PostService();
                var result = service.Delete(id);
                if (result)
                    return Request.CreateResponse(HttpStatusCode.OK, "Blog deleted successfully.");
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to delete blog.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Count Comment

        [HttpGet]
        [Route("api/comments/commenter/{commentedBy}/count")]
        public HttpResponseMessage GetCommentCountByUser(string commentedBy)
        {
            try
            {
                int count = PostService.GetCommentCountByUser(commentedBy);
                return Request.CreateResponse(HttpStatusCode.OK, new 
                { 
                  User_name = commentedBy,
                  TotalCommentCount = count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Warehouse.Entities;
using Warehouse.Models.Forum;
using Warehouse.Services.Interface;

namespace Warehouse.Controllers
{
    public class ForumController : Controller
    {
        readonly IForumArticleService _forumArticleService;
        readonly IForumCommentService _forumCommentService;
        readonly IForumArticleLikeService _forumArticleLikeService;
        readonly IForumArticleDisLikeService _forumArticleDisLikeService;

        public ForumController(IForumArticleService forumArticleService, IForumCommentService forumCommentService, IForumArticleLikeService forumArticleLikeService, IForumArticleDisLikeService forumArticleDisLikeService)
        {
            _forumArticleService = forumArticleService;
            _forumCommentService = forumCommentService;
            _forumArticleLikeService = forumArticleLikeService;
            _forumArticleDisLikeService = forumArticleDisLikeService;
        }

        public ActionResult Index()
        {
            ViewBag.ActiveForum = "active";
            ViewBag.ForumArticles = _forumArticleService.GetAll();
            return View();
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ActiveForum = "active";
            ViewBag.Article = _forumArticleService.GetById(id, true);
            ViewBag.ListComment = _forumCommentService.GetByArticleId(id, true);
            return View();
        }

        public ActionResult Like(int id)
        {
            ViewBag.ActiveForum = "active";
            bool Liked = _forumArticleLikeService.CheckLike(id, User.Identity.Name);
            if (Liked == false)
            {
                ForumArticleLike forumArticleLike = new ForumArticleLike
                {
                    ArticleId = id,
                    UserName = User.Identity.Name,
                };
                _forumArticleLikeService.Add(forumArticleLike);
                _forumArticleService.Like(id);
            }
            return RedirectToAction("Detail", new { id = id });
        }

        public ActionResult Unlike(int id)
        {
            ViewBag.ActiveForum = "active";
            bool Disliked = _forumArticleDisLikeService.CheckDisLike(id, User.Identity.Name);
            if (Disliked == false)
            {
                ForumArticleDisLike forumArticledISLike = new ForumArticleDisLike
                {
                    ArticleId = id,
                    UserName = User.Identity.Name,
                };
                _forumArticleDisLikeService.Add(forumArticledISLike);
                _forumArticleService.UnLike(id);
            }
            return RedirectToAction("Detail", new { id = id }); // h tạo lại từ đầu cũng đc,tạo migration lại
        }


        public ActionResult CreatePost()
        {
            ViewBag.ActiveForum = "active";
            return View();
        }

        [HttpPost]
        public ActionResult CreateComment(ForumComment forumComment)
        {
            string commentType = Request.Form["commentType"];
            ForumComment forumCmt = new ForumComment();
            forumCmt.ForumArticleID = forumComment.ForumArticleID;
            forumCmt.Content = forumComment.Content;
            forumCmt.Display = true;
            forumCmt.UserConfirmed = null;
            forumCmt.DateConfirmed = DateTime.Now;
            forumCmt.UserComment = forumComment.UserComment;
            forumCmt.DateComment = DateTime.Now;
            if (commentType == "true")
            {
                forumCmt.ForumCommentParentID = null;
            }
            else
            {
                forumCmt.ForumCommentParentID = forumComment.ForumCommentParentID;
            }
            _forumCommentService.Add(forumCmt);
            return RedirectToAction("Detail", new { id = forumComment.ForumArticleID });
        }

        public ActionResult EditPost()
        {
            ViewBag.ActiveForum = "active";
            return View();
        }
    }
}
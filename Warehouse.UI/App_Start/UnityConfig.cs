using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using Warehouse.Areas.Admin.Controllers;
using Warehouse.Controllers;
using Warehouse.Data.Data;
using Warehouse.Data.Interface;
using Warehouse.Models;
using Warehouse.Services.Interface;
using Warehouse.Services.Services;

namespace Warehouse
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ISlideService, SlideService>();
            container.RegisterType<ISlideDal, SlideDal>();

            container.RegisterType<IArticleService, ArticleService>();
            container.RegisterType<IArticleDal, ArticleDal>();

            container.RegisterType<IArticleCategoryService, ArticleCategoryService>();
            container.RegisterType<IArticleCategoryDal, ArticleCategoryDal>();

            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor(typeof(IProvinceService), typeof(IDistrictService), typeof(IWardService)));
            container.RegisterType<AspNetUserController>(new InjectionConstructor(typeof(IProvinceService), typeof(IDistrictService), typeof(IWardService)));
            container.RegisterType<ManageController>(new InjectionConstructor(typeof(IPlayGoldenGoalGameService), typeof(IProvinceService), typeof(IDistrictService), typeof(IWardService)));
            container.RegisterType<Controllers.GameController>(new InjectionConstructor(typeof(IPlayProphet1x2GameService), typeof(IProphet1x2GameDetailService), typeof(IProphet1x2GameService), typeof(IGoldenGoalGameService), typeof(IInfoWebService), typeof(IPlayGoldenGoalGameService)));

            container.RegisterType<IProvinceService, ProvinceService>();
            container.RegisterType<IProvinceDal, ProvinceDal>();

            container.RegisterType<IDistrictService, DistrictService>();
            container.RegisterType<IDistrictDal, DistrictDal>();

            container.RegisterType<IWardService, WardService>();
            container.RegisterType<IWardDal, WardDal>();

            container.RegisterType<IInfoWebService, InfoWebService>();
            container.RegisterType<IInfoWebDal, InfoWebDal>();

            container.RegisterType<ISubscriberService, SubscriberService>();
            container.RegisterType<ISubscriberDal, SubscriberDal>();

            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<ILanguageDal, LanguageDal>();

            container.RegisterType<ILeagueService, LeagueService>();
            container.RegisterType<ILeagueDal, LeagueDal>();

            container.RegisterType<IGoldenGoalGameService, GoldenGoalGameService>();
            container.RegisterType<IGoldenGoalGameDal, GoldenGoalGameDal>();

            container.RegisterType<IPlayGoldenGoalGameService, PlayGoldenGoalGameService>();
            container.RegisterType<IPlayGoldenGoalGameDal, PlayGoldenGoalGameDal>();

            container.RegisterType<IForumArticleService, ForumArticleService>();
            container.RegisterType<IForumArticleDal, ForumArticleDal>();

            container.RegisterType<IForumCommentService, ForumCommentService>();
            container.RegisterType<IForumCommentDal, ForumCommentDal>();

            container.RegisterType<IForumCategoryService, ForumCategoryService>();
            container.RegisterType<IForumCategoryDal, ForumCategoryDal>();

            container.RegisterType<IForumArticleLikeService, ForumArticleLikeService>();
            container.RegisterType<IForumArticleLikeDal, ForumArticleLikeDal>();

            container.RegisterType<IForumArticleDisLikeService, ForumArticleDisLikeService>();
            container.RegisterType<IForumArticleDisLikeDal, ForumArticleDisLikeDal>();

            container.RegisterType<IProphet1x2GameService, Prophet1x2GameService>();
            container.RegisterType<IProphet1x2GameDal, Prophet1x2GameDal>();

            container.RegisterType<IProphet1x2GameDetailService, Prophet1x2GameDetailService>();
            container.RegisterType<IProphet1x2GameDetailDal, Prophet1x2GameDetailDal>();

            container.RegisterType<IPlayProphet1x2GameService, PlayProphet1x2GameService>();
            container.RegisterType<IPlayProphet1x2GameDal, PlayProphet1x2GameDal>();

            container.RegisterType<IBlogService, BlogService>();
            container.RegisterType<IBlogDal, BlogDal>();

            container.RegisterType<IHandbookService, HandbookService>();
            container.RegisterType<IHandbookDal, HandbookDal>();

            container.RegisterType<IAlertService, AlertService>();
            container.RegisterType<IAlertDal, AlertDal>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
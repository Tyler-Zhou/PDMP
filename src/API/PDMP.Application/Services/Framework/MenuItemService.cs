/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-27 13:46:50
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using AutoMapper;
using System.Collections.ObjectModel;
using PDMP.Contract;
using PDMP.Domain;
using PDMP.Infrastructure;

namespace PDMP.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemService : IMenuItemService
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public MenuItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        #endregion

        #region 公用方法(Public Method)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<ObservableCollection<MenuItemInfo>> GetAllAsync(long userId)
        {
            return Task.Run(() =>
                {
                    ObservableCollection<MenuItemInfo> menuItems = new ObservableCollection<MenuItemInfo>()
                    {
                        new MenuItemInfo()
                        {
                            Id = 1,
                            Name ="MenuItemApplication",
                            Command = "",
                            CommandParameter ="",
                            Children =new ObservableCollection<MenuItemInfo>()
                            {
                                new MenuItemInfo()
                                {
                                    Id = 2,
                                    Name = "LoginOut",
                                    Command = "LoginOutCommand",
                                    CommandParameter = "",
                                },
                                new MenuItemInfo()
                                {
                                    Id = 3,
                                    Name = "ApplicationExit",
                                    Command = "ExitApplicationCommand",
                                    CommandParameter = "",
                                },
                            },
                        },
                        new MenuItemInfo()
                        {
                            Id = 4,
                            Name ="MenuItemFramework",
                            Command = "",
                            CommandParameter ="",
                            Children =new ObservableCollection<MenuItemInfo>()
                            {
                                new MenuItemInfo()
                                {
                                    Id = 5,
                                    Name = "User",
                                    Command = "NavigateViewCommand",
                                    CommandParameter = "UserView",
                                },
                                new MenuItemInfo()
                                {
                                    Id = 6,
                                    Name = "MenuItem",
                                    Command = "NavigateViewCommand",
                                    CommandParameter = "MenuItemView",
                                },
                                new MenuItemInfo()
                                {
                                    Id = 7,
                                    Name = "Role",
                                    Command = "NavigateViewCommand",
                                    CommandParameter = "RoleView",
                                },
                            },
                        },
                    };
                    return menuItems;
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<MenuItemInfo> AddAsync(MenuItemInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<MenuItemEntity>(modelInfo);
            modelEntity.CreateDate = DateTime.Now;
            await _UnitOfWork.GetRepository<MenuItemEntity>().InsertAsync(modelEntity);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<MenuItemInfo>(modelEntity);
            else
                throw new Exception("添加数据失败:更新行数错误");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<MenuItemEntity>();
            var modelFind = await repository
                .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            repository.Delete(modelFind);
            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return true;
            throw new Exception("删除数据失败:更新行数错误");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IPagedList<MenuItemInfo>> GetPagedListAsync(QueryParameter parameter)
        {
            var repository = _UnitOfWork.GetRepository<MenuItemEntity>();
            var modelPagedList = await repository.GetPagedListAsync(predicate:
               x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
               pageIndex: parameter.PageIndex,
               pageSize: parameter.PageSize,
               orderBy: source => source.OrderByDescending(t => t.CreateDate));
            var dbModel = _Mapper.Map<PagedList<MenuItemInfo>>(modelPagedList);
            return dbModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuItemInfo> GetSingleAsync(long id)
        {
            var repository = _UnitOfWork.GetRepository<MenuItemEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
            return _Mapper.Map<MenuItemInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelInfo"></param>
        /// <returns></returns>
        public async Task<MenuItemInfo> UpdateAsync(MenuItemInfo modelInfo)
        {
            var modelEntity = _Mapper.Map<MenuItemEntity>(modelInfo);
            var repository = _UnitOfWork.GetRepository<MenuItemEntity>();
            var modelFind = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(modelEntity.Id));

            modelFind.Name = modelEntity.Name;
            modelFind.UpdateDate = DateTime.Now;

            repository.Update(modelFind);

            if (await _UnitOfWork.SaveChangesAsync() > 0)
                return _Mapper.Map<MenuItemInfo>(modelFind);
            throw new Exception("更新数据异常！");
        }
        #endregion

    }
}


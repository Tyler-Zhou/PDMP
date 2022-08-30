using PDMP.Library.Client.Interfaces;
using PDMP.Library.Client.Models;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using PDMP.Client.Core.Common;

namespace PDMP.Library.Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BookViewModel: BindableBase
    {
        #region 成员(Member)
        #region 下拉列表选中状态值
        /// <summary>
        /// SelectedIndex
        /// </summary>
        private int _SelectedIndex = 0;
        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { _SelectedIndex = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 搜索条件
        /// <summary>
        /// Search
        /// </summary>
        private string _Search = "";
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return _Search; }
            set { _Search = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 右侧编辑窗口是否展开
        /// <summary>
        /// IsRightDrawerOpen
        /// </summary>
        private bool _IsRightDrawerOpen = false;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return _IsRightDrawerOpen; }
            set { _IsRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 编辑选中/新增时对象
        /// <summary>
        /// CurrentDto
        /// </summary>
        private BookModel _CurrentDto = new BookModel();
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public BookModel CurrentDto
        {
            get { return _CurrentDto; }
            set { _CurrentDto = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 书籍集合
        /// <summary>
        /// BookDtos
        /// </summary>
        private ObservableCollection<BookModel> _BookDtos = new ObservableCollection<BookModel>();
        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookModel> BookDtos
        {
            get { return _BookDtos; }
            set { _BookDtos = value; RaisePropertyChanged(); }
        }
        #endregion 
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 书籍服务
        /// </summary>
        private readonly IBookService _BookService;
        #endregion

        #region 命令
        /// <summary>
        /// 执行所有命令，根据类型参数选择不同操作
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        /// <summary>
        /// 选择
        /// </summary>
        public DelegateCommand<BookModel> SelectedCommand { get; private set; }
        /// <summary>
        /// 删除
        /// </summary>
        public DelegateCommand<BookModel> DeleteCommand { get; private set; }
        /// <summary>
        /// 编辑明细
        /// </summary>
        public DelegateCommand<BookModel> EditDetailCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 书籍视图模型
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="regionManager"></param>
        /// <param name="provider"></param>
        /// <param name="logger"></param>
        public BookViewModel(IBookService bookService, IRegionManager regionManager, IContainerProvider provider)
        {
            _BookService = bookService;
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<BookModel>(Selected);
            DeleteCommand = new DelegateCommand<BookModel>(Delete);
            EditDetailCommand = new DelegateCommand<BookModel>(EditDetail);
        }
        #endregion

        #region 重写方法(Override)
        
        #endregion

        #region 方法(Method)

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        private async void Delete(BookModel obj)
        {
            try
            {
                //var dialogResult = await _DialogHostService.Question("温馨提示", $"确认删除书籍:{obj.Name} ?");
                //if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                ////UpdateLoading(true);
                //var deleteResult = await _BookService.DeleteAsync(obj.Id);
                //if (deleteResult.Status)
                //{
                //    var model = BookDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                //    if (model != null)
                //        BookDtos.Remove(model);
                //}
            }
            finally
            {
                //UpdateLoading(false);
            }
        }
        /// <summary>
        /// 编辑明细
        /// </summary>
        /// <param name="obj"></param>
        private void EditDetail(BookModel obj)
        {
            NavigationParameters param = new NavigationParameters();
            if (obj != null)
                param.Add("BookModel", obj);
            //NavigationToView("ChapterView", param);
        }
        /// <summary>
        /// 选择数据(编辑/查看)
        /// </summary>
        /// <param name="obj"></param>
        private async void Selected(BookModel obj)
        {
            try
            {
                //UpdateLoading(true);
                var todoResult = await _BookService.GetFirstOfDefaultAsync(obj.Id);
                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result;
                    IsRightDrawerOpen = true;
                }
            }
            finally
            {
                //UpdateLoading(false);
            }
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="obj"></param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        private void Add()
        {
            CurrentDto = new BookModel();
            IsRightDrawerOpen = true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private async void Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentDto.Key)
                || string.IsNullOrWhiteSpace(CurrentDto.Name)
                || string.IsNullOrWhiteSpace(CurrentDto.Author)
                )
                    return;

                //UpdateLoading(true);
                if (!Guid.Empty.Equals(CurrentDto.Id))
                {
                    var updateResult = await _BookService.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var updateData = BookDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (updateData != null)
                        {
                            updateData.Key = CurrentDto.Key;
                            updateData.Name = CurrentDto.Name;
                            updateData.Author = CurrentDto.Author;
                            updateData.Link = CurrentDto.Link;
                            updateData.Tag = CurrentDto.Tag;
                            updateData.Introduction = CurrentDto.Introduction;
                            updateData.Status = CurrentDto.Status;
                        }
                        else
                        {
                            //SendMessage(updateResult.Message);
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var addResult = await _BookService.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        BookDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
                    else
                    {
                        //SendMessage(addResult.Message);
                    }
                }
            }
            finally
            {
                //UpdateLoading(false);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            try
            {
                //UpdateLoading(true);

                int? Status = null;
                if(SelectedIndex != 0 && SelectedIndex == 2)
                {
                    Status = 1;
                }else
                {
                    Status = 0;
                }

                var returnResult = await _BookService.GetAllFilterAsync(new BookParameter()
                {
                    PageIndex = 0,
                    PageSize = ApplicationContext.DefaultPageSize,
                    Search = Search,
                    Status = Status
                });

                if (returnResult.Status)
                {
                    BookDtos.Clear();
                    foreach (var item in returnResult.Result.Items)
                    {
                        BookDtos.Add(item);
                    }
                }
                else
                {
                    //SendMessage(returnResult.Message);
                }
            }
            finally
            {
                //UpdateLoading(false);
            }
        }
        #endregion
    }
}

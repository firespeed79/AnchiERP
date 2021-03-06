﻿using Anchi.ERP.Common.Filter;
using Anchi.ERP.Domain.RepairOrder;
using Anchi.ERP.Domain.RepairOrders.Filter;
using Anchi.ERP.Service.Customers;
using Anchi.ERP.Service.Employees;
using Anchi.ERP.Service.Repairs;
using Anchi.ERP.ServiceModel.Repairs;
using Anchi.ERP.UI.Web.Filter;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Anchi.ERP.UI.Web.Controllers
{
    [UserAuthorize]
    public class RepairController : BaseController
    {
        #region 构造函数和属性
        public RepairController(RepairOrderService repairOrderService, EmployeeService employeeService, CustomerService customerService)
        {
            this.RepairOrderService = repairOrderService;
            this.EmployeeService = employeeService;
            this.CustomerService = customerService;
        }

        RepairOrderService RepairOrderService { get; }

        EmployeeService EmployeeService { get; }

        CustomerService CustomerService { get; }
        #endregion

        #region 维修单管理
        /// <summary>
        /// 维修单管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            ViewBag.EmployeeList = EmployeeService.FindNormalList();
            return View("Index");
        }

        /// <summary>
        /// 维修单管理列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(FindRepairOrderFilter filter)
        {
            var result = RepairOrderService.FindList(filter);
            return new BetterJsonResult(result, true);
        }
        #endregion

        #region 新增维修单
        /// <summary>
        /// 新增维修单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.EmployeeList = EmployeeService.FindNormalList();

            var model = new RepairOrder();
            model.RepairOn = DateTime.Now;
            return View("Edit", model);
        }
        #endregion

        #region 修改维修单
        /// <summary>
        /// 修改维修单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ViewBag.EmployeeList = EmployeeService.FindNormalList();

            var model = RepairOrderService.Get(id);
            return View(model);
        }

        /// <summary>
        /// 获取维修单信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetEditModel(Guid Id)
        {
            var model = RepairOrderService.GetModel(Id);
            return new BetterJsonResult(model, true);
        }
        #endregion

        #region 保存维修单
        /// <summary>
        /// 保存维修单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RepairOrder model)
        {
            try
            {
                if (model.Id == Guid.Empty)
                    model.CreatedById = base.CurrentUser.Id;

                RepairOrderService.SaveOrUpdate(model);
                return new BetterJsonResult();
            }
            catch (Exception ex)
            {
                return new BetterJsonResult(ex.Message);
            }
        }
        #endregion

        #region 取消维修单
        /// <summary>
        /// 取消维修单
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cancel(IList<Guid> idList)
        {
            try
            {
                RepairOrderService.CancelOrder(idList);
                return new BetterJsonResult();
            }
            catch (Exception ex)
            {
                return new BetterJsonResult(ex.Message);
            }
        }
        #endregion

        #region 设置已完工
        /// <summary>
        /// 设置已完工
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Complete(IList<Guid> idList)
        {
            try
            {
                RepairOrderService.SetComplete(idList);
                return new BetterJsonResult();
            }
            catch (Exception ex)
            {
                return new BetterJsonResult(ex.Message);
            }
        }
        #endregion

        #region 设置已结算
        /// <summary>
        /// 设置已结算
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Settlement(IList<Guid> idList)
        {
            return new BetterJsonResult();
        }
        #endregion

        #region 结算维修单
        /// <summary>
        /// 结算维修单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settlement(Guid id)
        {
            return View();
        }

        /// <summary>
        /// 结算维修单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SettlementOrder(RepairSettlementModel model)
        {
            try
            {
                RepairOrderService.Settlement(model);
                return new BetterJsonResult();
            }
            catch (Exception ex)
            {
                return new BetterJsonResult(ex.Message);
            }
        }
        #endregion
    }
}
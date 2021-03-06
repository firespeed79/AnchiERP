﻿using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Anchi.ERP.Domain.PurchaseOrders.Enum
{
    /// <summary>
    /// 采购单状态
    /// </summary>
    [EnumAsInt]
    public enum EnumPurchaseOrderStatus
    {
        /// <summary>
        /// 采购中
        /// </summary>
        [Display(Name = "采购中")]
        Purchasing = 1,
        /// <summary>
        /// 全部到货
        /// </summary>
        [Display(Name = "全部到货")]
        Completed = 2,
    }
}

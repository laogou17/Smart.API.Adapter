using NEOCRM.Models.BEApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Biz.BEApi
{
    /// <summary>
    /// BE平台接入WebApi模块，调用CRM方法封装
    /// </summary>
    public class CRMApiBLL
    {
        #region 1.客户基本信息处理
        /// <summary>
        /// 1.客户基本信息处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerBaseInfoHanlder(CustomerBaseInfoEntity entity)
        {
            //CUSTOMER_BASE_INFO
            
        }
        #endregion

        #region 2.客户推荐人处理
        /// <summary>
        /// 2.客户推荐人处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerRecommendHanlder(CustomerRecommendEntity entity)
        {
            //CUSTOMER_RECOMMEND
            
        }
        #endregion

        #region 3.客户经理处理 CUSTOMER_MANAGER
        /// <summary>
        /// 3.客户经理处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerManagerHanlder(CustomerManagerEntity entity)
        {
            //CUSTOMER_MANAGER
            
        }
        #endregion

        #region 4.客户关键状态处理    CUSTOMER_STATUS
        /// <summary>
        /// 4.客户关键状态处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerStatusHanlder(CustomerStatusEntity entity)
        {
            //CUSTOMER_STATUS
            
        }
        #endregion

        #region 5.客户实名认证&绑卡处理 CUSTOMER_CARD
        /// <summary>
        /// 5.客户实名认证&绑卡处理 ？接口是否返回多条？
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerCardHanlder(CustomerCardEntity entity)
        {
            //CUSTOMER_CARD
            
        }
        #endregion

        #region 6.客户资金信息处理PartA   CUSTOMER_FOUNDS_A
        /// <summary>
        /// 6.客户资金信息处理PartA
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerFoundsAHanlder(CustomerFoundsAEntity entity)
        {
            //CUSTOMER_FOUNDS_A
            
        }
        #endregion

        #region 7.客户资金信息处理PartB   CUSTOMER_FOUNDS_B
        /// <summary>
        /// 7.客户资金信息处理PartB
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerFoundsBHanlder(CustomerFoundsBEntity entity)
        {
            //CUSTOMER_FOUNDS_B
            
        }
        #endregion

        #region 8.会员信息处理  CUSTOMER_MEMBER_INFO
        /// <summary>
        /// 8.会员信息处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerMemberInfoHanlder(CustomerMemberInfoEntity entity)
        {
            
        }
        #endregion

        #region 9.会员投资情况处理  CUSTOMER_INVESTMENT
        /// <summary>
        /// 9.会员投资情况处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerInvestmentHanlder(CustomerInvestmentEntity entity)
        {
            
        }
        #endregion

        #region 10.客户手机号变更历史记录处理    CUSTOMER_PHONES
        /// <summary>
        /// 10.客户手机号变更历史记录处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerPhonesHanlder(List<CustomerPhonesEntity> entitys)
        {
            
        }
        #endregion

        #region 11.交易记录处理   CUSTOMER_TRANSACTIONS
        /// <summary>
        /// 11.交易记录处理
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerTransactionsHanlder(List<CustomerTransactionsEntity> entitys)
        {
            
        }

        #endregion

        #region 12.提取信息处理	CUSTOMER_EXTRACT
        /// <summary>
        /// 12.提取信息处理	CUSTOMER_EXTRACT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerExtractHanlder(List<CustomerExtractEntity> entitys)
        {
            
        }
        #endregion

        #region 13.复投信息处理	CUSTOMER_REINVESTMENT
        /// <summary>
        /// 13.复投信息处理	CUSTOMER_REINVESTMENT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerReinvestmentHanlder(List<CustomerReinvestmentEntity> entitys)
        {
            
        }
        #endregion

        #region 14.债权转让	CUSTOMER_INVESTMENT_TRANSFER
        /// <summary>
        /// 14.债权转让记录处理	CUSTOMER_INVESTMENT_TRANSFER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerInvestmentTransferHanlder(List<CustomerInvestmentTransferEntity> entitys)
        {
            
        }
        #endregion

        #region 15.充值记录处理	ACCOUNT_RECHARGE
        /// <summary>
        /// 15.充值记录处理	ACCOUNT_RECHARGE
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountRechargeHanlder(List<AccountRechargeEntity> entitys)
        {
            
        }
        #endregion

        #region 16.提现记录处理	ACCOUNT_WITHDRAW
        public static void AccountWithdrawHanlder(List<AccountWithdrawEntity> entitys)
        {
            
        }
        #endregion

        #region 17.购买记录处理	ACCOUNT_PURCHASE
        public static void AccountPurchaseHanlder(List<AccountPurchaseEntity> entitys)
        {
            
        }
        #endregion

        #region 18.回款记录处理	ACCOUNT_BACK_SECTION
        /// <summary>
        /// 18.回款记录处理	ACCOUNT_BACK_SECTION
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountBackSectionHanlder(List<AccountBackSectionEntity> entitys)
        {
            
        }
        #endregion

        #region 19.补息记录记录处理	ACCOUNT_INTEREST
        /// <summary>
        /// 19.补息记录记录处理	ACCOUNT_INTEREST
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountInterestHanlder(List<AccountInterestEntity> entitys)
        {
            
        }

        #endregion

        #region 20.债券转让记录处理	ACCOUNT_BOND_TRANSFER
        /// <summary>
        /// 20.债券转让记录处理	ACCOUNT_BOND_TRANSFER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountBondTransferHanlder(List<AccountBondTransferEntity> entitys)
        {
            
        }

        #endregion

        #region 21.提取明细处理	ACCOUNT_EXTRACT
        /// <summary>
        /// 21.提取明细处理	ACCOUNT_EXTRACT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountExtractHanlder(List<AccountExtractEntity> entitys)
        {
            
        }

        #endregion

        #region 22.理财师奖励记录处理	ACCOUNT_FINALCIAL_PLANNER
        /// <summary>
        /// 22.理财师奖励记录处理	ACCOUNT_FINALCIAL_PLANNER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountFinalcialPlannerHanlder(List<AccountFinalcialPlannerEntity> entitys)
        {
            
        }

        #endregion

        #region 23.人脉奖励记录处理	ACCOUNT_CONTACTS
        /// <summary>
        /// 23.人脉奖励记录处理	ACCOUNT_CONTACTS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountContactsHanlder(List<AccountContactsEntity> entitys)
        {
            
        }

        #endregion

        #region 24.红包奖励记录处理	ACCOUNT_RED_PACKETS
        /// <summary>
        /// 24.红包奖励记录处理	ACCOUNT_RED_PACKETS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountRedPacketsHanlder(List<AccountRedPacketsEntity> entitys)
        {
            
        }

        #endregion

        #region 25.体验标处理（理财金账户）ACCOUNT_EXPERIENCE
        /// <summary>
        /// 25.体验标处理（理财金账户）ACCOUNT_EXPERIENCE
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void AccountExperienceHanlder(List<AccountExperienceEntity> entitys)
        {
            
        }

        #endregion

        #region 26.红包优惠券处理	REDPACKET_COUPONS
        /// <summary>
        /// 26.红包优惠券处理	REDPACKET_COUPONS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void RedpacketCouponsHanlder(List<RedpacketCouponsEntity> entitys)
        {
            
        }

        #endregion

        #region 27.参加过的活动	CUSTOMER_ACTIVITY
        /// <summary>
        /// 27.参加过的活动	CUSTOMER_ACTIVITY
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerActivityHanlder(List<CustomerActivityEntity> entitys)
        {
            
        }

        #endregion

        #region 28.活跃用户处理	CUSTOMER_ACTIVE_INCREMENTAL
        /// <summary>
        /// 28.活跃用户处理	CUSTOMER_ACTIVE_INCREMENTAL
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static void CustomerActiveIncrementalHanlder(List<CustomerActiveIncrementalEntity> entitys)
        {
            

            //var colletion = new List<CustomerActiveIncrementalEntity>();

            //var json = colletion.ToJson();

            //var obj = json.FromJson<List<CustomerActiveIncrementalEntity>>();

        }
        #endregion
    }
}

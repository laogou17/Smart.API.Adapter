using NEOCRM.Models.BEApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOCRM.Biz.BEApi
{
    /// <summary>
    /// BE平台接入WebApi模块，WebApi接口调用封装
    /// </summary>
    public static class BEApiBLL
    {
        #region 1.客户基本信息查询
        /// <summary>
        /// 1.客户基本信息查询
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerBaseInfoEntity GetCustomerBaseInfo(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_BASE_INFO
            
            return null;
        }
        #endregion

        #region 2.客户推荐人查询   CUSTOMER_RECOMMEND
        /// <summary>
        /// 2.客户推荐人查询   CUSTOMER_RECOMMEND
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerRecommendEntity GetCustomerRecommend(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_RECOMMEND
            return null;
        }
        #endregion

        #region 3.客户经理查询 CUSTOMER_MANAGER
        /// <summary>
        /// 3.客户经理查询 CUSTOMER_MANAGER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerManagerEntity GetCustomerManager(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_MANAGER
            return null;
        }
        #endregion

        #region 4.客户关键状态查询    CUSTOMER_STATUS
        /// <summary>
        /// 4.客户关键状态查询    CUSTOMER_STATUS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerStatusEntity GetCustomerStatus(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_STATUS
            return null;
        }
        #endregion

        #region 5.客户实名认证&绑卡查询 CUSTOMER_CARD
        /// <summary>
        /// 5.客户实名认证&绑卡查询
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerCardEntity> GetCustomerCard(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_CARD
            return null;
        }
        #endregion

        #region 6.客户资金信息查询PartA   CUSTOMER_FOUNDS_A
        /// <summary>
        /// 6.客户资金信息查询PartA   CUSTOMER_FOUNDS_A
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerFoundsAEntity GetCustomerFoundsA(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_FOUNDS_A
            return null;
        }
        #endregion

        #region 7.客户资金信息查询PartB   CUSTOMER_FOUNDS_B
        /// <summary>
        /// 7.客户资金信息查询PartB
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerFoundsBEntity GetCustomerFoundsB(BEApiParamsRequest apiParams)
        {
            //CUSTOMER_FOUNDS_B
            return null;
        }
        #endregion

        #region 8.会员信息查询  CUSTOMER_MEMBER_INFO
        /// <summary>
        /// 8.会员信息查询  CUSTOMER_MEMBER_INFO
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerMemberInfoEntity GetCustomerMemberInfo(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 9.会员投资情况查询  CUSTOMER_INVESTMENT
        /// <summary>
        /// 9.会员投资情况查询  CUSTOMER_INVESTMENT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static CustomerInvestmentEntity GetCustomerInvestment(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 10.客户手机号变更历史记录查询    CUSTOMER_PHONES
        /// <summary>
        /// 10.客户手机号变更历史记录查询    CUSTOMER_PHONES
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerPhonesEntity> GetCustomerPhones(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 11.交易记录查询   CUSTOMER_TRANSACTIONS
        /// <summary>
        /// 11.交易记录查询
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerTransactionsEntity> GetCustomerTransactions(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 12.提取信息查询	CUSTOMER_EXTRACT
        /// <summary>
        /// 12.提取信息查询	CUSTOMER_EXTRACT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerExtractEntity> GetCustomerExtract(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 13.复投信息查询	CUSTOMER_REINVESTMENT
        /// <summary>
        /// 13.复投信息查询	CUSTOMER_REINVESTMENT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerReinvestmentEntity> GetCustomerReinvestment(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 14.债权转让	CUSTOMER_INVESTMENT_TRANSFER
        /// <summary>
        /// 14.债权转让记录查询	CUSTOMER_INVESTMENT_TRANSFER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerInvestmentTransferEntity> GetCustomerInvestmentTransfer(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 15.充值记录查询	ACCOUNT_RECHARGE
        /// <summary>
        /// 15.充值记录查询	ACCOUNT_RECHARGE
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountRechargeEntity> GetAccountRecharge(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 16.提现记录查询	ACCOUNT_WITHDRAW
        public static List<AccountWithdrawEntity> GetAccountWithdraw(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 17.购买记录查询	ACCOUNT_PURCHASE
        public static List<AccountPurchaseEntity> GetAccountPurchase(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 18.回款记录查询	ACCOUNT_BACK_SECTION
        /// <summary>
        /// 18.回款记录查询	ACCOUNT_BACK_SECTION
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountBackSectionEntity> GetAccountBackSection(BEApiParamsRequest apiParams)
        {
            return null;
        }
        #endregion

        #region 19.补息记录记录查询	ACCOUNT_INTEREST
        /// <summary>
        /// 19.补息记录记录查询	ACCOUNT_INTEREST
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountInterestEntity> GetAccountInterest(BEApiParamsRequest apiParams)
        {
            return null;   
        }

        #endregion

        #region 20.债券转让记录查询	ACCOUNT_BOND_TRANSFER
        /// <summary>
        /// 20.债券转让记录查询	ACCOUNT_BOND_TRANSFER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountBondTransferEntity> GetAccountBondTransfer(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 21.提取明细查询	ACCOUNT_EXTRACT
        /// <summary>
        /// 21.提取明细查询	ACCOUNT_EXTRACT
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountExtractEntity> GetAccountExtract(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 22.理财师奖励记录查询	ACCOUNT_FINALCIAL_PLANNER
        /// <summary>
        /// 22.理财师奖励记录查询	ACCOUNT_FINALCIAL_PLANNER
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountFinalcialPlannerEntity> GetAccountFinalcialPlanner(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 23.人脉奖励记录查询	ACCOUNT_CONTACTS
        /// <summary>
        /// 23.人脉奖励记录查询	ACCOUNT_CONTACTS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountContactsEntity> GetAccountContacts(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 24.红包奖励记录查询	ACCOUNT_RED_PACKETS
        /// <summary>
        /// 24.红包奖励记录查询	ACCOUNT_RED_PACKETS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountRedPacketsEntity> GetAccountRedPackets(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 25.体验标查询（理财金账户）ACCOUNT_EXPERIENCE
        /// <summary>
        /// 25.体验标查询（理财金账户）ACCOUNT_EXPERIENCE
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<AccountExperienceEntity> GetAccountExperience(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 26.红包优惠券查询	REDPACKET_COUPONS
        /// <summary>
        /// 26.红包优惠券查询	REDPACKET_COUPONS
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<RedpacketCouponsEntity> RedpacketCoupons(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 27.参加过的活动	CUSTOMER_ACTIVITY
        /// <summary>
        /// 27.参加过的活动	CUSTOMER_ACTIVITY
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerActivityEntity> GetCustomerActivity(BEApiParamsRequest apiParams)
        {
            return null;
        }

        #endregion

        #region 28.活跃用户查询	CUSTOMER_ACTIVE_INCREMENTAL
        /// <summary>
        /// 28.活跃用户查询	CUSTOMER_ACTIVE_INCREMENTAL
        /// </summary>
        /// <param name="apiParams"></param>
        /// <returns></returns>
        public static List<CustomerActiveIncrementalEntity> GetCustomerActiveIncremental(BEApiParamsRequest apiParams)
        {
            return null;

            //var colletion = new List<CustomerActiveIncrementalEntity>();

            //var json = colletion.ToJson();

            //var obj = json.FromJson<List<CustomerActiveIncrementalEntity>>();
            
        }
        #endregion


        #region Post
        public static string BEApiPost(string jsonParams)
        {
            return null;

        }
        #endregion
    }
}
